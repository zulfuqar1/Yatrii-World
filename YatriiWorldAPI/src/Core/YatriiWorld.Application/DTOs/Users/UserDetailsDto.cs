using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Domain.Entities.Enums;

namespace YatriiWorld.Application.DTOs.Users
{
    public class UserDetailsDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Bio { get; set; }
        public string ProfileImageUrl { get; set; }
        public UserRole Role { get; set; }
        public List<TicketListDto> Tickets { get; set; }
        public List<ReviewListDto> Reviews { get; set; }
    }
}
