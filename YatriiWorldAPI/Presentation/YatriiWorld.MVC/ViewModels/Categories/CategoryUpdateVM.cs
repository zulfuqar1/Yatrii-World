using System.ComponentModel.DataAnnotations;

namespace YatriiWorld.MVC.ViewModels.Categories
{
    public class CategoryUpdateVM
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Name must be greater than 2 characters.")]
        [MaxLength(150, ErrorMessage = "Name must be less than 150 characters.")]
        [RegularExpression(@"^[a-zA-ZəƏıİöÖüÜşŞçÇğĞ\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }
    }
}
