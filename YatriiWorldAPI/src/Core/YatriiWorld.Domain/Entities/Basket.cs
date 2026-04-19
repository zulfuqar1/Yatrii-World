using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Basket : Base.BaseAccountableEntity
    {
        public long UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}