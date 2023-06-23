using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class VoucherPackgeInfo
    {
        public decimal Id { get; set; }
        public string Code { get; set; }
        public decimal Count { get; set; }
        public string Description { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal Discount { get; set; }
        public string Status { get; set; }
        public decimal VoucherType { get; set; }
        public decimal MinTotalBill { get; set; }
        public decimal ForMemberType { get; set; }
        public decimal MaximunUsage { get; set; }

        public decimal Deleted { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public string TypeMemberName { get; set; }
        public string StatusText { get; set; }
    }
}
