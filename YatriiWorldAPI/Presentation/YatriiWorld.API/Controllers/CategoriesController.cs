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
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

   
        [HttpGet("with-tours")]
        public async Task<IActionResult> GetAllWithTours()
        {
            var result = await _categoryService.GetAllCategoriesWithToursAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromForm] long id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto dto)
        {
            await _categoryService.CreateCategoryAsync(dto);
            return StatusCode(201);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDto dto)
        {
            await _categoryService.UpdateCategoryAsync(dto);
            return NoContent(); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromForm] long id)
        {
            await _categoryService.RemoveCategoryAsync(id);
            return Ok(new { message = "The category was successfully deleted (Soft Delete)." });
        }
    }
}
