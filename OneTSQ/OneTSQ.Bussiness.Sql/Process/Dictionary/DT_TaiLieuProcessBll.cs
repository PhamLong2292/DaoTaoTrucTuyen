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
    public class DT_TaiLieuProcessBll : DT_TaiLieuTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_TaiLieuProcessBll";
            }
        }
        public override DT_TaiLieuCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_TaiLieuFilterCls ODT_TaiLieuFilter)
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
                if (ODT_TaiLieuFilter == null)
                {
                    ODT_TaiLieuFilter = new DT_TaiLieuFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_TaiLieu where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_TaiLieuFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + ODT_TaiLieuFilter.Keyword + "%"));
                    Query += " and TENHIENTHI like " + ActionSqlParam.SpecialChar + "Keyword";
                }
                if (!string.IsNullOrEmpty(ODT_TaiLieuFilter.KHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("KHOAHOC_ID", ODT_TaiLieuFilter.KHOAHOC_ID));
                    Query += " and KHOAHOC_ID = " + ActionSqlParam.SpecialChar + "KHOAHOC_ID ";
                }
                Query += " order by NGAYTAO";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_TaiLieuCls[] DT_TaiLieus = DT_TaiLieuParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_TaiLieus;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_TaiLieuCls ODT_TaiLieu)
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
                if (string.IsNullOrEmpty(ODT_TaiLieu.ID))
                {
                    ODT_TaiLieu.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_TaiLieu",
                    new DbParam[]{
                    new DbParam("ID",ODT_TaiLieu.ID),
                    new DbParam("KHOAHOC_ID",ODT_TaiLieu.KHOAHOC_ID),
                    new DbParam("TENTEP",ODT_TaiLieu.TENTEP),
                    new DbParam("TENHIENTHI",ODT_TaiLieu.TENHIENTHI),
                    new DbParam("GHICHU",ODT_TaiLieu.GHICHU),
                    new DbParam("DUONGDAN",ODT_TaiLieu.DUONGDAN),
                    new DbParam("NGUOITAO_ID",ODT_TaiLieu.NGUOITAO_ID),
                    new DbParam("NGAYTAO",ODT_TaiLieu.NGAYTAO)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_TaiLieuCls ODT_TaiLieu)
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
                DBService.Update(ActionSqlParam.Trans, "DT_TaiLieu", "ID", ID,
                    new DbParam[]{
                   new DbParam("KHOAHOC_ID",ODT_TaiLieu.KHOAHOC_ID),
                   new DbParam("TENTEP",ODT_TaiLieu.TENTEP),
                   new DbParam("TENHIENTHI",ODT_TaiLieu.TENHIENTHI),
                   new DbParam("GHICHU",ODT_TaiLieu.GHICHU),
                   new DbParam("DUONGDAN",ODT_TaiLieu.DUONGDAN),
                   new DbParam("NGUOITAO_ID",ODT_TaiLieu.NGUOITAO_ID),
                   new DbParam("NGAYTAO",ODT_TaiLieu.NGAYTAO)
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
                string DelQuery = " Delete from DT_TaiLieu where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_TaiLieuCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_TaiLieu where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_TaiLieuCls ODT_TaiLieu = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_TaiLieu = DT_TaiLieuParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_TaiLieu;
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
                DT_TaiLieuCls ODT_TaiLieu = CreateModel(ActionSqlParam, ID);
                ODT_TaiLieu.ID = NewID;
                Add(ActionSqlParam, ODT_TaiLieu);

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
