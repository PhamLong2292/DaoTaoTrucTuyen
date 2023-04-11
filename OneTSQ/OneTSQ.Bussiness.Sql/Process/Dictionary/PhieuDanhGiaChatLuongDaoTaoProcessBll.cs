using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using OneTSQ.Database.Service;
using OneTSQ.Model;
using OneTSQ.Bussiness.Template;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Sql
{
    public class PhieuDanhGiaChatLuongDaoTaoProcessBll : PhieuDanhGiaChatLuongDaoTaoTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlPhieuDanhGiaChatLuongDaoTaoProcessBll";
            }
        }
        public override PhieuDanhGiaChatLuongDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter)
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
                if (OPhieuDanhGiaChatLuongDaoTaoFilter == null)
                {
                    OPhieuDanhGiaChatLuongDaoTaoFilter = new PhieuDanhGiaChatLuongDaoTaoFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = " select * from PHIEUDANHGIACHATLUONGDAOTAO ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                PhieuDanhGiaChatLuongDaoTaoCls[] PhieuDanhGiaChatLuongDaoTaos = PhieuDanhGiaChatLuongDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return PhieuDanhGiaChatLuongDaoTaos;
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
        public override PhieuDanhGiaChatLuongDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls oPhieuDanhGiaChatLuongDaoTaoFilter, ref long recordTotal)
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
                if (oPhieuDanhGiaChatLuongDaoTaoFilter == null)
                {
                    oPhieuDanhGiaChatLuongDaoTaoFilter = new PhieuDanhGiaChatLuongDaoTaoFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = " select * from PHIEUDANHGIACHATLUONGDAOTAO where 1 = 1 " + oPhieuDanhGiaChatLuongDaoTaoFilter.DataPermissionQuery;
                string recordTotalQuery = " select count(1) from PHIEUDANHGIACHATLUONGDAOTAO where 1 = 1 " + oPhieuDanhGiaChatLuongDaoTaoFilter.DataPermissionQuery;
                if (oPhieuDanhGiaChatLuongDaoTaoFilter.QUY != null)
                {
                    ColDbParams.Add(new DbParam("QUY", oPhieuDanhGiaChatLuongDaoTaoFilter.QUY));
                    Query += " and PHIEUDANHGIACHATLUONGDAOTAO.QUY = " + ActionSqlParam.SpecialChar + "QUY ";
                    recordTotalQuery += " and PHIEUDANHGIACHATLUONGDAOTAO.QUY = " + ActionSqlParam.SpecialChar + "QUY ";
                }
                if (oPhieuDanhGiaChatLuongDaoTaoFilter.NAM != null)
                {
                    ColDbParams.Add(new DbParam("NAM", oPhieuDanhGiaChatLuongDaoTaoFilter.NAM));
                    Query += " and PHIEUDANHGIACHATLUONGDAOTAO.NAM = " + ActionSqlParam.SpecialChar + "NAM ";
                    recordTotalQuery += " and PHIEUDANHGIACHATLUONGDAOTAO.NAM = " + ActionSqlParam.SpecialChar + "NAM ";
                }
                if (!string.IsNullOrEmpty(oPhieuDanhGiaChatLuongDaoTaoFilter.BENHVIENTHAMVAN_ID))
                {
                    ColDbParams.Add(new DbParam("BENHVIENTHAMVAN_ID", oPhieuDanhGiaChatLuongDaoTaoFilter.BENHVIENTHAMVAN_ID));
                    Query += " and PHIEUDANHGIACHATLUONGDAOTAO.BENHVIENTHAMVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTHAMVAN_ID ";
                    recordTotalQuery += " and PHIEUDANHGIACHATLUONGDAOTAO.BENHVIENTHAMVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTHAMVAN_ID ";
                }
                if (!string.IsNullOrEmpty(oPhieuDanhGiaChatLuongDaoTaoFilter.BENHVIENTUVAN_ID))
                {
                    ColDbParams.Add(new DbParam("BENHVIENTUVAN_ID", oPhieuDanhGiaChatLuongDaoTaoFilter.BENHVIENTUVAN_ID));
                    Query += " and PHIEUDANHGIACHATLUONGDAOTAO.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID ";
                    recordTotalQuery += " and PHIEUDANHGIACHATLUONGDAOTAO.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID ";
                }
                if (oPhieuDanhGiaChatLuongDaoTaoFilter.TRANGTHAI != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", oPhieuDanhGiaChatLuongDaoTaoFilter.TRANGTHAI));
                    Query += " and PHIEUDANHGIACHATLUONGDAOTAO.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                    recordTotalQuery += " and PHIEUDANHGIACHATLUONGDAOTAO.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                Query += "ORDER BY PHIEUDANHGIACHATLUONGDAOTAO.TAOVAO DESC " +
                    "OFFSET " + (oPhieuDanhGiaChatLuongDaoTaoFilter.PageIndex * oPhieuDanhGiaChatLuongDaoTaoFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + oPhieuDanhGiaChatLuongDaoTaoFilter.PageSize + " ROWS ONLY ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                PhieuDanhGiaChatLuongDaoTaoCls[] PhieuDanhGiaChatLuongDaoTaos = PhieuDanhGiaChatLuongDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = PhieuDanhGiaChatLuongDaoTaoParser.CountFromDataTable(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return PhieuDanhGiaChatLuongDaoTaos;
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
        public override long Count(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoFilterCls OPhieuDanhGiaChatLuongDaoTaoFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (OPhieuDanhGiaChatLuongDaoTaoFilter == null)
                    OPhieuDanhGiaChatLuongDaoTaoFilter = new PhieuDanhGiaChatLuongDaoTaoFilterCls();
                string Query = " select COUNT (*) from PHIEUDANHGIACHATLUONGDAOTAO";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
                long count = PhieuDanhGiaChatLuongDaoTaoParser.CountFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return count;
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
        public override void Add(ActionSqlParamCls ActionSqlParam, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao)
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
                DBService.Insert(ActionSqlParam.Trans, "PhieuDanhGiaChatLuongDaoTao",
                    new DbParam[]{
                    new DbParam("ID",OPhieuDanhGiaChatLuongDaoTao.ID),
                    new DbParam("QUY",OPhieuDanhGiaChatLuongDaoTao.QUY),
                    new DbParam("NAM",OPhieuDanhGiaChatLuongDaoTao.NAM),
                    new DbParam("BENHVIENTHAMVAN_ID",OPhieuDanhGiaChatLuongDaoTao.BENHVIENTHAMVAN_ID),
                    new DbParam("BENHVIENTUVAN_ID",OPhieuDanhGiaChatLuongDaoTao.BENHVIENTUVAN_ID),
                    new DbParam("SOBUOIBAOCAOTHAMGIA",OPhieuDanhGiaChatLuongDaoTao.SOBUOIBAOCAOTHAMGIA),
                    new DbParam("LYDODIEMYNGHIA",OPhieuDanhGiaChatLuongDaoTao.LYDODIEMYNGHIA),
                    new DbParam("YKIENDONGGOP",OPhieuDanhGiaChatLuongDaoTao.YKIENDONGGOP),
                    new DbParam("HOTENNGUOILAPPHIEU",OPhieuDanhGiaChatLuongDaoTao.HOTENNGUOILAPPHIEU),
                    new DbParam("SODIENTHOAI",OPhieuDanhGiaChatLuongDaoTao.SODIENTHOAI),
                    new DbParam("TAOBOI",OPhieuDanhGiaChatLuongDaoTao.TAOBOI),
                    new DbParam("TAOVAO",OPhieuDanhGiaChatLuongDaoTao.TAOVAO),
                    new DbParam("SUABOI",OPhieuDanhGiaChatLuongDaoTao.SUABOI),
                    new DbParam("SUAVAO",OPhieuDanhGiaChatLuongDaoTao.SUAVAO),
                    new DbParam("TRANGTHAI",OPhieuDanhGiaChatLuongDaoTao.TRANGTHAI)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao)
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
                DBService.Update(ActionSqlParam.Trans, "PHIEUDANHGIACHATLUONGDAOTAO", "ID", ID,
                   new DbParam[]{
               new DbParam("QUY",OPhieuDanhGiaChatLuongDaoTao.QUY),
               new DbParam("NAM",OPhieuDanhGiaChatLuongDaoTao.NAM),
               new DbParam("BENHVIENTHAMVAN_ID",OPhieuDanhGiaChatLuongDaoTao.BENHVIENTHAMVAN_ID),
               new DbParam("BENHVIENTUVAN_ID",OPhieuDanhGiaChatLuongDaoTao.BENHVIENTUVAN_ID),
               new DbParam("SOBUOIBAOCAOTHAMGIA",OPhieuDanhGiaChatLuongDaoTao.SOBUOIBAOCAOTHAMGIA),
               new DbParam("LYDODIEMYNGHIA",OPhieuDanhGiaChatLuongDaoTao.LYDODIEMYNGHIA),
               new DbParam("YKIENDONGGOP",OPhieuDanhGiaChatLuongDaoTao.YKIENDONGGOP),
               new DbParam("HOTENNGUOILAPPHIEU",OPhieuDanhGiaChatLuongDaoTao.HOTENNGUOILAPPHIEU),
               new DbParam("SODIENTHOAI",OPhieuDanhGiaChatLuongDaoTao.SODIENTHOAI),
               new DbParam("TAOBOI",OPhieuDanhGiaChatLuongDaoTao.TAOBOI),
               new DbParam("TAOVAO",OPhieuDanhGiaChatLuongDaoTao.TAOVAO),
               new DbParam("SUABOI",OPhieuDanhGiaChatLuongDaoTao.SUABOI),
               new DbParam("SUAVAO",OPhieuDanhGiaChatLuongDaoTao.SUAVAO),
               new DbParam("TRANGTHAI",OPhieuDanhGiaChatLuongDaoTao.TRANGTHAI)
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
                                    " Delete from ChatLuongHoatDongTtb where PhieuDanhGiaChatLuongDaoTao_Id=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DoHieuQuaGiangDay where PhieuDanhGiaChatLuongDaoTao_Id=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from MucDoPhongPhuBaiBaoCao where PhieuDanhGiaChatLuongDaoTao_Id=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DanhGiaThoiGianBuoiBaoCao where PhieuDanhGiaChatLuongDaoTao_Id=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from DanhGiaThoiLuongBuoiBaoCao where PhieuDanhGiaChatLuongDaoTao_Id=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from MucDoYNghiaChuongTrinhDaoTao where PhieuDanhGiaChatLuongDaoTao_Id=" + ActionSqlParam.SpecialChar + "ID; " +
                                    " Delete from PHIEUDANHGIACHATLUONGDAOTAO where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override PhieuDanhGiaChatLuongDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from PHIEUDANHGIACHATLUONGDAOTAO where (ID=" + ActionSqlParam.SpecialChar + "ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
                PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OPhieuDanhGiaChatLuongDaoTao = PhieuDanhGiaChatLuongDaoTaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OPhieuDanhGiaChatLuongDaoTao;
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
                PhieuDanhGiaChatLuongDaoTaoCls OPhieuDanhGiaChatLuongDaoTao = CreateModel(ActionSqlParam, ID);
                OPhieuDanhGiaChatLuongDaoTao.ID = NewID;
                Add(ActionSqlParam, OPhieuDanhGiaChatLuongDaoTao);

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
