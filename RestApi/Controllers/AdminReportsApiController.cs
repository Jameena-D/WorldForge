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
    }
}
