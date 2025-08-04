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
        private readonly UsecaseGetOrders _usecaseGetOrders;

        public OrderController(UsecaseCreateOrder usecaseCreateOrder, UsecaseGetOrders usecaseGetOrders)
        {
            _usecaseCreateOrder = usecaseCreateOrder;
            _usecaseGetOrders = usecaseGetOrders;
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateOrder> CreateOrder([FromBody] InputDtoCreateOrder dto)
        {
            dto.PaymentStatus = "Order_pending";
            return _usecaseCreateOrder.Execute(dto);
        }
        [HttpPost]
        [Route("OrdersByUserID")]
        public ActionResult<List<OutputDtoOrder>> GetOrdersByUserID([FromBody] int userID)
        {
            return _usecaseGetOrders.Execute(userID);
        }
    }
}
