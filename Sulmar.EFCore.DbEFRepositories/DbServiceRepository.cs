using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sulmar.EFCore.IRepositories;
using Sulmar.EFCore.Models;
using Sulmar.EFCore.Models.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulmar.EFCore.DbEFRepositories
{
    public class DbServiceRepository : DbEntityRepository<Service>, IServiceRepository
    {
        public DbServiceRepository(ShopContext context) : base(context)
        {
        }

        public override IEnumerable<Service> Get()
        {
           string sql = "SELECT * FROM Items WHERE [Discriminator] = 'Service'";

            // string sql = "EXECUTE dbo.uspGetServices";

            return entities.FromSqlRaw(sql).OrderBy(p=>p.Duration).ToList();
        }


        // https://docs.microsoft.com/pl-pl/ef/core/querying/raw-sql

        public IEnumerable<Service> Get(ServiceSearchCriteria searchCriteria)
        {
            // api/services?from=100';DROP TABLE Users;
            
            // SQL Injection!
            // string sql = "SELECT * FROM Items WHERE [Discriminator] = 'Service' AND UnitPrice > " + serviceSearch.UnitPriceFrom;


            string sql = "SELECT * FROM Items WHERE [Discriminator] = 'Service' AND UnitPrice >= {0} and UnitPrice =< {1}";

            return entities.FromSqlRaw(sql, searchCriteria.UnitPriceFrom, searchCriteria.UnitPriceTo).ToList();


            string sql2 = "SELECT * FROM Items WHERE [Discriminator] = 'Service' AND UnitPrice >= @from and UnitPrice =< @to";

            var fromParameter = new SqlParameter("from", searchCriteria.UnitPriceFrom);
            var toParameter = new SqlParameter("to", searchCriteria.UnitPriceTo);

            return entities.FromSqlRaw(sql, fromParameter, toParameter).ToList();


            FormattableString sql3 = $"SELECT * FROM Items WHERE [Discriminator] = 'Service' AND UnitPrice >= {searchCriteria.UnitPriceFrom} and UnitPrice =< {searchCriteria.UnitPriceTo}";

            return entities.FromSqlInterpolated(sql3);
        }
    }


}
