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
    public class StaffDA
    {
        public static List<UserInfo> Search(string name = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    string query = "";
                    if (name != null && name != "")
                    {
                        query = "SELECT m.* FROM [dbo].[Staff] m WHERE m.Name like N'%"+name+"%' or m.Name like '%"+name+"%'";
                    }
                    else
                    {
                        query = "SELECT m.* FROM [dbo].[Staff] m ";
                    }

                    con.Open();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<UserInfo>.FillCollectionFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new List<UserInfo>();
            }
        }

        public static UserInfo GetById(decimal id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    con.Open();
                    string query = $"SELECT m.* FROM [dbo].[Staff] m WHERE m.Id = {id}";
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<UserInfo>.FillObjectFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new UserInfo();
            }
        }

        public static decimal Insert(UserInfo _info)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Staff (Name, Birthday, Address, Sex, Phone, IdentityCard)"
                        + $"VALUES(N'{_info.Name}', '{_info.Birthday.ToString("yyyy-MM-dd")}',N'{_info.Address}',N'{_info.Sex}','{_info.Phone}','{_info.IdentityCard}');";
                    SqlHelper.ExecuteNonQuery(query, CommandType.Text, query);
                    con.Close();
                    return 8000;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Update(UserInfo _info)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "UPDATE Staff SET "
                        + $"Name = N'{_info.Name}',"
                        + $"Birthday = '{_info.Birthday.ToString("yyyy-MM-dd")}', "
                        + $"Address = N'{_info.Address}',"
                        + $"Sex = N'{_info.Sex}',"
                        + $"Phone = '{_info.Phone}',"
                        + $"IdentityCard = '{_info.IdentityCard}'"
                        + $"WHERE Id = {_info.Id};";
                    SqlHelper.ExecuteNonQuery(query, CommandType.Text, query);
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
                    string query = "DELETE Staff WHERE Id = " + id;
                    SqlHelper.ExecuteNonQuery(query, CommandType.Text, query);
                    con.Close();
                    return id;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static List<StaffInfo> GetAll()
        {
            try
            {
                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_staff_getAll");

                return CBO<StaffInfo>.FillCollectionFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<StaffInfo>();
            }
        }

    }
}
