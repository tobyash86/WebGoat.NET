using System;

namespace WebGoatCore.Models
{
    public class OrderPayment
    {
        public int OrderPaymentId { get; set; }
        public int OrderId { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ApprovalCode { get; set; }

        public virtual Order Order { get; set; }
    }
}
