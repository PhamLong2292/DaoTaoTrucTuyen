using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Permission
{
    public class DT_DangKyPermission : PermissionFunctionTemplate
    {
        public override int IsRoot
        {
            get
            {
                return 1;
            }
        }

        public override string PermissionFunctionId
        {
            get
            {
                return StaticPermissionFunctionId;
            }
        }

        public static string StaticPermissionFunctionId
        {
            get
            {
                return "DT_DangKyPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "DT_DangKyPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên đăng ký khóa học";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("479C69E7-2ABC-4BC4-94B5-AF5E090D2B5D",DT_KetQuaDaoTaoCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("11BE1EDF-1F69-41D4-90C1-A056C74D7F69",DT_KetQuaDaoTaoCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("3605D805-9759-4907-B7A0-B669E76E6F43",DT_KetQuaDaoTaoCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("7DF9D014-C2A8-4BBF-81C0-07A69BF0415D",DT_KetQuaDaoTaoCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("ED8B6FE2-4527-4986-8109-E80753334E26",DT_KetQuaDaoTaoCls.ePermission.TongHopDangKy.ToString(),"Tổng hợp đăng ký",PermissionFunctionId)
                };
            }
        }
    }
}