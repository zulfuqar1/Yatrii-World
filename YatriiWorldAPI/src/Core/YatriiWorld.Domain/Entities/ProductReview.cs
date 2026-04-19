using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Domain.Entities
{
    public class ProductReview :Base.BaseAccountableEntity
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("AppUser")]
        public long UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
