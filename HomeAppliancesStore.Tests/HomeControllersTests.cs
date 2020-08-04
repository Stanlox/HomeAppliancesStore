using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using HomeAppliancesStore.Controllers;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.ViewModels;
using HomeAppliancesStore.Repository;

namespace HomeAppliancesStore.Tests
{
    public class HomeControllersTests
    {
        private static Dictionary<string, CategoryProduct> category = new Dictionary<string, CategoryProduct>();
        private ProductRepositoryInMemory product = new ProductRepositoryInMemory();

        [Fact]
        public void AvailableReturnsAViewResultWithAAvailableViewModel()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(repo => repo.availableProduct).Returns(GetAvailableProducts());
            var controller = new HomeController(mock.Object);

            var result = controller.Available();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AvailableViewModel>(viewResult.Model);
            Assert.Equal(GetAvailableProducts().ToList().Count, product.GetAvailableProduct.Count());

        }

        [Fact]
        public void ListReturnsAViewResultWithAProductViewModel()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(repo => repo.products).Returns(GetPhonesProducts());
            var controller = new HomeController(mock.Object);

            var result = controller.List("��������");
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);
            Assert.Equal(GetPhonesProducts().ToList().Count, product.products.Select(x => x.Category).Where(x => x.categoryName == "��������� ��������").Count());
        }

        [Fact]
        public void IndexReturnAViewResult()
        { 
            var mock = new Mock<IProductRepository>();
            var controller = new HomeController(mock.Object);
            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void ContactsReturnAViewResult()
        {
            var mock = new Mock<IProductRepository>();
            var controller = new HomeController(mock.Object);
            var result = controller.Contacts();
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        private IEnumerable<Product> GetAvailableProducts()
        {
            var product = new List<Product>
            {
                    new Product
                    {
                         Name = "Epson EB-W39",
                         Price = 1849.92M,
                         isAvailable = true,
                         img = "/img/EB.png",
                         Category = Categories["���������"]
                    },
                    new Product
                    {
                        Name = "Galaxy Tab",
                        Price = 679M,
                        isAvailable = true,
                        img = "/img/tab.jpeg",
                        Category = Categories["��������"]
                    },
                     new Product
                    {
                        Name = "Prestigio Wize 4111",
                        Price = 880M,
                        isAvailable = true,
                        img = "/img/4117.png",
                        Category = Categories["��������"]
                    },
                      new Product
                    {

                        Name = "HP 255 G7 7DF18EA",
                        Price = 1024.52M,
                        isAvailable = true,
                        img = "/img/HP.jpeg",
                        Category = Categories["��������"]
                    },
                    new Product
                    {
                        Name = "Prestigio Wize 4117",
                        Price = 903M,
                        isAvailable = true,
                        img = "/img/4111.jpeg",
                        Category = Categories["��������"]
                    },
                     new Product
                    {
                        Name = "iPhone 7",
                        Price = 1342M,
                        isAvailable = true,
                        img = "/img/7.jpg",
                        Category = Categories["��������� ��������"]
                    },
                    new Product
                    {

                        Name = "iPhone SE",
                        Price = 877M,
                        isAvailable = true,
                        img = "/img/SEpng.png",
                        Category = Categories["��������� ��������"]
                    },
                    new Product
                    {
                        Name = "ASUS X540SA-XX236T",
                        Price = 668.58M,
                        isAvailable = true,
                        img = "/img/ASUS.jpg",
                        Category = Categories["��������"]
                    },
                    new Product
                     {
                         Name = "iPhone XR",
                         Price = 2100M,
                         isAvailable = true,
                         img = "/img/XR.png",
                         Category = Categories["��������� ��������"]
                     },
                    new Product
                    {
                        Name = "iPhone XS",
                        Price = 2010M,
                        isAvailable = true,
                        img = "/img/XS.jpg",
                        Category = Categories["��������� ��������"]
                    },
                    new Product
                    {
                        Name = "iPhone X",
                        Price = 1980M,
                        isAvailable = true,
                        img = "/img/X.png",
                        Category = Categories["��������� ��������"]
                    },
            };


            return product.AsEnumerable();
        }

        private IEnumerable<Product> GetPhonesProducts()
        {
            var product = new List<Product>
            {
                     new Product
                     {
                         Name = "iPhone XR",
                         Price = 2100M,
                         isAvailable = true,
                         img = "/img/XR.png",
                         Category = Categories["��������� ��������"]
                     },
                    new Product
                    {
                        Name = "iPhone XS",
                        Price = 2010M,
                        isAvailable = true,
                        img = "/img/XS.jpg",
                        Category = Categories["��������� ��������"]
                    },
                    new Product
                    {
                        Name = "iPhone X",
                        Price = 1980M,
                        isAvailable = true,
                        img = "/img/X.png",
                        Category = Categories["��������� ��������"]
                    },
                    new Product
                    {
                        Name = "iPhone 7",
                        Price = 1342M,
                        isAvailable = true,
                        img = "/img/7.jpg",
                        Category = Categories["��������� ��������"]
                    },
                    new Product
                    {

                        Name = "iPhone SE",
                        Price = 877M,
                        isAvailable = true,
                        img = "/img/SEpng.png",
                        Category = Categories["��������� ��������"]
                    },
            };


            return product.AsEnumerable();
        }

        public static Dictionary<string, CategoryProduct> Categories
        {
            get
            {
                if (!category.Any())
                {
                    var arrayOfCategories = new CategoryProduct[]
                    {
                         new CategoryProduct {categoryName = "��������"},
                         new CategoryProduct {categoryName = "��������� ��������"},
                         new CategoryProduct {categoryName = "��������"},
                         new CategoryProduct {categoryName = "���������"}
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
