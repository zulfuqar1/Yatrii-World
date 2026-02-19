using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities.YatriiWorld.Domain.Entities;

namespace YatriiWorld.Domain.Entities
{
    public class Review : Base.BaseEntitiy
    {
        public string Comment { get; set; }
        public int Rating { get; set; } // 1-5 ulduz kimi
        public long TourId { get; set; }
        public Tour Tour { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}