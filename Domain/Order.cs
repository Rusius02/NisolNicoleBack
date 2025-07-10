namespace Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; } = new Users();
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = String.Empty;
        public string StripePaymentIntentId { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderBook> OrderBooks { get; set; } = new();

    }
}
