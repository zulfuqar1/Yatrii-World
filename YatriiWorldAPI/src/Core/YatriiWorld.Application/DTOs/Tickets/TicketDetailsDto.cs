using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.DTOs.Tickets
{
    public class TicketDetailsDto
    {
        public long Id { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public long AppUserId { get; set; }

        public int AdultCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string TourName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
