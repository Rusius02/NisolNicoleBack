﻿using Domain;

namespace Application.UseCases.Orders.Dtos
{
    public class InputDtoCreateOrder
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string StripePaymentIntentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<InputDtoOrderBook> OrderBooks { get; set; } = new();
    }
}
