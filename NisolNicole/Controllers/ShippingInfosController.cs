using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Shipping.dtos;
using Application.UseCases.Shipping;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Shipping")]
    public class ShippingInfosController : ControllerBase
    {
        private readonly UsecaseCreateShippingInfos _usecaseCreateShippingsInfos;

        public ShippingInfosController(UsecaseCreateShippingInfos usecaseCreateShippingsInfos)
        {
            _usecaseCreateShippingsInfos = usecaseCreateShippingsInfos;
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoShippingInfos> Create([FromBody] InputShippingInfosDto dto)
        {
            return StatusCode(201, _usecaseCreateShippingsInfos.Execute(dto));
        }
    }
}
