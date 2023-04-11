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
    public class DT_KetQuaChuyenGiaoProcessBll : DT_KetQuaChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_KetQuaChuyenGiaoProcessBll";
            }
        }
        public override DT_KetQuaChuyenGiaoCls[] Reading(
            ActionSqlParamCls ActionSqlParam,
            DT_KetQuaChuyenGiaoFilterCls ODT_KetQuaChuyenGiaoFilter)
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
                if (ODT_KetQuaChuyenGiaoFilter == null)
                {
                    ODT_KetQuaChuyenGiaoFilter = new DT_KetQuaChuyenGiaoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_KETQUACHUYENGIAO where 1=1 ";
                Query += " order by THOIGIANBAOCAO desc";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_KetQuaChuyenGiaoCls[] DT_KetQuaChuyenGiaos = DT_KetQuaChuyenGiaoParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_KetQuaChuyenGiaos;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao)
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
                if (string.IsNullOrEmpty(ODT_KetQuaChuyenGiao.ID))
                {
                    ODT_KetQuaChuyenGiao.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_KETQUACHUYENGIAO",
                    new DbParam[]{
                       new DbParam("ID",ODT_KetQuaChuyenGiao.ID),
                       new DbParam("SOQD",ODT_KetQuaChuyenGiao.SOQD),
                       new DbParam("NGAYQD",ODT_KetQuaChuyenGiao.NGAYQD),
                       new DbParam("TUNGAY",ODT_KetQuaChuyenGiao.TUNGAY),
                       new DbParam("DENNGAY",ODT_KetQuaChuyenGiao.DENNGAY),
                       new DbParam("SOLUOTBENHNHAN",ODT_KetQuaChuyenGiao.SOLUOTBENHNHAN),
                       new DbParam("SOCAHUONGDAN",ODT_KetQuaChuyenGiao.SOCAHUONGDAN),
                       new DbParam("SOCAHOTRO",ODT_KetQuaChuyenGiao.SOCAHOTRO),
                       new DbParam("SOGIOTHAMGIA",ODT_KetQuaChuyenGiao.SOGIOTHAMGIA),
                       new DbParam("CHAPHANHTHOIGIAN",ODT_KetQuaChuyenGiao.CHAPHANHTHOIGIAN),
                       new DbParam("CHAPHANHQUYCHE",ODT_KetQuaChuyenGiao.CHAPHANHQUYCHE),
                       new DbParam("PHOIHOP",ODT_KetQuaChuyenGiao.PHOIHOP),
                       new DbParam("HTNHIEMVU",ODT_KetQuaChuyenGiao.HTNHIEMVU),
                       new DbParam("DEXUATTHOIGIAN",ODT_KetQuaChuyenGiao.DEXUATTHOIGIAN),
                       new DbParam("DEXUATCHEDO",ODT_KetQuaChuyenGiao.DEXUATCHEDO),
                       new DbParam("DEXUATDIEUKIEN",ODT_KetQuaChuyenGiao.DEXUATDIEUKIEN),
                       new DbParam("THOIGIANBAOCAO",ODT_KetQuaChuyenGiao.THOIGIANBAOCAO),
                       new DbParam("NXTINHTHANTHAIDOYTHUC",ODT_KetQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC),
                       new DbParam("NXKHANANGTHUCHIENDOCLAP",ODT_KetQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP),
                       new DbParam("NXDUNGYCDEXUAT",ODT_KetQuaChuyenGiao.NXDUNGYCDEXUAT),
                       new DbParam("NXMUCDOHTNHIEMVU",ODT_KetQuaChuyenGiao.NXMUCDOHTNHIEMVU),
                       new DbParam("DEXUATGIAIPHAP",ODT_KetQuaChuyenGiao.DEXUATGIAIPHAP),
                       new DbParam("NOINHANXET",ODT_KetQuaChuyenGiao.NOINHANXET),
                       new DbParam("NGAYNHANXET",ODT_KetQuaChuyenGiao.NGAYNHANXET),
                       new DbParam("NGUOINHANXET",ODT_KetQuaChuyenGiao.NGUOINHANXET),
                       new DbParam("NGAYTAO",ODT_KetQuaChuyenGiao.NGAYTAO),
                       new DbParam("NGUOITAO_ID",ODT_KetQuaChuyenGiao.NGUOITAO_ID),
                       new DbParam("NGAYSUA",ODT_KetQuaChuyenGiao.NGAYSUA),
                       new DbParam("NGUOISUA_ID",ODT_KetQuaChuyenGiao.NGUOISUA_ID)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao)
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
                DBService.Update(ActionSqlParam.Trans, "DT_KETQUACHUYENGIAO", "ID", ID,
                    new DbParam[]{
                    new DbParam("SOQD",ODT_KetQuaChuyenGiao.SOQD),
                    new DbParam("NGAYQD",ODT_KetQuaChuyenGiao.NGAYQD),
                    new DbParam("TUNGAY",ODT_KetQuaChuyenGiao.TUNGAY),
                    new DbParam("DENNGAY",ODT_KetQuaChuyenGiao.DENNGAY),
                    new DbParam("SOLUOTBENHNHAN",ODT_KetQuaChuyenGiao.SOLUOTBENHNHAN),
                    new DbParam("SOCAHUONGDAN",ODT_KetQuaChuyenGiao.SOCAHUONGDAN),
                    new DbParam("SOCAHOTRO",ODT_KetQuaChuyenGiao.SOCAHOTRO),
                    new DbParam("SOGIOTHAMGIA",ODT_KetQuaChuyenGiao.SOGIOTHAMGIA),
                    new DbParam("CHAPHANHTHOIGIAN",ODT_KetQuaChuyenGiao.CHAPHANHTHOIGIAN),
                    new DbParam("CHAPHANHQUYCHE",ODT_KetQuaChuyenGiao.CHAPHANHQUYCHE),
                    new DbParam("PHOIHOP",ODT_KetQuaChuyenGiao.PHOIHOP),
                    new DbParam("HTNHIEMVU",ODT_KetQuaChuyenGiao.HTNHIEMVU),
                    new DbParam("DEXUATTHOIGIAN",ODT_KetQuaChuyenGiao.DEXUATTHOIGIAN),
                    new DbParam("DEXUATCHEDO",ODT_KetQuaChuyenGiao.DEXUATCHEDO),
                    new DbParam("DEXUATDIEUKIEN",ODT_KetQuaChuyenGiao.DEXUATDIEUKIEN),
                    new DbParam("THOIGIANBAOCAO",ODT_KetQuaChuyenGiao.THOIGIANBAOCAO),
                    new DbParam("NXTINHTHANTHAIDOYTHUC",ODT_KetQuaChuyenGiao.NXTINHTHANTHAIDOYTHUC),
                    new DbParam("NXKHANANGTHUCHIENDOCLAP",ODT_KetQuaChuyenGiao.NXKHANANGTHUCHIENDOCLAP),
                    new DbParam("NXDUNGYCDEXUAT",ODT_KetQuaChuyenGiao.NXDUNGYCDEXUAT),
                    new DbParam("NXMUCDOHTNHIEMVU",ODT_KetQuaChuyenGiao.NXMUCDOHTNHIEMVU),
                    new DbParam("DEXUATGIAIPHAP",ODT_KetQuaChuyenGiao.DEXUATGIAIPHAP),
                    new DbParam("NOINHANXET",ODT_KetQuaChuyenGiao.NOINHANXET),
                    new DbParam("NGAYNHANXET",ODT_KetQuaChuyenGiao.NGAYNHANXET),
                    new DbParam("NGUOINHANXET",ODT_KetQuaChuyenGiao.NGUOINHANXET),
                    new DbParam("NGAYTAO",ODT_KetQuaChuyenGiao.NGAYTAO),
                    new DbParam("NGUOITAO_ID",ODT_KetQuaChuyenGiao.NGUOITAO_ID),
                    new DbParam("NGAYSUA",ODT_KetQuaChuyenGiao.NGAYSUA),
                    new DbParam("NGUOISUA_ID",ODT_KetQuaChuyenGiao.NGUOISUA_ID)
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
                string DelQuery = " Delete from DT_KETQUACHUYENGIAO where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DT_KetQuaChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_KETQUACHUYENGIAO where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_KetQuaChuyenGiao = DT_KetQuaChuyenGiaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_KetQuaChuyenGiao;
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
                DT_KetQuaChuyenGiaoCls ODT_KetQuaChuyenGiao = CreateModel(ActionSqlParam, ID);
                ODT_KetQuaChuyenGiao.ID = NewID;
                Add(ActionSqlParam, ODT_KetQuaChuyenGiao);

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
