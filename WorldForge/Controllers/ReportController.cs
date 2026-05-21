using Microsoft.AspNetCore.Mvc;

namespace WorldForge.Controllers
{
    public class ReportController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReportController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WorldForgeApi");
        }

        // post reports

        // admin notification page

        // admin mute server
    }
}
