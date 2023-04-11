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
    public class DanhGiaDeCuong_DeTaiProcessBll : DanhGiaDeCuong_DeTaiTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDanhGiaDeCuong_DeTaiProcessBll";
            }
        }
        public override DanhGiaDeCuong_DeTaiCls[] Reading(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter)
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
                if (ODanhGiaDeCuong_DeTaiFilter == null)
                {
                    ODanhGiaDeCuong_DeTaiFilter = new DanhGiaDeCuong_DeTaiFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select * from NCKH_DANHGIADECUONG_DETAI where 1=1 ";
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.NGUOIDANHGIA_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOIDANHGIA_ID", ODanhGiaDeCuong_DeTaiFilter.NGUOIDANHGIA_ID));
                    Query += " and NGUOIDANHGIA_ID = " + ActionSqlParam.SpecialChar + "NGUOIDANHGIA_ID ";
                }
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.DECUONG_ID))
                {
                    ColDbParams.Add(new DbParam("DECUONG_ID", ODanhGiaDeCuong_DeTaiFilter.DECUONG_ID));
                    Query += " and DECUONG_ID = " + ActionSqlParam.SpecialChar + "DECUONG_ID ";
                }
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.DETAI_ID))
                {
                    ColDbParams.Add(new DbParam("DETAI_ID", ODanhGiaDeCuong_DeTaiFilter.DETAI_ID));
                    Query += " and DETAI_ID = " + ActionSqlParam.SpecialChar + "DETAI_ID ";
                }              
                Query += " order by NGAYTAO";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DanhGiaDeCuong_DeTaiCls[] DanhGiaDeCuong_DeTais = DanhGiaDeCuong_DeTaiParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DanhGiaDeCuong_DeTais;
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
        public override DanhGiaDeCuong_DeTaiCls[] PageReading(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter, ref long recordTotal)
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
                if (ODanhGiaDeCuong_DeTaiFilter == null)
                {
                    ODanhGiaDeCuong_DeTaiFilter = new DanhGiaDeCuong_DeTaiFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";

                Query = " select * from NCKH_DANHGIADECUONG_DETAI Where 1=1 ";
                recordTotalQuery = " select count(1) from NCKH_DANHGIADECUONG_DETAI Where 1=1 ";
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.NGUOIDANHGIA_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOIDANHGIA_ID", ODanhGiaDeCuong_DeTaiFilter.NGUOIDANHGIA_ID));
                    Query += " and NGUOIDANHGIA_ID = " + ActionSqlParam.SpecialChar + "NGUOIDANHGIA_ID ";
                    recordTotalQuery += " and NGUOIDANHGIA_ID = " + ActionSqlParam.SpecialChar + "NGUOIDANHGIA_ID ";
                }
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.DECUONG_ID))
                {
                    ColDbParams.Add(new DbParam("DECUONG_ID", ODanhGiaDeCuong_DeTaiFilter.DECUONG_ID));
                    Query += " and DECUONG_ID = " + ActionSqlParam.SpecialChar + "DECUONG_ID ";
                    recordTotalQuery += " and DECUONG_ID = " + ActionSqlParam.SpecialChar + "DECUONG_ID ";
                }
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.DETAI_ID))
                {
                    ColDbParams.Add(new DbParam("DETAI_ID", ODanhGiaDeCuong_DeTaiFilter.DETAI_ID));
                    Query += " and DETAI_ID = " + ActionSqlParam.SpecialChar + "DETAI_ID ";
                    recordTotalQuery += " and DETAI_ID = " + ActionSqlParam.SpecialChar + "DETAI_ID ";
                }
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.Keyword))
                {
                    Query += " and (UPPER(DIEM) like UPPER('%" + ODanhGiaDeCuong_DeTaiFilter.Keyword + "%') OR UPPER(DANHGIA) like UPPER(N'%" + ODanhGiaDeCuong_DeTaiFilter.Keyword + "%'))";
                    recordTotalQuery += " and (UPPER(DIEM) like UPPER('%" + ODanhGiaDeCuong_DeTaiFilter.Keyword + "%') OR UPPER(DANHGIA) like UPPER(N'%" + ODanhGiaDeCuong_DeTaiFilter.Keyword + "%'))";
                }       
                Query += " ORDER BY NGAYTAO " +
                " OFFSET " + (ODanhGiaDeCuong_DeTaiFilter.PageIndex * ODanhGiaDeCuong_DeTaiFilter.PageSize) + " ROWS " +
                " FETCH NEXT " + ODanhGiaDeCuong_DeTaiFilter.PageSize + " ROWS ONLY ";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                DanhGiaDeCuong_DeTaiCls[] DanhGiaDeCuong_DeTais = DanhGiaDeCuong_DeTaiParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return DanhGiaDeCuong_DeTais;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai)
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
                if (string.IsNullOrEmpty(ODanhGiaDeCuong_DeTai.ID))
                {
                    ODanhGiaDeCuong_DeTai.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "NCKH_DANHGIADECUONG_DETAI",
                    new DbParam[]{
                    new DbParam("ID",ODanhGiaDeCuong_DeTai.ID),
                    new DbParam("NGUOIDANHGIA_ID",ODanhGiaDeCuong_DeTai.NGUOIDANHGIA_ID),
                    new DbParam("NGAYTAO",ODanhGiaDeCuong_DeTai.NGAYTAO),
                    new DbParam("DIEM",ODanhGiaDeCuong_DeTai.DIEM),
                    new DbParam("DANHGIA",ODanhGiaDeCuong_DeTai.DANHGIA),
                    new DbParam("YKIENKHAC",ODanhGiaDeCuong_DeTai.YKIENKHAC),
                    new DbParam("DECUONG_ID",ODanhGiaDeCuong_DeTai.DECUONG_ID),
                    new DbParam("DETAI_ID",ODanhGiaDeCuong_DeTai.DETAI_ID),
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai)
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
                DBService.Update(ActionSqlParam.Trans, "NCKH_DANHGIADECUONG_DETAI", "ID", ID,
                    new DbParam[]{
                    new DbParam("NGUOIDANHGIA_ID",ODanhGiaDeCuong_DeTai.NGUOIDANHGIA_ID),
                    new DbParam("NGAYTAO",ODanhGiaDeCuong_DeTai.NGAYTAO),
                    new DbParam("DIEM",ODanhGiaDeCuong_DeTai.DIEM),
                    new DbParam("DANHGIA",ODanhGiaDeCuong_DeTai.DANHGIA),
                    new DbParam("YKIENKHAC",ODanhGiaDeCuong_DeTai.YKIENKHAC),
                    new DbParam("DECUONG_ID",ODanhGiaDeCuong_DeTai.DECUONG_ID),
                    new DbParam("DETAI_ID",ODanhGiaDeCuong_DeTai.DETAI_ID),
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
                string DelQuery = " Delete from NCKH_DANHGIADECUONG_DETAI where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override DanhGiaDeCuong_DeTaiCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from NCKH_DANHGIADECUONG_DETAI where ID =" + ActionSqlParam.SpecialChar + "ID", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    ODanhGiaDeCuong_DeTai = DanhGiaDeCuong_DeTaiParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return ODanhGiaDeCuong_DeTai;
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
                DanhGiaDeCuong_DeTaiCls ODanhGiaDeCuong_DeTai = CreateModel(ActionSqlParam, ID);
                ODanhGiaDeCuong_DeTai.ID = NewID;
                Add(ActionSqlParam, ODanhGiaDeCuong_DeTai);

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
        public override long Count(ActionSqlParamCls ActionSqlParam, DanhGiaDeCuong_DeTaiFilterCls ODanhGiaDeCuong_DeTaiFilter)
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
                if (ODanhGiaDeCuong_DeTaiFilter == null)
                {
                    ODanhGiaDeCuong_DeTaiFilter = new DanhGiaDeCuong_DeTaiFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select count(1) from NCKH_DANHGIADECUONG_DETAI";
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.NGUOIDANHGIA_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOIDANHGIA_ID", ODanhGiaDeCuong_DeTaiFilter.NGUOIDANHGIA_ID));
                    Query += " and NGUOIDANHGIA_ID = " + ActionSqlParam.SpecialChar + "NGUOIDANHGIA_ID ";
                }
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.DECUONG_ID))
                {
                    ColDbParams.Add(new DbParam("DECUONG_ID", ODanhGiaDeCuong_DeTaiFilter.DECUONG_ID));
                    Query += " and DECUONG_ID = " + ActionSqlParam.SpecialChar + "DECUONG_ID";
                }
                if (!string.IsNullOrEmpty(ODanhGiaDeCuong_DeTaiFilter.DETAI_ID))
                {
                    ColDbParams.Add(new DbParam("DETAI_ID", ODanhGiaDeCuong_DeTaiFilter.DETAI_ID));
                    Query += " and DETAI_ID = " + ActionSqlParam.SpecialChar + "DETAI_ID";
                }        
                if (ODanhGiaDeCuong_DeTaiFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", ODanhGiaDeCuong_DeTaiFilter.TuNgay));
                    Query += " and NGAYTAO >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (ODanhGiaDeCuong_DeTaiFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", ODanhGiaDeCuong_DeTaiFilter.TuNgay));
                    Query += " and NGAYTAO < " + ActionSqlParam.SpecialChar + "DENNGAY ";
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
