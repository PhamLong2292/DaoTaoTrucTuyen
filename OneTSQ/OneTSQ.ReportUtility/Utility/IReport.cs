using OneTSQ.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility
{
    public interface IReport
    {
        ReportAttribute Attibute { get; }
        Type ReportType { get; }
        AjaxOut OnLoad(RenderInfoCls ORenderInfo, object filter);
        AjaxOut OnLoad(RenderInfoCls ORenderInfo, object filter, object[] parameters);
        Stream OnLoadStream(RenderInfoCls ORenderInfo, object filter);
        Stream OnLoadStream(RenderInfoCls ORenderInfo, object filter, object[] parameters);
        AjaxOut OnPrint(RenderInfoCls ORenderInfo, object filter);
        AjaxOut OnPrint(RenderInfoCls ORenderInfo, object filter, object[] parameters);
        AjaxOut OnExport(RenderInfoCls ORenderInfo, object filter);
        bool CanExecute(RenderInfoCls ORenderInfo, string id);
        //bool EnableExecute(RenderInfoCls ORenderInfo, string id);
        bool IsMultiTemplate { get; }
        bool AllowExport { get; }
    }
}
