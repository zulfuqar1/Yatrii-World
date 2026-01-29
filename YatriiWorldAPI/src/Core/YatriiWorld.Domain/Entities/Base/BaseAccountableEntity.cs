using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities.Base
{
    public abstract class BaseAccountableEntity: BaseEntitiy
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; } ="admin";
    
    }
}
