using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HomeAppliancesStore.Controllers;
using HomeAppliancesStore.DTO;
using HomeAppliancesStore.Filter;
using HomeAppliancesStore.Interfaces;
using HomeAppliancesStore.Models;
using HomeAppliancesStore.Repository;
using HomeAppliancesStore.Services;
using HomeAppliancesStore.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeAppliancesStore
{
    public class Startup
    {
        private IConfigurationRoot conf;
        public Startup(IConfiguration configuration, IHostingEnvironment host)
        {
            conf = new ConfigurationBuilder().SetBasePath(host.ContentRootPath).AddJsonFile("appsettings.json").Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContent>(options => options.UseSqlServer(conf.GetConnectionString("DefaultConnection")));
            services.AddTransient<IProduct, ProductRepository>();
            services.AddTransient<IProductCategory, CategoryRepository>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<EmailService>();
            services.AddTransient<IOrders, OrderService>();
            services.AddScoped(x => Basket.IsAddedProduuctInBasket(x));
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(new CountRequestAttribute());
                });
            services.AddMvc(
                config =>
                {

                    config.Filters.Add(new ExceptionFilterAttribute());
                });
                
            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{nameCategory?}");
            });

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContent content = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContent>();
                ProductDto.Initial(content);
            }
        }
    }
}
