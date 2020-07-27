using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> userManager;

        public AdminController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

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
                    return RedirectToAction("Index");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}
