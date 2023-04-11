using OneTSQ.Bussiness.Sql;
using OneTSQ.Bussiness.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.Bussiness.Utility
{
    public class OneTSQBussinessUtility
    {
        public static IBussinessProcess CreateBussinessProcess()
        {
            return new SqlBussinessProcess();
        }
    }
}
