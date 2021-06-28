using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.DbEFRepositories;
using Sulmar.EFCore.Fakers;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;

namespace Sulmar.EFCore.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CreateDatabaseTest();

            AddCustomersTest();


        }

        private static void AddCustomersTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            var context = shopContextFactory.CreateDbContext(null);

            var customerFaker = new CustomerFaker();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customers = customerFaker.Generate(100);

            customerRepository.Add(customers);

        }

        private static ShopContext Create()
        {
            
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NaskShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Application Name=Shop";

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();

            var context = new ShopContext(optionsBuilder.Options);

            return context;
        }

        private static void CreateDatabaseTest()
        {
            var context = Create();

           // context.Database.EnsureCreated();

             context.Database.Migrate();
        }
    }
}
