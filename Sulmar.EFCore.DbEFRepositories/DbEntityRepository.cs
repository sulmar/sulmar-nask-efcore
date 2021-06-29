using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sulmar.EFCore.DbEFRepositories
{
    public class DbEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly ShopContext _context;

        public DbEntityRepository(ShopContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> entities => _context.Set<TEntity>();

        public virtual void Add(TEntity entity)
        {
            Trace.WriteLine(_context.Entry(entity).State);

            entities.Add(entity);

            Trace.WriteLine(_context.Entry(entity).State);

            _context.SaveChanges();

            Trace.WriteLine(_context.Entry(entity).State);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities);
            _context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return entities.ToList();
        }

        public virtual TEntity Get(int id)
        {
            TEntity entity = entities.Find(id);

            Trace.WriteLine(_context.Entry(entity).State);

            return entity;
        }

        public virtual void Remove(int id)
        {
            // var entity = Get(id);

            var entity = new TEntity() { Id = id };

            Trace.WriteLine(_context.Entry(entity).State);

            // entities.Remove(entity);

            entities.Attach(entity);

            Trace.WriteLine(_context.Entry(entity).State);

            _context.Entry(entity).State = EntityState.Deleted;

            Trace.WriteLine(_context.Entry(entity).State);

            _context.SaveChanges();

            Trace.WriteLine(_context.Entry(entity).State);
        }

        public void Update(TEntity entity)
        {
            Trace.WriteLine(_context.Entry(entity).State);

            //  entities.Update(entity);

            _context.Entry(entity).State = EntityState.Modified;


            Trace.WriteLine(_context.Entry(entity).State);

            var IsModifiedOnModified = _context.Entry(entity).Property(p => p.ModifiedOn).IsModified;

            _context.SaveChanges();

            Trace.WriteLine(_context.Entry(entity).State);
        }
    }
}
