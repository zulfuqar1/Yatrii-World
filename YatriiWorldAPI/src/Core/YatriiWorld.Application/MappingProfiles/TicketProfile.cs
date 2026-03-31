using AutoMapper;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.MappingProfiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketCreateDto, Ticket>()
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.TravellerCount, opt => opt.MapFrom(src => src.TotalPersonCount))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.CardHolderName))
                .ForMember(dest => dest.Travellers, opt => opt.MapFrom(src => src.Travelers))
                .ReverseMap();

            CreateMap<TicketUpdateDto, Ticket>().ReverseMap();

            CreateMap<TicketTravelerDto, TicketTraveler>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ReverseMap();
        }
    }
}
