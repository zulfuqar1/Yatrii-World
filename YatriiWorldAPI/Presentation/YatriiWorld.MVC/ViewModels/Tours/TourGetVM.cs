namespace YatriiWorld.MVC.ViewModels.Tours
{
    public class TourGetVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public long CategoryId { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public int AmaizingPlacesCount { get; set; }
    }
}
