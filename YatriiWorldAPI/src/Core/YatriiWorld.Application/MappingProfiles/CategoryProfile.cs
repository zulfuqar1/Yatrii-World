using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Categories;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
       
            public CategoryProfile()
        {

            CreateMap<Category, CategoryCreateDto>().ReverseMap();

            CreateMap<Category, CategoryGetDto>();

            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryWithToursDto>().ReverseMap();

        }
    }
}
