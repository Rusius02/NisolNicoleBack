using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.ShippingInfos
{
    public class ShippingInfosFactory : IDomainFactory<Domain.ShippingInfos>
    {
        public Domain.ShippingInfos CreateFromSqlReader(SqlDataReader reader)
        {
            return new Domain.ShippingInfos()
            {
                Id = reader.GetInt32(reader.GetOrdinal(ShippingInfosRepository.ColId)),
                FullName = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColFullName)),
                AddressLine1 = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColAddressStreet)),
                AddressLine2 = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColAddressNumber)),
                City = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColAddressCity)),
                PostalCode = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColAddressZip)),
                Country = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColAddressCountry)),
                Email = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColMail)),
                PhoneNumber = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColMail)),
                OrderId = reader.GetInt32(reader.GetOrdinal(ShippingInfosRepository.ColOrderId)),
                ShippingMethod = reader.GetString(reader.GetOrdinal(ShippingInfosRepository.ColShippingMethod))

            };
        }
    }
}
