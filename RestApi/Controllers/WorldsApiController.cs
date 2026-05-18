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

        // To create a new world
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

        // To get all worlds of a user for the "My Worlds" page
        [HttpGet("myworlds/{userId}")]
        public async Task<IActionResult> GetMyWorlds(string userId)
        {
            var worlds = await _context.Worlds
                .Where(w => w.UserId == userId)
                .ToListAsync();
            return Ok(worlds.Select(w => new
            {
                w.Id,
                w.Name,
                w.Description,
                WorldType = w.WorldType.ToString(),
                w.IsPublic,
                w.UserId
            }));
        }

        // To get details of a specific world, including its sections and blocks
        // .Include is used to load related data (sections and blocks) in a single query or it will be null in response
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorld(int id)
        {
            var world = await _context.Worlds
                .Include(w => w.Sections)
                    .ThenInclude(s => s.Blocks)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (world == null) return NotFound();

            return Ok(new
            {
                world.Id,
                world.Name,
                world.Description,
                WorldType = world.WorldType.ToString(),
                world.IsPublic,
                world.UserId,
                Sections = world.Sections.Select(s => new
                {
                    s.Id,
                    s.Title,
                    Blocks = s.Blocks.Select(b => new
                    {
                        b.Id,
                        b.Name,
                        b.Content
                    })
                })
            });
        }

        // To update an existing world, including its sections and blocks
        [HttpPut]
        public IActionResult UpdateWorld([FromBody] DTOWorld.UpdateWorldRequest request)
        {
            // Get the world with its sections and blocks to update them
            var world = _context.Worlds
                .Include(w => w.Sections)
                    .ThenInclude(s => s.Blocks)
                .FirstOrDefault(w => w.Id == request.Id && w.UserId == request.UserId);

            if (world == null)
                return NotFound("World not found or you are not the owner.");

            world.Name = request.Name;
            world.Description = request.Description;
            world.WorldType = request.WorldType;
            world.IsPublic = request.IsPublic;

            // Remove old sections (and their blocks via cascade)
            _context.WorldSections.RemoveRange(world.Sections);

            // Clear the in-memory list
            world.Sections.Clear();

            // Remove old sections (and their blocks via cascade)
            _context.WorldSections.RemoveRange(world.Sections);
            world.Sections.Clear();

            if (request.Sections != null)
            {
                foreach (var s in request.Sections)
                {
                    var section = new WorldSection
                    {
                        Title = s.Title ?? string.Empty,
                        WorldId = world.Id,
                        Blocks = s.Blocks?.Select(b => new WorldBlock
                        {
                            Name = b.Name ?? string.Empty,
                            Content = b.Content ?? string.Empty
                        }).ToList() ?? new List<WorldBlock>()
                    };
                    world.Sections.Add(section);
                }
            }

            _context.SaveChanges();

            return Ok(new { message = "World updated successfully" });
        }

    }
}
