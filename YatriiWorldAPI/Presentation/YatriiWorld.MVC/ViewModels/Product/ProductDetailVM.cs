namespace YatriiWorld.MVC.ViewModels.Product
{
    public class ProductDetailVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int StockQuantity { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public bool HasDiscount => DiscountedPrice.HasValue && DiscountedPrice.Value < Price;
        public bool IsInStock => StockQuantity > 0;
        public bool IsLowStock => StockQuantity > 0 && StockQuantity <= 5;
    }
}
