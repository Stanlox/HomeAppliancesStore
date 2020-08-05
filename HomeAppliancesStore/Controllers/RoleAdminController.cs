using HomeAppliancesStore.Filter;
using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Middleware;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;
        private readonly IProductRepository product;

        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IProductRepository product)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.product = product;
        }

        public ViewResult Index()
        {
            return View(this.roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<User> representative = new List<User>();
            List<User> notRepresentative = new List<User>();
            foreach (var user in this.userManager.Users) 
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? representative : notRepresentative;
                list.Add(user);
            }

            return View(new RoleEditViewModel
            {
                Role = role,
                Representative = representative,
                NotRepresentative = notRepresentative
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationViewModel roleModificationViewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in roleModificationViewModel.ToAdd ?? new string[] { })
                {
                    User user = await this.userManager.FindByIdAsync(userId);

                    if(user != null)
                    {
                        IdentityResult result = await this.userManager.AddToRoleAsync(user, roleModificationViewModel.RoleName);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, result.Errors.ToString());
                        }
                    }
                }

                foreach (var userId in roleModificationViewModel.ToDelete ?? new string[] { })
                {
                    User user = await this.userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        IdentityResult result = await this.userManager.RemoveFromRoleAsync(user, roleModificationViewModel.RoleName);

                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, result.Errors.ToString());
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return await Edit(roleModificationViewModel.RoleId);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, result.Errors.ToString());
                }
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user!= null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Errors.ToString());
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Role no found");
            }

            return View("Index", roleManager.Roles);
        }

        public ViewResult GetRequest()
        {
            var count = CountRequestAttribute.GetCountRequest();
            var info = CountRequestAttribute.GetMoreInformationAboutRequest();
            ViewBag.Message = ++count;
            return View(info);
        }

        public ViewResult GetAuthenticatedUsers()
        {
            var info = RequestMiddleware.GetListAuthenticatedUsers;
            var countRequest = RequestMiddleware.GetCountAuthenticatedRequest;
            ViewBag.Message = ++countRequest;
            return View(info);
        }

        public ViewResult ListUsers()
        {
            return View(this.userManager.Users);
        }

        public ViewResult ProductManagement()
        {
            var allProducts = product.products;
            var productViewModel = new ProductViewModel
            {
                products = allProducts,
                Category = string.Empty
            };
            return View(Tuple.Create(productViewModel, new Product()));
        }

        public ViewResult EditProduct(int Id)
        {
            var foundProductById = product.products.FirstOrDefault(x => x.Id == Id);
            return View(foundProductById);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                this.product.SaveProduct(product);
                TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", product.Name);
                return RedirectToAction("ProductManagement");
            }
            else
            {
                return View(product);
            }
            
        }
    }
}
