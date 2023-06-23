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
    public class TypeMemberDA
    {
        public static List<TypeMemberInfo> GetAll()
        {
            try
            {
                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_typemember_getall");

                return CBO<TypeMemberInfo>.FillCollectionFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<TypeMemberInfo>();
            }
        }

        public static TypeMemberInfo GetById(decimal Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    DataSet dt = new DataSet();
                    string query = "SELECT * FROM [dbo].[TypeMember] WHERE Id = " + Id.ToString();
                    con.Open();
                    dt = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
                    con.Close();

                    return CBO<TypeMemberInfo>.FillObjectFromDataSet(dt);
                }
            }
            catch (Exception ex)
            {
                return new TypeMemberInfo();
            }
        }
    }
}
