using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities.Base
{
    public abstract class BaseEntitiy
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
