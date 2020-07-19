using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;

namespace HomeAppliancesStore.Services
{
    public class OrderService : IOrders
    {
        private Basket basket;

        public OrderService(Basket basket)
        {
            this.basket = basket;
        }
        public void CreateOrder(Order order)
        {
            var devices = basket.listProducts;

            foreach ( var device in devices)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = device.product.ProductId,
                    Price = device.product.Price,
                    OrderId = device.Id
                };
            }
        }
    }
}
