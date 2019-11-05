using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace WebGoatCore.ViewModels
{
    public class OfferListViewModel
    {
        public IEnumerable<OffersViewModel> Offers { get; set; }
        public IEnumerable<Category> ProductCategories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string? NameFilter { get; set; }

        public class OffersViewModel
        {
            public Product Offers { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
