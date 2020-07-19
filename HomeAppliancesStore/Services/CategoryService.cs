using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Services
{
    public class CategoryService : IProductCategory
    {
        ///<inheritdoc/>
        public IEnumerable<CategoryProduct> allCategory
        {
            get
            {
                return new List<CategoryProduct>
                {
                    new CategoryProduct {categoryName = "Ноутбуки"},
                    new CategoryProduct {categoryName = "Мобильные телефоны"}
                };
            }
        }
    } 
}
