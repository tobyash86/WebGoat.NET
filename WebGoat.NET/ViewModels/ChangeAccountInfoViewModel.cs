using System.ComponentModel.DataAnnotations;

namespace WebGoatCore.ViewModels
{
    public class ChangeAccountInfoViewModel
    {
        [Display(Name = "Company Name")]
        public string? CompanyName { get; set; }

        [Display(Name = "Contact Title")]
        public string? ContactTitle { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Region/State")]
        public string? Region { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }


        public bool UpdatedSucessfully { get; set; }
    }
}
