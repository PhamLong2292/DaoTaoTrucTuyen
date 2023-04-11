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
    public class DoHieuQuaGiangDayProcessBll : DoHieuQuaGiangDayTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDoHieuQuaGiangDayProcessBll";
            }
        }
        public override DoHieuQuaGiangDayCls[] Reading(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter)
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
                if (ODoHieuQuaGiangDayFilter == null)
                {
                    ODoHieuQuaGiangDayFilter = new DoHieuQuaGiangDayFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = " select * from DoHieuQuaGiangDay where 1=1 ";
                if (!string.IsNullOrEmpty(ODoHieuQuaGiangDayFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
                {
                    ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", ODoHieuQuaGiangDayFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
                    Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
                }
                Query += "ORDER BY STT ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DoHieuQuaGiangDayCls[] DoHieuQuaGiangDays = DoHieuQuaGiangDayParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DoHieuQuaGiangDays;
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
        public override long Count(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (ODoHieuQuaGiangDayFilter == null)
                    ODoHieuQuaGiangDayFilter = new DoHieuQuaGiangDayFilterCls();
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = " select COUNT (*) from DoHieuQuaGiangDay where 1=1 ";
                if (!string.IsNullOrEmpty(ODoHieuQuaGiangDayFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID))
                {
                    ColDbParams.Add(new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID", ODoHieuQuaGiangDayFilter.PHIEUDANHGIACHATLUONGDAOTAO_ID));
                    Query += " and PHIEUDANHGIACHATLUONGDAOTAO_ID = " + ActionSqlParam.SpecialChar + "PHIEUDANHGIACHATLUONGDAOTAO_ID ";
                }
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                long count = DoHieuQuaGiangDayParser.CountFromDataTable(dsResult.Tables[0]);
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
        public override DoHieuQuaGiangDayCls[] ReadingWithPaging(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayFilterCls ODoHieuQuaGiangDayFilter, int PageIndex, int PageSize)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (ODoHieuQuaGiangDayFilter == null)
                    ODoHieuQuaGiangDayFilter = new DoHieuQuaGiangDayFilterCls();
                var skip = PageIndex * PageSize;
                string Query = " select * from DoHieuQuaGiangDay OFFSET " + skip.ToString() + " ROWS FETCH NEXT " + PageSize + " ROWS ONLY";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                DoHieuQuaGiangDayCls[] DoHieuQuaGiangDays = DoHieuQuaGiangDayParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DoHieuQuaGiangDays;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay)
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
                DBService.Insert(ActionSqlParam.Trans, "DoHieuQuaGiangDay",
                    new DbParam[]{
                    new DbParam("ID",ODoHieuQuaGiangDay.ID),
                    new DbParam("CHUYENKHOADAOTAOTTMA",ODoHieuQuaGiangDay.CHUYENKHOADAOTAOTTMA),
                    new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",ODoHieuQuaGiangDay.PHIEUDANHGIACHATLUONGDAOTAO_ID),
                    new DbParam("DANHGIA",ODoHieuQuaGiangDay.DANHGIA),
                    new DbParam("STT",ODoHieuQuaGiangDay.STT)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DoHieuQuaGiangDayCls ODoHieuQuaGiangDay)
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
                DBService.Update(ActionSqlParam.Trans, "DoHieuQuaGiangDay", "ID", ID,
                   new DbParam[]{
               new DbParam("CHUYENKHOADAOTAOTTMA",ODoHieuQuaGiangDay.CHUYENKHOADAOTAOTTMA),
               new DbParam("PHIEUDANHGIACHATLUONGDAOTAO_ID",ODoHieuQuaGiangDay.PHIEUDANHGIACHATLUONGDAOTAO_ID),
               new DbParam("DANHGIA",ODoHieuQuaGiangDay.DANHGIA),
               new DbParam("STT",ODoHieuQuaGiangDay.STT)
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
                string DelQuery = " Delete from DoHieuQuaGiangDay where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DoHieuQuaGiangDayCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from DoHieuQuaGiangDay where (ID=" + ActionSqlParam.SpecialChar + "ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
                DoHieuQuaGiangDayCls ODoHieuQuaGiangDay = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODoHieuQuaGiangDay = DoHieuQuaGiangDayParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODoHieuQuaGiangDay;
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
                DoHieuQuaGiangDayCls ODoHieuQuaGiangDay = CreateModel(ActionSqlParam, ID);
                ODoHieuQuaGiangDay.ID = NewID;
                Add(ActionSqlParam, ODoHieuQuaGiangDay);

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
