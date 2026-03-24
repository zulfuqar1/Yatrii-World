using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review> 
    { 
        Task<List<Review>> GetReviewsByTourIdAsync(long tourId);
        Task<double> GetAverageRatingByTourIdAsync(long tourId);

    }

}
