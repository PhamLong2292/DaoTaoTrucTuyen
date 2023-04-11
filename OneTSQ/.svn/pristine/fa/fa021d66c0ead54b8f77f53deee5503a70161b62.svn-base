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
    public class LapLichTepDinhKemProcessBll : LapLichTepDinhKemTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlLapLichTepDinhKemProcessBll";
            }
        }
        public override LapLichTepDinhKemCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter)
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
                if (OLapLichTepDinhKemFilter == null)
                {
                    OLapLichTepDinhKemFilter = new LapLichTepDinhKemFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from LapLichTepDinhKem where 1=1 ";
                if (!string.IsNullOrEmpty(OLapLichTepDinhKemFilter.LICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("LICHHOICHANID", OLapLichTepDinhKemFilter.LICHHOICHANID));
                    Query += " and LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID";
                }
                Query += " order by TENHIENTHI";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                LapLichTepDinhKemCls[] LapLichTepDinhKems = LapLichTepDinhKemParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return LapLichTepDinhKems;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, LapLichTepDinhKemCls OLapLichTepDinhKem)
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
                if (string.IsNullOrEmpty(OLapLichTepDinhKem.ID))
                {
                    OLapLichTepDinhKem.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "LapLichTepDinhKem",
                    new DbParam[]{
                    new DbParam("ID",OLapLichTepDinhKem.ID),
                    new DbParam("LICHHOICHANID",OLapLichTepDinhKem.LICHHOICHANID),
                    new DbParam("TENTEP",OLapLichTepDinhKem.TENTEP),
                    new DbParam("DUONGDAN",OLapLichTepDinhKem.DUONGDAN),
                    new DbParam("TENHIENTHI",OLapLichTepDinhKem.TENHIENTHI)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, LapLichTepDinhKemCls OLapLichTepDinhKem)
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
                DBService.Update(ActionSqlParam.Trans, "LapLichTepDinhKem", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHHOICHANID",OLapLichTepDinhKem.LICHHOICHANID),
                   new DbParam("TENTEP",OLapLichTepDinhKem.TENTEP),
                   new DbParam("DUONGDAN",OLapLichTepDinhKem.DUONGDAN),
                   new DbParam("TENHIENTHI",OLapLichTepDinhKem.TENHIENTHI)
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
                string DelQuery = " Delete from LapLichTepDinhKem where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override LapLichTepDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from LapLichTepDinhKem where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                LapLichTepDinhKemCls OLapLichTepDinhKem = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OLapLichTepDinhKem = LapLichTepDinhKemParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OLapLichTepDinhKem;
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
                LapLichTepDinhKemCls OLapLichTepDinhKem = CreateModel(ActionSqlParam, ID);
                OLapLichTepDinhKem.ID = NewID;
                Add(ActionSqlParam, OLapLichTepDinhKem);

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
