using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities.Enums;

namespace YatriiWorld.Domain.Entities
{
    public class Tour : Base.BaseNameableEntity
    {
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; } 
        public string City { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public int Capacity { get; set; }
        public int AmaizingPlacesCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TransportType TransportType { get; set; }
        public Category Category { get; set; }



        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<TourImage> Images { get; set; }
    }
}
