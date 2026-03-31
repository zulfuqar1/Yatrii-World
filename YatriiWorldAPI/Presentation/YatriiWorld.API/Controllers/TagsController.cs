using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          
            var tags = await _tourService.GetAllTagsAsync();
            return Ok(tags);
        }
    }
}
