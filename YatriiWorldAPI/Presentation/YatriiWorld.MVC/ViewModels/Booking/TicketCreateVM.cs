using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.MVC.ViewModels.Booking
{
    public class TicketCreateVM
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

        public List<TravelerVM> Travelers { get; set; }
    }
}
