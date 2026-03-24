using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Ticket : Base.BaseAccountableEntity
    {
        public string CustomerFullName { get; set; }
        public string CustomerEmail { get; set; }
        public int AdultCount { get; set; }
        public int ChilderenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ChecikinDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
