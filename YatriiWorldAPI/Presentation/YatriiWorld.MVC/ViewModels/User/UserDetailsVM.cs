using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Domain.Entities.Enums;
using YatriiWorld.MVC.ViewModels.Booking;
using YatriiWorld.MVC.ViewModels.Review;

namespace YatriiWorld.MVC.ViewModels.User
{
    public class UserDetailsVM
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string?   Region { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public UserRole Role { get; set; }
        public List<TicketListVM> Tickets { get; set; }
        public List<ReviewListVM> Reviews { get; set; }
    }
}
