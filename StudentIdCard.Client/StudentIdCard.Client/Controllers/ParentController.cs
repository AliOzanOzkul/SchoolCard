using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentIdCard.Client.Models;
using System.Net.Http;
using System.Text;


namespace StudentIdCard.Client.Controllers
{
    public class ParentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ParentController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient _httpClient = new();
            string Id = Request.Cookies["id"];
            string baseAddress = _configuration["Server"];

            var apiUrl = baseAddress + "api/Card/CardInfo";

            CartInfo cart = new()
            {
                Id = Id
            };
            var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            Card responseContent = await response.Content.ReadFromJsonAsync<Card>();
            return View(responseContent);

        }

        public IActionResult Money()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Money(decimal money)
        {
            if (ModelState.IsValid)
            {
                HttpClient _httpClient = new();
                var apiUrl = _configuration["Server"] +"api/Card/MoneyToCard";
                Money m1 = new()
                {
                    cardId = Request.Cookies["Id"],
                    money = money
                };

                var content = new StringContent(JsonConvert.SerializeObject(m1), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("Index");
                }

            }
         

            return View();
        }
        public async Task<IActionResult> GetDailyFood()
        {
            HttpClient _httpClient = new();
            string Id = Request.Cookies["id"];
            string baseAddress = _configuration["Server"];

            var apiUrl = baseAddress + "api/Card/CardInfo";

            CartInfo cart = new()
            {
                Id = Id
            };
            var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            Card responseContent = await response.Content.ReadFromJsonAsync<Card>();

          
          

            apiUrl = baseAddress + "api/Canteen/DailyFoodForStudent";

            StudentIdCard.Client.Models.User user = new()
            {
                UserName = responseContent.ParentUserName,
                Password = responseContent.ParentPassword
            };
            var content2 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            string baseAddress2 = _configuration["Server"];
            var apiUrl2 = baseAddress + "api/Canteen/DailyFoodForStudent";
            var response2 = await _httpClient.PostAsync(apiUrl,content2);
            List<GetFood> products = await response2.Content.ReadFromJsonAsync<List<GetFood>>();
            return View(products);
      

        }
       


    }
}
