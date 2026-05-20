using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using RestApi.Data;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/publicworlds")]
    public class PublicWorldsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PublicWorldsApiController(AppDbContext context)
        {
            _context = context;
        }

        // Get the public worlds
        [HttpGet]
        public async Task<IActionResult> GetPublicWorlds()
        {
            // We query the database for worlds that are marked as public, order them by creation date in descending order.
            var worlds = await _context.Worlds
                .Where(w => w.IsPublic)
                .OrderByDescending(w => w.CreatedAt)
                .Select(w => new
                {
                    w.Id,
                    w.Name,
                    w.Description,
                    WorldType = w.WorldType.ToString(),
                    w.IsPublic
                })
                .ToListAsync();

            return Ok(worlds);
        }
    }
}
