using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class InvoiceInfo
    {
        public decimal STT { get; set; }
        public decimal Id { get; set; }
        public string Code { get; set; }
        public decimal MemberPackgeId { get; set; }
        public int PackgePeriod { get; set; } 
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal Cash { get; set; }
        public decimal InvoiceType { get; set; }
        public decimal PaymentType { get; set; }
        public string Status { get; set; }
        public string InvoiceIssuer { get; set; }
        public DateTime IssuDate { get; set; }
        public string Voucher { get; set; }

        public decimal TotalAfterDiscount { get; set; }

        ////ghi nợ
        //public DateTime CloseTimeDebit { get; set; }
        //public decimal Debit { get; set; }
        //public string PackgeName { get; set; }

        public string MemberName { get; set; }
        public string MemberCode { get; set; }

        public string PackgeName { get; set; }
        public decimal BondPeriod { get; set; }
        public string TotalPriceString { get; set; }


    }
}
