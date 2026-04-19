using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Persistance.Implementations.Repositories.Generic;

namespace YatriiWorld.Persistance.Implementations.Repositories
{
    internal class BasketRepository : Repository<Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext context) : base(context) { }

        public async Task<Basket> GetBasketByUserIdAsync(long userId)
        {
            return await _context.Baskets
                .Include(b => b.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(b => b.UserId == userId && !b.IsDeleted);
        }
    }
}