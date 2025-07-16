namespace Infrastructure.SqlServer.Repository.ShippingInfos
{
    public interface ShippingInfosIRepository
    {
        Domain.ShippingInfos Create(Domain.ShippingInfos shippingInfos);

        List<Domain.ShippingInfos> GetAll();
    }
}
