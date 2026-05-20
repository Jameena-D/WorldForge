using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorldForge.ViewModel;

namespace WorldForge.Controllers
{
    public class PublicWorldController : Controller
    {
        private readonly HttpClient _httpClient;

        // The HttpClient is injected via constructor injection, and we use a named client "WorldForgeApi" which should be configured in Program.cs to point to the base URL of our REST API.
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
    }
}
