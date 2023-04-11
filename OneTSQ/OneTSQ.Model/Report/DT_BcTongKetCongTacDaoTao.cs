using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Model
{
    public class DT_BcTongKetCongTacDaoTaoCls
    {
        public string ID;
        public int NAM;
        public DateTime TUNGAY;
        public DateTime DENNGAY;
        public string DTLIENTUC = "";
        public string DTTHEOKEHOACH = "";
        public string DTNANGCAO = "";
        public string DTTHEONGANSACHNHANUOC = "";
        public string DTTHEONHUCAUXAHOI = "";
        public string DTVIENTRUONG = "";
        public string CHUONGTRINHTAILIEU = "";
        public string THUANLOI = "";
        public string KHOKHAN = "";
        public string KHACPHUC = "";
        public string PHUONGHUONGKEHOACH = "";
        public string NGUOITAO_ID = "";
        public DateTime NGAYTAO;
        public string NGUOISUA_ID = "";
        public DateTime? NGAYSUA;
        public enum ePermission
        {
            Xem = 0,
            Them = 1,
            Sua = 2,
            Xoa = 3
        }
    }
}

public class DT_BcTongKetCongTacDaoTaoParser
{
    public static DT_BcTongKetCongTacDaoTaoCls CreateInstance()
    {
        DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao = new DT_BcTongKetCongTacDaoTaoCls();
        return ODT_BcTongKetCongTacDaoTao;
    }


    public static DT_BcTongKetCongTacDaoTaoCls ParseFromDataRow(DataRow dr)
    {
        DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao = new DT_BcTongKetCongTacDaoTaoCls();
        ODT_BcTongKetCongTacDaoTao.ID = CoreXmlUtility.GetString(dr, "ID", true);
        ODT_BcTongKetCongTacDaoTao.NAM = CoreXmlUtility.GetInt(dr, "NAM", true);
        ODT_BcTongKetCongTacDaoTao.TUNGAY = CoreXmlUtility.GetDate(dr, "TUNGAY", true);
        ODT_BcTongKetCongTacDaoTao.DENNGAY = CoreXmlUtility.GetDate(dr, "DENNGAY", true);
        ODT_BcTongKetCongTacDaoTao.DTLIENTUC = CoreXmlUtility.GetString(dr, "DTLIENTUC", true);
        ODT_BcTongKetCongTacDaoTao.DTTHEOKEHOACH = CoreXmlUtility.GetString(dr, "DTTHEOKEHOACH", true);
        ODT_BcTongKetCongTacDaoTao.DTNANGCAO = CoreXmlUtility.GetString(dr, "DTNANGCAO", true);
        ODT_BcTongKetCongTacDaoTao.DTTHEONGANSACHNHANUOC = CoreXmlUtility.GetString(dr, "DTTHEONGANSACHNHANUOC", true);
        ODT_BcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI = CoreXmlUtility.GetString(dr, "DTTHEONHUCAUXAHOI", true);
        ODT_BcTongKetCongTacDaoTao.DTVIENTRUONG = CoreXmlUtility.GetString(dr, "DTVIENTRUONG", true);
        ODT_BcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU = CoreXmlUtility.GetString(dr, "CHUONGTRINHTAILIEU", true);
        ODT_BcTongKetCongTacDaoTao.THUANLOI = CoreXmlUtility.GetString(dr, "THUANLOI", true);
        ODT_BcTongKetCongTacDaoTao.KHOKHAN = CoreXmlUtility.GetString(dr, "KHOKHAN", true);
        ODT_BcTongKetCongTacDaoTao.KHACPHUC = CoreXmlUtility.GetString(dr, "KHACPHUC", true);
        ODT_BcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH = CoreXmlUtility.GetString(dr, "PHUONGHUONGKEHOACH", true);
        ODT_BcTongKetCongTacDaoTao.NGUOITAO_ID = CoreXmlUtility.GetString(dr, "NGUOITAO_ID", true);
        ODT_BcTongKetCongTacDaoTao.NGAYTAO = CoreXmlUtility.GetDate(dr, "NGAYTAO", true);
        ODT_BcTongKetCongTacDaoTao.NGUOISUA_ID = CoreXmlUtility.GetString(dr, "NGUOISUA_ID", true);
        ODT_BcTongKetCongTacDaoTao.NGAYSUA = CoreXmlUtility.GetDateOrNull(dr, "NGAYSUA", true);
        return ODT_BcTongKetCongTacDaoTao;
    }

