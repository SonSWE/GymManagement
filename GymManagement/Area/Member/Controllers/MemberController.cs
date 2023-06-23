
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;

namespace GymManagement.Area.Member.Controllers
{
    [Route("thanh-vien")]
    public class MemberController : Controller
    {
        [Route("danh-sach-thanh-vien")]
        public IActionResult Index()
        {
            try
            {
                List<MemberInfo> _lstData = MemberDA.Search("");
                ViewBag.LstMember = _lstData;

            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/Member/MenberDisplay.cshtml");
        }

        [Route("tim-kiem")]
        public IActionResult Search(string name)
        {
            try
            {
                List<MemberInfo> _lstData = MemberDA.Search(name);
                ViewBag.LstMember = _lstData;
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Area/Member/Views/Member/_Partial_List_Member.cshtml");
        }

        [Route("chi-tiet/{id}"), HttpGet]
        public IActionResult Detail(decimal id)
        {
            try
            {
                MemberInfo _info = new MemberInfo();
                _info = MemberDA.GetById(id);
                ViewBag.MemberInfo = _info;
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/Member/_Partial_View_Member.cshtml");
        }

        [Route("cap-nhat/{id}"), HttpGet]
        public IActionResult Update(decimal id)
        {
            try
            {
                MemberInfo _info = new MemberInfo();
                _info = MemberDA.GetById(id);
                ViewBag.MemberInfo = _info;

                ViewBag.LstTypeMember = TypeMemberDA.GetAll();
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/Member/_Partial_Update_Member.cshtml");
        }

        [Route("cap-nhat"), HttpPut]
        public IActionResult DoUpdate(MemberInfo _info)
        {
            string _message = "Cập nhật thất bại!";
            decimal _result = -1;
            try
            {

                _result = MemberDA.Update(_info);
                if( _result > 0 )
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
            return View("~/Area/Member/Views/Member/_Partial_Add_Member.cshtml");
        }

        [Route("them-moi"), HttpPost]
        public IActionResult DoInsert(MemberInfo _info)
        {
            string _message = "Thêm mới thất bại!";
            decimal _result = -1;
            try
            {

                _result = MemberDA.Insert(_info);
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
