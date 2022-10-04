using System.ComponentModel.DataAnnotations;
using WebGoatCore.Models;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public string ProductImageUrl { get; set; }
        public string? ErrorMessage { get; set; }
        public bool CanAddToCart { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [Required]
        public short Quantity { get; set; }
    }
}