using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using RestApi.Data;

namespace RestApi.Controllers
{
    public class PublicWorldsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PublicWorldsApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/worlds/public
        [HttpGet("public")]
        public async Task<IActionResult> GetPublicWorlds()
        {
            var worlds = await _context.Worlds
                .Where(w => w.IsPublic)
                .OrderByDescending(w => w.CreatedAt) // Get newest first
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
