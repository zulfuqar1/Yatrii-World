using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistance.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
     

        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder
             .Property(c => c.Name)
             .IsRequired()
             .HasColumnType("varchar(150)");

            builder
                .HasIndex(c => c.Name).IsUnique();
        }
    }
}
