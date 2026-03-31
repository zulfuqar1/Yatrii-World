using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.MappingProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
           
            CreateMap<ReviewCreateDto, Review>();


            CreateMap<Review, ReviewListDto>()
              .ForMember(dest => dest.UserName, opt =>
                  opt.MapFrom(src => src.AppUser != null ? $"{src.AppUser.FirstName} {src.AppUser.LastName}" : "Anonymous"))
              .ForMember(dest => dest.UserProfilePicture, opt =>
                  opt.MapFrom(src => src.AppUser != null ? src.AppUser.ProfileImageUrl : null));

            CreateMap<Review, ReviewUserDto>();
        }
    }
}
