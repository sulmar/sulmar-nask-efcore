using Sulmar.EFCore.Models;
using Sulmar.EFCore.Models.SearchCriterias;
using System.Collections.Generic;

namespace Sulmar.EFCore.IRepositories
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> Get(ProductSearchCritieria searchCritieria);

    }
}
