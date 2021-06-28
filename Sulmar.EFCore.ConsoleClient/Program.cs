using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.DbEFRepositories;
using System;

namespace Sulmar.EFCore.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CreateDatabaseTest();
        }

        private static void CreateDatabaseTest()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NaskShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Application Name=Shop";

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();

            var context = new ShopContext(optionsBuilder.Options);

            context.Database.EnsureCreated();

            // context.Database.Migrate();
        }
    }
}
