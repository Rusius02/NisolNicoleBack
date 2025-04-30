using Domain;

namespace Infrastructure.SqlServer.Repository.SiteTraffic
{
    public interface ISiteTrafficRepository
    {
        SiteVisit Create(SiteVisit siteVisit);

        List<SiteVisit> GetAll();

    }
}
