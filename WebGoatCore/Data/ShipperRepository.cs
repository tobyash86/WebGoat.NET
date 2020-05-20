using WebGoatCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebGoatCore.Data
{
    public class ShipperRepository
    {
        private readonly NorthwindContext _context;

        public ShipperRepository(NorthwindContext context)
        {
            _context = context;
        }

        public Dictionary<int, string> GetShippingOptions(decimal orderSubtotal)
        {
            return _context.Shippers.ToDictionary(
                s => s.ShipperId, 
                s => GetShippingCostString(s, orderSubtotal));
        }

        private string GetShippingCostString(Shipper shipper, decimal orderSubtotal)
        {
            var shippingCost = Math.Round(shipper.GetShippingCost(orderSubtotal), 2).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            return $"{shipper.CompanyName} {shipper.ServiceName} - {shippingCost}";
        }

        public Shipper GetShipperByShipperId(int shipperId)
        {
            return _context.Shippers.Single(s => s.ShipperId == shipperId);
        }

        /// <summary>Gets a tracking number for the supplied shipper</summary>
        /// <param name="shipper">The shipper object</param>
        /// <returns>The tracking number</returns>
        /// <remarks>
        /// Simulates getting a tracking number.  In the real world, we'd contact their web service
        /// to get a real number.
        /// TODO: Use the check digit algorithms to make it realistic.
        /// Source for tracking number formats: http://answers.google.com/answers/threadview/id/207899.html
        /// </remarks>
        public string GetNextTrackingNumber(Shipper shipper)
        {
            var random = new Random();
            var companyName = shipper.CompanyName;
            if (companyName.Contains("UPS"))
            {
                return string.Format("1Z{0} {1} {2} {3} {4} {5}", random.Next(999).ToString("000"), random.Next(999).ToString("000"), random.Next(99).ToString("00"), random.Next(9999).ToString("0000"), random.Next(999).ToString("000"), random.Next(9).ToString("0"));
            }
            else if (companyName.Contains("FedEx"))
            {
                return string.Format("{0}{1}", random.Next(999999).ToString("000000"), random.Next(999999).ToString("000000"));
            }
            else if (companyName.Contains("US Postal Service"))
            {
                return string.Format("{0} {1} {2}", random.Next(9999).ToString("0000"), random.Next(9999).ToString("0000"), random.Next(99).ToString("00"));
            }
            else
            {
                return "Could not get a tracking number";
            }
        }
    }
}
