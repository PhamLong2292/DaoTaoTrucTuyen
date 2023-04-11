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
    public class DT_LichChuyenGiaoChiTietProcessBll : DT_LichChuyenGiaoChiTietTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichChuyenGiaoChiTietProcessBll";
            }
        }
        public override DT_LichChuyenGiaoChiTietCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_LichChuyenGiaoChiTietFilterCls ODT_LichChuyenGiaoChiTietFilter)
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
                if (ODT_LichChuyenGiaoChiTietFilter == null)
                {
                    ODT_LichChuyenGiaoChiTietFilter = new DT_LichChuyenGiaoChiTietFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_LICHCHUYENGIAOCHITIET where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoChiTietFilter.LICHCHUYENGIAO_ID))
                {
                    ColDbParams.Add(new DbParam("LICHCHUYENGIAO_ID", ODT_LichChuyenGiaoChiTietFilter.LICHCHUYENGIAO_ID));
                    Query += " and LICHCHUYENGIAO_ID = " + ActionSqlParam.SpecialChar + "LICHCHUYENGIAO_ID";
                }
                Query += " order by THOIGIAN";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_LichChuyenGiaoChiTietCls[] DT_LichChuyenGiaoChiTiets = DT_LichChuyenGiaoChiTietParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_LichChuyenGiaoChiTiets;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet)
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
                if (string.IsNullOrEmpty(ODT_LichChuyenGiaoChiTiet.ID))
                {
                    ODT_LichChuyenGiaoChiTiet.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_LICHCHUYENGIAOCHITIET",
                    new DbParam[]{
                       new DbParam("ID",ODT_LichChuyenGiaoChiTiet.ID),
                       new DbParam("LICHCHUYENGIAO_ID",ODT_LichChuyenGiaoChiTiet.LICHCHUYENGIAO_ID),
                       new DbParam("THOIGIAN",ODT_LichChuyenGiaoChiTiet.THOIGIAN),
                       new DbParam("NOIDUNG",ODT_LichChuyenGiaoChiTiet.NOIDUNG),
                       new DbParam("SOCAHUONGDAN",ODT_LichChuyenGiaoChiTiet.SOCAHUONGDAN),
                       new DbParam("SOCAHOTRO",ODT_LichChuyenGiaoChiTiet.SOCAHOTRO),
                       new DbParam("CANBOS",ODT_LichChuyenGiaoChiTiet.CANBOS),
                       new DbParam("NGUOITAO_ID",ODT_LichChuyenGiaoChiTiet.NGUOITAO_ID),
                       new DbParam("NGAYTAO",ODT_LichChuyenGiaoChiTiet.NGAYTAO)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet)
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
                DBService.Update(ActionSqlParam.Trans, "DT_LICHCHUYENGIAOCHITIET", "ID", ID,
                    new DbParam[]{
                   new DbParam("LICHCHUYENGIAO_ID",ODT_LichChuyenGiaoChiTiet.LICHCHUYENGIAO_ID),
                   new DbParam("THOIGIAN",ODT_LichChuyenGiaoChiTiet.THOIGIAN),
                   new DbParam("NOIDUNG",ODT_LichChuyenGiaoChiTiet.NOIDUNG),
                   new DbParam("SOCAHUONGDAN",ODT_LichChuyenGiaoChiTiet.SOCAHUONGDAN),
                   new DbParam("SOCAHOTRO",ODT_LichChuyenGiaoChiTiet.SOCAHOTRO),
                   new DbParam("CANBOS",ODT_LichChuyenGiaoChiTiet.CANBOS),
                   new DbParam("NGUOISUA_ID",ODT_LichChuyenGiaoChiTiet.NGUOISUA_ID),
                   new DbParam("NGAYSUA",ODT_LichChuyenGiaoChiTiet.NGAYSUA)
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
                string DelQuery = " Delete from DT_LICHCHUYENGIAOCHITIET where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_LichChuyenGiaoChiTietCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_LICHCHUYENGIAOCHITIET where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_LichChuyenGiaoChiTiet = DT_LichChuyenGiaoChiTietParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_LichChuyenGiaoChiTiet;
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
                DT_LichChuyenGiaoChiTietCls ODT_LichChuyenGiaoChiTiet = CreateModel(ActionSqlParam, ID);
                ODT_LichChuyenGiaoChiTiet.ID = NewID;
                Add(ActionSqlParam, ODT_LichChuyenGiaoChiTiet);

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
