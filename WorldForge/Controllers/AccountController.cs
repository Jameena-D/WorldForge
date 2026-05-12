using Microsoft.AspNetCore.Mvc;
using WorldForge.ViewModel;
using System.Text;
using System.Text.Json;

namespace WorldForge.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("WorldForgeApi");

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Construct full URL for API login
            var fullUrl = new Uri(client.BaseAddress, "api/account/login");
            Console.WriteLine($"Calling API URL: {fullUrl}");

            var response = await client.PostAsync("api/account/login", content);

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login failed. Status: {response.StatusCode}, Body: {errorBody}");
            }

            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("WorldForgeApi");

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Construct full URL for API registration
            var fullUrl = new Uri(client.BaseAddress, "api/account/register");
            Console.WriteLine($"Calling API URL: {fullUrl}");

            var response = await client.PostAsync("api/account/register", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Registration failed. Status: {response.StatusCode}, Body: {errorBody}");
            }

            ModelState.AddModelError("", "Registration failed");
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
