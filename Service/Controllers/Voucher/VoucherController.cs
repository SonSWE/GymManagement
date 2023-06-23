using DataAccess;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Service.Controllers.Voucher
{
    [Route("api/quan-tri")]
    public class VoucherController : Controller
    {
        [Route("voucher/search"), HttpGet]
        public IEnumerable<VoucherPackgeInfo> Search(string keySearch)
        {
            try
            {
                
                List<VoucherPackgeInfo> _lst = VoucherPackgeDA.Search(keySearch);
                return _lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("voucher/insert"), HttpPost]
        public IActionResult Insert([FromBody] VoucherPackgeInfo info)
        {
            try
            {
                info.Description = info.Description != null ? info.Description : "";
                decimal _result = VoucherPackgeDA.Insert(info);
                return Json(new { success = _result.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { success = "-1" });
            }
        }

        [Route("voucher/get-by-id"), HttpGet]
        public ActionResult<VoucherPackgeInfo> GetById(string voucherCode)
        {
            try
            {
                return VoucherPackgeDA.GetByVoucherCode(voucherCode);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("voucher/update"), HttpPut]
        public IActionResult Update([FromBody] VoucherPackgeInfo info)
        {
            try
            {

                decimal _result = VoucherPackgeDA.Update(info);
                return Json(new { success = _result.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { success = "-1" });
            }
        }

        [Route("voucher/delete"), HttpDelete]
        public IActionResult DeleteNews(string voucherCode)
        {
            try
            {
                decimal _result = VoucherPackgeDA.Delete(voucherCode);
                return Json(new { success = _result.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { success = "-1" });
            }
        }
    }
}
