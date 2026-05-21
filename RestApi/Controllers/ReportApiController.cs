using Microsoft.AspNetCore.Mvc;
using RestApi.Data;
using RestApi.Models;
using Shared.DTO;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportApiController(AppDbContext context)
        {
            _context = context;
        }

        // post api/reports
        [HttpPost]
        public async Task<IActionResult> ReportComment([FromBody] DTOReport dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var report = new Report
            {
                CommentId = dto.CommentId,
                ReportedUserId = dto.ReportedUserId,
                Reason = dto.Reason,
                ReportedAt = dto.ReportedAt
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment reported successfully" });
        }

        // Get report for admin 

        // post mute user
    }
}
