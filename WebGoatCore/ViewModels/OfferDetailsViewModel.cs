using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGoatCore.ViewModels
{
    public class OfferDetailsViewModel
    {
        public Product Offer { get; set; }
        public string ProductImageUrl { get; set; }
        public string? ErrorMessage { get; set; }
        public bool CanAddToCart { get; set; }
    }
}
