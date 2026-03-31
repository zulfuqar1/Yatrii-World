using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistance.Implementations.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IMapper _mapper;

        public WishlistService(IWishlistRepository wishlistRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }

        public async Task ToggleWishlistAsync(string userId, long tourId)
        {
            long uId = long.Parse(userId);

            var existing = await _wishlistRepository.GetAll()
                .FirstOrDefaultAsync(x => x.AppUserId == uId && x.TourId == tourId);

            if (existing != null)
            {
                _wishlistRepository.Delete(existing);
            }
            else
            {
                await _wishlistRepository.AddAsync(new Wishlist
                {
                    AppUserId = uId,
                    TourId = tourId
                });
            }

            await _wishlistRepository.SaveAsync();
        }
        public async Task<List<TourListDto>> GetUserWishlistAsync(string userId)
        {
            long uId = long.Parse(userId);

            var wishlistItems = await _wishlistRepository.GetAll()
                .Where(x => x.AppUserId == uId)
                .Include(x => x.Tour)
                    .ThenInclude(t => t.Images) 
                .Select(x => x.Tour) 
                .ToListAsync();

            return _mapper.Map<List<TourListDto>>(wishlistItems);
        }
        public async Task<bool> IsInWishlistAsync(string userId, long tourId)
        {
            long uId = long.Parse(userId);

            return await _wishlistRepository.GetAll()
                .AnyAsync(x => x.AppUserId == uId && x.TourId == tourId);
        }
    }
}