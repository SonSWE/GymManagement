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
    public class MemberPackgeDA
    {
        public static List<MemberPackgeInfo> Search(decimal memberCardId, string _packgeName = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    string query = "";
                    if (_packgeName != null && _packgeName != "")
                    {
                        query = "SELECT p.*, c.Name AS PackgeName, (DATEDIFF(day, GETDATE(), p.UnActiveDay) ) AS DaysLeft  FROM MemberPackge p " +
                            " JOIN Packge c ON c.Id = p.PackgeId " +
                            " WHERE p.MemberCardId = " + memberCardId + " AND c.Name like N'%" + _packgeName + "%' or c.Name like '%" + _packgeName + "%'";
                    }
                    else
                    {
                        query = "SELECT p.*, c.Name AS PackgeName, (DATEDIFF(day, GETDATE(), p.UnActiveDay) ) AS DaysLeft  FROM MemberPackge p " +
                            " JOIN Packge c ON c.Id = p.PackgeId " +
                            "WHERE p.MemberCardId = " + memberCardId;
                    }

                    con.Open();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<MemberPackgeInfo>.FillCollectionFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new List<MemberPackgeInfo>();
            }
        }

        public static MemberPackgeInfo GetById(decimal id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    con.Open();
                    string query = "SELECT p.*, c.Name AS PackgeName  FROM MemberPackge p " +
                            " JOIN Packge c ON c.Id = p.PackgeId " +
                            "WHERE p.Id = " + id.ToString() + " ";
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<MemberPackgeInfo>.FillObjectFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new MemberPackgeInfo();
            }
        }

        public static decimal ChangeMemberPackegeStatus(decimal _DebitId, string status, SqlTransaction _trans)
        {
            try
            {
                decimal _result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "UPDATE MemberPackge "
                        + $"SET Status = {status} "
                        + $"WHERE Id = {_DebitId};";
                    _result = SqlHelper.ExecuteNonQuery(_trans, CommandType.Text, query);
                    con.Close();
                    return _result;
                }
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }


        public static List<MemberPackgeInfo> GetByMemberId(decimal memberCardId)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@memberCardId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = memberCardId
                };
               

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_memberpackge_by_memberCardId", lstParam);

                return CBO<MemberPackgeInfo>.FillCollectionFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<MemberPackgeInfo>();
            }
        }

    }
}
