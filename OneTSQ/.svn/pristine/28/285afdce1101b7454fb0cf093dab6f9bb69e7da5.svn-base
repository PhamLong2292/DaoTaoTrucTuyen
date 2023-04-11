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
    public class CaBenhProcessBll : CaBenhTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlCaBenhProcessBll";
            }
        }
        public override CaBenhCls[] Reading(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls oCaBenhFilter)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;

            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                if (oCaBenhFilter == null)
                {
                    oCaBenhFilter = new CaBenhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                if (!string.IsNullOrEmpty(oCaBenhFilter.LICHHOICHANID))
                {
                    //Query = " select CABENH.* from CABENH inner join LICHHOICHANCABENH LHCCB ON LHCCB.CABENHID = CABENH.ID " +
                    //  " where 1=1 " + oCaBenhFilter.DataPermissionQuery;
                    ColDbParams.Add(new DbParam("LICHHOICHANID", oCaBenhFilter.LICHHOICHANID));
                    Query = " select ID, TIEUDE, CHUYENKHOAMA, BACSY, LYDOYEUCAU, DONVITHAMVANID, DONVITUVANID, TIENSUKHAMBENHID, CHANDOANBANDAUMA, TAOVAO, TRANGTHAI, GIOITINH, NGAYSINH, " +
                        " MOTA, BATDAUHOICHANVAO, KETTHUCHOICHANVAO, DIADIEMHOICHAN, GIUONG, PHONG, NGAYNHAPVIEN, NHAPVIENTAI, DIEUTRITAI, CHUYENGIAHOICHAN, YKIENCANLAMSANG, YKIENCHANDOAN, YKIENDIEUTRI, YKIENKHAC, " +
                        " THUKY, CHUTRIHOICHAN, YKIENKHAMBENH, HIENTHINGAYSINH, NGAYXUATVIEN, DENGHIVAO, NGAYHOICHANDENGHI, LAPLICHDAXEM, TAOBOI, LICHHOICHANID, DUYETHOICHANBOI, DUYETHOICHANVAO, NHAPKHOA, " +
                        " NGAYNHAPKHOA, VITRITAINAN, KHAMCHUABENHID, TIENSUBENH, TRINHBAY, CAPCUU, CHANDOANBANDAU, LOAIHOICHAN, NGHENGHIEPMA, DANTOCMA, LYDOVAOVIEN, BENHSU, TOANTHAN, BOPHAN, CHANDOANSOBO, " +
                        " CANLAMSANG, NXKETQUAXN, KETQUACDHA, CHANDOANXACDINH, PHAUTHUAT, THONGTINDIEUTRI, CAUHOI, DUYETDENGHIVAO, THUTHUAT, " +
                        " CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END MABN, " +
                        " CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END HOTENBN, " +
                        " CASE WHEN DIACHI IS NULL THEN DIACHI ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => DIACHI, key_string => ID)) END DIACHI " +
                        " from CABENH " +
                        " where CABENH.ID in (select LHCCB.CABENHID from LICHHOICHANCABENH LHCCB where LHCCB.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID) " + oCaBenhFilter.DataPermissionQuery;
                }
                else if (!string.IsNullOrEmpty(oCaBenhFilter.KHACLICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("KHACLICHHOICHANID", oCaBenhFilter.KHACLICHHOICHANID));
                    Query = " select ID, TIEUDE, CHUYENKHOAMA, BACSY, LYDOYEUCAU, DONVITHAMVANID, DONVITUVANID, TIENSUKHAMBENHID, CHANDOANBANDAUMA, TAOVAO, TRANGTHAI, GIOITINH, NGAYSINH, " +
                        " MOTA, BATDAUHOICHANVAO, KETTHUCHOICHANVAO, DIADIEMHOICHAN, GIUONG, PHONG, NGAYNHAPVIEN, NHAPVIENTAI, DIEUTRITAI, CHUYENGIAHOICHAN, YKIENCANLAMSANG, YKIENCHANDOAN, YKIENDIEUTRI, YKIENKHAC, " +
                        " THUKY, CHUTRIHOICHAN, YKIENKHAMBENH, HIENTHINGAYSINH, NGAYXUATVIEN, DENGHIVAO, NGAYHOICHANDENGHI, LAPLICHDAXEM, TAOBOI, LICHHOICHANID, DUYETHOICHANBOI, DUYETHOICHANVAO, NHAPKHOA, " +
                        " NGAYNHAPKHOA, VITRITAINAN, KHAMCHUABENHID, TIENSUBENH, TRINHBAY, CAPCUU, CHANDOANBANDAU, LOAIHOICHAN, NGHENGHIEPMA, DANTOCMA, LYDOVAOVIEN, BENHSU, TOANTHAN, BOPHAN, CHANDOANSOBO, " +
                        " CANLAMSANG, NXKETQUAXN, KETQUACDHA, CHANDOANXACDINH, PHAUTHUAT, THONGTINDIEUTRI, CAUHOI, DUYETDENGHIVAO, THUTHUAT, " +
                        " CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END MABN, " +
                        " CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END HOTENBN, " +
                        " CASE WHEN DIACHI IS NULL THEN DIACHI ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => DIACHI, key_string => ID)) END DIACHI " +
                        " from CABENH " +
                        " where CABENH.ID not in (select LHCCB.CABENHID from LICHHOICHANCABENH LHCCB where LHCCB.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "KHACLICHHOICHANID) " + oCaBenhFilter.DataPermissionQuery;
                }
                else
                    Query = " select ID, TIEUDE, CHUYENKHOAMA, BACSY, LYDOYEUCAU, DONVITHAMVANID, DONVITUVANID, TIENSUKHAMBENHID, CHANDOANBANDAUMA, TAOVAO, TRANGTHAI, GIOITINH, NGAYSINH, " +
                        " MOTA, BATDAUHOICHANVAO, KETTHUCHOICHANVAO, DIADIEMHOICHAN, GIUONG, PHONG, NGAYNHAPVIEN, NHAPVIENTAI, DIEUTRITAI, CHUYENGIAHOICHAN, YKIENCANLAMSANG, YKIENCHANDOAN, YKIENDIEUTRI, YKIENKHAC, " +
                        " THUKY, CHUTRIHOICHAN, YKIENKHAMBENH, HIENTHINGAYSINH, NGAYXUATVIEN, DENGHIVAO, NGAYHOICHANDENGHI, LAPLICHDAXEM, TAOBOI, LICHHOICHANID, DUYETHOICHANBOI, DUYETHOICHANVAO, NHAPKHOA, " +
                        " NGAYNHAPKHOA, VITRITAINAN, KHAMCHUABENHID, TIENSUBENH, TRINHBAY, CAPCUU, CHANDOANBANDAU, LOAIHOICHAN, NGHENGHIEPMA, DANTOCMA, LYDOVAOVIEN, BENHSU, TOANTHAN, BOPHAN, CHANDOANSOBO, " +
                        " CANLAMSANG, NXKETQUAXN, KETQUACDHA, CHANDOANXACDINH, PHAUTHUAT, THONGTINDIEUTRI, CAUHOI, DUYETDENGHIVAO, THUTHUAT, " +
                        " CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END MABN, " +
                        " CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END HOTENBN, " +
                        " CASE WHEN DIACHI IS NULL THEN DIACHI ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => DIACHI, key_string => ID)) END DIACHI " +
                        " from CABENH " +
                        " where 1=1 " + oCaBenhFilter.DataPermissionQuery;

                //Query = " select * from CABENH where 1=1 " + oCaBenhFilter.DataPermissionQuery;
                if (oCaBenhFilter.TUNGAY != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", oCaBenhFilter.TUNGAY));
                    Query += " and CABENH." + (string.IsNullOrEmpty(oCaBenhFilter.NGAYLOC) ? "TAOVAO" : oCaBenhFilter.NGAYLOC) + " >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (oCaBenhFilter.DENNGAY != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", oCaBenhFilter.DENNGAY));
                    Query += " and CABENH." + (string.IsNullOrEmpty(oCaBenhFilter.NGAYLOC) ? "TAOVAO" : oCaBenhFilter.NGAYLOC) + " < " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + oCaBenhFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + oCaBenhFilter.Keyword.ToUpper() + "%"));
                    Query += " and (upper(CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END) like " + ActionSqlParam.SpecialChar + "Keyword " +
                        " OR upper(CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END) like " + ActionSqlParam.SpecialChar + "Keyword1) ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.MABN))
                {
                    ColDbParams.Add(new DbParam("MABN", oCaBenhFilter.MABN));
                    Query += " and CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID) = " + ActionSqlParam.SpecialChar + "MABN ";
                }
                //if (!string.IsNullOrEmpty(oCaBenhFilter.CABENHID))
                //{
                //    ColDbParams.Add(new DbParam("CABENHID", oCaBenhFilter.MABN));
                //    Query += " and CABENHID = " + ActionSqlParam.SpecialChar + "CABENHID ";
                //}
                if (!string.IsNullOrEmpty(oCaBenhFilter.DONVITHAMVANID))
                {
                    ColDbParams.Add(new DbParam("DONVITHAMVANID", oCaBenhFilter.DONVITHAMVANID));
                    Query += " and CABENH.DONVITHAMVANID = " + ActionSqlParam.SpecialChar + "DONVITHAMVANID ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.DONVITUVANID))
                {
                    ColDbParams.Add(new DbParam("DONVITUVANID", oCaBenhFilter.DONVITUVANID));
                    Query += " and CABENH.DONVITUVANID = " + ActionSqlParam.SpecialChar + "DONVITUVANID ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.CHUYENKHOAMA))
                {
                    ColDbParams.Add(new DbParam("CHUYENKHOAMA", oCaBenhFilter.CHUYENKHOAMA));
                    Query += " and CABENH.CHUYENKHOAMA = " + ActionSqlParam.SpecialChar + "CHUYENKHOAMA ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.CHANDOANBANDAUMA))
                {
                    ColDbParams.Add(new DbParam("CHANDOANBANDAUMA", oCaBenhFilter.CHANDOANBANDAUMA));
                    Query += " and CABENH.CHANDOANBANDAUMA = " + ActionSqlParam.SpecialChar + "CHANDOANBANDAUMA ";
                }
                if (oCaBenhFilter.TRANGTHAI != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", oCaBenhFilter.TRANGTHAI));
                    Query += " and CABENH.TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                //if (oCaBenhFilter.THUOCLICHHOICHAN == 1 && !string.IsNullOrEmpty(oCaBenhFilter.LICHHOICHANID))
                //{
                //ColDbParams.Add(new DbParam("LICHHOICHANID", oCaBenhFilter.LICHHOICHANID));
                //Query += " and LHCCB.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID ";
                //}
                if (oCaBenhFilter.TrangThais.Count() > 0)
                {
                    string trangThaiQuery = "";
                    for (int i = oCaBenhFilter.TrangThais.Count() - 1; i >= 0; i--)
                    {
                        trangThaiQuery += string.Format("{0},", oCaBenhFilter.TrangThais[i]);
                    }
                    Query += " and CABENH.TRANGTHAI in ( " + trangThaiQuery.Substring(0, trangThaiQuery.Length - 1) + ") ";
                }
                if (oCaBenhFilter.LAPLICHDAXEM != null)
                {
                    ColDbParams.Add(new DbParam("LAPLICHDAXEM", oCaBenhFilter.LAPLICHDAXEM));
                    Query += " and CABENH.LAPLICHDAXEM = " + ActionSqlParam.SpecialChar + "LAPLICHDAXEM ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.UserId))
                {
                    ColDbParams.Add(new DbParam("TAOBOI", oCaBenhFilter.UserId));
                    Query += " and CABENH.TAOBOI = " + ActionSqlParam.SpecialChar + "TAOBOI";
                }
                Query += " order by CABENH.TAOVAO DESC";

                DataSet dsResult =
                        DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                CaBenhCls[] CaBenhs = CaBenhParser.ParseFromDataTable(dsResult.Tables[0]);

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return CaBenhs;
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
        public override DataTable BCQuery(ActionSqlParamCls ActionSqlParam, string Query)
        {
            IDatabaseService DBService = WebDatabaseService.CreateDBService(ActionSqlParam);
            bool HasTrans = ActionSqlParam.Trans != null;
            bool HasCommit = false;
            if (!HasTrans)
                ActionSqlParam.Trans = DBService.BeginTransaction();
            try
            {
                Collection<DbParam> ColDbParams = new Collection<DbParam>();
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return dsResult.Tables[0];
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
        public override CaBenhCls[] PageReading(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls oCaBenhFilter, ref long recordTotal)
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
                if (oCaBenhFilter == null)
                {
                    oCaBenhFilter = new CaBenhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = "";
                string recordTotalQuery = "";
                if (!string.IsNullOrEmpty(oCaBenhFilter.LICHHOICHANID))
                {
                    //Query = " select CABENH.* from CABENH inner join LICHHOICHANCABENH LHCCB ON LHCCB.CABENHID = CABENH.ID " +
                    //  " where 1=1 " + oCaBenhFilter.DataPermissionQuery;
                    ColDbParams.Add(new DbParam("LICHHOICHANID", oCaBenhFilter.LICHHOICHANID));
                    Query = " select ID, TIEUDE, CHUYENKHOAMA, BACSY, LYDOYEUCAU, DONVITHAMVANID, DONVITUVANID, TIENSUKHAMBENHID, CHANDOANBANDAUMA, TAOVAO, TRANGTHAI, GIOITINH, NGAYSINH, " +
                        " MOTA, BATDAUHOICHANVAO, KETTHUCHOICHANVAO, DIADIEMHOICHAN, GIUONG, PHONG, NGAYNHAPVIEN, NHAPVIENTAI, DIEUTRITAI, CHUYENGIAHOICHAN, YKIENCANLAMSANG, YKIENCHANDOAN, YKIENDIEUTRI, YKIENKHAC, " +
                        " THUKY, CHUTRIHOICHAN, YKIENKHAMBENH, HIENTHINGAYSINH, NGAYXUATVIEN, DENGHIVAO, NGAYHOICHANDENGHI, LAPLICHDAXEM, TAOBOI, LICHHOICHANID, DUYETHOICHANBOI, DUYETHOICHANVAO, NHAPKHOA, " +
                        " NGAYNHAPKHOA, VITRITAINAN, KHAMCHUABENHID, TIENSUBENH, TRINHBAY, CAPCUU, CHANDOANBANDAU, LOAIHOICHAN, NGHENGHIEPMA, DANTOCMA, LYDOVAOVIEN, BENHSU, TOANTHAN, BOPHAN, CHANDOANSOBO, " +
                        " CANLAMSANG, NXKETQUAXN, KETQUACDHA, CHANDOANXACDINH, PHAUTHUAT, THONGTINDIEUTRI, CAUHOI, DUYETDENGHIVAO, THUTHUAT, " +
                        " CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END MABN, " +
                        " CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END HOTENBN, " +
                        " CASE WHEN DIACHI IS NULL THEN DIACHI ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => DIACHI, key_string => ID)) END DIACHI " +
                        " from CABENH " +
                        " where CABENH.ID in (select LHCCB.CABENHID from LICHHOICHANCABENH LHCCB where LHCCB.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID) " + oCaBenhFilter.DataPermissionQuery;
                    recordTotalQuery = " select count(1) from CABENH " +
                        " where CABENH.ID in (select LHCCB.CABENHID from LICHHOICHANCABENH LHCCB where LHCCB.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "LICHHOICHANID) " + oCaBenhFilter.DataPermissionQuery;
                }
                else if (!string.IsNullOrEmpty(oCaBenhFilter.KHACLICHHOICHANID))
                {
                    ColDbParams.Add(new DbParam("KHACLICHHOICHANID", oCaBenhFilter.KHACLICHHOICHANID));
                    Query = " select ID, TIEUDE, CHUYENKHOAMA, BACSY, LYDOYEUCAU, DONVITHAMVANID, DONVITUVANID, TIENSUKHAMBENHID, CHANDOANBANDAUMA, TAOVAO, TRANGTHAI, GIOITINH, NGAYSINH, " +
                        " MOTA, BATDAUHOICHANVAO, KETTHUCHOICHANVAO, DIADIEMHOICHAN, GIUONG, PHONG, NGAYNHAPVIEN, NHAPVIENTAI, DIEUTRITAI, CHUYENGIAHOICHAN, YKIENCANLAMSANG, YKIENCHANDOAN, YKIENDIEUTRI, YKIENKHAC, " +
                        " THUKY, CHUTRIHOICHAN, YKIENKHAMBENH, HIENTHINGAYSINH, NGAYXUATVIEN, DENGHIVAO, NGAYHOICHANDENGHI, LAPLICHDAXEM, TAOBOI, LICHHOICHANID, DUYETHOICHANBOI, DUYETHOICHANVAO, NHAPKHOA, " +
                        " NGAYNHAPKHOA, VITRITAINAN, KHAMCHUABENHID, TIENSUBENH, TRINHBAY, CAPCUU, CHANDOANBANDAU, LOAIHOICHAN, NGHENGHIEPMA, DANTOCMA, LYDOVAOVIEN, BENHSU, TOANTHAN, BOPHAN, CHANDOANSOBO, " +
                        " CANLAMSANG, NXKETQUAXN, KETQUACDHA, CHANDOANXACDINH, PHAUTHUAT, THONGTINDIEUTRI, CAUHOI, DUYETDENGHIVAO, THUTHUAT, " +
                        " CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END MABN, " +
                        " CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END HOTENBN, " +
                        " CASE WHEN DIACHI IS NULL THEN DIACHI ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => DIACHI, key_string => ID)) END DIACHI " +
                        " from CABENH " +
                        " where CABENH.ID not in (select LHCCB.CABENHID from LICHHOICHANCABENH LHCCB where LHCCB.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "KHACLICHHOICHANID) " + oCaBenhFilter.DataPermissionQuery;
                    recordTotalQuery = " select count(1) from CABENH " +
                        " where CABENH.ID not in (select LHCCB.CABENHID from LICHHOICHANCABENH LHCCB where LHCCB.LICHHOICHANID = " + ActionSqlParam.SpecialChar + "KHACLICHHOICHANID) " + oCaBenhFilter.DataPermissionQuery;
                }
                else
                {
                    Query = " select ID, TIEUDE, CHUYENKHOAMA, BACSY, LYDOYEUCAU, DONVITHAMVANID, DONVITUVANID, TIENSUKHAMBENHID, CHANDOANBANDAUMA, TAOVAO, TRANGTHAI, GIOITINH, NGAYSINH, " +
                        " MOTA, BATDAUHOICHANVAO, KETTHUCHOICHANVAO, DIADIEMHOICHAN, GIUONG, PHONG, NGAYNHAPVIEN, NHAPVIENTAI, DIEUTRITAI, CHUYENGIAHOICHAN, YKIENCANLAMSANG, YKIENCHANDOAN, YKIENDIEUTRI, YKIENKHAC, " +
                        " THUKY, CHUTRIHOICHAN, YKIENKHAMBENH, HIENTHINGAYSINH, NGAYXUATVIEN, DENGHIVAO, NGAYHOICHANDENGHI, LAPLICHDAXEM, TAOBOI, LICHHOICHANID, DUYETHOICHANBOI, DUYETHOICHANVAO, NHAPKHOA, " +
                        " NGAYNHAPKHOA, VITRITAINAN, KHAMCHUABENHID, TIENSUBENH, TRINHBAY, CAPCUU, CHANDOANBANDAU, LOAIHOICHAN, NGHENGHIEPMA, DANTOCMA, LYDOVAOVIEN, BENHSU, TOANTHAN, BOPHAN, CHANDOANSOBO, " +
                        " CANLAMSANG, NXKETQUAXN, KETQUACDHA, CHANDOANXACDINH, PHAUTHUAT, THONGTINDIEUTRI, CAUHOI, DUYETDENGHIVAO, THUTHUAT, " +
                        " CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END MABN, " +
                        " CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END HOTENBN, " +
                        " CASE WHEN DIACHI IS NULL THEN DIACHI ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => DIACHI, key_string => ID)) END DIACHI " +
                        " from CABENH " +
                        " where 1=1 " + oCaBenhFilter.DataPermissionQuery;
                    recordTotalQuery = " select count(1) from CABENH where 1 = 1 " + oCaBenhFilter.DataPermissionQuery;
                }
                if (oCaBenhFilter.TUNGAY != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", oCaBenhFilter.TUNGAY));
                    Query += " and " + (string.IsNullOrEmpty(oCaBenhFilter.NGAYLOC) ? "TAOVAO" : oCaBenhFilter.NGAYLOC) + " >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                    recordTotalQuery += " and " + (string.IsNullOrEmpty(oCaBenhFilter.NGAYLOC) ? "TAOVAO" : oCaBenhFilter.NGAYLOC) + " >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (oCaBenhFilter.DENNGAY != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", oCaBenhFilter.DENNGAY));
                    Query += " and " + (string.IsNullOrEmpty(oCaBenhFilter.NGAYLOC) ? "TAOVAO" : oCaBenhFilter.NGAYLOC) + " <= " + ActionSqlParam.SpecialChar + "DENNGAY ";
                    recordTotalQuery += " and " + (string.IsNullOrEmpty(oCaBenhFilter.NGAYLOC) ? "TAOVAO" : oCaBenhFilter.NGAYLOC) + " <= " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + oCaBenhFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + oCaBenhFilter.Keyword.ToUpper() + "%"));
                    Query += " and (upper(CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END) like " + ActionSqlParam.SpecialChar + "Keyword " +
                        " OR upper(CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END) like " + ActionSqlParam.SpecialChar + "Keyword1) ";
                    recordTotalQuery += " and (upper(CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END) like " + ActionSqlParam.SpecialChar + "Keyword " +
                        " OR upper(CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END) like " + ActionSqlParam.SpecialChar + "Keyword1) ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.DONVITHAMVANID))
                {
                    ColDbParams.Add(new DbParam("DONVITHAMVANID", oCaBenhFilter.DONVITHAMVANID));
                    Query += " and DONVITHAMVANID = " + ActionSqlParam.SpecialChar + "DONVITHAMVANID ";
                    recordTotalQuery += " and DONVITHAMVANID = " + ActionSqlParam.SpecialChar + "DONVITHAMVANID ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.DONVITUVANID))
                {
                    ColDbParams.Add(new DbParam("DONVITUVANID", oCaBenhFilter.DONVITUVANID));
                    Query += " and DONVITUVANID = " + ActionSqlParam.SpecialChar + "DONVITUVANID ";
                    recordTotalQuery += " and DONVITUVANID = " + ActionSqlParam.SpecialChar + "DONVITUVANID ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.CHUYENKHOAMA))
                {
                    ColDbParams.Add(new DbParam("CHUYENKHOAMA", oCaBenhFilter.CHUYENKHOAMA));
                    Query += " and CABENH.CHUYENKHOAMA = " + ActionSqlParam.SpecialChar + "CHUYENKHOAMA ";
                    recordTotalQuery += " and CABENH.CHUYENKHOAMA = " + ActionSqlParam.SpecialChar + "CHUYENKHOAMA ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.CHANDOANBANDAUMA))
                {
                    ColDbParams.Add(new DbParam("CHANDOANBANDAUMA", oCaBenhFilter.CHANDOANBANDAUMA));
                    Query += " and CABENH.CHANDOANBANDAUMA = " + ActionSqlParam.SpecialChar + "CHANDOANBANDAUMA ";
                    recordTotalQuery += " and CABENH.CHANDOANBANDAUMA = " + ActionSqlParam.SpecialChar + "CHANDOANBANDAUMA ";
                }
                if (oCaBenhFilter.TRANGTHAI != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", oCaBenhFilter.TRANGTHAI));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                    recordTotalQuery += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                if (oCaBenhFilter.TrangThais.Count() > 0)
                {
                    string trangThaiQuery = "";
                    for (int i = oCaBenhFilter.TrangThais.Count() - 1; i >= 0; i--)
                    {
                        trangThaiQuery += string.Format("{0},", oCaBenhFilter.TrangThais[i]);
                    }
                    Query += " and TRANGTHAI in ( " + trangThaiQuery.Substring(0, trangThaiQuery.Length - 1) + ") ";
                    recordTotalQuery += " and TRANGTHAI in ( " + trangThaiQuery.Substring(0, trangThaiQuery.Length - 1) + ") ";
                }
                if (oCaBenhFilter.LAPLICHDAXEM != null)
                {
                    ColDbParams.Add(new DbParam("LAPLICHDAXEM", oCaBenhFilter.LAPLICHDAXEM));
                    Query += " and LAPLICHDAXEM = " + ActionSqlParam.SpecialChar + "LAPLICHDAXEM ";
                    recordTotalQuery += " and LAPLICHDAXEM = " + ActionSqlParam.SpecialChar + "LAPLICHDAXEM ";
                }
                Query += "ORDER BY TAOVAO DESC " +
                    "OFFSET " + (oCaBenhFilter.PageIndex * oCaBenhFilter.PageSize) + " ROWS " +
                    "FETCH NEXT " + oCaBenhFilter.PageSize + " ROWS ONLY ";
                DataSet dsResult = DBService.GetDataSet(ActionSqlParam.Trans, Query, ColDbParams.ToArray());

                CaBenhCls[] CaBenhs = CaBenhParser.ParseFromDataTable(dsResult.Tables[0]);
                recordTotal = long.Parse(DBService.GetDataSet(ActionSqlParam.Trans, recordTotalQuery, ColDbParams.ToArray()).Tables[0].Rows[0][0].ToString());

                dsResult.Clear();
                dsResult.Dispose();
                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return CaBenhs;
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
        public override long Count(ActionSqlParamCls ActionSqlParam, CaBenhFilterCls oCaBenhFilter)
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
                if (oCaBenhFilter == null)
                {
                    oCaBenhFilter = new CaBenhFilterCls();
                }
                Collection<DbParam>
                    ColDbParams = new Collection<DbParam>();
                string Query = " select count(1) from CABENH where 1=1 " + oCaBenhFilter.DataPermissionQuery;
                if (oCaBenhFilter.TUNGAY != null)
                {
                    ColDbParams.Add(new DbParam("TUNGAY", oCaBenhFilter.TUNGAY));
                    Query += " and TAOVAO >= " + ActionSqlParam.SpecialChar + "TUNGAY ";
                }
                if (oCaBenhFilter.DENNGAY != null)
                {
                    ColDbParams.Add(new DbParam("DENNGAY", oCaBenhFilter.DENNGAY));
                    Query += " and TAOVAO <= " + ActionSqlParam.SpecialChar + "DENNGAY ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.Keyword))
                {
                    ColDbParams.Add(new DbParam("Keyword", "%" + oCaBenhFilter.Keyword.ToUpper() + "%"));
                    ColDbParams.Add(new DbParam("Keyword1", "%" + oCaBenhFilter.Keyword.ToUpper() + "%"));
                    Query += " and (upper(CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID))) like " + ActionSqlParam.SpecialChar + "Keyword " +
                        " OR upper(CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID))) like " + ActionSqlParam.SpecialChar + "Keyword1) ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.DONVITHAMVANID))
                {
                    ColDbParams.Add(new DbParam("DONVITHAMVANID", oCaBenhFilter.DONVITHAMVANID));
                    Query += " and DONVITHAMVANID = " + ActionSqlParam.SpecialChar + "DONVITHAMVANID ";
                }
                if (!string.IsNullOrEmpty(oCaBenhFilter.DONVITUVANID))
                {
                    ColDbParams.Add(new DbParam("DONVITUVANID", oCaBenhFilter.DONVITUVANID));
                    Query += " and DONVITUVANID = " + ActionSqlParam.SpecialChar + "DONVITUVANID ";
                }
                if (oCaBenhFilter.TRANGTHAI != null)
                {
                    ColDbParams.Add(new DbParam("TRANGTHAI", oCaBenhFilter.TRANGTHAI));
                    Query += " and TRANGTHAI = " + ActionSqlParam.SpecialChar + "TRANGTHAI ";
                }
                if (oCaBenhFilter.TrangThais.Count() > 0)
                {
                    string trangThaiQuery = "";
                    for (int i = oCaBenhFilter.TrangThais.Count() - 1; i >= 0; i--)
                    {
                        trangThaiQuery += string.Format("{0},", oCaBenhFilter.TrangThais[i]);
                    }
                    Query += " and TRANGTHAI in ( " + trangThaiQuery.Substring(0, trangThaiQuery.Length - 1) + ") ";
                }
                if (oCaBenhFilter.LAPLICHDAXEM != null)
                {
                    ColDbParams.Add(new DbParam("LAPLICHDAXEM", oCaBenhFilter.LAPLICHDAXEM));
                    Query += " and LAPLICHDAXEM = " + ActionSqlParam.SpecialChar + "LAPLICHDAXEM ";
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
        public override void Add(ActionSqlParamCls ActionSqlParam, CaBenhCls OCaBenh)
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
                if (string.IsNullOrEmpty(OCaBenh.ID))
                {
                    OCaBenh.ID = System.Guid.NewGuid().ToString();
                }
                DBService.Insert(ActionSqlParam.Trans, "CABENH",
                    new DbParam[]{
                    new DbParam("ID",OCaBenh.ID),
                    new DbParam("KHAMCHUABENHID",OCaBenh.KHAMCHUABENHID),
                    new DbParam("LYDOVAOVIEN",OCaBenh.LYDOVAOVIEN),
                    new DbParam("CHUYENKHOAMA",OCaBenh.CHUYENKHOAMA),
                    new DbParam("BACSY",OCaBenh.BACSY),
                    new DbParam("LYDOYEUCAU",OCaBenh.LYDOYEUCAU),
                    new DbParam("DONVITHAMVANID",OCaBenh.DONVITHAMVANID),
                    new DbParam("DONVITUVANID",OCaBenh.DONVITUVANID),
                    new DbParam("BENHNHANID",OCaBenh.BENHNHANID),
                    new DbParam("TIENSUKHAMBENHID",OCaBenh.TIENSUKHAMBENHID),
                    new DbParam("CHANDOANBANDAUMA",OCaBenh.CHANDOANBANDAUMA),
                    new DbParam("CHANDOANBANDAU",OCaBenh.CHANDOANBANDAU),
                    new DbParam("TAOBOI",OCaBenh.TAOBOI),
                    new DbParam("TAOVAO",OCaBenh.TAOVAO),
                    new DbParam("DENGHIVAO",OCaBenh.DENGHIVAO),
                    new DbParam("DUYETDENGHIVAO",OCaBenh.DUYETDENGHIVAO),
                    new DbParam("NGAYHOICHANDENGHI",OCaBenh.NGAYHOICHANDENGHI),
                    new DbParam("BATDAUHOICHANVAO",OCaBenh.BATDAUHOICHANVAO),
                    new DbParam("KETTHUCHOICHANVAO",OCaBenh.KETTHUCHOICHANVAO),
                    new DbParam("MABN",OCaBenh.MABN),
                    new DbParam("TENBN",OCaBenh.TENBN),
                    new DbParam("HOBN",OCaBenh.HOBN),
                    new DbParam("HOTENBN",OCaBenh.HOTENBN),
                    new DbParam("NGAYSINH",OCaBenh.NGAYSINH),
                    new DbParam("HIENTHINGAYSINH",OCaBenh.HIENTHINGAYSINH),
                    new DbParam("GIOITINH",OCaBenh.GIOITINH),
                    new DbParam("DIACHI",OCaBenh.DIACHI),
                    new DbParam("MOTA",OCaBenh.MOTA),
                    new DbParam("TIENSUBENH",OCaBenh.TIENSUBENH),
                    new DbParam("TRANGTHAI",OCaBenh.TRANGTHAI),
                    new DbParam("LAPLICHDAXEM",OCaBenh.LAPLICHDAXEM),
                    new DbParam("NGAYNHAPVIEN",OCaBenh.NGAYNHAPVIEN),
                    new DbParam("NGAYXUATVIEN",OCaBenh.NGAYXUATVIEN),
                    new DbParam("NHAPVIENTAI",OCaBenh.NHAPVIENTAI),
                    new DbParam("BENHSU",OCaBenh.BENHSU),
                    new DbParam("TOANTHAN",OCaBenh.TOANTHAN),
                    new DbParam("BOPHAN",OCaBenh.BOPHAN),
                    new DbParam("CHANDOANSOBO",OCaBenh.CHANDOANSOBO),
                    new DbParam("CANLAMSANG",OCaBenh.CANLAMSANG),
                    new DbParam("NXKETQUAXN",OCaBenh.NXKETQUAXN),
                    new DbParam("KETQUACDHA",OCaBenh.KETQUACDHA),
                    new DbParam("CHANDOANXACDINH",OCaBenh.CHANDOANXACDINH),
                    new DbParam("PHAUTHUAT",OCaBenh.PHAUTHUAT),
                    new DbParam("THUTHUAT",OCaBenh.THUTHUAT),
                    new DbParam("CAUHOI",OCaBenh.CAUHOI),
                    new DbParam("THONGTINDIEUTRI",OCaBenh.THONGTINDIEUTRI),
                    new DbParam("GIUONG",OCaBenh.GIUONG),
                    new DbParam("PHONG",OCaBenh.PHONG),
                    new DbParam("LICHHOICHANID",OCaBenh.LICHHOICHANID),
                    new DbParam("DUYETHOICHANBOI",OCaBenh.DUYETHOICHANBOI),
                    new DbParam("DUYETHOICHANVAO",OCaBenh.DUYETHOICHANVAO),
                    new DbParam("VITRITAINAN",OCaBenh.VITRITAINAN),
                    new DbParam("LNG",OCaBenh.LNG),
                    new DbParam("LAT",OCaBenh.LAT),
                    new DbParam("TRINHBAY",OCaBenh.TRINHBAY),
                    new DbParam("CAPCUU",OCaBenh.CAPCUU),
                    new DbParam("NGHENGHIEPMA",OCaBenh.NGHENGHIEPMA),
                    new DbParam("DANTOCMA",OCaBenh.DANTOCMA),
                    new DbParam("LOAIHOICHAN",OCaBenh.LOAIHOICHAN)
                });
                string updateQuery = string.Format(@" update CABENH SET MABN={0}, HOBN={1}, TENBN={2}, HOTENBN={3}, DIACHI={4} " +
                                                        "where ID = " + ActionSqlParam.SpecialChar + "ID",
                                                        string.IsNullOrEmpty(OCaBenh.MABN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.MABN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.HOBN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.HOBN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.TENBN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.TENBN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.HOTENBN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.HOTENBN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.DIACHI) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.DIACHI + "'), key_string => ID) ");
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, updateQuery,
                    new DbParam[]
                {
                    new DbParam("ID", OCaBenh.ID)
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
        public override void Save(ActionSqlParamCls ActionSqlParam, string ID, CaBenhCls OCaBenh)
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
                DBService.Update(ActionSqlParam.Trans, "CABENH", "ID", ID,
                    new DbParam[]{
                   new DbParam("KHAMCHUABENHID",OCaBenh.KHAMCHUABENHID),
                   new DbParam("LYDOVAOVIEN",OCaBenh.LYDOVAOVIEN),
                   new DbParam("CHUYENKHOAMA",OCaBenh.CHUYENKHOAMA),
                   new DbParam("BACSY",OCaBenh.BACSY),
                   new DbParam("LYDOYEUCAU",OCaBenh.LYDOYEUCAU),
                   new DbParam("DONVITHAMVANID",OCaBenh.DONVITHAMVANID),
                   new DbParam("DONVITUVANID",OCaBenh.DONVITUVANID),
                   new DbParam("BENHNHANID",OCaBenh.BENHNHANID),
                   new DbParam("TIENSUKHAMBENHID",OCaBenh.TIENSUKHAMBENHID),
                   new DbParam("CHANDOANBANDAUMA",OCaBenh.CHANDOANBANDAUMA),
                   new DbParam("CHANDOANBANDAU",OCaBenh.CHANDOANBANDAU),
                   new DbParam("TAOBOI",OCaBenh.TAOBOI),
                   new DbParam("TAOVAO",OCaBenh.TAOVAO),
                   new DbParam("DENGHIVAO",OCaBenh.DENGHIVAO),
                   new DbParam("DUYETDENGHIVAO",OCaBenh.DUYETDENGHIVAO),
                   new DbParam("NGAYHOICHANDENGHI",OCaBenh.NGAYHOICHANDENGHI),
                   new DbParam("BATDAUHOICHANVAO",OCaBenh.BATDAUHOICHANVAO),
                   new DbParam("KETTHUCHOICHANVAO",OCaBenh.KETTHUCHOICHANVAO),
                   //new DbParam("MABN",OCaBenh.MABN),
                   //new DbParam("TENBN",OCaBenh.TENBN),
                   //new DbParam("HOBN",OCaBenh.HOBN),
                   //new DbParam("HOTENBN",OCaBenh.HOTENBN),
                   new DbParam("NGAYSINH",OCaBenh.NGAYSINH),
                   new DbParam("HIENTHINGAYSINH",OCaBenh.HIENTHINGAYSINH),
                   new DbParam("GIOITINH",OCaBenh.GIOITINH),
                   //new DbParam("DIACHI",OCaBenh.DIACHI),
                   new DbParam("MOTA",OCaBenh.MOTA),
                   new DbParam("TIENSUBENH",OCaBenh.TIENSUBENH),
                   new DbParam("TRANGTHAI",OCaBenh.TRANGTHAI),
                   new DbParam("LAPLICHDAXEM",OCaBenh.LAPLICHDAXEM),
                    new DbParam("NGAYNHAPVIEN",OCaBenh.NGAYNHAPVIEN),
                    new DbParam("NGAYXUATVIEN",OCaBenh.NGAYXUATVIEN),
                    new DbParam("NHAPVIENTAI",OCaBenh.NHAPVIENTAI),
                    new DbParam("BENHSU",OCaBenh.BENHSU),
                    new DbParam("TOANTHAN",OCaBenh.TOANTHAN),
                    new DbParam("BOPHAN",OCaBenh.BOPHAN),
                    new DbParam("CHANDOANSOBO",OCaBenh.CHANDOANSOBO),
                    new DbParam("CANLAMSANG",OCaBenh.CANLAMSANG),
                    new DbParam("NXKETQUAXN",OCaBenh.NXKETQUAXN),
                    new DbParam("KETQUACDHA",OCaBenh.KETQUACDHA),
                    new DbParam("CHANDOANXACDINH",OCaBenh.CHANDOANXACDINH),
                    new DbParam("PHAUTHUAT",OCaBenh.PHAUTHUAT),
                    new DbParam("THUTHUAT",OCaBenh.THUTHUAT),
                    new DbParam("CAUHOI",OCaBenh.CAUHOI),
                    new DbParam("THONGTINDIEUTRI",OCaBenh.THONGTINDIEUTRI),
                    new DbParam("GIUONG",OCaBenh.GIUONG),
                    new DbParam("PHONG",OCaBenh.PHONG),
                    new DbParam("LICHHOICHANID",OCaBenh.LICHHOICHANID),
                    new DbParam("DUYETHOICHANBOI",OCaBenh.DUYETHOICHANBOI),
                    new DbParam("DUYETHOICHANVAO",OCaBenh.DUYETHOICHANVAO),
                    new DbParam("VITRITAINAN",OCaBenh.VITRITAINAN),
                    new DbParam("LNG",OCaBenh.LNG),
                    new DbParam("LAT",OCaBenh.LAT),
                    new DbParam("TRINHBAY",OCaBenh.TRINHBAY),
                    new DbParam("CAPCUU",OCaBenh.CAPCUU),
                    new DbParam("NGHENGHIEPMA",OCaBenh.NGHENGHIEPMA),
                    new DbParam("DANTOCMA",OCaBenh.DANTOCMA),
                    new DbParam("LOAIHOICHAN",OCaBenh.LOAIHOICHAN)
                });
                string updateQuery = string.Format(@" update CABENH SET MABN={0}, HOBN={1}, TENBN={2}, HOTENBN={3}, DIACHI={4} " +
                                                        "where ID = " + ActionSqlParam.SpecialChar + "ID",
                                                        string.IsNullOrEmpty(OCaBenh.MABN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.MABN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.HOBN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.HOBN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.TENBN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.TENBN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.HOTENBN) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.HOTENBN + "'), key_string => ID) ",
                                                        string.IsNullOrEmpty(OCaBenh.DIACHI) ? "null" : "DBMS_OBFUSCATION_TOOLKIT.DESEncrypt(input_string => GetMultipleOf8Bytes(input_string => '" + OCaBenh.DIACHI + "'), key_string => ID) ");
                DBService.ExecuteNonQuery(ActionSqlParam.Trans, updateQuery,
                    new DbParam[]
                {
                    new DbParam("ID", OCaBenh.ID)
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
                                  " Delete from BinhLuanHinhAnh where HinhAnhId in (select id from HinhAnh where CaBenhId=" + ActionSqlParam.SpecialChar + "ID); " +
                                  " Delete from HinhAnh where CaBenhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from TepTin where CaBenhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from TepDinhKem where YKienChuyenGiaId in (select id from YKienChuyenGia where CaBenhId=" + ActionSqlParam.SpecialChar + "ID); " +
                                  " Delete from YKienChuyenGia where CaBenhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from ButPhe where CaBenhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from ChuyenGiaHoiChan where BienBanHoiChanId in (select id from BienBanHoiChan where CaBenhId=" + ActionSqlParam.SpecialChar + "ID); " +
                                  " Delete from BienBanHoiChan where CaBenhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from BinhLuanHinhAnh where CaBenhAnhCLSId in (select CaBenhId || AnhCLSId from CaBenhAnhCLS where CaBenhId=" + ActionSqlParam.SpecialChar + "ID); " +
                                  " Delete from CaBenhAnhCLS where CaBenhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from KetQuaXetNghiemChiTiet where KETQUAXETNGHIEM_ID in (select id from KetQuaXetNghiem where CaBenhId=" + ActionSqlParam.SpecialChar + "ID); " +
                                  " Delete from KetQuaXetNghiem where CaBenhId=" + ActionSqlParam.SpecialChar + "ID; " +
                                  " Delete from CABENH where ID=" + ActionSqlParam.SpecialChar + "ID; " +
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
        public override CaBenhCls CreateModel(ActionSqlParamCls ActionSqlParam, string ID)
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
                        DBService.GetDataSet(ActionSqlParam.Trans, " select ID, TIEUDE, CHUYENKHOAMA, BACSY, LYDOYEUCAU, DONVITHAMVANID, DONVITUVANID, TIENSUKHAMBENHID, CHANDOANBANDAUMA, TAOVAO, TRANGTHAI, GIOITINH, NGAYSINH, " +
                        " MOTA, BATDAUHOICHANVAO, KETTHUCHOICHANVAO, DIADIEMHOICHAN, GIUONG, PHONG, NGAYNHAPVIEN, NHAPVIENTAI, DIEUTRITAI, CHUYENGIAHOICHAN, YKIENCANLAMSANG, YKIENCHANDOAN, YKIENDIEUTRI, YKIENKHAC, " +
                        " THUKY, CHUTRIHOICHAN, YKIENKHAMBENH, HIENTHINGAYSINH, NGAYXUATVIEN, DENGHIVAO, NGAYHOICHANDENGHI, LAPLICHDAXEM, TAOBOI, LICHHOICHANID, DUYETHOICHANBOI, DUYETHOICHANVAO, NHAPKHOA, " +
                        " NGAYNHAPKHOA, VITRITAINAN, KHAMCHUABENHID, TIENSUBENH, TRINHBAY, CAPCUU, CHANDOANBANDAU, LOAIHOICHAN, NGHENGHIEPMA, DANTOCMA, LYDOVAOVIEN, BENHSU, TOANTHAN, BOPHAN, CHANDOANSOBO, " +
                        " CANLAMSANG, NXKETQUAXN, KETQUACDHA, CHANDOANXACDINH, PHAUTHUAT, THONGTINDIEUTRI, CAUHOI, DUYETDENGHIVAO, THUTHUAT, " +
                        " CASE WHEN MABN IS NULL THEN MABN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => MABN, key_string => ID)) END MABN, " +
                        " CASE WHEN HOTENBN IS NULL THEN HOTENBN ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => HOTENBN, key_string => ID)) END HOTENBN, " +
                        " CASE WHEN DIACHI IS NULL THEN DIACHI ELSE GetStringValue(DBMS_OBFUSCATION_TOOLKIT.DESDecrypt(input_string => DIACHI, key_string => ID)) END DIACHI " +
                        " from CABENH where ID=" + ActionSqlParam.SpecialChar + "ID ", new DbParam[]{
                    new DbParam("ID",ID)
                });
                CaBenhCls OCaBenh = null;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    OCaBenh = CaBenhParser.ParseFromDataRow(dsResult.Tables[0].Rows[0]);
                }
                dsResult.Clear();
                dsResult.Dispose();

                if (!HasTrans && !HasCommit)
                {
                    ActionSqlParam.Trans.Commit();
                    ActionSqlParam.Trans = null;
                    HasCommit = true;
                }
                return OCaBenh;
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
                CaBenhCls OCaBenh = CreateModel(ActionSqlParam, ID);
                OCaBenh.ID = NewID;
                Add(ActionSqlParam, OCaBenh);

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
