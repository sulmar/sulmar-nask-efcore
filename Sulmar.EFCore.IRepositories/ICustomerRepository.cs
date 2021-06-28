using Sulmar.EFCore.Models;
using System;

namespace Sulmar.EFCore.IRepositories
{

    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Customer Get(string pesel);
    }
}
