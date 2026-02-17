using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatriiWorld.Application.DTOs.Categories;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Application.DTOs.Tour;
using YatriiWorld.Application.DTOs.Tour; // TourListDto üçün
using Microsoft.EntityFrameworkCore;    // Include üçün


namespace YatriiWorld.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryGetDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(categories);
        }
        // GET by id:
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryGetDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();

            if (category == null) return NotFound();
            return Ok(category);
        }



        // GET:
        [HttpGet("withtours")]
        public async Task<IActionResult> GetAllWithTours()
        {
            var categories = await _context.Categories
                .Include(c => c.Tours)
                .Select(c => new CategoryWithToursDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Tours = c.Tours.Select(t => new TourListDto
                    {
                        Id = t.Id,
                        Name = t.Title,
                        Price = t.Price
                    }).ToList()
                })
                .ToListAsync();

            return Ok(categories);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpGet("{id}/withtours")]
        public async Task<IActionResult> GetByIdWithTours(long id)
        {
            var category = await _context.Categories
                .Include(c => c.Tours) // relation-u gətir
                .Where(c => c.Id == id)
                .Select(c => new CategoryWithToursDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Tours = c.Tours.Select(t => new TourListDto
                    {
                        Id = t.Id,
                        Name = t.Title,
                        Price = t.Price
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (category == null) return NotFound();

            return Ok(category);
        }




        // PUT: api/category
        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto dto)
        {
            var category = await _context.Categories.FindAsync(dto.Id);
            if (category == null)
                return NotFound();

            category.Name = dto.Name;
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}
