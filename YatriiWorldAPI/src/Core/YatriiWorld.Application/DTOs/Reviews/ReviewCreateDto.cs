using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Reviews
{
    public class ReviewCreateDto
    {
   
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public long TourId { get; set; }

    }
}
