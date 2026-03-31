using YatriiWorld.Application.DTOs.RegistrationCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.RegistrationCodes;
using YatriiWorld.Application.DTOs.Tokens;
using YatriiWorld.Application.DTOs.Users;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterDto userDto);
        Task<TokenResponseDto> LoginAsync(LoginDto userDto);
        Task<bool> VerifyRegistrationCodeAsync(PostRegistrationCodeDto dto);
    }
}
