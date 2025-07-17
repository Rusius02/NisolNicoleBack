namespace NisolNicole.Controllers
{
    using Application.UseCases.Orders.Dtos;
    using Application.UseCases.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using System.Threading.Tasks;
    using Application.UseCases.Shipping.dtos;
    using Application.UseCases.Shipping;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsecaseCreateOrder _usecaseCreateOrder;
        private readonly UsecaseModifyStatus _usecaseModifyStatus;
        private readonly UsecaseCreateShippingInfos _usecaseCreateShippingsInfos;

        public PaymentsController(IConfiguration configuration, UsecaseCreateOrder usecaseCreateOrder,
            UsecaseModifyStatus usecaseModifyStatus, UsecaseCreateShippingInfos usecaseCreateShippingsInfos)
        {
            _configuration = configuration;
            _usecaseCreateOrder = usecaseCreateOrder;
            _usecaseModifyStatus = usecaseModifyStatus;
            _usecaseCreateShippingsInfos = usecaseCreateShippingsInfos;
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

                //2. Create the shipping infos
                _usecaseCreateShippingsInfos.Execute(mapToShippingDto(request, order));
                // 3. Create a PaymentIntent in Stripe
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

                // 4. Update the order with the PaymentIntent ID
                _usecaseCreateOrder.Execute(order.OrderId, paymentIntent.Id, paymentIntent.Status);

                // 5. Return the ClientSecret to the frontend
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

                if (stripeEvent.Type == Events.PaymentIntentSucceeded ||
                     stripeEvent.Type == Events.PaymentIntentProcessing ||
                     stripeEvent.Type == Events.PaymentIntentCanceled)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent == null || paymentIntent.Metadata == null)
                        return BadRequest("Invalid PaymentIntent or missing metadata.");

                    if (!paymentIntent.Metadata.TryGetValue("orderId", out var orderIdString) || !int.TryParse(orderIdString, out var orderId))
                        return BadRequest("Invalid or missing orderId in metadata.");

                    await _usecaseModifyStatus.ExecuteAsync(orderId, paymentIntent.Status);
                    return Ok();
                }

                return Ok(); // OK même si on ignore l'événement
            }
            catch (StripeException e)
            {
                return BadRequest(new { Error = e.Message });
            }
        }
        private InputShippingInfosDto mapToShippingDto(CreatePaymentIntentRequest request, OutputDtoCreateOrder order)
        {
            if (request == null)
            {
                throw new Exception("Missing order and shipping infos");
            } else
            {
                InputShippingInfosDto dto = new InputShippingInfosDto();
                dto.OrderId = order.OrderId;
                dto.FullName = request.FullName;
                dto.PhoneNumber = request.PhoneNumber;
                dto.Email = request.Email;
                dto.AddressLine1 = request.AddressLine1;
                dto.AddressLine2 = request.AddressLine2;
                dto.PostalCode = request.PostalCode;
                dto.City = request.City;
                dto.Country = request.Country;
                return dto;
            }
        }
    }
    public class CreatePaymentIntentRequest
    {
        public int UserId { get; set; } // ID de l'utilisateur
        public List<InputDtoOrderBook> Books { get; set; } = new(); // Liste des livres achetés
        public string Currency { get; set; } = string.Empty;// Devise (par exemple, "usd")
        public long Amount { get; set; } // Montant total en centimes (Stripe utilise des centimes)
        public string FullName { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
