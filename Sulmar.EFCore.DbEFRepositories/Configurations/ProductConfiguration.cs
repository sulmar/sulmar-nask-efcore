using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;
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
        }
    }
}
