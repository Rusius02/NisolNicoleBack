using Infrastructure.SqlServer.Utils;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.ShippingInfos
{
    public partial class ShippingInfosRepository : ShippingInfosIRepository
    {
        private readonly IDomainFactory<Domain.ShippingInfos> _factory = new ShippingInfosFactory();
        public Domain.ShippingInfos Create(Domain.ShippingInfos shippingInfos)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreate
            };
            command.Parameters.AddWithValue("@" + ColFullName, shippingInfos.FullName);
            command.Parameters.AddWithValue("@" + ColMail, shippingInfos.Email);
            command.Parameters.AddWithValue("@" + ColPhoneNumber, shippingInfos.PhoneNumber);
            command.Parameters.AddWithValue("@" + ColAddressStreet, shippingInfos.AddressLine1);
            command.Parameters.AddWithValue("@" + ColAddressNumber, shippingInfos.AddressLine2);
            command.Parameters.AddWithValue("@" + ColAddressZip, shippingInfos.PostalCode);
            command.Parameters.AddWithValue("@" + ColAddressCity, shippingInfos.City);
            command.Parameters.AddWithValue("@" + ColAddressCountry, shippingInfos.Country);
            command.Parameters.AddWithValue("@" + ColOrderId, shippingInfos.OrderId);
            command.Parameters.AddWithValue("@" + ColShippingMethod,
                shippingInfos.ShippingMethod ?? (object)DBNull.Value);

            shippingInfos.Id = (int)command.ExecuteScalar();

            return shippingInfos;
        }

        public List<Domain.ShippingInfos> GetAll()
        {
            var shippingInfos = new List<Domain.ShippingInfos>();
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
                shippingInfos.Add(_factory.CreateFromSqlReader(reader));
            }

            return shippingInfos;
        }
    }
}