    public static DT_BcTongKetCongTacDaoTaoCls[] ParseFromDataTable(DataTable dtTable)
    {
        DT_BcTongKetCongTacDaoTaoCls[] DT_BcTongKetCongTacDaoTaos = new DT_BcTongKetCongTacDaoTaoCls[dtTable.Rows.Count];
        for (int iIndex = 0; iIndex < dtTable.Rows.Count; iIndex++)
        {
            DT_BcTongKetCongTacDaoTaos[iIndex] = ParseFromDataRow(dtTable.Rows[iIndex]);
        }
        return DT_BcTongKetCongTacDaoTaos;
    }


    public static DT_BcTongKetCongTacDaoTaoCls[] ParseFromMultiXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        DT_BcTongKetCongTacDaoTaoCls[] DT_BcTongKetCongTacDaoTaos = ParseFromDataTable(ds.Tables[0]);
        return DT_BcTongKetCongTacDaoTaos;
    }


    public static DT_BcTongKetCongTacDaoTaoCls ParseFromXml(string XmlData, string XmlDataSchema)
    {
        DataSet ds = CoreXmlUtility.GetDataSetFromXml(XmlData, XmlDataSchema);
        if (ds.Tables[0].Rows.Count == 0) return null;
        DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao = ParseFromDataRow(ds.Tables[0].Rows[0]);
        return ODT_BcTongKetCongTacDaoTao;
    }


    public static XmlCls GetXml(DT_BcTongKetCongTacDaoTaoCls[] DT_BcTongKetCongTacDaoTaos)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("NAM", typeof(int));
        ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DTLIENTUC");
        ds.Tables["info"].Columns.Add("DTTHEOKEHOACH");
        ds.Tables["info"].Columns.Add("DTNANGCAO");
        ds.Tables["info"].Columns.Add("DTTHEONGANSACHNHANUOC");
        ds.Tables["info"].Columns.Add("DTTHEONHUCAUXAHOI");
        ds.Tables["info"].Columns.Add("DTVIENTRUONG");
        ds.Tables["info"].Columns.Add("CHUONGTRINHTAILIEU");
        ds.Tables["info"].Columns.Add("THUANLOI");
        ds.Tables["info"].Columns.Add("KHOKHAN");
        ds.Tables["info"].Columns.Add("KHACPHUC");
        ds.Tables["info"].Columns.Add("PHUONGHUONGKEHOACH");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        for (int iIndex = 0; iIndex < DT_BcTongKetCongTacDaoTaos.Length; iIndex++)
        {
            ds.Tables["info"].Rows.Add(new object[]
            {
                DT_BcTongKetCongTacDaoTaos[iIndex].ID,
                DT_BcTongKetCongTacDaoTaos[iIndex].NAM,
                DT_BcTongKetCongTacDaoTaos[iIndex].TUNGAY,
                DT_BcTongKetCongTacDaoTaos[iIndex].DENNGAY,
                DT_BcTongKetCongTacDaoTaos[iIndex].DTLIENTUC,
                DT_BcTongKetCongTacDaoTaos[iIndex].DTTHEOKEHOACH,
                DT_BcTongKetCongTacDaoTaos[iIndex].DTNANGCAO,
                DT_BcTongKetCongTacDaoTaos[iIndex].DTTHEONGANSACHNHANUOC,
                DT_BcTongKetCongTacDaoTaos[iIndex].DTTHEONHUCAUXAHOI,
                DT_BcTongKetCongTacDaoTaos[iIndex].DTVIENTRUONG,
                DT_BcTongKetCongTacDaoTaos[iIndex].CHUONGTRINHTAILIEU,
                DT_BcTongKetCongTacDaoTaos[iIndex].THUANLOI,
                DT_BcTongKetCongTacDaoTaos[iIndex].KHOKHAN,
                DT_BcTongKetCongTacDaoTaos[iIndex].KHACPHUC,
                DT_BcTongKetCongTacDaoTaos[iIndex].PHUONGHUONGKEHOACH,
                DT_BcTongKetCongTacDaoTaos[iIndex].NGUOITAO_ID,
                DT_BcTongKetCongTacDaoTaos[iIndex].NGAYTAO,
                DT_BcTongKetCongTacDaoTaos[iIndex].NGUOISUA_ID,
                DT_BcTongKetCongTacDaoTaos[iIndex].NGAYSUA
            });
        }
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }


    public static XmlCls GetXml(DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("info");
        ds.Tables["info"].Columns.Add("ID");
        ds.Tables["info"].Columns.Add("NAM", typeof(int));
        ds.Tables["info"].Columns.Add("TUNGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DENNGAY", typeof(DateTime));
        ds.Tables["info"].Columns.Add("DTLIENTUC");
        ds.Tables["info"].Columns.Add("DTTHEOKEHOACH");
        ds.Tables["info"].Columns.Add("DTNANGCAO");
        ds.Tables["info"].Columns.Add("DTTHEONGANSACHNHANUOC");
        ds.Tables["info"].Columns.Add("DTTHEONHUCAUXAHOI");
        ds.Tables["info"].Columns.Add("DTVIENTRUONG");
        ds.Tables["info"].Columns.Add("CHUONGTRINHTAILIEU");
        ds.Tables["info"].Columns.Add("THUANLOI");
        ds.Tables["info"].Columns.Add("KHOKHAN");
        ds.Tables["info"].Columns.Add("KHACPHUC");
        ds.Tables["info"].Columns.Add("PHUONGHUONGKEHOACH");
        ds.Tables["info"].Columns.Add("NGUOITAO_ID");
        ds.Tables["info"].Columns.Add("NGAYTAO", typeof(DateTime));
        ds.Tables["info"].Columns.Add("NGUOISUA_ID");
        ds.Tables["info"].Columns.Add("NGAYSUA", typeof(DateTime?));
        ds.Tables["info"].Rows.Add(new object[]
        {
                ODT_BcTongKetCongTacDaoTao.ID,
                ODT_BcTongKetCongTacDaoTao.NAM,
                ODT_BcTongKetCongTacDaoTao.TUNGAY,
                ODT_BcTongKetCongTacDaoTao.DENNGAY,
                ODT_BcTongKetCongTacDaoTao.DTLIENTUC,
                ODT_BcTongKetCongTacDaoTao.DTTHEOKEHOACH,
                ODT_BcTongKetCongTacDaoTao.DTNANGCAO,
                ODT_BcTongKetCongTacDaoTao.DTTHEONGANSACHNHANUOC,
                ODT_BcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI,
                ODT_BcTongKetCongTacDaoTao.DTVIENTRUONG,
                ODT_BcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU,
                ODT_BcTongKetCongTacDaoTao.THUANLOI,
                ODT_BcTongKetCongTacDaoTao.KHOKHAN,
                ODT_BcTongKetCongTacDaoTao.KHACPHUC,
                ODT_BcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH,
                ODT_BcTongKetCongTacDaoTao.NGUOITAO_ID,
                ODT_BcTongKetCongTacDaoTao.NGAYTAO,
                ODT_BcTongKetCongTacDaoTao.NGUOISUA_ID,
                ODT_BcTongKetCongTacDaoTao.NGAYSUA
        });
        XmlCls OXml = new XmlCls();
        OXml.XmlData = ds.GetXml();
        OXml.XmlDataSchema = ds.GetXmlSchema();
        return OXml;
    }
}
