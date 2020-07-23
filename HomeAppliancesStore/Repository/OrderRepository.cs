using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Repository
{
    public class OrderRepository : IOrders
    {
        private readonly ApplicationDbContent applicationDbContent;
        private readonly Basket basket;

        public OrderRepository(ApplicationDbContent applicationDbContent, Basket basket)
        {
            this.applicationDbContent = applicationDbContent;
            this.basket = basket;
        }

        public void CreateOrder(Order order)
        {
            applicationDbContent.order.Add(order);
            var devices = basket.GetProductFromBasket();

            foreach (var device in devices)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = device.product.Id,
                    OrderId = order.id,
                    Price = device.product.Price,
                };

                applicationDbContent.orderDetail.Add(orderDetail);
            }

            applicationDbContent.SaveChanges();
        }
    }
}
