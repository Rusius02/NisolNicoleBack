namespace NisolNicole.Controllers
{
    using Application.UseCases.Orders.Dtos;
    using Application.UseCases.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using System.Threading.Tasks;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsecaseCreateOrder _usecaseCreateOrder;

        public PaymentsController(IConfiguration configuration, UsecaseCreateOrder usecaseCreateOrder)
        {
            _configuration = configuration;
            _usecaseCreateOrder = usecaseCreateOrder;
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
                // 1. Calculate total amount and create an order in the database
                var order = _usecaseCreateOrder.Execute(new InputDtoCreateOrder
                {
                    UserId = request.UserId,
                    Amount = request.Amount / 100M, // Convert cents to dollars/euros
                    OrderBooks = request.Books.Select(b => new InputDtoOrderBook
                    {
                        StripePriceId = b.StripePriceId,
                        Quantity = b.Quantity
                    }).ToList(),
                    PaymentStatus = "Pending"
                });

                // 2. Create a PaymentIntent in Stripe
                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                    },
                    Metadata = new Dictionary<string, string>
            {
                { "OrderId", order.OrderId.ToString() },
                { "UserId", order.UserId.ToString() }
            }
                };

                var service = new PaymentIntentService();
                PaymentIntent paymentIntent = await service.CreateAsync(options);

                // 3. Update the order with the PaymentIntent ID
                _usecaseCreateOrder.Execute(order.OrderId, paymentIntent.Id, paymentIntent.Status);

                // 4. Return the ClientSecret to the frontend
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
            string endpointSecret = _configuration["Stripe:WebhookSecret"]
                ?? throw new InvalidOperationException("Stripe Webhook Secret is missing.");


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
        public int UserId { get; set; } // ID de l'utilisateur
        public List<InputDtoOrderBook> Books { get; set; } = new(); // Liste des livres achetés
        public string Currency { get; set; } = string.Empty;// Devise (par exemple, "usd")
        public long Amount { get; set; } // Montant total en centimes (Stripe utilise des centimes)
    }

}
