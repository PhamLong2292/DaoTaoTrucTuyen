using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Permission
{
    public class DT_KhoaHocPermission : PermissionFunctionTemplate
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
                return "DT_KhoaHocPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "DT_KhoaHocPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên khóa học";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("7BD3C7E0-5F99-48FC-A200-4CF8ECC5A957",DT_KhoaHocCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("7E96A65C-7FBC-433F-A480-ED6B1F948E1A",DT_KhoaHocCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("64FAA236-A56B-42E4-8A16-7618CEC20450",DT_KhoaHocCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("C8F17C4A-DAAD-40F1-950C-771FB181C84F",DT_KhoaHocCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("B2D592EB-0098-4DA7-96E4-62AD456697D8",DT_KhoaHocCls.ePermission.GuiDuyet.ToString(),"Gửi duyệt",PermissionFunctionId),
                    new PermissionFunctionItemCls("89BEE9F3-2F95-40C8-9519-297E2F57C2F0",DT_KhoaHocCls.ePermission.PheDuyet.ToString(),"Phê duyệt",PermissionFunctionId)
                };
            }
        }
    }
}