using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Sulmar.EFCore.DbEFRepositories
{
    public class DbOrderRepository : DbEntityRepository<Order>, IOrderRepository
    {
        public DbOrderRepository(ShopContext context) : base(context)
        {
        }

        public override void Add(Order entity)
        {
            _context.Orders.Add(entity);

            var entities = _context.ChangeTracker.Entries();

            var items = entities.Select(e => new { e.Metadata, e.State }).ToList();

            _context.SaveChanges();
        }

        // Eager loading (zachłanne)
        //public override Order Get(int id)
        //{

        //    Order order = _context.Orders
        //        .Include(p=>p.Details).ThenInclude(x=>x.Item)
        //        .Include(p=>p.Customer.Where(c=>!c.IsRemoved))
        //        .AsNoTracking()
        //        .SingleOrDefault(p=>p.Id == id);

        //    return order;
        //}



        // Lazy Loading (leniwe ładowanie danych)
        //public override Order Get(int id)
        //{
        //    //if (_context.Orders.Any(p => p.Customer.CustomerType==CustomerType.Private))
        //    //{

        //    //}

        //    var order = _context.Orders.Find(id);

        //    Trace.WriteLine(order);

        //    Trace.WriteLine(order.Customer);

        //    // n + 1 queries
        //    foreach (var detail in order.Details)
        //    {
        //        Trace.WriteLine($"{detail.Quantity}");
        //    }

        //    return order;
        //}

        // Explicit loading
        public override Order Get(int id)
        {
            var order = _context.Orders.Find(id);

            // ...

            // Załadowanie pojedynczej encji
            _context.Entry(order).Reference(p => p.Customer).Load();


            _context.Entry(order).Collection(p => p.Details).Load();

            // nieprawidłowe, nie działa
            // _context.Entry(order).Collection(p => p.Details.Where(d=>d.Quantity>1)).Load();

            // prawidłowe
            _context.Entry(order).Collection(p => p.Details).Query().Where(d => d.Quantity > 1).Load();

            return order;


        }
    }
}
