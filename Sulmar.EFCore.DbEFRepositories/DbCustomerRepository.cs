using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Sulmar.EFCore.DbEFRepositories
{

    public class DbCustomerRepository : DbEntityRepository<Customer>, ICustomerRepository
    {
        public DbCustomerRepository(ShopContext context) : base(context)
        {
            
        }

        public Customer Get(string pesel)
        {
            // wyłączenie filtru globalnego
            return entities.IgnoreQueryFilters().SingleOrDefault(c => c.Pesel == pesel);
        }

        public override void Remove(int id)
        {
            // Customer customer = Get(id);

            Customer customer = new Customer { Id = id };
            Trace.WriteLine(_context.Entry(customer).State);

            customer.IsRemoved = true;

            Trace.WriteLine(_context.Entry(customer).State);

            // _context.Entry(customer).State = EntityState.Modified;

            _context.Entry(customer).Property(p => p.IsRemoved).IsModified = true;

            Trace.WriteLine(_context.Entry(customer).State);

            _context.SaveChanges();

            Trace.WriteLine(_context.Entry(customer).State);

        }

    }
}
