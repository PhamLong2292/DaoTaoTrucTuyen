using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class KyThuatChuyenGiaoPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "KyThuatChuyenGiaoPermission"; } }
        public override string PermissionFunctionCode { get { return "KyThuatChuyenGiaoPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục kỹ thuật chuyển giao"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("56EE3F18-787B-4B93-B56B-FEB01C73299C", DM_KyThuatChuyenGiaoCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("64C40AB7-89BC-40D1-A2CB-ED91D55F01EB", DM_KyThuatChuyenGiaoCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("A324BD75-3064-4146-86EE-2B35E7F5BB04", DM_KyThuatChuyenGiaoCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("91754FFB-7B3D-4410-8375-A17B5D9C7955", DM_KyThuatChuyenGiaoCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("05B4A7AA-CA5C-4EB2-9536-B8E6E2D81ED0", DM_KyThuatChuyenGiaoCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("4EA5B0AB-4208-4EBC-B69F-80158639DC4D", DM_KyThuatChuyenGiaoCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
