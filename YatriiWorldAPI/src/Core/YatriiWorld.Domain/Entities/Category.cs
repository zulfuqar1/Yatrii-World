using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Category:Base.BaseNameableEntitiy
    {
        public string Name { get; set; }

        public ICollection<Tour> Tours { get; set; }
    }
}