using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;

namespace Sulmar.EFCore.DbEFRepositories
{
    public class DbCustomerRepository : ICustomerRepository
    {
        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Get(string pesel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
