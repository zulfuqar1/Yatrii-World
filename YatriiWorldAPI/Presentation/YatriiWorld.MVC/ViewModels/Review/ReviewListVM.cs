namespace YatriiWorld.MVC.ViewModels.Review
{
    public class ReviewListVM
    {
       
            public long Id { get; set; }
            public long TourId { get; set; }
            public string TourName { get; set; }
            public string Comment { get; set; }
            public int Rating { get; set; }
            public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string UserProfilePicture { get; set; }
        public ReviewUserVM User { get; set; } = new();
        
    }
}
