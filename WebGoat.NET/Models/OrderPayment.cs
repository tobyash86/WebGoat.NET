using System;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.Models
{
    public class OrderPayment
    {
        public int OrderPaymentId { get; set; }
        public int OrderId { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? CVV { get; set; }
        public double AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ApprovalCode { get; set; }

        public virtual Order Order { get; set; }
    }
}
