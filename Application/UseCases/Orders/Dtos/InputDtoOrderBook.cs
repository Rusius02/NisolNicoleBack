using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Orders.Dtos
{
    public class InputDtoOrderBook
    {
        public string StripePriceId { get; set; } 
        public int Quantity { get; set; } 
    }
}
