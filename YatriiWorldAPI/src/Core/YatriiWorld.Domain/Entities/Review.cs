using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Review : Base.BaseNameableEntitiy
    {
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
