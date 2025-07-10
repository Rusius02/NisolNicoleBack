using Application.UseCases.Books;
using Application.UseCases.Books.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class StockController : ControllerBase
    {
        private readonly UseCaseUpdateStock _useCaseUpdateStock;
        private readonly UseCaseGetStock _useCaseGetStock;
        public StockController(UseCaseUpdateStock useCaseUpdateStock, UseCaseGetStock useCaseGetStock)
        {
            _useCaseUpdateStock = useCaseUpdateStock;
            _useCaseGetStock = useCaseGetStock;
        }

        [HttpPost]
        [Route("GetBook")]
        [ProducesResponseType(200)]
        public ActionResult<int> GetBook([FromBody] InputDtoStock inputDtoStock)
        {
            return StatusCode(200, _useCaseGetStock.Execute(inputDtoStock));
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [Route("updateStock")]
        public ActionResult<bool> Update([FromForm] InputDtoStock book)
        {
            _useCaseUpdateStock.Execute(book);
            return StatusCode(200);
        }
    }
}
