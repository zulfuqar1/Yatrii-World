using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class BasketItem : Base.BaseAccountableEntity
    {
        public long BasketId { get; set; }
        public Basket Basket { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
