using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProduct product;
        private  Dictionary<string, List<Product>> productDictionary = new Dictionary<string, List<Product>>();
        public SearchController(IProduct product)
        {
            this.product = product;
            foreach (var particularProduct in this.product.products)
            {
                if (this.productDictionary.ContainsKey(particularProduct.Name))
                {
                    this.productDictionary[particularProduct.Name].Add(particularProduct);
                }
                else
                {
                    this.productDictionary.Add(particularProduct.Name, new List<Product> { particularProduct });
                }
            }
        }

        [HttpPost]
        public IActionResult Index(string nameProduct)
        {
            if (string.IsNullOrEmpty(nameProduct))
            {
                return Redirect("/Home/List");
            }

            var mostSimilarProducts = GetMostSimilarProducts(nameProduct);
            if (!mostSimilarProducts.Any())
            {
                return Redirect("/Home/List");
            }

            List<List<Product>> foundProducts = new List<List<Product>>();
            foreach (var product in productDictionary)
            {
                foreach (var productName in mostSimilarProducts)
                {
                    if(productName == product.Key)
                    {
                        foundProducts.Add(product.Value);
                    }
                }
            }

            return View(foundProducts);
        }

        private IEnumerable<string> GetMostSimilarProducts(string nameProduct)
        {
            List<string> list = productDictionary.Select(item => item.Key).ToList();
            var requestCommandSymbols = nameProduct.ToUpperInvariant();
            var productsIntersactions = list.Select(command => (command, command.ToUpperInvariant()))
                .Select(commandTuple => (commandTuple.command, commandTuple.Item2.Intersect(requestCommandSymbols).Count()));
            var max = productsIntersactions.Max(tuple => tuple.Item2);
            return max > 2 ? productsIntersactions.Where(tuple => tuple.Item2.Equals(max)).Select(tuple => tuple.command) : Enumerable.Empty<string>();
        }
    }
}
