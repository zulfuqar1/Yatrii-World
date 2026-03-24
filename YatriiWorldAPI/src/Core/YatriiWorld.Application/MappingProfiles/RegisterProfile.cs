using AutoMapper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Categories;
using YatriiWorld.Application.DTOs.Users;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.MappingProfiles
{
    internal class RegisterProfile:Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterDto, AppUser>();
     
        }
    }
}
