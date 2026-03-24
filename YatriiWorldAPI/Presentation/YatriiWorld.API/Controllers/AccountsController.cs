using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YatriiWorld.Application.DTOs.Users;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        public AccountsController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            await _service.RegisterAsync(dto);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            return Ok(await _service.LoginAsync(dto));
        }




    }
}
