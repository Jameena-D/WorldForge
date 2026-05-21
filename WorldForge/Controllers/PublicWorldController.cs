using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WorldForge.ViewModel;

namespace WorldForge.Controllers
{
    public class PublicWorldController : Controller
    {
        private readonly HttpClient _httpClient;

        public PublicWorldController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WorldForgeApi");
        }

        // Get public worlds
        public async Task<IActionResult> PublicWorlds()
        {
            var response = await _httpClient.GetAsync("api/publicworlds");

            if (!response.IsSuccessStatusCode)
                return View(new List<PublicWorldViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var worlds = JsonSerializer.Deserialize<List<PublicWorldViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(worlds);
        }

        // Get details of a specific public world, including its comments
        public async Task<IActionResult> PublicWorldDetail(int id)
        {
            var worldResponse = await _httpClient.GetAsync($"api/publicworlds/{id}");

            if (!worldResponse.IsSuccessStatusCode)
                return RedirectToAction("PublicWorlds");

            var worldJson = await worldResponse.Content.ReadAsStringAsync();
            var world = JsonSerializer.Deserialize<EditWorldViewModel>(worldJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var commentsResponse = await _httpClient.GetAsync($"api/comments/{id}");
            var comments = new List<CommentViewModel>();
            if (commentsResponse.IsSuccessStatusCode)
            {
                var commentsJson = await commentsResponse.Content.ReadAsStringAsync();
                comments = JsonSerializer.Deserialize<List<CommentViewModel>>(commentsJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }

            var vm = new PublicWorldDetailViewModel
            {
                Id = world!.Id,
                Name = world.Name,
                Description = world.Description,
                WorldType = world.WorldType,
                IsPublic = world.IsPublic,
                Sections = world.Sections,
                Comments = comments
            };

            return View(vm);
        }

        // Post a new comment on a public world
        [HttpPost]
        public async Task<IActionResult> PostComment(int worldId, string content)
        {
            var userId = HttpContext.Session.GetString("UserId");

            var dto = new 
            { 
                WorldId = worldId, 
                Content = content, 
                UserId = userId 
            };

            var json = JsonSerializer.Serialize(dto);
            await _httpClient.PostAsync("api/comments",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return RedirectToAction("PublicWorldDetail", new { id = worldId });
        }
    }
}
