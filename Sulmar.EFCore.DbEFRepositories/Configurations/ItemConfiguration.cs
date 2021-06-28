using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sulmar.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.EFCore.DbEFRepositories.Configurations
{
    class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
              .ToTable("Items", "dbo");
        }
    }
}
