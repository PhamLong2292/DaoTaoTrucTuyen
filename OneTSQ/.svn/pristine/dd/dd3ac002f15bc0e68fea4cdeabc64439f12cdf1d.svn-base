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
    public class BcDanhGiaChatLuongDaoTaoPermission : PermissionFunctionTemplate
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
                return "BcDanhGiaChatLuongDaoTaoPer";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "BcDanhGiaChatLuongDaoTaoPer";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên báo cáo đánh giá chất lượng đào tạo";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("51AA45E5-4F6E-4F79-806E-A38CFA749186",BcDanhGiaChatLuongDaoTaoCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("7A3A2D03-9532-4712-BCAF-7DC1FCFE8F8A",BcDanhGiaChatLuongDaoTaoCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("B04272D0-FAF3-4C85-8DF3-D50A6FD3EA6A",BcDanhGiaChatLuongDaoTaoCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("696DEDFA-E97C-4C76-9346-9C30A75A3414",BcDanhGiaChatLuongDaoTaoCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("1E609D67-97DE-4B99-BCB7-45A3BE7C65B0",BcDanhGiaChatLuongDaoTaoCls.ePermission.Export.ToString(),"Export",PermissionFunctionId)
                };
            }
        }
    }
}