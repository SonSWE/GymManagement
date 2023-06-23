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
    public class InvoiceDA
    {
        public static decimal InsertInvoice(InvoiceInfo _info, SqlTransaction _trans)
        {
            try
            {
                decimal _result = -1;
                //decimal debit = _info.Total - _info.Discount - _info.Cash;

                //_info.Id = SqlHelper.GetNextVal("Invoice");
                //_info.DayCheckout = DateTime.Now;
                //_info.InvoiceStatus = debit == 0 ? InvoiceStatus.HoanThanh : InvoiceStatus.ChuaHoanThanh;
                //_info.TypePayment = debit == 0 ? PaymentType.KhongGhiNo : PaymentType.GhiNo;



                //using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                //{
                //    con.Open();
                //    string query = "INSERT INTO Invoice (Id, MemberPackgeId,InvoiceIssuer,DayCheckout, PackgeTerm, Discount, Total, Cash, TypeInvoice, TypePayment, InvoiceStatus)"
                //        + $"VALUES({_info.Id},{_info.MemberPackgeId},N'{_info.InvoiceIssuer}', N'{_info.DayCheckout.ToString("yyyy-MM-dd hh:mm:ss")}'," +
                //        $"{_info.PackgeTerm},{_info.Discount}, {_info.Total}, {_info.Cash}, {_info.TypeInvoice}, {_info.TypePayment}, {_info.InvoiceStatus});";
                //    _result = SqlHelper.ExecuteNonQuery(_trans, CommandType.Text, query);

                //    //nếu loại thanh toán là ghi nợ
                //    if (_info.TypePayment == PaymentType.GhiNo)
                //    {
                //        DebitDetailInfo _debitInfo = new DebitDetailInfo();
                //        _debitInfo.InvoiceId = _info.Id;
                //        _debitInfo.Debit = debit;
                //        _debitInfo.PaymentDeadline = _info.DayCheckout.AddDays(CommonData.PaymentDeadlineDefault);
                //        _debitInfo.PaymentStatus = InvoiceStatus.ChuaHoanThanh;

                //        _result = InsertDebitDetail(_debitInfo, _trans);
                //    }

                //    con.Close();
                //}
                return _result;

            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal InsertDebitDetail(DebitDetailInfo _info, SqlTransaction _trans)
        {
            try
            {
                decimal _result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO DebitDetail ( InvoiceId,Debit,PaymentDeadline, PaymentStatus)"
                        + $"VALUES({_info.InvoiceId},{_info.Debit}, N'{_info.PaymentDeadline.ToString("yyyy-MM-dd hh:mm:ss")}'," +
                        $"{_info.Status});";
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

        public static DebitDetailInfo GetDebitDetailByMemberPakcgeId(decimal _memberPakcgeId)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_memberPackgeId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPakcgeId
                };


                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_debitdetail_getDetail", lstParam);

                return CBO<DebitDetailInfo>.FillObjectFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new DebitDetailInfo();
            }
        }

        public static decimal ChangeInvoiceStatus(decimal _invoiceId, decimal status, SqlTransaction _trans)
        {
            try
            {
                decimal _result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "UPDATE Invoice "
                        + $"SET InvoiceStatus = {status} "
                        + $"WHERE Id = {_invoiceId};";
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



        public static decimal ChangeDebitDetailStatus(decimal _DebitId, decimal status, SqlTransaction _trans)
        {
            try
            {
                decimal _result = -1;
                using (SqlConnection con = new SqlConnection(CommonData.connectionString))
                {
                    con.Open();
                    string query = "UPDATE DebitDetail "
                        + $"SET PaymentStatus = {status} "
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

        public static decimal Payment(MemberPackgeInfo _memberPackgeInfo, InvoiceInfo _invoiceInfo)
        {
            try
            {
                decimal _result = -1;

                var lstParam = new SqlParameter[15];
                lstParam[0] = new SqlParameter("@p_memberPackgeId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.Id
                };
                lstParam[1] = new SqlParameter("@p_packgeId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.PackgeId
                };
                lstParam[2] = new SqlParameter("@p_memberCardId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.MemberCardId
                };
                lstParam[3] = new SqlParameter("@p_activeDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.ActiveDay
                };
                lstParam[4] = new SqlParameter("@p_unactiveDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.UnActiveDay
                };


                lstParam[5] = new SqlParameter("@p_packgePeriod", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.PackgePeriod
                };
                lstParam[6] = new SqlParameter("@p_discount", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.Discount
                };
                lstParam[7] = new SqlParameter("@p_total", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.Total
                };
                lstParam[8] = new SqlParameter("@p_cash", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.Cash
                };
                lstParam[9] = new SqlParameter("@p_invoiceType", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.InvoiceType
                };
                lstParam[10] = new SqlParameter("@p_paymentType", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.PaymentType
                };
                lstParam[11] = new SqlParameter("@p_invoiceIssuer", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.InvoiceIssuer
                };
                lstParam[12] = new SqlParameter("@p_issuDate", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.IssuDate
                };
                lstParam[13] = new SqlParameter("@p_voucher", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.Voucher
                };
                lstParam[14] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_memberpackge_buy_packge", lstParam);
                _result = Convert.ToDecimal(lstParam[14].Value);

                return _result;
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static decimal Payment_v2(MemberPackgeInfo _memberPackgeInfo, InvoiceInfo _invoiceInfo)
        {
            try
            {
                decimal _result = -1;

                var lstParam = new SqlParameter[15];
                lstParam[0] = new SqlParameter("@p_memberPackgeId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.Id
                };
                lstParam[1] = new SqlParameter("@p_packgeId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.PackgeId
                };
                lstParam[2] = new SqlParameter("@p_memberCardId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.MemberCardId
                };
                lstParam[3] = new SqlParameter("@p_activeDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.ActiveDay
                };
                lstParam[4] = new SqlParameter("@p_unactiveDate", SqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = _memberPackgeInfo.UnActiveDay
                };


                lstParam[5] = new SqlParameter("@p_packgePeriod", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.PackgePeriod
                };
                lstParam[6] = new SqlParameter("@p_discount", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.Discount
                };
                lstParam[7] = new SqlParameter("@p_total", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.Total
                };
                lstParam[8] = new SqlParameter("@p_paymentType", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.PaymentType
                };
                lstParam[9] = new SqlParameter("@p_invoiceIssuer", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.InvoiceIssuer
                };
                lstParam[10] = new SqlParameter("@p_issuDate", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.IssuDate
                };
                lstParam[11] = new SqlParameter("@p_voucher", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = _invoiceInfo.Voucher
                };
                lstParam[12] = new SqlParameter("@p_result", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output,
                    Value = -1
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_memberpackge_buy_packge_v2", lstParam);
                _result = Convert.ToDecimal(lstParam[12].Value);

                return _result;
            }
            catch (Exception ex)
            {
                return -1101;
            }
        }

        public static List<InvoiceInfo> Search(string keySearch, int startRow, int endRow, string orderBy, ref decimal totalRecord)
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

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_invoice_search", lstParam);
                totalRecord = Convert.ToDecimal(lstParam[4].Value);
                return CBO<InvoiceInfo>.FillCollectionFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new List<InvoiceInfo>();
            }
        }

        public static InvoiceInfo GetById(decimal id)
        {
            try
            {
                var lstParam = new SqlParameter[1];
                lstParam[0] = new SqlParameter("@p_id", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                var dt = SqlHelper.ExecuteDataset(CommonData.connectionString, CommandType.StoredProcedure, "sp_invoice_getById", lstParam);

                return CBO<InvoiceInfo>.FillObjectFromDataSet(dt);
            }
            catch (Exception ex)
            {
                return new InvoiceInfo();
            }
        }
    }
}
