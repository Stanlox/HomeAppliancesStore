using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAppliancesStore.ViewModels;

namespace HomeAppliancesStore.Models
{
    public class Basket
    {
        private readonly ApplicationDbContent dbContent;
        public Basket(ApplicationDbContent dbContent)
        {
            this.dbContent = dbContent;
        }

        public List<BasketProduct> productsFromBasket { get; set; }
        public string BasketId { get; set; } = String.Empty;


        public static Basket IsAddedProduuctInBasket(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ApplicationDbContent>();
            var basketId = session.GetString("BasketId") ?? Guid.NewGuid().ToString();
            session.SetString("BasketId", basketId);
            return new Basket(context)
            {
                BasketId = basketId
            };
        }

        public void AddProductInBasket(Product product)
        {
            this.dbContent.BasketProduct.Add(new BasketProduct
            {
                productIdInBasket = BasketId,
                product = product,
            });

            this.dbContent.SaveChanges();
        }

        public List<BasketProduct> GetProductFromBasket()
        {
            return this.dbContent.BasketProduct.Where(x => x.productIdInBasket == BasketId).Include(x => x.product).ToList();
        }

        public int GetCountProductFromBasket()
        {
            return dbContent.BasketProduct.Count();
        }

        public void DeleteProductFromBasket(int id)
        {
            var productToRemove = this.dbContent.BasketProduct.Single(x => x.Id == id);
            this.dbContent.BasketProduct.Remove(productToRemove);
            this.dbContent.SaveChanges();
        }
    }
}
