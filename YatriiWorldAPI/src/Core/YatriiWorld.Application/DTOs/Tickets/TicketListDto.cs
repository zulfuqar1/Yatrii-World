using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Tickets
{
    public class TicketListDto
    {
        public long Id { get; set; }
        public string UserFullName { get; set; }
        public string TourName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsDeleted { get; set; }
    }
}