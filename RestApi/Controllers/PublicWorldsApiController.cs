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

        // To get details of a specific public world, including its sections and blocks
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicWorldDetail(int id)
        {
            var world = await _context.Worlds
                .Include(w => w.Sections)
                    .ThenInclude(s => s.Blocks)
                .FirstOrDefaultAsync(w => w.Id == id && w.IsPublic);

            if (world == null) return NotFound();

            return Ok(new
            {
                world.Id,
                world.Name,
                world.Description,
                WorldType = world.WorldType.ToString(),
                world.IsPublic,
                Sections = world.Sections.Select(s => new
                {
                    s.Id,
                    s.Title,
                    Blocks = s.Blocks.Select(b => new { b.Id, b.Name, b.Content })
                })
            });
        }
    }
}
