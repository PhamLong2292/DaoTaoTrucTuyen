using OneTSQ.Core.Model;
using OneTSQ.Model;

namespace OneTSQ.Permission
{
    public class DM_GiayToDiChuyenGiaoPermission : PermissionFunctionTemplate
    {
        public override int IsRoot { get { return 1; } }
        public override string PermissionFunctionId { get { return StaticPermissionFunctionId; } }
        public static string StaticPermissionFunctionId { get { return "DmGiayToDiChuyenGiaoPermission"; } }
        public override string PermissionFunctionCode { get { return "DmGiayToDiChuyenGiaoPermission"; } }
        public override string PermissionFunctionName { get { return "Quyền trên danh mục giấy tờ đi chuyển giao"; } }
        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("38C65B24-A8DE-4CA0-98CE-E4E1C4411A51", DM_GiayToDiChuyenGiaoCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("B83A677F-06FE-4856-A9EE-8EDE52CDE63C", DM_GiayToDiChuyenGiaoCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("04517B35-455F-49A8-AA79-EFA9A9B5881E", DM_GiayToDiChuyenGiaoCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("EC5943DE-3DA7-4D3E-A609-71D9E4D39815", DM_GiayToDiChuyenGiaoCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("6653C213-1E16-424A-BFCA-95FFCEF4366B", DM_GiayToDiChuyenGiaoCls.ePermission.Import.ToString(),"Import",PermissionFunctionId),
                    new PermissionFunctionItemCls("7EFC58D6-2FE1-4D9D-B54F-B0FDC2C7A586", DM_GiayToDiChuyenGiaoCls.ePermission.Export.ToString(),"Export",PermissionFunctionId),
                };
            }
        }
    }
}
