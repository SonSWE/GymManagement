using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;

namespace GymManagement.Area.Voucher.Controllers
{
    [Route("ma-giam-gia")]
    public class VoucherController : Controller
    {
        [Route("danh-sach")]
        public IActionResult Index()
        {
            try
            {
                List<VoucherPackgeInfo> _lstData = VoucherPackgeDA.Search("");
                ViewBag.LstData = _lstData;

            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Voucher/Views/VoucherDisplay.cshtml");
        }

        [Route("tim-kiem")]
        public IActionResult Search(string name)
        {
            try
            {
                List<VoucherPackgeInfo> _lstData = VoucherPackgeDA.Search(name);
                ViewBag.LstData = _lstData;
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Area/Voucher/Views/_Partial_List_Voucher.cshtml");
        }

        [Route("chi-tiet/{_voucherCode}"), HttpGet]
        public IActionResult Detail(string _voucherCode)
        {
            try
            {
                ViewBag.VoucherInfo = ApiVoucher.GetDetail(_voucherCode);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Voucher/Views/_Partial_View_Voucher.cshtml");
        }

        [Route("cap-nhat/{_voucherCode}"), HttpGet]
        public IActionResult ShowUpdateForm(string _voucherCode)
        {
            try
            {
                ViewBag.VoucherInfo = ApiVoucher.GetDetail(_voucherCode);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Voucher/Views/_Partial_Update_Voucher.cshtml");
        }

        [Route("cap-nhat"), HttpPut]
        public IActionResult DoUpdate(VoucherPackgeInfo _info)
        {
            string _message = "Cập nhật thất bại!";
            decimal _result = -1;
            try
            {

                _result = ApiVoucher.Update(_info);
                if (_result > 0)
                {
                    _message = "Cập nhật thành công!";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = _result, message = _message });
        }

        [Route("them-moi"), HttpGet]
        public IActionResult ShowInsertForm()
        {
            try
            {
                ViewBag.LstTypeMember = TypeMemberDA.GetAll();
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Voucher/Views/_Partial_Add_Voucher.cshtml");
        }

        [Route("them-moi"), HttpPost]
        public IActionResult DoInsert([FromForm] VoucherPackgeInfo _info)
        {
            string _message = "Thêm mới thất bại!";
            decimal _result = -1;
            try
            {
                if(_info.OpenTime > DateTime.Now.Date)
                {
                    _info.Status = "P";
                }
                else
                {
                    _info.Status = "A";
                }
                _result = VoucherPackgeDA.Insert(_info);
                if (_result > 0)
                {
                    _message = "Thêm mới thành công!";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = _result, message = _message });
        }
    }
}
