using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.DTOs.Tours
{
    public class TourGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public long CategoryId { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public int AmaizingPlacesCount { get; set; }
        public List<ReviewListDto> Reviews { get; set; } = new();

        public double Rating { get; set; }
    }

}
