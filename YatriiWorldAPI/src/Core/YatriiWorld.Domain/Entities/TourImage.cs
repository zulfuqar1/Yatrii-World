using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class TourImage : Base.BaseNameableEntity
    {
        public string ImageUrl { get; set; }
        public long? TourId { get; set; }

        public bool? IsMain { get; set; }
        public Tour Tour { get; set; }
    }
}
