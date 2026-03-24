using AutoMapper;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Application.DTOs.Tickets;

namespace YatriiWorld.Application.MappingProfiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
       
            CreateMap<Ticket, TicketListDto>()
                .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.Tour.Title))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.CustomerFullName))
                .ForMember(dest => dest.CheckIn, opt => opt.MapFrom(src => src.ChecikinDate))
                .ForMember(dest => dest.CheckOut, opt => opt.MapFrom(src => src.CheckOutDate))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

           
            CreateMap<Ticket, TicketDetailsDto>()
                .ForMember(dest => dest.TourName, opt => opt.MapFrom(src => src.Tour.Title))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.CustomerFullName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.AdultCount, opt => opt.MapFrom(src => src.AdultCount))
                .ForMember(dest => dest.ChildrenCount, opt => opt.MapFrom(src => src.ChilderenCount))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => src.ChecikinDate))
                .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => src.CheckOutDate))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));


            CreateMap<TicketCreateDto, Ticket>();
            CreateMap<TicketUpdateDto, Ticket>();
        }
    }
}
