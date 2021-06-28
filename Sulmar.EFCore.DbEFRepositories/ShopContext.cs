using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.DbEFRepositories.Configurations;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sulmar.EFCore.DbEFRepositories
{

    // dotnet package Microsoft.EntityFrameworkCore
    // dotnet package Microsoft.EntityFrameworkCore.Relational
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        // public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // Fluent Api

            // modelBuilder.ApplyConfiguration(new ItemConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


    }
}
