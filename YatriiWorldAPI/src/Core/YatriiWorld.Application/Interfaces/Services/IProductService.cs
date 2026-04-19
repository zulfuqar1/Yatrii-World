using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Product;
using YatriiWorld.MVC.ViewModels.Product;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductTagDto> GetProductTagByIdAsync(long id);
        Task<bool> UpdateProductTagAsync(ProductTagUpdateDto dto);
        Task<bool> DeleteProductTagAsync(long id);
        Task CreateProductAsync(ProductCreateDto dto);
        Task<List<ProductTagDto>> GetAllProductTagsAsync();
        Task<bool> CreateProductTagAsync(ProductTagCreateDto dto);
        Task<ProductDto> GetProductByIdAsync(long id);
        Task<bool> UpdateProductAsync(ProductUpdateDto dto);
        Task<bool> DeleteProductAsync(long id);
    }
}
