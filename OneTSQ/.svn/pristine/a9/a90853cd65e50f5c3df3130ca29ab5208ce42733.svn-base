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
public class TepTinProcessBll : TepTinTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlTepTinProcessBll";
        }
    }
    public override TepTinCls[] Reading(
        ActionSqlParamCls ActionSqlParam,
        TepTinFilterCls OTepTinFilter)
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
            if (OTepTinFilter == null)
            {
                OTepTinFilter = new TepTinFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select * from TEPTIN where 1=1 ";
            if (!string.IsNullOrEmpty(OTepTinFilter.CABENHID))
            {
                ColDbParams.Add(new DbParam("CABENHID", OTepTinFilter.CABENHID));
                Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID";
            }
            Query += " order by TENHIENTHI";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            TepTinCls[] TepTins = TepTinParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return TepTins;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, TepTinCls OTepTin)
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
            if (string.IsNullOrEmpty(OTepTin.ID))
            {
                OTepTin.ID = System.Guid.NewGuid().ToString();
            }
            DBService.Insert(ActionSqlParam.Trans, "TEPTIN",
                new DbParam[]{
                new DbParam("ID",OTepTin.ID),
                new DbParam("CABENHID",OTepTin.CABENHID),
                new DbParam("TENHIENTHI",OTepTin.TENHIENTHI),
                new DbParam("TAILIEUID",OTepTin.TAILIEUID),
                new DbParam("TENTEP",OTepTin.TENTEP),
                new DbParam("GHICHU",OTepTin.GHICHU)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, TepTinCls OTepTin)
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
            DBService.Update(ActionSqlParam.Trans, "TEPTIN", "ID", ID,
                new DbParam[]{
                   new DbParam("CABENHID",OTepTin.CABENHID),
                   new DbParam("TENHIENTHI",OTepTin.TENHIENTHI),
                   new DbParam("TAILIEUID",OTepTin.TAILIEUID),
                   new DbParam("TENTEP",OTepTin.TENTEP),
                   new DbParam("GHICHU",OTepTin.GHICHU)
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
            string DelQuery = " Delete from TEPTIN where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override TepTinCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from TEPTIN where ID="+ActionSqlParam.SpecialChar+"ID ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            TepTinCls OTepTin = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OTepTin = TepTinParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OTepTin;
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
            TepTinCls OTepTin = CreateModel(ActionSqlParam, ID);
            OTepTin.ID = NewID;
            Add(ActionSqlParam, OTepTin);

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
