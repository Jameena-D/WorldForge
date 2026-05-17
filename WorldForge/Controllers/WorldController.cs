using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Shared.DTO;
using System.Text.Json;
using WorldForge.ViewModel;
using static Shared.DTO.DTOWorld;


namespace WorldForge.Controllers
{
    public class WorldController : Controller
    {
        private readonly HttpClient _httpClient;

        // The HttpClient is injected via constructor injection, and we use a named client "WorldForgeApi" which should be configured in Program.cs to point to the base URL of our REST API.
        public WorldController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WorldForgeApi");
        }

        // GET: World/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // We create a DTO (Data Transfer Object) to send to the API. This DTO matches the expected structure of the API endpoint for creating a world.
            var dto = new CreateWorldRequest
            {
                Name = model.Name,
                Description = model.Description,
                WorldType = model.WorldType!.Value,
                IsPublic = model.IsPublic,
                UserId = HttpContext.Session.GetString("UserId") ?? string.Empty    
            };

            var response = await _httpClient.PostAsJsonAsync("api/worlds", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "🌍 World is saved!";
                return RedirectToAction("MyWorlds", "World");
            }

            ModelState.AddModelError("", "World could not be created.");
            return View(model);
        }

        // GET: World/MyWorlds
        public async Task<IActionResult> MyWorlds(string searchTerm = "")
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.GetAsync($"api/worlds/myworlds/{userId}");

            if (!response.IsSuccessStatusCode)
                return View(new List<WorldViewModel>());

            // We read the response content as a string and then deserialize it into a list of WorldViewModel objects. The JsonSerializerOptions with PropertyNameCaseInsensitive set to true allows for case-insensitive matching of JSON property names to C# property names.
            var json = await response.Content.ReadAsStringAsync();
            var worlds = JsonSerializer.Deserialize<List<WorldViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // To search worlds by name
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                worlds = worlds.Where(w => w.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Saves the searchterm so it can be shown
            ViewBag.SearchTerm = searchTerm;

            return View(worlds);
        }

        // GET: World/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditWorld(int id)
        {
            var response = await _httpClient.GetAsync($"api/worlds/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("MyWorlds");

            var json = await response.Content.ReadAsStringAsync();
            var world = JsonSerializer.Deserialize<EditWorldViewModel>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(world);
        }

    }
}
