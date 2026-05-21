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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostReport(int worldId, int commentId, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "Reason cannot be empty.";
                return RedirectToAction("PublicWorldDetail", "PublicWorld", new { id = worldId });
            }

            var reporterUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var dto = new
            {
                CommentId = commentId,
                ReporterUserId = reporterUserId,
                Reason = reason
            };

            var response = await _httpClient.PostAsJsonAsync("api/report", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Comment got reported!";
                return RedirectToAction("PublicWorldDetail", "PublicWorld", new { id = worldId });
            }

            TempData["ErrorMessage"] = "Something went wrong while reporting the comment.";
            return RedirectToAction("PublicWorldDetail", "PublicWorld", new { id = worldId });
        }

        // admin notification page

        // admin mute server
    }
}
