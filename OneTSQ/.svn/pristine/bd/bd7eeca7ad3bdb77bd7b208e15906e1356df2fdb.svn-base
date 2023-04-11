using OneTSQ.Core.Model;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneTSQ.WebParts
{
    public class TimeFilterView
    {
        public int? thoiGian { get; set; }
        public int? nam { get; set; }
        public int? quy { get; set; }
        public int? thang { get; set; }
        private DateTime? _tuNgay;
        public DateTime? tuNgay
        {
            get
            {
                if (thoiGian == (int)TimeFilter.eThoiGian.nam)
                {
                    if (nam != null)
                        return new DateTime(nam.Value, 1, 1);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.quy)
                {
                    if (nam != null && quy != null)
                        return new DateTime(nam.Value, quy.Value * 3 - 2, 1);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.thang)
                {
                    if (nam != null && thang != null)
                        return new DateTime(nam.Value, thang.Value, 1);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.ngay)
                {
                    return _tuNgay;
                }
                return null;
            }
            set { _tuNgay = value; }
        }
        private DateTime? _denNgay;
        public DateTime? denNgay
        {
            get
            {
                if (thoiGian == (int)TimeFilter.eThoiGian.nam)
                {
                    if (nam != null)
                        return new DateTime(nam.Value, 12, 31);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.quy)
                {
                    if (nam != null && quy != null)
                        return new DateTime(nam.Value, quy.Value * 3, quy == (int)TimeFilter.eQuy.I || quy == (int)TimeFilter.eQuy.IV ? 31 : 30);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.thang)
                {
                    if (nam != null && thang != null)
                        return new DateTime(nam.Value, thang.Value, (new DateTime(nam.Value, thang.Value, 1)).AddMonths(1).AddDays(-1).Day);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.ngay)
                {
                    return _denNgay;
                }
                return null;
            }
            set { _denNgay = value; }
        }
        public string sTuNgay
        {
            set
            {
                if (value != null)
                    _tuNgay = DateTime.ParseExact(value, "HH:mm dd/MM/yyyy", null);
                else _tuNgay = null;
            }
        }
        public string sDenNgay
        {
            set
            {
                if (value != null)
                    _denNgay = DateTime.ParseExact(value, "HH:mm dd/MM/yyyy", null);
                else _denNgay = null;
            }
        }
        public string thoiGianDisplay
        {
            get
            {
                if (thoiGian == (int)TimeFilter.eThoiGian.nam)
                {
                    if (nam != null)
                        return "Năm " + nam;
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.quy)
                {
                    if (nam != null && quy != null)
                        return string.Format("Quý {0} năm {1}", TimeFilter.Quys.Where(q => q.Key == quy).Select(q => q.Value).FirstOrDefault(), nam.Value);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.thang)
                {
                    if (nam != null && thang != null)
                        return string.Format("Tháng {0} năm {1}", thang.Value, nam.Value);
                    else return null;
                }
                else if (thoiGian == (int)TimeFilter.eThoiGian.ngay && tuNgay != null && denNgay != null)
                {
                    return string.Format("Từ ngày {0} đến ngày {1}", tuNgay.Value.ToString("HH:mm dd/MM/yyyy"), denNgay.Value.ToString("HH:mm dd/MM/yyyy"));
                }
                return null;
            }
        }
    }
    public class TimeFilter
    {
        #region Tạo view cho TimeFilter

        public enum eThoiGian
        {
            [Description("0: Năm")]
            nam = 0,
            [Description("1: Quý")]
            quy = 1,
            [Description("2: Tháng")]
            thang = 2,
            [Description("3: Ngày")]
            ngay = 3
        }
        public static Dictionary<int, string> ThoiGians = new Dictionary<int, string>()
        {
            { (int)eThoiGian.nam, "Năm" },
            { (int)eThoiGian.quy, "Quý" },
            { (int)eThoiGian.thang, "Tháng" },
            { (int)eThoiGian.ngay, "Giai đoạn" },
        };
        public enum eQuy
        {
            [Description("1: Quý I")]
            I = 1,
            [Description("2: Quý II")]
            II = 2,
            [Description("3: Quý III")]
            III = 3,
            [Description("4: Quý IV")]
            IV = 4,
        }
        public static Dictionary<int, string> Quys = new Dictionary<int, string>()
        {
            { (int)eQuy.I, "I" },
            { (int)eQuy.II, "II" },
            { (int)eQuy.III, "III" },
            { (int)eQuy.IV, "IV" },
        };


        #endregion
        #region TimeFilter dùng hiển thị trước khi $(document).ready()
        public static AjaxOut Draw(RenderInfoCls renderInfo, TimeFilterView timeFilterView)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(renderInfo);
                WebSession.CheckSessionTimeOut(renderInfo);
                string cbbThoiGian =
                "<select id = 'cbbThoiGian' title = 'Loại thời gian' class=\"selectpicker width150px\">\r\n";
                foreach (var loaiTg in TimeFilter.ThoiGians)
                {
                    cbbThoiGian += string.Format("<option value={0} {1}>{2}</option>\r\n", loaiTg.Key, timeFilterView != null && timeFilterView.thoiGian == loaiTg.Key ? "selected" : null, loaiTg.Value);
                }
                cbbThoiGian += "</select>\r\n";

                string cbbQuy =
                "<select id = 'cbbQuy' title = 'Quý' class=\"selectpicker width100px\">\r\n";
                foreach (var quy in TimeFilter.Quys)
                {
                    cbbQuy += string.Format("<option value={0} {1}>{2}</option>\r\n", quy.Key, timeFilterView != null && timeFilterView.quy == quy.Key ? "selected" : null, quy.Value);
                }
                cbbQuy += "</select>\r\n";

                string cbbThang =
                "<select id = 'cbbThang' title = 'Tháng' class=\"selectpicker width100px\">\r\n";
                for (int thang = 1; thang <= 12; thang++)
                    cbbThang += string.Format("<option value={0} {1}>{0}</option>\r\n", thang, timeFilterView != null && timeFilterView.thang == thang ? "selected" : null);
                cbbThang += "</select>\r\n";

                string Html = "";
                #region Javascript
                Html = WebEnvironments.ProcessJavascript("<script> \r\n" +
                "    $(document).ready(function () { \r\n" +
                //"       $('.selectpicker').selectpicker({ \r\n" +
                ////      "  style: 'btn-info', \r\n" +
                ////      "  size: 4 \r\n" +
                //"       }); \r\n" +

                "       $('#dtTuNgay').datetimepicker({ \r\n" +
                "           format: 'HH:mm DD/MM/YYYY' \r\n" +
                "       }); \r\n" +

                "       $('#dtDenNgay').datetimepicker({ \r\n" +
                "           format: 'HH:mm DD/MM/YYYY' \r\n" +
                "       }); \r\n" +

                //"       $('#dtNam').datepicker({ \r\n" +
                //"           minViewMode: 2, \r\n" +
                //"           format: 'yyyy' \r\n" +
                //"       }); \r\n" +
                "       $('#dtNam').datetimepicker({ \r\n" +
                "           format: 'YYYY' \r\n" +
                "       }); \r\n" +

                "        var thoiGian = $('#cbbThoiGian').val(); \r\n" +
                "        showHide(thoiGian); \r\n" +

                "       $('#cbbThoiGian').change( function() { \r\n" +
                "           $(this).find(':selected').each(function () { \r\n" +
                "                var thoiGian = $(this).val(); \r\n" +
                "                showHideChange(thoiGian); \r\n" +
                "            }); \r\n" +
                "       }); \r\n" +

                "    }); \r\n" +
                #region ConvertDateTime from string to datetime
                "function convertDateTime(sDateTime){ \r\n" +
                "    dateTime = sDateTime.split(' '); \r\n" +

                "    var time = dateTime[0].split(':'); \r\n" +
                "    var h = time[0]; \r\n" +
                "    var m = time[1]; \r\n" +
                "    var s = 0; \r\n" +

                "    var date = dateTime[1].split('/'); \r\n" +
                "    var dd = date[0]; \r\n" +
                "    var mm = date[1]; \r\n" +
                "    var yyyy = date[2]; \r\n" +

                "    return new Date(yyyy,mm,dd,h,m,s); \r\n" +
                "} \r\n" +
                #endregion

                #region Ẩn hiện các control
                "    function cbbThoiGian_SelectedIndexChanged(s, e) { \r\n" +
                "        var thoiGian = $('#cbbThoiGian').val(); \r\n" +
                "        showHideChange(thoiGian); \r\n" +
                "    } \r\n" +
                "    function showHide(thoiGian) { \r\n" +
                "        if (thoiGian == '') \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').hide(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                "        } \r\n" +
                "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.nam + ")//năm \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').show(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                "        } \r\n" +
                "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.quy + ")//quý \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').show(); \r\n" +
                "            $('#nam').show(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                "        } \r\n" +
                "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.thang + ")//tháng \r\n" +
                "        { \r\n" +
                "            $('#thang').show(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').show(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                "        } \r\n" +
                "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.ngay + ")//ngày \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').hide(); \r\n" +
                "            $('#tu_ngay').show(); \r\n" +
                "            $('#den_ngay').show(); \r\n" +
                "        } \r\n" +
                "    } \r\n" +

                "    function showHideChange(thoiGian) { \r\n" +
                "        var now = new Date(); \r\n" +
                "        if (thoiGian == '')  \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').hide(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                "        } \r\n" +
                "        if (thoiGian == " + (int)TimeFilter.eThoiGian.nam + ")//năm \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').show(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                //"            $('#dtNam').val(now.getFullYear()); \r\n" +
                "        } \r\n" +
                "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.quy + ")//quý \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').show(); \r\n" +
                "            $('#nam').show(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                //"            $('#cbbQuy').val(Math.floor(now.getMonth() / 3) + 1) \r\n" +
                //"            $('#dtNam').val(now.getFullYear()); \r\n" +
                "        } \r\n" +
                "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.thang + ")//tháng \r\n" +
                "        { \r\n" +
                "            $('#thang').show(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').show(); \r\n" +
                "            $('#tu_ngay').hide(); \r\n" +
                "            $('#den_ngay').hide(); \r\n" +
                //"            $('#cbbThang').val(now.getMonth() + 1) \r\n" +
                //"            $('#dtNam').val(now.getFullYear()); \r\n" +
                "        } \r\n" +
                "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.ngay + ")//ngày \r\n" +
                "        { \r\n" +
                "            $('#thang').hide(); \r\n" +
                "            $('#quy').hide(); \r\n" +
                "            $('#nam').hide(); \r\n" +
                "            $('#tu_ngay').show(); \r\n" +
                "            $('#den_ngay').show(); \r\n" +
                //"            $('#tu_ngay').val(now); \r\n" +
                //"            $('#den_ngay').val(now); \r\n" +
                "        } \r\n" +
                "    } \r\n" +
                #endregion

                #region Check dữ liệu đầu vào
                "function checkTimeInput()\r\n" +
                 "{\r\n" +
                 "   var thoiGian = $('#cbbThoiGian').val();\r\n" +
                 "   if(thoiGian == '')\r\n" +
                 "   {\r\n" +
                 "       callSweetAlert('Xin chọn loại thời gian.');\r\n" +
                 "       return false;\r\n" +
                 "   }\r\n" +
                 "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.nam + ")\r\n" +
                 "   {\r\n" +
                 "       if($('#dtNam').val() == '')\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Xin chọn năm.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "   }\r\n" +
                 "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.quy + ")\r\n" +
                 "   {\r\n" +
                 "       if($('#cbbQuy').val() == '')\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Xin chọn quý.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "       if($('#dtNam').val() == '')\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Xin chọn năm.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "   }\r\n" +
                 "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.thang + ")\r\n" +
                 "   {\r\n" +
                 "       if($('#cbbThang').val() == '')\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Xin chọn tháng.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "       if($('#dtNam').val() == '')\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Xin chọn năm.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "   }\r\n" +
                 "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.ngay + ")\r\n" +
                 "   {\r\n" +
                 "       if($('#dtTuNgay').val() == '')\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Xin chọn từ ngày.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "       if($('#dtDenNgay').val() == '')\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Xin chọn đến ngày.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "       if(convertDateTime($('#dtDenNgay').val()) < convertDateTime($('#dtTuNgay').val()))\r\n" +
                 "       {\r\n" +
                 "           callSweetAlert('Đến ngày phải lớn hơn từ ngày.');\r\n" +
                 "           return false;\r\n" +
                 "       }\r\n" +
                 "   }\r\n" +
                 "   return true;\r\n" +
                 "}\r\n" +
                #endregion

                #region Tao doi tuong TimeFilterView
                "   function TimeFilterView(thoiGian, nam, quy, thang, tuNgay, denNgay) { \r\n" +
                "       this.thoiGian = thoiGian == '' ? null : thoiGian; \r\n" +
                "       this.nam = nam == '' ? null : nam; \r\n" +
                "       this.quy = quy == '' ? null : quy; \r\n" +
                "       this.thang = thang == '' ? null : thang; \r\n" +
                "       this.sTuNgay = tuNgay == '' ? null : tuNgay; \r\n" +
                "       this.sDenNgay = denNgay == '' ? null : denNgay; \r\n" +
                "   } \r\n" +
                #endregion

                #region Get dữ liệu đầu vào
                "function GetTimeInput()\r\n" +
                 "{\r\n" +
                 "   return new TimeFilterView($('#cbbThoiGian').val(), $('#dtNam').val(), $('#cbbQuy').val(), $('#cbbThang').val(), $('#dtTuNgay').val(), $('#dtDenNgay').val());\r\n" +
                 "}\r\n" +
                #endregion
            "</script>");

                #endregion
                Html += WebEnvironments.ProcessHtml(
                    "<table>\r\n" +
                    "    <tr>\r\n" +
                    "        <td padding-left: 11px; padding-right:5px'>\r\n" +
                    cbbThoiGian +
                    "        </td>\r\n" +
                    "        <td padding-right: 5px'>\r\n" +
                    "            <div id='thang' style='display:none'>\r\n" +
                    cbbThang +
                    "            </div>\r\n" +
                    "            <div id='quy' style='display:none'>\r\n" +
                    cbbQuy +
                    "            </div>\r\n" +
                    "            <div id='tu_ngay'>\r\n" +
                    "                <input type='text' id='dtTuNgay' value='" + (timeFilterView != null && timeFilterView.tuNgay != null ? timeFilterView.tuNgay.Value.ToString("HH:mm dd/MM/yyyy") : null) + "' class='form-control' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + "'>\r\n" +
                    "            </div>\r\n" +
                    "        </td>\r\n" +
                    "        <td>\r\n" +
                    "            <div id='nam' style='display:none'>\r\n" +
                    "                <input type='text' id='dtNam' value='" + (timeFilterView != null ? timeFilterView.nam : null) + "' class='form-control' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Năm") + "'>\r\n" +
                    "            </div>\r\n" +
                    "            <div id='den_ngay'>\r\n" +
                    "                <input type='text' id='dtDenNgay' value='" + (timeFilterView != null && timeFilterView.denNgay != null ? timeFilterView.denNgay.Value.ToString("HH:mm dd/MM/yyyy") : null) + "' class='form-control' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Đến ngày") + "'>\r\n" +
                    "            </div>\r\n" +
                    "        </td>\r\n" +
                    "    </tr>\r\n" +
                    "</table>\r\n"
                );
                RetAjaxOut.HtmlContent = Html;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        #endregion

        #region TimeFilter dùng hiển thị sau khi $(document).ready()
        public static string JavaScriptFollowHtml()//Viết sau html
        {
            string javaScript =
            "       $('.selectpicker').selectpicker({ \r\n" +
            "       }); \r\n" +

            "       $('#dtTuNgay').datetimepicker({ \r\n" +
            "           format: 'HH:mm DD/MM/YYYY' \r\n" +
            "       }); \r\n" +

            "       $('#dtDenNgay').datetimepicker({ \r\n" +
            "           format: 'HH:mm DD/MM/YYYY' \r\n" +
            "       }); \r\n" +

            "       $('#dtNam').datepicker({ \r\n" +
            "           minViewMode: 2, \r\n" +
            "           format: 'yyyy' \r\n" +
            "       }); \r\n" +

            "        var thoiGian = $('#cbbThoiGian').val(); \r\n" +
            "        showHide(thoiGian); \r\n" +

            "       $('#cbbThoiGian').change( function() { \r\n" +
            "           $(this).find(':selected').each(function () { \r\n" +
            "                var thoiGian = $(this).val(); \r\n" +
            "                showHideChange(thoiGian); \r\n" +
            "            }); \r\n" +
            "       }); \r\n";
            return javaScript;
        }
        public static string JavaScript()
        {
            string javaScript = WebEnvironments.ProcessJavascript("<script> \r\n" +

            #region ConvertDateTime from string to datetime
            "function convertDateTime(sDateTime){ \r\n" +
            "    dateTime = sDateTime.split(' '); \r\n" +

            "    var time = dateTime[0].split(':'); \r\n" +
            "    var h = time[0]; \r\n" +
            "    var m = time[1]; \r\n" +
            "    var s = 0; \r\n" +

            "    var date = dateTime[1].split('/'); \r\n" +
            "    var dd = date[0]; \r\n" +
            "    var mm = date[1]-1; \r\n" +
            "    var yyyy = date[2]; \r\n" +

            "    return new Date(yyyy,mm,dd,h,m,s); \r\n" +
            "} \r\n" +
            #endregion

            #region Ẩn hiện các control
            "    function cbbThoiGian_SelectedIndexChanged(s, e) { \r\n" +
            "        var thoiGian = $('#cbbThoiGian').val(); \r\n" +
            "        showHideChange(thoiGian); \r\n" +
            "    } \r\n" +
            "    function showHide(thoiGian) { \r\n" +
            "        if (thoiGian == '') \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').hide(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            "        } \r\n" +
            "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.nam + ")//năm \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').show(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            "        } \r\n" +
            "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.quy + ")//quý \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').show(); \r\n" +
            "            $('#nam').show(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            "        } \r\n" +
            "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.thang + ")//tháng \r\n" +
            "        { \r\n" +
            "            $('#thang').show(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').show(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            "        } \r\n" +
            "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.ngay + ")//ngày \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').hide(); \r\n" +
            "            $('#tu_ngay').show(); \r\n" +
            "            $('#den_ngay').show(); \r\n" +
            "        } \r\n" +
            "    } \r\n" +

            "    function showHideChange(thoiGian) { \r\n" +
            "        var now = new Date(); \r\n" +
            "        if (thoiGian == '')  \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').hide(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            "        } \r\n" +
            "        if (thoiGian == " + (int)TimeFilter.eThoiGian.nam + ")//năm \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').show(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            //"            $('#dtNam').val(now.getFullYear()); \r\n" +
            "        } \r\n" +
            "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.quy + ")//quý \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').show(); \r\n" +
            "            $('#nam').show(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            //"            $('#cbbQuy').val(Math.floor(now.getMonth() / 3) + 1) \r\n" +
            //"            $('#dtNam').val(now.getFullYear()); \r\n" +
            "        } \r\n" +
            "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.thang + ")//tháng \r\n" +
            "        { \r\n" +
            "            $('#thang').show(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').show(); \r\n" +
            "            $('#tu_ngay').hide(); \r\n" +
            "            $('#den_ngay').hide(); \r\n" +
            //"            $('#cbbThang').val(now.getMonth() + 1) \r\n" +
            //"            $('#dtNam').val(now.getFullYear()); \r\n" +
            "        } \r\n" +
            "        else if (thoiGian == " + (int)TimeFilter.eThoiGian.ngay + ")//ngày \r\n" +
            "        { \r\n" +
            "            $('#thang').hide(); \r\n" +
            "            $('#quy').hide(); \r\n" +
            "            $('#nam').hide(); \r\n" +
            "            $('#tu_ngay').show(); \r\n" +
            "            $('#den_ngay').show(); \r\n" +
            //"            $('#tu_ngay').val(now); \r\n" +
            //"            $('#den_ngay').val(now); \r\n" +
            "        } \r\n" +
            "    } \r\n" +
            #endregion

            #region Check dữ liệu đầu vào
            "function checkTimeInput()\r\n" +
             "{\r\n" +
             "   var thoiGian = $('#cbbThoiGian').val();\r\n" +
             "   if(thoiGian == '')\r\n" +
             "   {\r\n" +
             "       callSweetAlert('Xin chọn loại thời gian.');\r\n" +
             "       return false;\r\n" +
             "   }\r\n" +
             "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.nam + ")\r\n" +
             "   {\r\n" +
             "       if($('#dtNam').val() == '')\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Xin chọn năm.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "   }\r\n" +
             "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.quy + ")\r\n" +
             "   {\r\n" +
             "       if($('#cbbQuy').val() == '')\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Xin chọn quý.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "       if($('#dtNam').val() == '')\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Xin chọn năm.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "   }\r\n" +
             "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.thang + ")\r\n" +
             "   {\r\n" +
             "       if($('#cbbThang').val() == '')\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Xin chọn tháng.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "       if($('#dtNam').val() == '')\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Xin chọn năm.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "   }\r\n" +
             "   else if(thoiGian == " + (int)TimeFilter.eThoiGian.ngay + ")\r\n" +
             "   {\r\n" +
             "       if($('#dtTuNgay').val() == '')\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Xin chọn từ ngày.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "       if($('#dtDenNgay').val() == '')\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Xin chọn đến ngày.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "       if(convertDateTime($('#dtDenNgay').val()) < convertDateTime($('#dtTuNgay').val()))\r\n" +
             "       {\r\n" +
             "           callSweetAlert('Đến ngày phải lớn hơn từ ngày.');\r\n" +
             "           return false;\r\n" +
             "       }\r\n" +
             "   }\r\n" +
             "   return true;\r\n" +
             "}\r\n" +
            #endregion

            #region Tao doi tuong TimeFilterView
            "   function TimeFilterView(thoiGian, nam, quy, thang, tuNgay, denNgay) { \r\n" +
            "       this.thoiGian = thoiGian == '' ? null : thoiGian; \r\n" +
            "       this.nam = nam == '' ? null : nam; \r\n" +
            "       this.quy = quy == '' ? null : quy; \r\n" +
            "       this.thang = thang == '' ? null : thang; \r\n" +
            "       this.sTuNgay = tuNgay == '' ? null : tuNgay; \r\n" +
            "       this.sDenNgay = denNgay == '' ? null : denNgay; \r\n" +
            "   } \r\n" +
            #endregion

            #region Get dữ liệu đầu vào
            "function GetTimeInput()\r\n" +
             "{\r\n" +
             "   return new TimeFilterView($('#cbbThoiGian').val(), $('#dtNam').val(), $('#cbbQuy').val(), $('#cbbThang').val(), $('#dtTuNgay').val(), $('#dtDenNgay').val());\r\n" +
             "}\r\n" +
            #endregion
        "</script>");
            return javaScript;
        }
        public static AjaxOut Html(RenderInfoCls renderInfo, TimeFilterView timeFilterView)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam
                    OSiteParam = WebEnvironments.CreateSiteParam(renderInfo);
                WebSession.CheckSessionTimeOut(renderInfo);
                string cbbThoiGian =
                "<select id = 'cbbThoiGian' title = 'Loại thời gian' class=\"selectpicker width150px\">\r\n";
                foreach (var loaiTg in TimeFilter.ThoiGians)
                {
                    cbbThoiGian += string.Format("<option value={0} {1}>{2}</option>\r\n", loaiTg.Key, timeFilterView != null && timeFilterView.thoiGian == loaiTg.Key ? "selected" : null, loaiTg.Value);
                }
                cbbThoiGian += "</select>\r\n";

                string cbbQuy =
                "<select id = 'cbbQuy' title = 'Quý' class=\"selectpicker width100px\">\r\n";
                foreach (var quy in TimeFilter.Quys)
                {
                    cbbQuy += string.Format("<option value={0} {1}>{2}</option>\r\n", quy.Key, timeFilterView != null && timeFilterView.quy == quy.Key ? "selected" : null, quy.Value);
                }
                cbbQuy += "</select>\r\n";

                string cbbThang =
                "<select id = 'cbbThang' title = 'Tháng' class=\"selectpicker width100px\">\r\n";
                for (int thang = 1; thang <= 12; thang++)
                    cbbThang += string.Format("<option value={0} {1}>{0}</option>\r\n", thang, timeFilterView != null && timeFilterView.thang == thang ? "selected" : null);
                cbbThang += "</select>\r\n";

                string Html = WebEnvironments.ProcessHtml(
                    "<table>\r\n" +
                    "    <tr>\r\n" +
                    "        <td padding-left: 11px; padding-right:5px'>\r\n" +
                    cbbThoiGian +
                    "        </td>\r\n" +
                    "        <td padding-right: 5px'>\r\n" +
                    "            <div id='thang' style='display:none'>\r\n" +
                    cbbThang +
                    "            </div>\r\n" +
                    "            <div id='quy' style='display:none'>\r\n" +
                    cbbQuy +
                    "            </div>\r\n" +
                    "            <div id='tu_ngay'>\r\n" +
                    "                <input type='text' id='dtTuNgay' value='" + (timeFilterView != null && timeFilterView.tuNgay != null ? timeFilterView.tuNgay.Value.ToString("HH:mm dd/MM/yyyy") : null) + "' class='form-control' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Từ ngày") + "'>\r\n" +
                    "            </div>\r\n" +
                    "        </td>\r\n" +
                    "        <td>\r\n" +
                    "            <div id='nam' style='display:none'>\r\n" +
                    "                <input type='text' id='dtNam' value='" + (timeFilterView != null ? timeFilterView.nam : null) + "' class='form-control' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Năm") + "'>\r\n" +
                    "            </div>\r\n" +
                    "            <div id='den_ngay'>\r\n" +
                    "                <input type='text' id='dtDenNgay' value='" + (timeFilterView != null && timeFilterView.denNgay != null ? timeFilterView.denNgay.Value.ToString("HH:mm dd/MM/yyyy") : null) + "' class='form-control' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Đến ngày") + "'>\r\n" +
                    "            </div>\r\n" +
                    "        </td>\r\n" +
                    "    </tr>\r\n" +
                    "</table>\r\n"
                );
                RetAjaxOut.HtmlContent = Html;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        #endregion
    }
}
