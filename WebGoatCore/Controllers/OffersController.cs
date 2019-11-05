using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core;
using WebGoatCore.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace WebGoatCore.Controllers
{
    [Route("[controller]/[action]")]
    public class OffersController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly ProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OffersController(NorthwindContext context, ProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string? nameFilter, int? selectedCategoryId)
        {
            if(selectedCategoryId != null && _context.Categories.Find(selectedCategoryId) == null)
            {
                selectedCategoryId = null;
            }

            var offer = _productRepository.FindNonDiscontinuedProducts(nameFilter, selectedCategoryId)
                .Select(p => new OfferListViewModel.OffersViewModel() {
                    Offers = p,
                    ImageUrl = GetImageUrlForOffer(p),
                });

            return View(new OfferListViewModel()
            {
                Offers = offer,
                ProductCategories = _context.Categories,
                SelectedCategoryId = selectedCategoryId,
                NameFilter = nameFilter
            });
        }

        [HttpGet("{productId}")]
        public IActionResult Details(int productId)
        {
            var model = new OfferDetailsViewModel();
            try
            {
                var offer = _productRepository.GetProductById(productId);
                model.Offer = offer;
                model.CanAddToCart = true;
                model.ProductImageUrl = GetImageUrlForOffer(offer);
            }
            catch (InvalidOperationException)
            {
                model.ErrorMessage = "Product not found.";
            }
            catch (Exception ex)
            {
                model.ErrorMessage = string.Format("An error has occurred: {0}", ex.Message);
            }

            return View(model);
        }

        private string GetImageUrlForOffer(Product offer)
        {
            var imageUrl = $"/images/productImages/{offer.ProductId}.jpg";
            if (!_webHostEnvironment.WebRootFileProvider.GetFileInfo(imageUrl).Exists)
            {
                imageUrl = "/images/productImages/NoImage.jpg";
            }
            return imageUrl;
        }
    }
}