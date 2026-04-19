using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Basket
{
    public class BasketDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public decimal GrandTotal => Items.Sum(x => x.TotalPrice);
    }
}
