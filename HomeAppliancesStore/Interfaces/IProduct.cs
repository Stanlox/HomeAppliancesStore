using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Interfaces
{
    public interface IProduct
    {
        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        IEnumerable<Product> products { get; }

        /// <summary>
        /// Gets a list of available products.
        /// </summary>
        IEnumerable<Product> availableProduct();
    }
}
