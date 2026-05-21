using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Data;
using RestApi.Models;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/admin/reports")]
    public class AdminReportsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminReportsApiController(AppDbContext context)
        {
            _context = context;
        }

        // Get all reports for admin review
        [HttpGet]
        public async Task<IActionResult> GetReportedWorlds()
        {
            var reports = await _context.ReportWorlds
                .Include(r => r.World)
                    .ThenInclude(w => w.User)
                .Include(r => r.ReporterUser)
                .Where(r => !r.IsHandled)
                .OrderByDescending(r => r.ReportedAt)
                .GroupBy(r => r.WorldId)
                .Select(g => new
                {
                    WorldId = g.Key,
                    WorldName = g.First().World.Name,
                    OwnerName = g.First().World.User!.FullName,
                    IsPublic = g.First().World.IsPublic,
                    ReportCount = g.Count(),
                    Reports = g.Select(r => new
                    {
                        r.Id,
                        r.Reason,
                        r.ReportedAt,
                        ReporterName = r.ReporterUser.FullName
                    }).ToList()
                })
                .ToListAsync();

            return Ok(reports);
        }

        // DELETE api/admin/reports/world/{worldId}
        [HttpDelete("world/{worldId}")]
        public async Task<IActionResult> DeleteWorld(int worldId)
        {
            var world = await _context.Worlds.FindAsync(worldId);
            if (world == null)
                return NotFound("World not found.");

            // Mark all related reports as handled before deleting the world
            var reports = await _context.ReportWorlds
                .Where(r => r.WorldId == worldId)
                .ToListAsync();

            foreach (var r in reports)
                r.IsHandled = true;

            _context.Worlds.Remove(world);
            await _context.SaveChangesAsync();

            return Ok(new { message = "World deleted and reports resolved." });
        }

        // PATCH api/admin/reports/world/{worldId}/unpublish
        [HttpPatch("world/{worldId}/unpublish")]
        public async Task<IActionResult> UnpublishWorld(int worldId)
        {
            var world = await _context.Worlds.FindAsync(worldId);
            if (world == null)
                return NotFound("World not found.");

            world.IsPublic = false;

            var reports = await _context.ReportWorlds
                .Where(r => r.WorldId == worldId && !r.IsHandled)
                .ToListAsync();

            foreach (var r in reports)
                r.IsHandled = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "World unpublished and reports resolved." });
        }

        // POST api/admin/reports/{reportId}/dismiss
        [HttpPost("{reportId}/dismiss")]
        public async Task<IActionResult> DismissReport(int reportId)
        {
            var report = await _context.ReportWorlds.FindAsync(reportId);
            if (report == null)
                return NotFound("Report not found.");

            report.IsHandled = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Report dismissed." });
        }
    }
}
