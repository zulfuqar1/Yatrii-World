using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Product;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.MVC.ViewModels.Product;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTagsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductTagsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _productService.GetAllProductTagsAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var tag = await _productService.GetProductTagByIdAsync(id); 
            if (tag == null) return NotFound("Ürün etiketi bulunamadı.");
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductTagCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isSuccess = await _productService.CreateProductTagAsync(dto);

            if (isSuccess) return Ok("Ürün etiketi başarıyla eklendi.");

            return BadRequest("Ürün etiketi eklenirken bir hata oluştu.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductTagUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isSuccess = await _productService.UpdateProductTagAsync(dto);

            if (isSuccess) return Ok("Ürün etiketi başarıyla güncellendi.");

            return BadRequest("Ürün etiketi güncellenirken bir hata oluştu.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var isSuccess = await _productService.DeleteProductTagAsync(id);

            if (isSuccess) return Ok("Ürün etiketi başarıyla silindi.");

            return BadRequest("Ürün etiketi silinirken bir hata oluştu.");
        }
    }
}