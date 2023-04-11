using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Permission
{
    public class DictionaryPermission : PermissionFunctionTemplate
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
                return "DictionaryPermission";
            }
        }

        public override string PermissionFunctionCode
        {
            get
            {
                return "DictionaryPermission";
            }
        }

        public override string PermissionFunctionName
        {
            get
            {
                return "Quyền từ điển";
            }
        }

        public override PermissionFunctionItemCls[] PermissionFunctionItems
        {
            get
            {
                return new PermissionFunctionItemCls[]
                {
                    new PermissionFunctionItemCls("Dictionary.ExamList","access","Quyền truy cập",PermissionFunctionId),
                    new PermissionFunctionItemCls("Dictionary.Unit","access","Quyền truy cập",PermissionFunctionId),
                };
            }
        }
    }
}
