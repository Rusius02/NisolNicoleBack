using Application.UseCases.SiteTraffic;
using Microsoft.AspNetCore.Mvc;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly SiteTrafficService _visitService;

        public StatsController(SiteTrafficService visitService)
        {
            _visitService = visitService;
        }

        [HttpPost("visit")]
        public async Task<IActionResult> RegisterVisit()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _visitService.RegisterVisitAsync(ip);
            return Ok();
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetVisitCount()
        {
            var count = await _visitService.GetTotalVisitsAsync();
            return Ok(new { visits = count });
        }
    }
}
