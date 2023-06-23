using ConstData;
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
    public class InvoiceDebitDA
    {
        public static decimal PaymentDebit(InvoiceDebitInfo _invoiceDebitInfo)
        {
            try
            {
                decimal _result = -1;

                var lstParam = new SqlParameter[7];
                lstParam[0] = new SqlParameter("@p_debitDetailId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceDebitInfo.DebitDetailId
                };
                lstParam[1] = new SqlParameter("@p_fine", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceDebitInfo.Fine
                };
                lstParam[2] = new SqlParameter("@p_invoiceIssuer", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceDebitInfo.InvoiceIssuer
                };
                lstParam[3] = new SqlParameter("@p_paymentDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceDebitInfo.PaymentDay
                };
                lstParam[4] = new SqlParameter("@p_memberPackgeId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceDebitInfo.MemberPackgeId
                };


                lstParam[5] = new SqlParameter("@p_invoiceId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceDebitInfo.InvoiceId
                };
                lstParam[6] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_invoicedebit_insert", lstParam);
                _result = Convert.ToDecimal(lstParam[6].Value);

                return _result;
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }
    }
}
