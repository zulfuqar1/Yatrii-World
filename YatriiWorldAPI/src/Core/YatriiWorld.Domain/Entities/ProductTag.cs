using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Domain.Entities
{
    public class ProductTag : Base.BaseNameableEntity
    {

        public ICollection<Product> Product { get; set; } = new List<Product>();

    }
}
