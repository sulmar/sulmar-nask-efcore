using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.DbEFRepositories.Configurations;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

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


            // Custom convention
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.conventions?view=efcore-5.0

            var properties = modelBuilder.Properties<string>()
                      .Where(p => p.Name.Contains("Name"));

            foreach (var property in properties)
            {
                property.SetMaxLength(100);
                property.IsNullable = false;                
            }

            // Fluent Api

            // modelBuilder.ApplyConfiguration(new ItemConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


    }

    public static class ModelBuilderExtensions
    {
        public static IEnumerable<IMutableProperty> Properties(this ModelBuilder modelBuilder)
        {
            var properties = from e in modelBuilder.Model.GetEntityTypes()
                             from p in e.GetProperties()                             
                             select p;

            return properties;
        }

        public static IEnumerable<IMutableProperty> Properties<T>(this ModelBuilder modelBuilder)
             => modelBuilder.Properties().Where(p => p.PropertyInfo.PropertyType == typeof(T));
        
    }
}
