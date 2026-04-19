using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Product;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.MVC.ViewModels.Product;

namespace YatriiWorld.Persistance.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment; 

        public ProductService(
            IProductRepository productRepository,
            IProductTagRepository productTagRepository,
            IMapper mapper,
            IWebHostEnvironment environment) 
        {
            _productRepository = productRepository;
            _productTagRepository = productTagRepository;
            _mapper = mapper;
            _environment = environment; 
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }




        public async Task CreateProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            if (string.IsNullOrWhiteSpace(product.SKU))
            {
                product.SKU = "PRD-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            }

            if (string.IsNullOrWhiteSpace(product.Slug))
            {
                string baseName = !string.IsNullOrWhiteSpace(product.Name)
                    ? product.Name.ToLower().Replace(" ", "-")
                    : "product";

                product.Slug = $"{baseName}-{Guid.NewGuid().ToString("N").Substring(0, 6)}";
            }

            string mvcWwwRootPath = Path.Combine(_environment.ContentRootPath, "..", "YatriiWorld.MVC", "wwwroot");

            if (dto.UploadedImages != null && dto.UploadedImages.Any())
            {
                string uploadPath = Path.Combine(mvcWwwRootPath, "assets", "images", "Product");

                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                product.Images = new List<ProductImage>();
                bool isFirst = true;

                foreach (var file in dto.UploadedImages)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        product.Images.Add(new ProductImage
                        {
                            ImageUrl = "/images/product/" + fileName,
                            IsMain = isFirst
                        });
                        isFirst = false;
                    }
                }
            }

            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
        }






        public async Task<List<ProductTagDto>> GetAllProductTagsAsync()
        {
            var tags = await _productTagRepository.GetAll().ToListAsync();
            return _mapper.Map<List<ProductTagDto>>(tags);
        }

        public async Task<bool> CreateProductTagAsync(ProductTagCreateDto dto)
        {
            var newTag = _mapper.Map<ProductTag>(dto);

            newTag.CreatedAt = DateTime.Now;
            newTag.UpdatedAt = DateTime.Now;
            newTag.IsDeleted = false;

            await _productTagRepository.AddAsync(newTag);
            await _productRepository.SaveAsync();

            return true;
        }

        public async Task<ProductTagDto> GetProductTagByIdAsync(long id)
        {
            var tag = await _productTagRepository.GetByIdAsync(id);
            if (tag == null) return null;

            return _mapper.Map<ProductTagDto>(tag);
        }

        public async Task<bool> UpdateProductTagAsync(ProductTagUpdateDto dto)
        {
            var tag = await _productTagRepository.GetByIdAsync(dto.Id);
            if (tag == null) return false;

            tag.Name = dto.Name;
            tag.UpdatedAt = DateTime.Now;

            _productTagRepository.Update(tag);
            await _productRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteProductTagAsync(long id)
        {
            var tag = await _productTagRepository.GetByIdAsync(id);
            if (tag == null) return false;

            tag.IsDeleted = true;
            tag.UpdatedAt = DateTime.Now;

            _productTagRepository.Update(tag);
            await _productRepository.SaveAsync();

            return true;
        }

        public async Task<ProductDto> GetProductByIdAsync(long id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.Id);
            if (product == null) return false;

            _mapper.Map(dto, product);
            product.UpdatedAt = DateTime.Now;

            string mvcWwwRootPath = Path.Combine(_environment.ContentRootPath, "..", "YatriiWorld.MVC", "wwwroot");


            if (dto.DeletedImageUrls != null && dto.DeletedImageUrls.Any())
            {
                foreach (var url in dto.DeletedImageUrls)
                {
                    string relativeUrl = url.TrimStart('/');
                    var imagePath = Path.Combine(mvcWwwRootPath, relativeUrl);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    if (product.Images != null)
                    {
                        var imageToRemove = product.Images.FirstOrDefault(i => i.ImageUrl == url);
                        if (imageToRemove != null)
                        {
                            product.Images.Remove(imageToRemove);
                        }
                    }
                }
            }

            if (dto.UploadedImages != null && dto.UploadedImages.Any())
            {
                string uploadPath = Path.Combine(mvcWwwRootPath, "assets", "images", "Product");

                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                if (product.Images == null) product.Images = new List<ProductImage>();

                foreach (var file in dto.UploadedImages)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        product.Images.Add(new ProductImage
                        {
                            ImageUrl = "images/product/" + fileName,
                            IsMain = product.Images.Count == 0
                        });
                    }
                }
            }

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.Now;

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return true;
        }
    }
}