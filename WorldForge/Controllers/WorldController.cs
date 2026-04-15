using Microsoft.AspNetCore.Mvc;
using WorldForge.ViewModel;

namespace WorldForge.Controllers
{
    public class WorldController : Controller
    {
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
            if (ModelState.IsValid)
            {
                // Here you would typically save the world to the database
                // For now, we'll just redirect to a success page or the world details page
                TempData["SuccessMessage"] = "Wereld succesvol opgeslagen.";
                return RedirectToAction("Index", "Home"); // Redirect to home or world details page
            }
            // If we got this far, something failed; redisplay form with validation errors
            return View(model);
        }
    }
}
