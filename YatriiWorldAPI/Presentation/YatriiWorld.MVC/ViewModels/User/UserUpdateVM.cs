namespace YatriiWorld.MVC.ViewModels.User
{
    public class UserUpdateVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Bio { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
