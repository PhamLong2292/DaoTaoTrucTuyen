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
public class TepDinhKemBlHinhAnhProcessBll : TepDinhKemBlHinhAnhTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlTepDinhKemBlHinhAnhProcessBll";
        }
    }
    public override TepDinhKemBlHinhAnhCls[] Reading(
        ActionSqlParamCls ActionSqlParam,
        TepDinhKemBlHinhAnhFilterCls OTepDinhKemBlHinhAnhFilter)
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
            if (OTepDinhKemBlHinhAnhFilter == null)
            {
                OTepDinhKemBlHinhAnhFilter = new TepDinhKemBlHinhAnhFilterCls();
            }
            Collection<DbParam>
                ColDbParams = new Collection<DbParam>();
            string Query = "";

            Query = " select * from TEPDINHKEMBLHINHANH where 1=1 ";
            if (!string.IsNullOrEmpty(OTepDinhKemBlHinhAnhFilter.BINHLUANHINHANHID))
            {
                ColDbParams.Add(new DbParam("BINHLUANHINHANHID", OTepDinhKemBlHinhAnhFilter.BINHLUANHINHANHID));
                Query += " and BINHLUANHINHANHID = " + ActionSqlParam.SpecialChar + "BINHLUANHINHANHID";
            }
            Query += " order by TENHIENTHI";

            DataSet dsResult =
                    DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            TepDinhKemBlHinhAnhCls[] TepDinhKemBlHinhAnhs = TepDinhKemBlHinhAnhParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return TepDinhKemBlHinhAnhs;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh)
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
            if (string.IsNullOrEmpty(OTepDinhKemBlHinhAnh.ID))
            {
                OTepDinhKemBlHinhAnh.ID = System.Guid.NewGuid().ToString();
            }
                DBService.Insert(ActionSqlParam.Trans, "TEPDINHKEMBLHINHANH",
                    new DbParam[]{
                    new DbParam("ID",OTepDinhKemBlHinhAnh.ID),
                    new DbParam("BINHLUANHINHANHID",OTepDinhKemBlHinhAnh.BINHLUANHINHANHID),
                    new DbParam("TENTEP",OTepDinhKemBlHinhAnh.TENTEP),
                    new DbParam("TENHIENTHI",OTepDinhKemBlHinhAnh.TENHIENTHI)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh)
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
            DBService.Update(ActionSqlParam.Trans, "TEPDINHKEMBLHINHANH", "ID", ID,
                new DbParam[]{
                   new DbParam("BINHLUANHINHANHID",OTepDinhKemBlHinhAnh.BINHLUANHINHANHID),
                   new DbParam("TENTEP",OTepDinhKemBlHinhAnh.TENTEP),
                   new DbParam("TENHIENTHI",OTepDinhKemBlHinhAnh.TENHIENTHI)
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
            string DelQuery = " Delete from TEPDINHKEMBLHINHANH where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override TepDinhKemBlHinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                    DBService.GetDataSet(ActionSqlParam.Trans, "select * from TEPDINHKEMBLHINHANH where ID="+ActionSqlParam.SpecialChar+"ID ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OTepDinhKemBlHinhAnh = TepDinhKemBlHinhAnhParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OTepDinhKemBlHinhAnh;
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
            TepDinhKemBlHinhAnhCls OTepDinhKemBlHinhAnh = CreateModel(ActionSqlParam, ID);
            OTepDinhKemBlHinhAnh.ID = NewID;
            Add(ActionSqlParam, OTepDinhKemBlHinhAnh);

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
