using OnlineTour.Bussiness.Utility;
using OnlineTour.Model;
using OnlineTour.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTour.WebParts
{
    public class ProcessDownloadAttachedEmailService:DownloadTemplate
    {
        public override string ServiceId
        {
            get
            {
                return "view_email_attached_file";
            }
        }

        public override AjaxOut Download(RenderInfoCls ORenderInfo, string ServiceId, string Id)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                SiteParam OSiteParam = WebEnvironments.CreateSiteParam(ORenderInfo);
                ActionSqlParamCls
                    OActionSqlParam = WebEnvironments.CreateActionSqlParam(OSiteParam);
                string[] Items = Id.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (Items.Length != 2) throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Tham số không hợp lệ"));

                string Pop3EmailUserId = Items[0];
                string EmailAttactedId = Items[1];
                

                EmailAttachedFileCls
                    OEmailAttachedFile = OnlineTourBussinessUtility.CreateBussinessProcess().CreateEmailProcess().DownloadEmailAttachedFile(OActionSqlParam, Pop3EmailUserId, EmailAttactedId);

                if (OEmailAttachedFile == null)
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Không tìm thấy nội dung file gắn kèm"));
                }

                RetAjaxOut.RetExtraParam1=OEmailAttachedFile.FileName;
                RetAjaxOut.RetExtraParam2=new System.IO.FileInfo(OEmailAttachedFile.FileName).Extension;
                string EmailAttachedPath = WebConfig.GetEmailAttachedPath();
                if (OEmailAttachedFile.CheckExists(EmailAttachedPath))
                {
                    RetAjaxOut.RetBytes = FunctionUtilities.GetBytesFromFile(OEmailAttachedFile.GetSaveFile(EmailAttachedPath));
                }
                else
                {
                    throw new Exception(WebLanguage.GetLanguage(OSiteParam, "Không tìm thấy attached file"));
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.InfoMessage = ex.Message.ToString();
                RetAjaxOut.HtmlContent = ex.Message.ToString();
                RetAjaxOut.Error = true;
            }
            return RetAjaxOut;
        }
    }
}
