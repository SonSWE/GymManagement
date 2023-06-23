using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class InvoiceDebitInfo
    {
        public decimal Id { get; set; }
        public decimal DebitDetailId { get; set; }
        public decimal MemberPackgeId { get; set; }
        public decimal InvoiceId { get; set; }
        public decimal Fine { get; set; }
        
        public DateTime PaymentDay { get; set; }
        public string InvoiceIssuer { get; set; }
    }
}
