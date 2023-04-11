using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OneTSQ.Model;
using OneTSQ.Core.Model;

namespace OneTSQ.Call.Bussiness.Template
{
    public interface ILapLichTepDinhKemProcess
    {
        string ServiceId { get; }
        LapLichTepDinhKemCls[] Reading(RenderInfoCls ORenderInfo, LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter);
        void Add(RenderInfoCls ORenderInfo, LapLichTepDinhKemCls OLapLichTepDinhKem);
        void Save(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId, LapLichTepDinhKemCls OLapLichTepDinhKem);
        void Delete(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId);
        LapLichTepDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId);
        string Duplicate(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId);
    }

    public class LapLichTepDinhKemTemplate : ILapLichTepDinhKemProcess
    {
        public virtual string ServiceId { get { return null; } }
        public virtual LapLichTepDinhKemCls[] Reading(RenderInfoCls ORenderInfo, LapLichTepDinhKemFilterCls OLapLichTepDinhKemFilter) { return null; }
        public virtual void Add(RenderInfoCls ORenderInfo, LapLichTepDinhKemCls OLapLichTepDinhKem) { }
        public virtual void Save(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId, LapLichTepDinhKemCls OLapLichTepDinhKem) { }
        public virtual void Delete(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId) { }
        public virtual LapLichTepDinhKemCls CreateModel(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId) { return null; }
        public virtual string Duplicate(RenderInfoCls ORenderInfo, string LapLichTepDinhKemId) { return null; }
    }
}
