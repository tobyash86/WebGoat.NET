using System;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
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
        public virtual Shipper Shipper { get; set; }
    }
}
