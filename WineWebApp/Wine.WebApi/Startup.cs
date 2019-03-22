using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Wine.DataAccess;
using Wine.Commons.Business.Interfaces;
using Wine.Business.Services;
using Wine.Commons.DAL.Interfaces;
using AutoMapper;

namespace Wine.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();

            services
                   .AddScoped<ICountryService, CountryService>()
                   .AddScoped<IRegionService, RegionService>()
                   .AddScoped<IWineService, WineService>()
                   .AddScoped<IWineRepository, WineRepository>()
                   .AddScoped<IUserService, UserService>()
                   .BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory ilogger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

           /* app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
     
            });*/
        }
    }
}
