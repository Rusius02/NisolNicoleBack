﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Orders.Dtos
{
    public class OutputDtoCreateOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string StripePaymentIntentId { get; set; }
    }
}
