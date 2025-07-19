using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Shipping.dtos;
using Application.UseCases.Shipping;
using EasyPost.Models.API;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Shipping")]
    public class ShippingInfosController : ControllerBase
    {
        private readonly UsecaseCreateShippingInfos _usecaseCreateShippingsInfos;
        private readonly UsecaseGetEstimateShippingPrice _shippingEstimator;

        public ShippingInfosController(UsecaseCreateShippingInfos usecaseCreateShippingsInfos, UsecaseGetEstimateShippingPrice shippingEstimator)
        {
            _usecaseCreateShippingsInfos = usecaseCreateShippingsInfos;
            _shippingEstimator = shippingEstimator;
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoShippingInfos> Create([FromBody] InputShippingInfosDto dto)
        {
            return StatusCode(201, _usecaseCreateShippingsInfos.Execute(dto));
        }
        [HttpPost("estimate")]
        public async Task<IActionResult> EstimateShipping([FromBody] ShippingEstimateRequestDto dto)
        {
            // Validation basique des entrées du DTO
            if (dto == null)
            {
                return BadRequest("Request body is null.");
            }

            if (string.IsNullOrWhiteSpace(dto.FromCountry) || string.IsNullOrWhiteSpace(dto.FromPostal) ||
                string.IsNullOrWhiteSpace(dto.ToCountry) || string.IsNullOrWhiteSpace(dto.ToPostal) ||
                dto.WeightInGrams <= 0)
            {
                return BadRequest("All shipping parameters (fromCountry, fromPostal, toCountry, toPostal, weightInGrams) are required and must be valid.");
            }

            try
            {
                // Appel à ton service d'estimation des frais de port
                List<Rate> rates = await _shippingEstimator.EstimateShippingAsync(
                    dto.FromCountry,
                    dto.FromPostal,
                    dto.ToCountry,
                    dto.ToPostal,
                    dto.WeightInGrams
                );

                if (rates.Any())
                {
                    // Retourne les tarifs trouvés. Tu peux mapper cela vers un DTO de réponse si besoin.
                    return Ok(rates);
                }
                else
                {
                    // Aucun tarif n'a été trouvé
                    return NotFound("No shipping rates found for the given parameters.");
                }
            }
            catch (ArgumentException ex) // Catch les exceptions de validation de ton use case
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log l'erreur (utiliser un logger réel en production)
                Console.Error.WriteLine($"Error estimating shipping: {ex.Message}");
                return StatusCode(500, $"An error occurred while estimating shipping: {ex.Message}");
            }
        }
    }
}
