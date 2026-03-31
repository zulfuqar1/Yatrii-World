using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities.Base;

namespace YatriiWorld.Domain.Entities
{
    public class Wishlist : BaseEntity
    {
        public long AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public long TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
