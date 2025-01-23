using Application.UseCases.Orders.Dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.Orders;

namespace Application.UseCases.Orders
{
    public class UsecaseCreateOrder
    {
        private readonly IOrderRepository _orderRepository;
        public UsecaseCreateOrder(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /*Method that will create an User using an InputDTO given as an argument
         and that will return an Activity OutputDto*/
        public OutputDtoCreateOrder Execute(InputDtoCreateOrder dto)
        {
            var orderFromDto = Mapper.GetInstance().Map<Domain.Order>(dto);

            var orderFromDb = _orderRepository.Create(orderFromDto);
            return Mapper.GetInstance().Map<OutputDtoCreateOrder>(orderFromDb);
        }
    }
}
