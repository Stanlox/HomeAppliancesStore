using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Repository
{
    public class ProductRepositoryInMemory
    {
        public List<Product> products;
        private static Dictionary<string, CategoryProduct> category = new Dictionary<string, CategoryProduct>();

        public ProductRepositoryInMemory()
        {
            products = new List<Product>
            {
                    new Product
                    {
                         Name = "Epson EB-W39",
                         Price = 1849.92M,
                         isAvailable = true,
                         img = "/img/EB.png",
                         Category = Categories["Проекторы"]
                    },
                    new Product
                    {
                        Name = "Galaxy Tab",
                        Price = 679M,
                        isAvailable = true,
                        img = "/img/tab.jpeg",
                        Category = Categories["Планшеты"]
                    },
                     new Product
                    {
                        Name = "Prestigio Wize 4111",
                        Price = 880M,
                        isAvailable = true,
                        img = "/img/4117.png",
                        Category = Categories["Планшеты"]
                    },
                      new Product
                    {

                        Name = "HP 255 G7 7DF18EA",
                        Price = 1024.52M,
                        isAvailable = true,
                        img = "/img/HP.jpeg",
                        Category = Categories["Ноутбуки"]
                    },
                    new Product
                    {
                        Name = "Prestigio Wize 4117",
                        Price = 903M,
                        isAvailable = true,
                        img = "/img/4111.jpeg",
                        Category = Categories["Планшеты"]
                    },
                     new Product
                    {
                        Name = "iPhone 7",
                        Price = 1342M,
                        isAvailable = true,
                        img = "/img/7.jpg",
                        Category = Categories["Мобильные телефоны"]
                    },
                    new Product
                    {

                        Name = "iPhone SE",
                        Price = 877M,
                        isAvailable = true,
                        img = "/img/SEpng.png",
                        Category = Categories["Мобильные телефоны"]
                    },
                    new Product
                    {
                        Name = "ASUS X540SA-XX236T",
                        Price = 668.58M,
                        isAvailable = true,
                        img = "/img/ASUS.jpg",
                        Category = Categories["Ноутбуки"]
                    },
                    new Product
                     {
                         Name = "iPhone XR",
                         Price = 2100M,
                         isAvailable = true,
                         img = "/img/XR.png",
                         Category = Categories["Мобильные телефоны"]
                     },
                    new Product
                    {
                        Name = "iPhone XS",
                        Price = 2010M,
                        isAvailable = true,
                        img = "/img/XS.jpg",
                        Category = Categories["Мобильные телефоны"]
                    },
                    new Product
                    {
                        Name = "iPhone X",
                        Price = 1980M,
                        isAvailable = true,
                        img = "/img/X.png",
                        Category = Categories["Мобильные телефоны"]
                    },
            };
        }

        public IEnumerable<Product> GetAvailableProduct
        {
            get
            {
                return products.Where(x => x.isAvailable = true);
            }
        }

        public static Dictionary<string, CategoryProduct> Categories
        {
             get
             {
                  if (!category.Any())
                  {
                       var arrayOfCategories = new CategoryProduct[]
                       {
                            new CategoryProduct {categoryName = "Ноутбуки"},
                            new CategoryProduct {categoryName = "Мобильные телефоны"},
                            new CategoryProduct {categoryName = "Планшеты"},
                            new CategoryProduct {categoryName = "Проекторы"}
                       };

                      foreach (var categoryProduct in arrayOfCategories)
                      {
                           category.Add(categoryProduct.categoryName, categoryProduct);
                      }
                  }

                  return category;
             }
        }
    }
}

