namespace Application.UseCases.Books.Dtos
{
    public class InputDtoCreateBook
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string? CoverImagePath { get; set; }
        public string? StripeProductId { get; set; }
    }
}
