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
    public class DT_LopHocPermission : PermissionFunctionTemplate
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
                return "DT_LopHocPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "DT_LopHocPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên lớp học";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("19D5AC92-F58B-4589-9C45-631F60262302",DT_KhoaHocCls.eLopHocPermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("40661715-7D7E-40C0-8A91-A26915D75D7D",DT_KhoaHocCls.eLopHocPermission.QuanLyKeHoach.ToString(),"Quản lý kế hoạch",PermissionFunctionId),
                    new PermissionFunctionItemCls("8F722FBE-E723-405D-B802-D3395E9C6967",DT_KhoaHocCls.eLopHocPermission.QuanLyTaiLieu.ToString(),"Quản lý tài liệu",PermissionFunctionId),
                    new PermissionFunctionItemCls("7B8A8F13-9671-4994-A2D8-767D86ACBE79",DT_KhoaHocCls.eLopHocPermission.DiemDanh.ToString(),"Điểm danh",PermissionFunctionId),
                    new PermissionFunctionItemCls("60BBAC3F-B0B9-4171-967E-ECB53B9C6D36",DT_KhoaHocCls.eLopHocPermission.NhapKetQuaDaoTao.ToString(),"Nhập kết quả đào tạo",PermissionFunctionId),
                    new PermissionFunctionItemCls("A0023FB4-6ADB-4F20-B926-5591281FF24F",DT_KhoaHocCls.eLopHocPermission.KetThuc.ToString(),"Kết thúc",PermissionFunctionId)
                };
            }
        }
    }
}