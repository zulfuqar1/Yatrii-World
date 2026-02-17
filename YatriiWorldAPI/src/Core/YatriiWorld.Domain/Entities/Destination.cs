using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Destination : Base.BaseNameableEntitiy
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }

        // Bir məkana çoxlu turlar ola bilər
        public ICollection<Tour> Tours { get; set; }
    }
}
