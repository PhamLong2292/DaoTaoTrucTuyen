using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class DM_TieuChuanThamGiaKhoaHocPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "DmTieuChuanThamGiaKhoaHocPermission"; } }
        public override string PermissionFunctionCode { get { return "DmTieuChuanThamGiaKhoaHocPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục tiêu chuẩn tham giao khóa học"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("270349E0-A54A-41D4-8485-8EBEC1B549CD", DM_TieuChuanThamGiaKhoaHocCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("D54D0F69-3D13-429C-8D90-50F368FE44A6", DM_TieuChuanThamGiaKhoaHocCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("5CDD010D-802C-47CA-9174-408068932121", DM_TieuChuanThamGiaKhoaHocCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("0CDD92AF-A7A8-4D1E-8686-03E7E1C0A5D5", DM_TieuChuanThamGiaKhoaHocCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("EA4472C8-C0EB-4BE4-A7F7-404B7DD95371", DM_TieuChuanThamGiaKhoaHocCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("5B7944DD-931D-44FC-BB6C-50925C15641E", DM_TieuChuanThamGiaKhoaHocCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
