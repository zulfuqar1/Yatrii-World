namespace YatriiWorld.MVC.ViewModels.Tours
{
    public class TourCreateVM
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int AmaizingPlacesCount { get; set; }
        public DateTime StartDate { get; set; }
        public long CategoryId { get; set; }
        public List<long> SelectedTagIds { get; set; } = new();
        public List<string> ImageUrls { get; set; } = new();

    }
}
