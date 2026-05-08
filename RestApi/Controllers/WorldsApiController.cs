using Microsoft.AspNetCore.Mvc;
using WorldForge.ViewModel;
using WorldForge.Models;
using WorldForge.Data;

namespace WorldForge.Controllers
{
    [ApiController]
    [Route("api/Worlds")]
    public class WorldsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorldsApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateWorldViewModel model)
        {
            // T1 : Validate the incoming model
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var world = new World
            {
                Name = model.Name,
                Description = model.Description,
                WorldType = model.WorldType!.Value,
                IsPublic = model.IsPublic,
                CreatedAt = DateTime.UtcNow
            };

            _context.Worlds.Add(world);
            _context.SaveChanges();

            return Ok(new { message = "World created successfully. To add more details to the world and make use of the sections edit the world.", worldId = world.Id }
                );
        }
    }
}
