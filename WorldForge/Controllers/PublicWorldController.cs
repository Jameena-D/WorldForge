using Microsoft.AspNetCore.Mvc;

namespace WorldForge.Controllers
{
    public class PublicWorldController : Controller
    {
        // Get public worlds
        public async Task<IActionResult> PublicWorlds(string searchTerm = "")
        {
            // Here you would typically call your API to get the list of public worlds, possibly filtering by the search term.
            // For demonstration purposes, we'll just return the view with the search term.
            ViewBag.SearchTerm = searchTerm;
            return View();
        }
    }
}
