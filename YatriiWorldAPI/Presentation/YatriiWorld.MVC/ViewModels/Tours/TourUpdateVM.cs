using YatriiWorld.Application.DTOs.Tours;

namespace YatriiWorld.MVC.ViewModels.Tours
{

     public class TourUpdateVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int AmaizingPlacesCount { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public long CategoryId { get; set; }
        public List<TourImageVM> Images { get; set; } = new();
    }
}
