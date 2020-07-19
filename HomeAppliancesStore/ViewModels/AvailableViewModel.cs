using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.ViewModels
{
    public class AvailableViewModel
    {
        public IEnumerable<Product> availableProduct { get; set; }
    }
}
