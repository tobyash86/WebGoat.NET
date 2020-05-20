using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<OrderPayment> OrderPayments { get; set; }
        public virtual Shipment? Shipment { get; set; }

        public decimal SubTotal => OrderDetails.Sum(od => od.ExtendedPrice);

        public decimal Total => Math.Round(SubTotal + Freight, 2);

        public static string GetPackageTrackingUrl(string Carrier, string TrackingNumber)
        {
            string trackingUrl;
            Carrier = Carrier.ToLower();
            switch (Carrier)
            {
                case "fedex":
                case "federalexpress":
                case "federal express":
                    trackingUrl = string.Format("http://www.fedex.com/Tracking?tracknumbers={0}&action=track", TrackingNumber);
                    break;
                case "ups":
                case "unitedpostalservice":
                case "united postal service":
                    //trackingUrl = string.Format("http://wwwapps.ups.com/WebTracking/processInputRequest?InquiryNumber1={0}&tracknums_displayed=1&TypeOfInquiryNumber=T", TrackingNumber);
                    trackingUrl = string.Format("http://wwwapps.ups.com/WebTracking/track?loc=en_US&track.x=Track&trackNums={0}", TrackingNumber);
                    break;
                case "usps":
                case "unitedstatespostalservice":
                case "united states postal service":
                case "postoffice":
                case "post office":
                    trackingUrl = string.Format("http://trkcnfrm1.smi.usps.com/PTSInternetWeb/InterLabelInquiry.do?origTrackNum={0}", TrackingNumber);
                    break;
                default:
                    trackingUrl = string.Format("http://{0}?TrackingNumber={1}", Carrier, TrackingNumber);
                    break;
            }
            return trackingUrl;
        }
    }
}
