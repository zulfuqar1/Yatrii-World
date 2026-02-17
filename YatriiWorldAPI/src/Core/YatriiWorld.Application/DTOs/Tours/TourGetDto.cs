using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Tour
{
    public class TourGetDto
    {
        public long Id { get; set; }         // entity-də long-dursa long olmalı
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public long CategoryId { get; set; }  // entity-də long-dursa long olmalı
    }
}
