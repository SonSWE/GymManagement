
using CommnLib;
using ConstData;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace GymManagement.Area.Statistic.Controllers
{
    [Route("tra-cuu-hoa-don")]
    public class StatisticInvoiceController : Controller
    {
        [Route("danh-sach")]
        public IActionResult Index()
        {
            try
            {
                ViewBag.MenuActive = "statisticInvoice";

                decimal totalRecord = 0;
                List<MemberCardInfo> _lstMember = MemberCardDA.Search("||",1,10,"", ref totalRecord);
                ViewBag.lstMember = _lstMember;

                List<StaffInfo> _lstStaff = StaffDA.GetAll();
                ViewBag.lstStaff = _lstStaff;
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Statistic/Views/Index.cshtml");
        }

        [Route("tim-kiem")]
        public IActionResult Search(string keysearch, int curentPage, int p_record_on_page)
        {
            try
            {
                int p_to = 0;
                int p_from = CommonFunc.GetFromToPaging(curentPage, p_record_on_page, out p_to);
                decimal totalRecord = 0;
                List<InvoiceInfo> _lstData = InvoiceDA.Search(keysearch, p_from, p_to, "", ref totalRecord);
                ViewBag.LstData = _lstData;

                ViewBag.Paging = CommonFunc.PagingData(curentPage, p_record_on_page, (int)totalRecord);
                ViewBag.Record_On_Page = p_record_on_page;
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Area/Statistic/Views/_Partial_List.cshtml");
        }

        [Route("in-hoa-don/{id}"), HttpGet]
        public IActionResult Preview(decimal id)
        {
            try
            {
                ViewBag.MenuActive = "statisticInvoice";

                string words = CommonFunc.ConvertNumberToVietnameseWords(1);
                ViewBag.info = InvoiceDA.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Statistic/Views/_Partial_Print.cshtml");
        }
    }
}
