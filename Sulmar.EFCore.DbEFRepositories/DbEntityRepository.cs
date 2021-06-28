using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sulmar.EFCore.DbEFRepositories
{
    public class DbEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ShopContext _context;

        public DbEntityRepository(ShopContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> entities => _context.Set<TEntity>();

        public void Add(TEntity entity)
        {
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> Get()
        {
            return entities.ToList();
        }

        public TEntity Get(int id)
        {
            return entities.Find(id);
        }

        public void Remove(int id)
        {
            entities.Remove(Get(id));
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            entities.Update(entity);
            _context.SaveChanges();
        }
    }
}
