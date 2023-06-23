using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class DebitDetailInfo
    {
        public decimal Id { get; set; }
        public decimal InvoiceId { get; set; }
        public decimal MemberPackgeId { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public decimal Debit { get; set; }
        public decimal Fine { get; set; }
        public string Status { get; set; }
        public int DateLate { get; set; }

        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
    }
}
