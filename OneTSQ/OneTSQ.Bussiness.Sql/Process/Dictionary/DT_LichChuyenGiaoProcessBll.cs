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
    public class DT_LichChuyenGiaoProcessBll : DT_LichChuyenGiaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichChuyenGiaoProcessBll";
            }
        }
        public override DT_LichChuyenGiaoCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter)
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
                if (ODT_LichChuyenGiaoFilter == null)
                {
                    ODT_LichChuyenGiaoFilter = new DT_LichChuyenGiaoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_LICHCHUYENGIAO where 1=1 " + ODT_LichChuyenGiaoFilter.DataPermissionQuery;
                if (ODT_LichChuyenGiaoFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TuNgay", ODT_LichChuyenGiaoFilter.TuNgay));
                    Query += " and BATDAU >= " + ActionSqlParam.SpecialChar + "TuNgay ";
                }
                if (ODT_LichChuyenGiaoFilter.DenNgay != null)
                {
                    ColDbParams.Add(new DbParam("DenNgay", ODT_LichChuyenGiaoFilter.DenNgay));
                    Query += " and BATDAU < " + ActionSqlParam.SpecialChar + "DenNgay ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.KYTHUAT_MA))
                {
                    ColDbParams.Add(new DbParam("KYTHUAT_MA", ODT_LichChuyenGiaoFilter.KYTHUAT_MA));
                    Query += " and KYTHUAT_MA = " + ActionSqlParam.SpecialChar + "KYTHUAT_MA ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.KHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("KHOAHOC_ID", ODT_LichChuyenGiaoFilter.KHOAHOC_ID));
                    Query += " and KHOAHOC_ID = " + ActionSqlParam.SpecialChar + "KHOAHOC_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.BENHVIEN_MA))
                {
                    ColDbParams.Add(new DbParam("BENHVIEN_MA", ODT_LichChuyenGiaoFilter.BENHVIEN_MA));
                    Query += " and BENHVIEN_MA = " + ActionSqlParam.SpecialChar + "BENHVIEN_MA ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.BACSY_ID))
                {
                    ColDbParams.Add(new DbParam("BACSY_ID", ODT_LichChuyenGiaoFilter.BACSY_ID));
                    Query += " and BACSY_ID = " + ActionSqlParam.SpecialChar + "BACSY_ID ";
                }
                if (ODT_LichChuyenGiaoFilter.Nam != null)
                {
                    ColDbParams.Add(new DbParam("NAM", ODT_LichChuyenGiaoFilter.Nam));
                    Query += " and EXTRACT(YEAR FROM BATDAU) = " + ActionSqlParam.SpecialChar + "NAM";
                }
                if (ODT_LichChuyenGiaoFilter.TrangThai != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", ODT_LichChuyenGiaoFilter.TrangThai));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI";
                }
                Query += " order by BATDAU desc";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_LichChuyenGiaoCls[] DT_LichChuyenGiaos = DT_LichChuyenGiaoParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_LichChuyenGiaos;
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
        public override DT_LichChuyenGiaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoFilterCls ODT_LichChuyenGiaoFilter, ref long recordTotal)
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
                if (ODT_LichChuyenGiaoFilter == null)
                {
                    ODT_LichChuyenGiaoFilter = new DT_LichChuyenGiaoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";

                Query = " select * from DT_LICHCHUYENGIAO where 1=1 " + ODT_LichChuyenGiaoFilter.DataPermissionQuery;
                recordTotalQuery = " select count(*) from DT_LICHCHUYENGIAO where 1=1 " + ODT_LichChuyenGiaoFilter.DataPermissionQuery;
                if (ODT_LichChuyenGiaoFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TuNgay", ODT_LichChuyenGiaoFilter.TuNgay));
                    Query += " and BATDAU >= " + ActionSqlParam.SpecialChar + "TuNgay ";
                    recordTotalQuery += " and BATDAU >= " + ActionSqlParam.SpecialChar + "TuNgay ";
                }
                if (ODT_LichChuyenGiaoFilter.DenNgay != null)
                {
                    ColDbParams.Add(new DbParam("DenNgay", ODT_LichChuyenGiaoFilter.DenNgay));
                    Query += " and BATDAU < " + ActionSqlParam.SpecialChar + "DenNgay ";
                    recordTotalQuery += " and BATDAU < " + ActionSqlParam.SpecialChar + "DenNgay ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.KYTHUAT_MA))
                {
                    ColDbParams.Add(new DbParam("KYTHUAT_MA", ODT_LichChuyenGiaoFilter.KYTHUAT_MA));
                    Query += " and KYTHUAT_MA = " + ActionSqlParam.SpecialChar + "KYTHUAT_MA ";
                    recordTotalQuery += " and KYTHUAT_MA = " + ActionSqlParam.SpecialChar + "KYTHUAT_MA ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.KHOAHOC_ID))
                {
                    ColDbParams.Add(new DbParam("KHOAHOC_ID", ODT_LichChuyenGiaoFilter.KHOAHOC_ID));
                    Query += " and KHOAHOC_ID = " + ActionSqlParam.SpecialChar + "KHOAHOC_ID ";
                    recordTotalQuery += " and KHOAHOC_ID = " + ActionSqlParam.SpecialChar + "KHOAHOC_ID ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.BENHVIEN_MA))
                {
                    ColDbParams.Add(new DbParam("BENHVIEN_MA", ODT_LichChuyenGiaoFilter.BENHVIEN_MA));
                    Query += " and BENHVIEN_MA = " + ActionSqlParam.SpecialChar + "BENHVIEN_MA ";
                    recordTotalQuery += " and BENHVIEN_MA = " + ActionSqlParam.SpecialChar + "BENHVIEN_MA ";
                }
                if (!string.IsNullOrEmpty(ODT_LichChuyenGiaoFilter.BACSY_ID))
                {
                    ColDbParams.Add(new DbParam("BACSY_ID", ODT_LichChuyenGiaoFilter.BACSY_ID));
                    Query += " and BACSY_ID = " + ActionSqlParam.SpecialChar + "BACSY_ID ";
                    recordTotalQuery += " and BACSY_ID = " + ActionSqlParam.SpecialChar + "BACSY_ID ";
                }
                if (ODT_LichChuyenGiaoFilter.Nam != null)
                {
                    ColDbParams.Add(new DbParam("NAM", ODT_LichChuyenGiaoFilter.Nam));
                    Query += " and EXTRACT(YEAR FROM BATDAU) = " + ActionSqlParam.SpecialChar + "NAM";
                    recordTotalQuery += " and EXTRACT(YEAR FROM BATDAU) = " + ActionSqlParam.SpecialChar + "NAM";
                }
                if (ODT_LichChuyenGiaoFilter.TrangThai != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", ODT_LichChuyenGiaoFilter.TrangThai));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI";
                    recordTotalQuery += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI";
                }
                Query += " ORDER BY BATDAU DESC " +
                    " OFFSET " + (ODT_LichChuyenGiaoFilter.PageIndex * ODT_LichChuyenGiaoFilter.PageSize) + " ROWS " +
                    " FETCH NEXT " + ODT_LichChuyenGiaoFilter.PageSize + " ROWS ONLY ";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_LichChuyenGiaoCls[] DT_LichChuyenGiaos = DT_LichChuyenGiaoParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_LichChuyenGiaos;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_LichChuyenGiaoCls ODT_LichChuyenGiao)
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
                if (string.IsNullOrEmpty(ODT_LichChuyenGiao.ID))
                {
                    ODT_LichChuyenGiao.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_LICHCHUYENGIAO",
                    new DbParam[]{
                       new DbParam("ID",ODT_LichChuyenGiao.ID),
                       new DbParam("KYTHUAT_MA",ODT_LichChuyenGiao.KYTHUAT_MA),
                       new DbParam("KHOAHOC_ID",ODT_LichChuyenGiao.KHOAHOC_ID),
                       new DbParam("BENHVIEN_MA",ODT_LichChuyenGiao.BENHVIEN_MA),
                       new DbParam("LANHDAOBENHVIEN_ID",ODT_LichChuyenGiao.LANHDAOBENHVIEN_ID),
                       new DbParam("BACSY_ID",ODT_LichChuyenGiao.BACSY_ID),
                       new DbParam("BATDAU",ODT_LichChuyenGiao.BATDAU),
                       new DbParam("COGIAYDIDUONG",ODT_LichChuyenGiao.COGIAYDIDUONG),
                       new DbParam("GIAYTO",ODT_LichChuyenGiao.GIAYTO),
                       new DbParam("KETTHUC",ODT_LichChuyenGiao.KETTHUC),
                       new DbParam("NGUOITAO_ID",ODT_LichChuyenGiao.NGUOITAO_ID),
                       new DbParam("NGAYTAO",ODT_LichChuyenGiao.NGAYTAO),
                       new DbParam("NGUOISUA_ID",ODT_LichChuyenGiao.NGUOISUA_ID),
                       new DbParam("NGAYSUA",ODT_LichChuyenGiao.NGAYSUA),
                       new DbParam("TRANGTHAI",ODT_LichChuyenGiao.TRANGTHAI)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_LichChuyenGiaoCls ODT_LichChuyenGiao)
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
                DBService.Update(ActionSqlParam.Trans, "DT_LICHCHUYENGIAO", "ID", ID,
                    new DbParam[]{
                   new DbParam("KYTHUAT_MA",ODT_LichChuyenGiao.KYTHUAT_MA),
                   new DbParam("KHOAHOC_ID",ODT_LichChuyenGiao.KHOAHOC_ID),
                   new DbParam("BENHVIEN_MA",ODT_LichChuyenGiao.BENHVIEN_MA),
                   new DbParam("LANHDAOBENHVIEN_ID",ODT_LichChuyenGiao.LANHDAOBENHVIEN_ID),
                   new DbParam("BACSY_ID",ODT_LichChuyenGiao.BACSY_ID),
                   new DbParam("BATDAU",ODT_LichChuyenGiao.BATDAU),
                   new DbParam("KETTHUC",ODT_LichChuyenGiao.KETTHUC),
                   new DbParam("COGIAYDIDUONG",ODT_LichChuyenGiao.COGIAYDIDUONG),
                   new DbParam("GIAYTO",ODT_LichChuyenGiao.GIAYTO),
                   new DbParam("NGUOITAO_ID",ODT_LichChuyenGiao.NGUOITAO_ID),
                   new DbParam("NGAYTAO",ODT_LichChuyenGiao.NGAYTAO),
                   new DbParam("NGUOISUA_ID",ODT_LichChuyenGiao.NGUOISUA_ID),
                   new DbParam("NGAYSUA",ODT_LichChuyenGiao.NGAYSUA),
                   new DbParam("TRANGTHAI",ODT_LichChuyenGiao.TRANGTHAI)
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
                                    " Delete from DT_KETQUACHUYENGIAO where ID=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DT_LICHCHUYENGIAOCHITIET where LICHCHUYENGIAO_ID=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DT_LICHCHUYENGIAO where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override DT_LichChuyenGiaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_LICHCHUYENGIAO where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_LichChuyenGiaoCls ODT_LichChuyenGiao = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_LichChuyenGiao = DT_LichChuyenGiaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_LichChuyenGiao;
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
                DT_LichChuyenGiaoCls ODT_LichChuyenGiao = CreateModel(ActionSqlParam, ID);
                ODT_LichChuyenGiao.ID = NewID;
                Add(ActionSqlParam, ODT_LichChuyenGiao);

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
