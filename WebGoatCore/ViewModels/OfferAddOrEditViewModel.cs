using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
