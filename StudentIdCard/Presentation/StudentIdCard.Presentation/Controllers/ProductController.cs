using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentIdCard.Application.Repositories.Product;
using StudentIdCard.Application.ViewModels.Product;
using StudentIdCard.Domain.Entites;

namespace StudentIdCard.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(_productReadRepository.GetAll());
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(VM_Create_Product model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Stoct = model.Stoct

            };
           bool result = await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();
            if (result)
            {
                return Ok("Ekleme Başarılı");
            }
            else
            {
                return Ok("Ekleme Başarısız");
            }
        }
    }
}
