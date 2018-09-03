//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Wine.WebAPI
//{
//    public class Context: DbContext
//    {
//        public Context(DbContextOptions <Context> options): base(options)
//        {
            
//        }

//        // tables for the database

//        public DbSet<Wine.Data.Wine> Wines { get; set; }

//        public DbSet<Wine.Data.Country> Countries { get; set; }

//        public DbSet<Wine.Data.Region> Regions { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)  
//        {
//            modelBuilder.Entity< Wine.Data.Wine>().ToTable("Wines");

//            modelBuilder.Entity<Wine.Data.Country>().ToTable("Countries");

//            modelBuilder.Entity<Wine.Data.Region>().ToTable("Regions");
//        }
//    }
//}
