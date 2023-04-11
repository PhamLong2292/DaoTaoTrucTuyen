using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Bussiness.Template
{
    public interface IBinhLuanHinhAnhProcess
    {
        string ServiceId { get; }
        BinhLuanHinhAnhCls[] Reading(ActionSqlParamCls ActionSqlParam, BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter);
        void Add(ActionSqlParamCls ActionSqlParam, BinhLuanHinhAnhCls OBinhLuanHinhAnh);
        void Save(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId, BinhLuanHinhAnhCls OBinhLuanHinhAnh);
        void Delete(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId);
        BinhLuanHinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId);
        string Duplicate(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId);
    }

    public class BinhLuanHinhAnhTemplate : IBinhLuanHinhAnhProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual BinhLuanHinhAnhCls[] Reading(ActionSqlParamCls ActionSqlParam, BinhLuanHinhAnhFilterCls OBinhLuanHinhAnhFilter) { return null; }
        public virtual void Add(ActionSqlParamCls ActionSqlParam, BinhLuanHinhAnhCls OBinhLuanHinhAnh) { }
        public virtual void Save(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId, BinhLuanHinhAnhCls OBinhLuanHinhAnh) { }
        public virtual void Delete(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId) { }
        public virtual BinhLuanHinhAnhCls CreateModel(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId) { return null; }
        public virtual string Duplicate(ActionSqlParamCls ActionSqlParam, string BinhLuanHinhAnhId) { return null; }
    }
}
