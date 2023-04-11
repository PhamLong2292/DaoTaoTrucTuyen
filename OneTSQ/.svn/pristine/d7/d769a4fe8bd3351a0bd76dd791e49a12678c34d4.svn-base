<%@ Page Language="C#" AutoEventWireup="true"  %>
<%@ Register src="UserControls/USite.ascx" tagname="USite" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {

        OneTSQ.Core.Model.RenderInfoCls ORenderInfo = new OneTSQ.Core.Model.RenderInfoCls();
        string SiteId = USite1.SiteId;
        string SiteLang = USite1.SiteLang;
        string usid = Request["usid"];
        if (string.IsNullOrEmpty(usid))
        {
            usid = OneTSQ.Utility.WebEnvironments.GetUseSessionId()+System.Guid.NewGuid().ToString().Substring(0, 8);
        }
        string UserSessionId = usid;

        string SiteAssetLevelId = USite1.SiteAssetLevelId;
        string AssetLevelCode = USite1.AssetLevelCode;
        bool IsUserLogin = OneTSQ.Utility.WebSessionUtility.IsUserLogin(ORenderInfo);
        if (IsUserLogin == false)
        {
            Response.Redirect("login.aspx");
        }
        
        ORenderInfo.SiteId = SiteId;
        ORenderInfo.UserSessionId = UserSessionId;
        ORenderInfo.SiteLanguage = SiteLang;
        ORenderInfo.SiteAssetLevelId = SiteAssetLevelId;
        ORenderInfo.AssetLevelCode = AssetLevelCode;
        
        ORenderInfo.WebPage = this;
        
        string ServiceId = (string)OneTSQ.Utility.WebEnvironments.Request("ServiceId");
        string ObjectId = (string)OneTSQ.Utility.WebEnvironments.Request("ObjectId");
        OneTSQ.Core.Model.AjaxOut RetAjaxOut = OneTSQ.Utility.DownloadUtility.Download(ORenderInfo, ServiceId, ObjectId);
        if (RetAjaxOut.Error)
        {
            Response.Write(RetAjaxOut.HtmlContent);
        }
        else
        {
            
            Response.ContentType = RetAjaxOut.RetExtraParam2;
            Response.AddHeader("Content-Disposition", "inline;filename=" + RetAjaxOut.RetExtraParam1);
            Response.BufferOutput = true;
            Response.BinaryWrite(RetAjaxOut.RetBytes);
            Response.Write("Ok");
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmMain" runat="server">
    <div>
     <uc1:USite ID="USite1" runat="server" />
    </div>
    </form>
</body>
</html>
