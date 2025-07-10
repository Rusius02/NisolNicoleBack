namespace Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public double Price { get; set; }
        public string ISBN { get; set; } = String.Empty;
        public string CoverImagePath { get; set; } = String.Empty;
        public string StripeProductId { get; set; } = String.Empty;
        public int QuantityInStock { get; set; }
    }
}
