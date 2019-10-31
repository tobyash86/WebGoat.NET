using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core;
using WebGoatCore.ViewModels;

namespace WebGoatCore.Controllers
{
    public class OffersController : Controller
    {
        private readonly NorthwindContext _context;

        public OffersController(NorthwindContext context)
        {
            _context = context;
        }

        //[AcceptVerbs("get", "post")]
        //[HttpGet]
        //[HttpPost]
        public IActionResult Index(int? selectedCategoryId, string? nameFilter)
        {
            if(selectedCategoryId != null && _context.Categories.Find(selectedCategoryId) == null)
            {
                selectedCategoryId = null;
            }

            IEnumerable<Product> products = _context.Products;

            if (selectedCategoryId != null)
            {
                products = products.Where(p => p.CategoryId == selectedCategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                products = products.Where(p => p.ProductName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }

            return View(new OfferListViewModel()
            {
                Offers = products,
                ProductCategories = _context.Categories,
                SelectedCategoryId = selectedCategoryId,
                NameFilter = nameFilter
            });
        }

    }
}