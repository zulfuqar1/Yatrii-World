using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tour;

namespace YatriiWorld.Application.DTOs.Categories
{
    public class CategoryWithToursDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<TourListDto> Tours { get; set; }
    }
}
