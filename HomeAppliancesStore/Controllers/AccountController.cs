using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HomeAppliancesStore.Controllers
{
    public class AccountController : Controller
    {
        private const string Admin = "Admin";
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel details)
        {
            if (ModelState.IsValid)
            {
                User user = await this.userManager.FindByEmailAsync(details.Email);

                if(user == null)
                {
                    ModelState.AddModelError(nameof(details.Email), $"'There is no user this {details.Email} address'");
                }
                else
                {

                    SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (user.UserName == Admin)
                        {
                            return View("Admin");
                        }
                        else
                        {
                            return Redirect("/Home/Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(LoginViewModel.Email), "Invalid user or password");
                    }
                }

            }

            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
