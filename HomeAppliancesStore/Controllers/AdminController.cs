using HomeAppliancesStore.Models;
using HomeAppliancesStore.Services;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly EmailService service;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, EmailService service)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.service = service;
        }
        
        [Authorize]
        public ViewResult Index()
        {
            ViewBag.Message = "Вы успешно зарегестрировались!";
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                IdentityResult result = await this.userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return RedirectToAction("ListUsers");
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Admin",
                        new { userId = user.Id, code = code, password = model.Password },
                        protocol: HttpContext.Request.Scheme);

                     service.SendEmail(model.Email, "Подтверждения регистрации", $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
                     return View("CreateAccountConfirm");
                }
                
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code, string password)
        {
            var user = await userManager.FindByIdAsync(userId);
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                Microsoft.AspNetCore.Identity.SignInResult resultSignIn = await signInManager.PasswordSignInAsync(user, password, false, false);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
