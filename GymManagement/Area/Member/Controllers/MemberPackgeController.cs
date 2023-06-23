using ConstData;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagement.Area.Member.Controllers
{
    [Route("goi-tap-cua-thanh-vien")]
    public class MemberPackgeController : Controller
    {
        [Route("danh-sach/{memberCardId}")]
        public IActionResult Index(decimal memberCardId)
        {
            try
            {
                ViewBag.MenuActive = "memberCard";

                List<MemberPackgeInfo> _lstData = MemberPackgeDA.GetByMemberId(memberCardId);
                MemberCardInfo _memberCardInfo = MemberCardDA.GetById(memberCardId);
                
                ViewBag.LstPackgeOfMember = _lstData;
                ViewBag._memberCard = _memberCardInfo;
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/ListPackgeOfMember/PackgeOfMenberDisplay.cshtml");
        }

        [Route("mua-goi-tap/{MemberCardId}"), HttpGet]
        public IActionResult ShowFormBuyPackge(decimal MemberCardId)
        {
            try
            {
                ViewBag.MenuActive = "memberCard";

                List<PackgeInfo> _lstAllPackge = PackgeDA.GetPackgeMemberNotHave(MemberCardId);
                ViewBag.LstAllPackge = _lstAllPackge;
                ViewBag.MemberCardInfo = MemberCardDA.GetById(MemberCardId);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/ListPackgeOfMember/_Partial_Buy_Packge.cshtml",1);
        }

        [Route("thanh-toan"), HttpPost]
        public async Task<IActionResult> Payment(InvoiceInfo _invoiceInfo, MemberPackgeInfo _memberPackge)
        {
            string invoiceHtml = "";
            string _message = "Đăng ký thất bại!";
            decimal _result = -1;
            try
            {
                var user = "sondt24";
                _invoiceInfo.InvoiceIssuer = user;
                _invoiceInfo.IssuDate = DateTime.Now;
                _invoiceInfo.Status = "A";

                if (_memberPackge.Id == 0)
                {
                    _memberPackge.ActiveDay = DateTime.Now;
                    _memberPackge.UnActiveDay = _memberPackge.ActiveDay.AddMonths(_invoiceInfo.PackgePeriod);
                }
                else
                {
                    _memberPackge.UnActiveDay = _memberPackge.UnActiveDay.AddMonths(_invoiceInfo.PackgePeriod);
                }
                _memberPackge.Created_By = user;
                _memberPackge.Created_Date = DateTime.Now;

                _result = InvoiceDA.Payment_v2(_memberPackge, _invoiceInfo);

                if (_result > 0)
                {
                    _message = "Đăng ký thành công!";

                    InvoiceInfo _invoiceSucccess = InvoiceDA.GetById(_result);
                    if(_invoiceSucccess != null)
                    {
                        invoiceHtml = await this.RenderViewToStringAsync("~/Area/Member/Views/ListPackgeOfMember/_Partial_Invoice.cshtml", _invoiceSucccess);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = _result, message = _message, invoiceTemp = invoiceHtml });
        }

        [Route("gia-han-goi-tap/{idMemberPackge}"), HttpGet]
        public IActionResult ShowFormExtendPackge(decimal idMemberPackge)
        {
            MemberPackgeInfo _MemberPackgeInfo = new MemberPackgeInfo();
            try
            {
                ViewBag.MenuActive = "memberCard";

                _MemberPackgeInfo = MemberPackgeDA.GetById(idMemberPackge);

                ViewBag.PackgeInfo = PackgeDA.GetById(_MemberPackgeInfo.PackgeId);
                ViewBag.MemberCardInfo = MemberCardDA.GetById(_MemberPackgeInfo.MemberCardId);

                ViewBag.memberPackgeInfo = _MemberPackgeInfo;
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/ListPackgeOfMember/_Partial_Buy_Packge.cshtml", 2);

        }

        [Route("hoan-thien-hoa-don/{MemberPackgeId}"), HttpGet]
        public IActionResult ShowFormPaymentDebit(decimal MemberPackgeId)
        {
            try
            {
                DebitDetailInfo _debitDetail = InvoiceDA.GetDebitDetailByMemberPakcgeId(MemberPackgeId);
                ViewBag.DebitDetailInfo = _debitDetail;
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/ListPackgeOfMember/_Partial_Payment_Debit.cshtml");
        }

        [Route("hoan-thien-hoa-don"), HttpPost]
        public IActionResult DoPaymentDebit(InvoiceDebitInfo invoiceDebitInfo)
        {
            string _message = "Thanh toán thất bại!";
            decimal _result = -1;
            try
            {
                var user = "sondt24";
                invoiceDebitInfo.InvoiceIssuer = user;
                invoiceDebitInfo.PaymentDay = DateTime.Now;
                _result = InvoiceDebitDA.PaymentDebit(invoiceDebitInfo);

                if (_result > 0)
                {
                    _message = "Thanh toán thành công!";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = _result, message = _message });
        }


        [Route("tim-ma-giam-gia"), HttpGet]
        public IActionResult GetInfoVoucherByVoucherCode(string code)
        {
            VoucherPackgeInfo _data = new VoucherPackgeInfo();

            try
            {
                _data = VoucherPackgeDA.GetByVoucherCode(code);
            }
            catch (Exception ex)
            {
                return Json(new { success = -1, jsonData = "" });
            }
            return Json(new { success = 1, jsonData = JsonSerializer.Serialize(_data) });
        }
    }
}
