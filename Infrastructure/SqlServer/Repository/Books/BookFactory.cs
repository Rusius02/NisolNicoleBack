using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Books
{
    public class BookFactory : IDomainFactory<Domain.Book>
    {
        public Domain.Book CreateFromSqlReader(SqlDataReader reader)
        {
            return new Domain.Book()
            {
                Id = reader.GetInt32(reader.GetOrdinal(BookRepository.ColId)),
                Name = reader.GetString(reader.GetOrdinal(BookRepository.ColName)),
                Description = reader.GetString(reader.GetOrdinal(BookRepository.ColDescription)),
                Price = (double)reader.GetDecimal(reader.GetOrdinal(BookRepository.ColPrice)),
                ISBN = reader.GetString(reader.GetOrdinal(BookRepository.ColISBN))

            };
        }
    }
}
