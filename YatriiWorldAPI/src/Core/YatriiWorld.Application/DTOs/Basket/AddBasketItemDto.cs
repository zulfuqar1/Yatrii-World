using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Basket
{
    public class AddBasketItemDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
