using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Models
{
    public class Basket
    {
        private static List<BasketProduct> productsFromBasket = new List<BasketProduct>();
        public string BasketId { get; set; } = String.Empty;

        public List<BasketProduct> listProducts { get; set; }

        public static Basket IsAddedProduuctInBasket(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var basketId = session.GetString("BasketId") ?? Guid.NewGuid().ToString();
            session.SetString("BasketId", basketId);
            return new Basket()
            {
                BasketId = basketId
            };
        }

        public void AddProductInBasket(Product product)
        {
            productsFromBasket.Add(new BasketProduct
            {
                product = product,
                Id = product.ProductId,
                productIdInBasket = BasketId,
            });
        }

        public List<BasketProduct> GetProductFromBasket()
        {
            return productsFromBasket.AsEnumerable().Where(x => x.productIdInBasket == BasketId).ToList();
        }

        public IEnumerable<BasketProduct> GetProductFromBasketAftereSession()
        {
            return productsFromBasket;
        }

        public int GetCountProductFromBasket()
        {
            listProducts = productsFromBasket;
            return listProducts.Count();
        }

        public void DeleteProductFromBasket(int id)
        {
            var productToRemove = productsFromBasket.Single(x => x.Id == id);
            productsFromBasket.Remove(productToRemove);
        }
    }
}
