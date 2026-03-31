using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.DTOs.Tickets
{
    public class TicketCreateDto
    {
        public long TourId { get; set; }
        public int TotalPersonCount { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public long AppUserId { get; set; }


        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public List<TicketTravelerDto> Travelers { get; set; }
    }
}
