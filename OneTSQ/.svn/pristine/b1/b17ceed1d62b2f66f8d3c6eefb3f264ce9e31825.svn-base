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

namespace OneTSQ.Call.Bussiness.Ws
{
    public class DM_YKienBenhVienProcessBll : DM_YKienBenhVienTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDM_YKienBenhVienProcessBll";
            }
        }


        public override DM_YKienBenhVienCls[] Reading(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Reading(ActionSqlParam, OYKienBenhVienFilter);
        }

        public override long Count(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Count(ActionSqlParam, OYKienBenhVienFilter);
        }

        public override DM_YKienBenhVienCls[] ReadingWithPaging(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().ReadingWithPaging(ActionSqlParam, OYKienBenhVienFilter);
        }

        public override void Add(RenderInfoCls ORenderInfo, DM_YKienBenhVienCls OYKienBenhVien)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Add(ActionSqlParam, OYKienBenhVien);
        }


        public override void Save(RenderInfoCls ORenderInfo, string YKienBenhVienId, DM_YKienBenhVienCls OYKienBenhVien)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Save(ActionSqlParam, YKienBenhVienId, OYKienBenhVien);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string YKienBenhVienId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Delete(ActionSqlParam, YKienBenhVienId);
        }


        public override DM_YKienBenhVienCls CreateModel(RenderInfoCls ORenderInfo, string YKienBenhVienId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().CreateModel(ActionSqlParam, YKienBenhVienId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string YKienBenhVienId)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().Duplicate(ActionSqlParam, YKienBenhVienId);
        }

        public override DM_YKienBenhVienCls[] PageReading(RenderInfoCls ORenderInfo, DM_YKienBenhVienFilterCls OYKienBenhVienFilter, ref int recordTotal)
        {
            ActionSqlParamCls ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateYKienBenhVienProcess().PageReading(ActionSqlParam, OYKienBenhVienFilter, ref recordTotal);
        }
    }
}
