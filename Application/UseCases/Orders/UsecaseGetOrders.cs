using Application.UseCases.Orders.Dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.Orders;
using NisolNicole.Utils.Dtos;

namespace Application.UseCases.Orders
{
    public class UsecaseGetOrders
    {
        private readonly IOrderRepository _orderRepository;
        public UsecaseGetOrders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<OutputDtoOrder> Execute(int userId)
        {
            List<Domain.Order> orders = _orderRepository.GetOrderByUserId(userId);
            return Mapper.GetInstance().Map<List<OutputDtoOrder>>(orders);
        }
        
    }
}
