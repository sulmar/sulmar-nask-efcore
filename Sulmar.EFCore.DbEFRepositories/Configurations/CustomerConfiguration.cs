using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sulmar.EFCore.Models;

namespace Sulmar.EFCore.DbEFRepositories.Configurations
{



    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.FirstName)
            //    .HasMaxLength(50);

            //builder
            //    .Property(p => p.LastName)
            //    .HasMaxLength(50);

            builder
                .Property(p => p.Pesel)
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(p => p.Pesel);

            // builder.HasData()            

            builder.Property(p => p.NIP)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.OwnsOne(p => p.InvoiceAddress);

             builder.OwnsOne(p => p.ShipAddress);

            // Konwerter za pomocą wyrażeń
            //builder.Property(p => p.Location)
            //    .HasConversion(
            //        v => v.ToGeoHash(),
            //        v => new Coordinate(v));


            // Konwerter za pomocą klasy
            //builder.Property(p => p.Location)
            //    .HasConversion(new GeoHashConverter());

            // Konwerter za pomocą metody rozszerzającej
            builder.Property(p => p.Location)
                .HasGeoHashValueConversion();
        }
    }


    // Wbudowane konwertery
    // https://docs.microsoft.com/pl-pl/ef/core/modeling/value-conversions?tabs=data-annotations#built-in-converters

    public class GeoHashConverter : ValueConverter<Coordinate, string>
    {
        public GeoHashConverter()
            : base(v => v.ToGeoHash(), 
                  v=>new Coordinate(v))
        {

        }
    }

    public static class GeoHashExtensions
    {
        public static PropertyBuilder HasGeoHashValueConversion(this PropertyBuilder propertyBuilder)
        {
            propertyBuilder.HasConversion(new GeoHashConverter());

            return propertyBuilder;
        }
    }
}
