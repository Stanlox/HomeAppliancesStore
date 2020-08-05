using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Models
{
    public class CategoryProduct
    {
        public int id { get; set; }
        public string categoryName { get; set; } 
        public List<Product> products { get; set; }
    }
}
