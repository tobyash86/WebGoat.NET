using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebGoatCore.ViewModels;

namespace WebGoatCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;

        public HomeController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel() {
                TopOffers = _productRepository.GetTopProducts(4)
            });
        }

        public IActionResult About() => View();

        [Authorize(Roles = "Admin")]
        public IActionResult Admin() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
