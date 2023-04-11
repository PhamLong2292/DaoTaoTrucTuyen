using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class BacSyOwnerUserCls
    {
        public string ID;
        public string BACSYID = "";
        public string OWNERUSERID = "";
    }
}

public class BacSyOwnerUserParser
{
    public static BacSyOwnerUserCls CreateInstance()
    {
        BacSyOwnerUserCls OBacSyOwnerUser = new BacSyOwnerUserCls();
        return OBacSyOwnerUser;
    }


    public static BacSyOwnerUserCls ParseFromDataRow(DataRow dr)
    {
        BacSyOwnerUserCls OBacSyOwnerUser = new BacSyOwnerUserCls();
        OBacSyOwnerUser.ID = CoreXmlUtility.GetString(dr, "ID", true);
        OBacSyOwnerUser.BACSYID = CoreXmlUtility.GetString(dr, "BACSYID", true);
        OBacSyOwnerUser.OWNERUSERID = CoreXmlUtility.GetString(dr, "OWNERUSERID", true);
        return OBacSyOwnerUser;
    }

    public static BacSyOwnerUserCls[] ParseFromDataTable(DataTable dtTable)
    {
        BacSyOwnerUserCls[] BacSyOwnerUsers = new BacSyOwnerUserCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            BacSyOwnerUsers[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return BacSyOwnerUsers;
    }


    public static BacSyOwnerUserCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        BacSyOwnerUserCls[] BacSyOwnerUsers = ParseFromDataTable(ds.Tables[0]);
        return BacSyOwnerUsers;
    }


    public static BacSyOwnerUserCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        BacSyOwnerUserCls OBacSyOwnerUser = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return OBacSyOwnerUser;
    }


    public static XmlCls GetXml(BacSyOwnerUserCls[] BacSyOwnerUsers)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("OWNERUSERID");
        for (int iIndex = 0; iIndex < BacSyOwnerUsers.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                BacSyOwnerUsers[iIndex].ID,
                BacSyOwnerUsers[iIndex].BACSYID,
                BacSyOwnerUsers[iIndex].OWNERUSERID
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(BacSyOwnerUserCls OBacSyOwnerUser)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("BACSYID");
        ds.Tables["info"].Columns.Add("OWNERUSERID");
        ds.Tables["info"].Rows.Add(new object[]
        {
                OBacSyOwnerUser.ID,
                OBacSyOwnerUser.BACSYID,
                OBacSyOwnerUser.OWNERUSERID
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
