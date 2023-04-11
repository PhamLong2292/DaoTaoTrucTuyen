using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;
using OneTSQ.Common;
using OneTSQ.Core.Model;

namespace OneTSQ.Permission
{
    public class DT_BcTongKetCongTacDaoTaoPermission : PermissionFunctionTemplate
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
                return "DT_BcTongKetCongTacDaoTaoPer";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "DT_BcTongKetCongTacDaoTaoPer";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên Báo cáo tổng kết công tác đào tạo";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("E37EF21A-2A7D-4340-9906-EC5BB5ED8CAF",DT_BcTongKetCongTacDaoTaoCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("64F2DE06-B53A-45F5-AE41-262592203FAA",DT_BcTongKetCongTacDaoTaoCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("E0057505-C929-4C35-A1A4-0833170BFF7D",DT_BcTongKetCongTacDaoTaoCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("D1BB7AC1-2C00-4E4B-BC91-7447BBFC0263",DT_BcTongKetCongTacDaoTaoCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId)
                };
            }
        }
    }
}