﻿using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.Utility;
using System;
using System.Linq;
using System.Data;
using OneTSQ.CheckPermissionUtility;
using OneTSQ.Permission;
using OneTSQ.Core.Call.Bussiness.Utility;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;
using System.Web;
using OneTSQ.Common;
using FlexCel.Report;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{   
    public class DanhSachPhieuKhaoSat : WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "DanhSachPhieuKhaoSat";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách phiếu đăng ký khảo sát";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách phiếu đăng ký khảo sát";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DanhSachPhieuKhaoSat), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();

            int pageIndex = string.IsNullOrEmpty(WebEnvironments.Request("PageIndex")) ? 0 : int.Parse(WebEnvironments.Request("PageIndex"));
            string keyword = WebEnvironments.Request("Keyword");
            string tendonvikhaosat_ID = WebEnvironments.Request("BennhVien_ID");
            int? trangThai = string.IsNullOrEmpty(WebEnvironments.Request("TrangThai")) ? null : (int?)int.Parse(WebEnvironments.Request("TrangThai"));
            if (trangThai < 0)
                trangThai = null;
            try
            {
                string SessionId = System.Guid.NewGuid().ToString();              
                string cbbtendonvikhaosat = "";
                if (!string.IsNullOrEmpty(tendonvikhaosat_ID))
                {
                    var BenhViens = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), tendonvikhaosat_ID);
                    if (BenhViens != null)
                        cbbtendonvikhaosat += string.Format("<option value={0}>{1}</option>", BenhViens.Id, BenhViens.Ten);
                }

                string cbbTrangThai = "";
                foreach (var tt in PhieuKhaoSatBenhVienVeTinhParser.TrangThais)
                    cbbTrangThai += "<option value=" + tt.Key + " " + (trangThai == tt.Key ? "selected" : "") + ">" + tt.Value + "</option>";

                #region Javascript
                RetAjaxOut.HtmlContent =
                    WebEnvironments.ProcessJavascript(
                     "<script language=\"javascript\">\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách phiếu đăng ký khảo sát") + "';\r\n" +
                    "       $('#cbbtendonvikhaosat').html('" + cbbtendonvikhaosat + "');\r\n" +            
                    "       CallInitSelect2('cbbtendonvikhaosat', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "');\r\n" +
                    "       cbbTrangThai.innerHTML = '" + cbbTrangThai + "';\r\n" +
                    "       $('#cbbTrangThai').select2({\r\n" +
                    "          placeholder: 'Trạng thái',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       $('#cbbTrangThai').select2('val', " + (trangThai == null ? "null" : trangThai.ToString()) + ");\r\n" +
                    "   });\r\n" +

                    "   var CurrentPageIndex=0;\r\n" +

                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       CurrentPageIndex = PageIndex;\r\n" +
                    "       setTimeout('Search()',10);\r\n" +
                    "   }\r\n" +

                    "   function Search()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       tendonvikhaosat = document.getElementById('cbbtendonvikhaosat').value;\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DanhSachPhieuKhaoSat.DrawSearchResult(RenderInfo, CurrentPageIndex, keyword, tendonvikhaosat, trangThai).value;\r\n" +
                    "       document.getElementById('divPhieuKhaoSat').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +
                    "   function FilterChange(){\r\n" +
                    "       CurrentPageIndex = 0;\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +
                #region xuất
                    "   function ExPort()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo=CreateRenderInfo();\r\n" +
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       ChucDanh = document.getElementById('cbbChucDanh').value;\r\n" +
                    "       tendonvikhaosat = document.getElementById('cbbtendonvikhaosat').value;\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut  = OneTSQ.WebParts.DanhSachPhieuKhaoSat.Print(RenderInfo, keyword, ChucDanh, tendonvikhaosat, trangThai).value;\r\n" +
                    "       if(AjaxOut.Error)\r\n" +
                    "       {\r\n" +
                    "           callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                    "           return;\r\n" +
                    "       }\r\n" +
                    "       window.open(AjaxOut.RetUrl, 'Download');\r\n" +
                    "       callSweetAlert(\"" + WebLanguage.GetLanguage(OSiteParam, "Xuất thành công") + "!\");\r\n" +
                    "   }\r\n" +
                #endregion
                    "</script>\r\n") +
                #endregion
                #region html
                    WebEnvironments.ProcessHtml(
                    "<div id=\"divListForm\">\r\n" +
                    " <div class=\"ibox float-e-margins\"> \r\n" +
                    "     <div class=\"ibox-title\"> \r\n" +
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách đăng ký phiếu khảo sát") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "           <div id=\"divPhieuKhaoSat\">" + DanhSachPhieuKhaoSat.DrawSearchResult(ORenderInfo, pageIndex, keyword, tendonvikhaosat_ID, trangThai).HtmlContent + "</div>\r\n" +
                    "     </div> \r\n" +
                    " </div>\r\n" +
                    "</div>\r\n"
                        );
                #endregion
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        #region Vẽ giao diện
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut DrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex, string keyword, string tendonvikhaosat, int? trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);

                PhieuKhaoSatBenhVienVeTinhFilterCls filter = new PhieuKhaoSatBenhVienVeTinhFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    Keyword = keyword,
                    BenhVien_ID = tendonvikhaosat,                
                    TrangThai = trangThai,
                };
                long recordTotal = 0;
                PhieuKhaoSatBenhVienVeTinhCls[] PhieuKhaoSat = CallBussinessUtility.CreateBussinessProcess().CreatePhieuKhaoSatBenhVienVeTinhProcess().PageReading(ORenderInfo, filter, ref recordTotal);
                int dangKyTotal = PhieuKhaoSat.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, filter.PageIndex, filter.PageSize, 10, "NextPage");
                string Html = "";
                Html +=
                    "         <div class=\"table-responsive\"> \r\n" +
                    "             <table class=\"table table-bordered table-hover dataTables-autosort footable-loaded footable\"> \r\n" +
                    "                 <thead> \r\n" +
                    "                 <tr> \r\n" +
                    "                     <th class=\"th-func-20\">STT</th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Mã phiếu") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Thời gian lập") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Tên đơn vị khảo sát") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Bác sĩ") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Y sĩ") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Đ.Dưỡng") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "KTV") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Dược sĩ") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Khác") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giường KH") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Giường TK") + " </th> \r\n" +
                    "                     <th>" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + " </th> \r\n" +
                    "                 </tr> \r\n" +
                    "                 </thead> \r\n" +
                    "                 <tbody> \r\n";
                for (int iIndex = 0; iIndex < dangKyTotal; iIndex++)
                {
                    var PhieuKhaoSatUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "PhieuKhaoSatBenhVienVeTinh",
                        new WebParamCls[] { new WebParamCls("id", PhieuKhaoSat[iIndex].ID),
                            new WebParamCls("RecordTotal", recordTotal.ToString()),
                            new WebParamCls("TrangThai", trangThai == null ? -1 : trangThai.Value)});
                    string tendonvi = "";
                    var BenhViens = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), PhieuKhaoSat[iIndex].BENHVIEN_ID);
                    if (BenhViens != null)
                    {
                        tendonvi = BenhViens.Ten;
                    }
                        Html +=
                        "                 <tr> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (filter.PageIndex * filter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].MA + "</a></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].THOIGIAN.Value.ToString("HH:mm dd/MM/yyyy") + "</a></td> \r\n" +
                        "                     <td style='vertical-align: middle;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + tendonvi + "</a></td> \r\n" +
                        "                     <td style='text-align: center;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOLUONGBACSI + "</a></td> \r\n" +
                        "                     <td style='text-align: center;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOLUONGYSI + "</a></td> \r\n" +
                        "                     <td style='text-align: center;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOLUONGDIEUDUONG + "</a></td> \r\n" +
                        "                     <td style='text-align: center;;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOLUONGKTV + "</a></td> \r\n" +
                        "                     <td style='text-align: center;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOLUONGDUOCSI + "</a></td> \r\n" +
                        "                     <td style='text-align: center;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOLUONGKHAC + "</a></td> \r\n" +
                        "                     <td style='text-align: center;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOGIUONGKEHOACH  + "</a></td> \r\n" +
                        "                     <td style='text-align: center;'><a href='" + PhieuKhaoSatUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + PhieuKhaoSat[iIndex].SOGIUONGTHUCTE + "</a></td> \r\n" +
                        "                     <td style='text-align: center; vertical-align: middle;'>" + PhieuKhaoSatBenhVienVeTinhParser.sColorTrangThai[PhieuKhaoSat[iIndex].TRANGTHAI.Value] + "</td> \r\n" +
                        "                 </tr> \r\n";
                }
                Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "         <div class=\"col-md-12\">\r\n" +
                    "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                    "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (filter.PageIndex * filter.PageSize + dangKyTotal).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
                    "              </div>\r\n" +
                    "             <div class=\"col-md-10\" style=\"margin-top:20px;\">\r\n" +
                    RetPaging.PagingText +
                    "             </div>\r\n" +
                    "         </div>\r\n" +
                    "       </div>\r\n" +
                    "<style>\r\n" +
                    "table th{text-align: center; vertical-align: middle;}\r\n" +
                    "</style>\r\n"
                    ;
                Html = WebEnvironments.ProcessHtml(Html);
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
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut Print(RenderInfoCls ORenderInfo, string keyword, string ChucDanh_ID, string PhieuKhaoSatId, int? trangThai)
        {

            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                FlexCelReport flexCelReport = new FlexCelReport();
                string query = "select hv.ma, hv.hoten, hv.ngaysinh, hv.gioitinh, hv.chuyenmon_ma, hv.ChucDanh_ma, hv.sonamkinhnghiem, kqdt.ngaydangky," +
                                "(select ten from DM_TENKHOAHOC where ma = kh.ten and hieuluc = 1)  khoahoc, kqdt.trangthai " +
                                "  from PhieuPhieuKhaoSat kqdt left join DT_HocVien hv on hv.id = kqdt.hocvien_id " +
                                "left join DT_KHOAHOC kh on kh.id = kqdt.khoahocdangky_id where 1 = 1 ";
                string specialChar = WebConfig.GetWebConfig("SpecialChar");
                Dictionary<string, object> param = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(keyword))
                {
                    param.Add("Keyword", "%" + keyword.ToUpper() + "%");
                    param.Add("Keyword1", "%" + keyword.ToUpper() + "%");
                    query += " and (upper(hv.MA) like " + specialChar + "Keyword OR upper(hv.HOTEN) like " + specialChar + "Keyword1) ";
                }
                if (!string.IsNullOrEmpty(PhieuKhaoSatId))
                {
                    param.Add("KHOAHOCDANGKY_ID", PhieuKhaoSatId);
                    query += " and kqdt.KHOAHOCDANGKY_ID = " + specialChar + "KHOAHOCDANGKY_ID ";
                }
                if (!string.IsNullOrEmpty(ChucDanh_ID))
                {
                    param.Add("ChucDanh_MA", ChucDanh_ID);
                    query += " and hv.ChucDanh_MA = " + specialChar + "ChucDanh_MA ";
                }
                if (trangThai != null)
                {
                    param.Add("TRANGTHAI", trangThai);
                    query += " and kqdt.TRANGTHAI = " + specialChar + "TRANGTHAI ";
                }
                query += " ORDER BY hv.ChucDanh_ma, kqdt.ngaydangky ";
                DataTable dtResult = CallBussinessUtility.CreateBussinessProcess().CreateCommonProcess().GetData(ORenderInfo, new FilterCls(), query, param);
                long rowNumber = dtResult.Rows.Count;
                List<HocVien> Datas = new List<HocVien>();

                SiteParam
                      OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);

                for (int i = 0; i < rowNumber; i++)
                {
                    int? gt = CoreXmlUtility.GetIntOrNull(dtResult.Rows[i], "gioitinh", true);
                    string chuyenMonMa = CoreXmlUtility.GetString(dtResult.Rows[i], "chuyenmon_ma", true);
                    OneMES3.DM.Model.ChuyenMonCls chuyenMon = null;
                    if (!string.IsNullOrEmpty(chuyenMonMa))
                    {
                        chuyenMon = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenMonProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), chuyenMonMa);
                    }
                    OneMES3.DM.Model.BenhVienCls ChucDanh = null;
                    string dvctMa = CoreXmlUtility.GetString(dtResult.Rows[i], "ChucDanh_ma", true);
                    if (!string.IsNullOrEmpty(dvctMa))
                    {
                        ChucDanh = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(Model.Common.CreateRenderInfo(ORenderInfo), dvctMa);
                    }
                    var view = new HocVien();
                    view.Ma = CoreXmlUtility.GetString(dtResult.Rows[i], "ma", true);
                    view.HoTen = CoreXmlUtility.GetString(dtResult.Rows[i], "hoten", true);
                    view.NgaySinh = CoreXmlUtility.GetDateOrNull(dtResult.Rows[i], "ngaysinh", true);
                    view.GioiTinh = (gt == null ? null : Common.BenhNhan.GioiTinhs[gt.Value]);
                    view.TrinhDoChuyenMon = chuyenMon == null ? chuyenMonMa : chuyenMon.Ten;
                    view.CoQuanCongTac = ChucDanh == null ? dvctMa : ChucDanh.Ten;
                    view.SoNamKinhNghiem = CoreXmlUtility.GetIntOrNull(dtResult.Rows[i], "sonamkinhnghiem", true);
                    view.NgayDangKy = CoreXmlUtility.GetDateOrNull(dtResult.Rows[i], "ngaydangky", true);
                    view.KhoaHoc = CoreXmlUtility.GetString(dtResult.Rows[i], "khoahoc", true);
                    view.TrangThai = PhieuKhaoSatBenhVienVeTinhParser.TrangThais[CoreXmlUtility.GetInt(dtResult.Rows[i], "trangthai", true)];

                    Datas.Add(view);
                }
                flexCelReport.SetValue("DonViDaoTao", "BỆNH VIỆN YHCT NGHỆ AN");
                flexCelReport.AddTable("HocVien", Datas);

                string XmlFile = OSiteParam.PathRoot + "\\ReportTemplates\\DT_DanhSachHocVien.xlsx";

                string Id = "DT_DanhSachHocVien_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, Directoryfile + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().ExportEx(ORenderInfo, LoginName, XmlFile, "DT_DanhSachHocVien", flexCelReport, SaveFile);
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
        #region Override Func Core
        public override string divFilter
        {
            get
            {
                return "<div class=\"col-md-3\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm theo mã số phiếu, tên khoa/tổ YHCT\" class=\"form-control valueForm\" style=\"border-radius: 4px;\">\r\n" +
                       "</div>\r\n" +

                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbtendonvikhaosat\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbTrangThai\" class=\"form-control valueForm\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +
                       "<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <button id = 'btnTimKiem' class='btn btn-sm  mr-10px' onclick='FilterChange()' style='margin-top: 0px; height: 28px;background-color: #e26614;color:white;'><strong>Tìm kiếm</strong></button>\r\n" +
                       "</div>\r\n" 
                       //"<div class=\"col-md-1\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       //"    <button id = 'btnExport' class='btn btn-sm  mr-10px' onclick='ExPort()' style='margin-top: 0px; height: 28px;background-color: #e26614;color:white;'><strong>Xuất dữ liệu</strong></button>\r\n" +
                       //"</div>\r\n"
                   ;
            }
        }
        #endregion
    }
}
