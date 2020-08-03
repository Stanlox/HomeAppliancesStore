using HomeAppliancesStore.Models;
using HomeAppliancesStore.Services;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly EmailService service;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, EmailService service)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.service = service;
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
                    if(user.UserName != "Admin")
                    {
                        if (!await userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                            return View(details);
                        }
                    }

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

        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword([Required]string email)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                service.SendEmail(email, "Сброс пароля", $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(email);
        }

        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}
