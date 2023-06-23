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
    public class MemberCardDA
    {
        public static List<MemberCardInfo> Search(string name = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    string query = "";
                    if (name != null && name != "")
                    {
                        query = "SELECT mc.Id, mc.IdMember, mc.StartDay, mc.EndDay, mc.Avatar,mc.Status, m.Name," +
                            " m.Birthday, m.Address, m.Sex, m.Phone, m.IdentityCard , m.IdTypeMember, " +
                            "tm.Name AS TypeMemberText FROM MemberCard mc JOIN Member m ON m.Id = mc.IdMember " +
                            "LEFT JOIN TypeMember tm ON tm.Id = m.IdTypeMember" +
                            " WHERE m.Name like N'%" + name + "%' or m.Name like '%" + name + "%'";
                    }
                    else
                    {
                        query = "SELECT mc.Id, mc.IdMember, mc.StartDay, mc.EndDay, mc.Avatar,mc.Status, m.Name," +
                            " m.Birthday, m.Address, m.Sex, m.Phone, m.IdentityCard , m.IdTypeMember, " +
                            "tm.Name AS TypeMemberText FROM MemberCard mc JOIN Member m ON m.Id = mc.IdMember " +
                            "LEFT JOIN TypeMember tm ON tm.Id = m.IdTypeMember";
                    }

                    con.Open();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<MemberCardInfo>.FillCollectionFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new List<MemberCardInfo>();
            }
        }


        public static List<MemberCardInfo> Search(string keySearch, int startRow, int endRow, string orderBy, ref decimal totalRecord)
        {
            try
            {
                var lstParam = new SqlParameter[5];
                lstParam[0] = new SqlParameter("@p_key_search", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = keySearch
                };
                lstParam[1] = new SqlParameter("@p_startrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = startRow
                };
                lstParam[2] = new SqlParameter("@p_endrow", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = endRow
                };
                lstParam[3] = new SqlParameter("@p_orderby", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = orderBy
                };
                lstParam[4] = new SqlParameter("@p_total_record", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_membercard_search", lstParam);
                totalRecord = Convert.ToDecimal(lstParam[4].Value);
                return CBO<MemberCardInfo>.FillCollectionFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<MemberCardInfo>();
            }
        }

        public static MemberCardInfo GetById(decimal id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_membercard_getById", lstParam);

                return CBO<MemberCardInfo>.FillObjectFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new MemberCardInfo();
            }
        }

        public static MemberCardInfo GetByIdMember(decimal idMember)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    con.Open();
                    string query = $"SELECT * FROM MemberCard c WHERE c.IdMember = {idMember}";

                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);

                    con.Close();

                    return CBO<MemberCardInfo>.FillObjectFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new MemberCardInfo();
            }
        }

        public static decimal Insert(MemberCardInfo _info)
        {
            try
            {
                decimal _result = -1;

                var lstParam = new SqlParameter[15];
                lstParam[0] = new SqlParameter("@p_typeMemberId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.TypeMemberId
                };
                lstParam[1] = new SqlParameter("@p_imgLink", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.ImgLink
                };
                lstParam[2] = new SqlParameter("@p_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Name
                };
                lstParam[3] = new SqlParameter("@p_birthday", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Birthday
                };
                lstParam[4] = new SqlParameter("@p_address", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Address
                };
                lstParam[5] = new SqlParameter("@p_sex", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Sex
                };
                lstParam[6] = new SqlParameter("@p_phone", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Phone
                };
                lstParam[7] = new SqlParameter("@p_email", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Email
                };
                lstParam[8] = new SqlParameter("@p_identityCard", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.IdentityCard
                };
                lstParam[9] = new SqlParameter("@p_identityAddress", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.IdentityAddress
                };
                lstParam[10] = new SqlParameter("@p_identityDate", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.IdentityDate
                };
                lstParam[11] = new SqlParameter("@p_status", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Status
                }; 
                lstParam[12] = new SqlParameter("@p_createdBy", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_By
                };
                lstParam[13] = new SqlParameter("@p_createdDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Created_Date
                };
                lstParam[14] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_membercard_insert", lstParam);
                _result = Convert.ToDecimal(lstParam[14].Value);

                return _result;
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Update(MemberCardInfo _info)
        {
            try
            {
                decimal _result = -1;

                var lstParam = new SqlParameter[15];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Id
                };
                lstParam[1] = new SqlParameter("@p_imgLink", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.ImgLink
                };
                lstParam[2] = new SqlParameter("@p_name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Name
                };
                lstParam[3] = new SqlParameter("@p_birthday", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Birthday
                };
                lstParam[4] = new SqlParameter("@p_address", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Address
                };
                lstParam[5] = new SqlParameter("@p_sex", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Sex
                };
                lstParam[6] = new SqlParameter("@p_phone", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Phone
                };
                lstParam[7] = new SqlParameter("@p_email", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Email
                };
                lstParam[8] = new SqlParameter("@p_identityCard", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.IdentityCard
                };
                lstParam[9] = new SqlParameter("@p_identityAddress", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.IdentityAddress
                };
                lstParam[10] = new SqlParameter("@p_identityDate", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.IdentityDate
                };
                lstParam[11] = new SqlParameter("@p_status", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Status
                };
                lstParam[12] = new SqlParameter("@p_modifiedBy", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_By
                };
                lstParam[13] = new SqlParameter("@p_modifiedDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_Date
                };
                lstParam[14] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_membercard_update", lstParam);
                _result = Convert.ToDecimal(lstParam[14].Value);

                return _result;
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Delete(MemberCardInfo _info)
        {
            try
            {
                decimal _result = -1;

                var lstParam = new SqlParameter[15];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Id
                };
                lstParam[1] = new SqlParameter("@p_modifiedBy", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_By
                };
                lstParam[2] = new SqlParameter("@p_modifiedDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _info.Modified_Date
                };
                lstParam[3] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_membercard_delete", lstParam);
                _result = Convert.ToDecimal(lstParam[3].Value);

                return _result;
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

    }
}
