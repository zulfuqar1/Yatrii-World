using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities.Base
{
    internal class BaseNameableEntitiy:BaseAccountableEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
