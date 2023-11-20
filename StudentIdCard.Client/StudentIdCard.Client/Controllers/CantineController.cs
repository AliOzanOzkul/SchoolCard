using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentIdCard.Client.Models;
using System.Text;

namespace StudentIdCard.Client.Controllers
{
    public class CantineController : Controller
    {
        private readonly IConfiguration _configuration;

        public CantineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            HttpClient _httpClient = new();
            string baseAddress = _configuration["Server"];
            var apiUrl = baseAddress + "api/Canteen/GetProductList";
            var response = await _httpClient.GetAsync(apiUrl);
            List<ProductView> products = await response.Content.ReadFromJsonAsync<List<ProductView>>();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] List<ProductView> basket)
        {

            List<string> productIds = basket.Select(item => item.Id.ToString()).ToList();

         
            var addToBasketUrl = _configuration["Server"] + "api/Canteen/AddToBasket";
            var addToBasketContent = new StringContent(JsonConvert.SerializeObject(new { productId = productIds }), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var addToBasketResponse = await httpClient.PostAsync(addToBasketUrl, addToBasketContent);
                if (addToBasketResponse.IsSuccessStatusCode)
                {
                    return Ok(Json("success"));
                }
                else
                {
                    // Hata durumunda yapılacak işlemler
                    ViewBag.Message = "Hata";
                    return BadRequest("Hata");
                }
            }
        }

        
    }
}

