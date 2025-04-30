using Application.UseCases.Books.Dtos;
using Application.UseCases.Orders.Dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Repository.Orders;

namespace Application.UseCases.Orders
{
    public class UsecaseCreateOrder
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        public UsecaseCreateOrder(IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
        }

        /*Method that will create an User using an InputDTO given as an argument
         and that will return an Activity OutputDto*/
        public OutputDtoCreateOrder Execute(InputDtoCreateOrder dto)
        {
            var orderBooks = new List<InputDtoOrderBookResolved>();

            foreach (var item in dto.OrderBooks)
            {
                var book = _bookRepository.GetBookByStripeId(item.StripePriceId);
                if (book == null)
                {
                    throw new Exception($"Aucun livre trouvé pour le Stripe ID {item.StripePriceId}");
                }

                orderBooks.Add(new InputDtoOrderBookResolved
                {
                    BookId = book.Id,
                    Quantity = item.Quantity
                });
            }

            var orderDtoResolved = new InputDtoCreateOrderResolved
            {
                UserId = dto.UserId,
                Amount = dto.Amount,
                OrderBooks = orderBooks,
                PaymentStatus = dto.PaymentStatus
            };

            var orderFromDto = Mapper.GetInstance().Map<Domain.Order>(orderDtoResolved);

            var orderFromDb = _orderRepository.Create(orderFromDto);
            return Mapper.GetInstance().Map<OutputDtoCreateOrder>(orderFromDb);
        }
        public void Execute(int orderId, string stripePaymentIntentId, string paymentStatus)
        {
            var order = _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }

            order.StripePaymentIntentId = stripePaymentIntentId;
            order.PaymentStatus = paymentStatus;
            _orderRepository.Update(order);
        }

    }
    public class InputDtoOrderBookResolved
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }

    public class InputDtoCreateOrderResolved
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public List<InputDtoOrderBookResolved> OrderBooks { get; set; }
        public string PaymentStatus { get; set; }
    }
}
