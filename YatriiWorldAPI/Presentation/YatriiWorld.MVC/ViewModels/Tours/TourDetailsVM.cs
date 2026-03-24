using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.DTOs.Tours;

namespace YatriiWorld.MVC.ViewModels.Tours
{
    public class TourDetailsVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }

        public int AmaizingPlacesCount { get; set; }


        public string CategoryName { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }




        public List<TourImageVM> Images { get; set; } = new();
        public List<TourTagVM> Tags { get; set; } = new();
        public List<ReviewListDto> Reviews { get; set; } = new();
    }
}
