namespace YatriiWorld.MVC.ViewModels.Basket
{
    public class BasketVM
    {
     
        public List<BasketItemVM> Items { get; set; } = new List<BasketItemVM>();

        public decimal GrandTotal { get; set; }


        public bool IsBasketEmpty => Items == null || !Items.Any();
    }
}
