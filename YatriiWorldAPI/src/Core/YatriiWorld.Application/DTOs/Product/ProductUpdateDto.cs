using Microsoft.AspNetCore.Http;

public class ProductUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }

    public List<long>? TagIds { get; set; }
    public List<string>? ImageUrls { get; set; }
    public List<string>? DeletedImageUrls { get; set; }
    public List<IFormFile>? UploadedImages{get; set;}

}