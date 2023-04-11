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
public class NoiDungHoiChanProcessBll : NoiDungHoiChanTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlNoiDungHoiChanProcessBll";
        }
    }
    public override NoiDungHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam,NoiDungHoiChanFilterCls ONoiDungHoiChanFilter)
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
            if (ONoiDungHoiChanFilter == null)
            {
                ONoiDungHoiChanFilter = new NoiDungHoiChanFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from NOIDUNGHOICHAN where 1=1 ";
            if (!string.IsNullOrEmpty(ONoiDungHoiChanFilter.lichHoiChanId))
            {
                ColDbParams.Add(new DbParam("LICHHOICHANID", ONoiDungHoiChanFilter.lichHoiChanId));
                Query += " and LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID ";
            }
            Query += " order by STT ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            NoiDungHoiChanCls[] NoiDungHoiChans = NoiDungHoiChanParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return NoiDungHoiChans;
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
    public override long Count( ActionSqlParamCls ActionSqlParam,NoiDungHoiChanFilterCls ONoiDungHoiChanFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
           if (ONoiDungHoiChanFilter == null)
               ONoiDungHoiChanFilter = new NoiDungHoiChanFilterCls();
           string Query = " select COUNT (*) from NOIDUNGHOICHAN";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = NoiDungHoiChanParser.CountFromDataTable(dsResult.Tables[0]);
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
public override NoiDungHoiChanCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanFilterCls ONoiDungHoiChanFilter, int PageIndex, int PageSize)
{
    IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
    bool HasTrans = ActionSqlParam.Trans != null;
    bool HasCommit = false;
    if (!HasTrans)
        ActionSqlParam.Trans = DBService.BeginTransaction();
    try
    {
        if (ONoiDungHoiChanFilter == null)
            ONoiDungHoiChanFilter = new NoiDungHoiChanFilterCls();
        var skip = PageIndex * PageSize;
        string Query = " select * from NOIDUNGHOICHAN OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        NoiDungHoiChanCls[] NoiDungHoiChans = NoiDungHoiChanParser.ParseFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return NoiDungHoiChans;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, NoiDungHoiChanCls ONoiDungHoiChan)
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
                DBService.Insert(ActionSqlParam.Trans, "NoiDungHoiChan",
                    new DbParam[]{
                    new DbParam("ID",ONoiDungHoiChan.ID),
                    new DbParam("CABENHID",ONoiDungHoiChan.CABENHID),
                    new DbParam("LICHHOICHANID",ONoiDungHoiChan.LICHHOICHANID),
                    new DbParam("CHANDOAN",ONoiDungHoiChan.CHANDOAN),
                    new DbParam("HUONGXUTRI",ONoiDungHoiChan.HUONGXUTRI),
                    new DbParam("STT",ONoiDungHoiChan.STT)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, NoiDungHoiChanCls ONoiDungHoiChan)
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
            DBService.Update(ActionSqlParam.Trans, "NOIDUNGHOICHAN", "ID", ID,
               new DbParam[]{
               new DbParam("CABENHID",ONoiDungHoiChan.CABENHID),
               new DbParam("LICHHOICHANID",ONoiDungHoiChan.LICHHOICHANID),
               new DbParam("CHANDOAN",ONoiDungHoiChan.CHANDOAN),
               new DbParam("HUONGXUTRI",ONoiDungHoiChan.HUONGXUTRI),
               new DbParam("STT",ONoiDungHoiChan.STT)
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
            string DelQuery = " Delete from NOIDUNGHOICHAN where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override NoiDungHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from NOIDUNGHOICHAN where (ID="+ActionSqlParam.SpecialChar+"ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            NoiDungHoiChanCls ONoiDungHoiChan = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                ONoiDungHoiChan = NoiDungHoiChanParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return ONoiDungHoiChan;
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
            NoiDungHoiChanCls ONoiDungHoiChan = CreateModel(ActionSqlParam, ID);
            ONoiDungHoiChan.ID = NewID;
            Add(ActionSqlParam, ONoiDungHoiChan);

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
