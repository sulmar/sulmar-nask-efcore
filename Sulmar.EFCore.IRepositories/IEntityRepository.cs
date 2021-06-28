using Sulmar.EFCore.Models;
using System.Collections.Generic;

namespace Sulmar.EFCore.IRepositories
{
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
    }
}
