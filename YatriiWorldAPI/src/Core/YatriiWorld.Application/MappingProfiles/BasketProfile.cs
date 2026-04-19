using AutoMapper;
using System.Linq;
using YatriiWorld.Application.DTOs.Basket;

using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
          
            CreateMap<BasketItem, BasketItemDto>()
              
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))

               
                .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src =>
                    (src.Product != null && src.Product.Images != null && src.Product.Images.Any(i => i.IsMain))
                        ? src.Product.Images.FirstOrDefault(i => i.IsMain).ImageUrl
                        : null))

            
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src =>
                    (src.Product != null && src.Product.DiscountedPrice.HasValue)
                        ? src.Product.DiscountedPrice.Value
                        : (src.Product != null ? src.Product.Price : 0)));


            CreateMap<Basket, BasketDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<AddBasketItemDto, BasketItem>();
        }
    }
}