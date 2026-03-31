namespace YatriiWorld.Domain.Entities
{
    public class TicketTraveler : Base.BaseAccountableEntity
    {
        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }  
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public bool IsMainTraveller { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}