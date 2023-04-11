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
public class DanhGiaThoiGianBuoiBaoCaoProcessBll : DanhGiaThoiGianBuoiBaoCaoTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlDanhGiaThoiGianBuoiBaoCaoProcessBll";
        }
    }
    public override DanhGiaThoiGianBuoiBaoCaoCls[] Reading(ActionSqlParamCls ActionSqlParam,DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter)
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
            if (ODanhGiaThoiGianBuoiBaoCaoFilter == null)
            {
                ODanhGiaThoiGianBuoiBaoCaoFilter = new DanhGiaThoiGianBuoiBaoCaoFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from DanhGiaThoiGianBuoiBaoCao where 1=1 ";
            if (!string.IsNullOrEmpty(ODanhGiaThoiGianBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
            {
                ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", ODanhGiaThoiGianBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
                Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
            }
            Query += "ORDER BY STT ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            DanhGiaThoiGianBuoiBaoCaoCls[] DanhGiaThoiGianBuoiBaoCaos = DanhGiaThoiGianBuoiBaoCaoParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return DanhGiaThoiGianBuoiBaoCaos;
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
    public override long Count( ActionSqlParamCls ActionSqlParam,DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
         if (ODanhGiaThoiGianBuoiBaoCaoFilter == null)
             ODanhGiaThoiGianBuoiBaoCaoFilter = new DanhGiaThoiGianBuoiBaoCaoFilterCls();
         Collection<DbParam> ColDbParams = new Collection<DbParam>();
         string Query = " select COUNT (*) from DanhGiaThoiGianBuoiBaoCao where 1=1 ";
         if (!string.IsNullOrEmpty(ODanhGiaThoiGianBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
         {
            ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", ODanhGiaThoiGianBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
            Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
         }
         DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = DanhGiaThoiGianBuoiBaoCaoParser.CountFromDataTable(dsResult.Tables[0]);
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
public override DanhGiaThoiGianBuoiBaoCaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoFilterCls ODanhGiaThoiGianBuoiBaoCaoFilter, int PageIndex, int PageSize)
{
    IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
    bool HasTrans = ActionSqlParam.Trans != null;
    bool HasCommit = false;
    if (!HasTrans)
        ActionSqlParam.Trans = DBService.BeginTransaction();
    try
    {
        if (ODanhGiaThoiGianBuoiBaoCaoFilter == null)
            ODanhGiaThoiGianBuoiBaoCaoFilter = new DanhGiaThoiGianBuoiBaoCaoFilterCls();
        var skip = PageIndex * PageSize;
        string Query = " select * from DanhGiaThoiGianBuoiBaoCao OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        DanhGiaThoiGianBuoiBaoCaoCls[] DanhGiaThoiGianBuoiBaoCaos = DanhGiaThoiGianBuoiBaoCaoParser.ParseFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return DanhGiaThoiGianBuoiBaoCaos;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao)
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
                DBService.Insert(ActionSqlParam.Trans, "DanhGiaThoiGianBuoiBaoCao",
                    new DbParam[]{
                    new DbParam("ID",ODanhGiaThoiGianBuoiBaoCao.ID),
                    new DbParam("TIEUCHITHOIGIANDAOTAOTTMA",ODanhGiaThoiGianBuoiBaoCao.TIEUCHITHOIGIANDAOTAOTTMA),
                    new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",ODanhGiaThoiGianBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
                    new DbParam("DANHGIA",ODanhGiaThoiGianBuoiBaoCao.DANHGIA),
                    new DbParam("STT",ODanhGiaThoiGianBuoiBaoCao.STT)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao)
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
            DBService.Update(ActionSqlParam.Trans, "DanhGiaThoiGianBuoiBaoCao", "ID", ID,
               new DbParam[]{
               new DbParam("TIEUCHITHOIGIANDAOTAOTTMA",ODanhGiaThoiGianBuoiBaoCao.TIEUCHITHOIGIANDAOTAOTTMA),
               new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",ODanhGiaThoiGianBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
               new DbParam("DANHGIA",ODanhGiaThoiGianBuoiBaoCao.DANHGIA),
               new DbParam("STT",ODanhGiaThoiGianBuoiBaoCao.STT)
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
            string DelQuery = " Delete from DanhGiaThoiGianBuoiBaoCao where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override DanhGiaThoiGianBuoiBaoCaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from DanhGiaThoiGianBuoiBaoCao where (ID="+ActionSqlParam.SpecialChar+"ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                ODanhGiaThoiGianBuoiBaoCao = DanhGiaThoiGianBuoiBaoCaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return ODanhGiaThoiGianBuoiBaoCao;
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
            DanhGiaThoiGianBuoiBaoCaoCls ODanhGiaThoiGianBuoiBaoCao = CreateModel(ActionSqlParam, ID);
            ODanhGiaThoiGianBuoiBaoCao.ID = NewID;
            Add(ActionSqlParam, ODanhGiaThoiGianBuoiBaoCao);

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
