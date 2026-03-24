using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Categories;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface ICategoryService
    {
  
        Task<List<CategoryGetDto>> GetAllCategoriesAsync();
        Task<List<CategoryWithToursDto>> GetAllCategoriesWithToursAsync();
        Task<CategoryGetDto> GetCategoryByIdAsync(long id);
        Task<CategoryWithToursDto> GetCategoryWithToursByIdAsync(long id);
        Task<CategoryUpdateDto> GetCategoryUpdateByIdAsync(long id);
        Task CreateCategoryAsync(CategoryCreateDto dto);
        Task UpdateCategoryAsync(CategoryUpdateDto dto);
        Task RemoveCategoryAsync(long id);

    }
}
