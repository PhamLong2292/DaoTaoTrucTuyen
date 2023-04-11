using OneTSQ.Core.Model;
using OneTSQ.TempService;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OneTSQ.UploadUtility
{
    public class ProcessUploadHandlerUtility
    {
        public static void ProcessRequestSessionUploadFile(HttpContext context)
        {
            Collection<UploadHandlerResultCls>
                ColUploadHandlerResults = new Collection<UploadHandlerResultCls> { };
            UploadHandlerResultCls OUploadHandlerResult = new UploadHandlerResultCls();
            string RealFile = "";
            string Ext = "";
            bool IsMulti = false;
            try
            {
                context.Response.ContentType = "text/plain";//"application/json";
                if (context.Request.Files.Count > 1)
                {
                    IsMulti = true;
                }
                for (int iIndexFile = 0; iIndexFile < context.Request.Files.Count; iIndexFile++)
                {
                    HttpPostedFile hpf = context.Request.Files[iIndexFile] as HttpPostedFile;
                    string FileName = string.Empty;
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
                    {
                        string[] files = hpf.FileName.Split(new char[] { '\\' });
                        FileName = files[files.Length - 1];
                    }
                    else
                    {
                        FileName = hpf.FileName;
                    }
                    if (hpf.ContentLength == 0)
                        continue;

                    RealFile = new System.IO.FileInfo(FileName).Name;
                    Ext = new System.IO.FileInfo(FileName).Extension;
                    string SiteId = context.Request["SiteId"];

                    string SecurityCode = WebConfig.GetSecurityCode();
                    string SessionId = context.Request["SessionId"];
                    string LoginName = context.Request["LoginName"];
                    string UserId = context.Request["UserId"];
                    string DestinationPathKey = context.Request["DestinationPathKey"];

                    string WebPathRootTemp = WebConfig.GetWebPathRootTemp();
                    string WebHttpRootTemp = WebConfig.GetWebHttpRootTemp();

                    string MultiUpload = context.Request["MultiUpload"];
                    if (string.IsNullOrEmpty(MultiUpload))
                    {
                        MultiUpload = "0";
                    }
                    string savedFileName, UrlFileName;
                    string DigitalFileId = System.Guid.NewGuid().ToString();
                    FileInfo fileInfo = new System.IO.FileInfo(FileName);
                    if (string.IsNullOrEmpty(DestinationPathKey))
                    {
                        System.IO.Directory.CreateDirectory(WebPathRootTemp);
                        System.IO.Directory.CreateDirectory(WebPathRootTemp + "/" + SiteId);
                        System.IO.Directory.CreateDirectory(WebPathRootTemp + "/" + SiteId + "/" + LoginName);
                        savedFileName = WebPathRootTemp + "/" + SiteId + "/" + LoginName + "/" + DigitalFileId + fileInfo.Extension;
                        UrlFileName = WebHttpRootTemp + "/" + SiteId + "/" + LoginName + "/" + DigitalFileId + fileInfo.Extension;
                    }
                    else
                    {
                        string DestinationPath = System.Web.Configuration.WebConfigurationManager.AppSettings[DestinationPathKey];
                        string WebPathRoot = WebConfig.GetWebPathRoot();
                        string WebHttpRoot = WebConfig.GetWebHttpRoot();
                        savedFileName = WebPathRoot + "/" + DestinationPath + "/" + DigitalFileId + fileInfo.Extension;
                        UrlFileName = WebHttpRoot + "/" + DestinationPath + "/" + DigitalFileId + fileInfo.Extension;
                    }                
               
                    //AjaxOut TempAjaxOut = VNOfficeManUtility.GetDocumentTempFile(SiteId, SessionId, DigitalFileId, LoginName, FileName);

                    //WebPathRootTemp = new Uri(WebPathRootTemp).LocalPath;

                    //System.IO.Directory.CreateDirectory(WebPathRootTemp);
                    //System.IO.Directory.CreateDirectory(WebPathRootTemp + "\\" + SiteId);
                    //System.IO.Directory.CreateDirectory(WebPathRootTemp + "\\" + SiteId + "\\" + LoginName);
                    //string savedFileName = WebPathRootTemp + "\\" + SiteId + "\\" + LoginName + "\\" + DigitalFileId + Ext;
                    //string UrlFileName = WebHttpRootTemp + "/" + SiteId + "/" + LoginName + "/" + DigitalFileId + Ext;


                    //MediaInfoCls
                    //    OMediaInfo=new MediaInfoCls();
                    //OMediaInfo.LoginName=LoginName;
                    //OMediaInfo.MediaInfoId=System.Guid.NewGuid().ToString();
                    //OMediaInfo.Month=System.DateTime.Now.Month;
                    //OMediaInfo.Year=System.DateTime.Now.Year;
                    //OMediaInfo.Section="UploadTemp";
                    //OMediaInfo.SiteId=SiteId;
                    //XmlCls OXml = MediaInfoParser.GetXml(new MediaInfoCls[]{OMediaInfo});
                    //TempServiceUtility.UploadMedia(SecurityCode, OXml.XmlData, OXml.XmlDataSchema, hpf.InputStream);


                    hpf.SaveAs(savedFileName);

                    string uploadFile = new System.IO.FileInfo(FileName).Name;

                    OUploadHandlerResult = new UploadHandlerResultCls();
                    OUploadHandlerResult.Id = DigitalFileId;
                    OUploadHandlerResult.UploadFileName = uploadFile;
                    OUploadHandlerResult.SaveFile = savedFileName;
                    OUploadHandlerResult.SessionId = SessionId;
                    OUploadHandlerResult.UploadUrl = UrlFileName;


                    ColUploadHandlerResults.Add(OUploadHandlerResult);
                    //doan nay nhet vao file temp
                    //WebSystemBussinessUtility.CreateSystemsBussinessProcess(SiteId).SaveArchiveTempFile(null, SiteId, DigitalFileId, "DocumentUpload", savedFileName, uploadFile, SessionId, UserId);
                }
            }
            catch (Exception ex)
            {
                if (IsMulti == false)
                {
                    OUploadHandlerResult.Error = true;
                    OUploadHandlerResult.InfoMessage = ex.Message.ToString();
                    string XmlInfo = UploadHandlerResultParser.GetXml(OUploadHandlerResult);
                    context.Response.Write(XmlInfo);
                    return;
                }
                else
                {
                    MultiUploadHandlerResultCls
                        OMultiUploadHandlerResult = new MultiUploadHandlerResultCls();
                    OMultiUploadHandlerResult.InfoMessage = ex.Message.ToString();
                    OMultiUploadHandlerResult.Error = true;
                    string XmlInfo = MultiUploadHandlerResultParser.GetXml(OMultiUploadHandlerResult);
                    context.Response.Write(XmlInfo);
                    return;
                }
            }
            if (IsMulti == false)
            {
                string XmlInfo = UploadHandlerResultParser.GetXml(OUploadHandlerResult);
                context.Response.Write(XmlInfo);
            }
            else
            {
                string XmlInfo = UploadHandlerResultParser.GetMultiXml(ColUploadHandlerResults.ToArray());
                MultiUploadHandlerResultCls
                    OMultiUploadHandlerResult = new MultiUploadHandlerResultCls();
                OMultiUploadHandlerResult.XmlData = XmlInfo;
                OMultiUploadHandlerResult.Error = false;

                string XmlData = MultiUploadHandlerResultParser.GetXml(OMultiUploadHandlerResult);
                context.Response.Write(XmlData);
            }
        }
    }
}
