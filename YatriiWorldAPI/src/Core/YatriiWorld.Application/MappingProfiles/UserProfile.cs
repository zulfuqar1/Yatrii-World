using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.DTOs.Users;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            
            CreateMap<AppUser, LoginDto>();

           
            CreateMap<AppUser, UserGetDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

          
            CreateMap<AppUser, UserListDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));


            CreateMap<AppUser, UserDetailsDto>();



            CreateMap<UserUpdateDto, AppUser>();


        }
    }
}
