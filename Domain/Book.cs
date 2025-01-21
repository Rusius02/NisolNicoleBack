namespace Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ISBN { get; set; }
        public string CoverImagePath { get; set; }
        public string StripeProductId { get; set; }
    }
}
