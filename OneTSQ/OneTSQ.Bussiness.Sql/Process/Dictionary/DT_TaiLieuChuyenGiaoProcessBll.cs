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
    public class DT_TaiLieuChuyenGiaoProcessBll : DT_TaiLieuChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_TaiLieuChuyenGiaoProcessBll";
            }
        }
        public override DT_TaiLieuChuyenGiaoCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_TaiLieuChuyenGiaoFilterCls ODT_TaiLieuChuyenGiaoFilter)
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
                if (ODT_TaiLieuChuyenGiaoFilter == null)
                {
                    ODT_TaiLieuChuyenGiaoFilter = new DT_TaiLieuChuyenGiaoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_TaiLieuChuyenGiao where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_TaiLieuChuyenGiaoFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + ODT_TaiLieuChuyenGiaoFilter.Keyword + "%"));
                    Query += " and TENHIENTHI like " + ActionSqlParam.SpecialChar + "Keyword";
                }
                if (!string.IsNullOrEmpty(ODT_TaiLieuChuyenGiaoFilter.LICHCHUYENGIAO_ID))
                {
                    ColDbParams.Add(new DbParam("LICHCHUYENGIAO_ID", ODT_TaiLieuChuyenGiaoFilter.LICHCHUYENGIAO_ID));
                    Query += " and LICHCHUYENGIAO_ID = " + ActionSqlParam.SpecialChar + "LICHCHUYENGIAO_ID ";
                }
                Query += " order by NGAYTAO";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_TaiLieuChuyenGiaoCls[] DT_TaiLieuChuyenGiaos = DT_TaiLieuChuyenGiaoParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_TaiLieuChuyenGiaos;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao)
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
                if (string.IsNullOrEmpty(ODT_TaiLieuChuyenGiao.ID))
                {
                    ODT_TaiLieuChuyenGiao.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_TaiLieuChuyenGiao",
                    new DbParam[]{
                    new DbParam("ID",ODT_TaiLieuChuyenGiao.ID),
                    new DbParam("LICHCHUYENGIAO_ID",ODT_TaiLieuChuyenGiao.LICHCHUYENGIAO_ID),
                    new DbParam("TENTEP",ODT_TaiLieuChuyenGiao.TENTEP),
                    new DbParam("TENHIENTHI",ODT_TaiLieuChuyenGiao.TENHIENTHI),
                    new DbParam("GHICHU",ODT_TaiLieuChuyenGiao.GHICHU),
                    new DbParam("DUONGDAN",ODT_TaiLieuChuyenGiao.DUONGDAN),
                    new DbParam("NGUOITAO_ID",ODT_TaiLieuChuyenGiao.NGUOITAO_ID),
                    new DbParam("NGAYTAO",ODT_TaiLieuChuyenGiao.NGAYTAO)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao)
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
                DBService.Update(ActionSqlParam.Trans, "DT_TaiLieuChuyenGiao", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHCHUYENGIAO_ID",ODT_TaiLieuChuyenGiao.LICHCHUYENGIAO_ID),
                   new DbParam("TENTEP",ODT_TaiLieuChuyenGiao.TENTEP),
                   new DbParam("TENHIENTHI",ODT_TaiLieuChuyenGiao.TENHIENTHI),
                   new DbParam("GHICHU",ODT_TaiLieuChuyenGiao.GHICHU),
                   new DbParam("DUONGDAN",ODT_TaiLieuChuyenGiao.DUONGDAN),
                   new DbParam("NGUOITAO_ID",ODT_TaiLieuChuyenGiao.NGUOITAO_ID),
                   new DbParam("NGAYTAO",ODT_TaiLieuChuyenGiao.NGAYTAO)
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
                string DelQuery = " Delete from DT_TaiLieuChuyenGiao where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_TaiLieuChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_TaiLieuChuyenGiao where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_TaiLieuChuyenGiao = DT_TaiLieuChuyenGiaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_TaiLieuChuyenGiao;
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
                DT_TaiLieuChuyenGiaoCls ODT_TaiLieuChuyenGiao = CreateModel(ActionSqlParam, ID);
                ODT_TaiLieuChuyenGiao.ID = NewID;
                Add(ActionSqlParam, ODT_TaiLieuChuyenGiao);

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
