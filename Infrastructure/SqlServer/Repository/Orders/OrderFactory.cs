using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Orders
{
    public class OrderFactory : IDomainFactory<Domain.Order>
    {
        public Domain.Order CreateFromSqlReader(SqlDataReader reader)
        {
            return new Domain.Order()
            {
                Id = reader.GetInt32(reader.GetOrdinal(OrderRepository.ColId)),
                Title = reader.GetString(reader.GetOrdinal(OrderRepository.ColTitle)),
                Description = reader.GetString(reader.GetOrdinal(OrderRepository.ColDescription)),
                Price = (double)reader.GetDecimal(reader.GetOrdinal(OrderRepository.ColPrice)),
                ISBN = reader.GetString(reader.GetOrdinal(OrderRepository.ColISBN)),
                CoverImagePath = reader.GetString(reader.GetOrdinal(OrderRepository.ColCoverImagePath))
            };
        }
    }
}
