using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;

namespace GymManagement.Area.Packges.Controllers
{
    [Route("goi-tap")]
    public class PackgeController : Controller
    {
        [Route("danh-sach")]
        public IActionResult Index()
        {
            try
            {
                List<PackgeInfo> _lstData = PackgeDA.Search("");
                ViewBag.LstPackge = _lstData;

            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Packges/Views/Packges/PackgeDisplay.cshtml");
        }

        [Route("tim-kiem")]
        public IActionResult Search(string name)
        {
            try
            {
                List<PackgeInfo> _lstData = PackgeDA.Search(name);
                ViewBag.LstPackge = _lstData;
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Area/Packges/Views/Packges/_Partial_List_Packge.cshtml");
        }

        [Route("chi-tiet/{id}"), HttpGet]
        public IActionResult Detail(decimal id)
        {
            try
            {
                ViewBag.PackgeInfo = PackgeDA.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Packges/Views/Packges/_Partial_View_Packge.cshtml");
        }

        [Route("cap-nhat/{id}"), HttpGet]
        public IActionResult ShowUpdateForm(decimal id)
        {
            try
            {
                ViewBag.PackgeInfo = PackgeDA.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Packges/Views/Packges/_Partial_Update_Packge.cshtml");
        }

        [Route("cap-nhat"), HttpPut]
        public IActionResult DoUpdate(PackgeInfo _info)
        {
            string _message = "Cập nhật thất bại!";
            decimal _result = -1;
            try
            {

                _result = PackgeDA.Update(_info);
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
            return View("~/Area/Packges/Views/Packges/_Partial_Add_Packge.cshtml");
        }

        [Route("them-moi"), HttpPost]
        public IActionResult DoInsert(PackgeInfo _info)
        {
            string _message = "Thêm mới thất bại!";
            decimal _result = -1;
            try
            {

                _result = PackgeDA.Insert(_info);
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
