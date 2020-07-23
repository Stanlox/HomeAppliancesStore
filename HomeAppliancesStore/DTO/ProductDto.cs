using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.DTO
{
    public class ProductDto
    {
        private static Dictionary<string, CategoryProduct> category = new Dictionary<string, CategoryProduct>();
        public static void Initial(ApplicationDbContent content)
        {
            if (!content.Category.Any())
            {
                content.Category.AddRange(Categories.Select(x => x.Value));
            }

            if (!content.Product.Any())
            {
                content.AddRange(
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
                        Name = "LENOVO IDEAPAD 300",
                        Price = 1238.10M,
                        isAvailable = false,
                        img = "/img/Lenovo.jpg",
                        Category = Categories["Ноутбуки"]
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
                        Name = "Prestigio Wize 4111",
                        Price = 880M,
                        isAvailable = true,
                        img = "/img/4117.png",
                        Category = Categories["Планшеты"]
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
                        Name = "Epson EB-U05",
                        Price = 2263.70M,
                        isAvailable = false,
                        img = "/img/epson.png",
                        Category = Categories["Проекторы"]
                    },
                    new Product
                    {                       
                        Name = "Panasonic PT-TW350",
                        Price = 2318.62M,
                        isAvailable = false,
                        img = "/img/tab.jpeg",
                        Category = Categories["Проекторы"]
                    },
                    new Product
                    {                       
                        Name = "Epson EB-W39",
                        Price = 1849.92M,
                        isAvailable = true,
                        img = "/img/EB.png",
                        Category = Categories["Проекторы"]
                    }
                );
            }

            content.SaveChanges();
        }

        public static Dictionary<string, CategoryProduct> Categories
        {
            get
            {
                if(!category.Any())
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
