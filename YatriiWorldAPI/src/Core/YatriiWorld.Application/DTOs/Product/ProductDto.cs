using YatriiWorld.Application.DTOs.Product;

public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ShortDescription { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
    public string? MainImageUrl { get; set; }
    public double Rating { get; set; }
    public List<ProductTagDto> Tags { get; set; } = new List<ProductTagDto>();
    public string? CategoryName { get; set; }
    public int StockQuantity { get; set; }
}