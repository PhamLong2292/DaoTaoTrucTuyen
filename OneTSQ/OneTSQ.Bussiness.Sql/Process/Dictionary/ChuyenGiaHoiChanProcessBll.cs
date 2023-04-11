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
public class ChuyenGiaHoiChanProcessBll : ChuyenGiaHoiChanTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlChuyenGiaHoiChanProcessBll";
        }
    }
    public override ChuyenGiaHoiChanCls[] Reading(
        ActionSqlParamCls ActionSqlParam,
        ChuyenGiaHoiChanFilterCls OChuyenGiaHoiChanFilter)
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
            if (OChuyenGiaHoiChanFilter == null)
            {
                OChuyenGiaHoiChanFilter = new ChuyenGiaHoiChanFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select * from ChuyenGiaHoiChan where 1=1 ";
            if (!string.IsNullOrEmpty(OChuyenGiaHoiChanFilter.BIENBANHOICHANID))
            {
                ColDbParams.Add(new DbParam("BIENBANHOICHANID", OChuyenGiaHoiChanFilter.BIENBANHOICHANID));
                Query += " and BIENBANHOICHANID = " + ActionSqlParam.SpecialChar + "BIENBANHOICHANID";
            }
            //Query += " order by BACSYID";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            ChuyenGiaHoiChanCls[] ChuyenGiaHoiChans = ChuyenGiaHoiChanParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return ChuyenGiaHoiChans;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, ChuyenGiaHoiChanCls OChuyenGiaHoiChan)
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
            if (string.IsNullOrEmpty(OChuyenGiaHoiChan.ID))
            {
                OChuyenGiaHoiChan.ID = System.Guid.NewGuid().ToString();
            }
                DBService.Insert(ActionSqlParam.Trans, "ChuyenGiaHoiChan",
                    new DbParam[]{
                    new DbParam("ID",OChuyenGiaHoiChan.ID),
                    new DbParam("BIENBANHOICHANID",OChuyenGiaHoiChan.BIENBANHOICHANID),
                    new DbParam("BACSYID",OChuyenGiaHoiChan.BACSYID),
                    new DbParam("DONVICONGTAC",OChuyenGiaHoiChan.DONVICONGTAC)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, ChuyenGiaHoiChanCls OChuyenGiaHoiChan)
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
            DBService.Update(ActionSqlParam.Trans, "ChuyenGiaHoiChan", "ID", ID,
                new DbParam[]{
                   new DbParam("BIENBANHOICHANID",OChuyenGiaHoiChan.BIENBANHOICHANID),
                   new DbParam("BACSYID",OChuyenGiaHoiChan.BACSYID),
                   new DbParam("DONVICONGTAC",OChuyenGiaHoiChan.DONVICONGTAC)
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
            string DelQuery = " Delete from ChuyenGiaHoiChan where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override ChuyenGiaHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from ChuyenGiaHoiChan where ID="+ActionSqlParam.SpecialChar+"ID  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            ChuyenGiaHoiChanCls OChuyenGiaHoiChan = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OChuyenGiaHoiChan = ChuyenGiaHoiChanParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OChuyenGiaHoiChan;
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
            ChuyenGiaHoiChanCls OChuyenGiaHoiChan = CreateModel(ActionSqlParam, ID);
            OChuyenGiaHoiChan.ID = NewID;
            Add(ActionSqlParam, OChuyenGiaHoiChan);

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
