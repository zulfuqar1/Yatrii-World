using Microsoft.AspNetCore.Mvc;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourService.GetAllToursWithDetailsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var result = await _tourService.GetTourByIdAsync(id);
            if (result == null) return NotFound("Tour not found!");
            return Ok(result);
        }

        // POST: api/tours
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TourCreateDto dto)
        {
            try
            {
                await _tourService.CreateTourAsync(dto);
                return Ok(new { message = "Tour created successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] TourUpdateDto dto)
        {
            await _tourService.UpdateTourAsync(dto);
            return Ok(new { message = "Tour updated successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _tourService.RemoveTourAsync(id);
            return Ok(new { message = "Tour deleted successfully!" });
        }
    }
}