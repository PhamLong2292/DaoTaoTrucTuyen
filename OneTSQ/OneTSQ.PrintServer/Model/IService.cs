using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.PrintServer
{
    public interface IService
    {
        string ServiceId { get; }
        string ServiceName { get; }
    }

    public class PrintServiceTemplate:IService
    {
        public virtual string ServiceId { get { return null; } }
        public virtual string ServiceName { get { return null; } }
    }
}
