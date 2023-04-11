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
public class BienBanHoiChanToanLichBacSyProcessBll : BienBanHoiChanToanLichBacSyTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlBienBanHoiChanToanLichBacSyProcessBll";
        }
    }
    public override BienBanHoiChanToanLichBacSyCls[] Reading(ActionSqlParamCls ActionSqlParam,BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter)
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
            if (OBienBanHoiChanToanLichBacSyFilter == null)
            {
                OBienBanHoiChanToanLichBacSyFilter = new BienBanHoiChanToanLichBacSyFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from BIENBANHOICHANTOANLICHBACSY where 1=1 ";
            if (!string.IsNullOrEmpty(OBienBanHoiChanToanLichBacSyFilter.lichHoiChanId))
            {
                ColDbParams.Add(new DbParam("LICHHOICHANID", OBienBanHoiChanToanLichBacSyFilter.lichHoiChanId));
                Query += " and LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID";
            }
            if (OBienBanHoiChanToanLichBacSyFilter.isChuyenGia != null)
            {
                ColDbParams.Add(new DbParam("ISCHUYENGIA", OBienBanHoiChanToanLichBacSyFilter.isChuyenGia));
                Query += " and ISCHUYENGIA = " + ActionSqlParam.SpecialChar + "ISCHUYENGIA";
            }
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            BienBanHoiChanToanLichBacSyCls[] BienBanHoiChanToanLichBacSys = BienBanHoiChanToanLichBacSyParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return BienBanHoiChanToanLichBacSys;
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
    public override long Count( ActionSqlParamCls ActionSqlParam,BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
           if (OBienBanHoiChanToanLichBacSyFilter == null)
               OBienBanHoiChanToanLichBacSyFilter = new BienBanHoiChanToanLichBacSyFilterCls();
           string Query = " select COUNT (*) from BIENBANHOICHANTOANLICHBACSY";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = BienBanHoiChanToanLichBacSyParser.CountFromDataTable(dsResult.Tables[0]);
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
    public override BienBanHoiChanToanLichBacSyCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichBacSyFilterCls OBienBanHoiChanToanLichBacSyFilter, int PageIndex, int PageSize)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
        if (!HasTrans)
            ActionSqlParam.Trans = DBService.BeginTransaction();
        try
        {
            if (OBienBanHoiChanToanLichBacSyFilter == null)
                OBienBanHoiChanToanLichBacSyFilter = new BienBanHoiChanToanLichBacSyFilterCls();
            var skip = PageIndex * PageSize;
            string Query = " select * from BIENBANHOICHANTOANLICHBACSY OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
            BienBanHoiChanToanLichBacSyCls[] BienBanHoiChanToanLichBacSys = BienBanHoiChanToanLichBacSyParser.ParseFromDataTable(dsResult.Tables[0]);
            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return BienBanHoiChanToanLichBacSys;
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
    public override void Add(ActionSqlParamCls ActionSqlParam, BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy)
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
                string query = " select ID from BIENBANHOICHANTOANLICHBACSY where LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID and BACSYID = " + ActionSqlParam.SpecialChar + "BACSYID";
                DataSet dsQuery = DBService.GetDataSet(ActionSqlParam.Trans, query,
                        new DbParam[]{
                            new DbParam("LICHHOICHANID",OBienBanHoiChanToanLichBacSy.LICHHOICHANID),
                            new DbParam("BACSYID",OBienBanHoiChanToanLichBacSy.BACSYID)
                        });
                if (dsQuery.Tables[0].Rows.Count == 0)
                {
                    if (string.IsNullOrEmpty(OBienBanHoiChanToanLichBacSy.ID))
                    {
                        OBienBanHoiChanToanLichBacSy.ID = System.Guid.NewGuid().ToString();
                    }
                    DBService.Insert(ActionSqlParam.Trans, "BienBanHoiChanToanLichBacSy",
                        new DbParam[]{
                            new DbParam("ID",OBienBanHoiChanToanLichBacSy.ID),
                            new DbParam("LICHHOICHANID",OBienBanHoiChanToanLichBacSy.LICHHOICHANID),
                            new DbParam("BACSYID",OBienBanHoiChanToanLichBacSy.BACSYID),
                            new DbParam("VANGMAT",OBienBanHoiChanToanLichBacSy.VANGMAT),
                            new DbParam("SONGUOITHAMDU",OBienBanHoiChanToanLichBacSy.SONGUOITHAMDU),
                            new DbParam("DONVICONGTACMA",OBienBanHoiChanToanLichBacSy.DONVICONGTACMA),
                            new DbParam("ISCHUYENGIA",OBienBanHoiChanToanLichBacSy.ISCHUYENGIA),
                            new DbParam("GHICHU",OBienBanHoiChanToanLichBacSy.GHICHU)
                        });
                }
                else
                {
                    DBService.Update(ActionSqlParam.Trans, "BIENBANHOICHANTOANLICHBACSY", "ID", dsQuery.Tables[0].Rows[0][0].ToString(),
                       new DbParam[]{
                           new DbParam("VANGMAT",OBienBanHoiChanToanLichBacSy.VANGMAT),
                           new DbParam("SONGUOITHAMDU",OBienBanHoiChanToanLichBacSy.SONGUOITHAMDU),
                           new DbParam("DONVICONGTACMA",OBienBanHoiChanToanLichBacSy.DONVICONGTACMA),
                           new DbParam("ISCHUYENGIA",OBienBanHoiChanToanLichBacSy.ISCHUYENGIA),
                           new DbParam("GHICHU",OBienBanHoiChanToanLichBacSy.GHICHU)
                        });
                }
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy)
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
            DBService.Update(ActionSqlParam.Trans, "BIENBANHOICHANTOANLICHBACSY", "ID", ID,
               new DbParam[]{
               new DbParam("LICHHOICHANID",OBienBanHoiChanToanLichBacSy.LICHHOICHANID),
               new DbParam("BACSYID",OBienBanHoiChanToanLichBacSy.BACSYID),
               new DbParam("VANGMAT",OBienBanHoiChanToanLichBacSy.VANGMAT),
               new DbParam("SONGUOITHAMDU",OBienBanHoiChanToanLichBacSy.SONGUOITHAMDU),
               new DbParam("DONVICONGTACMA",OBienBanHoiChanToanLichBacSy.DONVICONGTACMA),
               new DbParam("ISCHUYENGIA",OBienBanHoiChanToanLichBacSy.ISCHUYENGIA),
               new DbParam("GHICHU",OBienBanHoiChanToanLichBacSy.GHICHU)
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
            string DelQuery = " Delete from BIENBANHOICHANTOANLICHBACSY where ID=" + ActionSqlParam.SpecialChar+ "ID";
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
    public override BienBanHoiChanToanLichBacSyCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from BIENBANHOICHANTOANLICHBACSY where (ID=" + ActionSqlParam.SpecialChar+ "ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OBienBanHoiChanToanLichBacSy = BienBanHoiChanToanLichBacSyParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OBienBanHoiChanToanLichBacSy;
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
        public override BienBanHoiChanToanLichBacSyCls CreateModel(ActionSqlParamCls ActionSqlParam, string LichHoiChanId, string BacSyId)
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
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from BIENBANHOICHANTOANLICHBACSY where LICHHOICHANID=" + ActionSqlParam.SpecialChar + "LICHHOICHANID and BACSYID=" + ActionSqlParam.SpecialChar + "BACSYID ", new DbParam[]{
                    new DbParam("LICHHOICHANID",LichHoiChanId),
                    new DbParam("BACSYID",BacSyId)
                });
                BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OBienBanHoiChanToanLichBacSy = BienBanHoiChanToanLichBacSyParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OBienBanHoiChanToanLichBacSy;
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

        public override string Duplicate(ActionSqlParamCls ActionSqlParam, string LICHHOICHANID)
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
            BienBanHoiChanToanLichBacSyCls OBienBanHoiChanToanLichBacSy = CreateModel(ActionSqlParam, LICHHOICHANID);
            OBienBanHoiChanToanLichBacSy.LICHHOICHANID = NewID;
            Add(ActionSqlParam, OBienBanHoiChanToanLichBacSy);

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
