using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core
{
    public class Cart
    {
        public List<OrderDetail> OrderDetails { get; set; }

        public decimal SubTotal => 
            OrderDetails.Sum(od => od.ExtendedPrice);

        public Cart()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
