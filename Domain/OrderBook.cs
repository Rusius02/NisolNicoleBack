namespace Domain
{
    public class OrderBook
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }

        // navigation
        public Book? Book { get; set; }
    }
}
