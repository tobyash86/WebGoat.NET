using WebGoatCore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebGoatCore.ViewModels
{
    public class CheckoutViewModel
    {
        [Display(Name = "Name to ship to")]
        [Required(ErrorMessage = "adsasdasd")]
        public string ShipTarget { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "adsasdasd")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "adsasdasd")]
        public string City { get; set; }

        [Display(Name = "Region/State")]
        [Required(ErrorMessage = "adsasdasd")]
        public string Region { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "adsasdasd")]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "adsasdasd")]
        public string Country { get; set; }

        [Display(Name = "Shipping method")]
        [Required(ErrorMessage = "adsasdasd")]
        public int ShippingMethod { get; set; }

        [Display(Name = "Credit card number:")]
        [DataType(DataType.CreditCard, ErrorMessage = "This doesn't look like credit card number")]
        [Required(ErrorMessage = "adsasdasd")]
        public string CreditCard { get; set; }

        [Display(Name = "Expiration")]
        [Required(ErrorMessage = "adsasdasd")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessage = "adsasdasd")]
        public int ExpirationYear { get; set; }

        [Display(Name = "Remember this credit card number for next time I check out.")]
        [Required(ErrorMessage = "adsasdasd")]
        public bool RememberCreditCard { get; set; }

        public IDictionary<int, string> ShippingOptions { get; set; }

        public IList<int> AvailableExpirationYears { get; set; }

        public Cart? Cart { get; set; }
    }
}
