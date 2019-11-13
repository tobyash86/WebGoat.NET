using WebGoatCore.Models;
using WebGoatCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebGoatCore.ViewModels;

namespace WebGoatCore.Controllers
{
    [Route("[controller]/[action]")]
    public class OffersController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly SupplierRepository _supplierRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OffersController(ProductRepository productRepository, IWebHostEnvironment webHostEnvironment, CategoryRepository categoryRepository, SupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string? nameFilter, int? selectedCategoryId)
        {
            if (selectedCategoryId != null && _categoryRepository.GetById(selectedCategoryId.Value) == null)
            {
                selectedCategoryId = null;
            }

            var offer = _productRepository.FindNonDiscontinuedProducts(nameFilter, selectedCategoryId)
                .Select(p => new OfferListViewModel.OfferItem()
                {
                    Offer = p,
                    ImageUrl = GetImageUrlForOffer(p),
                });

            return View(new OfferListViewModel()
            {
                Offers = offer,
                ProductCategories = _categoryRepository.GetAllCategories(),
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

        [Authorize]
        public IActionResult Manage()
        {
            return View(_productRepository.GetAllProducts());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View("AddOrEdit", new OfferAddOrEditViewModel()
            {
                AddsNew = true,
                ProductCategories = _categoryRepository.GetAllCategories(),
                Suppliers = _supplierRepository.GetAllSuppliers(),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Product offer)
        {
            try
            {
                _productRepository.Add(offer);
                return RedirectToAction("Edit", new { id = offer.ProductId });
            }
            catch
            {
                return View("AddOrEdit", new OfferAddOrEditViewModel()
                {
                    AddsNew = true,
                    ProductCategories = _categoryRepository.GetAllCategories(),
                    Suppliers = _supplierRepository.GetAllSuppliers(),
                    Offer = offer,
                });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            return View("AddOrEdit", new OfferAddOrEditViewModel()
            {
                AddsNew = false,
                ProductCategories = _categoryRepository.GetAllCategories(),
                Offer = _productRepository.GetProductById(id),
            });
        }

        [Authorize]
        [HttpPost("{id?}")]
        public IActionResult Edit(Product offer)
        {
            offer = _productRepository.Update(offer);
            return View("AddOrEdit", new OfferAddOrEditViewModel()
            {
                AddsNew = false,
                ProductCategories = _categoryRepository.GetAllCategories(),
                Offer = offer,
            });
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