using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class ProductImage : Base.BaseAccountableEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
