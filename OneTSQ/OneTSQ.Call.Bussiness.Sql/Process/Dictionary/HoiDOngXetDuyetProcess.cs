using System;
using System.Collections.Generic;
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
    public class HoiDongXetDuyetProcessBll : HoiDongXetDuyetTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlHoiDongXetDuyetProcessBll";
            }
        }


        public override HoiDongXetDuyetCls[] Reading(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Reading(ActionSqlParam, OHoiDongXetDuyetFilter);
        }      
        public override HoiDongXetDuyetCls[] PageReading(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().PageReading(ActionSqlParam, OHoiDongXetDuyetFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, HoiDongXetDuyetCls OHoiDongXetDuyet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Add(ActionSqlParam, OHoiDongXetDuyet);
        }


        public override void Save(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId, HoiDongXetDuyetCls OHoiDongXetDuyet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Save(ActionSqlParam, HoiDongXetDuyetId, OHoiDongXetDuyet);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Delete(ActionSqlParam, HoiDongXetDuyetId);
        }


        public override HoiDongXetDuyetCls CreateModel(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().CreateModel(ActionSqlParam, HoiDongXetDuyetId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string HoiDongXetDuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Duplicate(ActionSqlParam, HoiDongXetDuyetId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, HoiDongXetDuyetFilterCls OHoiDongXetDuyetFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateHoiDongXetDuyetProcess().Count(ActionSqlParam, OHoiDongXetDuyetFilter);
        }


    }
}
