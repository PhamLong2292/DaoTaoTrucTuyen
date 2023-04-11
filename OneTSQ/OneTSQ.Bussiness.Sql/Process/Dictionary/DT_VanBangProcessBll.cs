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
public class DT_VanBangProcessBll : DT_VanBangTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlDT_VanBangProcessBll";
        }
    }
    public override DT_VanBangCls[] Reading(
        ActionSqlParamCls ActionSqlParam,
        DT_VanBangFilterCls ODT_VanBangFilter)
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
            if (ODT_VanBangFilter == null)
            {
                ODT_VanBangFilter = new DT_VanBangFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select * from DT_VanBang where 1=1 ";
            if (!string.IsNullOrEmpty(ODT_VanBangFilter.Keyword))
            {
                ColDbParams.Add(new DbParam("Keyword", "%" + ODT_VanBangFilter.Keyword + "%"));
                Query += " and TEN like " + ActionSqlParam.SpecialChar + "Keyword";
            }
            if (!string.IsNullOrEmpty(ODT_VanBangFilter.HOCVIEN_ID))
            {
                ColDbParams.Add(new DbParam("HOCVIEN_ID", ODT_VanBangFilter.HOCVIEN_ID));
                Query += " and HOCVIEN_ID = " + ActionSqlParam.SpecialChar + "HOCVIEN_ID";
            }
            Query += " order by NAM";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            DT_VanBangCls[] DT_VanBangs = DT_VanBangParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return DT_VanBangs;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, DT_VanBangCls ODT_VanBang)
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
            if (string.IsNullOrEmpty(ODT_VanBang.ID))
            {
                ODT_VanBang.ID = System.Guid.NewGuid().ToString();
            }
                DBService.Insert(ActionSqlParam.Trans, "DT_VanBang",
                    new DbParam[]{
                    new DbParam("ID",ODT_VanBang.ID),
                    new DbParam("HOCVIEN_ID",ODT_VanBang.HOCVIEN_ID),
                    new DbParam("TEN",ODT_VanBang.TEN),
                    new DbParam("DONVICAP",ODT_VanBang.DONVICAP),
                    new DbParam("NAM",ODT_VanBang.NAM)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_VanBangCls ODT_VanBang)
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
            DBService.Update(ActionSqlParam.Trans, "DT_VanBang", "ID", ID,
                new DbParam[]{
                   new DbParam("HOCVIEN_ID",ODT_VanBang.HOCVIEN_ID),
                   new DbParam("TEN",ODT_VanBang.TEN),
                   new DbParam("DONVICAP",ODT_VanBang.DONVICAP),
                   new DbParam("NAM",ODT_VanBang.NAM)
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
            string DelQuery = " Delete from DT_VanBang where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override DT_VanBangCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_VanBang where ID="+ActionSqlParam.SpecialChar+"ID ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            DT_VanBangCls ODT_VanBang = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                ODT_VanBang = DT_VanBangParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return ODT_VanBang;
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
            DT_VanBangCls ODT_VanBang = CreateModel(ActionSqlParam, ID);
            ODT_VanBang.ID = NewID;
            Add(ActionSqlParam, ODT_VanBang);

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
