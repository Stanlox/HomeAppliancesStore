using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeAppliancesStore.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public string img { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Наличие")]
        public bool isAvailable { get; set; }

        [ScaffoldColumn(false)]
        public int categoryId { get; set; }

        [Display(Name = "Категория")]
        public virtual CategoryProduct Category { get; set; }
    }
}
