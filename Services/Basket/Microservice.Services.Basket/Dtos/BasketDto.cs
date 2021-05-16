using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microservice.Services.Basket.Dtos
{
    public class BasketDto
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<ItemDto> Items { get; set; }

        public decimal TotalPrice
        {
            get => Items.Sum(s => s.Price * s.Quantity);
        }
    }
}
