using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using WorldForge.ViewModel;
using static Shared.DTO.DTOLogin;

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

    var response = await client.PostAsync("api/account/login", content);

    var responseBody = await response.Content.ReadAsStringAsync();
    Debug.WriteLine("Login API Response: " + responseBody);

    if (response.IsSuccessStatusCode)
    {
        var loginResponse = JsonSerializer.Deserialize<LoginResponse>(
            responseBody,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (loginResponse != null)
        {
            HttpContext.Session.SetString("UserId", loginResponse.UserId);
            HttpContext.Session.SetString("Email", loginResponse.Email);
            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("IsAdmin", loginResponse.IsAdmin ? "true" : "false");
            return RedirectToAction("MyWorlds", "World");
        }
    }
    else
    {
        Console.WriteLine($"Login failed. Status: {response.StatusCode}, Body: {responseBody}");
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
