using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class TrangThietBiTruyenHinhTtPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "TrangThietBiTruyenHinhTtPermission"; } }
        public override string PermissionFunctionCode { get { return "TrangThietBiTruyenHinhTtPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục Trang thiết bị truyền hình trực tuyến"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("C0ABD4CF-A027-4336-BCDD-72E3D6BC624E", DM_TrangThietBiTruyenHinhTtCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("AC6975F0-A090-458F-98A1-75352BEBEA1E", DM_TrangThietBiTruyenHinhTtCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("9955D357-FAD4-4EEC-B1C2-88EE41FDF12C", DM_TrangThietBiTruyenHinhTtCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("FB418325-B022-4CAC-BAD0-826F036ABE49", DM_TrangThietBiTruyenHinhTtCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("B358C9FB-C600-4AE9-A088-1AA1EA68F5A9", DM_TrangThietBiTruyenHinhTtCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("E4BFA80A-B8F9-4B25-A29D-DF0C6A2B0258", DM_TrangThietBiTruyenHinhTtCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
