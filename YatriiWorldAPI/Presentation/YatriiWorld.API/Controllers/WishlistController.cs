using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistsController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost("ToggleWishlist")]
        public async Task<IActionResult> ToggleWishlist(long tourId)
        {
         
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Please login first.");

            await _wishlistService.ToggleWishlistAsync(userId, tourId);

            return Ok(new { message = "Successful" });
        }
    }
}