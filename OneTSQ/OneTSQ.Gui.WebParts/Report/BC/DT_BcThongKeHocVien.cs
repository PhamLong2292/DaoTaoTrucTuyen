﻿using FlexCel.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Call.Bussiness.Utility;
using OneTSQ.Core.Call.Bussiness.Utility;
using OneTSQ.Model;
using OneTSQ.ReportUtility;
using OneTSQ.Utility;
using OneTSQ.WebParts;
using OneTSQ.Core.Model;

namespace OneTSQ.WebParts
{
    internal class DT_BcThongKeHocVien : WebPartTemplate
    {
        public override string WebPartId { get { return "dttkhv"; } }
        public override string WebPartTitle { get { return "Thống kê học viên"; } }
        public override string Description { get { return "Thống kê học viên"; } }
        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DT_BcThongKeHocVien), Page);
        }
        
        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string UserId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                string cbbTrangThai = "<select id=\"cbbTrangThai\" class=\"form-control valueForm\" placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "'>";
                foreach (var tt in DT_KetQuaDaoTaoParser.TrangThaiHocs)
                    cbbTrangThai += "<option value=" + tt.Key + " >" + tt.Value + "</option>";
                cbbTrangThai += "    </select>\r\n";
                OneTSQ.WebParts.TimeFilterView timeFilterView = new OneTSQ.WebParts.TimeFilterView()
                {
                    thoiGian = (int)OneTSQ.WebParts.TimeFilter.eThoiGian.quy,
                    quy = ((DateTime.Today.Month - 1) / 3) + 1,
                    thang = DateTime.Today.Month,
                    nam = DateTime.Today.Year,
                };
                RetAjaxOut.HtmlContent =
                #region Html
                WebEnvironments.ProcessHtml(
                "<div class='modal fade' id='ModalFilter' style='overflow: auto' role='dialog' aria-labelledby='largeModal' aria-hidden='true' data-backdrop='static'>\r\n" +
                "     <div class='modal-dialog modal-lg' style='width:780px;'>\r\n" +
                "         <div class='modal-content'>\r\n" +
                "               <div class='panel-heading'>" +
                "                   <strong class='mr-top-x' data-dismiss='modal' aria-hidden='true'><a style='color:white; font-size: x-large;'>&times;</a></strong>\r\n" +
                "                   <strong class='modal-title'>" + WebLanguage.GetLanguage(OSiteParam, new DT_BcThongKeHocVien().WebPartTitle.ToUpper()) + "</strong>\r\n" +
                "               </div>" +
                "              <div class='modal-body'>\r\n" +
                "                   <div class='row'>\r\n" +
                "                        <div class='form-group col-sm-6'>\r\n" +
                "                           <select class='form-control' id='cbbTenKhoaHoc' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Tên khóa học") + "'></select>\r\n" +
                "                        </div>\r\n" +
                "                        <div class='form-group col-sm-6' style=\"height: 28px\">\r\n" +
                "                           <input type='number' class='form-control' id='txtKhoa' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Khóa học số") + "'>\r\n" +
                "                        </div>\r\n" +
                "                        <div class='form-group col-sm-6'>\r\n" +
                "                           <select class='form-control' id='cbbDonViCongTac' placeholder='" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + "'></select>\r\n" +
                "                        </div>\r\n" +
                "                        <div class='form-group col-sm-6'>\r\n" +
                                            cbbTrangThai +
                "                        </div>\r\n" +
                "                        <div class='form-group col-sm-12'>\r\n" +
                                            TimeFilter.Draw(ORenderInfo, timeFilterView).HtmlContent +
                "                        </div>\r\n" +
                "                              <div class='form-group btn-padding-modal col-sm-12' style=\"padding-top: 0px;\">\r\n" +
                "                                  <button style='margin:0px; padding:0px; width: 100px; height: 28px; margin-right: 5px; float: left;' class='btn btn-primary' onclick='FilterChange();' id='btnTimKiem'><strong>" + WebLanguage.GetLanguage(OSiteParam, "Xem báo cáo") + "</strong></button>\r\n" +
                "                                   <div class='dropdown pull-right' style=\"float: left !important;\">\r\n" +
                "                                        <button style='margin:0px; padding:0px; width: 100px; height: 28px;' class='btn btn-primary dropdown-toggle' type='button' data-toggle='dropdown' aria-expanded='false' id='btnExport'>\r\n" +
                "                                             <strong>" + WebLanguage.GetLanguage(OSiteParam, "Export") + "</strong>\r\n" +
                "                                             <span class='caret'></span>" +
                "                                        </button>\r\n" +
                "                                        <ul class='dropdown-menu'>\r\n" +
                "                                      <li><a href=\"javascript:onExport('xlsx');\">" + WebLanguage.GetLanguage(OSiteParam, "Excel 2007") + "</a></li>\r\n" +
                "                                      <li><a href=\"javascript:onExport('xls');\">" + WebLanguage.GetLanguage(OSiteParam, "Excel 97-2003") + "</a></li>\r\n" +
                "                                      <li><a href=\"javascript:FilterChange();\">" + WebLanguage.GetLanguage(OSiteParam, "PDF") + "</a></li>\r\n" +
                "                                      <li><a >" + WebLanguage.GetLanguage(OSiteParam, "TXT") + "</a></li>\r\n" +
                "                                  </ul>\r\n" +
                "                             </div>\r\n" +
                "                        </div>\r\n" +
                "                   </div>\r\n" +
                "              </div>\r\n" +
                "         </div>\r\n" +
                "     </div>\r\n" +
                "</div>\r\n") +
                #endregion
                #region Javascript
                WebEnvironments.ProcessJavascript(
                "<script language=javascript>\r\n" +
                "    RenderInfo=CreateRenderInfo();\r\n" +
                "    $(document).ready(function() \r\n" +
                "    {\r\n" +
                "       $('#ModalFilter').modal('show');\r\n" +
                "       $('#cbbTrangThai').select2({\r\n" +
                "          placeholder: 'Trạng thái',\r\n" +
                "          allowClear: true\r\n" +
                "       });\r\n" +
                "       CallInitSelect2('cbbTenKhoaHoc', '" + WebEnvironments.GetRemoteProcessDataUrl(DM_TenKhoaHocService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Tên khóa học") + "');\r\n" +
                "       CallInitSelect2('cbbDonViCongTac', '" + WebEnvironments.GetRemoteProcessDataUrl(BenhVienService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Đơn vị công tác") + "');\r\n" +
                "    });\r\n" +
                "    function FilterChange(){\r\n" +
                "        tenKhoaHoc = document.getElementById('cbbTenKhoaHoc').value;\r\n" +
                "        khoa = parseInt(document.getElementById('txtKhoa').value);\r\n" +
                "        donViCongTac = document.getElementById('cbbDonViCongTac').value;\r\n" +
                "        trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                "        timeFilter=GetTimeInput();\r\n" +
                "        AjaxOut = OneTSQ.WebParts.DT_BcThongKeHocVien.ServerSideDrawSearchResult(RenderInfo, tenKhoaHoc, khoa, donViCongTac, trangThai, timeFilter).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "            callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "            return;\r\n" +
                "        }\r\n" +
                "        window.open(AjaxOut.RetUrl,'_blank');\r\n" +
                "    }\r\n" +
                "    function onExport(stern){\r\n" +
                "        tenKhoaHoc = document.getElementById('cbbTenKhoaHoc').value;\r\n" +
                "        khoa = parseInt(document.getElementById('txtKhoa').value);\r\n" +
                "        donViCongTac = document.getElementById('cbbDonViCongTac').value;\r\n" +
                "        trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                "        timeFilter=GetTimeInput();\r\n" +
                "        AjaxOut = OneTSQ.WebParts.DT_BcThongKeHocVien.ServerSideDrawExportResult(RenderInfo, tenKhoaHoc, khoa, donViCongTac, trangThai, timeFilter).value;\r\n" +
                "        if(AjaxOut.Error)\r\n" +
                "        {\r\n" +
                "            callSweetAlert(AjaxOut.InfoMessage);\r\n" +
                "            return;\r\n" +
                "        }\r\n" +
                "        window.open(AjaxOut.RetUrl,'_blank');\r\n" +
                "    }\r\n" +
                "</script>\r\n");
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

        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawSearchResult(RenderInfoCls ORenderInfo, string tenKhoaHoc, int? khoa, string donViCongTac, int? trangThai, OneTSQ.WebParts.TimeFilterView timeFilterView)
        {
            FlexCelReport flexCelReport = new FlexCelReport();
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string query = "select dmtkh.TEN, kh.KHOA, hv.MA, hv.HOTEN, hv.GIOITINH, convert(date,hv.NGAYSINH,103) NGAYSINH, hv.DONVICONGTAC_MA, CONCAT(CONCAT(convert(date,kh.NGAYKHAIGIANGDUKIEN,103), '-'), convert(date,kh.NGAYBEGIANGDUKIEN, 103)) THOIGIANHOC, kh.THOILUONG, kh.LOAITHOILUONG, kh.TRANGTHAI " +
                               "from DT_KETQUADAOTAO kqdt inner join DT_KHOAHOC kh on kqdt.KHOAHOCDUYET_ID = kh.ID " +
                                                            "inner join DT_HOCVIEN hv on hv.ID = kqdt.HOCVIEN_ID " +
                                                            "inner join DM_TENKHOAHOC dmtkh on dmtkh.MA = kh.TEN " +
                                                            "where 1=1 ";
                if (!string.IsNullOrEmpty(tenKhoaHoc))
                    query += string.Format("and kh.TEN = '{0}' ", tenKhoaHoc);
                if (!string.IsNullOrEmpty(donViCongTac))
                    query += string.Format("and hv.DONVICONGTAC_MA = '{0}' ", donViCongTac);
                if (khoa != null)
                    query += string.Format("and kh.KHOA = {0} ", khoa);
                if (trangThai == (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.DangHoc)
                    query += string.Format("and kh.TRANGTHAI = {0} ", (int)DT_KhoaHocCls.eTrangThai.Duyet);
                else if (trangThai == (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.KetThuc)
                    query += string.Format("and kh.TRANGTHAI = {0} ", (int)DT_KhoaHocCls.eTrangThai.KetThuc);
                else
                    query += string.Format("and (kh.TRANGTHAI = {0} or kh.TRANGTHAI = {1}) ", (int)DT_KhoaHocCls.eTrangThai.Duyet, (int)DT_KhoaHocCls.eTrangThai.KetThuc);
                if (timeFilterView.tuNgay != null && timeFilterView.denNgay != null)
                    query += string.Format("and CONVERT(DATETIME,'{0}' ) <= kh.NGAYBEGIANGDUKIEN and CONVERT(DATETIME,'{1}') >= kh.NGAYKHAIGIANGDUKIEN ", timeFilterView.tuNgay.Value.ToString("HH:mm yyyy/MM/dd"), timeFilterView.denNgay.Value.ToString("HH:mm yyyy/MM/dd"));
                else if (timeFilterView.tuNgay != null)
                    query += string.Format("and CONVERT(DATETIME,'{0}') >= kh.NGAYKHAIGIANGDUKIEN and CONVERT(DATETIME ,'{0}') <= kh.NGAYBEGIANGDUKIEN ", timeFilterView.tuNgay.Value.ToString("HH:mm yyyy/MM/dd"));
                else if (timeFilterView.denNgay != null)
                    query += string.Format("and CONVERT(DATETIME, '{0}' ) >= kh.NGAYKHAIGIANGDUKIEN and CONVERT(DATETIME,'{0}') <= kh.NGAYBEGIANGDUKIEN ", timeFilterView.denNgay.Value.ToString("HH:mm yyyy/MM/dd"));
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                var dsResult = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().BCQuery(ORenderInfo, query);
                var sRenderInfo = Model.Common.CreateRenderInfo(ORenderInfo);
                List<HocVienReport> Datas = new List<HocVienReport>();
                for (int i = 0; i < dsResult.Rows.Count; i++)
                {
                    OneMES3.DM.Model.BenhVienCls benhVien = null;
                    if (!string.IsNullOrEmpty(CoreXmlUtility.GetString(dsResult.Rows[i], "DONVICONGTAC_MA", true)))
                    {
                        benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(sRenderInfo, (string)dsResult.Rows[i]["DONVICONGTAC_MA"]);
                    }
                    HocVienReport hocVien = new HocVienReport();
                    hocVien.TENKHOAHOC = CoreXmlUtility.GetString(dsResult.Rows[i], "TEN", true);
                    hocVien.SOKHOAHOC = dsResult.Rows[i]["KHOA"] == null ? null : dsResult.Rows[i]["KHOA"].ToString();
                    hocVien.MAHV = CoreXmlUtility.GetString(dsResult.Rows[i], "MA", true);
                    hocVien.HOTENHV = CoreXmlUtility.GetString(dsResult.Rows[i], "HOTEN", true);
                    hocVien.GIOITINH = dsResult.Rows[i]["GIOITINH"] == null ? null : Common.BenhNhan.GioiTinhs[int.Parse(dsResult.Rows[i]["GIOITINH"].ToString())];
                    hocVien.NGAYSINH = CoreXmlUtility.GetString(dsResult.Rows[i], "NGAYSINH", true);
                    hocVien.DONVICONGTAC = benhVien == null ? null : benhVien.Ten;
                    hocVien.THOIGIANHOC = CoreXmlUtility.GetString(dsResult.Rows[i], "THOIGIANHOC", true);
                    hocVien.THOILUONG = dsResult.Rows[i]["THOILUONG"] == null ? null : dsResult.Rows[i]["THOILUONG"] + (string.IsNullOrEmpty(CoreXmlUtility.GetString(dsResult.Rows[i], "LOAITHOILUONG", true)) ? null : " " + DT_KhoaHocParser.LoaiThoiLuongs[CoreXmlUtility.GetString(dsResult.Rows[i], "LOAITHOILUONG", true)]);
                    hocVien.TRANGTHAI = dsResult.Rows[i]["TRANGTHAI"] == null ? null :
                        int.Parse(dsResult.Rows[i]["TRANGTHAI"].ToString()) == (int)DT_KhoaHocCls.eTrangThai.Duyet ? DT_KetQuaDaoTaoParser.TrangThaiHocs[(int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.DangHoc]
                        : int.Parse(dsResult.Rows[i]["TRANGTHAI"].ToString()) == (int)DT_KhoaHocCls.eTrangThai.KetThuc ? DT_KetQuaDaoTaoParser.TrangThaiHocs[(int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.KetThuc] : null;

                    Datas.Add(hocVien);
                }
                flexCelReport.AddTable("HocVien", Datas);

                string XmlFile = OSiteParam.PathRoot + "\\ReportTemplates\\DT_ThongKeHocVien.xlsx";
                string Id = "ThongKeHocVien_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().Execute(ORenderInfo, LoginName, XmlFile, "DT_ThongKeHocVien", flexCelReport, SaveFile);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
        public sealed class HocVienReport
        {
            public string TENKHOAHOC { set; get; }
            public string SOKHOAHOC { set; get; }
            public string MAHV { set; get; }
            public string HOTENHV { set; get; }
            public string GIOITINH { set; get; }
            public string NGAYSINH { set; get; }
            public string DONVICONGTAC { set; get; }
            public string THOIGIANHOC { set; get; }
            public string THOILUONG { set; get; }
            public string TRANGTHAI { set; get; }

        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public static AjaxOut ServerSideDrawExportResult(RenderInfoCls ORenderInfo, string tenKhoaHoc, int? khoa, string donViCongTac, int? trangThai, OneTSQ.WebParts.TimeFilterView timeFilterView)
        {
            FlexCelReport flexCelReport = new FlexCelReport();
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string query = "select dmtkh.TEN, kh.KHOA, hv.MA, hv.HOTEN, hv.GIOITINH, convert(date,hv.NGAYSINH,103) NGAYSINH, hv.DONVICONGTAC_MA, CONCAT(CONCAT(convert(date,kh.NGAYKHAIGIANGDUKIEN,103), '-'), convert(date,kh.NGAYBEGIANGDUKIEN, 103)) THOIGIANHOC, kh.THOILUONG, kh.LOAITHOILUONG, kh.TRANGTHAI " +
                                 "from DT_KETQUADAOTAO kqdt inner join DT_KHOAHOC kh on kqdt.KHOAHOCDUYET_ID = kh.ID " +
                                                              "inner join DT_HOCVIEN hv on hv.ID = kqdt.HOCVIEN_ID " +
                                                              "inner join DM_TENKHOAHOC dmtkh on dmtkh.MA = kh.TEN " +
                                                              "where 1=1 ";
                if (!string.IsNullOrEmpty(tenKhoaHoc))
                    query += string.Format("and kh.TEN = '{0}' ", tenKhoaHoc);
                if (!string.IsNullOrEmpty(donViCongTac))
                    query += string.Format("and hv.DONVICONGTAC_MA = '{0}' ", donViCongTac);
                if (khoa != null)
                    query += string.Format("and kh.KHOA = {0} ", khoa);
                if (trangThai == (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.DangHoc)
                    query += string.Format("and kh.TRANGTHAI = {0} ", (int)DT_KhoaHocCls.eTrangThai.Duyet);
                else if (trangThai == (int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.KetThuc)
                    query += string.Format("and kh.TRANGTHAI = {0} ", (int)DT_KhoaHocCls.eTrangThai.KetThuc);
                else
                    query += string.Format("and (kh.TRANGTHAI = {0} or kh.TRANGTHAI = {1}) ", (int)DT_KhoaHocCls.eTrangThai.Duyet, (int)DT_KhoaHocCls.eTrangThai.KetThuc);
                if (timeFilterView.tuNgay != null && timeFilterView.denNgay != null)
                    query += string.Format("and CONVERT(DATETIME,'{0}' ) <= kh.NGAYBEGIANGDUKIEN and CONVERT(DATETIME,'{1}') >= kh.NGAYKHAIGIANGDUKIEN ", timeFilterView.tuNgay.Value.ToString("HH:mm yyyy/MM/dd"), timeFilterView.denNgay.Value.ToString("HH:mm yyyy/MM/dd"));
                else if (timeFilterView.tuNgay != null)
                    query += string.Format("and CONVERT(DATETIME,'{0}') >= kh.NGAYKHAIGIANGDUKIEN and CONVERT(DATETIME ,'{0}') <= kh.NGAYBEGIANGDUKIEN ", timeFilterView.tuNgay.Value.ToString("HH:mm yyyy/MM/dd"));
                else if (timeFilterView.denNgay != null)
                    query += string.Format("and CONVERT(DATETIME, '{0}' ) >= kh.NGAYKHAIGIANGDUKIEN and CONVERT(DATETIME,'{0}') <= kh.NGAYBEGIANGDUKIEN ", timeFilterView.denNgay.Value.ToString("HH:mm yyyy/MM/dd"));
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                var dsResult = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().BCQuery(ORenderInfo, query);
                var sRenderInfo = Model.Common.CreateRenderInfo(ORenderInfo);
                List<HocVienReport> Datas = new List<HocVienReport>();
                for (int i = 0; i < dsResult.Rows.Count; i++)
                {
                    OneMES3.DM.Model.BenhVienCls benhVien = null;
                    if (!string.IsNullOrEmpty((string)dsResult.Rows[i]["DONVICONGTAC_MA"]))
                    {
                        benhVien = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateBenhVienProcess().CreateModel(sRenderInfo, (string)dsResult.Rows[i]["DONVICONGTAC_MA"]);
                    }
                    HocVienReport hocVien = new HocVienReport();
                    hocVien.TENKHOAHOC = (string)dsResult.Rows[i]["TEN"];
                    hocVien.SOKHOAHOC = dsResult.Rows[i]["KHOA"] == null ? null : dsResult.Rows[i]["KHOA"].ToString();
                    hocVien.MAHV = (string)dsResult.Rows[i]["MA"];
                    hocVien.HOTENHV = (string)dsResult.Rows[i]["HOTEN"];
                    hocVien.GIOITINH = dsResult.Rows[i]["GIOITINH"] == null ? null : Common.BenhNhan.GioiTinhs[int.Parse(dsResult.Rows[i]["GIOITINH"].ToString())];
                    hocVien.NGAYSINH = (string)dsResult.Rows[i]["NGAYSINH"];
                    hocVien.DONVICONGTAC = benhVien == null ? null : benhVien.Ten;
                    hocVien.THOIGIANHOC = (string)dsResult.Rows[i]["THOIGIANHOC"];
                    hocVien.THOILUONG = dsResult.Rows[i]["THOILUONG"] == null ? null : dsResult.Rows[i]["THOILUONG"] + (string.IsNullOrEmpty((string)dsResult.Rows[i]["LOAITHOILUONG"]) ? null : " " + DT_KhoaHocParser.LoaiThoiLuongs[(string)dsResult.Rows[i]["LOAITHOILUONG"]]);
                    hocVien.TRANGTHAI = dsResult.Rows[i]["TRANGTHAI"] == null ? null :
                        int.Parse(dsResult.Rows[i]["TRANGTHAI"].ToString()) == (int)DT_KhoaHocCls.eTrangThai.Duyet ? DT_KetQuaDaoTaoParser.TrangThaiHocs[(int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.DangHoc]
                        : int.Parse(dsResult.Rows[i]["TRANGTHAI"].ToString()) == (int)DT_KhoaHocCls.eTrangThai.KetThuc ? DT_KetQuaDaoTaoParser.TrangThaiHocs[(int)DT_KetQuaDaoTaoCls.eTrangThaiHoc.KetThuc] : null;

                    Datas.Add(hocVien);
                }
                flexCelReport.AddTable("HocVien", Datas);

                string XmlFile = OSiteParam.PathRoot + "\\ReportTemplates\\DT_ThongKeHocVien.xlsx";
                string Id = "ThongKeHocVien_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                string Directoryfile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot);
                string SaveFile = WebConfig.ConvertPathRoot(OSiteParam, OSiteParam.TempPathRoot + "\\" + Id);
                if (!System.IO.Directory.Exists(Directoryfile))
                    System.IO.Directory.CreateDirectory(Directoryfile);
                RetAjaxOut = new OneTSQ.ReportUtility.FlexcelReportUtility().Execute(ORenderInfo, LoginName, XmlFile, "DT_ThongKeHocVien", flexCelReport, SaveFile);
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
            }
            return RetAjaxOut;
        }
    }
}