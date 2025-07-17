namespace Application.UseCases.Orders.Dtos
{
    public class OutputDtoCreateOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string StripePaymentIntentId { get; set; }
    }
}
