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
    internal class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {

            // 1. Primary Key (Adətən avtomatik təyin olunur, amma dəqiqləşdirmək yaxşıdır)
            builder.HasKey(t => t.Id);

            // 2. Name - Mütləq olmalıdır və maksimum 150 simvol
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            // 3. Description - Mütləq olmalıdır və daha uzun ola bilər (məs: 1000 simvol)
            //builder.Property(t => t.Description)
            //       .IsRequired()
            //       .HasMaxLength(1000);

            // 4. Price - Qiymət sütunu (18 rəqəm, vergüldən sonra 2 rəqəm)
            builder.Property(t => t.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // 5. Relationships (Əlaqələr) - Ən vacib hissə!
            // Bir turun BİR kateqoriyası var, bir kateqoriyanın ÇOXLU turu ola bilər.
            builder.HasOne(t => t.Category)
                   .WithMany(c => c.Tours)
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            // Restrict: Kateqoriya silinəndə içindəki turlar avtomatik silinməsin (təhlükəsizlik üçün)
        }
    }
}
