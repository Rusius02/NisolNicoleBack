using Domain;

namespace Infrastructure.SqlServer.Repository.Orders
{
    public interface IOrderRepository
    {
        Domain.Order Create(Domain.Order order);

        List<Domain.Order> GetAll();

        Domain.Order GetOrderById(int id);

        bool Delete(Domain.Order order);

        bool UpdatePaymentStatus(int orderId, string paymentStatus);
        bool Update(Order order);
    }
}
