using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Shared.DTO;
using Shared.Enum;
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
            // We get the userId from the session to ensure that we only fetch worlds that belong to the currently logged-in user. 
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var response = await _httpClient.GetAsync($"api/worlds/myworlds/{userId}");

            if (!response.IsSuccessStatusCode)
                return View(new List<WorldViewModel>());

            // We read the response content as a string and then deserialize it into a list of WorldViewModel objects. The JsonSerializerOptions with PropertyNameCaseInsensitive set to true allows for case-insensitive matching of JSON property names to C# property names.
            var json = await response.Content.ReadAsStringAsync();
            var worlds = JsonSerializer.Deserialize<List<WorldViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                // Null-coalescing operator to ensure that if the deserialization returns null (which can happen if the API returns an empty response or if there's an issue with the JSON), we will have an empty list instead of a null reference.
                ?? new List<WorldViewModel>(); 

            // To search worlds by name
            if (!string.IsNullOrWhiteSpace(searchTerm) && worlds != null) // We check if the searchTerm is not null, empty, or whitespace, and also ensure that worlds is not null before attempting to filter it. This prevents potential null reference exceptions.
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

            // If the world is null, we redirect to the MyWorlds page.
            if (world == null)
                return RedirectToAction("MyWorlds");

            if (world.Sections.Count == 0)
            {
                world.Sections = new List<WorldForge.ViewModel.WorldSectionInputViewModel>
                {
                    new WorldForge.ViewModel.WorldSectionInputViewModel { Title = "Lore", Blocks = new List<WorldForge.ViewModel.WorldBlockInputViewModel> { new() } },
                    new WorldForge.ViewModel.WorldSectionInputViewModel { Title = "Characters", Blocks = new List<WorldForge.ViewModel.WorldBlockInputViewModel> { new() } },
                    new WorldForge.ViewModel.WorldSectionInputViewModel { Title = "Locations", Blocks = new List<WorldForge.ViewModel.WorldBlockInputViewModel> { new() } },
                    new WorldForge.ViewModel.WorldSectionInputViewModel { Title = "Flora & Fauna", Blocks = new List<WorldForge.ViewModel.WorldBlockInputViewModel> { new() } },
                    new WorldForge.ViewModel.WorldSectionInputViewModel { Title = "Extra", Blocks = new List<WorldForge.ViewModel.WorldBlockInputViewModel> { new() } }
                };
            }
            return View(world);
        }

        // POST: World/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorld(EditWorldViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // DTO voor API
            var dto = new DTOWorld.UpdateWorldRequest
            {
                Id = model.Id,
                UserId = HttpContext.Session.GetString("UserId") ?? string.Empty,
                Name = model.Name ?? string.Empty,
                Description = model.Description ?? string.Empty,
                WorldType = Enum.TryParse<WorldTypeEnum>(model.WorldType, out var wt) ? wt : WorldTypeEnum.Fantasy,
                IsPublic = model.IsPublic,
                Sections = model.Sections?.Select(s => new Shared.DTO.DTOWorld.WorldSectionInputViewModel
                {
                    Title = s.Title ?? string.Empty,
                    Blocks = s.Blocks?.Select(b => new Shared.DTO.DTOWorld.WorldBlockInputViewModel
                    {
                        Name = b.Name ?? string.Empty,
                        Content = b.Content ?? string.Empty
                    }).ToList() ?? new List<Shared.DTO.DTOWorld.WorldBlockInputViewModel>()
                }).ToList() ?? new List<Shared.DTO.DTOWorld.WorldSectionInputViewModel>()
            };

            // PUT request naar API
            var response = await _httpClient.PutAsJsonAsync("api/worlds", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "🌍 World updated successfully!";
                return RedirectToAction("MyWorlds");
            }

            ModelState.AddModelError("", "World could not be updated.");
            return View(model);
        }

        // Delete world
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorld(int id)
        {
            // We get the userId from the session to ensure that only the owner of the world can delete it.
            var userId = HttpContext.Session.GetString("UserId") ?? string.Empty;

            var response = await _httpClient.DeleteAsync($"api/worlds/{id}?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "World deleted successfully!";
                return RedirectToAction("MyWorlds");
            }

            TempData["ErrorMessage"] = "Could not delete world.";
            return RedirectToAction("MyWorlds");
        }
    }
}
