using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sulmar.EFCore.DbEFRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.EFCore.ConsoleClient
{
    public class ShopContextFactory : IDesignTimeDbContextFactory<ShopContext>
    {
        public ShopContext CreateDbContext(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NaskShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Application Name=Shop";

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();

            var context = new ShopContext(optionsBuilder.Options);

            return context;
        }
    }
}
