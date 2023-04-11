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
    public class DT_DaoTaoVienTruongProcessBll : DT_DaoTaoVienTruongTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_DaoTaoVienTruongProcessBll";
            }
        }
        public override DT_DaoTaoVienTruongCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_DaoTaoVienTruongFilterCls ODT_DaoTaoVienTruongFilter)
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
                if (ODT_DaoTaoVienTruongFilter == null)
                {
                    ODT_DaoTaoVienTruongFilter = new DT_DaoTaoVienTruongFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_DaoTaoVienTruong where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_DaoTaoVienTruongFilter.BCTONGKETCONGTACDAOTAO_ID))
                {
                    ColDbParams.Add(new DbParam("Keyword", ODT_DaoTaoVienTruongFilter.BCTONGKETCONGTACDAOTAO_ID));
                    Query += " and BCTONGKETCONGTACDAOTAO_ID = " + ActionSqlParam.SpecialChar + "BCTONGKETCONGTACDAOTAO_ID";
                }
                Query += " order by TRUONG";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_DaoTaoVienTruongCls[] DT_DaoTaoVienTruongs = DT_DaoTaoVienTruongParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_DaoTaoVienTruongs;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong)
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
                if (string.IsNullOrEmpty(ODT_DaoTaoVienTruong.ID))
                {
                    ODT_DaoTaoVienTruong.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_DaoTaoVienTruong",
                    new DbParam[]{
                    new DbParam("ID",ODT_DaoTaoVienTruong.ID),
                   new DbParam("TRUONG",ODT_DaoTaoVienTruong.TRUONG),
                   new DbParam("SOHOCVIEN",ODT_DaoTaoVienTruong.SOHOCVIEN),
                   new DbParam("NGUYENTAC",ODT_DaoTaoVienTruong.NGUYENTAC),
                   new DbParam("CHITIET",ODT_DaoTaoVienTruong.CHITIET),
                   new DbParam("BCTONGKETCONGTACDAOTAO_ID",ODT_DaoTaoVienTruong.BCTONGKETCONGTACDAOTAO_ID)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong)
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
                DBService.Update(ActionSqlParam.Trans, "DT_DaoTaoVienTruong", "ID", ID,
                    new DbParam[]{
                   new DbParam("TRUONG",ODT_DaoTaoVienTruong.TRUONG),
                   new DbParam("SOHOCVIEN",ODT_DaoTaoVienTruong.SOHOCVIEN),
                   new DbParam("NGUYENTAC",ODT_DaoTaoVienTruong.NGUYENTAC),
                   new DbParam("CHITIET",ODT_DaoTaoVienTruong.CHITIET),
                   new DbParam("BCTONGKETCONGTACDAOTAO_ID",ODT_DaoTaoVienTruong.BCTONGKETCONGTACDAOTAO_ID)
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
                string DelQuery = " Delete from DT_DaoTaoVienTruong where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_DaoTaoVienTruongCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_DaoTaoVienTruong where ID=" + ActionSqlParam.SpecialChar + "ID  ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_DaoTaoVienTruong = DT_DaoTaoVienTruongParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_DaoTaoVienTruong;
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
                DT_DaoTaoVienTruongCls ODT_DaoTaoVienTruong = CreateModel(ActionSqlParam, ID);
                ODT_DaoTaoVienTruong.ID = NewID;
                Add(ActionSqlParam, ODT_DaoTaoVienTruong);

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
