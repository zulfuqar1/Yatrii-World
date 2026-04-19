using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Product;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.CreateProductAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromForm] ProductUpdateDto dto)
        {
            if (id != dto.Id || !ModelState.IsValid) return BadRequest();

            var result = await _productService.UpdateProductAsync(dto);
            if (!result) return NotFound();

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }


    }
}