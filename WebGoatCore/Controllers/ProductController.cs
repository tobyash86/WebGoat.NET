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
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly SupplierRepository _supplierRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ProductRepository productRepository, IWebHostEnvironment webHostEnvironment, CategoryRepository categoryRepository, SupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Search(string? nameFilter, int? selectedCategoryId)
        {
            if (selectedCategoryId != null && _categoryRepository.GetById(selectedCategoryId.Value) == null)
            {
                selectedCategoryId = null;
            }

            var product = _productRepository.FindNonDiscontinuedProducts(nameFilter, selectedCategoryId)
                .Select(p => new ProductListViewModel.ProductItem()
                {
                    Product = p,
                    ImageUrl = GetImageUrlForProduct(p),
                });

            return View(new ProductListViewModel()
            {
                Products = product,
                ProductCategories = _categoryRepository.GetAllCategories(),
                SelectedCategoryId = selectedCategoryId,
                NameFilter = nameFilter
            });
        }

        [HttpGet("{productId}")]
        public IActionResult Details(int productId, short quantity = 1)
        {
            var model = new ProductDetailsViewModel();
            try
            {
                var product = _productRepository.GetProductById(productId);
                model.Product = product;
                model.CanAddToCart = true;
                model.ProductImageUrl = GetImageUrlForProduct(product);
                model.Quantity = quantity;
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

        [Authorize(Roles = "Admin")]
        public IActionResult Manage()
        {
            return View(_productRepository.GetAllProducts());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View("AddOrEdit", new ProductAddOrEditViewModel()
            {
                AddsNew = true,
                ProductCategories = _categoryRepository.GetAllCategories(),
                Suppliers = _supplierRepository.GetAllSuppliers(),
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(Product product)
        {
            try
            {
                _productRepository.Add(product);
                return RedirectToAction("Edit", new { id = product.ProductId });
            }
            catch
            {
                return View("AddOrEdit", new ProductAddOrEditViewModel()
                {
                    AddsNew = true,
                    ProductCategories = _categoryRepository.GetAllCategories(),
                    Suppliers = _supplierRepository.GetAllSuppliers(),
                    Product = product,
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            return View("AddOrEdit", new ProductAddOrEditViewModel()
            {
                AddsNew = false,
                ProductCategories = _categoryRepository.GetAllCategories(),
                Product = _productRepository.GetProductById(id),
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id?}")]
        public IActionResult Edit(Product product)
        {
            product = _productRepository.Update(product);
            return View("AddOrEdit", new ProductAddOrEditViewModel()
            {
                AddsNew = false,
                ProductCategories = _categoryRepository.GetAllCategories(),
                Product = product,
            });
        }

        private string GetImageUrlForProduct(Product product)
        {
            var imageUrl = $"/Images/ProductImages/{product.ProductId}.jpg";
            if (!_webHostEnvironment.WebRootFileProvider.GetFileInfo(imageUrl).Exists)
            {
                imageUrl = "/Images/ProductImages/NoImage.jpg";
            }
            return imageUrl;
        }
    }
}