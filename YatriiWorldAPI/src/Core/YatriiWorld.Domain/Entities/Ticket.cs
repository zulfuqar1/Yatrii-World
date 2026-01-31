using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Ticket : Base.BaseNameableEntitiy
    {
    
        public string TicketCode { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
