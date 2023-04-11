//using OneTSQ.Call.Bussiness.Utility;
//using OneTSQ.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Newtonsoft.Json;
//using OneTSQ.Core.Call.Bussiness.Utility;
//using System.Web;
//using System.Runtime.Caching;

//namespace OneTSQ.WebParts
//{
//    public class CaBenhService : RemoteDataTemplate
//    {
//        private static string _DsChuyenKhoa = "DsChuyenKhoa";
//        private static string _DsBenhVien = "DsBenhVien";
//        private static readonly MemoryCache _cache = MemoryCache.Default;
//        public static string StaticServiceId { get { return "CaBenhService"; } }
//        public override string ServiceId { get { return StaticServiceId; } }
//        public override string ServiceName { get { return "Danh sách bệnh nhân"; } }
//        public override AjaxOut Reading(RenderInfoCls ORenderInfo, int PageIndex, string Keyword)
//        {
//            AjaxOut RetAjaxOut = new AjaxOut();
//            try
//            {
//                long recordTotal = 0;
//                CaBenhFilterCls OCaBenhFilter = new CaBenhFilterCls();
//                OCaBenhFilter.Keyword = Keyword;
//                CaBenhCls[] caBenhs = new List<CaBenhCls>().ToArray();
//                caBenhs = CallBussinessUtility.CreateBussinessProcess().CreateCaBenhProcess().PageReading(ORenderInfo, OCaBenhFilter, ref recordTotal);
//                var cacheItemPolicy = new CacheItemPolicy()
//                {
//                    AbsoluteExpiration = DateTime.Now.AddDays(1)
//                };
//                if (caBenhs.Length != 0 && PageIndex == 0)
//                {
//                    CaBenhCls OCaBenh = new CaBenhCls();
//                    OCaBenh.ID = null;
//                    var lstHang = caBenhs.ToList();
//                    lstHang.Insert(0, OCaBenh);
//                    caBenhs = lstHang.ToArray();
//                }
//                CaBenhRecord ORecord = new CaBenhRecord();
//                ORecord.total_count = recordTotal;
//                ORecord.incomplete_results = true;
//                ORecord.items = new CaBenhItems[caBenhs.Length];
//                for (int iIndex = 0; iIndex < caBenhs.Length; iIndex++)
//                {
//                    ORecord.items[iIndex] = new CaBenhItems();
//                    if (!string.IsNullOrEmpty(caBenhs[iIndex].ID))
//                    {
//                        ORecord.items[iIndex].id = caBenhs[iIndex].ID;
//                        ORecord.items[iIndex].text = caBenhs[iIndex].HOTENBN;
//                        ORecord.items[iIndex].Code = caBenhs[iIndex].MABN;
//                        ORecord.items[iIndex].Name = caBenhs[iIndex].HOTENBN;
//                        ORecord.items[iIndex].NamSinh = caBenhs[iIndex].NGAYSINH.HasValue ? caBenhs[iIndex].NGAYSINH.Value.Year.ToString() : "";
//                        //OneMES3.DM.Model.ChuyenKhoaCls chuyenKhoa = null;
//                        if (!string.IsNullOrEmpty(caBenhs[iIndex].CHUYENKHOAMA))
//                        {
//                            if (_cache.Get(_DsChuyenKhoa) == null)
//                            {
//                                var lstchuyenKhoa = CallBussinessUtility.CreateBussinessProcess().CreateChuyenKhoaDaoTaoTtProcess().Reading(ORenderInfo, new DM_ChuyenKhoaDaoTaoTtFilterCls() { HieuLuc = (int)Common.eHieuLuc.Co });
//                                _cache.Add(_DsChuyenKhoa, lstchuyenKhoa, cacheItemPolicy);
//                            }
//                            OneMES3.DM.Model.ChuyenKhoaCls[] ChuyenKhoas = (OneMES3.DM.Model.ChuyenKhoaCls[])_cache.Get(_DsChuyenKhoa);
//                            if (ChuyenKhoas.ToList().Exists(p=>p.Ma== caBenhs[iIndex].CHUYENKHOAMA)) ORecord.items[iIndex].ChuyenKhoa = ChuyenKhoas.FirstOrDefault(p => p.Ma == caBenhs[iIndex].CHUYENKHOAMA).Ten;
//                        }
//                        if (_cache.Get(_DsBenhVien) == null)
//                        {
//                            var benhViens = CoreCallBussinessUtility.CreateBussinessProcess().CreateOwnerProcess().Reading(ORenderInfo, new OwnerFilterCls() { });
//                            _cache.Add(_DsBenhVien, benhViens, cacheItemPolicy);
//                        }
//                        OwnerCls[] BenhViens = (OwnerCls[])_cache.Get(_DsBenhVien);
//                        if (!string.IsNullOrEmpty(caBenhs[iIndex].DONVITHAMVANID))
//                        {
//                            var benhVien = BenhViens.FirstOrDefault(p => p.OwnerId == caBenhs[iIndex].DONVITHAMVANID);
//                            ORecord.items[iIndex].DvThamVan = benhVien != null ? benhVien.OwnerName : "";
//                        }
//                        if (!string.IsNullOrEmpty(caBenhs[iIndex].DONVITUVANID))
//                        {
//                            var benhVien = BenhViens.FirstOrDefault(p => p.OwnerId == caBenhs[iIndex].DONVITUVANID);
//                            ORecord.items[iIndex].DvTuVan = benhVien != null ? benhVien.OwnerName : "";
//                        }
//                    }
//                    else
//                    {
//                        ORecord.items[iIndex].id = null;
//                        ORecord.items[iIndex].Code = "<b>Mã</b>";
//                        ORecord.items[iIndex].Name = "<b>Tên</b>";
//                        ORecord.items[iIndex].ChuyenKhoa = "<b>Chuyên khoa</b>";
//                        ORecord.items[iIndex].NamSinh = "<b>Năm sinh</b>";
//                        ORecord.items[iIndex].DvThamVan = "<b>Đơn vị tham vấn</b>";
//                        ORecord.items[iIndex].DvTuVan = "<b>Đơn vị tư vấn</b>";
//                    }
//                }
//                string json = JsonConvert.SerializeObject(ORecord, Formatting.Indented);
//                RetAjaxOut.HtmlContent = json;
//            }
//            catch (Exception ex)
//            {
//                RetAjaxOut.Error = true;
//                RetAjaxOut.InfoMessage = ex.Message.ToString();
//            }
//            return RetAjaxOut;
//        }
//    }
//    public class CaBenhItems
//    {
//        public string id;
//        public string text;
//        public string Code;
//        public string Name;
//        public string NamSinh;
//        public string ChuyenKhoa;
//        public string GioTinh;
//        public string DvThamVan;
//        public string DvTuVan;
//        public string Status;
//    }
//    internal class CaBenhRecord
//    {
//        public long total_count;
//        public bool incomplete_results;
//        public CaBenhItems[] items;
//    }
//}
