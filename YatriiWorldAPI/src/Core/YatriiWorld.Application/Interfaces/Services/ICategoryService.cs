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
        Task<CategoryGetDto> GetCategoryByIdAsync(int id);

        Task<List<CategoryWithToursDto>> GetAllCategoriesWithToursAsync();

        Task<CategoryWithToursDto> GetCategoryWithToursByIdAsync(int id);

        Task<CategoryUpdateDto> GetCategoryUpdateByIdAsync(int id);

    }
}
