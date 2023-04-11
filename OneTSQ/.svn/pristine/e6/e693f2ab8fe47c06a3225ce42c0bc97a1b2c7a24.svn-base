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
public class MucDoPhongPhuBaiBaoCaoProcessBll : MucDoPhongPhuBaiBaoCaoTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlMucDoPhongPhuBaiBaoCaoProcessBll";
        }
    }
    public override MucDoPhongPhuBaiBaoCaoCls[] Reading(ActionSqlParamCls ActionSqlParam,MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter)
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
            if (OMucDoPhongPhuBaiBaoCaoFilter == null)
            {
                OMucDoPhongPhuBaiBaoCaoFilter = new MucDoPhongPhuBaiBaoCaoFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from MucDoPhongPhuBaiBaoCao where 1=1 ";
            if (!string.IsNullOrEmpty(OMucDoPhongPhuBaiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
            {
                ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", OMucDoPhongPhuBaiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
                Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
            }
            Query += "ORDER BY STT ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            MucDoPhongPhuBaiBaoCaoCls[] MucDoPhongPhuBaiBaoCaos = MucDoPhongPhuBaiBaoCaoParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return MucDoPhongPhuBaiBaoCaos;
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
    public override long Count( ActionSqlParamCls ActionSqlParam,MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
         if (OMucDoPhongPhuBaiBaoCaoFilter == null)
             OMucDoPhongPhuBaiBaoCaoFilter = new MucDoPhongPhuBaiBaoCaoFilterCls();
         Collection<DbParam> ColDbParams = new Collection<DbParam>();
         string Query = " select COUNT (*) from MucDoPhongPhuBaiBaoCao where 1=1 ";
         if (!string.IsNullOrEmpty(OMucDoPhongPhuBaiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
         {
            ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", OMucDoPhongPhuBaiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
            Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
         }
         DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = MucDoPhongPhuBaiBaoCaoParser.CountFromDataTable(dsResult.Tables[0]);
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
public override MucDoPhongPhuBaiBaoCaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoFilterCls OMucDoPhongPhuBaiBaoCaoFilter, int PageIndex, int PageSize)
{
    IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
    bool HasTrans = ActionSqlParam.Trans != null;
    bool HasCommit = false;
    if (!HasTrans)
        ActionSqlParam.Trans = DBService.BeginTransaction();
    try
    {
        if (OMucDoPhongPhuBaiBaoCaoFilter == null)
            OMucDoPhongPhuBaiBaoCaoFilter = new MucDoPhongPhuBaiBaoCaoFilterCls();
        var skip = PageIndex * PageSize;
        string Query = " select * from MucDoPhongPhuBaiBaoCao OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        MucDoPhongPhuBaiBaoCaoCls[] MucDoPhongPhuBaiBaoCaos = MucDoPhongPhuBaiBaoCaoParser.ParseFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return MucDoPhongPhuBaiBaoCaos;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao)
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
                DBService.Insert(ActionSqlParam.Trans, "MucDoPhongPhuBaiBaoCao",
                    new DbParam[]{
                    new DbParam("ID",OMucDoPhongPhuBaiBaoCao.ID),
                    new DbParam("CHUYENKHOADAOTAOTTMA",OMucDoPhongPhuBaiBaoCao.CHUYENKHOADAOTAOTTMA),
                    new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",OMucDoPhongPhuBaiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
                    new DbParam("DANHGIA",OMucDoPhongPhuBaiBaoCao.DANHGIA),
                    new DbParam("STT",OMucDoPhongPhuBaiBaoCao.STT)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao)
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
            DBService.Update(ActionSqlParam.Trans, "MucDoPhongPhuBaiBaoCao", "ID", ID,
               new DbParam[]{
               new DbParam("CHUYENKHOADAOTAOTTMA",OMucDoPhongPhuBaiBaoCao.CHUYENKHOADAOTAOTTMA),
               new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",OMucDoPhongPhuBaiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
               new DbParam("DANHGIA",OMucDoPhongPhuBaiBaoCao.DANHGIA),
               new DbParam("STT",OMucDoPhongPhuBaiBaoCao.STT)
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
            string DelQuery = " Delete from MucDoPhongPhuBaiBaoCao where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override MucDoPhongPhuBaiBaoCaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from MucDoPhongPhuBaiBaoCao where (ID="+ActionSqlParam.SpecialChar+"ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OMucDoPhongPhuBaiBaoCao = MucDoPhongPhuBaiBaoCaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OMucDoPhongPhuBaiBaoCao;
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
            MucDoPhongPhuBaiBaoCaoCls OMucDoPhongPhuBaiBaoCao = CreateModel(ActionSqlParam, ID);
            OMucDoPhongPhuBaiBaoCao.ID = NewID;
            Add(ActionSqlParam, OMucDoPhongPhuBaiBaoCao);

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
