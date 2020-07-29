﻿using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
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
                            ModelState.AddModelError("", result.Errors.ToString());
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
                            ModelState.AddModelError("", result.Errors.ToString());
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
                    ModelState.AddModelError("", result.Errors.ToString());
                }
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            if(role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.ToString());
                }
            }
            else
            {
                ModelState.AddModelError("", "Role no found");
            }

            return View("Index", roleManager.Roles);
        }
    }
}