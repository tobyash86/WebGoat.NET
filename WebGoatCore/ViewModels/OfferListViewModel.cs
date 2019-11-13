using WebGoatCore.Models;
using System.Collections.Generic;

namespace WebGoatCore.ViewModels
{
    public class OfferListViewModel
    {
        public IEnumerable<OfferItem> Offers { get; set; }
        public IEnumerable<Category> ProductCategories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string? NameFilter { get; set; }

        public class OfferItem
        {
            public Product Offer { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
