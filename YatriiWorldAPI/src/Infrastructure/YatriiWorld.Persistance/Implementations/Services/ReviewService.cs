using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistance.Implementations.Services
{
    internal class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddReviewAsync(ReviewCreateDto reviewDto, long userId)
        {
            var review = _mapper.Map<Review>(reviewDto);
            review.UserId = userId;
            review.CreatedAt = DateTime.Now;

         
            await _reviewRepository.AddAsync(review);

        
            int result = await _reviewRepository.SaveAsync();

            return result > 0;
        }


        public async Task<List<ReviewListDto>> GetReviewsByTourIdAsync(long tourId)
        {
            var reviews = await _reviewRepository.GetReviewsByTourIdAsync(tourId);

       
            return _mapper.Map<List<ReviewListDto>>(reviews);
        }
    }
}
