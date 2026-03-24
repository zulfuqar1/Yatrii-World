using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistance.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
           builder.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

        }
    }
}
