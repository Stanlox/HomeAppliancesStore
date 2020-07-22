using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContent dbContent;

        public ProductRepository(ApplicationDbContent dbContent)
        {
            this.dbContent = dbContent;
        }
        public IEnumerable<Product> products
        {
            get
            {
                return dbContent.Product.Include(x => x.Category);
            }
        }

        public IEnumerable<Product> availableProduct
        {
            get
            {
                return dbContent.Product.Where(x => x.isAvailable).Include(x => x.Category);
            }
        }
    }
}
