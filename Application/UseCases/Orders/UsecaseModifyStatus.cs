using Infrastructure.SqlServer.Repository.Orders;

namespace Application.UseCases.Orders
{
    public class UsecaseModifyStatus
    {
        private readonly IOrderRepository _orderRepository;
        public UsecaseModifyStatus(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task ExecuteAsync(int orderId, string stripePaymentStatus)
        {
            var order = _orderRepository.GetOrderById(orderId);

            if (order == null)
                throw new Exception($"Order with ID {orderId} not found.");

            order.PaymentStatus = MapStripeStatus(stripePaymentStatus);
            _orderRepository.Update(order);
        }
        private string MapStripeStatus(string stripeStatus)
        {
            // Tu peux éventuellement mapper vers un enum ou status interne
            return stripeStatus switch
            {
                "succeeded" => "Paid",
                "processing" => "Processing",
                "requires_payment_method" => "Failed",
                "requires_action" => "ActionRequired",
                "requires_confirmation" => "RequiresConfirmation",
                "canceled" => "Canceled",
                _ => "Unknown"
            };
        }
    }
}
