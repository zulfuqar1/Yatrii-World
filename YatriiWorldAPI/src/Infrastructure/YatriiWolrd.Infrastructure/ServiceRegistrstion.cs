using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YatriiWolrd.Infrastructure.Implementations.Services;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWolrd.Infrastructure
{
    public static class ServiceRegistrstion
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(
             opt => opt.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,


                 ValidIssuer = configuration["JWT:issuer"],
                 ValidAudience = configuration["JWT:audiance"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:secretKey"])),
                 LifetimeValidator = (_, exp, token, _) => token !=null && exp != null? exp>DateTime.UtcNow : false,
                  RoleClaimType = ClaimTypes.Role
             }
             );

            return services;
        }

    }
}
