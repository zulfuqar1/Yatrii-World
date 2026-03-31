using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;

namespace YatriiWorld.Application.DTOs.Tours
{
    public class TourDetailDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AmaizingPlacesCount { get; set; }


        public string CategoryName { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }

        public double Rating { get; set; }




        public List<TourImageDto> Images { get; set; } = new();
        public List<TourTagDto> Tags { get; set; } = new();
        public List<ReviewListDto> Reviews { get; set; } = new();
    }
}
