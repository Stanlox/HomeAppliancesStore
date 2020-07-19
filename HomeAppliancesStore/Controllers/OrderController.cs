using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrders orders;
        private readonly Basket basket;

        public OrderController(IOrders orders, Basket basket)
        {
            this.orders = orders;
            this.basket = basket;
        }

        [HttpGet]
        public IActionResult Decoration()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Decoration(Order order)
        {
            var countDeviceInBasket = basket.GetCountProductFromBasket();

            if(countDeviceInBasket == 0)
            {
                ModelState.AddModelError("", "Для того чтобы совершить покупку для начало необходимо добавить товар в корзину");
            }

            if (ModelState.IsValid)
            {
                orders.CreateOrder(order);
                return RedirectToAction("Successful");
            }
            return View(order);
        }

        public ViewResult Successful()
        {
            ViewBag.Message = "Заказ успешно обработан, дополнительная информация вышлена вам на почту";
            return View();
        }
    }
}
