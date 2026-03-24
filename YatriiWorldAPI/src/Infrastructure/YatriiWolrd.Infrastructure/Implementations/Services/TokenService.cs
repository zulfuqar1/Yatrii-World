using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tokens;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;

namespace YatriiWolrd.Infrastructure.Implementations.Services
{
    internal class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public TokenResponseDto CreateAcessToken(AppUser user  ,  IEnumerable<string> roles  ,  int minutes)
        {

            ICollection<Claim> claims = new List<Claim>
            {
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(ClaimTypes.Surname, user.LastName),
                     new Claim(ClaimTypes.GivenName, user.FirstName)
            };
  

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }




            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audiance"],
                expires: DateTime.UtcNow.AddMinutes(minutes),
                notBefore: DateTime.UtcNow,

                claims: claims,

                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secretKey"])),
                    SecurityAlgorithms.HmacSha256)
                );


            return new TokenResponseDto(
                new JwtSecurityTokenHandler().WriteToken(securityToken),
                user.UserName,
                securityToken.ValidTo
                );

        }

    }


}





    //   public TokenResponseDto CreateAcessToken(AppUser user, int minutes)
    //    {
    //        IEnumerable<Claim> userClaims = new List<Claim>
    //         {
    //             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

    //             new Claim(ClaimTypes.Name, user.UserName),

    //             new Claim(ClaimTypes.Email, user.Email),

    //             new Claim(ClaimTypes.Surname, user.LastName),

    //             new Claim(ClaimTypes.GivenName, user.FirstName),


    //         };


    //        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secretKey"]));

    //        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //        JwtSecurityToken securityToken = new JwtSecurityToken(
    //            issuer: _configuration["JWT:issuer"],
    //            audience: _configuration["JWT:audiance"],
    //            expires: DateTime.Now.AddMinutes(minutes),
    //            notBefore: DateTime.Now,
    //            claims: userClaims,
    //            signingCredentials: credentials
    //            );

    //        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    //        string token = tokenHandler.WriteToken(securityToken);

    //        return new TokenResponseDto(
    //            token,
    //            user.UserName,
    //            securityToken.ValidTo
    //            );

    //    }

    //}