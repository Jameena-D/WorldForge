using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorldForge.ViewModel;

namespace WorldForge.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WorldForgeApi");
        }

        // Guard: only admins may access this area
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        // GET /Admin/ReportedWorlds
        public async Task<IActionResult> ReportedWorlds()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.GetAsync("api/admin/reports");

            if (!response.IsSuccessStatusCode)
                return View(new List<ReportWorldViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var worlds = JsonSerializer.Deserialize<List<ReportWorldViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<ReportWorldViewModel>();

            return View(worlds);
        }
    }
}
