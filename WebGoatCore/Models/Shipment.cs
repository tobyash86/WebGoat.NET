using System;

namespace WebGoatCore.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public int OrderId { get; set; }
        public int ShipperId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string TrackingNumber { get; set; }

        public virtual Order Order { get; set; }
    }
}
