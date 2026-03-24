using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Persistance.Implementations.Services;

namespace YatriiWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }


      
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourService.GetAllToursWithDetailsAsync();
            return Ok(result);
        }



        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var result = await _tourService.GetTourByIdAsync(id);
            if (result == null) return NotFound("Tour not found!");

            return Ok(result);
        }


        [HttpGet("top-rated/{count}")]
        public async Task<IActionResult> GetTopRated([FromForm] int count)
        {
            var result = await _tourService.GetTopRatedToursAsync(count);
            return Ok(result);
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourCreateDto dto)
        {
            // Servisindeki 'SaveAsync'i tetikler
            await _tourService.CreateTourAsync(dto);
            return Ok(new { message = "Tour created successfully!" });
        }

        // 5. UPDATE: api/tours
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TourUpdateDto dto)
        {
            await _tourService.UpdateTourAsync(dto);
            return Ok(new { message = "Tour updated successfully!" });
        }

        // 6. DELETE: api/tours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromForm] long id)
        {
            await _tourService.RemoveTourAsync(id);
            return Ok(new { message = "Tour deleted (soft delete) successfully!" });
        }



    }

}

