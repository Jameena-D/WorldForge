using Microsoft.AspNetCore.Mvc;
using RestApi.Data;

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

        // Get report for admin 

        // post mute user
    }
}
