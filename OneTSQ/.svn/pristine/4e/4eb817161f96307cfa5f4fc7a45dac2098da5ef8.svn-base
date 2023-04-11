using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class DM_TieuChuanThamGiaKhoaHocProcessBll : DM_TieuChuanThamGiaKhoaHocTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_TieuChuanThamGiaKhoaHocProcessBll";
            }
        }


        public override DM_TieuChuanThamGiaKhoaHocCls[] Reading(
            RenderInfoCls ORenderInfo,
            DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Reading(ActionSqlParam, OTieuChuanThamGiaKhoaHocFilter);
        }

        public override long Count(
            RenderInfoCls ORenderInfo,
            DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Count(ActionSqlParam, OTieuChuanThamGiaKhoaHocFilter);
        }

        public override DM_TieuChuanThamGiaKhoaHocCls[] ReadingWithPaging(
            RenderInfoCls ORenderInfo,
            DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().ReadingWithPaging(ActionSqlParam, OTieuChuanThamGiaKhoaHocFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Add(ActionSqlParam, OTieuChuanThamGiaKhoaHoc);
        }


        public override void Save(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId, DM_TieuChuanThamGiaKhoaHocCls OTieuChuanThamGiaKhoaHoc)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Save(ActionSqlParam, TieuChuanThamGiaKhoaHocId, OTieuChuanThamGiaKhoaHoc);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Delete(ActionSqlParam, TieuChuanThamGiaKhoaHocId);
        }


        public override DM_TieuChuanThamGiaKhoaHocCls CreateModel(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CreateModel(ActionSqlParam, TieuChuanThamGiaKhoaHocId);
        }
        public override string Duplicate(RenderInfoCls ORenderInfo, string TieuChuanThamGiaKhoaHocId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().Duplicate(ActionSqlParam, TieuChuanThamGiaKhoaHocId);
        }
        public override DM_TieuChuanThamGiaKhoaHocCls[] PageReading(RenderInfoCls ORenderInfo, DM_TieuChuanThamGiaKhoaHocFilterCls OTieuChuanThamGiaKhoaHocFilter, ref int recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().PageReading(ActionSqlParam, OTieuChuanThamGiaKhoaHocFilter, ref recordTotal);
        }
        public override DM_TieuChuanThamGiaKhoaHocCls CheckCode(RenderInfoCls ORenderInfo, string MaTieuChuanThamGiaKhoaHoc)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDM_TieuChuanThamGiaKhoaHocProcess().CheckCode(ActionSqlParam, MaTieuChuanThamGiaKhoaHoc);
        }
    }
}
