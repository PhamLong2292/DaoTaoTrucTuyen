﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Permission
{
    public class DT_LichChuyenGiaoPermission : PermissionFunctionTemplate
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
                return "DT_LichChuyenGiaoPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "DT_LichChuyenGiaoPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền trên lịch chuyển giao";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("BCEC3477-5286-45AA-A274-B251C9B138A6",DT_LichChuyenGiaoCls.ePermission.Xem.ToString(),"Xem",PermissionFunctionId),
                    new PermissionFunctionItemCls("DB5F0687-1DDA-4CFE-89A7-792F5CD1F857",DT_LichChuyenGiaoCls.ePermission.Them.ToString(),"Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("5A13D615-08FC-4CC1-AD06-5EC514BECA15",DT_LichChuyenGiaoCls.ePermission.Sua.ToString(),"Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("4A3C195D-5A68-4ECE-B0BF-B725C185B5E8",DT_LichChuyenGiaoCls.ePermission.Xoa.ToString(),"Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("E09D6DDC-B7E8-49FE-BDE1-723D18EACD24",DT_LichChuyenGiaoCls.ePermission.GuiDuyet.ToString(),"Gửi duyệt",PermissionFunctionId),
                    new PermissionFunctionItemCls("B57A8033-69C2-4925-8460-9AB6D36C1894",DT_LichChuyenGiaoCls.ePermission.PheDuyet.ToString(),"Phê duyệt",PermissionFunctionId)
                };
            }
        }
    }
}