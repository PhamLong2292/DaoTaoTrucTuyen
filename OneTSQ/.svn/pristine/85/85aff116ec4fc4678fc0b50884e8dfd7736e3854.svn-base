using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class YKienBenhVienPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "YKienBenhVienPermission"; } }
        public override string PermissionFunctionCode { get { return "YKienBenhVienPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục biểu mẫu ý kiến bệnh viện"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("1C997A23-EBA3-4C61-A07D-3735D9603155", DM_YKienBenhVienCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("427B17B1-AD3D-43F7-95C6-76394135B6A2", DM_YKienBenhVienCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("F2654EDF-B777-45F4-A2D9-A54186B5C096", DM_YKienBenhVienCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("FF6A27E5-132C-4EB6-A994-FFB825649B31", DM_YKienBenhVienCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("B97C608B-4C8C-46DD-8F10-806BDDF7175E", DM_YKienBenhVienCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("52C6DF97-C091-46A8-ADB2-DD787E9415DD", DM_YKienBenhVienCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
