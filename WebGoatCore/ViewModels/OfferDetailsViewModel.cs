using WebGoatCore.Models;

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