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
    public class AllcodeDA
    {
        public static List<AllcodeInfo> GetByCDNameCDType(string cdname, string cdtype)
        {
            try
            {
                var lstParam = new SqlParameter[2];
                lstParam[0] = new SqlParameter("@p_cdName", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = cdname
                };
                lstParam[1] = new SqlParameter("@p_cdType", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = cdtype
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_allcode_getByCdNamCdType", lstParam);

                return CBO<AllcodeInfo>.FillCollectionFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<AllcodeInfo>();
            }
        }
    }
}
