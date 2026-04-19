using Microsoft.AspNetCore.Mvc;
using WorldForge.ViewModel;
using WorldForge.Models;
using WorldForge.Data;

namespace WorldForge.Controllers
{
    public class WorldController : Controller
    {

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
