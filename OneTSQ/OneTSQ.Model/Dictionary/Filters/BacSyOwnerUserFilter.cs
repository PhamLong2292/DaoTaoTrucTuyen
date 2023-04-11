using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BacSyOwnerUserFilterCls : FilterCls
    {
        public string BACSYID;
        public string OWNERUSERID;
    }
}

public class BacSyOwnerUserFilterParser
{
    public static BacSyOwnerUserFilterCls CreateInstance()
    {
        BacSyOwnerUserFilterCls OBacSyOwnerUserFilter = new BacSyOwnerUserFilterCls();
        return OBacSyOwnerUserFilter;
    }


    public static BacSyOwnerUserFilterCls ParseFromDataRow(DataRow dr)
    {
        BacSyOwnerUserFilterCls OBacSyOwnerUserFilter = new BacSyOwnerUserFilterCls();
        return OBacSyOwnerUserFilter;
    }


    public static BacSyOwnerUserFilterCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BacSyOwnerUserFilterCls OBacSyOwnerUserFilter = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBacSyOwnerUserFilter;
    }



    public static XmlCls GetXml(BacSyOwnerUserFilterCls OBacSyOwnerUserFilter)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
