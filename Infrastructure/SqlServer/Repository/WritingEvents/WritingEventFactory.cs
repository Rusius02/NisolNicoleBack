using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.WritingEvents
{
    public class WritingEventFactory : IDomainFactory<Domain.WritingEvent>
    {
        public Domain.WritingEvent CreateFromSqlReader(SqlDataReader reader)
        {
            return new Domain.WritingEvent()
            {
                Id = reader.GetInt32(reader.GetOrdinal(WritingEventRepository.ColId)),
                Name = reader.GetString(reader.GetOrdinal(WritingEventRepository.ColName)),
                Description = reader.GetString(reader.GetOrdinal(WritingEventRepository.ColDescription)),
                Theme = reader.IsDBNull(reader.GetOrdinal(WritingEventRepository.ColTheme))
                        ? null
                        : reader.GetString(reader.GetOrdinal(WritingEventRepository.ColTheme)),
                StartDate = reader.GetDateTime(reader.GetOrdinal(WritingEventRepository.ColStartDate)),
                EndDate = reader.GetDateTime(reader.GetOrdinal(WritingEventRepository.ColEndDate)),
            };
        }
    }
}
