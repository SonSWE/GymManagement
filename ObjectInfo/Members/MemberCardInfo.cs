using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class MemberCardInfo
    {
        public decimal STT { get; set; }
        public decimal Id { get; set; }
        public string Code { get; set; }
        public decimal TypeMemberId { get; set; }
        public string Name { get; set; }
        public string ImgLink { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string IdentityCard { get; set; }
        public string IdentityAddress { get; set; }
        public DateTime IdentityDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        
        public decimal Deleted { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public string TypeMemberName { get; set; }
        public string StatusText { get; set; }
        public float Discount { get; set; }
    }
}
