namespace NisolNicole.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
                // Add additional options if needed
            };

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent = await service.CreateAsync(options);

            return Ok(new { ClientSecret = paymentIntent.ClientSecret });
        }
    }

    public class CreatePaymentIntentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
    }

}
