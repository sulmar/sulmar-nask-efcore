using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Sulmar.EFCore.DbEFRepositories.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
               .Property(p => p.BarCode)
               .HasMaxLength(13)
               .IsUnicode(false);

            //builder.Property(p => p.Parameters)
            //    .HasConversion(
            //        v => JsonConvert.SerializeObject(v),
            //        v => JsonConvert.DeserializeObject<ProductParameters>(v));

            builder.Property(p => p.Parameters)
                .HasJsonValueConversion();

            builder.Property(p => p.UnitPrice)
                .IsConcurrencyToken();

            builder.Property(p => p.Color)
                .IsConcurrencyToken();
        }
    }

    public class JsonValueConverter<T> : ValueConverter<T, string> 
    {
        public JsonValueConverter()
            : base(v=>JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<T>(v))
        {

        }
    }

    public static class JsonExtensions
    {
        public static PropertyBuilder<T> HasJsonValueConversion<T> (this PropertyBuilder<T> propertyBuilder)
        {
            propertyBuilder.HasConversion(new JsonValueConverter<T>());

            return propertyBuilder;
        }
    }
}
