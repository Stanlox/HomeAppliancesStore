﻿using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Services
{
    public class ProductService : IProduct
    {
        private readonly IProductCategory productCategory = new CategoryService();
        public IEnumerable<Product> products
        {
            get
            {
                return new List<Product>
                {
                    new Product
                    {
                       ProductId = 1,
                       Name = "iPhone XR",
                       Price = 2100M,
                       isAvailable = true,
                       img = "/img/XR.png",
                       Category = productCategory.allCategory.Last(),
                    },
                    new Product
                    {
                       ProductId = 2,
                       Name = "iPhone XS",
                       Price = 2010M,
                       isAvailable = true,
                       img = "/img/XS.jpg",
                       Category = productCategory.allCategory.Last(),
                    },
                    new Product
                    {
                       ProductId = 3,
                       Name = "iPhone X",
                       Price = 1980M,
                       isAvailable = true,
                       img = "/img/X.png",
                       Category = productCategory.allCategory.Last(),
                    },
                    new Product
                    {
                       ProductId = 4,
                       Name = "iPhone 7",
                       Price = 1342M,
                       isAvailable = true,
                       img = "/img/7.jpg",
                       Category = productCategory.allCategory.Last(),
                    },
                    new Product
                    {
                       ProductId = 5,
                       Name = "iPhone SE",
                       Price = 877M,
                       isAvailable = true,
                       img = "/img/SEpng.png",
                       Category = productCategory.allCategory.Last(),
                    },
                    new Product
                    {
                       ProductId = 5,
                       Name = "ASUS X540SA-XX236T",
                       Price = 668.58M,
                       isAvailable = true,
                       img = "/img/ASUS.jpg",
                       Category = productCategory.allCategory.First(),
                    },
                    new Product
                    {
                       ProductId = 5,
                       Name = "LENOVO IDEAPAD 300",
                       Price = 1238.10M,
                       isAvailable = false,
                       img = "/img/Lenovo.jpg",
                       Category = productCategory.allCategory.First(),
                    },
                    new Product
                    {
                       ProductId = 5,
                       Name = "HP 255 G7 7DF18EA",
                       Price = 1024.52M,
                       isAvailable = true,
                       img = "/img/HP.jpeg",
                       Category = productCategory.allCategory.First(),
                    },
                };
            }
        }

        public IEnumerable<Product> availableProduct()
        {
            return products.Where(x => x.isAvailable == true);
        }
    }
}
