using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.MappingProfiles
{
    public class TagMapping : Profile
    {
        public TagMapping()
        {
          
            CreateMap<Tag, TourTagDto>().ReverseMap();

            CreateMap<TagCreateDto, Tag>();
        }
    }
}
