using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wine.Business.Services;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.DAL.Interfaces;
using Wine.DataAccess;

namespace WineWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services
                    .AddScoped<ICountryService, CountryService>()
                    .AddScoped<IRegionService, RegionService>()
                    .AddScoped<IWineService, WineService>()
                    .AddScoped<ICountryRepository, CountryRepository>()
                    .AddScoped<IRegionRepository, RegionRepository>()
                    .AddScoped<IWineRepository, WineRepository>()
                    //.AddSingleton<IWineRepository>(new WineRepository(Configuration))
                    .BuildServiceProvider();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
