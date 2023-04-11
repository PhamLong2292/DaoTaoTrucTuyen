using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class DM_NhomKhoaHocPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "DmNhomKhoaHocPermission"; } }
        public override string PermissionFunctionCode { get { return "DmNhomKhoaHocPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục nhóm khóa học"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("EBC5F47B-CF72-44AE-8542-08F0BC9A92E7", DM_NhomKhoaHocCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("9D056F04-C15D-4E21-9709-1AFBA16BE156", DM_NhomKhoaHocCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("5E0A7E25-ED25-4A3E-A48B-A4665E39BEC4", DM_NhomKhoaHocCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("284AE0D5-AF4B-4ACF-8A55-B378303EEC10", DM_NhomKhoaHocCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("3119E052-20B3-4CD5-80D1-3B8C468F21F4", DM_NhomKhoaHocCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("472C1144-EB9B-44D9-B35B-F794862F5222", DM_NhomKhoaHocCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
