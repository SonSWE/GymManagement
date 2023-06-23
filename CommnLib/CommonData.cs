using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstData
{
    public class CommonData
    {
        public static string connectionString = "Data Source=(local);Initial Catalog=GymManagement;Integrated Security=True";
        public static string addressUploadFile = "G:\\BTL-PTTK\\GymManagement\\GymManagement\\wwwroot\\images";
        public static string httpApiClientHost = "https://localhost:44354/";

        public static string c_router_quan_tri = "api/quan-tri/";


        public static int PaymentDeadlineDefault = 5;
        public static float FineDefault = 0.05f;



    }

    public class ResponseInfo
    {
        public decimal success { get; set; }    
    }

    public class TypeMember
    {
        public static decimal Normal = 1;
        public static decimal Vip1 = 2;
        public static decimal Vip2 = 3;
    }

    public class StatusMemberCard
    {
        public static string HieuLuc = "A";
        public static string DungHD = "P";
        public static string Khoa = "L";
    }

    public class MemberSex
    {
        public static string nam = "Nam";
        public static string nu = "Nữ";
        public static string khac = "Khác";
    }

    public class VoucherType
    {
        public static decimal PhanTram = 0;
        public static decimal Tien = 1;
    }

    public class VoucherStatus
    {
        public static string HieuLuc = "A";
        public static string HetHieuLuc = "L";
        public static string HetLuot = "E";
    }
    public class InvoiceStatus
    {
        public static decimal HoanThanh = 1;
        public static decimal ChuaHoanThanh = 2;
    }

    public class BillPackgeType
    {
        public static decimal GiaHan = 1;
        public static decimal MuaGoi = 2;
    }

    public class InvoiceType
    {
        public static decimal MuaGoi = 1;
        public static decimal GiaHan = 2;
    }

    public class PaymentType
    {
        public static decimal GhiNo = 1;
        public static decimal KhongGhiNo = 2;
    }

    public class MemberPackgeStatus
    {
        public static string HoatDong = "A";
        public static string HetHan = "E";
        public static string Khoa = "L";
        public static string HoatDongGhiNo = "P";
    }


}
