﻿using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Permission
{
    public class DictionaryUnitPermission : PermissionFunctionTemplate
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
                return "DictionaryUnitPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "DictionaryUnitPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền từ điển danh mục đơn vị tính";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("C869A4DE-E718-4094-B1AE-00D07A3B290D","access","Quyền truy cập",PermissionFunctionId),
                    new PermissionFunctionItemCls("C869A4DE-E718-4094-B1AE-00D07A3B290G","add","Thêm",PermissionFunctionId),
                    new PermissionFunctionItemCls("C869A4DE-E718-4094-B1AE-00D07A3B290H","update","Sửa",PermissionFunctionId),
                    new PermissionFunctionItemCls("C869A4DE-E718-4094-B1AE-00D07A3B290Z","delete","Xóa",PermissionFunctionId),
                    new PermissionFunctionItemCls("C869A4DE-E718-4094-B1AE-00D07A3B290O","excel","Xuất Excel",PermissionFunctionId),
                    new PermissionFunctionItemCls("C869A4DE-E718-4094-B1AE-00D07A3B29**","import.excel","Nhập Excel",PermissionFunctionId),
                };
            }
        }
    }
}
