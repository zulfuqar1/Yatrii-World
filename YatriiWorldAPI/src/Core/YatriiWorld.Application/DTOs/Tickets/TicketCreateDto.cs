using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Tickets
{
    public class TicketCreateDto
    {
        public string CustomerFullName { get; set; }
        public string CustomerEmail { get; set; }
        public int AdultCount { get; set; }
        public int ChildrenCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int TourId { get; set; }
    }
}
