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
    public class DT_LichLyThuyetProcessBll : DT_LichLyThuyetTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichLyThuyetProcessBll";
            }
        }
        public override DT_LichLyThuyetCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_LichLyThuyetFilterCls ODT_LichLyThuyetFilter)
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
                if (ODT_LichLyThuyetFilter == null)
                {
                    ODT_LichLyThuyetFilter = new DT_LichLyThuyetFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_LichLyThuyet where 1=1 ";
                Query += " order by BATDAU";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_LichLyThuyetCls[] DT_LichLyThuyets = DT_LichLyThuyetParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_LichLyThuyets;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_LichLyThuyetCls ODT_LichLyThuyet)
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
                if (string.IsNullOrEmpty(ODT_LichLyThuyet.ID))
                {
                    ODT_LichLyThuyet.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_LichLyThuyet",
                    new DbParam[]{
                    new DbParam("ID",ODT_LichLyThuyet.ID),
                    new DbParam("BATDAU",ODT_LichLyThuyet.BATDAU),
                    new DbParam("KETTHUC",ODT_LichLyThuyet.KETTHUC),
                    new DbParam("DIADIEM",ODT_LichLyThuyet.DIADIEM),
                    new DbParam("PTCHUYENMON_ID",ODT_LichLyThuyet.PTCHUYENMON_ID),
                    new DbParam("LANHDAO_ID",ODT_LichLyThuyet.LANHDAO_ID),
                    new DbParam("NGUOITAO_ID",ODT_LichLyThuyet.NGUOITAO_ID),
                    new DbParam("NGAYTAO",ODT_LichLyThuyet.NGAYTAO),
                    new DbParam("NGUOISUA_ID",ODT_LichLyThuyet.NGUOISUA_ID),
                    new DbParam("NGAYSUA",ODT_LichLyThuyet.NGAYSUA)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_LichLyThuyetCls ODT_LichLyThuyet)
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
                DBService.Update(ActionSqlParam.Trans, "DT_LichLyThuyet", "ID", ID,
                    new DbParam[]{
                   new DbParam("BATDAU",ODT_LichLyThuyet.BATDAU),
                   new DbParam("KETTHUC",ODT_LichLyThuyet.KETTHUC),
                   new DbParam("DIADIEM",ODT_LichLyThuyet.DIADIEM),
                   new DbParam("PTCHUYENMON_ID",ODT_LichLyThuyet.PTCHUYENMON_ID),
                   new DbParam("LANHDAO_ID",ODT_LichLyThuyet.LANHDAO_ID),
                   new DbParam("NGUOITAO_ID",ODT_LichLyThuyet.NGUOITAO_ID),
                   new DbParam("NGAYTAO",ODT_LichLyThuyet.NGAYTAO),
                   new DbParam("NGUOISUA_ID",ODT_LichLyThuyet.NGUOISUA_ID),
                   new DbParam("NGAYSUA",ODT_LichLyThuyet.NGAYSUA)
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
                string DelQuery = " DECLARE BEGIN " +
                                    " Delete from DT_LichLyThuyetChiTiet where LICHLYTHUYET_ID =" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DT_LichLyThuyet where ID=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " END;";
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
        public override DT_LichLyThuyetCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_LichLyThuyet where ID=" + ActionSqlParam.SpecialChar + "ID  ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_LichLyThuyetCls ODT_LichLyThuyet = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_LichLyThuyet = DT_LichLyThuyetParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_LichLyThuyet;
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
                DT_LichLyThuyetCls ODT_LichLyThuyet = CreateModel(ActionSqlParam, ID);
                ODT_LichLyThuyet.ID = NewID;
                Add(ActionSqlParam, ODT_LichLyThuyet);

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
