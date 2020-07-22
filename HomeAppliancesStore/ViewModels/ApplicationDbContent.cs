using HomeAppliancesStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.ViewModels
{
    public class ApplicationDbContent : DbContext
    {
        public ApplicationDbContent(DbContextOptions<ApplicationDbContent> options)
            : base(options)
        { }

        public DbSet<Product> Product { get; set; }
        public DbSet<CategoryProduct> Caategory { get; set; }
    }
}
