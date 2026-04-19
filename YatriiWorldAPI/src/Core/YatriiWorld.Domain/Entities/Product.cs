using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities.Enums;

namespace YatriiWorld.Domain.Entities
{
    public class Product : Base.BaseNameableEntity
    {

        public long CategoryId { get; set; }
        public string Slug { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }

        public string SKU { get; set; } = string.Empty; 
        public int StockQuantity { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
      
        public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();

        public double Rating { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
