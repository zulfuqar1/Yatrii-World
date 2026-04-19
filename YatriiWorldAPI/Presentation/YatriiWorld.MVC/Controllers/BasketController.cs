using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.MVC.ViewModels.Basket;

namespace YatriiWorld.MVC.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var basketDto = await _basketService.GetUserBasketAsync(userId);

            var basketVM = new BasketVM
            {
                Items = basketDto.Items.Select(item => new BasketItemVM
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductImageUrl = item.ProductImageUrl,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                GrandTotal = basketDto.GrandTotal
            };

            return View(basketVM);
        }



         [HttpPost]
        public async Task<IActionResult> AddToBasket(long productId, int quantity = 1)
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _basketService.AddItemToBasketAsync(userId, productId, quantity);

                var basket = await _basketService.GetUserBasketAsync(userId);
                int totalCartItems = basket.Items.Sum(x => x.Quantity);

                return Json(new { success = true, cartCount = totalCartItems, message = "Ürün sepete eklendi!" });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}