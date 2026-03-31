using YatriiWorld.Application.DTOs.Tours;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task ToggleWishlistAsync(string userId, long tourId);
        Task<List<TourListDto>> GetUserWishlistAsync(string userId);
        Task<bool> IsInWishlistAsync(string userId, long tourId);
    }
}