using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Model
{

    public enum eSearch
    {
        SearchAll = 99
    }
    public enum eHieuLuc
    {
        Khong,
        Co,
        ChuaCapNhat
    }
    public enum eTuyChinh
    {
        KhongDuocSua,
        DuocSua,
    }
    public enum eLoai
    {
        Loai1,
        Loai2,
        LoaiDacBiet
    }
    public enum eNhomLopDichVu
    {
        PhauThuatThuThuat = 0,
        XetNghiem = 1,
        GiaiPhauBenh = 2,
        ChanDoanHinhAnh = 3,
        ThamDoChucNang = 4,
        CanLamSangChung = 5
    }
    public class Common
    {
        public readonly static Dictionary<int, string> HieuLucs = new Dictionary<int, string>()
        {
            { (int)eHieuLuc.Co, "Có" },
            { (int)eHieuLuc.Khong, "Không" },
            { (int)eHieuLuc.ChuaCapNhat, "Chưa cập nhật" }
        };
        //Trả về phần cố định của biểu thức trong bộ mã.
        public static string GetDisplayPart(string bieuThuc)
        {
            if (!string.IsNullOrEmpty(bieuThuc))
                return bieuThuc.Split('{')[0];
            return null;
        }
        //Trả về chuỗi mask của biểu thức trong bộ mã.
        public static string GetMaskString(string bieuThuc)
        {
            if (!string.IsNullOrEmpty(bieuThuc))
            {
                var bieuThucArr = bieuThuc.Split('{');
                //Biểu thức dạng 18/{MaBN,\d\d\d\d\d\d}
                if (bieuThucArr.Length == 2)
                {
                    return bieuThucArr[0] + bieuThucArr[1].Split(',')[1].Split('}')[0];
                }
            }
            return null;
        }
        //Trả về mã bộ đếm của biểu thức trong bộ mã.
        public static string GetMaBoDem(string bieuThuc)
        {
            if (!string.IsNullOrEmpty(bieuThuc))
            {
                var bieuThucArr = bieuThuc.Split('{');
                //Biểu thức dạng 18/{MaBN,\d\d\d\d\d\d}
                if (bieuThucArr.Length == 2)
                {
                    return bieuThucArr[1].Split(',')[0];
                }
            }
            return null;
        }
        //Trả về định dạng giá trị bộ đếm trả về của biểu thức trong bộ mã.
        public static string GetValueFormat(string bieuThuc)
        {
            if (!string.IsNullOrEmpty(bieuThuc))
            {
                var bieuThucArr = bieuThuc.Split('{');
                //Biểu thức dạng 18/{MaBN,\d\d\d\d\d\d}
                if (bieuThucArr.Length == 2)
                {
                    return bieuThucArr[1].Split(',')[1].Split('}')[0];
                }
            }
            return null;
        }
        public static OneMES3.SYS.Core.Model.RenderInfoCls CreateRenderInfo( OneTSQ.Core.Model.RenderInfoCls ORenderInfo)
        {
            if (ORenderInfo == null)
                return null;

            var sRenderInfo = new OneMES3.SYS.Core.Model.RenderInfoCls();
            sRenderInfo.AssetLevelCode = ORenderInfo.AssetLevelCode;
            sRenderInfo.LoginName = ORenderInfo.LoginName;
            sRenderInfo.OwnerCode = ORenderInfo.OwnerCode;
            sRenderInfo.OwnerId = ORenderInfo.OwnerId;
            sRenderInfo.OwnerUserId = ORenderInfo.OwnerUserId;
            sRenderInfo.RoleId = ORenderInfo.RoleId;
            sRenderInfo.SiteAssetLevelId = ORenderInfo.SiteAssetLevelId;
            sRenderInfo.SiteId = ORenderInfo.SiteId;
            sRenderInfo.SiteLanguage = ORenderInfo.SiteLanguage;
            sRenderInfo.UserSessionId = ORenderInfo.UserSessionId;
            sRenderInfo.WebPage = ORenderInfo.WebPage;
            sRenderInfo.WebPartId = ORenderInfo.WebPartId;
            return sRenderInfo;
        }
    }
    public class CalendarEvent
    {
        public string id;
        public string title;
        public string url;
        public string start;
        public string end;
        public string color;
        public string textColor;
        public string someKey;
        public string allDay;
        public string CreatedBy;
        public string StatusRemark;
        public string DurationMnt;
        public string Studio;
        public string tooltip;
    }

}
