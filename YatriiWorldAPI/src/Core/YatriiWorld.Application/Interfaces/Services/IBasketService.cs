using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Basket;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface IBasketService
    {
        Task<BasketDto> GetUserBasketAsync(long userId);
        Task AddItemToBasketAsync(long userId, long productId, int quantity);
        Task RemoveItemFromBasketAsync(long userId, long basketItemId);
        Task ClearBasketAsync(long userId);
    }
}
