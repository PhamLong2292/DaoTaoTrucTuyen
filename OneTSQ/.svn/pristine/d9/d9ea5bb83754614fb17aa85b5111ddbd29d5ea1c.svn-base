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
    public class ThamVanCaBenhPermission : PermissionFunctionTemplate
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
                return "ThamVanCaBenhPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "ThamVanCaBenhPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên ca bệnh tham vấn";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("53A2D4D6-8BBF-4059-8D8A-6F3DC5FFCCF5",ThamVanCaBenh.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("3162D16E-B076-48DF-90E1-29F9C400B2E7",ThamVanCaBenh.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("D6D3B983-619B-41FA-8F30-ED0D113B789A",ThamVanCaBenh.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("4239C14F-6618-4CF4-AEE1-0DF91C0B6357",ThamVanCaBenh.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("B1141EFD-0527-45C6-B6BD-5342E7EB2E92",ThamVanCaBenh.ePermission.DeNghi.ToString(),"Đề nghị",PermissionFunctionId),
                    new PermissionFunctionItemCls("FBD7DE2C-0A50-4F09-9A37-F08EA3A5FA6F",ThamVanCaBenh.ePermission.DuyetDeNghi.ToString(),"Duyệt đề nghị",PermissionFunctionId)
                };
            }
        }
    }
}