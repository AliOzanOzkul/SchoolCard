using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentIdCard.Application.Dto_s;
using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Application.Repositories.Basket;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Application.Repositories.Product;
using StudentIdCard.Application.Repositories.User;
using StudentIdCard.Application.ViewModels.BaseCard;
using StudentIdCard.Application.ViewModels.User;
using StudentIdCard.Domain.Entites;
using StudentIdCard.Persistence.Context;
using StudentIdCard.Persistence.Services;

namespace StudentIdCard.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanteenController : ControllerBase
    {
        private readonly ICardWriteRepository _cardWriteRepository;
        private readonly ICardReadRepository _cardReadRepository;
        private readonly StudentIdCardContext _context;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IBaseCardReadRepository _baseCardReadRepository;
        private readonly IUserReadRepository _userReadRepository;

        public CanteenController(ICardWriteRepository cardWriteRepository, StudentIdCardContext context, IBasketWriteRepository basketWriteRepository, ICardReadRepository cardReadRepository, IProductReadRepository productReadRepository, IBasketReadRepository basketReadRepository, IProductWriteRepository productWriteRepository, IBaseCardReadRepository baseCardReadRepository, IUserReadRepository userReadRepository)
        {
            _cardWriteRepository = cardWriteRepository;
            _context = context;
            _basketWriteRepository = basketWriteRepository;
            _cardReadRepository = cardReadRepository;
            _productReadRepository = productReadRepository;
            _basketReadRepository = basketReadRepository;
            _productWriteRepository = productWriteRepository;
            _baseCardReadRepository = baseCardReadRepository;
            _userReadRepository = userReadRepository;
        }
        [HttpPost("AddActiveCard")]
        public async Task<IActionResult> AddActiveCard(VM_Create_BaseCard model)
        {
            await _context.AktiveCard.AddAsync(new()
            {
                CardNo = model.CardNo
            });
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("AddToBasket")]
        public async Task<IActionResult> AddToBasket(BasketDTO basket)
        {
            try
            {
                AktiveCard aktiveCard = _context.AktiveCard.ToList().OrderByDescending(card => card.CreatedTime)
    .FirstOrDefault();
                BaseCard baseCard = await _baseCardReadRepository.GetSingleAsync(data => data.CardNo == aktiveCard.CardNo);
                Card card = await _cardReadRepository.GetSingleAsync(data => data.BaseCardId == baseCard.Id);
                List<Product> products = new();
                foreach (var item in basket.productId)
                {
                    products.Add(await _productReadRepository.GetByIdAsync(item));
                }
                card.Baskets.Add(new()
                {
                    Products = products
                });
                _context.SaveChanges();
                var sonBasketId = _basketReadRepository.GetAll().OrderBy(x => x.Id).Last().Card.Id.ToString();
                card = await _cardReadRepository.GetByIdAsync(sonBasketId);


                try
                {
                    var result = await PayToBill(card.Baskets.Last().Id.ToString());
                    if (result is ActionResult actionResult)
                    {
                        if (actionResult is OkResult)
                        {
                            // Payment successful, you can return Ok or any other response
                            return Ok();
                        }
                        else
                        {
                            // Payment failed, handle accordingly
                            return Ok("Payment failed: " + actionResult.ToString());
                        }
                    }
                    else
                    {
                        // Handle the case where result is not of type ActionResult
                        return Ok("Invalid result type");
                    }



                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                    throw;
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PayToBill")]
        public async Task<IActionResult> PayToBill(string billId)
        {
            Basket basket = await _basketReadRepository.GetByIdAsync(billId);
            var selectedCard = _context.AktiveCard.ToList().Last();
            BaseCard baseCard = await _baseCardReadRepository.GetSingleAsync(data => data.CardNo == selectedCard.CardNo);
            Card card1 = await _cardReadRepository.GetSingleAsync(data => data.BaseCardId == baseCard.Id);

            decimal totalPrice = 0;
            Basket selectedBasket = await _context.Baskets.Include(c => c.Products).FirstOrDefaultAsync(data => data.Id == card1.Baskets.ToList().Last().Id);


            foreach (var item in selectedBasket.Products)
            {
                if (item.Stoct > 0)
                {
                    Product selectedProduct = await _productReadRepository.GetByIdAsync(item.Id.ToString());
                    selectedProduct.Stoct -= 1;
                }
                totalPrice += item.Price;
            }
            Card card = basket.Card;
            if (card.Balance < totalPrice)
            {
                SmsService smsService = new SmsService();

                SmsService sms = new();
                StudentIdCard.Domain.Entites.Card selectedStudent = await _cardReadRepository.GetByIdAsync(card1.Id.ToString());
                //sms.SendSms(selectedStudent.ParentPhoneNumber.ToString(), $"Öğrenciniz {DateTime.Now} kartında yeterli bakiye bulunmamaktadır. ");
                return BadRequest("Karta para yüklemelisiniz");
            }
            else
            {
                card.Balance -= totalPrice;
                selectedBasket.PaymentStatus = true;
                await _basketWriteRepository.SaveAsync();
                await _cardWriteRepository.SaveAsync();
                await _productWriteRepository.SaveAsync();
                return Ok("Ödeme Başarılı");
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var entity = _context.Products.ToList();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DailyFoodForStudent")]
        public async Task<IActionResult> DailyFoodForStudent(VM_Create_User model)
        {
            // Kullanıcıyı bul
            StudentIdCard.Domain.Entites.Card user = await _cardReadRepository.GetSingleAsync(data => data.ParentUserName == model.UserName && data.ParentPassword == model.Password);

            if (user != null)
            {
                // Bugün için kullanıcının sepetinden ürünleri seç
                var selectedProducts = _context.Baskets
                    .Include(data => data.Products)
                    .Where(x => x.Card.Id == user.Id && x.CreatedTime.Date == DateTime.Now.Date)
                    .SelectMany(x => x.Products)
                    .ToList();

                // Sadece ürün isimlerini seç ve geri dön
                var selectedProductNames = selectedProducts.Select(product => new { Name = product.Name }).ToList();

                return Ok(selectedProductNames);
            }

            return BadRequest();
        }

    }
}
