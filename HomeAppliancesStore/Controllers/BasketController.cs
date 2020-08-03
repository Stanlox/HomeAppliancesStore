using HomeAppliancesStore.Filter;
using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.Repository;
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
        private readonly IProduct product;
        private readonly Basket basket;

        public BasketController(IProduct product, Basket basket)
        {
            this.basket = basket;
            this.product = product;
        }
        public ViewResult Index()
        {
            var product = basket.GetProductFromBasket();
            basket.productsFromBasket = product;

            var basketViewModel = new BasketViewModel
            {
                basket = basket
            };

            return View(basketViewModel);
        }

        public IActionResult DeleteProduct(int id)
        {
            basket.DeleteProductFromBasket(id);
            return RedirectToAction("Index");
        }

        public IActionResult AddInBasket(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("NotAuthorized");
            }
            var concreteProduct = product.products.FirstOrDefault(x => x.Id == id);
            if(product != null)
            {
                basket.AddProductInBasket(concreteProduct);
            }

            return RedirectToAction("Index"); 
        }
    }
}
