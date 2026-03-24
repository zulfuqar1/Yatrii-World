using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Tokens
{
    public record TokenResponseDto(
        string AccessToken,
        string UserName,
        DateTime Expiration
        );
}
