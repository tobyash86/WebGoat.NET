using WebGoatCore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.ViewModels
{
    public class CheckoutViewModel
    {
        [Display(Name = "Name to ship to")]
        [Required(ErrorMessage = "Please enter the name")]
        public string ShipTarget { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter city")]
        public string City { get; set; }

        [Display(Name = "Region/State")]
        [Required(ErrorMessage = "Please enter region/state")]
        public string Region { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Please enter postal code")]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Please enter country")]
        public string Country { get; set; }

        [Display(Name = "Shipping method")]
        [Required(ErrorMessage = "Please enter country")]
        public int ShippingMethod { get; set; }

        [Display(Name = "Credit card number:")]
        [DataType(DataType.CreditCard, ErrorMessage = "This doesn't look like credit card number")]
        [Required(ErrorMessage = "Please provide valid card number.")]
        public string CreditCard { get; set; }

        [Display(Name = "Expiration")]
        [Required(ErrorMessage = "Please enter expiration month")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessage = "Please enter expiration year")]
        public int ExpirationYear { get; set; }

        [Display(Name = "Remember this credit card number for next time I check out.")]
        [Required(ErrorMessage = "Please mark whether credit card number should be remembered")]
        public bool RememberCreditCard { get; set; }

        private static IDictionary<int, string> _shippingOptions;
        public IDictionary<int, string> ShippingOptions {
            get
            {
                return _shippingOptions;
            }

            set
            {
                _shippingOptions = value;
            }
        }

        private static IList<int> _expYears;
        public IList<int> AvailableExpirationYears
        {
            get
            {
                return _expYears;
            }

            set
            {
                _expYears = value;
            }
        }

        public Cart? Cart { get; set; }
    }
}
