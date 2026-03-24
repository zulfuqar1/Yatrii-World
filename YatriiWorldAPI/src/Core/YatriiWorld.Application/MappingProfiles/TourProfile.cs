using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;

public class TourProfile : Profile
{
    public TourProfile()
    {

        CreateMap<Tour, TourDetailDto>()
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
    
        .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
   
        .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
   
        .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));
        
        CreateMap<TourImage, TourImageDto>();

        
        CreateMap<Tag, TourTagDto>();

        // main mapping
        CreateMap<Tour, TourListDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))

            //image mapping
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))

            // TAG
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))

            //Reviews
            .ForMember(dest => dest.ReviewCount, opt => opt.MapFrom(src => src.Reviews != null ? src.Reviews.Count : 0))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                (src.Reviews != null && src.Reviews.Any()) ? src.Reviews.Average(r => r.Rating) : 0));

        CreateMap<TourCreateDto, Tour>();
        CreateMap<TourUpdateDto, Tour>();
    }
}