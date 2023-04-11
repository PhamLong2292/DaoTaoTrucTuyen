using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class CommonProcessBll : CommonTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "CommonProcessBll";
            }
        }


        public override DataTable GetData(RenderInfoCls ORenderInfo, FilterCls filter, string query, Dictionary<string, object> param)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateCommonProcess().GetData(ActionSqlParam, filter, query, param);
        }
    }
}
