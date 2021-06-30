using Sulmar.EFCore.Models;
using Sulmar.EFCore.Models.SearchCriterias;
using System.Collections.Generic;

namespace Sulmar.EFCore.IRepositories
{
    public interface IServiceRepository : IEntityRepository<Service>
    {
        IEnumerable<Service> Get(ServiceSearchCriteria serviceSearch);
    }
}
