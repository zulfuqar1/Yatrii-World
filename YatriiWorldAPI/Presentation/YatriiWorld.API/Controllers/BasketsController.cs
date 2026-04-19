using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Basket;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserBasket(long userId)
        {
            var basket = await _basketService.GetUserBasketAsync(userId);
            return Ok(basket);
        }

        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddItemToBasket(long userId, [FromBody] AddBasketItemDto dto)
        {
            try
            {
                await _basketService.AddItemToBasketAsync(userId, dto.ProductId, dto.Quantity);
                return Ok(new { message = "Ürün sepete başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{userId}/remove/{productId}")]
        public async Task<IActionResult> RemoveItemFromBasket(long userId, long productId)
        {
            await _basketService.RemoveItemFromBasketAsync(userId, productId);
            return Ok(new { message = "Ürün sepetten çıkarıldı." });
        }

        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearBasket(long userId)
        {
            await _basketService.ClearBasketAsync(userId);
            return Ok(new { message = "Sepet tamamen temizlendi." });
        }
    }
}