using Sulmar.EFCore.Models;
using Sulmar.EFCore.Models.SearchCriterias;
using System;
using System.Collections.Generic;

namespace Sulmar.EFCore.IRepositories
{

    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Customer Get(string pesel);
    }

    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> Get(ProductSearchCritieria searchCritieria);

    }

    public interface IServiceRepository : IEntityRepository<Service>
    {

    }

    public interface IOrderRepository : IEntityRepository<Order>
    {

    }
}
