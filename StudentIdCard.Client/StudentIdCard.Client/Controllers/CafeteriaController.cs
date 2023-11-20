using Microsoft.AspNetCore.Mvc;
using StudentIdCard.Client.Models;

namespace StudentIdCard.Client.Controllers
{
    public class CafeteriaController : Controller
    {
        private readonly IConfiguration _configuration;

        public CafeteriaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient _httpClient = new();
            string Id = Request.Cookies["id"];
            string baseAddress = _configuration["Server"];

            var apiUrl = baseAddress + "api/Cafeteria/GetHasEaten"; // Hedef API'nin URL'si



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
