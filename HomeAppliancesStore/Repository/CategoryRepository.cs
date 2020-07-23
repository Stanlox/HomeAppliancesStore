using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Repository
{
    public class CategoryRepository : IProductCategory
    {
        private readonly ApplicationDbContent dbContent;

        public CategoryRepository(ApplicationDbContent dbContent)
        {
            this.dbContent = dbContent;
        }
        public IEnumerable<CategoryProduct> allCategory
        {
            get
            {
                return dbContent.Category; 
            }
        }
    }
}
