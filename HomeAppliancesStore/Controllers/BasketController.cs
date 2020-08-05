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
        private readonly IProductRepository product;
        private readonly Basket basket;

        public BasketController(IProductRepository product, Basket basket)
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
                TempData["message"] = string.Format("Для того чтобы добавить товар в корзину необходимо авторизоваться");
                return Redirect("/Home/List");
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
