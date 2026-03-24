using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatriiWorld.Application.DTOs.Categories;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;



namespace YatriiWorld.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService; // Context yerine Service!

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // 1. Tüm Kategoriler
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        // 2. Detaylı (Turlarla Beraber) Kategoriler
        [HttpGet("with-tours")]
        public async Task<IActionResult> GetAllWithTours()
        {
            var result = await _categoryService.GetAllCategoriesWithToursAsync();
            return Ok(result);
        }

        // 3. ID ile Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromForm] long id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(result);
        }

        // 4. Yeni Kategori Oluştur
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto dto)
        {
            await _categoryService.CreateCategoryAsync(dto);
            return StatusCode(201); // Created
        }

        // 5. Güncelle
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDto dto)
        {
            await _categoryService.UpdateCategoryAsync(dto);
            return NoContent(); // Veya Ok(dto)
        }

        // 6. Sil (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromForm] long id)
        {
            await _categoryService.RemoveCategoryAsync(id);
            return Ok(new { message = "Kategori başarıyla silindi (Soft Delete)." });
        }
    }
}
