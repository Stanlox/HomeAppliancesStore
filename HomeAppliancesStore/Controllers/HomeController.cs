using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.ViewModels;
using HomeAppliancesStore.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Security.Claims;

namespace HomeAppliancesStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct product;
        private static string NameDeviceCategory = "Все товары";
        private IEnumerable<Product> productsByCategoria = new List<Product>();


        delegate IEnumerable<Product> GetProducts(IEnumerable<Product> productsByCategoria, string nameCategory, IProduct product);
        private Tuple<string, GetProducts>[] commands = new Tuple<string, GetProducts>[]
         {
                new Tuple<string, GetProducts>("phones", GetPhones),
                new Tuple<string, GetProducts>("laptop", GetLaptop),
                new Tuple<string, GetProducts>("tablets", GetTablets),
                new Tuple<string, GetProducts>("projectors", GetProjectors)
         };


        public HomeController(IProduct product, IProductCategory productCategory)
        {
            this.product = product;
        }


        public ViewResult Index()
        {
            ViewBag.Message = "Добро пожаловать на сайт интернет-магазина продажи бытовой техники.";
            return View();
        }

        public ViewResult Contacts()
        {
            return View();
        }

        public ViewResult List(string nameCategory)
        {
            if (string.IsNullOrEmpty(nameCategory))
            {
                productsByCategoria = product.products;
                ViewBag.Category = "Все товары";
            }
            else
            {
                var index = Array.FindIndex(commands, j => j.Item1.Equals(nameCategory, StringComparison.InvariantCultureIgnoreCase));

                if (index >= 0)
                {
                    productsByCategoria = commands[index].Item2(productsByCategoria, nameCategory, product);
                    ViewBag.Category = NameDeviceCategory;
                }
            }
            var productViewModel = new ProductViewModel
            {
                products = productsByCategoria,
                Category = NameDeviceCategory,
            };

            return View(productViewModel);
        }

        public ViewResult Available()
        {
            var availableProduct = new AvailableViewModel
            {
                availableProduct = product.availableProduct
            };

            return View(availableProduct);
        }

        public static IEnumerable<Product> GetPhones(IEnumerable<Product> productsByCategory, string nameCategory, IProduct product)
        {
            NameDeviceCategory = "Мобильные телефоны";
            return productsByCategory = product.products.Where(i => i.Category.categoryName.Equals(NameDeviceCategory, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<Product> GetLaptop(IEnumerable<Product> productsByCategory, string nameCategory, IProduct product)
        {
            NameDeviceCategory = "Ноутбуки";
            return productsByCategory = product.products.Where(i => i.Category.categoryName.Equals(NameDeviceCategory, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<Product> GetTablets(IEnumerable<Product> productsByCategory, string nameCategory, IProduct product)
        {
            NameDeviceCategory = "Планшеты";
            return productsByCategory = product.products.Where(i => i.Category.categoryName.Equals(NameDeviceCategory, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<Product> GetProjectors(IEnumerable<Product> productsByCategory, string nameCategory, IProduct product)
        {
            NameDeviceCategory = "Проекторы";
            return productsByCategory = product.products.Where(i => i.Category.categoryName.Equals(NameDeviceCategory, StringComparison.InvariantCultureIgnoreCase));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
