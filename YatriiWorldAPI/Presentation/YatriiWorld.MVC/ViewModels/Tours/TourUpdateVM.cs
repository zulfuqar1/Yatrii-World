using System.Text.Json.Serialization;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Domain.Entities.Enums;
using YatriiWorld.MVC.ViewModels.Categories;

namespace YatriiWorld.MVC.ViewModels.Tours
{
    public class TourUpdateVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AmaizingPlacesCount { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long CategoryId { get; set; }
        public TransportType TransportType { get; set; }
        public List<long> SelectedTagIds { get; set; } = new();
        public List<IFormFile>? Photos { get; set; }
        public List<CategoryDetailsVM> AvailableCategories { get; set; } = new();
        public List<TourTagVM> Tags { get; set; } = new();

        [JsonPropertyName("images")] 
        public List<TourImageVM> Images { get; set; } = new();
        public List<string> ImageUrls
        {
            get
            {
                if (Images != null && Images.Any())
                {
                    return Images.Select(x => x.ImageUrl).ToList();
                }
                return new List<string>();
            }
            set { }
        }
    }
}