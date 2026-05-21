using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Data;
using RestApi.Models;
using static Shared.DTO.DTOComment;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentApiController(AppDbContext context)
        {
            _context = context;
        }

        // Get the comments for a specific world, including the owner's name
        [HttpGet("{worldId}")]
        public async Task<IActionResult> GetComments(int worldId)
        {
            var comments = await _context.Comments
                .Include(c => c.User) 
                .Where(c => c.WorldId == worldId)
                .OrderBy(c => c.CreatedAt)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    OwnerName = c.User != null ? c.User.FullName : "Unknown"
                })
                .ToListAsync();

            return Ok(comments);
        }

        // POST for adding a new comment to a world
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = new Comment
            {
                Content = dto.Content,
                WorldId = dto.WorldId,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment added successfully" });
        }
    }
}
