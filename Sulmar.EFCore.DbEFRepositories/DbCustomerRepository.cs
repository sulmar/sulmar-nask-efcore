using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System;
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
            
            return entities.SingleOrDefault(c => c.Pesel == pesel);
        }

      
    }
}
