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
    public class LichXetDuyetProcessBll : LichXetDuyetTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlLichXetDuyetProcessBll";
            }
        }


        public override LichXetDuyetCls[] Reading(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Reading(ActionSqlParam, OLichXetDuyetFilter);
        }

        public override LichXetDuyetCls[] PageReadingDC(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().PageReadingDC(ActionSqlParam, OLichXetDuyetFilter, ref recordTotal);
        }

        public override LichXetDuyetCls[] PageReadingDT(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().PageReadingDT(ActionSqlParam, OLichXetDuyetFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, LichXetDuyetCls OLichXetDuyet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Add(ActionSqlParam, OLichXetDuyet);
        }


        public override void Save(RenderInfoCls ORenderInfo, string LichXetDuyetId, LichXetDuyetCls OLichXetDuyet)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Save(ActionSqlParam, LichXetDuyetId, OLichXetDuyet);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string LichXetDuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Delete(ActionSqlParam, LichXetDuyetId);
        }


        public override LichXetDuyetCls CreateModel(RenderInfoCls ORenderInfo, string LichXetDuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().CreateModel(ActionSqlParam, LichXetDuyetId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string LichXetDuyetId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Duplicate(ActionSqlParam, LichXetDuyetId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, LichXetDuyetFilterCls OLichXetDuyetFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateLichXetDuyetProcess().Count(ActionSqlParam, OLichXetDuyetFilter);
        }


    }
}
