using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using WorldForge.DTO;
using WorldForge.Models;
using WorldForge.ViewModel;
using static WorldForge.DTO.DTOWorld;


namespace WorldForge.Controllers
{
    public class WorldController : Controller
    {
        private readonly HttpClient _httpClient;

        public WorldController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WorldForgeApi");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new CreateWorldRequest
            {
                Name = model.Name,
                Description = model.Description,
                WorldType = model.WorldType!.Value,
                IsPublic = model.IsPublic
            };

            var response = await _httpClient.PostAsJsonAsync("api/worlds", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "🌍 World is saved! Edit your world to add more details.";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "World could not be created.");
            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateWorldViewModel
            {
                // Initialize any default values for the form here if needed is not currently being used but is in preperation for future function.
                Sections = new List<WorldSectionInputViewModel>
                {
                    new WorldSectionInputViewModel { Title = "Lore", Blocks = new() { new() } },
                    new WorldSectionInputViewModel { Title = "Characters", Blocks = new() { new() } },
                    new WorldSectionInputViewModel { Title = "Races & classes", Blocks = new() { new() } },
                    new WorldSectionInputViewModel { Title = "Flora & fauna", Blocks = new() { new() } },
                    new WorldSectionInputViewModel { Title = "Locations", Blocks = new() { new() } },
                    new WorldSectionInputViewModel { Title = "Extra", Blocks = new() { new() } }
                }
            };
            return View(model);
        }
    }
}
