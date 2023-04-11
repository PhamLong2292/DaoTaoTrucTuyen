using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Bussiness.Template;
using OneTSQ.Database.Service;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Sql
{
public class MucDoYNghiaChuongTrinhDaoTaoProcessBll : MucDoYNghiaChuongTrinhDaoTaoTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlMucDoYNghiaChuongTrinhDaoTaoProcessBll";
        }
    }
    public override MucDoYNghiaChuongTrinhDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam,MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter)
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
            if (OMucDoYNghiaChuongTrinhDaoTaoFilter == null)
            {
                OMucDoYNghiaChuongTrinhDaoTaoFilter = new MucDoYNghiaChuongTrinhDaoTaoFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from MucDoYNghiaChuongTrinhDaoTao where 1=1 ";
            if (!string.IsNullOrEmpty(OMucDoYNghiaChuongTrinhDaoTaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
            {
                ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", OMucDoYNghiaChuongTrinhDaoTaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
                Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
            }
            Query += "ORDER BY STT ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            MucDoYNghiaChuongTrinhDaoTaoCls[] MucDoYNghiaChuongTrinhDaoTaos = MucDoYNghiaChuongTrinhDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return MucDoYNghiaChuongTrinhDaoTaos;
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
    public override long Count( ActionSqlParamCls ActionSqlParam,MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
         if (OMucDoYNghiaChuongTrinhDaoTaoFilter == null)
             OMucDoYNghiaChuongTrinhDaoTaoFilter = new MucDoYNghiaChuongTrinhDaoTaoFilterCls();
         Collection<DbParam> ColDbParams = new Collection<DbParam>();
         string Query = " select COUNT (*) from MucDoYNghiaChuongTrinhDaoTao where 1=1 ";
         if (!string.IsNullOrEmpty(OMucDoYNghiaChuongTrinhDaoTaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
         {
            ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", OMucDoYNghiaChuongTrinhDaoTaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
            Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
         }
         DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = MucDoYNghiaChuongTrinhDaoTaoParser.CountFromDataTable(dsResult.Tables[0]);
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
public override MucDoYNghiaChuongTrinhDaoTaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoFilterCls OMucDoYNghiaChuongTrinhDaoTaoFilter, int PageIndex, int PageSize)
{
    IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
    bool HasTrans = ActionSqlParam.Trans != null;
    bool HasCommit = false;
    if (!HasTrans)
        ActionSqlParam.Trans = DBService.BeginTransaction();
    try
    {
        if (OMucDoYNghiaChuongTrinhDaoTaoFilter == null)
            OMucDoYNghiaChuongTrinhDaoTaoFilter = new MucDoYNghiaChuongTrinhDaoTaoFilterCls();
        var skip = PageIndex * PageSize;
        string Query = " select * from MucDoYNghiaChuongTrinhDaoTao OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        MucDoYNghiaChuongTrinhDaoTaoCls[] MucDoYNghiaChuongTrinhDaoTaos = MucDoYNghiaChuongTrinhDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return MucDoYNghiaChuongTrinhDaoTaos;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao)
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
                DBService.Insert(ActionSqlParam.Trans, "MucDoYNghiaChuongTrinhDaoTao",
                    new DbParam[]{
                    new DbParam("ID",OMucDoYNghiaChuongTrinhDaoTao.ID),
                    new DbParam("CHUYENKHOADAOTAOTTMA",OMucDoYNghiaChuongTrinhDaoTao.CHUYENKHOADAOTAOTTMA),
                    new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",OMucDoYNghiaChuongTrinhDaoTao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
                    new DbParam("DANHGIA",OMucDoYNghiaChuongTrinhDaoTao.DANHGIA),
                    new DbParam("STT",OMucDoYNghiaChuongTrinhDaoTao.STT)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao)
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
            DBService.Update(ActionSqlParam.Trans, "MucDoYNghiaChuongTrinhDaoTao", "ID", ID,
               new DbParam[]{
               new DbParam("CHUYENKHOADAOTAOTTMA",OMucDoYNghiaChuongTrinhDaoTao.CHUYENKHOADAOTAOTTMA),
               new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",OMucDoYNghiaChuongTrinhDaoTao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
               new DbParam("DANHGIA",OMucDoYNghiaChuongTrinhDaoTao.DANHGIA),
               new DbParam("STT",OMucDoYNghiaChuongTrinhDaoTao.STT)
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
            string DelQuery = " Delete from MucDoYNghiaChuongTrinhDaoTao where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override MucDoYNghiaChuongTrinhDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from MucDoYNghiaChuongTrinhDaoTao where (ID="+ActionSqlParam.SpecialChar+"ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OMucDoYNghiaChuongTrinhDaoTao = MucDoYNghiaChuongTrinhDaoTaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OMucDoYNghiaChuongTrinhDaoTao;
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
            MucDoYNghiaChuongTrinhDaoTaoCls OMucDoYNghiaChuongTrinhDaoTao = CreateModel(ActionSqlParam, ID);
            OMucDoYNghiaChuongTrinhDaoTao.ID = NewID;
            Add(ActionSqlParam, OMucDoYNghiaChuongTrinhDaoTao);

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
