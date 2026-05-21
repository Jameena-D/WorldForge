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
    }
}
