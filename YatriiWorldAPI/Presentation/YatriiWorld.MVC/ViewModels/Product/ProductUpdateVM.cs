using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using YatriiWorld.MVC.ViewModels.Categories;
using YatriiWorld.MVC.ViewModels.Product;

namespace YatriiWorld.MVC.ViewModels.Product 
{
    public class ProductUpdateVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }

        public List<long>? TagIds { get; set; }
        public List<string>? ImageUrls { get; set; }
        public List<IFormFile>? UploadedImages { get; set; }
        public List<string>? DeletedImageUrls { get; set; }

        public List<CategoryDetailsVM> AvailableCategories { get; set; } = new();
        public List<ProductTagVM> AvailableTags { get; set; } = new();
    }
}