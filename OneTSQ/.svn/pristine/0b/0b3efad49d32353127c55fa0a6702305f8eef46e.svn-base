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
    public class PhieuPhanTichNguyenNhanSuCoProcessBll : PhieuPhanTichNguyenNhanSuCoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlPhieuPhanTichNguyenNhanSuCoProcessBll";
            }
        }
        public override PhieuPhanTichNguyenNhanSuCoCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter)
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
                if (OPhieuPhanTichNguyenNhanSuCoFilter == null)
                {
                    OPhieuPhanTichNguyenNhanSuCoFilter = new PhieuPhanTichNguyenNhanSuCoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select * from PhieuPhanTichNguyenNhanSuCo where 1=1 ";
                if (!string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCoFilter.ChucDanh_ID))
                {
                    ColDbParams.Add(new DbParam("CHUCDANH_ID", OPhieuPhanTichNguyenNhanSuCoFilter.ChucDanh_ID));
                    Query += " and CHUCDANH_ID = " + ActionSqlParam.SpecialChar + "CHUCDANH_ID ";
                }
                if (!string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCoFilter.NguoiLap_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOILAP_ID", OPhieuPhanTichNguyenNhanSuCoFilter.NguoiLap_ID));
                    Query += " and NGUOILAP_ID = " + ActionSqlParam.SpecialChar + "NGUOILAP_ID ";
                }               
                if (OPhieuPhanTichNguyenNhanSuCoFilter.TrangThai != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", OPhieuPhanTichNguyenNhanSuCoFilter.TrangThai));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                Query += " order by NGAYDANGKY";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                PhieuPhanTichNguyenNhanSuCoCls[] PhieuPhanTichNguyenNhanSuCos = PhieuPhanTichNguyenNhanSuCoParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return PhieuPhanTichNguyenNhanSuCos;
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
        public override PhieuPhanTichNguyenNhanSuCoCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter, ref long recordTotal)
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
                if (OPhieuPhanTichNguyenNhanSuCoFilter == null)
                {
                    OPhieuPhanTichNguyenNhanSuCoFilter = new PhieuPhanTichNguyenNhanSuCoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";

                Query = " select * from PhieuPhanTichNguyenNhanSuCo Where 1=1 ";
                recordTotalQuery = " select count(1) from PhieuPhanTichNguyenNhanSuCo Where 1=1 ";
                if (!string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCoFilter.ChucDanh_ID))
                {
                    ColDbParams.Add(new DbParam("CHUCDANH_ID)", OPhieuPhanTichNguyenNhanSuCoFilter.ChucDanh_ID));
                    Query += " and CHUCDANH_ID = " + ActionSqlParam.SpecialChar + "CHUCDANH_ID ";
                    recordTotalQuery += " and CHUCDANH_ID = " + ActionSqlParam.SpecialChar + "CHUCDANH_ID ";
                }
                if (!string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCoFilter.NguoiLap_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOILAP_ID", OPhieuPhanTichNguyenNhanSuCoFilter.NguoiLap_ID));
                    Query += " and NGUOILAP_ID = " + ActionSqlParam.SpecialChar + "NGUOILAP_ID ";
                    recordTotalQuery += " and NGUOILAP_ID = " + ActionSqlParam.SpecialChar + "NGUOILAP_ID ";
                }              
                if (!string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCoFilter.Keyword))
                {
                    Query += " and ( SOBAOCAO like '%" + OPhieuPhanTichNguyenNhanSuCoFilter.Keyword + "%')";
                    recordTotalQuery += " and ( SOBAOCAO like '%" + OPhieuPhanTichNguyenNhanSuCoFilter.Keyword + "%')";
                }              
                if (OPhieuPhanTichNguyenNhanSuCoFilter.TrangThai != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", OPhieuPhanTichNguyenNhanSuCoFilter.TrangThai));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                    recordTotalQuery += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                Query += " ORDER BY THOIGIANLAP " +
                " OFFSET " + (OPhieuPhanTichNguyenNhanSuCoFilter.PageIndex * OPhieuPhanTichNguyenNhanSuCoFilter.PageSize) + " ROWS " +
                " FETCH NEXT " + OPhieuPhanTichNguyenNhanSuCoFilter.PageSize + " ROWS ONLY ";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                PhieuPhanTichNguyenNhanSuCoCls[] PhieuPhanTichNguyenNhanSuCos = PhieuPhanTichNguyenNhanSuCoParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return PhieuPhanTichNguyenNhanSuCos;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo)
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
                if (string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCo.ID))
                {
                    OPhieuPhanTichNguyenNhanSuCo.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "PhieuPhanTichNguyenNhanSuCo",
                    new DbParam[]{
                    new DbParam("ID",OPhieuPhanTichNguyenNhanSuCo.ID),
                    new DbParam("SOBAOCAO",OPhieuPhanTichNguyenNhanSuCo.SOBAOCAO),
                    new DbParam("NGUOILAP_ID",OPhieuPhanTichNguyenNhanSuCo.NGUOILAP_ID),
                    new DbParam("CHUCDANH_ID",OPhieuPhanTichNguyenNhanSuCo.CHUCDANH_ID),
                    new DbParam("THOIGIANLAP",OPhieuPhanTichNguyenNhanSuCo.THOIGIANLAP),
                    new DbParam("MOTASUCO",OPhieuPhanTichNguyenNhanSuCo.MOTASUCO),
                    new DbParam("THUCHIENQTKT",OPhieuPhanTichNguyenNhanSuCo.THUCHIENQTKT),
                    new DbParam("NHIEMKHUANBENHVIEN",OPhieuPhanTichNguyenNhanSuCo.NHIEMKHUANBENHVIEN),
                    new DbParam("THUOCDICHTRUYEN",OPhieuPhanTichNguyenNhanSuCo.THUOCDICHTRUYEN),
                    new DbParam("CHEPHAMMAU",OPhieuPhanTichNguyenNhanSuCo.CHEPHAMMAU),
                    new DbParam("THIETBIYTE",OPhieuPhanTichNguyenNhanSuCo.THIETBIYTE),
                    new DbParam("HANHVI",OPhieuPhanTichNguyenNhanSuCo.HANHVI),
                    new DbParam("TAINANNGUOIBENH",OPhieuPhanTichNguyenNhanSuCo.TAINANNGUOIBENH),
                    new DbParam("HATANGCOSO",OPhieuPhanTichNguyenNhanSuCo.HATANGCOSO),
                    new DbParam("QLNGUONLUCTC",OPhieuPhanTichNguyenNhanSuCo.QLNGUONLUCTC),
                    new DbParam("HSTHUTUCHANHCHINH",OPhieuPhanTichNguyenNhanSuCo.HSTHUTUCHANHCHINH),
                    new DbParam("KHAC",OPhieuPhanTichNguyenNhanSuCo.KHAC),
                    new DbParam("DTYLDUOCTHUCHIEN",OPhieuPhanTichNguyenNhanSuCo.DTYLDUOCTHUCHIEN),
                    new DbParam("NNNHANVIEN",OPhieuPhanTichNguyenNhanSuCo.NNNHANVIEN),
                    new DbParam("NNNGUOIBENH",OPhieuPhanTichNguyenNhanSuCo.NNNGUOIBENH),
                    new DbParam("NNMOITRUONGLAMVIEC",OPhieuPhanTichNguyenNhanSuCo.NNMOITRUONGLAMVIEC),
                    new DbParam("NNTOCHUCDICHVU",OPhieuPhanTichNguyenNhanSuCo.NNTOCHUCDICHVU),
                    new DbParam("NNYEUTOBENNGOAI",OPhieuPhanTichNguyenNhanSuCo.NNYEUTOBENNGOAI),
                    new DbParam("NNKHAC",OPhieuPhanTichNguyenNhanSuCo.NNKHAC),
                    new DbParam("KHACPHUCSUCO",OPhieuPhanTichNguyenNhanSuCo.KHACPHUCSUCO),
                    new DbParam("DEXUATKHUYENCAO",OPhieuPhanTichNguyenNhanSuCo.DEXUATKHUYENCAO),
                    new DbParam("MOTAKETQUA",OPhieuPhanTichNguyenNhanSuCo.MOTAKETQUA),
                    new DbParam("THAOLUANKHUYENCAO",OPhieuPhanTichNguyenNhanSuCo.THAOLUANKHUYENCAO),
                    new DbParam("PHUHOPKHUYENCAO",OPhieuPhanTichNguyenNhanSuCo.PHUHOPKHUYENCAO),
                    new DbParam("TONGTHUONGNBNC0",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC0),
                    new DbParam("TONGTHUONGNBNC1",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC1),
                    new DbParam("TONGTHUONGNBNC2",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC2),
                    new DbParam("TONGTHUONGNBNC3",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC3),
                    new DbParam("DANHGIATRENTOCHUC",OPhieuPhanTichNguyenNhanSuCo.DANHGIATRENTOCHUC),
                    new DbParam("TRANGTHAI",OPhieuPhanTichNguyenNhanSuCo.TRANGTHAI)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo)
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
                DBService.Update(ActionSqlParam.Trans, "PhieuPhanTichNguyenNhanSuCo", "ID", ID,
                    new DbParam[]{
                    new DbParam("SOBAOCAO",OPhieuPhanTichNguyenNhanSuCo.SOBAOCAO),
                    new DbParam("NGUOILAP_ID",OPhieuPhanTichNguyenNhanSuCo.NGUOILAP_ID),
                    new DbParam("CHUCDANH_ID",OPhieuPhanTichNguyenNhanSuCo.CHUCDANH_ID),
                    new DbParam("THOIGIANLAP",OPhieuPhanTichNguyenNhanSuCo.THOIGIANLAP),
                    new DbParam("MOTASUCO",OPhieuPhanTichNguyenNhanSuCo.MOTASUCO),
                    new DbParam("THUCHIENQTKT",OPhieuPhanTichNguyenNhanSuCo.THUCHIENQTKT),
                    new DbParam("NHIEMKHUANBENHVIEN",OPhieuPhanTichNguyenNhanSuCo.NHIEMKHUANBENHVIEN),
                    new DbParam("THUOCDICHTRUYEN",OPhieuPhanTichNguyenNhanSuCo.THUOCDICHTRUYEN),
                    new DbParam("CHEPHAMMAU",OPhieuPhanTichNguyenNhanSuCo.CHEPHAMMAU),
                    new DbParam("THIETBIYTE",OPhieuPhanTichNguyenNhanSuCo.THIETBIYTE),
                    new DbParam("HANHVI",OPhieuPhanTichNguyenNhanSuCo.HANHVI),
                    new DbParam("TAINANNGUOIBENH",OPhieuPhanTichNguyenNhanSuCo.TAINANNGUOIBENH),
                    new DbParam("HATANGCOSO",OPhieuPhanTichNguyenNhanSuCo.HATANGCOSO),
                    new DbParam("QLNGUONLUCTC",OPhieuPhanTichNguyenNhanSuCo.QLNGUONLUCTC),
                    new DbParam("HSTHUTUCHANHCHINH",OPhieuPhanTichNguyenNhanSuCo.HSTHUTUCHANHCHINH),
                    new DbParam("KHAC",OPhieuPhanTichNguyenNhanSuCo.KHAC),
                    new DbParam("DTYLDUOCTHUCHIEN",OPhieuPhanTichNguyenNhanSuCo.DTYLDUOCTHUCHIEN),
                    new DbParam("NNNHANVIEN",OPhieuPhanTichNguyenNhanSuCo.NNNHANVIEN),
                    new DbParam("NNNGUOIBENH",OPhieuPhanTichNguyenNhanSuCo.NNNGUOIBENH),
                    new DbParam("NNMOITRUONGLAMVIEC",OPhieuPhanTichNguyenNhanSuCo.NNMOITRUONGLAMVIEC),
                    new DbParam("NNTOCHUCDICHVU",OPhieuPhanTichNguyenNhanSuCo.NNTOCHUCDICHVU),
                    new DbParam("NNYEUTOBENNGOAI",OPhieuPhanTichNguyenNhanSuCo.NNYEUTOBENNGOAI),
                    new DbParam("NNKHAC",OPhieuPhanTichNguyenNhanSuCo.NNKHAC),
                    new DbParam("KHACPHUCSUCO",OPhieuPhanTichNguyenNhanSuCo.KHACPHUCSUCO),
                    new DbParam("DEXUATKHUYENCAO",OPhieuPhanTichNguyenNhanSuCo.DEXUATKHUYENCAO),
                    new DbParam("MOTAKETQUA",OPhieuPhanTichNguyenNhanSuCo.MOTAKETQUA),
                    new DbParam("THAOLUANKHUYENCAO",OPhieuPhanTichNguyenNhanSuCo.THAOLUANKHUYENCAO),
                    new DbParam("PHUHOPKHUYENCAO",OPhieuPhanTichNguyenNhanSuCo.PHUHOPKHUYENCAO),
                    new DbParam("TONGTHUONGNBNC0",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC0),
                    new DbParam("TONGTHUONGNBNC1",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC1),
                    new DbParam("TONGTHUONGNBNC2",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC2),
                    new DbParam("TONGTHUONGNBNC3",OPhieuPhanTichNguyenNhanSuCo.TONGTHUONGNBNC3),
                    new DbParam("DANHGIATRENTOCHUC",OPhieuPhanTichNguyenNhanSuCo.DANHGIATRENTOCHUC),
                    new DbParam("TRANGTHAI",OPhieuPhanTichNguyenNhanSuCo.TRANGTHAI)
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
                string DelQuery = " Delete from PhieuPhanTichNguyenNhanSuCo where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override PhieuPhanTichNguyenNhanSuCoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from PhieuPhanTichNguyenNhanSuCo where (ID =" + ActionSqlParam.SpecialChar + "ID or SOBAOCAO =" + ActionSqlParam.SpecialChar + "ID)", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OPhieuPhanTichNguyenNhanSuCo = PhieuPhanTichNguyenNhanSuCoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OPhieuPhanTichNguyenNhanSuCo;
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
                PhieuPhanTichNguyenNhanSuCoCls OPhieuPhanTichNguyenNhanSuCo = CreateModel(ActionSqlParam, ID);
                OPhieuPhanTichNguyenNhanSuCo.ID = NewID;
                Add(ActionSqlParam, OPhieuPhanTichNguyenNhanSuCo);

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
        public override long Count(ActionSqlParamCls ActionSqlParam, PhieuPhanTichNguyenNhanSuCoFilterCls OPhieuPhanTichNguyenNhanSuCoFilter)
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
                if (OPhieuPhanTichNguyenNhanSuCoFilter == null)
                {
                    OPhieuPhanTichNguyenNhanSuCoFilter = new PhieuPhanTichNguyenNhanSuCoFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select count(1) from PhieuPhanTichNguyenNhanSuCo";
                if (!string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCoFilter.ChucDanh_ID))
                {
                    ColDbParams.Add(new DbParam("CHUCDANH_ID", OPhieuPhanTichNguyenNhanSuCoFilter.ChucDanh_ID));
                    Query += " and CHUCDANH_ID = " + ActionSqlParam.SpecialChar + "CHUCDANH_ID ";
                }
                if (!string.IsNullOrEmpty(OPhieuPhanTichNguyenNhanSuCoFilter.NguoiLap_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOILAP_ID", OPhieuPhanTichNguyenNhanSuCoFilter.NguoiLap_ID));
                    Query += " and NGUOILAP_ID = " + ActionSqlParam.SpecialChar + "NGUOILAP_ID";
                }              
                if (OPhieuPhanTichNguyenNhanSuCoFilter.TrangThai != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", OPhieuPhanTichNguyenNhanSuCoFilter.TrangThai));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                if (OPhieuPhanTichNguyenNhanSuCoFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", OPhieuPhanTichNguyenNhanSuCoFilter.TuNgay));
                    Query += " and THOIGIANLAP >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (OPhieuPhanTichNguyenNhanSuCoFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", OPhieuPhanTichNguyenNhanSuCoFilter.TuNgay));
                    Query += " and THOIGIANLAP < " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }              
                long result = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return result;
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
