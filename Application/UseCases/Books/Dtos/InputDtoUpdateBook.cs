namespace Application.UseCases.Books.Dtos
{
    public class InputDtoUpdateBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ISBN { get; set; }
        public string CoverImagePath { get; set; }
    }
}
