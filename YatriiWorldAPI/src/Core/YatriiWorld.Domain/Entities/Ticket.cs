using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Ticket : Base.BaseNameableEntitiy
    {
        public string CustomerFullName { get; set; }
        public string CustomerEmail { get; set; }
        public int PersonCount { get; set; }
        public decimal TotalPrice { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
