using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStoreCore1.Models;
using SportStoreCore1.Models.Interfaces;

namespace SportStoreCore1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options => 
            options.UseSqlServer(
                Configuration["Data:SportStoreProductsCore1:ConnectionString"]
                ));

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routers => {

                routers.MapRoute(name: null, template: "{category}/Page{page:int}", defaults: new { controller = "Product", action = "List" });

                //routers.MapRoute(
                //    name: null,
                //    template: "{category}/Page{page:int}",
                //    defaults: new { Controller = "Product", action = "List" });

                routers.MapRoute(name: null, template: "Page{page:int}", defaults: new { controller = "Product", action = "List", page = 1 });


                //routers.MapRoute(
                //   name: null,
                //   template: "Page{page:int}",
                //   defaults: new { Controller = "Product", action = "List", page = 1 });

                routers.MapRoute(name: null, template: "{category}", defaults: new { controller = "Product", action = "List", page = 1 });

                //routers.MapRoute(
                //   name: null,
                //   template: "{category}",
                //   defaults: new { Controller = "Product", action = "List", page = 1 });

                routers.MapRoute(name: null, template: "", defaults: new { controller = "Product", action = "List", page = 1 });


                //routers.MapRoute(
                //   name: null,
                //   template: "",
                //   defaults: new { Controller = "Product", action = "List", page = 1 });

                routers.MapRoute(name: null, template: "{controller}/{action}/{id?}");

                //routers.MapRoute(
                //    name: "default",
                //    template: "{controller=Product}/{action=List}/{id?}");
            });
          
            SeedData.EnsurePopulated(app);
        }
    }
}
