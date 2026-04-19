using AutoMapper;
using System.Linq;
using YatriiWorld.Application.DTOs.Product;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductTag, ProductTagDto>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.MainImageUrl, opt => opt.MapFrom(src =>
                    (src.Images != null && src.Images.Any(i => i.IsMain))
                        ? src.Images.FirstOrDefault(i => i.IsMain).ImageUrl
                        : null))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    (src.Reviews != null && src.Reviews.Any())
                        ? src.Reviews.Average(r => r.Rating)
                        : 0))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                    src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.ProductTags));

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.ProductTags, opt => opt.Ignore());

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();

            CreateMap<ProductTagDto, Tag>();

            CreateMap<ProductUpdateDto, Product>();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();




        }
    }
}