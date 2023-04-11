using OneTSQ.Core.Model;
using OneTSQ.Utility;
using OneTSQ.TempService;
using OneTSQ.UploadUtility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using Spire.Xls;
using System.Globalization;
using OneTSQ.Core.Call.Bussiness.Utility;
using System.IO;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Utility;

namespace OneTSQ.UploadUtility
{
    public class ProcessImportHandlerUtility
    {
        public enum eFileType
        {
            OWNER_USER = 1,
            DM_CHUYENKHOADAOTAOTT = 1,
            DM_TIEUCHITHOIGIANDAOTAOTT = 2,
            DM_TIEUCHITHOILUONGDAOTAOTT = 3,
            DM_TRANGTHIETBITRUYENHINHTT = 4,
            DM_YKIENBENHVIEN = 5,
            DT_KHOAHOC = 6,
            DM_KYTHUATCHUYENGIAO = 7,
            DT_LICHCHUYENGIAO = 8,
            BACSY = 9,
            DM_NHOMKHOAHOC = 10,
            DM_TENKHOAHOC = 11,
            LICHHOICHAN = 12,
            DM_TIEUCHUANTHAMGIAKHOAHOC = 13,
            DM_GIAYTODICHUYENGIAO = 14
        }
        public static void ProcessRequestSessionImportFile(HttpContext context)
        {
            Collection<UploadHandlerResultCls>
             ColUploadHandlerResults = new Collection<UploadHandlerResultCls> { };
            UploadHandlerResultCls OUploadHandlerResult = new UploadHandlerResultCls();
            string RealFile = "";
            string Ext = "";
            bool IsMulti = false;
            try
            {
                RenderInfoCls ORenderInfo = new RenderInfoCls();
                ORenderInfo.SiteId = "online.onenet";
                ORenderInfo.SiteLanguage = "vi";
                ORenderInfo.SiteAssetLevelId = "online.onenet";
                ORenderInfo.SiteAssetLevelId = "online.onenet";
                ORenderInfo.AssetLevelCode = "online.onenet";
                context.Response.ContentType = "text/plain";
                if (context.Request.Files.Count > 1)
                {
                    IsMulti = true;
                }
                for (int iIndexFile = 0; iIndexFile < context.Request.Files.Count; iIndexFile++)
                {
                    HttpPostedFile hpf = context.Request.Files[iIndexFile] as HttpPostedFile;
                    string FileName = string.Empty;
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
                    {
                        string[] files = hpf.FileName.Split(new char[] { '\\' });
                        FileName = files[files.Length - 1];
                    }
                    else
                    {
                        FileName = hpf.FileName;
                    }
                    if (hpf.ContentLength == 0)
                        continue;

                    RealFile = new System.IO.FileInfo(FileName).Name;
                    Ext = new System.IO.FileInfo(FileName).Extension;
                    string SiteId = context.Request["SiteId"];

                    string SecurityCode = WebConfig.GetSecurityCode();
                    string SessionId = context.Request["SessionId"];
                    string LoginName = context.Request["LoginName"];
                    string UserId = context.Request["UserId"];
                    int filetype = Int32.Parse(context.Request["FileType"]);
                    string WebPathRootTemp = WebConfig.GetWebPathRootTemp();
                    string WebHttpRootTemp = WebConfig.GetWebHttpRootTemp();

                    string MultiUpload = context.Request["MultiUpload"];
                    //string message;
                    if (string.IsNullOrEmpty(MultiUpload))
                    {
                        MultiUpload = "0";
                    }
                    if (filetype == (int)eFileType.OWNER_USER)
                    {
                        OUploadHandlerResult.InfoMessage = ImportOWNER_USER(ORenderInfo, hpf.InputStream);
                    }
                    if (filetype == (int)eFileType.DM_CHUYENKHOADAOTAOTT)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_CHUYENKHOADAOTAOTT(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DM_TIEUCHITHOIGIANDAOTAOTT)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_TIEUCHITHOIGIANDAOTAOTT(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DM_TIEUCHITHOILUONGDAOTAOTT)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_TIEUCHITHOILUONGDAOTAOTT(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DM_TRANGTHIETBITRUYENHINHTT)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_TRANGTHIETBITRUYENHINHTT(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DM_YKIENBENHVIEN)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_YKIENBENHVIEN(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DT_KHOAHOC)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDT_KHOAHOC(ORenderInfo, hpf.InputStream, UserId);
                    }
                    else if (filetype == (int)eFileType.DM_KYTHUATCHUYENGIAO)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_KYTHUATCHUYENGIAO(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DT_LICHCHUYENGIAO)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDT_LICHCHUYENGIAO(ORenderInfo, hpf.InputStream, UserId);
                    }
                    else if (filetype == (int)eFileType.BACSY)
                    {
                        OUploadHandlerResult.InfoMessage = ImportBACSY(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DM_NHOMKHOAHOC)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_NHOMKHOAHOC(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DM_TENKHOAHOC)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_TENKHOAHOC(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.LICHHOICHAN)
                    {
                        OUploadHandlerResult.InfoMessage = ImportLICHHOICHAN(ORenderInfo, hpf.InputStream, UserId);
                    }
                    else if (filetype == (int)eFileType.DM_GIAYTODICHUYENGIAO)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_GiayToDiChuyenGiao(ORenderInfo, hpf.InputStream);
                    }
                    else if (filetype == (int)eFileType.DM_TIEUCHUANTHAMGIAKHOAHOC)
                    {
                        OUploadHandlerResult.InfoMessage = ImportDM_TieuChuanThamGiaKhoaHoc(ORenderInfo, hpf.InputStream);
                    }
                    else
                        OUploadHandlerResult.InfoMessage = string.Format("Không hỗ trợ Import!");
                    ColUploadHandlerResults.Add(OUploadHandlerResult);
                }        
            }
            catch (Exception ex)
            {
                if (IsMulti == false)
                {
                    OUploadHandlerResult.Error = true;
                    OUploadHandlerResult.InfoMessage = ex.Message.ToString();
                    string XmlInfo = UploadHandlerResultParser.GetXml(OUploadHandlerResult);
                    context.Response.Write(XmlInfo);
                    return;
                }
                else
                {
                    MultiUploadHandlerResultCls
                        OMultiUploadHandlerResult = new MultiUploadHandlerResultCls();
                    OMultiUploadHandlerResult.InfoMessage = ex.Message.ToString();
                    OMultiUploadHandlerResult.Error = true;
                    string XmlInfo = MultiUploadHandlerResultParser.GetXml(OMultiUploadHandlerResult);
                    context.Response.Write(XmlInfo);
                    return;
                }
            }
            if (IsMulti == false)
            {
                string XmlInfo = UploadHandlerResultParser.GetXml(OUploadHandlerResult);
                context.Response.Write(XmlInfo);
            }
            else
            {
                string XmlInfo = UploadHandlerResultParser.GetMultiXml(ColUploadHandlerResults.ToArray());
                MultiUploadHandlerResultCls
                    OMultiUploadHandlerResult = new MultiUploadHandlerResultCls();
                OMultiUploadHandlerResult.XmlData = XmlInfo;
                OMultiUploadHandlerResult.Error = false;

                string XmlData = MultiUploadHandlerResultParser.GetXml(OMultiUploadHandlerResult);
                context.Response.Write(XmlData);
            }
        }

        private static string ImportOWNER_USER(RenderInfoCls ORenderInfo, System.IO.Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet worksheet = workbook.Worksheets[0];
            int num = 0;
            foreach (CellRange cellRange in worksheet.Rows)
            {
                bool flag = num > 0;
                if (flag)
                {
                    bool flag2 = string.IsNullOrEmpty(cellRange.Cells[1].Value);
                    string result;
                    if (flag2)
                    {
                        result = "Mã không được trống dòng: " + (num + 1).ToString();
                    }
                    else
                    {
                        bool flag3 = string.IsNullOrEmpty(cellRange.Cells[2].Value);
                        if (flag3)
                        {
                            result = "Tên không được trống dòng: " + (num + 1).ToString();
                        }
                        else
                        {
                            bool flag4 = string.IsNullOrEmpty(cellRange.Cells[3].Value);
                            if (flag4)
                            {
                                result = "MK không được trống dòng: " + (num + 1).ToString();
                            }
                            else
                            {
                                bool flag5 = string.IsNullOrEmpty(cellRange.Cells[5].Value);
                                if (flag5)
                                {
                                    result = "Mã khoa phòng không được trống dòng: " + (num + 1).ToString();
                                }
                                else
                                {
                                    bool flag6 = string.IsNullOrEmpty(cellRange.Cells[9].Value);
                                    if (flag6)
                                    {
                                        result = "Vai trò không được trống dòng: " + (num + 1).ToString();
                                    }
                                    else
                                    {
                                        bool flag7 = string.IsNullOrEmpty(cellRange.Cells[10].Value);
                                        if (!flag7)
                                        {
                                            goto IL_190;
                                        }
                                        result = "Menu không được trống dòng: " + (num + 1).ToString();
                                    }
                                }
                            }
                        }
                    }
                    return result;
                }
                IL_190:
                num++;
            }
            for (int j = 1; j < worksheet.Rows.Length; j++)
            {
                CellRange cellRange2 = worksheet.Rows[j];
                string text = "user";
                string text2 = "sys.admin";
                string value = cellRange2.Cells[1].Value;
                string value2 = cellRange2.Cells[2].Value;
                string value3 = cellRange2.Cells[3].Value;
                string value4 = cellRange2.Cells[10].Value;
                bool flag8 = !string.IsNullOrEmpty(value4);
                if (flag8)
                {
                    MenuTemplateCls menuTemplateCls = CoreCallBussinessUtility.CreateBussinessProcess().CreateMenuTemplateProcess().CreateModel(ORenderInfo, value4);
                    bool flag9 = menuTemplateCls != null;
                    if (flag9)
                    {
                        text = menuTemplateCls.MenuTemplateId;
                    }
                }
                string value5 = cellRange2.Cells[6].Value;
                bool flag10 = !string.IsNullOrEmpty(value5);
                if (flag10)
                {
                    DashboardCls dashboardCls = CoreCallBussinessUtility.CreateBussinessProcess().CreateDashboardProcess().CreateModel(ORenderInfo, value5);
                    bool flag11 = dashboardCls != null;
                    if (flag11)
                    {
                        text2 = dashboardCls.DashboardId;
                    }
                }
                string value6 = cellRange2.Cells[9].Value;
                string text3 = null;
                bool flag12 = !string.IsNullOrEmpty(value6);
                if (flag12)
                {
                    RoleCls roleCls = CoreCallBussinessUtility.CreateBussinessProcess().CreateRoleProcess().CreateModel(ORenderInfo, value6);
                    bool flag13 = roleCls != null;
                    if (flag13)
                    {
                        text3 = roleCls.RoleId;
                    }
                }
                string value7 = cellRange2.Cells[4].Value;
                string frkTitleId = null;
                bool flag14 = !string.IsNullOrEmpty(value7);
                if (flag14)
                {
                    TitleCls titleCls = CoreCallBussinessUtility.CreateBussinessProcess().CreateTitleProcess().CreateModel(ORenderInfo, value7);
                    bool flag15 = titleCls != null;
                    if (flag15)
                    {
                        frkTitleId = titleCls.TitleId;
                    }
                }
                string value8 = cellRange2.Cells[5].Value;
                string frkDepartmentId = null;
                bool flag16 = !string.IsNullOrEmpty(value8);
                if (flag16)
                {
                    DepartmentCls departmentCls = CoreCallBussinessUtility.CreateBussinessProcess().CreateDepartmentProcess().CreateModel(ORenderInfo, value8);
                    bool flag17 = departmentCls != null;
                    if (flag17)
                    {
                        frkDepartmentId = departmentCls.DepartmentId;
                    }
                }
                bool flag18 = string.IsNullOrEmpty(value);
                if (flag18)
                {
                    throw new Exception(WebLanguage.GetLanguage(ORenderInfo, "Mã người dùng không hợp lệ"));
                }
                bool flag19 = string.IsNullOrEmpty(value2);
                if (flag19)
                {
                    throw new Exception(WebLanguage.GetLanguage(ORenderInfo, "Tên người dùng không hợp lệ"));
                }
                bool flag20 = string.IsNullOrEmpty(value3);
                if (flag20)
                {
                    throw new Exception(WebLanguage.GetLanguage(ORenderInfo, "Mật khẩu chưa nhập"));
                }
                bool flag21 = string.IsNullOrEmpty(text);
                if (flag21)
                {
                    throw new Exception(WebLanguage.GetLanguage(ORenderInfo, "Chưa chọn menu chức năng"));
                }
                bool flag22 = string.IsNullOrEmpty(text2);
                if (flag22)
                {
                    throw new Exception(WebLanguage.GetLanguage(ORenderInfo, "Chưa chọn dashboard chức năng"));
                }
                string[] roleIds = new string[]
                {
                    text3
                };
                string[] itemOwnerIds = new string[]
                {
                    "sys"
                };
                OwnerUserCls ownerUserCls = new OwnerUserCls();
                ownerUserCls.OwnerUserId = Guid.NewGuid().ToString();
                bool flag23 = cellRange2.Cells.Length > 11;
                if (flag23)
                {
                    bool flag24 = !string.IsNullOrEmpty(cellRange2.Cells[11].Value);
                    if (flag24)
                    {
                        ownerUserCls.OwnerUserId = cellRange2.Cells[11].Value;
                    }
                }
                ownerUserCls.LoginName = value;
                ownerUserCls.FullName = value2;
                ownerUserCls.frkOwnerId = "sys";
                ownerUserCls.frkMenuTemplateId = text;
                ownerUserCls.Password = value3;
                ownerUserCls.frkTitleId = frkTitleId;
                ownerUserCls.frkDepartmentId = frkDepartmentId;
                ownerUserCls.RoleIds = roleIds;
                ownerUserCls.frkLanguageId = "vi";
                ownerUserCls.Mobile = cellRange2.Cells[7].Value;
                ownerUserCls.Email = cellRange2.Cells[8].Value;
                ownerUserCls.frkDashboardId = text2;
                ownerUserCls.Active = 1;
                ownerUserCls.ItemOwnerIds = itemOwnerIds;
                ownerUserCls.WorkingOwnerId = "sys";
                CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().Add(ORenderInfo, ownerUserCls);
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_CHUYENKHOADAOTAOTT(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[6].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_ChuyenKhoaDaoTaoTtCls ck = new DM_ChuyenKhoaDaoTaoTtCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.Ma = item.Cells[0].Value;
                    ck.Ten = item.Cells[1].Value;
                    ck.TenNgan = item.Cells[2].Value;
                    ck.MoTa = item.Cells[3].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[4].Value);
                    ck.Stt = int.Parse(item.Cells[5].Value);
                    ck.TuNgay = item.Cells[6].Value2 is DateTime ? (DateTime)item.Cells[6].Value2 : !string.IsNullOrEmpty(item.Cells[6].Value) ? DateTime.ParseExact(item.Cells[6].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_TIEUCHITHOIGIANDAOTAOTT(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_TieuChiThoiGianDaoTaoTtCls ck = new DM_TieuChiThoiGianDaoTaoTtCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.Ma = item.Cells[0].Value;
                    ck.Ten = item.Cells[1].Value;
                    ck.MoTa = item.Cells[2].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[3].Value);
                    ck.Stt = int.Parse(item.Cells[4].Value);
                    ck.TuNgay = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : !string.IsNullOrEmpty(item.Cells[5].Value) ? DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiGianDaoTaoTtProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_TIEUCHITHOILUONGDAOTAOTT(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_TieuChiThoiLuongDaoTaoTtCls tieuChiThoiLuongDaoTao = new DM_TieuChiThoiLuongDaoTaoTtCls();
                    tieuChiThoiLuongDaoTao.Id = System.Guid.NewGuid().ToString();
                    tieuChiThoiLuongDaoTao.Ma = item.Cells[0].Value;
                    tieuChiThoiLuongDaoTao.Ten = item.Cells[1].Value;
                    tieuChiThoiLuongDaoTao.MoTa = item.Cells[2].Value ?? "";
                    tieuChiThoiLuongDaoTao.HieuLuc = int.Parse(item.Cells[3].Value);
                    tieuChiThoiLuongDaoTao.Stt = int.Parse(item.Cells[4].Value);
                    tieuChiThoiLuongDaoTao.TuNgay = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : !string.IsNullOrEmpty(item.Cells[5].Value) ? DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    tieuChiThoiLuongDaoTao.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Save(ORenderInfo, checkma.Id, tieuChiThoiLuongDaoTao);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateTieuChiThoiLuongDaoTaoTtProcess().Add(ORenderInfo, tieuChiThoiLuongDaoTao);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_TRANGTHIETBITRUYENHINHTT(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[2].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[6].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_TrangThietBiTruyenHinhTtCls ck = new DM_TrangThietBiTruyenHinhTtCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.ChaId = item.Cells[0].Value;
                    ck.Ma = item.Cells[1].Value;
                    ck.Ten = item.Cells[2].Value;
                    ck.MoTa = item.Cells[3].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[4].Value);
                    ck.Stt = int.Parse(item.Cells[5].Value);
                    ck.TuNgay = item.Cells[6].Value2 is DateTime ? (DateTime)item.Cells[6].Value2 : !string.IsNullOrEmpty(item.Cells[6].Value) ? DateTime.ParseExact(item.Cells[6].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateTrangThietBiTruyenHinhTtProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_YKIENBENHVIEN(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[2].Value))
                    {
                        return "Nội dung không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[6].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_YKienBenhVienCls ykbv = new DM_YKienBenhVienCls();
                    ykbv.Id = System.Guid.NewGuid().ToString();
                    ykbv.Ma = item.Cells[0].Value;
                    ykbv.Ten = item.Cells[1].Value;
                    ykbv.NoiDung = item.Cells[2].Value;
                    ykbv.MoTa = item.Cells[3].Value ?? "";
                    ykbv.HieuLuc = int.Parse(item.Cells[4].Value);
                    ykbv.Stt = int.Parse(item.Cells[5].Value);
                    ykbv.TuNgay = item.Cells[6].Value2 is DateTime ? (DateTime)item.Cells[6].Value2 : !string.IsNullOrEmpty(item.Cells[6].Value) ? DateTime.ParseExact(item.Cells[6].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ykbv.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Save(ORenderInfo, checkma.Id, ykbv);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Add(ORenderInfo, ykbv);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDT_KHOAHOC(RenderInfoCls ORenderInfo, Stream stream, string userId)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Số khóa học không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Mã tên khóa học không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[7].Value))
                    {
                        return "Mã loại hình đào tạo không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[9].Value))
                    {
                        return "Loại khóa học không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DT_KhoaHocCls khoaHoc = CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, item.Cells[1].Value + item.Cells[0].Value);
                    if (khoaHoc == null)
                        khoaHoc = new DT_KhoaHocCls();
                    khoaHoc.MA = item.Cells[1].Value + item.Cells[0].Value;
                    khoaHoc.TEN = item.Cells[1].Value;
                    khoaHoc.KHOA = int.Parse(item.Cells[0].Value);
                    khoaHoc.THOILUONG = string.IsNullOrEmpty(item.Cells[2].Value) ? null : (int?)int.Parse(item.Cells[2].Value);
                    khoaHoc.LOAITHOILUONG = item.Cells[3].Value;
                    khoaHoc.NGAYKHAIGIANGDUKIEN = item.Cells[4].Value2 is DateTime ? (DateTime)item.Cells[4].Value2 : !string.IsNullOrEmpty(item.Cells[4].Value) ? (DateTime?)DateTime.ParseExact(item.Cells[4].Value, "dd/MM/yyyy", null) : null;
                    khoaHoc.NGAYBEGIANGDUKIEN = khoaHoc.NGAYKHAIGIANGDUKIEN == null || khoaHoc.THOILUONG == null || string.IsNullOrEmpty(khoaHoc.LOAITHOILUONG) ? null :
                        khoaHoc.LOAITHOILUONG == "D" ? khoaHoc.NGAYKHAIGIANGDUKIEN.Value.AddDays(khoaHoc.THOILUONG.Value) :
                        khoaHoc.LOAITHOILUONG == "W" ? khoaHoc.NGAYKHAIGIANGDUKIEN.Value.AddDays(khoaHoc.THOILUONG.Value * 7) :
                        (DateTime?)khoaHoc.NGAYKHAIGIANGDUKIEN.Value.AddMonths(khoaHoc.THOILUONG.Value);
                    khoaHoc.HANNOPHOSO = khoaHoc.NGAYKHAIGIANGDUKIEN == null ? null : (DateTime?)khoaHoc.NGAYKHAIGIANGDUKIEN.Value.AddDays(-10);
                    khoaHoc.SOLUONGHOCVIENDUKIEN = string.IsNullOrEmpty(item.Cells[5].Value) ? null : (int?)int.Parse(item.Cells[5].Value);
                    khoaHoc.HOCPHI = string.IsNullOrEmpty(item.Cells[6].Value) ? null : (decimal?)decimal.Parse(item.Cells[6].Value);
                    khoaHoc.LOAIHINHDAOTAO = int.Parse(item.Cells[7].Value);
                    khoaHoc.DOITUONG = item.Cells[8].Value;
                    khoaHoc.LOAIKHOAHOC = int.Parse(item.Cells[9].Value);
                    khoaHoc.DONVIHOTRO_MA = item.Cells[10].Value;
                    khoaHoc.MOTA = item.Cells[11].Value;
                    if (!string.IsNullOrEmpty(khoaHoc.ID))
                    {
                        khoaHoc.NGUOISUA_ID = userId;
                        khoaHoc.NGAYSUA = DateTime.Now;
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Save(ORenderInfo, khoaHoc.ID, khoaHoc);
                    }
                    else
                    {
                        khoaHoc.ID = System.Guid.NewGuid().ToString();
                        khoaHoc.NGUOITAO_ID = userId;
                        khoaHoc.NGAYTAO = DateTime.Now;
                        CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().Add(ORenderInfo, khoaHoc);
                    }

                }
                i++;
            }
            return "Đã nhập dữ liệu thành công!";
        }
        private static string ImportDM_KYTHUATCHUYENGIAO(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_KyThuatChuyenGiaoCls ck = new DM_KyThuatChuyenGiaoCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.Ma = item.Cells[0].Value;
                    ck.Ten = item.Cells[1].Value;
                    ck.MoTa = item.Cells[2].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[3].Value);
                    ck.Stt = int.Parse(item.Cells[4].Value);
                    ck.TuNgay = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : !string.IsNullOrEmpty(item.Cells[5].Value) ? DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateKyThuatChuyenGiaoProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDT_LICHCHUYENGIAO(RenderInfoCls ORenderInfo, Stream stream, string userId)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Kỹ thuật chuyển giao không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "Ngày bắt đầu không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Ngày kết thúc không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    BacSyCls canBoChuyenGiao = string.IsNullOrEmpty(item.Cells[3].Value) ? null : CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, item.Cells[3].Value);
                    DT_KhoaHocCls khoaHoc = string.IsNullOrEmpty(item.Cells[1].Value) ? null : CallBussinessUtility.CreateBussinessProcess().CreateDT_KhoaHocProcess().CreateModel(ORenderInfo, item.Cells[1].Value);

                    DT_LichChuyenGiaoCls lichChuyenGiao = new DT_LichChuyenGiaoCls();
                    lichChuyenGiao.ID = System.Guid.NewGuid().ToString();
                    lichChuyenGiao.KYTHUAT_MA = item.Cells[0].Value;
                    lichChuyenGiao.KHOAHOC_ID = khoaHoc == null ? null : khoaHoc.ID;
                    lichChuyenGiao.BENHVIEN_MA = item.Cells[2].Value;
                    lichChuyenGiao.BACSY_ID = canBoChuyenGiao == null ? null : canBoChuyenGiao.ID;
                    lichChuyenGiao.BATDAU = item.Cells[4].Value2 is DateTime ? (DateTime)item.Cells[4].Value2 : DateTime.ParseExact(item.Cells[4].Value, "dd/MM/yyyy", null);
                    lichChuyenGiao.KETTHUC = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null);
                    lichChuyenGiao.TRANGTHAI = (int)DT_LichChuyenGiaoCls.eTrangThai.Moi;
                    lichChuyenGiao.NGUOITAO_ID = userId;
                    lichChuyenGiao.NGAYTAO = DateTime.Now;
                    CallBussinessUtility.CreateBussinessProcess().CreateDT_LichChuyenGiaoProcess().Add(ORenderInfo, lichChuyenGiao);
                }
                i++;
            }
            return "Đã nhập dữ liệu thành công!";
        }
        private static string ImportBACSY(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Họ tên không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    string ma = item.Cells[0].Value;
                    string hoTen = item.Cells[1].Value;
                    var bacSy = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, ma);
                    if (bacSy == null)
                        bacSy = new BacSyCls();
                    string ho = "", ten = "";
                    if (!string.IsNullOrEmpty(hoTen))
                    {
                        hoTen = hoTen.TrimStart().TrimEnd();
                        int index = hoTen.IndexOf(' ');
                        if (index > -1 && index < hoTen.Length - 1)
                        {
                            ho = hoTen.Substring(0, index);
                            ten = hoTen.Substring(index + 1);
                        }
                        else ten = hoTen;
                    }
                    bacSy.MA = ma;
                    bacSy.HO = ho;
                    bacSy.TEN = ten;
                    bacSy.HOTEN = hoTen;
                    bacSy.TENTHUONGGOI = item.Cells[2].Value;
                    bacSy.BIDANH = item.Cells[3].Value;
                    bacSy.NGAYSINH = item.Cells[4].Value2 is DateTime ? (DateTime?)item.Cells[4].Value2 : !string.IsNullOrEmpty(item.Cells[4].Value) ? (DateTime?)DateTime.ParseExact(item.Cells[4].Value, "dd/MM/yyyy", null) : null;
                    bacSy.GIOITINH = string.IsNullOrEmpty(item.Cells[5].Value) ? null : (int?)int.Parse(item.Cells[5].Value);
                    bacSy.DIENTHOAI = item.Cells[6].Value;
                    bacSy.EMAIL = item.Cells[7].Value;
                    bacSy.DIACHISONHA = item.Cells[8].Value;
                    bacSy.DIACHIHANHCHINHMA = item.Cells[9].Value;
                    bacSy.CMND = item.Cells[10].Value;
                    bacSy.NGAYCAPCMND = item.Cells[11].Value2 is DateTime ? (DateTime?)item.Cells[11].Value2 : !string.IsNullOrEmpty(item.Cells[11].Value) ? (DateTime?)DateTime.ParseExact(item.Cells[11].Value, "dd/MM/yyyy", null) : null;
                    bacSy.NOICAPCMND = item.Cells[12].Value;
                    bacSy.CHUYENMONMA = item.Cells[13].Value;
                    bacSy.CAPBACMA = item.Cells[14].Value;
                    bacSy.CHUYENNGANHMA = item.Cells[15].Value;
                    bacSy.TRINHDOMA = item.Cells[16].Value;
                    bacSy.CHUCDANHMA = item.Cells[17].Value;
                    bacSy.CHUYENKHOAMA = item.Cells[18].Value;
                    bacSy.DANTOCMA = item.Cells[19].Value;
                    bacSy.QUOCTICHMA = item.Cells[20].Value;
                    bacSy.DONVIMA = item.Cells[21].Value;
                    bacSy.QUATRINHCONGTAC = item.Cells[22].Value;
                    if (string.IsNullOrEmpty(bacSy.ID))
                    {
                        bacSy.ID = System.Guid.NewGuid().ToString();
                        CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Add(ORenderInfo, bacSy);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().Save(ORenderInfo, bacSy.ID, bacSy);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_NHOMKHOAHOC(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_NhomKhoaHocCls ck = new DM_NhomKhoaHocCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.Ma = item.Cells[0].Value;
                    ck.Ten = item.Cells[1].Value;
                    ck.MoTa = item.Cells[2].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[3].Value);
                    ck.Stt = int.Parse(item.Cells[4].Value);
                    ck.TuNgay = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : !string.IsNullOrEmpty(item.Cells[5].Value) ? DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_NhomKhoaHocProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_TENKHOAHOC(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[6].Value))
                    {
                        return "Mã khóa học không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_TenKhoaHocCls ck = new DM_TenKhoaHocCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.Ma = item.Cells[0].Value;
                    ck.Ten = item.Cells[1].Value;
                    ck.MoTa = item.Cells[2].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[3].Value);
                    ck.Stt = int.Parse(item.Cells[4].Value);
                    ck.TuNgay = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : !string.IsNullOrEmpty(item.Cells[5].Value) ? DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    ck.NhomKhoaHoc_Ma = item.Cells[6].Value;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_TenKhoaHocProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportLICHHOICHAN(RenderInfoCls ORenderInfo, Stream stream, string userId)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Ngày hội chẩn không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[2].Value))
                    {
                        return "chuyên khoa không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Chủ trì không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "Thư ký không được trống dòng: " + (j + 1).ToString();
                    }
                    //if (string.IsNullOrEmpty(item.Cells[2].Value))
                    //{
                    //    return "Địa điểm không được trống dòng: " + (j + 1).ToString();
                    //}
                    //if (string.IsNullOrEmpty(item.Cells[2].Value))
                    //{
                    //    return "Người duyệt không được trống dòng: " + (j + 1).ToString();
                    //}
                }
                j++;
            }
            for (int m = 0; m < sheet.Rows.Count(); m++)
            {
                for (int n = 0; n < m; n++)
                {
                    if (sheet.Rows[m].Cells[1].Value + "" == sheet.Rows[n].Cells[1].Value + "")
                    {
                        return "Ngày: " + sheet.Rows[m].Cells[1].Value + " có hơn 1 lịch hội chẩn";
                    }
                }
            }
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    var OTaoLichHoiChan = new LichHoiChanCls();
                    OTaoLichHoiChan.ID = System.Guid.NewGuid().ToString();
                    //OTaoLichHoiChan.TAOBOI = WebEnvironments.GetOwnerUserId(ORenderInfo);
                    //OTaoLichHoiChan.TAOVAO = DateTime.Now;
                    if (!string.IsNullOrEmpty(item.Cells[1].Value))
                        OTaoLichHoiChan.BATDAU = DateTime.ParseExact(item.Cells[1].Value + " 13:30", new string[] { "dd/MM/yyyy", "yyyy-MM-dd", "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd/MM/yyyy HH:mm", "HH:mm dd/MM/yyyy", "yyyy-MM-ddTHH:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    if (!string.IsNullOrEmpty(item.Cells[1].Value))
                        OTaoLichHoiChan.KETTHUC = DateTime.ParseExact(item.Cells[1].Value + " 15:00", new string[] { "dd/MM/yyyy", "yyyy-MM-dd", "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd/MM/yyyy HH:mm", "HH:mm dd/MM/yyyy", "yyyy-MM-ddTHH:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    if (!string.IsNullOrEmpty(item.Cells[2].Value))
                        OTaoLichHoiChan.CHUYENKHOAMA = item.Cells[2].Value;
                    BacSyCls chutri = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, item.Cells[3].Value);
                    if (chutri != null)
                        OTaoLichHoiChan.CHUTRI = chutri.ID;
                    BacSyCls thuky = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, item.Cells[4].Value);
                    if (thuky != null)
                        OTaoLichHoiChan.THUKY = thuky.ID;

                    if (!string.IsNullOrEmpty(item.Cells[5].Value))
                        OTaoLichHoiChan.DIADIEM = item.Cells[5].Value;
                    else
                        OTaoLichHoiChan.DIADIEM = "Phòng khách - Hội trường Tôn Thất Tùng - Bệnh viện Hữu nghị Việt Đức";
                    OwnerUserCls taoboi = string.IsNullOrEmpty(item.Cells[6].Value) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, item.Cells[6].Value);
                    if (taoboi == null)
                        taoboi = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, "01901.thuky");
                    if (taoboi != null)
                        OTaoLichHoiChan.TAOBOI = taoboi == null ? null : taoboi.OwnerUserId;
                    OTaoLichHoiChan.TAOVAO = OTaoLichHoiChan.BATDAU.Value.AddDays(-1);
                    OwnerUserCls duyetboi = string.IsNullOrEmpty(item.Cells[7].Value) ? null : CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, item.Cells[7].Value);
                    if (duyetboi == null)
                        duyetboi = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerUserProcess().CreateModel(ORenderInfo, "01901.thuky");
                    if (duyetboi != null)
                        OTaoLichHoiChan.DUYETBOI = duyetboi.OwnerUserId;
                    OTaoLichHoiChan.DUYETVAO = OTaoLichHoiChan.BATDAU.Value.AddDays(-1);

                    CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().Add(ORenderInfo, OTaoLichHoiChan);
                    string araybs = "36001,25001,01831,27009,38280,14001,10061,22002,34014,40001,11001,30013,31153,24011,34001,37101,02001,22001,22007,40019,01219,31313,12096";
                    string[] bacSyIds = araybs.Split(',');
                    string[] bacsis = new string[23];
                    string[] donViCongTacTens = new string[23];
                    for (int k = 0; k < bacSyIds.Length; k++)
                    {
                        BacSyCls bs = CallBussinessUtility.CreateBussinessProcess().CreateBacSyProcess().CreateModel(ORenderInfo, bacSyIds[k]);
                        if (bs != null)
                        {
                            bacsis[k] = bs.ID;
                            donViCongTacTens[k] = bs.HOTEN;
                        }

                    }
                    CallBussinessUtility.CreateBussinessProcess().CreateLichHoiChanProcess().AddChuyenGias(ORenderInfo, OTaoLichHoiChan.ID, bacsis, donViCongTacTens);
                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_GiayToDiChuyenGiao(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_GiayToDiChuyenGiaoCls ck = new DM_GiayToDiChuyenGiaoCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.Ma = item.Cells[0].Value;
                    ck.Ten = item.Cells[1].Value;
                    ck.MoTa = item.Cells[2].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[3].Value);
                    ck.Stt = int.Parse(item.Cells[4].Value);
                    ck.TuNgay = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : !string.IsNullOrEmpty(item.Cells[5].Value) ? DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_GiayToDiChuyenGiaoProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
        private static string ImportDM_TieuChuanThamGiaKhoaHoc(RenderInfoCls ORenderInfo, Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream);
            Worksheet sheet = workbook.Worksheets[0];

            #region check row
            int j = 0;
            foreach (var item in sheet.Rows)
            {
                if (j > 0)
                {

                    if (string.IsNullOrEmpty(item.Cells[0].Value))
                    {
                        return "Mã không được trống => dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[1].Value))
                    {
                        return "Tên không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[4].Value))
                    {
                        return "STT không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[5].Value))
                    {
                        return "Từ ngày không được trống dòng: " + (j + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(item.Cells[3].Value))
                    {
                        return "Hiệu lực không được trống dòng: " + (j + 1).ToString();
                    }
                }
                j++;
            }
            #endregion
            int i = 0;
            foreach (var item in sheet.Rows)
            {
                if (i > 0)
                {
                    DM_TieuChuanThamGiaKhoaHocCls ck = new DM_TieuChuanThamGiaKhoaHocCls();
                    ck.Id = System.Guid.NewGuid().ToString();
                    ck.Ma = item.Cells[0].Value;
                    ck.Ten = item.Cells[1].Value;
                    ck.MoTa = item.Cells[2].Value ?? "";
                    ck.HieuLuc = int.Parse(item.Cells[3].Value);
                    ck.Stt = int.Parse(item.Cells[4].Value);
                    ck.TuNgay = item.Cells[5].Value2 is DateTime ? (DateTime)item.Cells[5].Value2 : !string.IsNullOrEmpty(item.Cells[5].Value) ? DateTime.ParseExact(item.Cells[5].Value, "dd/MM/yyyy", null) : DateTime.Now;
                    ck.NgayTao = DateTime.Now;
                    var checkma = CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CheckCode(ORenderInfo, item.Cells[0].Value);
                    if (checkma != null)
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Save(ORenderInfo, checkma.Id, ck);
                    }
                    else
                    {
                        CallBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Add(ORenderInfo, ck);
                    }

                }
                i++;
            }
            return "Đã Import thành công!";
        }
    }
}
