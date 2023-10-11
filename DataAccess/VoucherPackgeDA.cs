using ConstData;
using Lib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VoucherPackgeDA
    {

        public static List<VoucherPackgeInfo> Search(string name = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    string query = "";
                    if (name != null && name != "")
                    {
                        query = "SELECT m.*, al1.Content Status_Text, al2.Content VoucherType_Text FROM VoucherPackge m " +
                            "LEFT JOIN AllCode al1 ON al1.CDName = 'VOUCHER' AND al1.CDType = 'STATUS' AND al1.CDVal = m.Status " +
                            "LEFT JOIN AllCode al2 ON al2.CDName = 'VOUCHER' AND al2.CDType = 'VOUCHERTYPE' AND al2.CDVal = m.VoucherType " +
                            "WHERE m.deleted = 0 and m.VoucherCode like N'%" + name + "%' or m.VoucherCode like '%" + name + "%'";
                    }
                    else
                    {
                        query = "SELECT m.*, al1.Content Status_Text, al2.Content VoucherType_Text FROM VoucherPackge m " +
                            "LEFT JOIN AllCode al1 ON al1.CDName = 'VOUCHER' AND al1.CDType = 'STATUS' AND al1.CDVal = m.Status " +
                            "LEFT JOIN AllCode al2 ON al2.CDName = 'VOUCHER' AND al2.CDType = 'VOUCHERTYPE' AND al2.CDVal = m.VoucherType " +
                            "WHERE m.deleted = 0";
                    }

                    con.Open();

                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<VoucherPackgeInfo>.FillCollectionFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new List<VoucherPackgeInfo>();
            }
        }

        public static VoucherPackgeInfo GetByVoucherCode(string code)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_code", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = code
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_vourcherpackge_getByCode", lstParam);

                return CBO<VoucherPackgeInfo>.FillObjectFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new VoucherPackgeInfo();
            }
        }

        public static decimal Insert(VoucherPackgeInfo _info)
        {
            try
            {
                decimal result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO VoucherPackge (Code,Discount, Count, CloseTime, OpenTime, Status, VoucherType, Description)"
                        + $"VALUES(N'{_info.Code}',{_info.Discount},{_info.Count}, '{_info.CloseTime.ToString("yyyy-MM-dd")}','{_info.OpenTime.ToString("yyyy-MM-dd")}',N'{_info.Status}',{_info.VoucherType},'{_info.Description}');";
                    result = SqlHelper.ExecuteNonQuery(con, CommandType.Text, query);
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Update(VoucherPackgeInfo _info)
        {
            try
            {
                decimal result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "UPDATE VoucherPackge SET "
                        + $"VoucherCount = '{_info.Count}', " +
                        $" Discount = {_info.Discount}"
                        + $"CloseTime = N'{_info.CloseTime.ToString("yyyy-MM-dd")}',"
                        + $"OpenTime = N'{_info.OpenTime.ToString("yyyy-MM-dd")}',"
                        + $"Status = '{_info.Status}',"
                        + $"VourcherType = '{_info.VoucherType}'"
                        + $"WHERE VoucherCode = {_info.Code};";
                    result = SqlHelper.ExecuteNonQuery(con, CommandType.Text, query);
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Delete(string code)
        {
            try
            {
                decimal result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = $"DELETE VoucherPackge WHERE VoucherCode = '{code}'" ;
                    result = SqlHelper.ExecuteNonQuery(con, CommandType.Text, query);
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

    }
}
