using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITourService _tourService;

        public TagsController(ITourService tourService)
        {
            _tourService = tourService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var tag = await _tourService.GetTagByIdAsync(id); 
            if (tag == null) return NotFound();
            return Ok(tag);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] TagUpdateDto dto)
        {
            var isSuccess = await _tourService.UpdateTagAsync(dto);
            if (isSuccess) return Ok("Tur etiketi güncellendi.");
            return BadRequest("Güncelleme başarısız.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var isSuccess = await _tourService.DeleteTagAsync(id); 
            if (isSuccess) return Ok("Etiket silindi.");
            return BadRequest("Silme işlemi başarısız.");
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tourService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TagCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isSuccess = await _tourService.CreateTagAsync(dto);

            if (isSuccess) return Ok("Tur etiketi başarıyla eklendi.");

            return BadRequest("Tur etiketi eklenirken bir hata oluştu.");
        }
    }
}