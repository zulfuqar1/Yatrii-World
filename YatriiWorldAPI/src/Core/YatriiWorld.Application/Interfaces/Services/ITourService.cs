using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Task<TourTagDto> GetTagByIdAsync(long id);
        Task<bool> UpdateTagAsync(TagUpdateDto dto);
        Task<bool> DeleteTagAsync(long id);
        Task<List<TourTagDto>> GetAllTagsAsync();
        Task<bool> CreateTagAsync(TagCreateDto dto);
    }
}
