using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using Sulmar.EFCore.Models.SearchCriterias;
using System.Collections.Generic;
using System.Linq;

namespace Sulmar.EFCore.DbEFRepositories
{
    public class DbProductRepository : DbEntityRepository<Product>, IProductRepository
    {
        public DbProductRepository(ShopContext context) : base(context)
        {
        }

        public override IEnumerable<Product> Get()
        {
            return entities.AsNoTracking().ToList();
        }


        // http://www.albahari.com/nutshell/predicatebuilder.aspx
        public IEnumerable<Product> Get(ProductSearchCritieria searchCritieria)
        {
            var query = _context.Products.AsQueryable();            

            if (!string.IsNullOrEmpty(searchCritieria.BarCode))
            {
                query = query.Where(p => p.BarCode == searchCritieria.BarCode);
            }

            if (!string.IsNullOrEmpty(searchCritieria.Color))
            {
                query = query.Where(p => p.Color == searchCritieria.Color);
            }

            if (searchCritieria.WeightFrom.HasValue)
            {
                query = query.Where(p => p.Weight >= searchCritieria.WeightFrom);
            }

            if (searchCritieria.WeightTo.HasValue)
            {
                query = query.Where(p => p.Weight >= searchCritieria.WeightTo);
            }

            var rawQuery = _context.Products.AsQueryable();

            if (query == rawQuery)
            {

            }

            return query.Take(1000).ToList();
        }
    }
}
