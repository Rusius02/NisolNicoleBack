namespace Domain
{
    public class SiteVisit
    {
        public int Id { get; set; }
        public DateTime VisitedAt { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
    }
}
