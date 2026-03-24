using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tokens;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface ITokenService
    {

        TokenResponseDto CreateAcessToken(AppUser user, IEnumerable<string> roles, int minutes);
    }
}
