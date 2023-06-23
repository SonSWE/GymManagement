
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

namespace GymManagement.Area.Member.Controllers
{
    [Route("khach-hang")]
    public class MemberCardController : Controller
    {
        [Route("danh-sach")]
        public IActionResult Index()
        {
            try
            {
                ViewBag.MenuActive = "memberCard";

                List<TypeMemberInfo> _lstTypeMember = TypeMemberDA.GetAll();
                ViewBag.LstTypeMember = _lstTypeMember;

                List<AllcodeInfo> _lstStatus = AllcodeDA.GetByCDNameCDType("MEMBERCARD", "STATUS");
                ViewBag.LstStatus = _lstStatus;
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/MemberCard/MenberCardDisplay.cshtml");
        }

        [Route("tim-kiem")]
        public IActionResult Search(string keysearch, int curentPage, int p_record_on_page)
        {
            try
            {
                int p_to = 0;
                int p_from = CommonFunc.GetFromToPaging(curentPage, p_record_on_page, out p_to);
                decimal totalRecord = 0;
                List<MemberCardInfo> _lstData = MemberCardDA.Search(keysearch, p_from, p_to, "", ref totalRecord);
                ViewBag.LstData = _lstData;

                ViewBag.Paging = CommonFunc.PagingData(curentPage, p_record_on_page, (int)totalRecord);
                ViewBag.Record_On_Page = p_record_on_page;
            }
            catch (Exception ex)
            {

            }
            return PartialView("~/Area/Member/Views/MemberCard/_Partial_List_MemberCard.cshtml");
        }

        [Route("chi-tiet/{id}"), HttpGet]
        public IActionResult Detail(decimal id)
        {
            try
            {
                ViewBag.MenuActive = "memberCard";

                ViewBag.MemberCardInfo = MemberCardDA.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/MemberCard/_Partial_View_MemberCard.cshtml");
        }


        [Route("dang-ky"), HttpGet]
        public IActionResult ShowInsertForm()
        {
            try
            {
                ViewBag.MenuActive = "memberCard";

                ViewBag.LstTypeMember = TypeMemberDA.GetAll();
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/MemberCard/_Partial_Add_MemberCard.cshtml");
        }

        [Route("dang-ky"), HttpPost]
        public IActionResult DoInsert(MemberCardInfo _info, IFormFile fileContent)
        {
            string _message = "Đăng ký thất bại!";
            decimal _result = -1;
            try
            {
                var user = "sondt24";
                _info.Created_By = user;
                _info.Created_Date = DateTime.Now;

                string path = CommonData.addressUploadFile;
                if (fileContent != null)
                {

                    string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssFFF")}" + fileContent.FileName;
                    bool statusUpload = new FileHelper().UpLoadFile(fileContent.OpenReadStream(), path, fileName);

                    if (!statusUpload)
                    {
                        return Json(new { success = -11, message = "Tải ảnh đại diện thất bại" });
                    }
                    _info.ImgLink = "https://localhost:44392/images/" + fileName;
                }
                

                _result = MemberCardDA.Insert(_info);

                if (_result > 0)
                {
                    _message = "Đăng ký thành công!";
                }
                else if (_result == -1001)
                {
                    _message = "Tên khách hàng này đã tồn tại trong hệ thống";
                }
                else if (_result == -1002)
                {
                    _message = "Số CCCD này đã tồn tại trong hệ thống";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = _result, message = _message });
        }



        [Route("cap-nhat/{id}"), HttpGet]
        public IActionResult Update(decimal id)
        {
            try
            {
                ViewBag.MenuActive = "memberCard";

                ViewBag.MemberCardInfo = MemberCardDA.GetById(id);
            }
            catch (Exception ex)
            {

            }
            return View("~/Area/Member/Views/MemberCard/_Partial_Update_MemberCard.cshtml");
        }

        [Route("cap-nhat"), HttpPost]
        public IActionResult Update(MemberCardInfo _info, IFormFile fileContent)
        {
            string _message = "Cập nhật thất bại!";
            decimal _result = -1;
            try
            {
                var user = "sondt24";
                _info.Modified_By = user;
                _info.Modified_Date = DateTime.Now;

                string path = CommonData.addressUploadFile;
                if (fileContent != null)
                {

                    string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssFFF")}" + fileContent.FileName;
                    bool statusUpload = new FileHelper().UpLoadFile(fileContent.OpenReadStream(), path, fileName);

                    if (!statusUpload)
                    {
                        return Json(new { success = -11, message = "Tải ảnh đại diện thất bại" });
                    }
                    _info.ImgLink = "https://localhost:44392/images/" + fileName;
                }


                _result = MemberCardDA.Update(_info);

                if (_result > 0)
                {
                    _message = "Cập nhật thành công!";
                }
                else if (_result == -1001)
                {
                    _message = "Tên khách hàng này đã tồn tại trong hệ thống";
                }
                else if (_result == -1002)
                {
                    _message = "Số CCCD này đã tồn tại trong hệ thống";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = _result, message = _message });
        }

        [Route("xoa"), HttpPost]
        public IActionResult Delete(decimal Id)
        {
            string _message = "Xóa khách hàng thất bại!";
            decimal _result = -1;
            try
            {
                var user = "sondt24";

                MemberCardInfo _info = new MemberCardInfo();
                _info.Id = Id;
                _info.Modified_By = user;
                _info.Modified_Date = DateTime.Now;

                _result = MemberCardDA.Delete(_info);

                if (_result > 0)
                {
                    _message = "Xóa khách hàng thành công!";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = _result, message = _message });
        }
    }
}
