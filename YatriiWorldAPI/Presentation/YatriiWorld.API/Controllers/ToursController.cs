using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatriiWorld.Application.DTOs.Tour;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;

namespace YatriiWorld.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToursController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToursController(AppDbContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tours = await _context.Tours
                .Select(t => new TourGetDto
                {
                    Id = t.Id,
                    Name = t.Title,
                    Description = t.Description,
                    Price = t.Price,
                    Capacity = t.Capacity,
                    StartDate = t.StartDate,
                    CategoryId = t.CategoryId
                })
                .ToListAsync();

            return Ok(tours);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(TourCreateDto dto)
        {
            var tour = new Tour
            {
                Title = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Capacity = dto.Capacity,
                StartDate = dto.StartDate,
                CategoryId = dto.CategoryId
            };

            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();

            return Ok(tour);
        }

        // GET by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var tour = await _context.Tours
                .Where(t => t.Id == id)
                .Select(t => new TourGetDto
                {
                    Id = t.Id,
                    Name = t.Title,
                    Description = t.Description,
                    Price = t.Price,
                    Capacity = t.Capacity,
                    StartDate = t.StartDate,
                    CategoryId = t.CategoryId
                })
                .FirstOrDefaultAsync();

            if (tour == null) return NotFound();
            return Ok(tour);
        }
        // UPDATE
        [HttpPut]
        public async Task<IActionResult> Update(TourUpdateDto dto)
        {
            var tour = await _context.Tours.FindAsync(dto.Id);
            if (tour == null) return NotFound();

            tour.Title = dto.Name;
            tour.Description = dto.Description;
            tour.Price = dto.Price;
            tour.Capacity = dto.Capacity;
            tour.StartDate = dto.StartDate;
            tour.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return Ok(tour);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) return NotFound();

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
