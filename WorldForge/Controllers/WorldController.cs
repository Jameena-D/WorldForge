using Microsoft.AspNetCore.Mvc;
using WorldForge.ViewModel;
using WorldForge.Models;
using WorldForge.Data;

namespace WorldForge.Controllers
{
    public class WorldController : Controller
    {
        private readonly AppDbContext _context;

        public WorldController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateWorldViewModel
            {
                // Initialize any default values for the form here if needed
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

        [HttpPost]
        public IActionResult Create(CreateWorldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If we got this far, something failed; redisplay form with validation errors
                return View(model);
            }

            var world = new World
            {
                Name = model.Name,
                Description = model.Description,
                WorldType = model.WorldType!.Value,
                IsPublic = model.IsPublic,
            };
            _context.Worlds.Add(world);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Wereld succesvol opgeslagen.";

            return RedirectToAction("Index", "Home");
        }
    }
}
