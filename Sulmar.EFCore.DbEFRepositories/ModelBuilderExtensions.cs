using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sulmar.EFCore.DbEFRepositories
{
    public static class ModelBuilderExtensions
    {
        public static IEnumerable<IMutableProperty> Properties(this ModelBuilder modelBuilder)
        {
            var properties = from e in modelBuilder.Model.GetEntityTypes()
                             from p in e.GetProperties()                             
                             select p;

            return properties;
        }

        public static IEnumerable<IMutableProperty> Properties<T>(this ModelBuilder modelBuilder)
             => modelBuilder.Properties().Where(p => p.PropertyInfo.PropertyType == typeof(T));
        
    }
}
