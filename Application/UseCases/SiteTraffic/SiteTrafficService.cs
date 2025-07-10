using Application.UseCases.Books.Dtos;
using Application.UseCases.SiteTraffic.dtos;
using Application.Utils;
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

        public SiteVisitDto RegisterVisit(string? ipAddress)
        {
            var visit = new SiteVisit
            {
                IpAddress = ipAddress,
                VisitedAt = DateTime.UtcNow
            };

            var created = _repository.Create(visit);

            return Mapper.GetInstance().Map<SiteVisitDto>(created);
        }

        public List<SiteVisitDto> GetAllVisits()
        {
            List<SiteVisit> visits = _repository.GetAll();
            return Mapper.GetInstance().Map<List<SiteVisitDto>>(visits);
        }

        public int GetTotalVisits()
        {
            return _repository.GetAll().Count;
        }
    }
}
