using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Categories;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.Persistance.Implementations.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryGetDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();
            return _mapper.Map<List<CategoryGetDto>>(categories);
        }

        public async Task<List<CategoryWithToursDto>> GetAllCategoriesWithToursAsync()
        {
            var categories = await _categoryRepository.GetAll()
                .Include(c => c.Tours)
                .ToListAsync();
            return _mapper.Map<List<CategoryWithToursDto>>(categories);
        }

        public async Task<CategoryGetDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryGetDto>(category);
        }

        public async Task<CategoryUpdateDto> GetCategoryUpdateByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryUpdateDto>(category);
        }

        public async Task<CategoryWithToursDto> GetCategoryWithToursByIdAsync(int id)
        {
            var category = await _categoryRepository.GetAll()
                .Include(c => c.Tours)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<CategoryWithToursDto>(category);
        }
    }
}
