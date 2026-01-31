using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Tour
{
    internal class TourGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string CategoryName { get; set; }
    }
}
