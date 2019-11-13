using WebGoatCore.Models;

namespace WebGoatCore.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public string ProductImageUrl { get; set; }
        public string? ErrorMessage { get; set; }
        public bool CanAddToCart { get; set; }
    }
}