using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        // GET: api/reviews/tour/5
        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetByTourId(long tourId)
        {
            var reviews = await _reviewService.GetReviewsByTourIdAsync(tourId);
            return Ok(reviews);
        }


        // POST: api/reviews
        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto reviewDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out long userId))
            {
                return Unauthorized(new { message = "User identity could not be verified." });
            }

         
            var isCreated = await _reviewService.AddReviewAsync(reviewDto, userId);

            if (isCreated)
            {
                return Ok(new { message = "Review has been successfully submitted." });
            }
            return BadRequest(new { message = "An error occurred while saving the review." });
        }



    }
}
