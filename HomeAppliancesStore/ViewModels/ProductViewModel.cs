using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.ViewModels
{
    public class ProductViewModel
    {
        /// <summary>
        /// Gets all the products.
        /// </summary>
        public IEnumerable<Product> products { get; set; }

        /// <summary>
        /// Name of current category.
        /// </summary>
        public string Category { get; set; }
    }
}
