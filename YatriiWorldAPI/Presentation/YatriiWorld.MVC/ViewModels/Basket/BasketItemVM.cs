namespace YatriiWorld.MVC.ViewModels.Basket
{
    public class BasketItemVM
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductImageUrl { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
