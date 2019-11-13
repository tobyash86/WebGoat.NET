using System;

namespace WebGoatCore.Models
{
    public class Shipper
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string ServiceName { get; set; }
        public double ShippingCostMultiplier { get; set; }
        public string Phone { get; set; }

        public decimal GetShippingCost(decimal subTotal)
        {
            return subTotal * Convert.ToDecimal(ShippingCostMultiplier);
        }
    }
}
