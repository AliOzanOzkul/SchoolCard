using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentIdCard.Application.Dto_s;
using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Application.ViewModels;
using StudentIdCard.Application.ViewModels.BaseCard;
using StudentIdCard.Domain.Entites;
using StudentIdCard.Persistence.Context;
using System.Net;

namespace StudentIdCard.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardReadRepository _cardReadRepository;
        private readonly ICardWriteRepository _cardWriteRepository;
        private readonly IBaseCardWriteRepository _baseCardWriteRepository;
        private readonly IBaseCardReadRepository _baseCardReadRepository;
        private readonly StudentIdCardContext _context;
        private readonly ICardUpdateRepository _cardUpdateRepository;
        private readonly string UploadDirectory = "Photos"; // Bu dizini kendi proje yapınıza uygun olarak değiştirin.

        public CardController(ICardReadRepository cardReadRepository, ICardWriteRepository cardWriteRepository, IBaseCardWriteRepository baseCardWriteRepository, IBaseCardReadRepository baseCardReadRepository, StudentIdCardContext context, ICardUpdateRepository cardUpdateRepository)
        {
            _cardReadRepository = cardReadRepository;
            _cardWriteRepository = cardWriteRepository;
            _baseCardWriteRepository = baseCardWriteRepository;
            _baseCardReadRepository = baseCardReadRepository;
            _context = context;
            _cardUpdateRepository = cardUpdateRepository;
        }
        [HttpGet("GetAllCardInfo")]
        public IActionResult GetAllCardInfo()
        {
            var selectedAllCard = _context.BaseCardsTable.Include(data => data.Card).Select(x => new { CardNo = x.CardNo, Name = x.Card.Name,Surname = x.Card.Surname});
            return Ok(selectedAllCard);
        }

        [HttpPost("CardInfo")]
        public async Task<IActionResult> GetCardInfo(CardInfo card)
        {
            return Ok(await _cardReadRepository.GetByIdAsync(card.Id));
        }
        [HttpPost("CreateBaseCard")]
        public async Task<IActionResult> CreateBaseCard(VM_Create_BaseCard card)
        {
            var createdCard = await _baseCardWriteRepository.AddAsync(new BaseCard()
            {
                CardNo = card.CardNo,
                
            });
           await _baseCardWriteRepository.SaveAsync();
            return Ok(createdCard);
        }
        [HttpPost("GetBaseCardNo")]
        public  async Task<IActionResult> GetBaseCardNo(VM_Create_BaseCard vM_Create_BaseCard)
        {
            var selectedCard = _baseCardReadRepository.GetWhere(data => data.CardNo == vM_Create_BaseCard.CardNo);

            await _context.AddAsync(selectedCard);
            await _context.SaveChangesAsync();
            return Ok(selectedCard);
        }
        [HttpPost("CreateCard")]
        public async Task<IActionResult> CreateCard(VM_Create_Card card)
        {
            var selectedBaseCard = _baseCardReadRepository.GetAll().ToList();
            BaseCard baseCard = selectedBaseCard.OrderByDescending(card => card.CreatedTime) 
    .FirstOrDefault();
            Card newCard = new()
            {
                Name = card.Name,
                Surname = card.Surname,
                ParentName = card.ParentName,
                ParentPhoneNumber = card.ParentPhoneNumber,
                SchoolId = card.SchoolId,
                Balance = card.Balance,
                ParentUserName = card.ParentUserName,
                ParentPassword = card.ParentPassword,
                BaseCard = baseCard,
                PhotoPath = card.PhotoPath
                
            };
            Card selectedCardControl = await _cardReadRepository.GetSingleAsync(data => data.ParentUserName == card.ParentUserName);
            if (selectedCardControl == null)
            {
                await _cardWriteRepository.AddAsync(newCard);
                await _cardWriteRepository.SaveAsync();
                Card selectedCard = await _cardReadRepository.GetSingleAsync(data => data.ParentUserName == card.ParentUserName);
                return Ok(selectedCard.Id);

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("MoneyToCard")]
        public async Task<IActionResult> MoneyToCard(MoneyToCardDTO moneyToCardDTO)
        {
            BaseCard selectedCard = await _baseCardReadRepository.GetSingleAsync(data => data.CardNo == moneyToCardDTO.CardId);
            selectedCard.Card.Balance += moneyToCardDTO.Money;
            int result = await _cardWriteRepository.SaveAsync();
            return Ok(result);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya seçilmedi.");
            }

            try
            {
                var yuklemeDizini = ".\\StudentIdCard\\Presentation\\StudentIdCard.Presentation\\Photos";

                // Dizinin var olduğundan emin olun, yoksa oluşturun
                Directory.CreateDirectory(yuklemeDizini);

                var dosyaYolu = Path.Combine(yuklemeDizini, file.FileName);

                using (var akis = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await file.CopyToAsync(akis);
                }

                return Ok($"Dosya başarıyla yüklendi. Yol: {dosyaYolu}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"İç sunucu hatası: {ex}");
            }
        }

        

    }
}
