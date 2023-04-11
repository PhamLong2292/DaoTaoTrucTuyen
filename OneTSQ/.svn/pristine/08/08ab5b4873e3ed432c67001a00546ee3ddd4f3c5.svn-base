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
    public class LapLichThanhVienHoiChanProcessBll : LapLichThanhVienHoiChanTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlLapLichThanhVienHoiChanProcessBll";
            }
        }
        public override LapLichThanhVienHoiChanCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            LapLichThanhVienHoiChanFilterCls OLapLichThanhVienHoiChanFilter)
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
                if (OLapLichThanhVienHoiChanFilter == null)
                {
                    OLapLichThanhVienHoiChanFilter = new LapLichThanhVienHoiChanFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from LapLichThanhVienHoiChan where 1=1 ";
                if (!string.IsNullOrEmpty(OLapLichThanhVienHoiChanFilter.LICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("LICHHOICHANID", OLapLichThanhVienHoiChanFilter.LICHHOICHANID));
                    Query += " and LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID";
                }
                if (!string.IsNullOrEmpty(OLapLichThanhVienHoiChanFilter.DONVICONGTACMA))
                {
                    ColDbParams.Add(new DbParam("DONVICONGTACMA", OLapLichThanhVienHoiChanFilter.DONVICONGTACMA));
                    Query += " and DONVICONGTACMA = " + ActionSqlParam.SpecialChar + "DONVICONGTACMA";
                }
                else if (!string.IsNullOrEmpty(OLapLichThanhVienHoiChanFilter.KHACDONVICONGTACMA))
                {
                    ColDbParams.Add(new DbParam("KHACDONVICONGTACMA", OLapLichThanhVienHoiChanFilter.KHACDONVICONGTACMA));
                    Query += " and DONVICONGTACMA <> " + ActionSqlParam.SpecialChar + "KHACDONVICONGTACMA";
                }

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                LapLichThanhVienHoiChanCls[] LapLichThanhVienHoiChans = LapLichThanhVienHoiChanParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return LapLichThanhVienHoiChans;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan)
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
                if (string.IsNullOrEmpty(OLapLichThanhVienHoiChan.ID))
                {
                    OLapLichThanhVienHoiChan.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "LapLichThanhVienHoiChan",
                    new DbParam[]{
                    new DbParam("ID",OLapLichThanhVienHoiChan.ID),
                    new DbParam("LICHHOICHANID",OLapLichThanhVienHoiChan.LICHHOICHANID),
                    new DbParam("BACSYID",OLapLichThanhVienHoiChan.BACSYID),
                    new DbParam("DONVICONGTACMA",OLapLichThanhVienHoiChan.DONVICONGTACMA),
                    new DbParam("DONVICONGTAC",OLapLichThanhVienHoiChan.DONVICONGTAC)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan)
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
                DBService.Update(ActionSqlParam.Trans, "LapLichThanhVienHoiChan", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHHOICHANID",OLapLichThanhVienHoiChan.LICHHOICHANID),
                   new DbParam("BACSYID",OLapLichThanhVienHoiChan.BACSYID),
                   new DbParam("DONVICONGTACMA",OLapLichThanhVienHoiChan.DONVICONGTACMA),
                   new DbParam("DONVICONGTAC",OLapLichThanhVienHoiChan.DONVICONGTAC)
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
                string DelQuery = " Delete from LapLichThanhVienHoiChan where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override LapLichThanhVienHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from LapLichThanhVienHoiChan where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OLapLichThanhVienHoiChan = LapLichThanhVienHoiChanParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OLapLichThanhVienHoiChan;
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
                LapLichThanhVienHoiChanCls OLapLichThanhVienHoiChan = CreateModel(ActionSqlParam, ID);
                OLapLichThanhVienHoiChan.ID = NewID;
                Add(ActionSqlParam, OLapLichThanhVienHoiChan);

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
