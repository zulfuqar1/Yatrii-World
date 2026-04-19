using System.ComponentModel.DataAnnotations;

namespace YatriiWorld.MVC.ViewModels.Tours
{
    public class TourTagCreateVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Name must be greater than 2 characters.")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string Name { get; set; }
    }
}
