namespace YatriiWorld.MVC.ViewModels.Booking
{
    public class TicketListVM
    {
        public long Id { get; set; }
        public string UserFullName { get; set; }
        public string TourName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsDeleted { get; set; }
    }
}
