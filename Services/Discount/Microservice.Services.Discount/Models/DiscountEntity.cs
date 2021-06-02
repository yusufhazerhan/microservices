using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Microservice.Services.Discount.Models
{
    [Table("discount")]
    public class DiscountEntity
    {
        public int Id{ get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
