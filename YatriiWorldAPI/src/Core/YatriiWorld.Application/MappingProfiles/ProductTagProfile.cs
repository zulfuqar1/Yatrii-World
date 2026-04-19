using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Product;
using YatriiWorld.Domain.Entities;
using YatriiWorld.MVC.ViewModels.Product;

namespace YatriiWorld.Application.MappingProfiles
{
    public class ProductTagProfile : Profile
    {
        public ProductTagProfile()
        {
          
            CreateMap<ProductTag, ProductTagDto>().ReverseMap();
            CreateMap<ProductTagCreateDto, ProductTag>();
            CreateMap<ProductTagUpdateDto, ProductTag>();
        }
    }
}
