using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistance.Configurations
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.Property(pt => pt.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(pt => pt.Product)
                   .WithMany(p => p.ProductTags)
                   .UsingEntity(j => j.ToTable("ProductProductTags")); 
        }
    }
}