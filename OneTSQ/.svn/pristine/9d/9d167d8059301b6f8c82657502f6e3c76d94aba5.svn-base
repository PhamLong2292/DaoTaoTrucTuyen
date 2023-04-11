using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class TieuChiThoiGianDaoTaoTtPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "TieuChiThoiGianDaoTaoTtPermission"; } }
        public override string PermissionFunctionCode { get { return "TieuChiThoiGianDaoTaoTtPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục Tiêu chí đánh giá thời gian đào tạo trực tuyến"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("4B1B3C0E-ABEA-4F20-995B-699F87367F6D", DM_TrangThietBiTruyenHinhTtCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("D9F2071F-D2EC-4C42-9F32-5112FFB64B23", DM_TrangThietBiTruyenHinhTtCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("C74CC673-B08D-4E17-8200-7656686DC93A", DM_TrangThietBiTruyenHinhTtCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("998E71DD-DB7C-456B-847D-D2025D670F2F", DM_TrangThietBiTruyenHinhTtCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("63DCD291-5779-44B3-891B-EA4B887822E8", DM_TrangThietBiTruyenHinhTtCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("75BAD6CE-8DAB-4D86-AD05-54FDCCF90B23", DM_TrangThietBiTruyenHinhTtCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
