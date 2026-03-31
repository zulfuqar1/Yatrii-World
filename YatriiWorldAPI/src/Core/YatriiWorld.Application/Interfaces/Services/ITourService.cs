using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.DTOs.Tours;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface ITourService
    {

        Task<List<TourListDto>> GetAllToursWithDetailsAsync();
        Task<TourDetailDto> GetTourByIdAsync(long id);
        Task<List<TourListDto>> GetTopRatedToursAsync(int count);
        Task CreateTourAsync(TourCreateDto dto);
        Task UpdateTourAsync(TourUpdateDto dto);
        Task RemoveTourAsync(long id);

        Task<List<TourTagDto>> GetAllTagsAsync();

    }
}
