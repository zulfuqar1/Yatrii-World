using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Implementations.Repositories;

namespace YatriiWorld.Persistance.Implementations.Services
{
    public class TourService : ITourService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;
        public TourService(ITourRepository tourRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _tourRepository = tourRepository;
            _tagRepository = tagRepository; 
            _mapper = mapper;
        }

        public async Task<List<TourListDto>> GetAllToursWithDetailsAsync()
        {
           
            var tours = await _tourRepository.GetToursWithDetailsAsync();

          
            return _mapper.Map<List<TourListDto>>(tours);
        }

        //top rated tours
        public async Task<List<TourListDto>> GetTopRatedToursAsync(int count)
        {
            var tours = await _tourRepository.GetTopRatedToursAsync(count);
            return _mapper.Map<List<TourListDto>>(tours);
        }


        // ID
        public async Task<TourDetailDto> GetTourByIdAsync(long id)
        {
            var tour = await _tourRepository.GetTourByIdWithDetailsAsync(id);

            if (tour == null) return null;

            return _mapper.Map<TourDetailDto>(tour);
        }



        public async Task CreateTourAsync(TourCreateDto dto)
        {
           
            var tour = _mapper.Map<Tour>(dto);

        
            tour.CreatedAt = DateTime.Now;
            tour.UpdatedAt = DateTime.Now;
            tour.IsDeleted = false;

          
            await _tourRepository.AddAsync(tour);
            await _tourRepository.SaveAsync();

          
            if (dto.SelectedTagIds != null && dto.SelectedTagIds.Any())
            {
                foreach (var tagId in dto.SelectedTagIds)
                {
                    var tag = await _tagRepository.GetByIdAsync(tagId);
                    if (tag != null)
                    {
                        if (tour.Tags == null) tour.Tags = new List<Tag>();
                        tour.Tags.Add(tag);
                    }
                }
             
                await _tourRepository.SaveAsync();
            }

            if (dto.Photos != null && dto.Photos.Count > 0)
            {
                string currentDir = Directory.GetCurrentDirectory();
                string mvcPath = Path.GetFullPath(Path.Combine(currentDir, "..", "YatriiWorld.MVC", "wwwroot", "assets", "images", "Tour"));

                if (!Directory.Exists(mvcPath)) Directory.CreateDirectory(mvcPath);

                foreach (var photo in dto.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    string fullPath = Path.Combine(mvcPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    var tourImage = new TourImage
                    {
                        TourId = tour.Id,
                        ImageUrl = "images/Tour/" + fileName,
                        IsMain = (tour.Images == null || !tour.Images.Any())
                    };

                    if (tour.Images == null) tour.Images = new List<TourImage>();
                    tour.Images.Add(tourImage);
                }

                await _tourRepository.SaveAsync();
            }
        }








        public async Task UpdateTourAsync(TourUpdateDto dto)
        {
         
            var existTour = await _tourRepository.GetAll()
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (existTour == null) throw new Exception("Tour not found");

          
            _mapper.Map(dto, existTour);
         
            existTour.Tags.Clear(); 

            foreach (var tagId in dto.SelectedTagIds)
            {

                var tag = await _tagRepository.GetByIdAsync(tagId);

                if (tag != null)
                {
                    existTour.Tags.Add(tag);
                }
            }


            _tourRepository.Update(existTour);
            await _tourRepository.SaveAsync();
        }





        public async Task RemoveTourAsync(long id)
        {
            var tour = await _tourRepository.GetByIdAsync(id);
            if (tour == null) throw new Exception("Tour not found.");

            tour.IsDeleted = true;
            _tourRepository.Update(tour);
            await _tourRepository.SaveAsync();
        }





        public async Task<List<TourTagDto>> GetAllTagsAsync()
        {

            var tags = await _tagRepository.GetAll().ToListAsync();

          
            return _mapper.Map<List<TourTagDto>>(tags);
        }




    }
}

