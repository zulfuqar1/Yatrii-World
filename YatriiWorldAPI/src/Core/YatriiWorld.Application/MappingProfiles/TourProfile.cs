using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tour;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.MappingProfiles
{
    internal class TourProfile:Profile
    {
        public TourProfile()
        {

            CreateMap<Tour, TourCreateDto>();

            CreateMap<Tour, TourGetDto>();

            CreateMap<Tour, TourListDto>();

            CreateMap<Tour, TourUpdateDto>();

        }

    }
}
