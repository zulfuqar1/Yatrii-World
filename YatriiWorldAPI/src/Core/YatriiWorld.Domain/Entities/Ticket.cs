namespace YatriiWorld.Domain.Entities
{
    public class Ticket : Base.BaseAccountableEntity
    {
        public string CustomerFullName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }


        public long AppUserId { get; set; }

        public AppUser AppUser { get; set; }



        public long TourId { get; set; }
        public Tour Tour { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }

 
        public int TravellerCount { get; set; } 
        public decimal TotalPrice { get; set; }

        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; } 
        public DateTime? PaymentDate { get; set; }

        public ICollection<TicketTraveler> Travellers { get; set; }
    }
}