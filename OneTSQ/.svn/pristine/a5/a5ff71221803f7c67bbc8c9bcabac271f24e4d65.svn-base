using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ICommonProcess
    {
        string ServiceId { get; }
        DataTable GetData(RenderInfoCls ORenderInfo, FilterCls filter, string query, Dictionary<string, object> param);
    }

    public class CommonTemplate : ICommonProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual DataTable GetData(RenderInfoCls ORenderInfo, FilterCls filter, string query, Dictionary<string, object> param) { return null; }
    }
}
