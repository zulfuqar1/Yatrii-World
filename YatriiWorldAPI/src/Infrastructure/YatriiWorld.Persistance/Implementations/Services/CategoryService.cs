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
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistance.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CategoryCreateDto dto)
        {
            if (!await _categoryRepository.IsCategoryNameUniqueAsync(dto.Name))
                throw new Exception("Bu isimde bir kategori zaten mevcut.");

            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task<List<CategoryGetDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return _mapper.Map<List<CategoryGetDto>>(categories);
        }

        public async Task<List<CategoryWithToursDto>> GetAllCategoriesWithToursAsync()
        {
            var categories = await _categoryRepository.GetCategoriesWithToursAsync();
            return _mapper.Map<List<CategoryWithToursDto>>(categories);
        }

        public async Task<CategoryGetDto> GetCategoryByIdAsync(long id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.IsDeleted) throw new Exception("Kategori bulunamadı.");

            return _mapper.Map<CategoryGetDto>(category);
        }

        public async Task<CategoryUpdateDto> GetCategoryUpdateByIdAsync(long id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.IsDeleted) throw new Exception("Kategori bulunamadı.");

            return _mapper.Map<CategoryUpdateDto>(category);
        }

        public async Task<CategoryWithToursDto> GetCategoryWithToursByIdAsync(long id)
        {
            var category = await _categoryRepository.GetCategoryWithToursByIdAsync(id);
            if (category == null || category.IsDeleted) throw new Exception("Kategori bulunamadı.");

            return _mapper.Map<CategoryWithToursDto>(category);
        }

        public async Task RemoveCategoryAsync(long id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) throw new Exception("Kategori zaten mevcut değil.");

            category.IsDeleted = true;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task UpdateCategoryAsync(CategoryUpdateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.Id);
            if (category == null || category.IsDeleted) throw new Exception("Kategori bulunamadı.");

            _mapper.Map(dto, category);

            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
        }
    }
}
