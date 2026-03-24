using YatriiWorld.MVC.ViewModels.Tours;

namespace YatriiWorld.MVC.Services.Interfaces
{
    public interface ITourClientService
    {
        Task<List<TourListVM>> GetAllAsync(int page = 1, int take = 10);

        Task<TourDetailsVM> GetByIdAsync(long id);

        Task CreateAsync(TourCreateVM vm);

        Task UpdateAsync(TourUpdateVM vm);

        Task SoftDeleteAsync(long id);

        Task RestoreAsync(long id);

        Task DeleteAsync(long id);
    }
}
