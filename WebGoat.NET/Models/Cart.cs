using System.Collections.Generic;
using System.Linq;

namespace WebGoatCore.Models
{
    public class Cart
    {
        //public List<OrderDetail> OrderDetails { get; set; }
        public IDictionary<int, OrderDetail> OrderDetails { get; set; }

        public decimal SubTotal =>
            OrderDetails.Values.Sum(od => od.ExtendedPrice);

        public Cart()
        {
            OrderDetails = new Dictionary<int, OrderDetail>();
        }
    }
}
