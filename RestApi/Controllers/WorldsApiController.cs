using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using RestApi.Data;
using Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/worlds")]
    public class WorldApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorldApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateWorld([FromBody] DTOWorld.CreateWorldRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var world = new RestApi.Models.World
            {
                Name = request.Name,
                Description = request.Description,
                WorldType = request.WorldType,
                IsPublic = request.IsPublic,
                CreatedAt = DateTime.UtcNow,
                UserId = request.UserId
            };

            _context.Worlds.Add(world);
            _context.SaveChanges();

            return Ok(new { worldId = world.Id });
        }

        [HttpGet("myworlds/{userId}")]
        public async Task<IActionResult> GetMyWorlds(string userId)
        {
            var worlds = await _context.Worlds
                .Where(w => w.UserId == userId)
                .ToListAsync();
            return Ok(worlds);
        }
    }
}
