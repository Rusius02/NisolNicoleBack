using Application.UseCases.Shipping.dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.ShippingInfos;

namespace Application.UseCases.Shipping
{
    public class UsecaseCreateShippingInfos
    {
        private readonly ShippingInfosIRepository _shippingInfosIRepository;
        public UsecaseCreateShippingInfos(ShippingInfosIRepository shippingInfosIRepository)
        {
            _shippingInfosIRepository = shippingInfosIRepository;
        }

        public OutputDtoShippingInfos Execute(InputShippingInfosDto dto)
        {
            var shippingInfosFromDto = Mapper.GetInstance().Map<Domain.ShippingInfos>(dto);

            var shippingInfosFromDb = _shippingInfosIRepository.Create(shippingInfosFromDto);
            return Mapper.GetInstance().Map<OutputDtoShippingInfos>(shippingInfosFromDb);
        }
    }
}
