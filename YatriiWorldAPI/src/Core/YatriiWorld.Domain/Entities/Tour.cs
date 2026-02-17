using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class Tour : Base.BaseNameableEntitiy
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<TourImage> Images { get; set; }
    }
}
