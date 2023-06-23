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
    public class PackgeDA
    {
        public static List<PackgeInfo> Search(string name = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    string query = "";
                    if (name != null && name != "")
                    {
                        query = "SELECT m.*,( m.Price - m.Price*m.Discount) AS PriceAfterDiscount FROM [dbo].[Packge] m WHERE m.Name like N'%" + name + "%' or m.Name like '%" + name + "%'";
                    }
                    else
                    {
                        query = "SELECT m.*,( m.Price - m.Price*m.Discount) AS PriceAfterDiscount FROM [dbo].[Packge] m ";
                    }

                    con.Open();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<PackgeInfo>.FillCollectionFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new List<PackgeInfo>();
            }
        }

        public static List<PackgeInfo> GetPackgeMemberNotHave(decimal memberCardId)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@memberCardId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = memberCardId
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_packge_MemberNotBuy", lstParam);

                return CBO<PackgeInfo>.FillCollectionFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<PackgeInfo>();
            }
        }

        public static PackgeInfo GetById(decimal id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = $"SELECT m.*,( m.Price - m.Price*m.Discount) AS PriceAfterDiscount FROM [dbo].[Packge] m WHERE m.Id = {id}";
                    DataSet dt = new DataSet();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<PackgeInfo>.FillObjectFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new PackgeInfo();
            }
        }

        public static decimal Insert(PackgeInfo _info)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Packge (Name, Description, Discount, Price) "
                        + $"VALUES( N'{_info.Name}',N'{_info.Description}',{_info.Discount},{_info.Price});";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return 9000;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Update(PackgeInfo _info)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "UPDATE Packge "
                        + $"SET Name = N'{_info.Name}',"
                        + $"Description = N'{_info.Description}',"
                        + $"Discount = {_info.Discount}, "
                        + $"Price = {_info.Price}"
                        + $"WHERE Id = {_info.Id};";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return _info.Id;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Delete(decimal id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "DELETE Packge WHERE Id = " + id;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return id;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

    }
}
