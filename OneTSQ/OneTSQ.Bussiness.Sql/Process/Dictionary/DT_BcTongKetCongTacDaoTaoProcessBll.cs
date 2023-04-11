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
    public class DT_BcTongKetCongTacDaoTaoProcessBll : DT_BcTongKetCongTacDaoTaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_BcTongKetCongTacDaoTaoProcessBll";
            }
        }
        public override DT_BcTongKetCongTacDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter)
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
                if (ODT_BcTongKetCongTacDaoTaoFilter == null)
                {
                    ODT_BcTongKetCongTacDaoTaoFilter = new DT_BcTongKetCongTacDaoTaoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                if (ODT_BcTongKetCongTacDaoTaoFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TuNgay", ODT_BcTongKetCongTacDaoTaoFilter.TuNgay));
                    Query += " and NGAYTAO >= " + ActionSqlParam.SpecialChar + "TuNgay ";
                }
                if (ODT_BcTongKetCongTacDaoTaoFilter.DenNgay != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", ODT_BcTongKetCongTacDaoTaoFilter.DenNgay));
                    Query += " and NGAYTAO < " + ActionSqlParam.SpecialChar + "DenNgay ";
                }
                Query += " order by NGAYTAO desc";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_BcTongKetCongTacDaoTaoCls[] DT_BcTongKetCongTacDaoTaos = DT_BcTongKetCongTacDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_BcTongKetCongTacDaoTaos;
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
        public override DT_BcTongKetCongTacDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoFilterCls ODT_BcTongKetCongTacDaoTaoFilter, ref long recordTotal)
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
                if (ODT_BcTongKetCongTacDaoTaoFilter == null)
                {
                    ODT_BcTongKetCongTacDaoTaoFilter = new DT_BcTongKetCongTacDaoTaoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select * from DT_BCTONGKETCONGTACDAOTAO where 1 = 1 " + ODT_BcTongKetCongTacDaoTaoFilter.DataPermissionQuery;
                string recordTotalQuery = " select count(1) from DT_BCTONGKETCONGTACDAOTAO where 1 = 1 " + ODT_BcTongKetCongTacDaoTaoFilter.DataPermissionQuery;

                if (ODT_BcTongKetCongTacDaoTaoFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TuNgay", ODT_BcTongKetCongTacDaoTaoFilter.TuNgay));
                    Query += " and NGAYTAO >= " + ActionSqlParam.SpecialChar + "TuNgay ";
                    recordTotalQuery += " and NGAYTAO >= " + ActionSqlParam.SpecialChar + "TuNgay ";
                }
                if (ODT_BcTongKetCongTacDaoTaoFilter.DenNgay != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", ODT_BcTongKetCongTacDaoTaoFilter.DenNgay));
                    Query += " and NGAYTAO < " + ActionSqlParam.SpecialChar + "DenNgay ";
                    recordTotalQuery += " and NGAYTAO < " + ActionSqlParam.SpecialChar + "DenNgay ";
                }
                Query += "ORDER BY NGAYTAO DESC " +
                    "OFFSET " + (ODT_BcTongKetCongTacDaoTaoFilter.PageIndex * ODT_BcTongKetCongTacDaoTaoFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + ODT_BcTongKetCongTacDaoTaoFilter.PageSize + " ROWS ONLY ";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_BcTongKetCongTacDaoTaoCls[] DT_BcTongKetCongTacDaoTaos = DT_BcTongKetCongTacDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_BcTongKetCongTacDaoTaos;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao)
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
                if (string.IsNullOrEmpty(ODT_BcTongKetCongTacDaoTao.ID))
                {
                    ODT_BcTongKetCongTacDaoTao.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_BcTongKetCongTacDaoTao",
                    new DbParam[]{
                new DbParam("ID",ODT_BcTongKetCongTacDaoTao.ID),
                new DbParam("NAM",ODT_BcTongKetCongTacDaoTao.NAM),
                new DbParam("TUNGAY",ODT_BcTongKetCongTacDaoTao.TUNGAY),
                new DbParam("DENNGAY",ODT_BcTongKetCongTacDaoTao.DENNGAY),
                new DbParam("DTLIENTUC",ODT_BcTongKetCongTacDaoTao.DTLIENTUC),
                new DbParam("DTTHEOKEHOACH",ODT_BcTongKetCongTacDaoTao.DTTHEOKEHOACH),
                new DbParam("DTNANGCAO",ODT_BcTongKetCongTacDaoTao.DTNANGCAO),
                new DbParam("DTTHEONGANSACHNHANUOC",ODT_BcTongKetCongTacDaoTao.DTTHEONGANSACHNHANUOC),
                new DbParam("DTTHEONHUCAUXAHOI",ODT_BcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI),
                new DbParam("DTVIENTRUONG",ODT_BcTongKetCongTacDaoTao.DTVIENTRUONG),
                new DbParam("CHUONGTRINHTAILIEU",ODT_BcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU),
                new DbParam("THUANLOI",ODT_BcTongKetCongTacDaoTao.THUANLOI),
                new DbParam("KHOKHAN",ODT_BcTongKetCongTacDaoTao.KHOKHAN),
                new DbParam("KHACPHUC",ODT_BcTongKetCongTacDaoTao.KHACPHUC),
                new DbParam("PHUONGHUONGKEHOACH",ODT_BcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH),
                new DbParam("NGUOITAO_ID",ODT_BcTongKetCongTacDaoTao.NGUOITAO_ID),
                new DbParam("NGAYTAO",ODT_BcTongKetCongTacDaoTao.NGAYTAO),
                new DbParam("NGUOISUA_ID",ODT_BcTongKetCongTacDaoTao.NGUOISUA_ID),
                new DbParam("NGAYSUA",ODT_BcTongKetCongTacDaoTao.NGAYSUA)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao)
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
                DBService.Update(ActionSqlParam.Trans, "DT_BcTongKetCongTacDaoTao", "ID", ID,
                    new DbParam[]{
                   new DbParam("NAM",ODT_BcTongKetCongTacDaoTao.NAM),
                   new DbParam("TUNGAY",ODT_BcTongKetCongTacDaoTao.TUNGAY),
                   new DbParam("DENNGAY",ODT_BcTongKetCongTacDaoTao.DENNGAY),
                   new DbParam("DTLIENTUC",ODT_BcTongKetCongTacDaoTao.DTLIENTUC),
                   new DbParam("DTTHEOKEHOACH",ODT_BcTongKetCongTacDaoTao.DTTHEOKEHOACH),
                   new DbParam("DTNANGCAO",ODT_BcTongKetCongTacDaoTao.DTNANGCAO),
                   new DbParam("DTTHEONGANSACHNHANUOC",ODT_BcTongKetCongTacDaoTao.DTTHEONGANSACHNHANUOC),
                   new DbParam("DTTHEONHUCAUXAHOI",ODT_BcTongKetCongTacDaoTao.DTTHEONHUCAUXAHOI),
                   new DbParam("DTVIENTRUONG",ODT_BcTongKetCongTacDaoTao.DTVIENTRUONG),
                   new DbParam("CHUONGTRINHTAILIEU",ODT_BcTongKetCongTacDaoTao.CHUONGTRINHTAILIEU),
                   new DbParam("THUANLOI",ODT_BcTongKetCongTacDaoTao.THUANLOI),
                   new DbParam("KHOKHAN",ODT_BcTongKetCongTacDaoTao.KHOKHAN),
                   new DbParam("KHACPHUC",ODT_BcTongKetCongTacDaoTao.KHACPHUC),
                   new DbParam("PHUONGHUONGKEHOACH",ODT_BcTongKetCongTacDaoTao.PHUONGHUONGKEHOACH),
                   new DbParam("NGUOITAO_ID",ODT_BcTongKetCongTacDaoTao.NGUOITAO_ID),
                   new DbParam("NGAYTAO",ODT_BcTongKetCongTacDaoTao.NGAYTAO),
                   new DbParam("NGUOISUA_ID",ODT_BcTongKetCongTacDaoTao.NGUOISUA_ID),
                   new DbParam("NGAYSUA",ODT_BcTongKetCongTacDaoTao.NGAYSUA)
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
                                    " Delete from DT_DaoTaoVienTruong where BCTONGKETCONGTACDAOTAO_ID=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DT_BcTongKetCongTacDaoTao where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override DT_BcTongKetCongTacDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_BcTongKetCongTacDaoTao where ID=" + ActionSqlParam.SpecialChar + "ID  ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_BcTongKetCongTacDaoTao = DT_BcTongKetCongTacDaoTaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_BcTongKetCongTacDaoTao;
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
                DT_BcTongKetCongTacDaoTaoCls ODT_BcTongKetCongTacDaoTao = CreateModel(ActionSqlParam, ID);
                ODT_BcTongKetCongTacDaoTao.ID = NewID;
                Add(ActionSqlParam, ODT_BcTongKetCongTacDaoTao);

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
