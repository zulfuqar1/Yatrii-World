using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.DiscountedPrice).HasColumnType("decimal(18,2)");

            builder.HasIndex(p => p.SKU).IsUnique();

            builder.HasIndex(p => p.Slug).IsUnique();
        }
    }

    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.HasIndex(pr => new { pr.ProductId, pr.UserId }).IsUnique();
        }
    }
}