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
public class BcDanhGiaChatLuongDaoTaoProcessBll : BcDanhGiaChatLuongDaoTaoTemplate
{
    public override string ServiceId
    {
        get
        {
            return "SqlBcDanhGiaChatLuongDaoTaoProcessBll";
        }
    }
    public override BcDanhGiaChatLuongDaoTaoCls[] Reading(ActionSqlParamCls ActionSqlParam,BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter)
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
            if (OBcDanhGiaChatLuongDaoTaoFilter == null)
            {
                OBcDanhGiaChatLuongDaoTaoFilter = new BcDanhGiaChatLuongDaoTaoFilterCls();
            }
            Collection<DbParam> ColDbParams = new Collection<DbParam>();
            string Query = " select * from BAOCAODANHGIACHATLUONGDAOTAO ";
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
            BcDanhGiaChatLuongDaoTaoCls[] BcDanhGiaChatLuongDaoTaos = BcDanhGiaChatLuongDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);

            dsResult.Clear();
            dsResult.Dispose();
            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return BcDanhGiaChatLuongDaoTaos;
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
        public override BcDanhGiaChatLuongDaoTaoCls[] PageReading(ActionSqlParamCls ActionSqlParam, BcDanhGiaChatLuongDaoTaoFilterCls oBcDanhGiaChatLuongDaoTaoFilter, ref long recordTotal)
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
                if (oBcDanhGiaChatLuongDaoTaoFilter == null)
                {
                    oBcDanhGiaChatLuongDaoTaoFilter = new BcDanhGiaChatLuongDaoTaoFilterCls();
                }
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                string Query = " select * from BAOCAODANHGIACHATLUONGDAOTAO where 1 = 1 " + oBcDanhGiaChatLuongDaoTaoFilter.DataPermissionQuery;
                string recordTotalQuery = " select count(1) from BAOCAODANHGIACHATLUONGDAOTAO where 1 = 1 " + oBcDanhGiaChatLuongDaoTaoFilter.DataPermissionQuery;
                if (oBcDanhGiaChatLuongDaoTaoFilter.TUNGAY != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", oBcDanhGiaChatLuongDaoTaoFilter.TUNGAY));
                    Query += " and BAOCAODANHGIACHATLUONGDAOTAO.TAOVAO >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                    recordTotalQuery += " and BAOCAODANHGIACHATLUONGDAOTAO.TAOVAO >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (oBcDanhGiaChatLuongDaoTaoFilter.DENNGAY != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", oBcDanhGiaChatLuongDaoTaoFilter.DENNGAY));
                    Query += " and BAOCAODANHGIACHATLUONGDAOTAO.TAOVAO < " + ActionSqlParam.SpecialChar + "DENNGAY ";
                    recordTotalQuery += " and BAOCAODANHGIACHATLUONGDAOTAO.TAOVAO < " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }
                if (!string.IsNullOrEmpty(oBcDanhGiaChatLuongDaoTaoFilter.BENHVIENID))
                {
                    ColDbParams.Add(new DbParam("BENHVIENID", oBcDanhGiaChatLuongDaoTaoFilter.BENHVIENID));
                    Query += " and BAOCAODANHGIACHATLUONGDAOTAO.BENHVIENID = " + ActionSqlParam.SpecialChar + "BENHVIENID ";
                    recordTotalQuery += " and BAOCAODANHGIACHATLUONGDAOTAO.BENHVIENID = " + ActionSqlParam.SpecialChar + "BENHVIENID ";
                }
                if (oBcDanhGiaChatLuongDaoTaoFilter.TRANGTHAI != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", oBcDanhGiaChatLuongDaoTaoFilter.TRANGTHAI));
                    Query += " and BAOCAODANHGIACHATLUONGDAOTAO.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                    recordTotalQuery += " and BAOCAODANHGIACHATLUONGDAOTAO.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                Query += "ORDER BY BAOCAODANHGIACHATLUONGDAOTAO.TAOVAO DESC " +
                    "OFFSET " + (oBcDanhGiaChatLuongDaoTaoFilter.PageIndex * oBcDanhGiaChatLuongDaoTaoFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + oBcDanhGiaChatLuongDaoTaoFilter.PageSize + " ROWS ONLY ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                BcDanhGiaChatLuongDaoTaoCls[] BcDanhGiaChatLuongDaoTaos = BcDanhGiaChatLuongDaoTaoParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = BcDanhGiaChatLuongDaoTaoParser.CountFromDataTable(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return BcDanhGiaChatLuongDaoTaos;
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
        public override long Count( ActionSqlParamCls ActionSqlParam,BcDanhGiaChatLuongDaoTaoFilterCls OBcDanhGiaChatLuongDaoTaoFilter)
    {
        IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
        bool HasTrans = ActionSqlParam.Trans != null;
        bool HasCommit = false;
       if (!HasTrans)
           ActionSqlParam.Trans = DBService.BeginTransaction();
       try
       {
           if (OBcDanhGiaChatLuongDaoTaoFilter == null)
               OBcDanhGiaChatLuongDaoTaoFilter = new BcDanhGiaChatLuongDaoTaoFilterCls();
           string Query = " select COUNT (*) from BAOCAODANHGIACHATLUONGDAOTAO";
        DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query);
        long count = BcDanhGiaChatLuongDaoTaoParser.CountFromDataTable(dsResult.Tables[0]);
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
    public override void Add(ActionSqlParamCls ActionSqlParam, BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao)
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
                DBService.Insert(ActionSqlParam.Trans, "BAOCAODanhGiaChatLuongDaoTao",
                    new DbParam[]{
                    new DbParam("ID",OBcDanhGiaChatLuongDaoTao.ID),
                    new DbParam("QUY",OBcDanhGiaChatLuongDaoTao.QUY),
                    new DbParam("NAM",OBcDanhGiaChatLuongDaoTao.NAM),
                    new DbParam("BENHVIENID",OBcDanhGiaChatLuongDaoTao.BENHVIENID),
                    new DbParam("GHICHULYDOBVKODANHGIA",OBcDanhGiaChatLuongDaoTao.GHICHULYDOBVKODANHGIA),
                    new DbParam("DIENGIAICHATLUONGTTB",OBcDanhGiaChatLuongDaoTao.DIENGIAICHATLUONGTTB),
                    new DbParam("DIENGIAIDOHIEUQUA",OBcDanhGiaChatLuongDaoTao.DIENGIAIDOHIEUQUA),
                    new DbParam("DIENGIAIMUCDOPHONGPHU",OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCDOPHONGPHU),
                    new DbParam("DIENGIAITHOIGIANTHOILUONG",OBcDanhGiaChatLuongDaoTao.DIENGIAITHOIGIANTHOILUONG),
                    new DbParam("DIENGIAIMUCYNGHIA",OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCYNGHIA),
                    new DbParam("YKIENKHAC",OBcDanhGiaChatLuongDaoTao.YKIENKHAC),
                    new DbParam("TAOBOI",OBcDanhGiaChatLuongDaoTao.TAOBOI),
                    new DbParam("TAOVAO",OBcDanhGiaChatLuongDaoTao.TAOVAO),
                    new DbParam("SUABOI",OBcDanhGiaChatLuongDaoTao.SUABOI),
                    new DbParam("SUAVAO",OBcDanhGiaChatLuongDaoTao.SUAVAO),
                    new DbParam("TRANGTHAI",OBcDanhGiaChatLuongDaoTao.TRANGTHAI)
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
    public override void Save(ActionSqlParamCls ActionSqlParam, string ID, BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao)
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
            DBService.Update(ActionSqlParam.Trans, "BAOCAODANHGIACHATLUONGDAOTAO", "ID", ID,
               new DbParam[]{
               new DbParam("QUY",OBcDanhGiaChatLuongDaoTao.QUY),
               new DbParam("NAM",OBcDanhGiaChatLuongDaoTao.NAM),
               new DbParam("BENHVIENID",OBcDanhGiaChatLuongDaoTao.BENHVIENID),
               new DbParam("GHICHULYDOBVKODANHGIA",OBcDanhGiaChatLuongDaoTao.GHICHULYDOBVKODANHGIA),
               new DbParam("DIENGIAICHATLUONGTTB",OBcDanhGiaChatLuongDaoTao.DIENGIAICHATLUONGTTB),
               new DbParam("DIENGIAIDOHIEUQUA",OBcDanhGiaChatLuongDaoTao.DIENGIAIDOHIEUQUA),
               new DbParam("DIENGIAIMUCDOPHONGPHU",OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCDOPHONGPHU),
               new DbParam("DIENGIAITHOIGIANTHOILUONG",OBcDanhGiaChatLuongDaoTao.DIENGIAITHOIGIANTHOILUONG),
               new DbParam("DIENGIAIMUCYNGHIA",OBcDanhGiaChatLuongDaoTao.DIENGIAIMUCYNGHIA),
               new DbParam("YKIENKHAC",OBcDanhGiaChatLuongDaoTao.YKIENKHAC),
               new DbParam("TAOBOI",OBcDanhGiaChatLuongDaoTao.TAOBOI),
               new DbParam("TAOVAO",OBcDanhGiaChatLuongDaoTao.TAOVAO),
               new DbParam("SUABOI",OBcDanhGiaChatLuongDaoTao.SUABOI),
               new DbParam("SUAVAO",OBcDanhGiaChatLuongDaoTao.SUAVAO),
               new DbParam("TRANGTHAI",OBcDanhGiaChatLuongDaoTao.TRANGTHAI)
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
                                " Delete from BAOCAODANHGIACHATLUONGDAOTAO where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
    public override BcDanhGiaChatLuongDaoTaoCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
            DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, "select * from BAOCAODANHGIACHATLUONGDAOTAO where (ID=" + ActionSqlParam.SpecialChar+"ID)  ", new DbParam[]{
                    new DbParam("ID",ID)
                });
            BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao = null;
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                OBcDanhGiaChatLuongDaoTao = BcDanhGiaChatLuongDaoTaoParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
            }
            dsResult.Clear();
            dsResult.Dispose();

            if (!HasTrans && !HasCommit)
            {
                ActionSqlParam.Trans.Commit();
                ActionSqlParam.Trans = null;
                HasCommit = true;
            }
            return OBcDanhGiaChatLuongDaoTao;
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
            BcDanhGiaChatLuongDaoTaoCls OBcDanhGiaChatLuongDaoTao = CreateModel(ActionSqlParam, ID);
            OBcDanhGiaChatLuongDaoTao.ID = NewID;
            Add(ActionSqlParam, OBcDanhGiaChatLuongDaoTao);

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
        public override void AddBenhVienThamGiaHoiChan(ActionSqlParamCls ActionSqlParam, BCDGCLuongDaoTaoDonViCongTacCls oBCDGCLuongDaoTaoDonViCongTac)
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
                DBService.Insert(ActionSqlParam.Trans, "BCDGCLUONGDAOTAODONVICONGTAC",
                    new DbParam[]{
                    new DbParam("BCDANHGIACHATLUONGDAOTAOID",oBCDGCLuongDaoTaoDonViCongTac.BCDANHGIACHATLUONGDAOTAOID),
                    new DbParam("DONVICONGTACMA",oBCDGCLuongDaoTaoDonViCongTac.DONVICONGTACMA),
                    new DbParam("STT",oBCDGCLuongDaoTaoDonViCongTac.STT)
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
        public override void UpdateBenhVienThamGiaHoiChan(ActionSqlParamCls ActionSqlParam, BCDGCLuongDaoTaoDonViCongTacCls oBCDGCLuongDaoTaoDonViCongTac)
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
                string DelQuery = " Update BCDGCLUONGDAOTAODONVICONGTAC " +
                    "set STT = " + ActionSqlParam.SpecialChar + "STT " +
                    "where BCDANHGIACHATLUONGDAOTAOID=" + ActionSqlParam.SpecialChar + "BCDANHGIACHATLUONGDAOTAOID and DONVICONGTACMA=" + ActionSqlParam.SpecialChar + "DONVICONGTACMA ";
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, DelQuery,
                new DbParam[]
                {
                    new DbParam("STT",oBCDGCLuongDaoTaoDonViCongTac.STT),
                    new DbParam("BCDANHGIACHATLUONGDAOTAOID",oBCDGCLuongDaoTaoDonViCongTac.BCDANHGIACHATLUONGDAOTAOID),
                    new DbParam("DONVICONGTACMA",oBCDGCLuongDaoTaoDonViCongTac.DONVICONGTACMA)
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
        public override void DeleteBenhVienThamGiaHoiChan(ActionSqlParamCls ActionSqlParam, BCDGCLuongDaoTaoDonViCongTacCls oBCDGCLuongDaoTaoDonViCongTac)
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
                                    " Delete from BCDGCLUONGDAOTAODONVICONGTAC where BCDANHGIACHATLUONGDAOTAOID=" + ActionSqlParam.SpecialChar + "BCDANHGIACHATLUONGDAOTAOID and DONVICONGTACMA=" + ActionSqlParam.SpecialChar + "DONVICONGTACMA; " +
                                    " END;";
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, DelQuery,
                new DbParam[]
                {
                    new DbParam("BCDANHGIACHATLUONGDAOTAOID",oBCDGCLuongDaoTaoDonViCongTac.BCDANHGIACHATLUONGDAOTAOID),
                    new DbParam("DONVICONGTACMA",oBCDGCLuongDaoTaoDonViCongTac.DONVICONGTACMA)
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
        public override BCDGCLuongDaoTaoDonViCongTacCls[] GetBenhVienThamGiaHoiChan(ActionSqlParamCls ActionSqlParam, string bcDanhGiaChatLuongDaoTaoId)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                string Query = " select * " +
                                "from BCDGCLuongDaoTaoDonViCongTac " +
                                "where BCDANHGIACHATLUONGDAOTAOID = " + ActionSqlParam.SpecialChar + "BCDANHGIACHATLUONGDAOTAOID " +
                                "order by STT ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("BCDANHGIACHATLUONGDAOTAOID", bcDanhGiaChatLuongDaoTaoId)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                BCDGCLuongDaoTaoDonViCongTacCls[] bcdgcLuongDaoTaoDonViCongTacs = BCDGCLuongDaoTaoDonViCongTacParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return bcdgcLuongDaoTaoDonViCongTacs;
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
        public override string[] GetBenhVienDanhGiaChatLuongDaoTao(ActionSqlParamCls ActionSqlParam, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (benhVienHoiChanMas.Count() == 0)
                    return new string[0];
                string bvmas = string.Join("','", benhVienHoiChanMas);
                if (!string.IsNullOrEmpty(bvmas))
                    bvmas = "'" + bvmas + "'";
                string Query = " select distinct tbo.OWNERCODE " +
                                "from PHIEUDANHGIACHATLUONGDAOTAO pdgcldt inner join TABLEOWNER tbo on pdgcldt.BENHVIENTHAMVAN_ID = tbo.OWNERID " +
                                "where pdgcldt.QUY = " + ActionSqlParam.SpecialChar + "QUY " +
                                "and pdgcldt.NAM = " + ActionSqlParam.SpecialChar + "NAM " +
                                "and pdgcldt.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID " +
                                "and pdgcldt.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI " +
                                "and tbo.OWNERCODE in (" + bvmas + ") ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("QUY", quy),
                    new DbParam("NAM", nam),
                    new DbParam("BENHVIENTUVAN_ID", donViTuVanId),
                    new DbParam("TRANGTHAI", (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                string[] sResult = new string[dsResult.Tables[0].Rows.Count];
                for (int i = dsResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    sResult[i] = dsResult.Tables[0].Rows[i][0].ToString();
                }
                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return sResult;
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
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaChatLuongHoatDongTtb(ActionSqlParamCls ActionSqlParam, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (benhVienHoiChanMas.Count() == 0)
                    return new TongHopDanhGiaCls[0];
                string bvmas = string.Join("','", benhVienHoiChanMas);
                if (!string.IsNullOrEmpty(bvmas))
                    bvmas = "'" + bvmas + "'";
                string Query = " select pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, clhdttb.STT, clhdttb.TRANGTHIETBITRUYENHINHTTMA DANHMUCMA, ttbthtt.TEN DANHMUCTEN, clhdttb.DANHGIA, count(clhdttb.DANHGIA) SOLUONG " +
                                "from CHATLUONGHOATDONGTTB clhdttb inner join PHIEUDANHGIACHATLUONGDAOTAO pdgcldt on clhdttb.PHIEUDANHGIACHATLUONGDAOTAO_ID = pdgcldt.ID " +
                                "inner join TABLEOWNER tbo on pdgcldt.BENHVIENTHAMVAN_ID = tbo.OWNERID and tbo.OWNERCODE in (" + bvmas + ") " +
                                "inner join DM_TRANGTHIETBITRUYENHINHTT ttbthtt on ttbthtt.MA = clhdttb.TRANGTHIETBITRUYENHINHTTMA " +
                                "group by pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, clhdttb.STT, clhdttb.TRANGTHIETBITRUYENHINHTTMA, ttbthtt.TEN, clhdttb.DANHGIA " +
                                "having pdgcldt.QUY = " + ActionSqlParam.SpecialChar + "QUY " +
                                "and pdgcldt.NAM = " + ActionSqlParam.SpecialChar + "NAM " +
                                "and pdgcldt.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID " +
                                "and pdgcldt.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI " +
                                "and clhdttb.DANHGIA is not null " +
                                "order by clhdttb.STT ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("QUY", quy),
                    new DbParam("NAM", nam),
                    new DbParam("BENHVIENTUVAN_ID", donViTuVanId),
                    new DbParam("TRANGTHAI", (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TongHopDanhGiaCls[] result = TongHopDanhGiaParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
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
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaDoHieuQua(ActionSqlParamCls ActionSqlParam, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (benhVienHoiChanMas.Count() == 0)
                    return new TongHopDanhGiaCls[0];
                string bvmas = string.Join("','", benhVienHoiChanMas);
                if (!string.IsNullOrEmpty(bvmas))
                    bvmas = "'" + bvmas + "'";
                string Query = " select pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, dhqgd.STT, dhqgd.CHUYENKHOADAOTAOTTMA DANHMUCMA, ckdttt.TEN DANHMUCTEN, dhqgd.DANHGIA, count(dhqgd.DANHGIA) SOLUONG " +
                                "from DOHIEUQUAGIANGDAY dhqgd inner join PHIEUDANHGIACHATLUONGDAOTAO pdgcldt on dhqgd.PHIEUDANHGIACHATLUONGDAOTAO_ID = pdgcldt.ID " +
                                "inner join TABLEOWNER tbo on pdgcldt.BENHVIENTHAMVAN_ID = tbo.OWNERID and tbo.OWNERCODE in (" + bvmas + ") " +
                                "inner join DM_CHUYENKHOADAOTAOTT ckdttt on ckdttt.MA = dhqgd.CHUYENKHOADAOTAOTTMA " +
                                "group by pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, dhqgd.STT, dhqgd.CHUYENKHOADAOTAOTTMA, ckdttt.TEN, dhqgd.DANHGIA " +
                                "having pdgcldt.QUY = " + ActionSqlParam.SpecialChar + "QUY " +
                                "and pdgcldt.NAM = " + ActionSqlParam.SpecialChar + "NAM " +
                                "and pdgcldt.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID " +
                                "and pdgcldt.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI " +
                                "order by dhqgd.STT ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("QUY", quy),
                    new DbParam("NAM", nam),
                    new DbParam("BENHVIENTUVAN_ID", donViTuVanId),
                    new DbParam("TRANGTHAI", (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TongHopDanhGiaCls[] result = TongHopDanhGiaParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
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
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaMucDoPhongPhu(ActionSqlParamCls ActionSqlParam, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (benhVienHoiChanMas.Count() == 0)
                    return new TongHopDanhGiaCls[0];
                string bvmas = string.Join("','", benhVienHoiChanMas);
                if (!string.IsNullOrEmpty(bvmas))
                    bvmas = "'" + bvmas + "'";
                string Query = " select pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, mdppbbc.STT, mdppbbc.CHUYENKHOADAOTAOTTMA DANHMUCMA, ckdttt.TEN DANHMUCTEN, mdppbbc.DANHGIA, count(mdppbbc.DANHGIA) SOLUONG " +
                                "from MUCDOPHONGPHUBAIBAOCAO mdppbbc inner join PHIEUDANHGIACHATLUONGDAOTAO pdgcldt on mdppbbc.PHIEUDANHGIACHATLUONGDAOTAO_ID = pdgcldt.ID " +
                                "inner join TABLEOWNER tbo on pdgcldt.BENHVIENTHAMVAN_ID = tbo.OWNERID and tbo.OWNERCODE in (" + bvmas + ") " +
                                "inner join DM_CHUYENKHOADAOTAOTT ckdttt on ckdttt.MA = mdppbbc.CHUYENKHOADAOTAOTTMA " +
                                "group by pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, mdppbbc.STT, mdppbbc.CHUYENKHOADAOTAOTTMA, ckdttt.TEN, mdppbbc.DANHGIA " +
                                "having pdgcldt.QUY = " + ActionSqlParam.SpecialChar + "QUY " +
                                "and pdgcldt.NAM = " + ActionSqlParam.SpecialChar + "NAM " +
                                "and pdgcldt.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID " +
                                "and pdgcldt.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI " +
                                "order by mdppbbc.STT ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("QUY", quy),
                    new DbParam("NAM", nam),
                    new DbParam("BENHVIENTUVAN_ID", donViTuVanId),
                    new DbParam("TRANGTHAI", (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TongHopDanhGiaCls[] result = TongHopDanhGiaParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
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
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaThoiGian(ActionSqlParamCls ActionSqlParam, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (benhVienHoiChanMas.Count() == 0)
                    return new TongHopDanhGiaCls[0];
                string bvmas = string.Join("','", benhVienHoiChanMas);
                if (!string.IsNullOrEmpty(bvmas))
                    bvmas = "'" + bvmas + "'";
                string Query = " select pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, dgtgbbc.STT, dgtgbbc.TIEUCHITHOIGIANDAOTAOTTMA DANHMUCMA, tctgdttt.TEN DANHMUCTEN, dgtgbbc.DANHGIA, count(dgtgbbc.DANHGIA) SOLUONG " +
                                "from DANHGIATHOIGIANBUOIBAOCAO dgtgbbc inner join PHIEUDANHGIACHATLUONGDAOTAO pdgcldt on dgtgbbc.PHIEUDANHGIACHATLUONGDAOTAO_ID = pdgcldt.ID " +
                                "inner join TABLEOWNER tbo on pdgcldt.BENHVIENTHAMVAN_ID = tbo.OWNERID and tbo.OWNERCODE in (" + bvmas + ") " +
                                "inner join DM_TIEUCHITHOIGIANDAOTAOTT tctgdttt on tctgdttt.MA = dgtgbbc.TIEUCHITHOIGIANDAOTAOTTMA " +
                                "group by pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, dgtgbbc.STT, dgtgbbc.TIEUCHITHOIGIANDAOTAOTTMA, tctgdttt.TEN, dgtgbbc.DANHGIA " +
                                "having pdgcldt.QUY = " + ActionSqlParam.SpecialChar + "QUY " +
                                "and pdgcldt.NAM = " + ActionSqlParam.SpecialChar + "NAM " +
                                "and pdgcldt.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID " +
                                "and pdgcldt.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI " +
                                "order by dgtgbbc.STT ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("QUY", quy),
                    new DbParam("NAM", nam),
                    new DbParam("BENHVIENTUVAN_ID", donViTuVanId),
                    new DbParam("TRANGTHAI", (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TongHopDanhGiaCls[] result = TongHopDanhGiaParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
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
        public override TongHopDanhGiaCls[] GetTongHopDanhGiaThoiLuong(ActionSqlParamCls ActionSqlParam, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (benhVienHoiChanMas.Count() == 0)
                    return new TongHopDanhGiaCls[0];
                string bvmas = string.Join("','", benhVienHoiChanMas);
                if (!string.IsNullOrEmpty(bvmas))
                    bvmas = "'" + bvmas + "'";
                string Query = " select pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, dgtlbbc.STT, dgtlbbc.TIEUCHITHOILUONGDAOTAOTTMA DANHMUCMA, tctldttt.TEN DANHMUCTEN, dgtlbbc.DANHGIA, count(dgtlbbc.DANHGIA) SOLUONG " +
                                "from DANHGIATHOILUONGBUOIBAOCAO dgtlbbc inner join PHIEUDANHGIACHATLUONGDAOTAO pdgcldt on dgtlbbc.PHIEUDANHGIACHATLUONGDAOTAO_ID = pdgcldt.ID " +
                                "inner join TABLEOWNER tbo on pdgcldt.BENHVIENTHAMVAN_ID = tbo.OWNERID and tbo.OWNERCODE in (" + bvmas + ") " +
                                "inner join DM_TIEUCHITHOILUONGDAOTAOTT tctldttt on tctldttt.MA = dgtlbbc.TIEUCHITHOILUONGDAOTAOTTMA " +
                                "group by pdgcldt.QUY, pdgcldt.NAM, pdgcldt.BENHVIENTUVAN_ID, pdgcldt.TRANGTHAI, dgtlbbc.STT, dgtlbbc.TIEUCHITHOILUONGDAOTAOTTMA, tctldttt.TEN, dgtlbbc.DANHGIA " +
                                "having pdgcldt.QUY = " + ActionSqlParam.SpecialChar + "QUY " +
                                "and pdgcldt.NAM = " + ActionSqlParam.SpecialChar + "NAM " +
                                "and pdgcldt.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID " +
                                "and pdgcldt.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI " +
                                "order by dgtlbbc.STT ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("QUY", quy),
                    new DbParam("NAM", nam),
                    new DbParam("BENHVIENTUVAN_ID", donViTuVanId),
                    new DbParam("TRANGTHAI", (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TongHopDanhGiaCls[] result = TongHopDanhGiaParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
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
        public override TongHopDanhGiaMucYNghiaCls[] GetTongHopDanhGiaMucYNghia(ActionSqlParamCls ActionSqlParam, string donViTuVanId, int quy, int nam, string[] benhVienHoiChanMas)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (benhVienHoiChanMas.Count() == 0)
                    return new TongHopDanhGiaMucYNghiaCls[0];
                string bvmas = string.Join("','", benhVienHoiChanMas);
                if (!string.IsNullOrEmpty(bvmas))
                    bvmas = "'" + bvmas + "'";
                string Query = " select mdynctdt.STT, mdynctdt.CHUYENKHOADAOTAOTTMA, ckdttt.TEN CHUYENKHOADAOTAOTTTEN, ckdttt.TENNGAN CHUYENKHOADAOTAOTTTENNGAN, tbo.OWNERCODE BENHVIENMA, tbo.OWNERNAME BENHVIENTEN, mdynctdt.DANHGIA " +
                                "from MUCDOYNGHIACHUONGTRINHDAOTAO mdynctdt inner join PHIEUDANHGIACHATLUONGDAOTAO pdgcldt on mdynctdt.PHIEUDANHGIACHATLUONGDAOTAO_ID = pdgcldt.ID " +
                                "inner join TABLEOWNER tbo on pdgcldt.BENHVIENTHAMVAN_ID = tbo.OWNERID and tbo.OWNERCODE in (" + bvmas + ") " +
                                "inner join DM_CHUYENKHOADAOTAOTT ckdttt on ckdttt.MA = mdynctdt.CHUYENKHOADAOTAOTTMA " +
                                "inner join TABLEOWNER tbo on tbo.OWNERID = pdgcldt.BENHVIENTHAMVAN_ID " +
                                "where pdgcldt.QUY = " + ActionSqlParam.SpecialChar + "QUY " +
                                "and pdgcldt.NAM = " + ActionSqlParam.SpecialChar + "NAM " +
                                "and pdgcldt.BENHVIENTUVAN_ID = " + ActionSqlParam.SpecialChar + "BENHVIENTUVAN_ID " +
                                "and pdgcldt.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI " +
                                "order by mdynctdt.STT ";
                Collection<DbParam> ColDbParams = new Collection<DbParam>()
                {
                    new DbParam("QUY", quy),
                    new DbParam("NAM", nam),
                    new DbParam("BENHVIENTUVAN_ID", donViTuVanId),
                    new DbParam("TRANGTHAI", (int)PhieuDanhGiaChatLuongDaoTaoCls.eTrangThai.DaGui)
                };
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                TongHopDanhGiaMucYNghiaCls[] result = TongHopDanhGiaMucYNghiaParser.ParseFromDataTable(dsResult.Tables[0]);
                dsResult.Clear();
                dsResult.Dispose();
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
