﻿using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;

namespace OneTSQ.WebParts
{
   public class DanhSachPhieuADR: WebPartTemplate
    {
        public override string WebPartId
        {
            get
            {
                return "danhsachphieuadr";
            }
        }
        public override string WebPartTitle
        {
            get
            {
                return "Danh sách phiếu ADR";
            }
        }
        public override string Description
        {
            get
            {
                return "Danh sách phiếu ADR";

            }
        }

        public override void RegAjaxPro(RenderInfoCls ORenderInfo, System.Web.UI.Page Page)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(DanhSachPhieuADR), Page);
        }

        public override AjaxOut CheckPermission(RenderInfoCls ORenderInfo)
        {
            SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
            bool HasPermission = true;
            AjaxOut RetAjaxOut = new AjaxOut();
            RetAjaxOut.RetBoolean = HasPermission;

            return RetAjaxOut;
        }

        public override AjaxOut Draw(SiteParam OSiteParam, RenderInfoCls ORenderInfo)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                OwnerUserCls user = WebSessionUtility.GetCurrentLoginUser(OSiteParam);
                AjaxOut RetAjaxOutCheckPermission = CheckPermission(ORenderInfo);
                string userId = WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerUserId;
                int pageIndex = string.IsNullOrEmpty(WebEnvironments.Request("PageIndex")) ? 0 : int.Parse(WebEnvironments.Request("PageIndex"));
                string keyword = WebEnvironments.Request("Keyword");
                int? KQXuTri = string.IsNullOrEmpty(WebEnvironments.Request("KetQuaSauXuTri")) ? null : (int?)int.Parse(WebEnvironments.Request("KetQuaSauXuTri"));
                if (KQXuTri < 0)
                    KQXuTri = null;
                string NoiBaoCaoId = WebEnvironments.Request("NoiBaoCao_ID");

                int? trangThai = string.IsNullOrEmpty(WebEnvironments.Request("TrangThai")) ? null : (int?)int.Parse(WebEnvironments.Request("TrangThai"));
                if (trangThai < 0)
                    trangThai = null;
                string cbNoiBaoCao = null;
                var sRenderInfo = Model.Common.CreateRenderInfo(ORenderInfo);
                if (!string.IsNullOrEmpty(NoiBaoCaoId))
                {
                    var ONoiBaoCao = OneMES3.DM.Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaProcess().CreateModel(sRenderInfo, NoiBaoCaoId);
                    if (ONoiBaoCao != null)
                        cbNoiBaoCao += string.Format("<option value={0}>{1}</option>", ONoiBaoCao.Ma, ONoiBaoCao.Ten);
                }
                string SessionId = System.Guid.NewGuid().ToString();
                string LoginName = WebSessionUtility.GetCurrentLoginUser(OSiteParam).LoginName;
                RetAjaxOut.HtmlContent =
                      WebEnvironments.ProcessHtml(
                        "<script language=\"javascript\">\r\n" +
                    "   $(document).ready(function() {\r\n" +
                    "       document.title = '" + WebLanguage.GetLanguage(OSiteParam, "OneTSQ: Danh sách phiếu ADR") + "';\r\n" +
                    "       $('#cbNoiBaoCao').html('" + cbNoiBaoCao + "');\r\n" +
                    "       CallInitSelect2('cbNoiBaoCao', '" + WebEnvironments.GetRemoteProcessDataUrl(DepartmentService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Nơi báo cáo") + "');\r\n" +
                    "       CallInitSelect2('cbbNguoiLap', '" + WebEnvironments.GetRemoteProcessDataUrl(OwnerUserService.StaticServiceId) + "', '" + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "');\r\n" +
                    "       $('#cbbTrangThai').select2({\r\n" +
                    "          placeholder: 'Trạng thái',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       $('#cbbTrangThai').select2('val', " + (trangThai == null ? "null" : trangThai.ToString()) + ");\r\n" +
                    "       $('#cbKQXuTri').select2({\r\n" +
                    "          placeholder: 'Kết quả sau khi xử trí',\r\n" +
                    "          allowClear: true\r\n" +
                    "       });\r\n" +
                    "       $('#cbKQXuTri').select2('val', " + (KQXuTri == null ? "null" : KQXuTri.ToString()) + ");\r\n" +
                    "   });\r\n" +

                    "   var CurrentPageIndex=0;\r\n" +

                    "   function NextPage(PageIndex)\r\n" +
                    "   {\r\n" +
                    "       CurrentPageIndex = PageIndex;\r\n" +
                    "       setTimeout('Search()',10);\r\n" +
                    "   }\r\n" +

                    "   function Search()\r\n" +
                    "   {\r\n" +
                    "       RenderInfo = CreateRenderInfo();\r\n" +
                    "       keyword = document.getElementById('txtKeyword').value;\r\n" +
                    "       KQXuTri = parseInt(document.getElementById('cbKQXuTri').value);\r\n" +
                    "       NoiBaoCaoId = document.getElementById('cbNoiBaoCao').value;\r\n" +
                    "       trangThai = parseInt(document.getElementById('cbbTrangThai').value);\r\n" +
                    "       AjaxOut = OneTSQ.WebParts.DanhSachPhieuADR.ServerSideDrawSearchResult(RenderInfo,CurrentPageIndex, keyword, NoiBaoCaoId, KQXuTri, trangThai).value;\r\n" +
                    "       document.getElementById('divbaocaosucoykhoa').innerHTML=AjaxOut.HtmlContent;\r\n" +
                    "   }\r\n" +
                    "   function FilterChange(){\r\n" +
                    "       CurrentPageIndex = 0;\r\n" +
                    "       Search();\r\n" +
                    "   }\r\n" +
                    "</script>\r\n") +
                #region html
                    WebEnvironments.ProcessHtml(
                    "<div id=\"divListForm\">\r\n" +
                    " <div class=\"ibox float-e-margins\"> \r\n" +
                    "     <div class=\"ibox-title\"> \r\n" +
                    "         <h5>" + WebLanguage.GetLanguage(OSiteParam, "Danh sách phản ứng có hại của thuốc") + " </h5> \r\n" +
                    "     </div> \r\n" +
                    "     <div class=\"ibox-content\"> \r\n" +
                    "           <div id=\"divbaocaosucoykhoa\">" + DanhSachPhieuADR.ServerSideDrawSearchResult(ORenderInfo, pageIndex, keyword, NoiBaoCaoId, KQXuTri, trangThai).HtmlContent + "</div>\r\n" +
                    "     </div> \r\n" +
                    " </div>\r\n" +
                    "</div>\r\n");
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
        public static AjaxOut ServerSideDrawSearchResult(RenderInfoCls ORenderInfo, int PageIndex ,string Keyword, string NoiBaoCaoId, int? KQXuTri, int? trangThai)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                WebSession.CheckSessionTimeOut(ORenderInfo);                
                PhieuBaoCaoPhanUngCoHaiADRFilterCls OPhieuADRFilter = new PhieuBaoCaoPhanUngCoHaiADRFilterCls()
                {
                    PageIndex = PageIndex,
                    PageSize = 20,
                    Keyword = Keyword,
                    TrangThai = trangThai,
                    KQXuTri = KQXuTri,
                    NoiBaoCao_ID = NoiBaoCaoId,
                };
                long recordTotal = 0;
                PhieuBaoCaoPhanUngCoHaiADRCls[] OPhieuADR = Call.Bussiness.Utility.CallBussinessUtility.CreateBussinessProcess().CreatePhieuBaoCaoPhanUngCoHaiADRProcess().PageReading(ORenderInfo, OPhieuADRFilter, ref recordTotal);
                int countOPhieuADR = OPhieuADR.Count();
                ReturnPaging RetPaging = WebPaging.GetPaging((int)recordTotal, OPhieuADRFilter.PageIndex, OPhieuADRFilter.PageSize, 10, "Next Page");
                string Html = null;
                if (OPhieuADR.Length == 0)
                {
                    Html = "   <div class=\"search-result-info\"></div>\r\n" +
                     "         <div class=\"table-responsive\"> \r\n" +
                     "             <table id=\"sample\" class=\"table table-striped table-bordered table-hover\"> \r\n" +
                     "                 <thead> \r\n" +
                     "                 <tr> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "STT") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Mã đơn vị báo cáo") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Nơi báo cáo") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Ngày báo cáo") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Mã BN") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Họ tên bệnh nhân") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Năm sinh") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Cân nặng (kg)") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Ngày xuất hiện phản ứng") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Kết quả sau khi xử trí phản ứng") + "</th> \r\n" +
                     "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</th> \r\n" +
                     "                 </tr> \r\n" +
                     "                 </thead> \r\n" +
                     "             </table> \r\n" +
                     "       </div>\r\n";
                }
                else
                {
                    Html =
                        "   <div class=\"search-result-info\"></div>\r\n" +
                        "         <div class=\"table-responsive\"> \r\n" +
                        "             <table id=\"sample\" class=\"table table-striped table-bordered table-hover dataTables-autosort\"> \r\n" +
                        "                 <thead> \r\n" +
                        "                 <tr> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "STT") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Mã đơn vị báo cáo") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Nơi báo cáo") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Ngày báo cáo") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Người lập") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Mã BN") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Họ tên bệnh nhân") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Giới tính") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Năm sinh") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Cân nặng (kg)") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Ngày xuất hiện phản ứng") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Kết quả sau khi xử trí phản ứng") + "</th> \r\n" +
                        "                     <th> " + WebLanguage.GetLanguage(OSiteParam, "Trạng thái") + "</th> \r\n" +
                        "                 </tr> \r\n" +
                        "                 </thead> \r\n" +
                        "                 <tbody> \r\n";
                    for (int iIndex = 0; iIndex < OPhieuADR.Length; iIndex++)
                    {
                        var PhieuADRUrl = WebScreen.BuildUrl(OSiteParam, WebSessionUtility.GetCurrentLoginUser(OSiteParam).OwnerCode, "PhieuADR",
                            new WebParamCls[]
                            {
                                new WebParamCls("id",OPhieuADR[iIndex].Id),
                                new WebParamCls("RecordTotal", recordTotal.ToString()),
                            });
                        string nguoilap = null;
                        OwnerUserCls Users = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, OPhieuADR[iIndex].NguoiLap_Id);
                        if (Users != null)
                        {
                            nguoilap = Users.FullName;
                        }
                        string NoiBaoCao = null;
                        DepartmentCls OKhoaPhong = OneTSQ.Core.Call.Bussiness.Utility.CoreCallBussinessUtility.CreateBussinessProcess().CreateDepartmentProcess().CreateModel(ORenderInfo, OPhieuADR[iIndex].NoiBaoCao_Id);
                        if (OKhoaPhong != null)
                        {
                            NoiBaoCao = OKhoaPhong.DepartmentName;
                        }                                           
                        Html +=
                            "                 <tr> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (OPhieuADRFilter.PageIndex * OPhieuADRFilter.PageSize + iIndex + 1).ToString("#,##0") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + OPhieuADR[iIndex].SoBcDonVi + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + NoiBaoCao + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + OPhieuADR[iIndex].NgayLap.Value.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + nguoilap + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + OPhieuADR[iIndex].MaBN + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + OPhieuADR[iIndex].HoTen + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (OPhieuADR[iIndex].GioiTinh == (int)PhieuBaoCaoPhanUngCoHaiADRCls.eGioiTinh.Nam ? "Nam" : "Nữ") + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + OPhieuADR[iIndex].NgaySinh.Value.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + OPhieuADR[iIndex].CanNang + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + OPhieuADR[iIndex].NgayXuatHienPU.Value.ToString("dd/MM/yyyy") + "</a></td> \r\n" +
                            "                     <td style='vertical-align: middle;'><a href='" + PhieuADRUrl + "' title='" + WebLanguage.GetLanguage(OSiteParam, "Mở phiếu") + "'>" + (OPhieuADR[iIndex] == null || OPhieuADR[iIndex].KetQuaSauXuTri == null ? null : PhieuBaoCaoPhanUngCoHaiADRCls.Loais[OPhieuADR[iIndex].KetQuaSauXuTri.Value]) + "</a></td> \r\n" +
                            "                     <td style='text-align: center; vertical-align: middle;'>" + PhieuBaoCaoPhanUngCoHaiADRCls.sColorTrangThais[OPhieuADR[iIndex].TrangThai.Value] + "</td> \r\n" +
                            "                 </tr> \r\n";
                    }
                    Html +=
                    "                 </tbody> \r\n" +
                    "             </table> \r\n" +
                    "         <div class=\"col-md-12\">\r\n" +
                    "              <div class=\"col-md-1\" style=\"margin-top:26px;padding-left: 0px;\">\r\n" +
                    "               <lable>" + WebLanguage.GetLanguage(OSiteParam, "Tổng số") + ": " + (OPhieuADRFilter.PageIndex * OPhieuADRFilter.PageSize + countOPhieuADR).ToString("#,##0") + "" + "/ " + "" + (int)recordTotal + "</lable>\r\n" +
                    "              </div>\r\n" +
                    "             <div class=\"col-md-10\" style=\"margin-top:20px;\">\r\n" +
                    RetPaging.PagingText +
                    "             </div>\r\n" +
                    "         </div>\r\n" +
                    "       </div>\r\n" +
                    "   <style>\r\n" +
                    "table th{text-align: center; vertical-align: middle;}\r\n" +
                    "</style>\r\n";
                }
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

        #region divFilter
        public override string divFilter
        {
            get
            {
                return "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <input id=\"txtKeyword\" placeholder=\"Tìm kiếm theo số báo cáo, họ tên bệnh nhân\" class=\"form-control\" >\r\n" +
                       "</div>\r\n" +

                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbNoiBaoCao\"  class=\"form-control kqxuly_select\">\r\n" +
                       "        <option></option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +

                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbKQXuTri\"  class=\"form-control noibaocao_select\">\r\n" +
                       "        <option></option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongDoADR + "\"> Tử vong do ADR </option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.ChuaHoiPhuc + "\"> Chưa hồi phục </option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucCoDiChung + "\"> Hồi phục có di chứng </option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.TuVongKhongLqThuoc+ "\"> Tử vong không liên quan thuốc </option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.DangHoiPhuc + "\"> Đang hồi phục </option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.HoiPhucKhongDiChung + "\"> Hồi phục không có di chứng </option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eLoai.KhongRo + "\"> Không rõ </option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +

                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "    <select id=\"cbbTrangThai\"  class=\"form-control hieuluc_select\">\r\n" +
                       "        <option></option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eTrangThai.HoanTat + "\"> Hoàn tất </option>\r\n" +
                       "        <option value=\"" + (int)PhieuBaoCaoPhanUngCoHaiADRCls.eTrangThai.Moi + "\"> Mới </option>\r\n" +
                       "    </select>\r\n" +
                       "</div>\r\n" +

                       "<div class=\"col-md-2\" style=\"padding-top: 6px; padding-bottom: 6px;\">\r\n" +
                       "       <button type=\"button\" style=\"margin-top: 0px; height: 28px;background-color: #e26614;color:white;\" class=\"btn btn-sm btn-primary\" onclick=\"javascript:FilterChange();\" > Tìm kiếm </button>\r\n" +
                       "</div>\r\n";
            }
        }
        #endregion
    }
}
