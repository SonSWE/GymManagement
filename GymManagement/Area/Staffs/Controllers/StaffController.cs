using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;

namespace GymManagement.Area.Staffs.Controllers
{
    [Route("nhan-vien")]
    public class StaffController : Controller
    {
        [Route("danh-sach")]
        public IActionResult Index()
        {
            try
            {
                List<UserInfo> _lstData = StaffDA.Search("");
                ViewBag.LstData = _lstData;

            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Staffs/Views/StaffDisplay.cshtml");
        }

        [Route("tim-kiem")]
        public IActionResult Search(string name)
        {
            try
            {
                List<UserInfo> _lstData = StaffDA.Search(name);
                ViewBag.LstData = _lstData;
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Area/Staffs/Views/_Partial_List_Staff.cshtml");
        }

        [Route("chi-tiet/{id}"), HttpGet]
        public IActionResult Detail(decimal id)
        {
            try
            {
                ViewBag.StaffInfo = StaffDA.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Staffs/Views/_Partial_View_Staff.cshtml");
        }

        [Route("cap-nhat/{id}"), HttpGet]
        public IActionResult ShowUpdateForm(decimal id)
        {
            try
            {
                ViewBag.StaffInfo = StaffDA.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Staffs/Views/_Partial_Update_Staff.cshtml");
        }

        [Route("cap-nhat"), HttpPut]
        public IActionResult DoUpdate(UserInfo _info)
        {
            string _message = "Cập nhật thất bại!";
            decimal _result = -1;
            try
            {

                _result = StaffDA.Update(_info);
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

            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Staffs/Views/_Partial_Add_Staff.cshtml");
        }

        [Route("them-moi"), HttpPost]
        public IActionResult DoInsert(UserInfo _info)
        {
            string _message = "Thêm mới thất bại!";
            decimal _result = -1;
            try
            {

                _result = StaffDA.Insert(_info);
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
