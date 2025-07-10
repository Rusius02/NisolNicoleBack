using Application.UseCases.Books;
using Application.UseCases.Books.Dtos;
using Application.UseCases.SiteTraffic;
using Application.UseCases.SiteTraffic.dtos;
using Domain;
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
        public ActionResult<SiteVisitDto> RegisterVisit()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            return StatusCode(201, _visitService.RegisterVisit(ip));
        }

        [HttpGet("count")]
        public ActionResult<List<SiteVisitDto>> GetVisitCount()
        {
            return StatusCode(200, _visitService.GetAllVisits());
        }
    }
}
