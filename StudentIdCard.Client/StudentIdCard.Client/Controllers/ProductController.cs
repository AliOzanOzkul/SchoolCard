using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentIdCard.Client.Models;
using System.Text;

namespace StudentIdCard.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            if(ModelState.IsValid)
            {
                HttpClient _httpClient = new();
                string Id = Request.Cookies["id"];
                string baseAddress = _configuration["Server"];

                var apiUrl = baseAddress+"api/Product/CreateProduct"; // Hedef API'nin URL'si

                var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("CreateProduct");
                }

            }
            return View();
        }

        public async Task<IActionResult> ProductList()
        {

            HttpClient _httpClient = new();
            string Id = Request.Cookies["id"];
            string baseAddress = _configuration["Server"];

            var apiUrl = baseAddress+"api/Product/GetAllProduct"; // Hedef API'nin URL'si

                

                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                List<CreateProduct> responseContent = await response.Content.ReadFromJsonAsync<List<CreateProduct>>();
                    return Ok(responseContent);
                }

            
            return View();
        }
    }
}
