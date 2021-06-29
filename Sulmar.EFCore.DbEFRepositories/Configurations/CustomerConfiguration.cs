using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
