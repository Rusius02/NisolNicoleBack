using Domain;
using Infrastructure.SqlServer.Repository.SiteTraffic;

namespace Application.UseCases.SiteTraffic
{
    public class SiteTrafficService
    {
        private readonly ISiteTrafficRepository _repository;

        public SiteTrafficService(ISiteTrafficRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterVisitAsync(string? ipAddress)
        {
            var visit = new SiteVisit { IpAddress = ipAddress };
            await _repository.AddVisitAsync(visit);
        }

        public async Task<int> GetTotalVisitsAsync()
        {
            return await _repository.GetVisitCountAsync();
        }
    }
}
