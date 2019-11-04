using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebGoatCore.ViewModels;

namespace WebGoatCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CustomerRepository _customerRepository;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CustomerRepository customerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Password,RememberMe")] LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(model.Username) {
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _customerRepository.CreateCustomer(model.CompanyName, model.Username, model.Address, model.City, model.Region, model.PostalCode, model.Country);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}