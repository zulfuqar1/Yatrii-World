

using YatriiWorld.MVC.ViewModels.Categories;

namespace YatriiWorld.MVC.Services.Interfaces
{
    public interface ICategoryClientService
    {
        Task<List<CategoryItemVM>> GetAllAsync(int page = 1, int take = 10);
        Task<List<CategoryItemVM>> GetAllArchivedAsync(int page = 1, int take = 10);
        Task<CategoryDetailsVM> GetByIdAsync(long id);
        Task CreateAsync(CategoryCreateVM vm);
        Task UpdateAsync(CategoryUpdateVM vm);
        Task DeleteAsync(long id);
        Task SoftDeleteAsync(long id);
        Task RestoreAsync(long id);
    }
}
