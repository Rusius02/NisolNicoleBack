namespace Application.UseCases.Books.Dtos
{
    public class OutputDtoCreateBook
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string CoverImagePath { get; set; } = string.Empty;
        public string StripeProductId { get; set; } = string.Empty;
    }
}
