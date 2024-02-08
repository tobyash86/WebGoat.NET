using WebGoatCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebGoatCore.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using OpenTelemetry;

namespace WebGoatCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ProductRepository _productRepository;
        string filepath = "C:\\Users\\jruszil\\Desktop\\optm_research\\baggage.txt";

        public HomeController(ProductRepository productRepository)
        {
            var b = Baggage.Current;
            System.IO.File.AppendAllText(filepath, "Baggage: \n");
            foreach (var x in Baggage.Current.GetBaggage())
            {
                System.IO.File.AppendAllText
                    (filepath, string.Format("{0}: {1}\n", x.Key, x.Value));
            }
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel()
            {
                TopProducts = _productRepository.GetTopProducts(4)
            });
        }

        public IActionResult About() => View();

        [Authorize(Roles = "Admin")]
        public IActionResult Admin() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ExceptionInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>(),
            });
        }
    }
}
