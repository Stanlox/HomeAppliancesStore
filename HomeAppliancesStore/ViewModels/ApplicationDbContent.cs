using HomeAppliancesStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.ViewModels
{
    public class ApplicationDbContent : IdentityDbContext<User>
    {
        public ApplicationDbContent(DbContextOptions<ApplicationDbContent> options)
            : base(options)
        { }

        public DbSet<Product> Product { get; set; }
        public DbSet<CategoryProduct> Category { get; set; }       
        public DbSet<BasketProduct> BasketProduct { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<Order> order { get; set; }
    }
}
