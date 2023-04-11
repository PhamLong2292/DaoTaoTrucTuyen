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
    public class DT_LichThucHanhProcessBll : DT_LichThucHanhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDT_LichThucHanhProcessBll";
            }
        }
        public override DT_LichThucHanhCls[] Reading(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhFilterCls ODT_LichThucHanhFilter)
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
                if (ODT_LichThucHanhFilter == null)
                {
                    ODT_LichThucHanhFilter = new DT_LichThucHanhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";

                Query = " select * from DT_LichThucHanh where 1=1 ";
                if (!string.IsNullOrEmpty(ODT_LichThucHanhFilter.KhoaHocId))
                {
                    ColDbParams.Add(new DbParam("KhoaHocId", ODT_LichThucHanhFilter.KhoaHocId));
                    Query += " and KHOAHOC_ID = " + ActionSqlParam.SpecialChar + "KhoaHocId ";
                }
                Query += " order by BATDAU";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DT_LichThucHanhCls[] DT_LichThucHanhs = DT_LichThucHanhParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_LichThucHanhs;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhCls ODT_LichThucHanh)
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
                if (string.IsNullOrEmpty(ODT_LichThucHanh.ID))
                {
                    ODT_LichThucHanh.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "DT_LichThucHanh",
                    new DbParam[]{
                    new DbParam("ID",ODT_LichThucHanh.ID),
                    new DbParam("KHOAHOC_ID",ODT_LichThucHanh.KHOAHOC_ID),
                    new DbParam("BATDAU",ODT_LichThucHanh.BATDAU),
                    new DbParam("KETTHUC",ODT_LichThucHanh.KETTHUC),
                    new DbParam("DIADIEM",ODT_LichThucHanh.DIADIEM),
                    new DbParam("NHOM",ODT_LichThucHanh.NHOM),
                    new DbParam("PTCHUYENMON_ID",ODT_LichThucHanh.PTCHUYENMON_ID),
                    new DbParam("LANHDAO_ID",ODT_LichThucHanh.LANHDAO_ID),
                    new DbParam("NGUOITAO_ID",ODT_LichThucHanh.NGUOITAO_ID),
                    new DbParam("NGAYTAO",ODT_LichThucHanh.NGAYTAO),
                    new DbParam("NGUOISUA_ID",ODT_LichThucHanh.NGUOISUA_ID),
                    new DbParam("NGAYSUA",ODT_LichThucHanh.NGAYSUA)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DT_LichThucHanhCls ODT_LichThucHanh)
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
                DBService.Update(ActionSqlParam.Trans, "DT_LichThucHanh", "ID", ID,
                    new DbParam[]{
                   new DbParam("KHOAHOC_ID",ODT_LichThucHanh.KHOAHOC_ID),
                   new DbParam("BATDAU",ODT_LichThucHanh.BATDAU),
                   new DbParam("KETTHUC",ODT_LichThucHanh.KETTHUC),
                   new DbParam("DIADIEM",ODT_LichThucHanh.DIADIEM),
                   new DbParam("NHOM",ODT_LichThucHanh.NHOM),
                   new DbParam("PTCHUYENMON_ID",ODT_LichThucHanh.PTCHUYENMON_ID),
                   new DbParam("LANHDAO_ID",ODT_LichThucHanh.LANHDAO_ID),
                   new DbParam("NGUOITAO_ID",ODT_LichThucHanh.NGUOITAO_ID),
                   new DbParam("NGAYTAO",ODT_LichThucHanh.NGAYTAO),
                   new DbParam("NGUOISUA_ID",ODT_LichThucHanh.NGUOISUA_ID),
                   new DbParam("NGAYSUA",ODT_LichThucHanh.NGAYSUA)
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
                string DelQuery = " BEGIN " +
                                    " Delete from DT_LichThucHanh_HocVien where LICHTHUCHANH_ID=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DT_LichThucHanhChiTiet where LICHTHUCHANH_ID=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DT_LichThucHanh where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override DT_LichThucHanhCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from DT_LichThucHanh where (ID=" + ActionSqlParam.SpecialChar + "ID OR KHOAHOC_ID=" + ActionSqlParam.SpecialChar + "ID)", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DT_LichThucHanhCls ODT_LichThucHanh = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODT_LichThucHanh = DT_LichThucHanhParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODT_LichThucHanh;
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
                DT_LichThucHanhCls ODT_LichThucHanh = CreateModel(ActionSqlParam, ID);
                ODT_LichThucHanh.ID = NewID;
                Add(ActionSqlParam, ODT_LichThucHanh);

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
        public override DT_HocVienCls[] GetHocViens(ActionSqlParamCls ActionSqlParam, string LichThucHanhId)
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
                if (string.IsNullOrEmpty(LichThucHanhId))
                {
                    return new DT_HocVienCls[0];
                }
                DbParam[] dbParams = new DbParam[] { new DbParam("LichThucHanhId", LichThucHanhId) };
                string Query = " select hv.* from DT_HocVien hv join DT_LICHTHUCHANH_HOCVIEN lthhv on hv.ID = lthhv.HOCVIEN_ID " +
                    " where lthhv.LICHTHUCHANH_ID = " + ActionSqlParam.SpecialChar + "LichThucHanhId";

                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, dbParams);
                DT_HocVienCls[] DT_HocViens = DT_HocVienParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DT_HocViens;
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
        public override int GetHocVienQuantity(ActionSqlParamCls ActionSqlParam, string LichThucHanhId)
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
                if (string.IsNullOrEmpty(LichThucHanhId))
                {
                    return 0;
                }
                DbParam[] dbParams = new DbParam[] { new DbParam("LichThucHanhId", LichThucHanhId) };
                string Query = " select count(1) from DT_HocVien hv join DT_LICHTHUCHANH_HOCVIEN lthhv on hv.ID = lthhv.HOCVIEN_ID " +
                    " where lthhv.LICHTHUCHANH_ID = " + ActionSqlParam.SpecialChar + "LichThucHanhId";

                int quantity = int.Parse(DBService.GetDataSet(ActionSqlParam.Trans, Query, dbParams).Tables[0].Rows[0][0].ToString());

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return quantity;
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
        public override void AddHocViens(ActionSqlParamCls ActionSqlParam, DT_LichThucHanhHocVienCls[] LichThucHanhHocViens)
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
                foreach (DT_LichThucHanhHocVienCls LichThucHanhHocVien in LichThucHanhHocViens)
                {
                    DBService.Insert(ActionSqlParam.Trans, "DT_LICHTHUCHANH_HOCVIEN",
                        new DbParam[]{
                            new DbParam("LICHTHUCHANH_ID",LichThucHanhHocVien.LICHTHUCHANH_ID),
                            new DbParam("HOCVIEN_ID",LichThucHanhHocVien.HOCVIEN_ID)
                        });
                }
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
        public override void RemoveHocViens(ActionSqlParamCls ActionSqlParam, string LichThucHanhId, string[] HocVienIds)
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
                if (string.IsNullOrEmpty(LichThucHanhId))
                {
                    return;
                }
                string sHocVienIds = string.Join("','", HocVienIds);
                if (!string.IsNullOrEmpty(sHocVienIds))
                {
                    sHocVienIds = "'" + sHocVienIds + "'";
                    DbParam[] dbParams = new DbParam[] { new DbParam("LichThucHanhId", LichThucHanhId) };
                    string Query = " delete from DT_LICHTHUCHANH_HOCVIEN " +
                        " where LICHTHUCHANH_ID = " + ActionSqlParam.SpecialChar + "LichThucHanhId" +
                        " and HOCVIEN_ID in (" + sHocVienIds + ")";

                    DBService.ExecuteNonQuery(ActionSqlParam.Trans, Query, dbParams);
                }
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
        public override void DeleteHocVien(ActionSqlParamCls ActionSqlParam, string LichThucHanhId, string HocVienId)
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
                if (string.IsNullOrEmpty(LichThucHanhId))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(HocVienId))
                {
                    DbParam[] dbParams = new DbParam[] { new DbParam("LichThucHanhId", LichThucHanhId) };
                    string Query = " delete from DT_LICHTHUCHANH_HOCVIEN " +
                        " where LICHTHUCHANH_ID = " + ActionSqlParam.SpecialChar + "LichThucHanhId" +
                        " and HOCVIEN_ID  = '" + HocVienId + "'";

                    DBService.ExecuteNonQuery(ActionSqlParam.Trans, Query, dbParams);
                }
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
    }
}
