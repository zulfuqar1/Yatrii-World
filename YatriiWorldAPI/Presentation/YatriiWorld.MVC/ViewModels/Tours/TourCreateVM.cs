using YatriiWorld.Domain.Entities.Enums;

namespace YatriiWorld.MVC.ViewModels.Tours
{
    public class TourCreateVM
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AmaizingPlacesCount { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long CategoryId { get; set; }
        public List<Categories.CategoryDetailsVM> AvailableCategories { get; set; } = new();
        public List<TourTagVM> AvailableTags { get; set; } = new();
        public List<IFormFile>? Photos { get; set; }

        public TransportType TransportType { get; set; }
       
        public List<long> SelectedTagIds { get; set; } = new();
        public List<string> ImageUrls { get; set; } = new();

    }
}
