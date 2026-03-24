using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Tours
{
    public class TourImageDto
    {
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
