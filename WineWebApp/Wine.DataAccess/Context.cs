using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Wine.Data;

namespace Wine.DataAccess
{
    public class Context : DbContext
    {
        public IConfiguration Configuration { get; }

        public Context() : base()
        {

        }
        public Context(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var conectionString = configuration.GetConnectionString("DefaultConnection");           
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                    .UseSqlServer("Data Source=DESKTOP-23IAU6E\\SQLSERVER;Initial Catalog=Wine;Integrated Security=False;User Id=sa;Password=Manchester2018!;MultipleActiveResultSets=True");
            }
        }

        // tables for the database

        public DbSet<Wine.Data.Wine> Wines { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Region> Regions { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Wine.Data.Wine>().ToTable("Wines");

        //    modelBuilder.Entity<Wine.Data.Country>().ToTable("Countries");

        //    modelBuilder.Entity<Wine.Data.Region>().ToTable("Regions");
        //}
    }
}
