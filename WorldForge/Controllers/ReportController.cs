using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WorldForge.ViewModel;

namespace WorldForge.Controllers
{
    public class ReportController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReportController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WorldForgeApi");
        }

        // post reports
        [HttpPost]
        public async Task<IActionResult> PostReport(ReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PublicWorldDetail", "PublicWorld");
            }

            var dto = new
            {
                CommentId = model.CommentId,
                ReporterUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                Reason = model.Reason
            };

            var json = JsonSerializer.Serialize(dto);
            var response = await _httpClient.PostAsync("api/reports", new StringContent(json, Encoding.UTF8, "application/json"));

            TempData["Success"] = response.IsSuccessStatusCode ?
                "Comment reported successfully." :
                "Failed to report the comment.";

            return RedirectToAction("PublicWorldDetail", "PublicWorld");
        }

        // admin notification page

        // admin mute server
    }
}
