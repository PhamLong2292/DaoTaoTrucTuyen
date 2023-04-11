using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;
using OneTSQ.Common;
using OneTSQ.Core.Model;

namespace OneTSQ.Permission
{
    public class PhieuDanhGiaChatLuongDaoTaoPermission : PermissionFunctionTemplate
    {
        public override int IsRoot
        {
            get
            {
                return 1;
            }
        }

        public override string PermissionFunctionId
        {
            get
            {
                return StaticPermissionFunctionId;
            }
        }

        public static string StaticPermissionFunctionId
        {
            get
            {
                return "PhieuDanhGiaChatLuongDaoTaoPer";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "PhieuDanhGiaChatLuongDaoTaoPer";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên phiếu đánh giá chất lượng đào tạo";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("43A7AD5E-F015-49F7-AD0C-84FA224F6A2D",PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("3DC3F0DC-D49C-433D-9B72-453F44F068EE",PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("352D8C2C-13A6-4E2A-85AF-AFCD282CEEC6",PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("A327E030-323E-43C8-A1C5-8048247364FC",PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("B9F5AB9A-F9A4-4AD5-AA82-91DF95F18B30",PhieuDanhGiaChatLuongDaoTaoCls.ePermission.Gui.ToString(),"Gửi",PermissionFunctionId),
                    new PermissionFunctionItemCls("A3CD5A9F-59E2-419A-8032-36985EF4904D",PhieuDanhGiaChatLuongDaoTaoCls.ePermission.TongHop.ToString(),"Tổng hợp",PermissionFunctionId)
                };
            }
        }
    }
}