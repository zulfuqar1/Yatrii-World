using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Reviews
{
    public class ReviewListDto
    {
        public long Id { get; set; }
        public long TourId { get; set; }
        public string TourName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
