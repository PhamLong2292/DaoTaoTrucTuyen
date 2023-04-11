using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class ChuyenKhoaDaoTaoTtPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "ChuyenKhoaDaoTaoTtPermission"; } }
        public override string PermissionFunctionCode { get { return "ChuyenKhoaDaoTaoTtPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục Chuyên khoa đào tạo trực tuyến"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("AF81854C-E026-41EB-8F3A-2C6AC75435C4", DM_ChuyenKhoaDaoTaoTtCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("A5790016-B6A7-4FF7-91A4-241FFCD06956", DM_ChuyenKhoaDaoTaoTtCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("13B9590B-33AC-42B9-B545-BFC7D6F0F0AB", DM_ChuyenKhoaDaoTaoTtCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("B39798F3-D24A-407E-9F08-5F1A2AC8E675", DM_ChuyenKhoaDaoTaoTtCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("88723106-DF9B-44F0-97A1-34D09517A8E1", DM_ChuyenKhoaDaoTaoTtCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("656A1661-5BA1-47FB-A17C-4D63A66C2C16", DM_ChuyenKhoaDaoTaoTtCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
