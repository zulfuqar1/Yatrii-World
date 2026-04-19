using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<string>? ImageUrls { get; set; }
        public List<ProductTagDto> Tags { get; set; } = new List<ProductTagDto>();
        public List<IFormFile>? UploadedImages { get; set; }
    }
}
