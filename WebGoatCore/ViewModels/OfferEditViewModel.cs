using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGoatCore.ViewModels
{
    public class OfferEditViewModel
    {
        public Product? Offer { get; set; }
        public IList<Category> ProductCategories { get; set; }
    }
}
