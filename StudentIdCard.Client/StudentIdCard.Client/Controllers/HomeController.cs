using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentIdCard.Client.Models;
using System.Text;

namespace StudentIdCard.Client.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            HttpClient _httpClient = new();
            string baseAddress = _configuration["Server"];

            var apiUrl = baseAddress + "api/User/Login"; // Hedef API'nin URL'si

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);
            var responseContent2 = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode && responseContent2 != "Böyle bir kullanıcı bulunamadı")
            {
                LoginUser responseContent = await response.Content.ReadFromJsonAsync<LoginUser>();
                if (responseContent.Role == "Admin")
                {

                    Response.Cookies.Append("Id", responseContent.Id);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Response.Cookies.Append("Id", responseContent.Id);
                    
                    return RedirectToAction("Index", "Parent");
                }
            }
            else
            {
                ViewBag.Message = "Hata";
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
            HttpClient _httpClient = new();
            var apiUrl = _configuration["Server"] + "api/User/CreateUser"; // Hedef API'nin URL'si

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }

            return View();
        }


    }
}