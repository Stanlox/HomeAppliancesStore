using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Models
{
    public class BasketProduct
    {
        /// <summary>
        /// Product id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product.
        /// </summary>
        public Product product { get; set; }


        /// <summary>
        /// Id product in basket.
        /// </summary>
        public string productIdInBasket { get; set; }
    }
}
