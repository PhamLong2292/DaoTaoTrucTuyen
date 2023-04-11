﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Call.Bussiness.Template;
using OneTSQ.Utility;
using OneTSQ.Bussiness.Utility;
using OneTSQ.Core.Model;
using OneTSQ.Database.Service;

namespace OneTSQ.Call.Bussiness.Sql
{
    public class DeTaiProcessBll : DeTaiTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "SqlDeTaiProcessBll";
            }
        }


        public override DeTaiCls[] Reading(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Reading(ActionSqlParam, ODeTaiFilter);
        }      
        public override DeTaiCls[] PageReading(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter, ref long recordTotal)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().PageReading(ActionSqlParam, ODeTaiFilter, ref recordTotal);
        }


        public override void Add(RenderInfoCls ORenderInfo, DeTaiCls ODeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Add(ActionSqlParam, ODeTai);
        }


        public override void Save(RenderInfoCls ORenderInfo, string DeTaiId, DeTaiCls ODeTai)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Save(ActionSqlParam, DeTaiId, ODeTai);
        }


        public override void Delete(RenderInfoCls ORenderInfo, string DeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Delete(ActionSqlParam, DeTaiId);
        }


        public override DeTaiCls CreateModel(RenderInfoCls ORenderInfo, string DeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().CreateModel(ActionSqlParam, DeTaiId);
        }


        public override string Duplicate(RenderInfoCls ORenderInfo, string DeTaiId)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Duplicate(ActionSqlParam, DeTaiId);
        }    
        public override long Count(RenderInfoCls ORenderInfo, DeTaiFilterCls ODeTaiFilter)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            return OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().Count(ActionSqlParam, ODeTaiFilter);
        }

        public override void UpdateLichXetDuyetID(RenderInfoCls ORenderInfo, string DelQuery, DbParam[] Params = null)
        {
            ActionSqlParamCls
                ActionSqlParam = WebEnvironments.CreateActionSqlParam(ORenderInfo);
            OneTSQBussinessUtility.CreateBussinessProcess().CreateDeTaiProcess().UpdateLichXetDuyetID(ActionSqlParam, DelQuery, Params);
        }

    }
}
