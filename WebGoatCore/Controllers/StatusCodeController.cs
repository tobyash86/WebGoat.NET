using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGoatCore.ViewModels;

namespace WebGoatCore.Controllers
{
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public class StatusCodeController : Controller
    {
        public const string NAME = "StatusCode";

        public StatusCodeController()
        {
        }

        [HttpGet, Route(NAME)]
        public IActionResult StatusCodeView([FromQuery] int code)
        {
            var view = View(StatusCodeViewModel.Create(new ApiResponse(code)));
            view.StatusCode = code;
            return view;
        }
    }
}