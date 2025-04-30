namespace Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string StripePaymentIntentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderBook> OrderBooks { get; set; } = new();

    }
}
