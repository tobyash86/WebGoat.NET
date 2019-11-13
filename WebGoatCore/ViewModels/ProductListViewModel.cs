using WebGoatCore.Models;
using System.Collections.Generic;

namespace WebGoatCore.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductItem> Products { get; set; }
        public IEnumerable<Category> ProductCategories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string? NameFilter { get; set; }

        public class ProductItem
        {
            public Product Product { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
