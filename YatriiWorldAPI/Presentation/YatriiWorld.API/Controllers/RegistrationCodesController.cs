using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YatriiWorld.Application.DTOs.RegistrationCodes;
using PetCareManagement.Application.Interfaces.Services;
using System.Security.Claims;

namespace YatriiWorld.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationCodesController : ControllerBase
    {
        private readonly IRegistrationCodeService _registrationCodeService;

        public RegistrationCodesController(IRegistrationCodeService registrationCodeService)
        {
            _registrationCodeService = registrationCodeService;
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostRegistrationCodeDto dto)
        {
            var result = await _registrationCodeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] bool? isUsed = null, [FromQuery] int page = 0, [FromQuery] int take = 0)
        {
            var result = await _registrationCodeService.GetAllAsync(isUsed, page, take);
            return Ok(result);
        }



        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            if (id < 1)
                return BadRequest();
            var result = await _registrationCodeService.GetByIdAsync(id);
            return Ok(result);
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id < 1)
                return BadRequest();
            await _registrationCodeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
