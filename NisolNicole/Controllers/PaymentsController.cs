namespace NisolNicole.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using Stripe.Checkout;
    using System.Threading.Tasks;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest("Le montant doit être supérieur à zéro.");
            }

            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                    },
                };

                var service = new PaymentIntentService();
                PaymentIntent paymentIntent = await service.CreateAsync(options);

                // Enregistrez les informations de paiement dans la base de données si nécessaire

                return Ok(new { ClientSecret = paymentIntent.ClientSecret });
            }
            catch (StripeException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            string endpointSecret = _configuration["Stripe:WebhookSecret"]; // Clé secrète du webhook

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret
                );

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    // Gérez la logique post-paiement ici, par exemple :
                    // - Marquez une commande comme payée dans la base de données
                    // - Envoyez un email de confirmation

                    return Ok();
                }

                return BadRequest();
            }
            catch (StripeException e)
            {
                return BadRequest(new { Error = e.Message });
            }
        }
    }

    public class CreatePaymentIntentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
    }
}
