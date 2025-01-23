using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Orders.Dtos
{
    public class InputDtoOrderBook
    {
        public int BookId { get; set; } // ID du livre
        public int Quantity { get; set; } // Quantité commandée
    }
}
