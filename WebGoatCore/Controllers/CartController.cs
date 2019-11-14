using WebGoatCore.Models;
using WebGoatCore.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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

        public IActionResult Index()
        {
            if (!HttpContext.Session.TryGet<Cart>("Cart", out var cart))
            {
                cart = new Cart();
            }

            return View(cart);
        }

        [HttpPost("{productId}")]
        public IActionResult AddOrder(int productId, short quantity)
        {
            if (!HttpContext.Session.TryGet<Cart>("Cart", out var cart))
            {
                cart = new Cart();
            }

            var product = _productRepository.GetProductById(productId);
            var orderDetail = new OrderDetail()
            {
                Discount = 0.0F,
                ProductId = productId,
                Quantity = quantity,
                Product = product,
                UnitPrice = product.UnitPrice
            };
            cart.OrderDetails.Add(orderDetail);

            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index");
        }

        [HttpGet("{productId}")]
        public IActionResult RemoveOrder(int productId)
        {
            try
            {
                if (HttpContext.Session.TryGet<Cart>("Cart", out var cart))
                {
                    var orderDetail = cart.OrderDetails.First(od => od.ProductId == productId);
                    if (orderDetail == null)
                    {
                        return View("RemoveOrderError", string.Format("Product {0} was not found in your cart.", productId));
                    }

                    cart.OrderDetails.Remove(orderDetail);
                    HttpContext.Session.Set("Cart", cart);

                    Response.Redirect("~/ViewCart.aspx");
                }
            }
            catch (Exception ex)
            {
                return View("RemoveOrderError", string.Format("An error has occurred: {0}", ex.Message));
            }

            return RedirectToAction("Index");
        }
    }
}