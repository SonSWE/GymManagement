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
    public class MemberDA
    {
        public static List<MemberInfo> Search(string name = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    string query = "";
                    if (name != null && name != "")
                    {
                        query = "SELECT m.*, t.Name AS TypeMemberText FROM [dbo].[Member] m LEFT JOIN TypeMember t ON t.Id = m.IdTypeMember WHERE m.Name like N'%"+name+"%' or m.Name like '%"+name+"%'";
                    }
                    else
                    {
                        query = "SELECT m.*, t.Name AS TypeMemberText FROM [dbo].[Member] m LEFT JOIN TypeMember t ON t.Id = m.IdTypeMember ";
                    }
                    
                    con.Open();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<MemberInfo>.FillCollectionFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new List<MemberInfo>();
            }
        }

        public static MemberInfo GetById(decimal id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
            
                    string query = $"SELECT *, t.Name AS TypeMemberText  FROM Member m LEFT JOIN TypeMember t ON t.Id = m.IdTypeMember WHERE m.Id = {id}";

                    con.Open();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<MemberInfo>.FillObjectFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new MemberInfo();
            }
        }

        public static decimal Insert(MemberInfo _info)
        {
            try
            {
                decimal _result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Member (IdTypeMember, Name, Birthday, Address, Sex, Phone, IdentityCard)"
                        + $"VALUES({_info.IdTypeMember}, N'{_info.Name}', '{_info.Birthday.ToString("yyyy-MM-dd")}',N'{_info.Address}',N'{_info.Sex}','{_info.Phone}','{_info.IdentityCard}');";

                    _result = SqlHelper.ExecuteNonQuery(con, CommandType.Text, query);

                    con.Close();
                    return _result;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Update(MemberInfo _info)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "UPDATE Member "
                        + $"SET IdTypeMember = {_info.IdTypeMember},"
                        + $"Name = N'{_info.Name}',"
                        + $"Birthday = '{_info.Birthday.ToString("yyyy-MM-dd")}', "
                        + $"Address = N'{_info.Address}',"
                        + $"Sex = N'{_info.Sex}',"
                        + $"Phone = '{_info.Phone}',"
                        + $"IdentityCard = '{_info.IdentityCard}'"
                        + $"WHERE Id = {_info.Id};";

                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, query);
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
                    string query = "DELETE MEMBER WHERE Id = " + id;

                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, query);
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
