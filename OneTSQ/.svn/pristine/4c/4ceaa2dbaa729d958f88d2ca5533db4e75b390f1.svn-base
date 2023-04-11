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
    public class TaiLieuDinhKemProcessBll : TaiLieuDinhKemTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlTaiLieuDinhKemProcessBll";
            }
        }
        public override TaiLieuDinhKemCls[] Reading(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter)
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
                if (OTaiLieuDinhKemFilter == null)
                {
                    OTaiLieuDinhKemFilter = new TaiLieuDinhKemFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select * from TAILIEUDINHKEM where 1=1 ";
                if (!string.IsNullOrEmpty(OTaiLieuDinhKemFilter.DOCUMENT_ID))
                {
                    ColDbParams.Add(new DbParam("DOCUMENT_ID", OTaiLieuDinhKemFilter.DOCUMENT_ID));
                    Query += " and DOCUMENT_ID = " + ActionSqlParam.SpecialChar + "DOCUMENT_ID ";
                }
                if (!string.IsNullOrEmpty(OTaiLieuDinhKemFilter.NGUOITAO_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOITAO_ID", OTaiLieuDinhKemFilter.NGUOITAO_ID));
                    Query += " and NGUOITAO_ID = " + ActionSqlParam.SpecialChar + "NGUOITAO_ID ";
                }            
                Query += " order by NGAYTAO";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TaiLieuDinhKemCls[] TaiLieuDinhKems = TaiLieuDinhKemParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return TaiLieuDinhKems;
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
        public override TaiLieuDinhKemCls[] PageReading(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter, ref long recordTotal)
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
                if (OTaiLieuDinhKemFilter == null)
                {
                    OTaiLieuDinhKemFilter = new TaiLieuDinhKemFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";

                Query = " select * from TAILIEUDINHKEM Where 1=1 ";
                recordTotalQuery = " select count(1) from TAILIEUDINHKEM Where 1=1 ";
                if (!string.IsNullOrEmpty(OTaiLieuDinhKemFilter.DOCUMENT_ID))
                {
                    ColDbParams.Add(new DbParam("DOCUMENT_ID", OTaiLieuDinhKemFilter.DOCUMENT_ID));
                    Query += " and DOCUMENT_ID = " + ActionSqlParam.SpecialChar + "DOCUMENT_ID ";
                    recordTotalQuery += " and DOCUMENT_ID = " + ActionSqlParam.SpecialChar + "DOCUMENT_ID ";
                }
                if (!string.IsNullOrEmpty(OTaiLieuDinhKemFilter.NGUOITAO_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOITAO_ID", OTaiLieuDinhKemFilter.NGUOITAO_ID));
                    Query += " and NGUOITAO_ID = " + ActionSqlParam.SpecialChar + "NGUOITAO_ID ";
                    recordTotalQuery += " and NGUOITAO_ID = " + ActionSqlParam.SpecialChar + "NGUOITAO_ID ";
                }             
                if (!string.IsNullOrEmpty(OTaiLieuDinhKemFilter.Keyword))
                {
                    Query += " and (UPPER(DUONGDAN) like UPPER('%" + OTaiLieuDinhKemFilter.Keyword + "%') OR UPPER(GHICHU) like UPPER(N'%" + OTaiLieuDinhKemFilter.Keyword + "%'))";
                    recordTotalQuery += " and (UPPER(DUONGDAN) like UPPER('%" + OTaiLieuDinhKemFilter.Keyword + "%') OR UPPER(GHICHU) like UPPER(N'%" + OTaiLieuDinhKemFilter.Keyword + "%'))";
                }         
                Query += " ORDER BY NGAYTAO " +
                " OFFSET " + (OTaiLieuDinhKemFilter.PageIndex * OTaiLieuDinhKemFilter.PageSize) + " ROWS " +
                " FETCH NEXT " + OTaiLieuDinhKemFilter.PageSize + " ROWS ONLY ";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TaiLieuDinhKemCls[] TaiLieuDinhKems = TaiLieuDinhKemParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return TaiLieuDinhKems;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemCls OTaiLieuDinhKem)
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
                if (string.IsNullOrEmpty(OTaiLieuDinhKem.ID))
                {
                    OTaiLieuDinhKem.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "TAILIEUDINHKEM",
                    new DbParam[]{
                    new DbParam("ID",OTaiLieuDinhKem.ID),
                    new DbParam("DOCUMENT_ID",OTaiLieuDinhKem.DOCUMENT_ID),
                    new DbParam("TENTAILIEU",OTaiLieuDinhKem.TENTAILIEU),
                    new DbParam("TENHIENTHI",OTaiLieuDinhKem.TENHIENTHI),
                    new DbParam("DUONGDAN",OTaiLieuDinhKem.DUONGDAN),
                    new DbParam("NGUOITAO_ID",OTaiLieuDinhKem.NGUOITAO_ID),
                    new DbParam("NGAYTAO",OTaiLieuDinhKem.NGAYTAO),
                    new DbParam("GHICHU",OTaiLieuDinhKem.GHICHU),      
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, TaiLieuDinhKemCls OTaiLieuDinhKem)
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
                DBService.Update(ActionSqlParam.Trans, "TAILIEUDINHKEM", "ID", ID,
                    new DbParam[]{
                    new DbParam("DOCUMENT_ID",OTaiLieuDinhKem.DOCUMENT_ID),
                    new DbParam("TENTAILIEU",OTaiLieuDinhKem.TENTAILIEU),
                    new DbParam("TENHIENTHI",OTaiLieuDinhKem.TENHIENTHI),
                    new DbParam("DUONGDAN",OTaiLieuDinhKem.DUONGDAN),
                    new DbParam("NGUOITAO_ID",OTaiLieuDinhKem.NGUOITAO_ID),
                    new DbParam("NGAYTAO",OTaiLieuDinhKem.NGAYTAO),
                    new DbParam("GHICHU",OTaiLieuDinhKem.GHICHU),
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
                string DelQuery = " Delete from TAILIEUDINHKEM where ID=" + ActionSqlParam.SpecialChar + "ID";
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
        public override TaiLieuDinhKemCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, "select * from TAILIEUDINHKEM where (ID =" + ActionSqlParam.SpecialChar + "ID OR DOCUMENT_ID  =" + ActionSqlParam.SpecialChar + "ID)", new DbParam[]{
                    new DbParam("ID",ID)
                    });
                TaiLieuDinhKemCls OTaiLieuDinhKem = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OTaiLieuDinhKem = TaiLieuDinhKemParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OTaiLieuDinhKem;
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
                TaiLieuDinhKemCls OTaiLieuDinhKem = CreateModel(ActionSqlParam, ID);
                OTaiLieuDinhKem.ID = NewID;
                Add(ActionSqlParam, OTaiLieuDinhKem);

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
        public override long Count(ActionSqlParamCls ActionSqlParam, TaiLieuDinhKemFilterCls OTaiLieuDinhKemFilter)
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
                if (OTaiLieuDinhKemFilter == null)
                {
                    OTaiLieuDinhKemFilter = new TaiLieuDinhKemFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select count(1) from TAILIEUDINHKEM";
                if (!string.IsNullOrEmpty(OTaiLieuDinhKemFilter.DOCUMENT_ID))
                {
                    ColDbParams.Add(new DbParam("DOCUMENT_ID", OTaiLieuDinhKemFilter.DOCUMENT_ID));
                    Query += " and DOCUMENT_ID = " + ActionSqlParam.SpecialChar + "DOCUMENT_ID ";
                }
                if (!string.IsNullOrEmpty(OTaiLieuDinhKemFilter.NGUOITAO_ID))
                {
                    ColDbParams.Add(new DbParam("NGUOITAO_ID", OTaiLieuDinhKemFilter.NGUOITAO_ID));
                    Query += " and NGUOITAO_ID = " + ActionSqlParam.SpecialChar + "NGUOITAO_ID";
                }    
                if (OTaiLieuDinhKemFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", OTaiLieuDinhKemFilter.TuNgay));
                    Query += " and NGAYTAO >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (OTaiLieuDinhKemFilter.TuNgay != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", OTaiLieuDinhKemFilter.TuNgay));
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
