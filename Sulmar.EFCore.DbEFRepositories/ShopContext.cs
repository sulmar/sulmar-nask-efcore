using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.DbEFRepositories.Configurations;
using Sulmar.EFCore.Models;
using System;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Sulmar.EFCore.DbEFRepositories
{

    /*
     -- funkcja skalarna 
     CREATE OR ALTER FUNCTION dbo.CountCustomers(@isRemoved bit)
     RETURNS int
      AS
      BEGIN
	    RETURN (SELECT count(*) from dbo.Customers WHERE IsRemoved = @isRemoved)
      END

    */

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
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.changetrackingstrategy?view=efcore-1.1&viewFallbackFrom=efcore-3.1
            // modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);

            base.OnModelCreating(modelBuilder);


            // Custom convention

            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.conventions?view=efcore-5.0
            //var properties = modelBuilder.Properties<string>()
            //          .Where(p => p.Name.Contains("Name"));

            //foreach (var property in properties)
            //{
            //    property.SetMaxLength(100);
            //    property.IsNullable = false;                
            //}

            // Fluent Api

            // modelBuilder.ApplyConfiguration(new ItemConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // https://docs.microsoft.com/pl-pl/ef/core/querying/user-defined-function-mapping
            modelBuilder.HasDbFunction(
                    typeof(ShopContext).GetMethod(nameof(CountCustomers), new[] { typeof(bool) })).HasName("CountCustomers");
                   


        }

        public int CountCustomers(bool isRemoved) => throw new NotSupportedException();

        public override int SaveChanges()
        {
            // Added
            var added = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e=>e.Entity)
                .OfType<BaseEntity>();

            foreach (var entity in added)
            {
                entity.CreatedOn = DateTime.Now;
            }


            // Modified
            var modified = this.ChangeTracker.Entries()
               .Where(e => e.State == EntityState.Modified)
               .Select(e => e.Entity)
               .OfType<BaseEntity>();

            foreach (var entity in modified)
            {
                entity.ModifiedOn = DateTime.Now;
            }


            // Shadow Property
            var updated = this.ChangeTracker.Entries()
              .Where(e => e.State == EntityState.Modified)
              .Where(e=>e.Entity.GetType() == typeof(Customer));

            foreach (var entity in updated)
            {
                entity.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }


    }
}
