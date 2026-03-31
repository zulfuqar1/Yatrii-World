using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Implementations.Repositories;

namespace YatriiWorld.Persistance.Implementations.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly ITourRepository _tourRepository;
        public ReviewService(IReviewRepository reviewRepository, ITourRepository tourRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;

            _tourRepository = tourRepository;
        }

        public async Task<bool> AddReviewAsync(ReviewCreateDto reviewDto, long userId)
        {
            var review = _mapper.Map<Review>(reviewDto);
            review.UserId = userId;
            review.CreatedAt = DateTime.Now;

         
            await _reviewRepository.AddAsync(review);
            int result = await _reviewRepository.SaveAsync();


            if (result > 0)
            {
               
                var tourReviews = await _reviewRepository.GetReviewsByTourIdAsync(review.TourId);

              
                double averageRating = tourReviews.Any()
                    ? Math.Round(tourReviews.Average(r => r.Rating), 1)
                    : 0;

           
                var tour = await _tourRepository.GetByIdAsync(review.TourId);

                if (tour != null)
                {
                   
                    tour.Rating = averageRating;

                    _tourRepository.Update(tour);
                    await _tourRepository.SaveAsync();
                }
            }

            return result > 0;
        }


        public async Task<List<ReviewListDto>> GetReviewsByTourIdAsync(long tourId)
        {
          
            var reviews = await _reviewRepository.GetReviewsByTourIdAsync(tourId);

          
            return _mapper.Map<List<ReviewListDto>>(reviews);
        }
    }
}
