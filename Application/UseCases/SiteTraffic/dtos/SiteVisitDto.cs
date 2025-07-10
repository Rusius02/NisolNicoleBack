namespace Application.UseCases.SiteTraffic.dtos
{
    public class SiteVisitDto
    {
        public string? IpAddress { get; set; }
        public DateTime VisitedAt { get; set; }
    }
}
