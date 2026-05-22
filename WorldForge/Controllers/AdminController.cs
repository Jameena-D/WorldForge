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
                return View(new List<ReportedWorldViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var worlds = JsonSerializer.Deserialize<List<ReportedWorldViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<ReportedWorldViewModel>();

            return View(worlds);
        }

        // POST /Admin/DeleteWorld
        [HttpPost]
        public async Task<IActionResult> DeleteWorld(int worldId)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.DeleteAsync($"api/admin/reports/world/{worldId}");

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode
                    ? "World deleted successfully."
                    : "Failed to delete the world.";

            return RedirectToAction("ReportedWorlds");
        }

        // POST /Admin/UnpublishWorld
        [HttpPost]
        public async Task<IActionResult> UnpublishWorld(int worldId)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.PatchAsync(
                $"api/admin/reports/world/{worldId}/unpublish", null);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode
                    ? "World made non-public successfully."
                    : "Failed to unpublish the world.";

            return RedirectToAction("ReportedWorlds");
        }

        // POST /Admin/DismissReport
        [HttpPost]
        public async Task<IActionResult> DismissReport(int reportId)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.PostAsync(
                $"api/admin/reports/{reportId}/dismiss", null);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode
                    ? "Report dismissed."
                    : "Failed to dismiss the report.";

            return RedirectToAction("ReportedWorlds");
        }
    }
}
