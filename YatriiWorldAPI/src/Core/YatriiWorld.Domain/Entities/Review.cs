using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YatriiWorld.Domain.Entities
{
    public class Review : Base.BaseAccountableEntity
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public long TourId { get; set; }
        public Tour Tour { get; set; }

        public long UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}