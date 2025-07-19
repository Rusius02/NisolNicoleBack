namespace Application.UseCases.Shipping.dtos
{
    // Dans un dossier comme Models/DTOs ou directement dans le dossier de ton contrôleur si c'est simple
    public class ShippingEstimateRequestDto
    {
        public string FromCountry { get; set; }
        public string FromPostal { get; set; }
        public string ToCountry { get; set; }
        public string ToPostal { get; set; }
        public double WeightInGrams { get; set; }
    }

}
