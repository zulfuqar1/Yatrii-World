using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoryWithDetailsAsync();
        Task<IEnumerable<Category>> GetCategoriesWithToursAsync();

        Task<Category> GetCategoryWithToursByIdAsync(long id);
        Task<List<Category>> GetCategoriesWithActiveToursAsync();

       
        Task<bool> IsCategoryNameUniqueAsync(string name);

        Task<int> GetTotalTourCountAsync(long categoryId);

    }
}
