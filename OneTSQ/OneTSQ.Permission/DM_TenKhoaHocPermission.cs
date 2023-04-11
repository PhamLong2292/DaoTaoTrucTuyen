using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class DM_TenKhoaHocPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "DmTenKhoaHocPermission"; } }
        public override string PermissionFunctionCode { get { return "DmTenKhoaHocPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục tên khóa học"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("1885BF30-36C0-41F7-B647-C22CB672ADF2", DM_TenKhoaHocCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("8B141499-5C28-466E-9890-A6FDBC9C8BC5", DM_TenKhoaHocCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("C69D79D3-4F8C-444A-911A-BCFE32D55CE0", DM_TenKhoaHocCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("55A97DC1-1863-4ED4-A18E-EE86806281A7", DM_TenKhoaHocCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("2C4ED171-445D-4B81-BBB9-75ADCF43F59B", DM_TenKhoaHocCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("86071BD7-F516-4AA9-8AAF-79B892B26770", DM_TenKhoaHocCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
