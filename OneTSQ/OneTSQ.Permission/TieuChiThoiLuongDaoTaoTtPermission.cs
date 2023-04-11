using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class TieuChiThoiLuongDaoTaoTtPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "TieuChiThoiLuongDaoTaoTtPermission"; } }
        public override string PermissionFunctionCode { get { return "TieuChiThoiLuongDaoTaoTtPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục Tiêu chí đánh giá thời lượng đào tạo trực tuyến"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("75055EC0-5726-4A6A-B954-D5DD5D3C52E0", DM_TrangThietBiTruyenHinhTtCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("1C9E47F5-2401-4B8D-8004-F8B0A10797C6", DM_TrangThietBiTruyenHinhTtCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("5743D6FE-D309-4CF3-BF6A-8BFFEAAFE72B", DM_TrangThietBiTruyenHinhTtCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("31D259CF-B441-476B-AD97-C059B41B0C17", DM_TrangThietBiTruyenHinhTtCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("784EEAF4-29CB-4690-9807-72766B57B745", DM_TrangThietBiTruyenHinhTtCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("5FD25245-4316-4F38-8561-2AC21E8F0950", DM_TrangThietBiTruyenHinhTtCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
