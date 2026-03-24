using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;

namespace YatriiWorld.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewCreateDto reviewDto, long userId);
        Task<List<ReviewListDto>> GetReviewsByTourIdAsync(long tourId);
    }
}
