<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register src="UserControls/USite.ascx" tagname="USite" tagprefix="uc1" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        //string Data=OneTSQ.JSONP.OrderUtility.GetData();

        OneTSQ.Core.Model.RenderInfoCls ORenderInfo = new OneTSQ.Core.Model.RenderInfoCls();
        string SiteId = USite1.SiteId;
        string SiteLang = USite1.SiteLang;
        string UserSessionId = OneTSQ.Utility.WebEnvironments.GetUseSessionId() + System.Guid.NewGuid().ToString().Substring(0, 8);
        string SiteAssetLevelId = USite1.SiteAssetLevelId;
        string AssetLevelCode = USite1.AssetLevelCode;

        ORenderInfo.SiteId = SiteId;
        ORenderInfo.UserSessionId = UserSessionId;
        ORenderInfo.SiteLanguage = SiteLang;
        ORenderInfo.SiteAssetLevelId = SiteAssetLevelId;
        ORenderInfo.AssetLevelCode = AssetLevelCode;

        
        string ServiceId = OneTSQ.Utility.WebEnvironments.Request("ServiceId");
        string Page = OneTSQ.Utility.WebEnvironments.Request("page");
        string filter = OneTSQ.Utility.WebEnvironments.Request("filter");
        if (string.IsNullOrEmpty(Page))
        {
            Page = "0";
        }
        int iPage = int.Parse(Page);
        string q = HttpUtility.HtmlDecode(OneTSQ.Utility.WebEnvironments.Request("q"));
        
        OneTSQ.Core.Model.AjaxOut RetAjaxOut = null;
        if (string.IsNullOrEmpty(filter))
            RetAjaxOut = OneTSQ.Utility.RemoteDataUtility.Find(ServiceId).Reading(ORenderInfo, iPage, q);
        else RetAjaxOut = OneTSQ.Utility.RemoteDataUtility.Find(ServiceId).Reading(ORenderInfo, iPage, q, filter);
        
        Response.ClearHeaders();
        Response.ClearContent();
        Response.AddHeader("Content-type", "application/javascript");
        Response.BufferOutput = true;
        Response.Write(RetAjaxOut.HtmlContent);
    }
</script>
<uc1:USite ID="USite1" runat="server" />