using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class MemberInfo
    {
        public decimal Id { get; set; }
        public decimal IdTypeMember { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
        public string TypeMemberText { get; set; }
    }

    public class TypeMemberInfo
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Discount { get; set; }
        public decimal Deleted { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
    }
}
