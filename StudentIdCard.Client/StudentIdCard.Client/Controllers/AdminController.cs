using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentIdCard.Client.Models;
using System.Text;

namespace StudentIdCard.Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult CreateCard()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCard(Card card, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                HttpClient _httpClient = new();
                var apiUrl = _configuration["Server"] + "api/Card/CreateCard";
                if (file != null)
                {

                    string fileName = await UploadFile(file);
                    card.PhotoPath = fileName;
                }
               
                var content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("Index");
                }

            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Money()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Money(string id, decimal money)
        {
            if (ModelState.IsValid)
            {
                HttpClient _httpClient = new();
                var apiUrl = _configuration["Server"] + "api/Card/MoneyToCard";
                Money m1 = new()
                {
                    cardId = id,
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
        public async Task<IActionResult> GetAttendance()
        {
            HttpClient _httpClient = new();
            string baseAddress = _configuration["Server"];
            var apiUrl = baseAddress + "api/Attendance";
            var response = await _httpClient.GetAsync(apiUrl);
            List<Attendance> products = await response.Content.ReadFromJsonAsync<List<Attendance>>();
            return View(products);

        }
        public async Task<IActionResult> GetAllCards()
        {
            HttpClient _httpClient = new();
            string baseAddress = _configuration["Server"];
            var apiUrl = baseAddress + "api/Card/GetAllCardInfo";
            var response = await _httpClient.GetAsync(apiUrl);
            List<GetAllCardInfo> allCard = await response.Content.ReadFromJsonAsync<List<GetAllCardInfo>>();
            return View(allCard);

        }
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "Dosya seçilmedi.";
            }

            try
            {
                var yuklemeDizini = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                // Dizinin var olduğundan emin olun, yoksa oluşturun
                Directory.CreateDirectory(yuklemeDizini);

                // Dosya adını değiştirme ve GUID ekleme
                var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var dosyaYolu = Path.Combine(yuklemeDizini, dosyaAdi);

                using (var akis = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await file.CopyToAsync(akis);
                }

                return dosyaAdi;
            }
            catch (Exception ex)
            {
                return $"İç sunucu hatası: {ex}";
            }
        }
    }
}
