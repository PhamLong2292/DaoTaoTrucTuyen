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
public class BienBanHoiChanProcessBll : BienBanHoiChanTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlBienBanHoiChanProcessBll";
        }
    }
    public override BienBanHoiChanCls[] Reading(
        ActionSqlParamCls ActionSqlParam,
        BienBanHoiChanFilterCls OBienBanHoiChanFilter)
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
            if (OBienBanHoiChanFilter == null)
            {
                OBienBanHoiChanFilter = new BienBanHoiChanFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select * from BienBanHoiChan where 1=1 ";
            if (!string.IsNullOrEmpty(OBienBanHoiChanFilter.CABENHID))
            {
                ColDbParams.Add(new DbParam("CABENHID", OBienBanHoiChanFilter.CABENHID));
                Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
            }
            Query += " order by BATDAUHOICHANVAO desc";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            BienBanHoiChanCls[] BienBanHoiChans = BienBanHoiChanParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return BienBanHoiChans;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, BienBanHoiChanCls OBienBanHoiChan)
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
            if (string.IsNullOrEmpty(OBienBanHoiChan.ID))
            {
                OBienBanHoiChan.ID = System.Guid.NewGuid().ToString();
            }
                DBService.Insert(ActionSqlParam.Trans, "BienBanHoiChan",
                    new DbParam[]{
                    new DbParam("ID",OBienBanHoiChan.ID),
                    new DbParam("CABENHID",OBienBanHoiChan.CABENHID),
                    new DbParam("BATDAUHOICHANVAO",OBienBanHoiChan.BATDAUHOICHANVAO),
                    new DbParam("KETTHUCHOICHANVAO",OBienBanHoiChan.KETTHUCHOICHANVAO),
                    new DbParam("DIADIEMHOICHAN",OBienBanHoiChan.DIADIEMHOICHAN),
                    new DbParam("YKIENKHAMBENH",OBienBanHoiChan.YKIENKHAMBENH),
                    new DbParam("YKIENCANLAMSANG",OBienBanHoiChan.YKIENCANLAMSANG),
                    new DbParam("YKIENCHANDOAN",OBienBanHoiChan.YKIENCHANDOAN),
                    new DbParam("YKIENDIEUTRI",OBienBanHoiChan.YKIENDIEUTRI),
                    new DbParam("YKIENKHAC",OBienBanHoiChan.YKIENKHAC),
                    new DbParam("THUKY",OBienBanHoiChan.THUKY),
                    new DbParam("CHUTRIHOICHAN",OBienBanHoiChan.CHUTRIHOICHAN),
                    new DbParam("TAOBOI",OBienBanHoiChan.TAOBOI),
                    new DbParam("TAOVAO",OBienBanHoiChan.TAOVAO),
                    new DbParam("TRANGTHAI",OBienBanHoiChan.TRANGTHAI)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, BienBanHoiChanCls OBienBanHoiChan)
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
            DBService.Update(ActionSqlParam.Trans, "BienBanHoiChan", "ID", ID,
                new DbParam[]{
                   new DbParam("CABENHID",OBienBanHoiChan.CABENHID),
                   new DbParam("BATDAUHOICHANVAO",OBienBanHoiChan.BATDAUHOICHANVAO),
                   new DbParam("KETTHUCHOICHANVAO",OBienBanHoiChan.KETTHUCHOICHANVAO),
                   new DbParam("DIADIEMHOICHAN",OBienBanHoiChan.DIADIEMHOICHAN),
                   new DbParam("YKIENKHAMBENH",OBienBanHoiChan.YKIENKHAMBENH),
                   new DbParam("YKIENCANLAMSANG",OBienBanHoiChan.YKIENCANLAMSANG),
                   new DbParam("YKIENCHANDOAN",OBienBanHoiChan.YKIENCHANDOAN),
                   new DbParam("YKIENDIEUTRI",OBienBanHoiChan.YKIENDIEUTRI),
                   new DbParam("YKIENKHAC",OBienBanHoiChan.YKIENKHAC),
                   new DbParam("THUKY",OBienBanHoiChan.THUKY),
                   new DbParam("CHUTRIHOICHAN",OBienBanHoiChan.CHUTRIHOICHAN),
                   new DbParam("TAOBOI",OBienBanHoiChan.TAOBOI),
                   new DbParam("TAOVAO",OBienBanHoiChan.TAOVAO),
                   new DbParam("TRANGTHAI",OBienBanHoiChan.TRANGTHAI)
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
    public override void Delete(ActionSqlParamCls ActionSqlParam, string ID)
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
            string DelQuery = " DECLARE BEGIN " +
                              " Delete from ChuyenGiaHoiChan where BienBanHoiChanId=" + ActionSqlParam.SpecialChar + "ID; " +
                              " Delete from BienBanHoiChan where ID=" + ActionSqlParam.SpecialChar + "ID; " +
                              " END;";
            DBService.ExecuteNonQuery(ActionSqlParam.Trans, DelQuery, 
                new DbParam[]
                {
                    new DbParam("ID", ID)
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
    public override BienBanHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from BienBanHoiChan where ID="+ActionSqlParam.SpecialChar+"ID ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            BienBanHoiChanCls OBienBanHoiChan = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OBienBanHoiChan = BienBanHoiChanParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OBienBanHoiChan;
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

    public override string Duplicate(ActionSqlParamCls ActionSqlParam, string ID)
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
            BienBanHoiChanCls OBienBanHoiChan = CreateModel(ActionSqlParam, ID);
            OBienBanHoiChan.ID = NewID;
            Add(ActionSqlParam, OBienBanHoiChan);

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
}
}
