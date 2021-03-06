using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.DbEFRepositories;
using Sulmar.EFCore.Fakers;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.EF;

namespace Sulmar.EFCore.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello EF Core 3.1!");

            Setup();

            GetProductsTest();

            GetCustomersTest();


            // UpdateCustomerTest();

            // AddCustomerTest();

            // RemoveCustomerTest();

            // AddProductsTest();

            //  AddServicesTest();



            //   GetOrderTest();

            // TrackGraphTest();

            // AddOrderTest();

            // DisableAutoDetectedTest();

            // GlobalAsNoTrackingTest();

            // PropertyChangedStrategyTest();

            // GetOrderLazyLoadingTest();

            // GetILazyLoaderTest();


            // ExpliciteLoadingTest();

            // AddCustomersTest();

            // GetCustomerTest();


            // ShadowPropertyUpdateTest();

            // ShadowPropertyQueryTest();

            // MaterializedTest();

            // TransactionNativeTest();

            // TransactionDistributedTest();

            // ConcurrencyTokenTest();

            // ConcurrencyTokenTest2();

            // GetCustomersTest();

            // GetServicesTest();

            // GetFunctionTest();

        }

        private static void Setup()
        {
            var context = Create();

            Console.WriteLine("Connecting to database...");

            if (context.Database.CanConnect())
            {
                Console.WriteLine("Connected.");
            }
            else
            {
                Console.WriteLine("Creating database...");

                CreateDatabaseTest();

                Console.WriteLine("Created.");

                SeedData();
            }
        }

        private static void SeedData()
        {
            Console.WriteLine("Preparing data...");

            
            AddCustomersTest();

            Console.WriteLine("Customers...");
            AddProductsTest();
            AddServicesTest();
        }

        private static void GetFunctionTest()
        {
            var context = Create();

            var query = context.Customers
                .Where(p => context.CountCustomers(p.IsRemoved) > 0)
                .ToList();
        }

        private static void GetServicesTest()
        {
            var context = Create();

            IServiceRepository serviceRepository = new DbServiceRepository(context);

            var services = serviceRepository.Get();
        }

        private static void GetCustomersTest()
        {
            var context = Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customers = customerRepository.Get();
            Display(customers);

            if (customers.Any(p => p.IsRemoved))
            {

            }

            var customer = customerRepository.Get("80063097764");
        }

        private static void Display(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FullName}");
            }
        }

        private static void ConcurrencyTokenTest2()
        {
            var context1 = Create();
            var context2 = Create();

            var customer1 = context1.Customers.Find(10);
            customer1.FirstName = "John";
            customer1.LastName = "Smith";
            customer1.Amount += 1000;

            var customer2 = context2.Customers.Find(10);
            customer1.FirstName = "Ann";
            customer1.LastName = "Novak";
            customer1.Amount += 50;
            customer1.CustomerType = CustomerType.Company;

            context2.SaveChanges();

            // ...

            bool isSaved = false;

            while (!isSaved)
            {

                try
                {
                    context1.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine("Cena została w międzyczasie zmodyfikowana.");

                    var entity = e.Entries.First();

                    entity.Reload();

                    Thread.Sleep(TimeSpan.FromSeconds(1));

                }

                context1.SaveChanges();

                isSaved = true;
            }
        }

        private static void ConcurrencyTokenTest()
        {
            
            var context1 = Create();
            var context2 = Create();

            var product1 = context1.Products.Find(10);
            product1.UnitPrice = 150;
            product1.Color = "Black";

            var product2 = context2.Products.Find(10);
            product2.UnitPrice = 50;
            product2.Color = "Red";

            context2.SaveChanges();

            // ...

            try
            {
                context1.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                Console.WriteLine("Cena została w międzyczasie zmodyfikowana.");

                var entity = e.Entries.First();

                entity.Reload();

            }


        }

        // https://docs.microsoft.com/pl-pl/dotnet/api/system.transactions.transactionscope?view=net-5.0
        private static void TransactionDistributedTest()
        {
            decimal amount = 100;

            var senderContext = Create();
            var recipientContext = Create();

            using (TransactionScope transaction = new TransactionScope())
            {
                Console.WriteLine("Rozpoczęto transakcję...");

                try
                {
                    var sender = senderContext.Customers.Find(106);
                    sender.Amount -= amount;

                    // throw new Exception();

                    var recipient = recipientContext.Customers.Find(118);
                    recipient.Amount += amount;

                    senderContext.SaveChanges();
                    recipientContext.SaveChanges();

                    transaction.Complete(); // To Commit

                    Console.WriteLine("Zatwierdzono transakcję.");
                }
                catch (Exception e)
                {

                    Console.WriteLine("Wycofano transakcję.");
                }
            } // <-- commit / rollback zależnie od flagi Complete
        }

        private static void TransactionNativeTest()
        {
            decimal amount = 100;

            var context = Create();


            // Poziomy izolacji
            // https://docs.microsoft.com/en-us/sql/connect/jdbc/understanding-isolation-levels?view=sql-server-ver15#remarks

            using (var transaction = context.Database.BeginTransaction()) // BEGIN TRAN
            {
                Console.WriteLine("Rozpoczęto transakcję...");

                try
                {
                    var sender = context.Customers.Find(106);
                    sender.Amount -= amount;

                    context.SaveChanges();

                    // throw new Exception();

                    var recipient = context.Customers.Find(118);
                    recipient.Amount += amount;

                    context.SaveChanges();

                    transaction.Commit(); // COMMIT

                    Console.WriteLine("Zatwierdzono transakcję.");
                }
                catch (Exception e)
                {
                    transaction.Rollback(); // ROLLBACK

                    Console.WriteLine("Wycofano transakcję.");
                }
            }





        }

        private static void MaterializedTest()
        {
            var context = Create();

            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.tracked?view=efcore-3.1
            context.ChangeTracker.Tracked += ChangeTracker_Tracked;

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customers = customerRepository.Get();
        }

        private static void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            if (e.FromQuery && e.Entry.Entity is Customer customer)
            {
                customer.FirstName += "!";
            }
        }

        private static void ShadowPropertyQueryTest()
        {
            var context = Create();

            var customers = context.Customers.OrderByDescending(p => Property<DateTime?>(p, "LastUpdated")).ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine(context.Entry(customer).Property("LastUpdated").CurrentValue);
            }
        }

        private static void ShadowPropertyUpdateTest()
        {
            var context = Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customer = customerRepository.Get(102);

            context.Entry(customer).Property("LastUpdated").CurrentValue = DateTime.UtcNow;

            context.SaveChanges();
        }

        private static void GetCustomerTest()
        {
            var context = Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customer = customerRepository.Get(102);

            Console.WriteLine($"{customer.Location.Latitude} : {customer.Location.Longitude}");
        }

        private static void ExpliciteLoadingTest()
        {
            var context = Create();

            IOrderRepository orderRepository = new DbOrderRepository(context);

            var order = orderRepository.Get(5);
        }

        private static void GetILazyLoaderTest()
        {
            var context = Create();

            IOrderRepository orderRepository = new DbOrderRepository(context);

            var order = orderRepository.Get(5);
        }

        private static void PropertyChangedStrategyTest()
        {
            var context = Create();

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            Product product = context.Products.Find(10);

            Trace.WriteLine(context.Entry(product).State);

            // product.UnitPrice += 1;
            product.Color = "Red";

            Trace.WriteLine(context.Entry(product).State);


        }

        private static void GlobalAsNoTrackingTest()
        {
            var context = Create();

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;


        }

        private static void DisableAutoDetectedTest()
        {
            var context = Create();

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            var customer = new Customer { Id = 10 };

            Trace.WriteLine(context.Entry(customer).State);

            context.Customers.Attach(customer);

            Trace.WriteLine(context.Entry(customer).State);

            customer.FirstName = "Marcin";

            Trace.WriteLine(context.Entry(customer).State);

            context.ChangeTracker.DetectChanges();

            Trace.WriteLine(context.Entry(customer).State);

        }

        private static void GetOrderTest()
        {
            var context = Create();

            IOrderRepository orderRepository = new DbOrderRepository(context);

            var order = orderRepository.Get(5);
        }

        private static void GetOrderLazyLoadingTest()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NaskShopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Application Name=Shop";

            // dotnet add package Microsoft.EntityFrameworkCore.Proxies

            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies();

            var context = new ShopContext(optionsBuilder.Options);

            IOrderRepository orderRepository = new DbOrderRepository(context);

            var order = orderRepository.Get(5);
        }

        private static void GetProductsTest()
        {
            var context = Create();
            IProductRepository productRepository = new DbProductRepository(context);

            var products = productRepository.Get();

            Display(products);
        }

        private static void Display(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} {product.BarCode}");
            }
        }

        private static void TrackGraphTest()
        {
            var context = Create();

            IOrderRepository orderRepository = new DbOrderRepository(context);

            var customer = new Customer { Id = 10 };

            var product1 = new Product { Id = 11 };

            var product2 = new Product { Id = 12 };

            var service = new Service { Id = 25 };

            var order = new Order
            {
                Customer = customer,
            };

            order.Details.Add(new OrderDetail { Item = product1, Quantity = 1, UnitPrice = product1.UnitPrice });
            order.Details.Add(new OrderDetail { Item = product2, Quantity = 5, UnitPrice = product2.UnitPrice });
            order.Details.Add(new OrderDetail { Item = product1, Quantity = 10, UnitPrice = 9.99m });
            order.Details.Add(new OrderDetail { Item = service, Quantity = 1, UnitPrice = service.UnitPrice });

            // Strategia
            context.ChangeTracker.TrackGraph(order, e =>
            {
                if (e.Entry.IsKeySet)
                {
                    e.Entry.State = EntityState.Unchanged;
                }
                else
                {
                    e.Entry.State = EntityState.Added;
                }
            });

            orderRepository.Add(order);

        }

        private static void AddOrderTest()
        {
            var context = Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);
            IProductRepository productRepository = new DbProductRepository(context);
            IServiceRepository serviceRepository = new DbServiceRepository(context);
            IOrderRepository orderRepository = new DbOrderRepository(context);

            var customer = customerRepository.Get(10);

            var product1 = productRepository.Get(11);
            var product2 = productRepository.Get(12);

            var service = serviceRepository.Get(25);

            var order = new Order
            {
                Customer = customer,
            };

            order.Details.Add(new OrderDetail { Item = product1, Quantity = 1, UnitPrice = product1.UnitPrice });
            order.Details.Add(new OrderDetail { Item = product2, Quantity = 5, UnitPrice = product2.UnitPrice });
            order.Details.Add(new OrderDetail { Item = service, Quantity = 1, UnitPrice = service.UnitPrice });


            orderRepository.Add(order);

        }

        private static void AddProductsTest()
        {
            Console.WriteLine("Generating products...");

            var products = new ProductFaker().Generate(20);

            var context = Create();
            IProductRepository productRepository = new DbProductRepository(context);
            productRepository.Add(products);

            Console.WriteLine($"Added {products.Count} products.");
        }

        private static void AddServicesTest()
        {
            Console.WriteLine("Generating services...");

            var services = new ServiceFaker().Generate(10);

            var context = Create();
            IServiceRepository serviceRepository = new DbServiceRepository(context);
            serviceRepository.Add(services);

            Console.WriteLine($"Added {services.Count} services.");
        }

        private static void AddCustomerTest()
        {
            var context = Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            Customer customer = new CustomerFaker(new CoordinateFaker(), new AddressFaker()).Generate();


            customerRepository.Add(customer);

        }

        private static void RemoveCustomerTest()
        {
            var context = Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            customerRepository.Remove(4);

        }

        private static void UpdateCustomerTest()
        {
            var context = Create();

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            var customer = customerRepository.Get(10);

            // customer.FirstName = "John";

            customer.IsRemoved = !customer.IsRemoved;
            customer.ModifiedOn = DateTime.UtcNow;

            customerRepository.Update(customer);

        }

        private static void AddCustomersTest()
        {
            ShopContextFactory shopContextFactory = new ShopContextFactory();
            var context = shopContextFactory.CreateDbContext(null);

            var customerFaker = new CustomerFaker(new CoordinateFaker(), new AddressFaker());

            ICustomerRepository customerRepository = new DbCustomerRepository(context);

            Console.WriteLine("Generating customers...");

            var customers = customerFaker.Generate(100);

            customerRepository.Add(customers);

            Console.WriteLine($"Added {customers.Count} customers.");

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
