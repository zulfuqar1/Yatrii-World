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
            // Opsiyonel: Aynı isimde tur var mı kontrolü
            var existing = await _tourRepository.GetAll().AnyAsync(x => x.Title == dto.Title);
            if (existing) throw new Exception("This tour title already exists.");
            var tour = _mapper.Map<Tour>(dto);
            await _tourRepository.AddAsync(tour);
            await _tourRepository.SaveAsync();
        }



        public async Task UpdateTourAsync(TourUpdateDto dto)
        {
            // 1. Turu, mevcut etiketleriyle (Tags) birlikte getir
            var existTour = await _tourRepository.GetAll()
                .Include(x => x.Tags) // Burada TourTags değil, direkt Tags yazıyoruz
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (existTour == null) throw new Exception("Tour not found");

            // 2. Basit alanları Maple
            _mapper.Map(dto, existTour);

            // 3. MANY-TO-MANY GÜNCELLEMESİ (EF Core Otomatik Yöntem)
            existTour.Tags.Clear(); // Mevcut tüm etiket ilişkilerini temizle

            foreach (var tagId in dto.SelectedTagIds)
            {
                // TagRepository kullanarak veritabanındaki gerçek Tag nesnesini bulmalısın
                // Not: Eğer TagRepository yoksa _context.Tags üzerinden de yapabilirsin
                var tag = await _tagRepository.GetByIdAsync(tagId);

                if (tag != null)
                {
                    existTour.Tags.Add(tag); // Direkt Tag nesnesini ekle
                }
            }

            // 4. Kaydet
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

    }
}

