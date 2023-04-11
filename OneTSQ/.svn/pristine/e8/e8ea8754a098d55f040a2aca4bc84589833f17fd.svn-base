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
    public class TuVanCaBenhPermission : PermissionFunctionTemplate
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
                return "TuVanCaBenhPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "TuVanCaBenhPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên ca bệnh tư vấn";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("7FC77299-250E-4CB3-8142-AF9463CA6819",TuVanCaBenh.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("CBFC6F6A-BBA3-46D4-AFF0-B7C98EBD7937",TuVanCaBenh.ePermission.TiepNhan.ToString(),"Tiếp nhận",PermissionFunctionId),
                    new PermissionFunctionItemCls("8616F1FD-48DB-4E22-8F12-95602E5BCF7C",TuVanCaBenh.ePermission.LapLich.ToString(),"Lập lịch",PermissionFunctionId),
                    new PermissionFunctionItemCls("B40A9D8C-28E2-4837-95AE-6EB1315A5F2C",TuVanCaBenh.ePermission.DuyetHoiChan.ToString(),"Duyệt hội chẩn",PermissionFunctionId)
                };
            }
        }
    }
}