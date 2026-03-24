using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Category:Base.BaseNameableEntity
    {
        public ICollection<Tour> Tours { get; set; }
    }
}