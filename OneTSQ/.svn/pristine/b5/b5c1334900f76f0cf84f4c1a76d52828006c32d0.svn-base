using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Permission
{
    public class LichHoiChanPermission : PermissionFunctionTemplate
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
                return "LichHoiChanPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "LichHoiChanPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên lịch hội chẩn";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("CDACDBA5-4AD2-4115-93BB-7080AC4E3713",LichHoiChanCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("32FC384B-1E10-455B-ABE9-CE290AB6EF89",LichHoiChanCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("AA5D82DC-8DF1-4CED-912F-1815F667EB2A",LichHoiChanCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("DC7B8621-D64B-40B2-8C87-550C67C4A08B",LichHoiChanCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("2B0069C1-49FC-4D65-929B-1C8AE06DA63E",LichHoiChanCls.ePermission.ChuyenDuyet.ToString(),"Chuyển duyệt",PermissionFunctionId),
                    new PermissionFunctionItemCls("DC7B8621-D64B-40B2-8C87-550C67C4A09B",LichHoiChanCls.ePermission.Huy.ToString(),"Hủy",PermissionFunctionId),
                };
            }
        }
    }
}