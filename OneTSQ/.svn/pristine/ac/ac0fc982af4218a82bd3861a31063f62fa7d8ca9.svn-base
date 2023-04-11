using Newtonsoft.Json;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace OneTSQ.JSONP
{
    public class Record
    {
        public int total_count;
        public bool incomplete_results;
        public RecordItemCls[] items;
    }
    public class RecordItemCls
    {
        public string id = "10248";
        public string CustomerID = "WILMK";
        public string OrderDate = "1996-07-04 00:00:00";
        public string Freight = "32.3800";
        public string text = "Vins et alcools Chevalier";
    }


    public class OrderUtility
    {
        public static string GetData()
        {
            string Page = WebEnvironments.Request("page");
            if (string.IsNullOrEmpty(Page))
            {
                Page = "0";
            }
            int iPage = int.Parse(Page);
            string q = WebEnvironments.Request("q");

            Record ORecord = new Record();
            ORecord.total_count = 300;
            ORecord.incomplete_results = false;
            ORecord.items = new RecordItemCls[30];
            for (int iIndex = 0; iIndex < ORecord.items.Length; iIndex++)
            {
                ORecord.items[iIndex] = new RecordItemCls();
                ORecord.items[iIndex].id = System.Guid.NewGuid().ToString();
                ORecord.items[iIndex].text = "Vins et alcools Chevalier " + ORecord.items[iIndex].id;
            }

            string json = JsonConvert.SerializeObject(ORecord, Formatting.Indented);

            return json;
        }
    }

}
