using Application.UseCases.Orders;
using Application.UseCases.Orders.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Orders")]
    public class OrderController
    {
        private readonly UsecaseCreateOrder _usecaseCreateOrder;

        public OrderController(UsecaseCreateOrder usecaseCreateOrder)
        {
            _usecaseCreateOrder = usecaseCreateOrder;
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateOrder> CreateOrder([FromBody] InputDtoCreateOrder dto)
        {
            dto.PaymentStatus = "Order_pending";
            return _usecaseCreateOrder.Execute(dto);
        }
    }
}
