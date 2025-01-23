using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Utils;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Orders
{
    public class OrderFactory : IDomainFactory<Domain.Order>
    {
        public Domain.Order CreateFromSqlReader(SqlDataReader reader)
        {
            return new Domain.Order()
            {
                OrderId = reader.GetInt32(reader.GetOrdinal(OrderRepository.ColOrderId)),
                UserId = reader.GetInt32(reader.GetOrdinal(OrderRepository.ColUserId)),
                Amount = reader.GetDecimal(reader.GetOrdinal(OrderRepository.ColAmount)),
                PaymentStatus = reader.GetString(reader.GetOrdinal(OrderRepository.ColPaymentStatus)),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal(OrderRepository.ColCreatedAt))
            };
        }
    }
}
