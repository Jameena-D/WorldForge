using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using RestApi.Data;
using Shared.DTO;

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
                CreatedAt = DateTime.UtcNow
            };

            _context.Worlds.Add(world);
            _context.SaveChanges();

            return Ok(new { worldId = world.Id });
        }
    }
}
