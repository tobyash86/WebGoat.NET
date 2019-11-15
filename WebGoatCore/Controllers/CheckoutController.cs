using WebGoatCore.Models;
using WebGoatCore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using WebGoatCore.ViewModels;
using System.Linq;

namespace WebGoatCore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CustomerRepository _customerRepository;
        private readonly ShipperRepository _shipperRepository;
        private readonly OrderRepository _orderRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CheckoutController(UserManager<IdentityUser> userManager, CustomerRepository customerRepository, IWebHostEnvironment webHostEnvironment, ShipperRepository shipperRepository, OrderRepository orderRepository)
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
            _shipperRepository = shipperRepository;
            _webHostEnvironment = webHostEnvironment;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new CheckoutViewModel();
            var customer = GetCustomerOrAddError();
            var creditCard = GetCreditCardForUser();

            try
            {
                creditCard.GetCardForUser();
                model.CreditCard = creditCard.Number;
                model.ExpirationMonth = creditCard.Expiry.Month;
                model.ExpirationYear = creditCard.Expiry.Year;
            }
            catch (NullReferenceException)
            {
            }

            model.Cart = HttpContext.Session.Get<Cart>("Cart");
            if (model.Cart == null || model.Cart.OrderDetails.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "You have no items in your cart.");
            }

            if (customer != null)
            {
                model.ShipTarget = customer.CompanyName;
                model.Address = customer.Address;
                model.City = customer.City;
                model.Region = customer.Region;
                model.PostalCode = customer.PostalCode;
                model.Country = customer.Country;
            }

            model.AvailableExpirationYears = Enumerable.Range(1, 5).Select(i => DateTime.Now.Year + i).ToList();
            model.ShippingOptions = _shipperRepository.GetShippingOptions(model.Cart?.SubTotal ?? 0);

            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            model.Cart = HttpContext.Session.Get<Cart>("Cart")!;

            var customer = GetCustomerOrAddError();
            if(customer == null)
            {
                return View(model);
            }

            var creditCard = GetCreditCardForUser();
            try
            {
                creditCard.GetCardForUser();
            }
            catch (NullReferenceException)
            {
            }

            //Get form of payment
            //If old card is null or if the number, month or year were changed then take what was on the form.
            if (creditCard.Number.Length <= 4)
            {
                creditCard.Number = model.CreditCard;
                creditCard.Expiry = new DateTime(model.ExpirationYear, model.ExpirationMonth, 1);
            }
            else
            {
                if (model.CreditCard.Substring(model.CreditCard.Length - 4) !=
                    creditCard.Number.Substring(creditCard.Number.Length - 4))
                {
                    creditCard.Number = model.CreditCard;
                }

                if (model.ExpirationMonth != creditCard.ExpiryMonth || model.ExpirationYear != creditCard.ExpiryYear)
                {
                    creditCard.Expiry = new DateTime(model.ExpirationYear, model.ExpirationMonth, 1);
                }
            }

            //Authorize payment through our bank or Authorize.net or someone.
            if (!creditCard.IsValid())
            {
                ModelState.AddModelError(string.Empty, "That card is not valid. Please enter a valid card.");
                return View(model);
            }

            if (model.RememberCreditCard)
            {
                creditCard.SaveCardForUser();
            }

            var order = new Order
            {
                ShipVia = model.ShippingMethod,
                ShipName = model.ShipTarget,
                ShipAddress = model.Address,
                ShipCity = model.City,
                ShipRegion = model.Region,
                ShipPostalCode = model.PostalCode,
                ShipCountry = model.Country,
                OrderDetails = model.Cart.OrderDetails,
                CustomerId = customer.CustomerId,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(7),
                Freight = _shipperRepository.GetShipperByShipperId(model.ShippingMethod).GetShippingCost(model.Cart.SubTotal),
                EmployeeId = 1,
            };

            var approvalCode = creditCard.ChargeCard(order.Total);

            order.Shipment = new Shipment()
            {
                ShipmentDate = DateTime.Today.AddDays(1),
                ShipperId = order.ShipVia,
                TrackingNumber = _shipperRepository.GetNextTrackingNumber(_shipperRepository.GetShipperByShipperId(order.ShipVia)),
            };

            //Create the order itself.
            int orderId = _orderRepository.CreateOrder(order);

            //Create the payment record.
            _orderRepository.CreateOrderPayment(orderId, order.Total, creditCard.Number, creditCard.Expiry, approvalCode);

            HttpContext.Session.SetInt32("OrderId", orderId);
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Receipt");
        }

        public IActionResult Receipt(int? id)
        {
            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (id != null)
            {
                orderId = id;
            }

            if (orderId == null)
            {
                ModelState.AddModelError(string.Empty, "No order specified. Please try again.");
                return View();
            }

            Order order;
            try
            {
                order = _orderRepository.GetOrderById(orderId.Value);
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, string.Format("Order {0} was not found.", orderId));
                return View();
            }

            return View(order);
        }

        public IActionResult Receipts()
        {
            var customer = GetCustomerOrAddError();
            if(customer == null)
            {
                return View();
            }

            return View(_orderRepository.GetAllOrdersByCustomerId(customer.CustomerId));
        }

        public IActionResult PackageTracking(string? carrier, string? trackingNumber)
        {
            var model = new PackageTrackingViewModel()
            {
                SelectedCarrier = carrier,
                SelectedTrackingNumber = trackingNumber,
            };

            var customer = GetCustomerOrAddError();
            if (customer != null)
            {
                model.Orders = _orderRepository.GetAllOrdersByCustomerId(customer.CustomerId);
            }
            
            return View(model);
        }

        public IActionResult GoToExternalTracker(string carrier, string trackingNumber)
        {
            return Redirect(Order.GetPackageTrackingUrl(carrier, trackingNumber));
        }

        private Customer? GetCustomerOrAddError()
        {
            var username = _userManager.GetUserName(User);
            var customer = _customerRepository.GetCustomerByUsername(username);
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "I can't identify you. Please log in and try again.");
                return null;
            }

            return customer;
        }

        private CreditCard GetCreditCardForUser()
        {
            return new CreditCard()
            {
                Filename = Path.Combine(_webHostEnvironment.WebRootPath, "StoredCreditCards.xml"),
                Username = _userManager.GetUserName(User)
            };
        }
    }
}