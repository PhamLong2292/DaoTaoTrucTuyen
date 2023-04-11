using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OneTSQ.ReportUtility
{
    [Serializable, DataContract]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ReportAttribute : Attribute
    {
        [DataMember]
        public string ID;
        [DataMember] 
        public string Name;
        [DataMember] 
        public string Title;
        [DataMember] 
        public string ReportPath;
        [DataMember]
        public Report.eType Type;

    }

    [Serializable, DataContract]
    public sealed class BusinessReportAttribute : ReportAttribute
    {
    }

    [Serializable, DataContract]
    public sealed class ObjectReportAttribute : ReportAttribute
    {
    }
}
