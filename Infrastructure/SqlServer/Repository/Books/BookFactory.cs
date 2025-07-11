using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Books
{
    public class BookFactory : IDomainFactory<Domain.Book>
    {
        public Domain.Book CreateFromSqlReader(SqlDataReader reader)
        {
            int stripeProductIdIndex = reader.GetOrdinal(BookRepository.ColStripeProductId);
            return new Domain.Book()
            {
                Id = reader.GetInt32(reader.GetOrdinal(BookRepository.ColId)),
                Title = reader.GetString(reader.GetOrdinal(BookRepository.ColTitle)),
                Description = reader.GetString(reader.GetOrdinal(BookRepository.ColDescription)),
                Price = reader.GetDouble(reader.GetOrdinal(BookRepository.ColPrice)),
                ISBN = reader.GetString(reader.GetOrdinal(BookRepository.ColISBN)),
                CoverImagePath = reader.GetString(reader.GetOrdinal(BookRepository.ColCoverImagePath)),
                QuantityInStock = reader.GetInt32(reader.GetOrdinal(BookRepository.ColQuantityStock)),
                StripeProductId = reader.IsDBNull(stripeProductIdIndex)
                    ? null
                    : reader.GetString(stripeProductIdIndex)
            };
        }
    }
}
