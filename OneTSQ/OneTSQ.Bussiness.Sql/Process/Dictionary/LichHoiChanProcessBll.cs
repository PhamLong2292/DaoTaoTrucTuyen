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
    public class LichHoiChanProcessBll : LichHoiChanTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlLichHoiChanProcessBll";
            }
        }
        public override LichHoiChanCls[] Reading(ActionSqlParamCls ActionSqlParam, LichHoiChanFilterCls OLichHoiChanFilter)
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
                if (OLichHoiChanFilter == null)
                {
                    OLichHoiChanFilter = new LichHoiChanFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = "";
                if (!string.IsNullOrEmpty(OLichHoiChanFilter.CaBenhId))
                {
                    ColDbParams.Add(new DbParam("CaBenhId", OLichHoiChanFilter.CaBenhId));
                    Query = " select LICHHOICHAN.* from LICHHOICHAN " +
                        " where LICHHOICHAN.ID in (select LHCCB.LICHHOICHANID from LICHHOICHANCABENH LHCCB where LHCCB.CABENHID = " + ActionSqlParam.SpecialChar + "CaBenhId) ";
                }
                else
                    Query = " select LICHHOICHAN.* from LICHHOICHAN where 1=1 " + OLichHoiChanFilter.DataPermissionQuery;
                if (OLichHoiChanFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", OLichHoiChanFilter.TuNgay));
                    Query += " and LICHHOICHAN.BATDAU >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (OLichHoiChanFilter.DenNgay != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", OLichHoiChanFilter.DenNgay));
                    Query += " and LICHHOICHAN.BATDAU < " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }
                if (!string.IsNullOrEmpty(OLichHoiChanFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OLichHoiChanFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + OLichHoiChanFilter.Keyword.ToUpper() + "%"));
                    Query += " and (upper(LICHHOICHAN.DIADIEM) like " + ActionSqlParam.SpecialChar + "Keyword OR upper(LICHHOICHAN.GHICHU) like " + ActionSqlParam.SpecialChar + "Keyword1) ";
                }
                if (!string.IsNullOrEmpty(OLichHoiChanFilter.ChuyenKhoaMa))
                {
                    ColDbParams.Add(new DbParam("CHUYENKHOAMA", OLichHoiChanFilter.ChuyenKhoaMa));
                    Query += " and LICHHOICHAN.CHUYENKHOAMA = " + ActionSqlParam.SpecialChar + "CHUYENKHOAMA ";
                }
                if (!string.IsNullOrEmpty(OLichHoiChanFilter.ChuTriId))
                {
                    ColDbParams.Add(new DbParam("ChuTriId", OLichHoiChanFilter.ChuTriId));
                    Query += " and LICHHOICHAN.CHUTRI = " + ActionSqlParam.SpecialChar + "ChuTriId ";
                }
                if (OLichHoiChanFilter.TrangThai != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", OLichHoiChanFilter.TrangThai));
                    Query += " and LICHHOICHAN.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI";
                }
                if (OLichHoiChanFilter.TrangThais.Count() > 0)
                {
                    string trangThaiQuery = "";
                    for (int i = OLichHoiChanFilter.TrangThais.Count() - 1; i >= 0; i--)
                    {
                        trangThaiQuery += string.Format("{0},", OLichHoiChanFilter.TrangThais[i]);
                    }
                    Query += " and LICHHOICHAN.TRANGTHAI in ( " + trangThaiQuery.Substring(0, trangThaiQuery.Length - 1) + ") ";
                }
                Query += " order by LICHHOICHAN.BATDAU desc";

                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                LichHoiChanCls[] LichHoiChans = LichHoiChanParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return LichHoiChans;
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
        public override LichHoiChanCls[] PageReading(ActionSqlParamCls ActionSqlParam, LichHoiChanFilterCls OLichHoiChanFilter, ref long recordTotal)
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
                if (OLichHoiChanFilter == null)
                {
                    OLichHoiChanFilter = new LichHoiChanFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";
                Query = " select * from LICHHOICHAN where 1=1 " + OLichHoiChanFilter.DataPermissionQuery;
                recordTotalQuery = " select count(1) from LICHHOICHAN where 1=1 " + OLichHoiChanFilter.DataPermissionQuery;
                if (OLichHoiChanFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", OLichHoiChanFilter.TuNgay));
                    Query += " and BATDAU >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                    recordTotalQuery += " and BATDAU >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (OLichHoiChanFilter.DenNgay != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", OLichHoiChanFilter.DenNgay));
                    Query += " and BATDAU < " + ActionSqlParam.SpecialChar + "DENNGAY ";
                    recordTotalQuery += " and BATDAU < " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }
                if (!string.IsNullOrEmpty(OLichHoiChanFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + OLichHoiChanFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + OLichHoiChanFilter.Keyword.ToUpper() + "%"));
                    Query += " and (upper(DIADIEM) like " + ActionSqlParam.SpecialChar + "Keyword OR upper(GHICHU) like " + ActionSqlParam.SpecialChar + "Keyword1) ";
                    recordTotalQuery += " and (upper(DIADIEM) like " + ActionSqlParam.SpecialChar + "Keyword OR upper(GHICHU) like " + ActionSqlParam.SpecialChar + "Keyword1) ";
                }
                if (!string.IsNullOrEmpty(OLichHoiChanFilter.ChuyenKhoaMa))
                {
                    ColDbParams.Add(new DbParam("CHUYENKHOAMA", OLichHoiChanFilter.ChuyenKhoaMa));
                    Query += " and CHUYENKHOAMA = " + ActionSqlParam.SpecialChar + "CHUYENKHOAMA ";
                    recordTotalQuery += " and CHUYENKHOAMA = " + ActionSqlParam.SpecialChar + "CHUYENKHOAMA ";
                }
                if (!string.IsNullOrEmpty(OLichHoiChanFilter.ChuTriId))
                {
                    ColDbParams.Add(new DbParam("ChuTriId", OLichHoiChanFilter.ChuTriId));
                    Query += " and CHUTRI = " + ActionSqlParam.SpecialChar + "ChuTriId ";
                    recordTotalQuery += " and CHUTRI = " + ActionSqlParam.SpecialChar + "ChuTriId ";
                }
                if (OLichHoiChanFilter.TrangThai != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", OLichHoiChanFilter.TrangThai));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI";
                    recordTotalQuery += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI";
                }
                if (OLichHoiChanFilter.TrangThais.Count() > 0)
                {
                    string trangThaiQuery = "";
                    for (int i = OLichHoiChanFilter.TrangThais.Count() - 1; i >= 0; i--)
                    {
                        trangThaiQuery += string.Format("{0},", OLichHoiChanFilter.TrangThais[i]);
                    }
                    Query += " and TRANGTHAI in ( " + trangThaiQuery.Substring(0, trangThaiQuery.Length - 1) + ") ";
                    recordTotalQuery += " and TRANGTHAI in ( " + trangThaiQuery.Substring(0, trangThaiQuery.Length - 1) + ") ";
                }
                Query += " order by BATDAU desc " +
                    "OFFSET " + (OLichHoiChanFilter.PageIndex * OLichHoiChanFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + OLichHoiChanFilter.PageSize + " ROWS ONLY ";

                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                LichHoiChanCls[] LichHoiChans = LichHoiChanParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return LichHoiChans;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, LichHoiChanCls OLichHoiChan)
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
                if (string.IsNullOrEmpty(OLichHoiChan.ID))
                {
                    OLichHoiChan.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "LichHoiChan",
                    new DbParam[]{
                    new DbParam("ID",OLichHoiChan.ID),
                    new DbParam("BATDAU",OLichHoiChan.BATDAU),
                    new DbParam("KETTHUC",OLichHoiChan.KETTHUC),
                    new DbParam("DIADIEM",OLichHoiChan.DIADIEM),
                    new DbParam("THUKY",OLichHoiChan.THUKY),
                    new DbParam("CHUTRI",OLichHoiChan.CHUTRI),
                    new DbParam("TRANGTHAI",OLichHoiChan.TRANGTHAI),
                    new DbParam("TAOBOI",OLichHoiChan.TAOBOI),
                    new DbParam("TAOVAO",OLichHoiChan.TAOVAO),
                    new DbParam("GHICHU",OLichHoiChan.GHICHU),
                    new DbParam("DUYETBOI",OLichHoiChan.DUYETBOI),
                    new DbParam("DUYETVAO",OLichHoiChan.DUYETVAO),
                    new DbParam("CHUYENKHOAMA",OLichHoiChan.CHUYENKHOAMA)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, LichHoiChanCls OLichHoiChan)
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
                DBService.Update(ActionSqlParam.Trans, "LichHoiChan", "ID", ID,
                   new DbParam[]{
                   new DbParam("BATDAU",OLichHoiChan.BATDAU),
                   new DbParam("KETTHUC",OLichHoiChan.KETTHUC),
                   new DbParam("DIADIEM",OLichHoiChan.DIADIEM),
                   new DbParam("THUKY",OLichHoiChan.THUKY),
                   new DbParam("CHUTRI",OLichHoiChan.CHUTRI),
                   new DbParam("TRANGTHAI",OLichHoiChan.TRANGTHAI),
                   new DbParam("TAOBOI",OLichHoiChan.TAOBOI),
                   new DbParam("TAOVAO",OLichHoiChan.TAOVAO),
                   new DbParam("GHICHU",OLichHoiChan.GHICHU),
                   new DbParam("DUYETBOI",OLichHoiChan.DUYETBOI),
                   new DbParam("DUYETVAO",OLichHoiChan.DUYETVAO),
                   new DbParam("CHUYENKHOAMA",OLichHoiChan.CHUYENKHOAMA)
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
                                  " Delete from LichHoiChanCaBenh where LichHoiChanId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from LapLichTepDinhKem where LichHoiChanId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from LapLichThanhVienHoiChan where LichHoiChanId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from LichHoiChan where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override LichHoiChanCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from LICHHOICHAN where ID=" + ActionSqlParam.SpecialChar + "ID  ", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                LichHoiChanCls OLichHoiChan = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OLichHoiChan = LichHoiChanParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OLichHoiChan;
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
                LichHoiChanCls OLichHoiChan = CreateModel(ActionSqlParam, ID);
                OLichHoiChan.ID = NewID;
                Add(ActionSqlParam, OLichHoiChan);

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
        public override void AddChuyenGias(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] bacSyIds, string[] donViCongTacTens)
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
                string AddQuery = " DECLARE BEGIN ";
                for (int i = bacSyIds.Count() - 1; i >= 0; i--)
                {
                    BacSyCls bacSy = new BacSyProcessBll().CreateModel(ActionSqlParam, bacSyIds[i]);
                    if (bacSy != null)
                        AddQuery += string.Format(" Insert into LapLichThanhVienHoiChan(Id, LichHoiChanId, BacSyId, DONVICONGTACMA, DONVICONGTAC) values('{0}','{1}','{2}', '{3}', '{4}'); ", Guid.NewGuid().ToString(), lichHoiChanId, bacSyIds[i], bacSy.DONVIMA, donViCongTacTens[i]);
                }
                AddQuery += " END;";
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, AddQuery, new DbParam[] { });
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
        public override void RemoveChuyenGias(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] bacSyIds)
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
                string RemoveQuery = " DECLARE BEGIN ";
                foreach (string bacSyId in bacSyIds)
                    RemoveQuery += string.Format(" Delete from LapLichThanhVienHoiChan where LichHoiChanId='{0}' and BacSyId = '{1}'; ", lichHoiChanId, bacSyId);
                RemoveQuery += " END;";
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, RemoveQuery, new DbParam[] { });
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
        public override void AddCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] caBenhIds)
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
                string AddQuery = " DECLARE BEGIN ";
                foreach (string caBenhId in caBenhIds)
                    AddQuery += string.Format(" Insert into LichHoiChanCaBenh(LichHoiChanId, CaBenhId) values('{0}','{1}'); "
                        + " update CaBenh set LICHHOICHANID = '{0}', TrangThai = {2} where id = '{1}'; ", lichHoiChanId, caBenhId, (int)CaBenhCls.eTrangThai.DaLapLich);
                AddQuery += " END;";
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, AddQuery, new DbParam[] { });
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
        public override void RemoveCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId, string[] caBenhIds)
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
                string RemoveQuery = " DECLARE BEGIN ";
                foreach (string caBenhId in caBenhIds)
                    RemoveQuery += string.Format(" Delete from LichHoiChanCaBenh where LichHoiChanId='{0}' and CaBenhId = '{1}'; "
                        + " update CaBenh set LICHHOICHANID = null, TrangThai = {2} where id = '{1}'; ", lichHoiChanId, caBenhId, (int)CaBenhCls.eTrangThai.ChoLapLich);
                RemoveQuery += " END;";
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, RemoveQuery, new DbParam[] { });
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
        public override void UpdateCaBenhStt(ActionSqlParamCls ActionSqlParam, LichHoiChanCaBenhCls[] lichHoiChanCaBenhs)
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
                string UpdateQuery = " DECLARE BEGIN ";
                foreach (LichHoiChanCaBenhCls lichHoiChanCaBenh in lichHoiChanCaBenhs)
                    UpdateQuery += " update LichHoiChanCaBenh set STT = " + lichHoiChanCaBenh.STT +
                                   " where LICHHOICHANID = '" + lichHoiChanCaBenh.LICHHOICHANID + "' " +
                                   " and CABENHID = '" + lichHoiChanCaBenh.CABENHID + "'; ";
                UpdateQuery += " END;";
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, UpdateQuery, new DbParam[] { });
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
        public override LichHoiChanCaBenhCls[] GetLichHoiChanCaBenhs(ActionSqlParamCls ActionSqlParam, string lichHoiChanId)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;

            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                string Query = " select * from LICHHOICHANCABENH where LICHHOICHANID= " + ActionSqlParam.SpecialChar + "LICHHOICHANID  order by STT";
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>() { new DbParam("LICHHOICHANID", lichHoiChanId) };

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                LichHoiChanCaBenhCls[] lichHoiChanCaBenhs = LichHoiChanCaBenhParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return lichHoiChanCaBenhs;
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
        //Kiểm tra bác sỹ là thuộc bệnh viện trực tiếp hay dự thính trong phiên hội chẩn
        public override bool IsTrucTiepHoiChan(ActionSqlParamCls ActionSqlParam, string LichHoiChanId, string BacSyId)
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
                string Query =
                    " select count(*) from LichHoiChanCaBenh lhccb join CaBenh cb on lhccb.cabenhid = cb.id " +
                    "                                               join TableOwner tbo on tbo.ownerid = cb.donvithamvanid " +
                    "                                               join BacSy bs on tbo.ownercode = bs.donvima " +
                    " where bs.id = " + ActionSqlParam.SpecialChar + "BacSyId " +
                    "       and lhccb.lichhoichanid = " + ActionSqlParam.SpecialChar + "LichHoiChanId ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>() {
                    new DbParam("BacSyId", BacSyId),
                    new DbParam("LichHoiChanId", LichHoiChanId)
                };
                return int.Parse(DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString()) > 0;
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
