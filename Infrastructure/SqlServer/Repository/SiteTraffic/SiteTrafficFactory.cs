using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Repository.Users;
using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.SiteTraffic
{
    public class SiteTrafficFactory : IDomainFactory<Domain.SiteVisit>
    {
        public Domain.SiteVisit CreateFromSqlReader(SqlDataReader reader)
        {
            return new Domain.SiteVisit()
            {
                Id = reader.GetInt32(reader.GetOrdinal(SiteTrafficRepository.ColId)),
                VisitedAt = reader.GetDateTime(reader.GetOrdinal(SiteTrafficRepository.ColDate)),
                IpAddress = reader.GetString(reader.GetOrdinal(SiteTrafficRepository.ColIP))
            };
        }
    }
}
