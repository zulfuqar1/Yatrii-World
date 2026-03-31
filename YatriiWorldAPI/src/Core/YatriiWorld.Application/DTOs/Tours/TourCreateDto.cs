using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities.Enums;

namespace YatriiWorld.Application.DTOs.Tours
{
public class TourCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AmaizingPlacesCount { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int DurationInDays { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public TransportType TransportType { get; set; }
        public long CategoryId { get; set; }
        public List<long> SelectedTagIds { get; set; } = new();
        public List<string> ImageUrls { get; set; } = new();
    }
}

