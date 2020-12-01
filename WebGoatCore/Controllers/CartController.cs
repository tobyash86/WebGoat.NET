using WebGoatCore.Models;
using WebGoatCore.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebGoatCore.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private readonly ProductRepository _productRepository;

        public CartController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private Cart GetCart()
        {
            HttpContext.Session.TryGet<Cart>("Cart", out var cart);
            if(cart == null)
            {
                cart = new Cart();
            }
            return cart;
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }

        [HttpPost("{productId}")]
        public IActionResult AddOrder(int productId, short quantity)
        {
            if(quantity <= 0)
            {
                return RedirectToAction("Details", "Product", new { productId = productId, quantity = quantity });
            }

            var product = _productRepository.GetProductById(productId);
            
            var cart = GetCart();
            if(!cart.OrderDetails.ContainsKey(productId))
            {
                var orderDetail = new OrderDetail()
                {
                    Discount = 0.0F,
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product,
                    UnitPrice = product.UnitPrice
                };
                cart.OrderDetails.Add(orderDetail.ProductId, orderDetail);
            }
            else
            {
                var originalOrder = cart.OrderDetails[productId];
                originalOrder.Quantity += quantity;
            }

            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index");
        }

        [HttpGet("{productId}")]
        public IActionResult RemoveOrder(int productId)
        {
            try
            {
                var cart = GetCart();
                if (!cart.OrderDetails.ContainsKey(productId))
                {
                    return View("RemoveOrderError", string.Format("Product {0} was not found in your cart.", productId));
                }

                cart.OrderDetails.Remove(productId);
                HttpContext.Session.Set("Cart", cart);

                Response.Redirect("~/ViewCart.aspx");
            }
            catch (Exception ex)
            {
                return View("RemoveOrderError", string.Format("An error has occurred: {0}", ex.Message));
            }

            return RedirectToAction("Index");
        }
    }
}