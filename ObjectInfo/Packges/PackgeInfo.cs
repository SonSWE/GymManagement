using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class PackgeInfo
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Discount { get; set; }
        public decimal Price { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}
