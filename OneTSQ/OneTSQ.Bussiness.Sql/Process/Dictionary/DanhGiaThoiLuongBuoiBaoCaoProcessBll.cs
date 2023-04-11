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
public class DanhGiaThoiLuongBuoiBaoCaoProcessBll : DanhGiaThoiLuongBuoiBaoCaoTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlDanhGiaThoiLuongBuoiBaoCaoProcessBll";
        }
    }
    public override DanhGiaThoiLuongBuoiBaoCaoCls[] Reading(ActionSqlParamCls ActionSqlParam,DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter)
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
            if (ODanhGiaThoiLuongBuoiBaoCaoFilter == null)
            {
                ODanhGiaThoiLuongBuoiBaoCaoFilter = new DanhGiaThoiLuongBuoiBaoCaoFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from DanhGiaThoiLuongBuoiBaoCao where 1=1 ";
            if (!string.IsNullOrEmpty(ODanhGiaThoiLuongBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
            {
                ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", ODanhGiaThoiLuongBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
                Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
            }
            Query += "ORDER BY STT ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            DanhGiaThoiLuongBuoiBaoCaoCls[] DanhGiaThoiLuongBuoiBaoCaos = DanhGiaThoiLuongBuoiBaoCaoParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return DanhGiaThoiLuongBuoiBaoCaos;
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
    public override long Count( ActionSqlParamCls ActionSqlParam,DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
         if (ODanhGiaThoiLuongBuoiBaoCaoFilter == null)
             ODanhGiaThoiLuongBuoiBaoCaoFilter = new DanhGiaThoiLuongBuoiBaoCaoFilterCls();
         Collection<DbParam> ColDbParams = new Collection<DbParam>();
         string Query = " select COUNT (*) from DanhGiaThoiLuongBuoiBaoCao where 1=1 ";
         if (!string.IsNullOrEmpty(ODanhGiaThoiLuongBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
         {
            ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", ODanhGiaThoiLuongBuoiBaoCaoFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
            Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
         }
         DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = DanhGiaThoiLuongBuoiBaoCaoParser.CountFromDataTable(dsResult.Tables[0]);
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
public override DanhGiaThoiLuongBuoiBaoCaoCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DanhGiaThoiLuongBuoiBaoCaoFilterCls ODanhGiaThoiLuongBuoiBaoCaoFilter, int PageIndex, int PageSize)
{
    IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
    bool HasTrans = ActionSqlParam.Trans != null;
    bool HasCommit = false;
    if (!HasTrans)
        ActionSqlParam.Trans = DBService.BeginTransaction();
    try
    {
        if (ODanhGiaThoiLuongBuoiBaoCaoFilter == null)
            ODanhGiaThoiLuongBuoiBaoCaoFilter = new DanhGiaThoiLuongBuoiBaoCaoFilterCls();
        var skip = PageIndex * PageSize;
        string Query = " select * from DanhGiaThoiLuongBuoiBaoCao OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        DanhGiaThoiLuongBuoiBaoCaoCls[] DanhGiaThoiLuongBuoiBaoCaos = DanhGiaThoiLuongBuoiBaoCaoParser.ParseFromDataTable(dsResult.Tables[0]);
        dsResult.Clear();
        dsResult.Dispose();
        if (!HasTrans && !HasCommit)
        {
            ActionSqlParam.Trans.Commit();
            ActionSqlParam.Trans = null;
            HasCommit = true;
        }
        return DanhGiaThoiLuongBuoiBaoCaos;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao)
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
                DBService.Insert(ActionSqlParam.Trans, "DanhGiaThoiLuongBuoiBaoCao",
                    new DbParam[]{
                    new DbParam("ID",ODanhGiaThoiLuongBuoiBaoCao.ID),
                    new DbParam("TIEUCHITHOILUONGDAOTAOTTMA",ODanhGiaThoiLuongBuoiBaoCao.TIEUCHITHOILUONGDAOTAOTTMA),
                    new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",ODanhGiaThoiLuongBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
                    new DbParam("DANHGIA",ODanhGiaThoiLuongBuoiBaoCao.DANHGIA),
                    new DbParam("LYDOTHUA",ODanhGiaThoiLuongBuoiBaoCao.LYDOTHUA),
                    new DbParam("LYDOTHIEU",ODanhGiaThoiLuongBuoiBaoCao.LYDOTHIEU),
                    new DbParam("STT",ODanhGiaThoiLuongBuoiBaoCao.STT)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao)
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
            DBService.Update(ActionSqlParam.Trans, "DanhGiaThoiLuongBuoiBaoCao", "ID", ID,
               new DbParam[]{
               new DbParam("TIEUCHITHOILUONGDAOTAOTTMA",ODanhGiaThoiLuongBuoiBaoCao.TIEUCHITHOILUONGDAOTAOTTMA),
               new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",ODanhGiaThoiLuongBuoiBaoCao.PHIEUDANHGIACHATLUONGDAOTAO_ID),
               new DbParam("DANHGIA",ODanhGiaThoiLuongBuoiBaoCao.DANHGIA),
               new DbParam("LYDOTHUA",ODanhGiaThoiLuongBuoiBaoCao.LYDOTHUA),
               new DbParam("LYDOTHIEU",ODanhGiaThoiLuongBuoiBaoCao.LYDOTHIEU),
               new DbParam("STT",ODanhGiaThoiLuongBuoiBaoCao.STT)
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
            string DelQuery = " Delete from DanhGiaThoiLuongBuoiBaoCao where ID="+ActionSqlParam.SpecialChar+"ID";
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
    public override DanhGiaThoiLuongBuoiBaoCaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from DanhGiaThoiLuongBuoiBaoCao where (ID="+ActionSqlParam.SpecialChar+"ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                ODanhGiaThoiLuongBuoiBaoCao = DanhGiaThoiLuongBuoiBaoCaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return ODanhGiaThoiLuongBuoiBaoCao;
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
            DanhGiaThoiLuongBuoiBaoCaoCls ODanhGiaThoiLuongBuoiBaoCao = CreateModel(ActionSqlParam, ID);
            ODanhGiaThoiLuongBuoiBaoCao.ID = NewID;
            Add(ActionSqlParam, ODanhGiaThoiLuongBuoiBaoCao);

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
