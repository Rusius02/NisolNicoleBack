using Domain;
using Infrastructure.SqlServer.Utils;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.SiteTraffic
{
    public partial class SiteTrafficRepository : ISiteTrafficRepository
    {
        private readonly IDomainFactory<SiteVisit> _factory = new SiteTrafficFactory();
        public SiteVisit Create(SiteVisit siteVisit)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreate
            };
            command.Parameters.AddWithValue("@" + ColIP, siteVisit.IpAddress);
            command.Parameters.AddWithValue("@" + ColDate, siteVisit.VisitedAt);

            siteVisit.Id = (int)command.ExecuteScalar();

            return siteVisit;
        }

        public List<SiteVisit> GetAll()
        {
            var siteVisits = new List<SiteVisit>();
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetAll
            };

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                siteVisits.Add(_factory.CreateFromSqlReader(reader));
            }

            return siteVisits;
        }

    }
}
