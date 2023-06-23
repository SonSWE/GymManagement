using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommnLib
{
    public class CommonFunc
    {
        public static int GetFromToPaging(int pCurentPage, int pRecordOnPage, out int pToRecord)
        {
            pToRecord = -1;
            try
            {
                int _FromRecord = pRecordOnPage * (pCurentPage - 1) + 1;
                pToRecord = pRecordOnPage * pCurentPage;
                return _FromRecord;
            }
            catch (Exception ex)
            {
                pToRecord = -1;
                return -1;
            }
        }

        public static string ConvertNumberToVietnameseWords(int number)
        {
            if (number == 0)
                return "không";

            if (number < 0)
                return "âm " + ConvertNumberToVietnameseWords(Math.Abs(number));

            string[] unitsMap = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín", "mười", "mười một", "mười hai", "mười ba", "mười bốn", "mười năm", "mười sáu", "mười bảy", "mười tám", "mười chín" };
            string[] tensMap = { "", "", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };

            if (number < 20)
                return unitsMap[number];
            else if (number < 100)
            {
                int unitDigit = number % 10;
                int tensDigit = number / 10;

                if (unitDigit != 0)
                    return tensMap[tensDigit] + " " + unitsMap[unitDigit];
                else
                    return tensMap[tensDigit];
            }
            else if (number < 1000)
            {
                int hundredDigit = number / 100;
                int remainder = number % 100;

                if (remainder != 0)
                    return unitsMap[hundredDigit] + " trăm " + ConvertNumberToVietnameseWords(remainder);
                else
                    return unitsMap[hundredDigit] + " trăm";
            }
            else if (number < 1000000)
            {
                int thousandDigit = number / 1000;
                int remainder = number % 1000;

                if (remainder != 0)
                    return ConvertNumberToVietnameseWords(thousandDigit) + " nghìn " + ConvertNumberToVietnameseWords(remainder);
                else
                    return ConvertNumberToVietnameseWords(thousandDigit) + " nghìn";
            }
            else
            {
                int millionDigit = number / 1000000;
                int remainder = number % 1000000;

                if (remainder != 0)
                    return ConvertNumberToVietnameseWords(millionDigit) + " triệu " + ConvertNumberToVietnameseWords(remainder);
                else
                    return ConvertNumberToVietnameseWords(millionDigit) + " triệu";
            }
        }


        #region Pageing Ver 2

        public static string PagingData(int pCurPage, int pRecordOnPage, int pTotalRecord, string functionJavascript = "ChangePage", bool hasExportBtn = false)
        {
            try
            {
                int p_end = -1;
                int p_start = CommonFunc.GetFromToPaging(pCurPage, pRecordOnPage, out p_end);
                string pStrPaging = "";
                double _dobTotalRec = Convert.ToDouble(pTotalRecord);
                int _TotalPage = RoundUp(_dobTotalRec / pRecordOnPage);
                pStrPaging = WritePagingV2(p_start, p_end, _TotalPage, pCurPage, functionJavascript, pTotalRecord, pRecordOnPage, "bản ghi", hasExportBtn);
                return pStrPaging;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static int RoundUp(double pDoubleNum)
        {
            try
            {
                return Convert.ToInt32(Math.Ceiling(pDoubleNum).ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static string WritePagingV2(int p_start, int p_end, int iPageCount, int iCurrentPage, string functionJavascript, int iTotalRecords, int iPageSize, string pLoaiBanGhi, bool hasExportBtn = false)
        {
            try
            {
                string strPage = "";
                //strPage += "<div class='box-paging'><ul>";
                strPage += "<div class='box-paging'>";
                if (hasExportBtn)
                {
                    strPage += "<div><button id=\"btnExport\" class=\"btn-render\">Kết xuất <span><img src=\"../version_2/img/icon-render.svg\" alt=\"img\"></span></button></div>";
                }
                else
                {
                    strPage += "<div></div>";
                }
                strPage += "<div class=\"d-flex\" id='d_page'>";
                //strPage += "<div id='d_total_rec'>Tổng số " + iTotalRecords + " bản ghi </div>";
                if (iTotalRecords > 0)
                {
                    strPage += "<div class=\"number-record\" id='d_total_rec'>Hiển thị:  " + p_start + " - " + (p_end < iTotalRecords ? p_end : iTotalRecords) + " / " + iTotalRecords + " bản ghi </div>";
                }
                else
                {
                    strPage += "<div id='d_total_rec'>Hiển thị:  0 - " + (p_end < iTotalRecords ? p_end : iTotalRecords) + " / " + iTotalRecords + " bản ghi </div>";
                }
                strPage += "<div class='d_number_of_page' id='d_number_of_page'>";
                if (iPageCount <= 1)
                {
                    strPage += "</div>";
                    strPage += "</div>";
                    //strPage += "<ul>";
                    strPage += "</div>";
                    return strPage;
                }
                if (iCurrentPage > 1)
                {
                    strPage += "<button onclick=\"" + functionJavascript + "(1)\"><span id=\"first\"  href=\"\"><<</span></button>";
                    strPage += "<button onclick=\"" + functionJavascript + "(" + (iCurrentPage - 1) + ")\"><span id=\"back\"  href=\"\"><</span></button>";
                }
                if (iPageCount <= 5)
                {
                    for (int j = 0; j < iPageCount; j++)
                    {
                        if (iCurrentPage == (j + 1))
                        {
                            strPage += "<button onclick=\"" + functionJavascript + "(" + (j + 1) + ")\"><span class=\"a-active\" id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></button>";
                        }
                        else
                        {
                            strPage += "<button onclick=\"" + functionJavascript + "(" + (j + 1) + ")\"><span id=" + (j + 1) + "  href=\"\">" + (j + 1) + "</span></button>";
                        }
                    }
                }
                else
                {
                    string cl = "";
                    int t;
                    int pagePreview = 0;
                    //nếu đang ở trang 2 thì vẽ đc có 1 trang trước nó nên sẽ vẽ thêm 3 trang phía sau
                    //default là vẽ 2 trang trc 2 trang sau
                    int soTrangVeLui = 2;
                    if ((iPageCount - iCurrentPage) == 1)
                    {
                        soTrangVeLui = soTrangVeLui + 1;
                    }
                    else if ((iPageCount - iCurrentPage) == 0)
                    {
                        soTrangVeLui = soTrangVeLui + 2;
                    }
                    for (t = iCurrentPage - soTrangVeLui; t <= iCurrentPage; t++) //ve truoc 2 trang
                    {
                        if (t < 1) continue;
                        cl = t == iCurrentPage ? "a-active" : "";
                        strPage += t == iCurrentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                    : "<button  onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        pagePreview++;
                    }
                    t = 0;
                    cl = "";
                    if (iCurrentPage == 1) //truong hop trang dau tien thi ve du 5 trang
                    {
                        for (t = iCurrentPage + 1; t < iCurrentPage + 5; t++)
                        {
                            if (t >= t + 5 || t > iPageCount) continue;
                            cl = t == iCurrentPage ? "a-active" : "";
                            strPage += t == iCurrentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                     : "<button   onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        }
                    }
                    else if (iCurrentPage > 1)  //truogn hop ma la trang lon hon 1 thi se ve 4 trang ke tiep + 1 trang truoc
                    {
                        int incr = 5 - (pagePreview - 1);
                        for (t = iCurrentPage + 1; t < iCurrentPage + incr; t++)
                        {
                            if (t >= t + incr || t > iPageCount) continue;
                            cl = t == iCurrentPage ? "a-active" : "";
                            strPage += t == iCurrentPage ? "<button onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>"
                                     : "<button   onclick=\"" + functionJavascript + "(" + (t) + ")\"><span class='" + cl + "' id=" + (t) + "  href=\"\">" + (t) + "</span></button>";
                        }
                    }
                }
                if (iCurrentPage < iPageCount)
                {
                    strPage += "<button onclick=\"" + functionJavascript + "(" + (iCurrentPage + 1) + ")\"><span id=\"next\"  href=\"\">></span></button>";
                    strPage += "<button onclick=\"" + functionJavascript + "(" + iPageCount + ")\"><span id=\"end\"  href=\"\">>></span></button>";
                }
                strPage += "</div>";
                strPage += "</div>";
                //strPage += "<ul>";
                strPage += "</div>";
                return strPage;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
