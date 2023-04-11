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
    public class DaoTaoNhanLucProcessBll : DaoTaoNhanLucTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDaoTaoNhanLucProcessBll";
            }
        }
        public override DaoTaoNhanLucCls[] Reading(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter)
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
                if (ODaoTaoNhanLucFilter == null)
                {
                    ODaoTaoNhanLucFilter = new DaoTaoNhanLucFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select * from DaoTaoNhanLuc where 1=1 ";
                if (!string.IsNullOrEmpty(ODaoTaoNhanLucFilter.PHIEUKHAOSATBVVT_ID))
                {
                    ColDbParams.Add(new DbParam("PHIEUKHAOSATBVVT_ID", ODaoTaoNhanLucFilter.PHIEUKHAOSATBVVT_ID));
                    Query += " and PHIEUKHAOSATBVVT_ID = " + ActionSqlParam.SpecialChar + "PHIEUKHAOSATBVVT_ID ";
                }
                if (!string.IsNullOrEmpty(ODaoTaoNhanLucFilter.DM_TENKHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("DM_TENKHOAHOC_ID", ODaoTaoNhanLucFilter.DM_TENKHOAHOC_ID));
                    Query += " and DM_TENKHOAHOC_ID = " + ActionSqlParam.SpecialChar + "DM_TENKHOAHOC_ID ";
                }            
                Query += " order by SOLUONG";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DaoTaoNhanLucCls[] DaoTaoNhanLucs = DaoTaoNhanLucParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DaoTaoNhanLucs;
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
        public override DaoTaoNhanLucCls[] PageReading(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter, ref long recordTotal)
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
                if (ODaoTaoNhanLucFilter == null)
                {
                    ODaoTaoNhanLucFilter = new DaoTaoNhanLucFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";

                Query = " select * from DaoTaoNhanLuc Where 1=1 ";
                recordTotalQuery = " select count(1) from DaoTaoNhanLuc Where 1=1 ";
                if (!string.IsNullOrEmpty(ODaoTaoNhanLucFilter.PHIEUKHAOSATBVVT_ID))
                {
                    ColDbParams.Add(new DbParam("PHIEUKHAOSATBVVT_ID", ODaoTaoNhanLucFilter.PHIEUKHAOSATBVVT_ID));
                    Query += " and PHIEUKHAOSATBVVT_ID = " + ActionSqlParam.SpecialChar + "PHIEUKHAOSATBVVT_ID ";
                    recordTotalQuery += " and PHIEUKHAOSATBVVT_ID = " + ActionSqlParam.SpecialChar + "PHIEUKHAOSATBVVT_ID ";
                }
                if (!string.IsNullOrEmpty(ODaoTaoNhanLucFilter.DM_TENKHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("DM_TENKHOAHOC_ID", ODaoTaoNhanLucFilter.DM_TENKHOAHOC_ID));
                    Query += " and DM_TENKHOAHOC_ID = " + ActionSqlParam.SpecialChar + "DM_TENKHOAHOC_ID ";
                    recordTotalQuery += " and DM_TENKHOAHOC_ID = " + ActionSqlParam.SpecialChar + "DM_TENKHOAHOC_ID ";
                }             
                if (!string.IsNullOrEmpty(ODaoTaoNhanLucFilter.Keyword))
                {
                    Query += " and (UPPER(SOLUONG) like UPPER('%" + ODaoTaoNhanLucFilter.Keyword + "%'))";
                    recordTotalQuery += " and (UPPER(SOLUONG) like UPPER('%" + ODaoTaoNhanLucFilter.Keyword + "%'))";
                }         
                Query += " ORDER BY SOLUONG " +
                " OFFSET " + (ODaoTaoNhanLucFilter.PageIndex * ODaoTaoNhanLucFilter.PageSize) + " ROWS " +
                " FETCH NEXT " + ODaoTaoNhanLucFilter.PageSize + " ROWS ONLY ";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DaoTaoNhanLucCls[] DaoTaoNhanLucs = DaoTaoNhanLucParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DaoTaoNhanLucs;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucCls ODaoTaoNhanLuc)
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
                if (string.IsNullOrEmpty(ODaoTaoNhanLuc.ID))
                {
                    ODaoTaoNhanLuc.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DaoTaoNhanLuc",
                    new DbParam[]{
                    new DbParam("ID",ODaoTaoNhanLuc.ID),
                    new DbParam("PHIEUKHAOSATBVVT_ID",ODaoTaoNhanLuc.PHIEUKHAOSATBVVT_ID),
                    new DbParam("SOLUONG",ODaoTaoNhanLuc.SOLUONG),
                    new DbParam("DM_TENKHOAHOC_ID",ODaoTaoNhanLuc.DM_TENKHOAHOC_ID),     
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DaoTaoNhanLucCls ODaoTaoNhanLuc)
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
                DBService.Update(ActionSqlParam.Trans, "DaoTaoNhanLuc", "ID", ID,
                    new DbParam[]{
                    new DbParam("PHIEUKHAOSATBVVT_ID",ODaoTaoNhanLuc.PHIEUKHAOSATBVVT_ID),
                    new DbParam("SOLUONG",ODaoTaoNhanLuc.SOLUONG),
                    new DbParam("DM_TENKHOAHOC_ID",ODaoTaoNhanLuc.DM_TENKHOAHOC_ID),
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
                string DelQuery = " Delete from DaoTaoNhanLuc where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DaoTaoNhanLucCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DaoTaoNhanLuc where (ID =" + ActionSqlParam.SpecialChar + "ID OR PHIEUKHAOSATBVVT_ID  =" + ActionSqlParam.SpecialChar + "ID OR DM_TENKHOAHOC_ID  =" + ActionSqlParam.SpecialChar + "ID)", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DaoTaoNhanLucCls ODaoTaoNhanLuc = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODaoTaoNhanLuc = DaoTaoNhanLucParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODaoTaoNhanLuc;
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
                DaoTaoNhanLucCls ODaoTaoNhanLuc = CreateModel(ActionSqlParam, ID);
                ODaoTaoNhanLuc.ID = NewID;
                Add(ActionSqlParam, ODaoTaoNhanLuc);

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
        public override long Count(ActionSqlParamCls ActionSqlParam, DaoTaoNhanLucFilterCls ODaoTaoNhanLucFilter)
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
                if (ODaoTaoNhanLucFilter == null)
                {
                    ODaoTaoNhanLucFilter = new DaoTaoNhanLucFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select count(1) from DaoTaoNhanLuc";
                if (!string.IsNullOrEmpty(ODaoTaoNhanLucFilter.PHIEUKHAOSATBVVT_ID))
                {
                    ColDbParams.Add(new DbParam("PHIEUKHAOSATBVVT_ID", ODaoTaoNhanLucFilter.PHIEUKHAOSATBVVT_ID));
                    Query += " and PHIEUKHAOSATBVVT_ID = " + ActionSqlParam.SpecialChar + "PHIEUKHAOSATBVVT_ID ";
                }
                if (!string.IsNullOrEmpty(ODaoTaoNhanLucFilter.DM_TENKHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("DM_TENKHOAHOC_ID", ODaoTaoNhanLucFilter.DM_TENKHOAHOC_ID));
                    Query += " and DM_TENKHOAHOC_ID = " + ActionSqlParam.SpecialChar + "DM_TENKHOAHOC_ID";
                }                 
                long result = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return result;
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
