using WebGoatCore.Models;
using System.Collections.Generic;

namespace WebGoatCore.ViewModels
{
    public class OfferAddOrEditViewModel
    {
        public bool AddsNew { get; set; }
        public Product? Offer { get; set; }
        public IList<Category> ProductCategories { get; set; }
        public IList<Supplier>? Suppliers { get; set; }
    }
}
