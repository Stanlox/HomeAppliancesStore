using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        IEnumerable<Product> products { get; }

        /// <summary>
        /// Gets a list of available products.
        /// </summary>
        IEnumerable<Product> availableProduct { get; }

        /// <summary>
        /// Save a product with a change.
        /// </summary>
        /// <param name="product">Input product.</param>
        void SaveProduct(Product product);
    }
}
