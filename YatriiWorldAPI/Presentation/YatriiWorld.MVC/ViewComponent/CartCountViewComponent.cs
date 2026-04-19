using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.MVC.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly IBasketService _basketService;

        public CartCountViewComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int totalItems = 0;
            if (User.Identity.IsAuthenticated)
            {
                long userId = long.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
                var basket = await _basketService.GetUserBasketAsync(userId);
                totalItems = basket?.Items.Sum(x => x.Quantity) ?? 0;
            }
            return View(totalItems);
        }
    }
}