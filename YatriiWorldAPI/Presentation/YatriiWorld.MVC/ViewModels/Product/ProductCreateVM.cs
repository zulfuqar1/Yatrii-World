using System.ComponentModel.DataAnnotations;
using YatriiWorld.MVC.ViewModels.Categories;

namespace YatriiWorld.MVC.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Disclosure is mandatory.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a category")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Price is mandatory")]
        [Range(0.1, 100000, ErrorMessage = "Please enter a valid price.")]

        public decimal Price { get; set; }
        public List<long> SelectedTagIds { get; set; } = new List<long>();
        public List<ProductTagVM> AvailableTags { get; set; } = new List<ProductTagVM>();
        public List<CategoryDetailsVM>? AvailableCategories { get; set; }

        [Range(0, 10000, ErrorMessage = "Stok cant be negative")]
        public int StockQuantity { get; set; }
        public List<IFormFile>? UploadedImages { get; set; }

    }
}
