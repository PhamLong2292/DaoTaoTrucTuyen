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
public class TaiLieuProcessBll : TaiLieuTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlTaiLieuProcessBll";
        }
    }
    public override TaiLieuCls[] Reading(
        ActionSqlParamCls ActionSqlParam,
        TaiLieuFilterCls OTaiLieuFilter)
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
            if (OTaiLieuFilter == null)
            {
                OTaiLieuFilter = new TaiLieuFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select * from TAILIEU where 1=1 ";
            //if (!string.IsNullOrEmpty(OTaiLieuFilter.Keyword))
            //{
            //    ColDbParams.Add(new DbParam("Keyword", "%" + OTaiLieuFilter.Keyword + "%"));
            //    Query += " and TaiLieuName like " + ActionSqlParam.SpecialChar + "Keyword";
            //}
            Query += " order by TEN";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            TaiLieuCls[] TaiLieus = TaiLieuParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return TaiLieus;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, TaiLieuCls OTaiLieu)
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
            if (string.IsNullOrEmpty(OTaiLieu.ID))
            {
                OTaiLieu.ID = System.Guid.NewGuid().ToString();
            }
            DBService.Insert(ActionSqlParam.Trans, "TAILIEU",
                new DbParam[]{
                new DbParam("ID",OTaiLieu.ID),
                new DbParam("CABENHID",OTaiLieu.CABENHID),
                new DbParam("TEN",OTaiLieu.TEN)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, TaiLieuCls OTaiLieu)
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
            DBService.Update(ActionSqlParam.Trans, "TAILIEU", "ID", ID,
                new DbParam[]{
                   new DbParam("CABENHID",OTaiLieu.CABENHID),
                   new DbParam("TEN",OTaiLieu.TEN)
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
            string DelQuery = " Delete from TAILIEU where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override TaiLieuCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from TAILIEU where ID="+ActionSqlParam.SpecialChar+"ID ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            TaiLieuCls OTaiLieu = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OTaiLieu = TaiLieuParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OTaiLieu;
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
            TaiLieuCls OTaiLieu = CreateModel(ActionSqlParam, ID);
            OTaiLieu.ID = NewID;
            Add(ActionSqlParam, OTaiLieu);

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
