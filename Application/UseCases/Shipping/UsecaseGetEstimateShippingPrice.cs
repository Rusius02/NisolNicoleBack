using EasyPost; 
using Microsoft.Extensions.Configuration;
using EasyPost.Models.API;


namespace Application.UseCases.Shipping
{
    public class UsecaseGetEstimateShippingPrice
    {
        private readonly Client _easyPostClient;
        private readonly EasyPost.Services.ShipmentService _shipmentService; // Spécifie le namespace complet

        public UsecaseGetEstimateShippingPrice(IConfiguration configuration)
        {
            var apiKey = configuration["EasyPost:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), "EasyPost API Key is not configured.");
            }

            // Correction de l'erreur CS1503 : Utiliser ClientConfiguration
            var clientConfiguration = new EasyPost.ClientConfiguration(apiKey);
            _easyPostClient = new EasyPost.Client(clientConfiguration);

            // Accéder au service Shipment via le client
            _shipmentService = _easyPostClient.Shipment;
        }

        public async Task<List<Rate>> EstimateShippingAsync(string fromCountry, string fromPostal, string toCountry, string toPostal, double weightInGrams)
        {
            if (string.IsNullOrWhiteSpace(fromCountry) || string.IsNullOrWhiteSpace(fromPostal) ||
                string.IsNullOrWhiteSpace(toCountry) || string.IsNullOrWhiteSpace(toPostal) ||
                weightInGrams <= 0)
            {
                throw new ArgumentException("All shipping parameters (countries, postal codes, and weight) must be valid.");
            }

            var toAddress = new Address
            {
                Country = toCountry,
                Zip = toPostal
            };

            var fromAddress = new Address
            {
                Country = fromCountry,
                Zip = fromPostal
            };

            var parcel = new Parcel
            {
                Weight = weightInGrams
            };

            // Correction de l'erreur CS0246 : Utiliser EasyPost.Parameters.Shipment.Create
            var shipmentParams = new EasyPost.Parameters.Shipment.Create
            {
                ToAddress = toAddress,
                FromAddress = fromAddress,
                Parcel = parcel
            };

            try
            {
                var shipment = await _shipmentService.Create(shipmentParams);

                if (shipment == null || shipment.Rates == null || !shipment.Rates.Any())
                {
                    return new List<Rate>();
                }

                return shipment.Rates;
            }
            // Correction de l'erreur CS0234 : EasyPostException est directement dans EasyPost
            catch (EasyPost.Exceptions.EasyPostError ex)
            {
                Console.WriteLine($"EasyPost Error: {ex.Message}");
                throw new Exception($"Failed to estimate shipping with EasyPost: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw new Exception($"An unexpected error occurred during shipping estimation: {ex.Message}", ex);
            }
        }
    }
}