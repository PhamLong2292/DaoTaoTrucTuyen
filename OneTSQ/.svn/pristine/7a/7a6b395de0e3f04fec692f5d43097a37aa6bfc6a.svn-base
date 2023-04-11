using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Bussiness.Template;
using OneTSQ.Database.Service;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Sql
{
public class BienBanHoiChanToanLichProcessBll : BienBanHoiChanToanLichTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlBienBanHoiChanToanLichProcessBll";
        }
    }
    public override BienBanHoiChanToanLichCls[] Reading(ActionSqlParamCls ActionSqlParam,BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            if (OBienBanHoiChanToanLichFilter == null)
            {
                OBienBanHoiChanToanLichFilter = new BienBanHoiChanToanLichFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from BIENBANHOICHANTOANLICH where 1=1 ";
            Query += " order by TAOVAO DESC";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            BienBanHoiChanToanLichCls[] BienBanHoiChanToanLichs = BienBanHoiChanToanLichParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return BienBanHoiChanToanLichs;
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override long Count( ActionSqlParamCls ActionSqlParam,BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
           if (OBienBanHoiChanToanLichFilter == null)
               OBienBanHoiChanToanLichFilter = new BienBanHoiChanToanLichFilterCls();
           string Query = " select COUNT (*) from BIENBANHOICHANTOANLICH";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = BienBanHoiChanToanLichParser.CountFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return count;
    }
    catch (Exception ex)
    {
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Rollback();
            ActionSqlParam.Trans = null;
        }
        throw (ex);
    }
}
    public override BienBanHoiChanToanLichCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichFilterCls OBienBanHoiChanToanLichFilter, int PageIndex, int PageSize)
{
    IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
    bool HasTrans = ActionSqlParam.Trans != null;
    bool HasCommit = false;
    if (!HasTrans)
        ActionSqlParam.Trans = DBService.BeginTransaction();
    try
    {
        if (OBienBanHoiChanToanLichFilter == null)
            OBienBanHoiChanToanLichFilter = new BienBanHoiChanToanLichFilterCls();
        var skip = PageIndex * PageSize;
        string Query = " select * from BIENBANHOICHANTOANLICH OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        BienBanHoiChanToanLichCls[] BienBanHoiChanToanLichs = BienBanHoiChanToanLichParser.ParseFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return BienBanHoiChanToanLichs;
    }
    catch (Exception ex)
    {
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Rollback();
            ActionSqlParam.Trans = null;
        }
        throw (ex);
    }
}
    public override void Add(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
                DBService.Insert(ActionSqlParam.Trans, "BienBanHoiChanToanLich",
                    new DbParam[]{
                    new DbParam("LICHHOICHANID",OBienBanHoiChanToanLich.LICHHOICHANID),
                    new DbParam("BATDAU",OBienBanHoiChanToanLich.BATDAU),
                    new DbParam("KETTHUC",OBienBanHoiChanToanLich.KETTHUC),
                    new DbParam("DIADIEM",OBienBanHoiChanToanLich.DIADIEM),
                    new DbParam("CHUTRI",OBienBanHoiChanToanLich.CHUTRI),
                    new DbParam("THUKY",OBienBanHoiChanToanLich.THUKY),
                    new DbParam("YKIENNOIDUNGHOICHAN",OBienBanHoiChanToanLich.YKIENNOIDUNGHOICHAN),
                    new DbParam("YKIENCACHTHUCTRINHBAY",OBienBanHoiChanToanLich.YKIENCACHTHUCTRINHBAY),
                    new DbParam("YKIENTHAIDOTHANHVIEN",OBienBanHoiChanToanLich.YKIENTHAIDOTHANHVIEN),
                    new DbParam("YKIENCUABVVETINH",OBienBanHoiChanToanLich.YKIENCUABVVETINH),
                    new DbParam("TAOBOI",OBienBanHoiChanToanLich.TAOBOI),
                    new DbParam("TAOVAO",OBienBanHoiChanToanLich.TAOVAO),
                    new DbParam("SUABOI",OBienBanHoiChanToanLich.SUABOI),
                    new DbParam("SUAVAO",OBienBanHoiChanToanLich.SUAVAO),
                    new DbParam("TRANGTHAI",OBienBanHoiChanToanLich.TRANGTHAI)
                });
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override void Save(ActionSqlParamCls ActionSqlParam, string LICHHOICHANID, BienBanHoiChanToanLichCls OBienBanHoiChanToanLich)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            DBService.Update(ActionSqlParam.Trans, "BIENBANHOICHANTOANLICH", "LICHHOICHANID", LICHHOICHANID,
               new DbParam[]{
               new DbParam("BATDAU",OBienBanHoiChanToanLich.BATDAU),
               new DbParam("KETTHUC",OBienBanHoiChanToanLich.KETTHUC),
               new DbParam("DIADIEM",OBienBanHoiChanToanLich.DIADIEM),
               new DbParam("CHUTRI",OBienBanHoiChanToanLich.CHUTRI),
               new DbParam("THUKY",OBienBanHoiChanToanLich.THUKY),
               new DbParam("YKIENNOIDUNGHOICHAN",OBienBanHoiChanToanLich.YKIENNOIDUNGHOICHAN),
               new DbParam("YKIENCACHTHUCTRINHBAY",OBienBanHoiChanToanLich.YKIENCACHTHUCTRINHBAY),
               new DbParam("YKIENTHAIDOTHANHVIEN",OBienBanHoiChanToanLich.YKIENTHAIDOTHANHVIEN),
               new DbParam("YKIENCUABVVETINH",OBienBanHoiChanToanLich.YKIENCUABVVETINH),
               new DbParam("TAOBOI",OBienBanHoiChanToanLich.TAOBOI),
               new DbParam("TAOVAO",OBienBanHoiChanToanLich.TAOVAO),
               new DbParam("SUABOI",OBienBanHoiChanToanLich.SUABOI),
               new DbParam("SUAVAO",OBienBanHoiChanToanLich.SUAVAO),
               new DbParam("TRANGTHAI",OBienBanHoiChanToanLich.TRANGTHAI)
                });
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override void Delete(ActionSqlParamCls ActionSqlParam, string LICHHOICHANID)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            string DelQuery = " Delete from BIENBANHOICHANTOANLICH where LICHHOICHANID=" + ActionSqlParam.SpecialChar+ "LICHHOICHANID";
            DBService.ExecuteNonQuery(ActionSqlParam.Trans, DelQuery, 
                   new DbParam[]
                {
                   new DbParam("LICHHOICHANID", LICHHOICHANID)
                });
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }
    public override BienBanHoiChanToanLichCls CreateModel(ActionSqlParamCls ActionSqlParam, string LICHHOICHANID)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from BIENBANHOICHANTOANLICH where (LICHHOICHANID=" + ActionSqlParam.SpecialChar+ "LICHHOICHANID)  ", new DbParam[]{
                    new DbParam("LICHHOICHANID",LICHHOICHANID)
                });
            BienBanHoiChanToanLichCls OBienBanHoiChanToanLich = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OBienBanHoiChanToanLich = BienBanHoiChanToanLichParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OBienBanHoiChanToanLich;
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
    }

    public override string Duplicate(ActionSqlParamCls ActionSqlParam, string LICHHOICHANID)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;

        string NewID = System.Guid.NewGuid().ToString();
        if (!HasTrans)
        {
            ActionSqlParam.Trans = DBService.BeginTransaction();
        }

        try
        {
            BienBanHoiChanToanLichCls OBienBanHoiChanToanLich = CreateModel(ActionSqlParam, LICHHOICHANID);
            OBienBanHoiChanToanLich.LICHHOICHANID = NewID;
            Add(ActionSqlParam, OBienBanHoiChanToanLich);

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return NewID;
        }
        catch (Exception ex)
        {
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Rollback();
                ActionSqlParam.Trans = null;
            }
            throw (ex);
        }
        }
        public override int GetSoBuoiHoiChanThamGia(ActionSqlParamCls ActionSqlParam, string donViThamVanMa, string donViTuVanMa, DateTime tuNgay, DateTime denNgay)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                string Query = " select count(*) from BIENBANHOICHANTOANLICH bbhctl inner join BACSY bs on bbhctl.THUKY = bs.ID " +
                                "where bbhctl.BATDAU >= " + ActionSqlParam.SpecialChar + "TUNGAY " +
                                "and bbhctl.BATDAU < " + ActionSqlParam.SpecialChar + "DENNGAY " +
                                "and bs.DONVIMA = " + ActionSqlParam.SpecialChar + "DONVITUVANMA " +
                                "and bbhctl.LICHHOICHANID in (select LICHHOICHANID from BIENBANHOICHANTOANLICHBACSY bbhctlbs where bbhctlbs.DONVICONGTACMA = " + ActionSqlParam.SpecialChar + "DONVITHAMVANMA and (VANGMAT <> 1 or VANGMAT is null) and (ISCHUYENGIA <> 1 or ISCHUYENGIA is null))";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("TUNGAY", tuNgay),
                    new DbParam("DENNGAY", denNgay),
                    new DbParam("DONVITUVANMA", donViTuVanMa),
                    new DbParam("DONVITHAMVANMA", donViThamVanMa)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                int count = (int)BienBanHoiChanToanLichParser.CountFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return count;
            }
            catch (Exception ex)
            {
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Rollback();
                    ActionSqlParam.Trans = null;
                }
                throw (ex);
            }
        }
        public override string[] GetBenhVienThamGiaHoiChan(ActionSqlParamCls ActionSqlParam, string donViTuVanMa, DateTime tuNgay, DateTime denNgay)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                string Query = " select distinct DONVICONGTACMA " +
                                "from BienBanHoiChanToanLichBacSy bbhctlbs inner join BIENBANHOICHANTOANLICH bbhctl on bbhctlbs.LICHHOICHANID = bbhctl.LICHHOICHANID " +
                                "inner join BACSY bs on bbhctl.THUKY = bs.ID " +
                                "where bbhctl.BATDAU >= " + ActionSqlParam.SpecialChar + "TUNGAY " +
                                "and bbhctl.BATDAU < " + ActionSqlParam.SpecialChar + "DENNGAY " +
                                "and bs.DONVIMA = " + ActionSqlParam.SpecialChar + "DONVITUVANMA " +
                                "and bbhctlbs.DONVICONGTACMA is not null and (bbhctlbs.VANGMAT <> 1 or bbhctlbs.VANGMAT is null) and (bbhctlbs.ISCHUYENGIA <> 1 or bbhctlbs.ISCHUYENGIA is null)";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("TUNGAY", tuNgay),
                    new DbParam("DENNGAY", denNgay),
                    new DbParam("DONVITUVANMA", donViTuVanMa)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                string[] sResult = new string[dsResult.Tables[0].Rows.Count];
                for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    sResult[i] = dsResult.Tables[0].Rows[i][0].ToString();
                }
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return sResult;
            }
            catch (Exception ex)
            {
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Rollback();
                    ActionSqlParam.Trans = null;
                }
                throw (ex);
            }
        }
    }
}
