using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class MemberPackgeInfo
    {
        public decimal STT { get; set; }
        public decimal Id { get; set; }
        public decimal MemberCardId { get; set; }
        public decimal PackgeId { get; set; }
        public DateTime ActiveDay { get; set; }
        public DateTime UnActiveDay { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
        public decimal DaysLeft { get; set; }
        public decimal MonthsLeft { get; set; }
        public decimal Debit { get; set; }
        public string PackgeName { get; set; }
        public DateTime CloseTimeDebit { get; set; }

        public decimal Deleted { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }

    }
}
