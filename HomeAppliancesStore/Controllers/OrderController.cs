using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrders orders;
        private readonly Basket basket;
        private readonly ILogger<OrderController> logger;
        private readonly EmailService service;

        public OrderController(IOrders orders, Basket basket
            , ILogger<OrderController> logger, EmailService service)
        {
            this.orders = orders;
            this.basket = basket;
            this.logger = logger;
            this.service = service;
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
                return RedirectToAction("Successful", order);
            }
            return View(order);
        }

        public ViewResult Successful(Order order)
        {
            ViewBag.Message = $"Заказ успешно обработан, дополнительная информация вышлена вам на почту по адресу {order.Email}";
            var textMessage = string.Empty;
            basket.listProducts = basket.GetProductFromBasketAftereSession().ToList();
            var products = basket.listProducts;
            StringBuilder builder = new StringBuilder();
            builder.Append("Ваш заказ принят, спасибо что выбрали нас! ");
            foreach (var orderedProduct in products)
            {
                builder.Append($"Вы приобрели: {orderedProduct.product.Name} По цене {orderedProduct.product.Price} руб. ");
            }
            builder.Append($"В ближайшее время с вами свяжутся наши сотрудники по телефону {order.Phone} для подтверждения заказа.");
            service.SendEmail(order.Email, builder.ToString());
            return View();
        }
    }
}
