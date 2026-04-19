namespace YatriiWorld.MVC.ViewModels.Product
{
    public class ProductVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public string? MainImageUrl { get; set; }
        public double Rating { get; set; }

        public string? CategoryName { get; set; }
        public int StockQuantity { get; set; }

        public List<ProductTagVM> Tags { get; set; } = new List<ProductTagVM>();
        public bool HasDiscount => DiscountedPrice.HasValue && DiscountedPrice.Value < Price;

        public int DiscountPercentage => HasDiscount
            ? (int)Math.Round((1 - (DiscountedPrice!.Value / Price)) * 100)
            : 0;
    }
}