using HomeAppliancesStore.Filter;
using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.Services;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    [CountRequest]
    public class BasketController : Controller
    {
        private Basket basket = new Basket();
        private IProduct productService;

        public BasketController(IProduct productService)
        {
            this.productService = productService;
        }
        public ViewResult Index()
        {
            var product = basket.GetProductFromBasket();
            basket.listProducts = product;

            var basketViewModel = new BasketViewModel
            {
                basket = basket
            };

            return View(basketViewModel);
        }

        public RedirectToActionResult AddInBasket(int id)
        {
            var product = productService.products.FirstOrDefault(x => x.ProductId == id);
            if(product != null)
            {
                basket.AddProductInBasket(product);
            }

            return RedirectToAction("Index"); 
        }
    }
}
