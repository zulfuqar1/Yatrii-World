using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tokens;
using YatriiWorld.Application.DTOs.Users;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Domain.Entities.Enums;

namespace YatriiWorld.Persistance.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthenticationService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IConfiguration configuration,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenService = tokenService;
        }




        public async Task RegisterAsync(RegisterDto userDto)
        {

            AppUser user = _mapper.Map<AppUser>(userDto);

            IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {


                StringBuilder sb = new StringBuilder();
                foreach (IdentityError error in result.Errors)
                {
                    sb.Append(error.Description);

                }
                throw new Exception(sb.ToString());
            }

            await _userManager.AddToRoleAsync(user,UserRole.Customer.ToString());

        }



        public async Task<TokenResponseDto> LoginAsync(LoginDto userDto)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserNameOrEmail || u.Email == userDto.UserNameOrEmail);

            if (user == null)
            {
                throw new Exception("User name ,Email or Password is invalid");
            }


            bool result = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (!result)
            {

                await _userManager.AccessFailedAsync(user);

                throw new Exception("User name ,Email or Password is invalid");

            }

            var roles = await _userManager.GetRolesAsync(user);

            return _tokenService.CreateAcessToken(user,roles,15);


        }
    }
}