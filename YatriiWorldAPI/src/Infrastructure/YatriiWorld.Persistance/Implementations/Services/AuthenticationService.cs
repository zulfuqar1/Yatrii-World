using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tokens;
using YatriiWorld.Application.DTOs.Users;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Domain.Entities.Enums;
using YatriiWorld.Application.DTOs.RegistrationCodes; // DTO namespace'ini ekledik

namespace YatriiWorld.Persistance.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService; // 1. Postacımızı tanımladık

        public AuthenticationService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IConfiguration configuration,
            ITokenService tokenService,
            IEmailService emailService // 2. Dependency Injection ile içeri aldık
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task RegisterAsync(RegisterDto userDto)
        {

            // 1. BU KISMI EKLE: Kullanıcıyı e-postaya göre kontrol et
            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);

            if (existingUser != null)
            {
                // Eğer adam mailini onaylamışsa, gerçekten kayıtlıdır; hata ver.
                if (existingUser.EmailConfirmed)
                {
                    throw new Exception("This email is already in use.");
                }

                // EĞER ONAYLAMAMIŞSA: Eskisini sil ki aşağıda tertemiz yeniden oluşsun.
                await _userManager.DeleteAsync(existingUser);
            }



            if (userDto.Password != userDto.ConfirmPassword)
            {
                throw new Exception("Passwords do not match");
            }

            AppUser user = _mapper.Map<AppUser>(userDto);

            IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

            // 4. RESULT CHECK
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (IdentityError error in result.Errors)
                {
                    sb.AppendLine(error.Description);
                }

                throw new Exception(sb.ToString());
            }

            await _userManager.AddToRoleAsync(user, UserRole.Customer.ToString());

            // 🚀 BİZİM EKLEDİĞİMİZ E-POSTA GÖNDERME KISMI 🚀

            // 1. 6 Haneli rastgele kod üret
            string verificationCode = new Random().Next(100000, 999999).ToString();

            // 2. Kodu veritabanındaki kullanıcıya kaydet (AppUser'da bu property olmalı!)
            user.VerificationCode = verificationCode;
            await _userManager.UpdateAsync(user);

            // 3. E-postayı fırlat
            string subject = "YatriiWorld - Verify Your Email! 🚀";
            string htmlMessage = $@"
                <div style='text-align:center; padding: 20px; font-family: Arial; border: 1px solid #ddd; border-radius: 10px; max-width: 500px; margin: auto;'>
                    <h2 style='color: #4CAF50;'>Welcome to YatriiWorld!</h2>
                    <p style='font-size: 16px; color: #555;'>Thank you for registering. Please use the verification code below to activate your account:</p>
                    <h1 style='color: #ffb100; letter-spacing: 10px; font-size: 36px; background: #f8f9fa; padding: 15px; border-radius: 5px; border: 2px dashed #ffb100;'>{verificationCode}</h1>
                    <p style='font-size: 12px; color: #aaa;'>If you did not request this, please ignore this email.</p>
                </div>";

            await _emailService.SendEmailAsync(user.Email, subject, htmlMessage);
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto userDto)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserNameOrEmail || u.Email == userDto.UserNameOrEmail);

            if (user == null)
            {
                throw new Exception("User name, Email or Password is invalid");
            }
            if (!user.EmailConfirmed)
            {
                throw new Exception("Please verify your email before logging in.");
            }


            bool result = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(user);
                throw new Exception("User name, Email or Password is invalid");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return _tokenService.CreateAcessToken(user, roles, 50);
        }

      
        public async Task<bool> VerifyRegistrationCodeAsync(PostRegistrationCodeDto dto)
        {
          
            AppUser user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new Exception("User not found!");
            }

           
            if (user.VerificationCode != dto.Code)
            {
                throw new Exception("Invalid or expired verification code!");
            }

         
            user.EmailConfirmed = true; 
            user.VerificationCode = null;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Verification failed at database level: {errors}");
            }

            await _userManager.UpdateAsync(user);

            return true;
        }
    }
}