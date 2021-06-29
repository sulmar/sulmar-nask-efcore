using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;

namespace Sulmar.EFCore.DbEFRepositories
{
    public class DbServiceRepository : DbEntityRepository<Service>, IServiceRepository
    {
        public DbServiceRepository(ShopContext context) : base(context)
        {
        }
    }
}
