<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register src="UserControls/USite.ascx" tagname="USite" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {

        OneTSQ.Core.Model.RenderInfoCls ORenderInfo = new OneTSQ.Core.Model.RenderInfoCls();
        string SiteId = USite1.SiteId;
        string SiteLang = Request["lang"];
        if (string.IsNullOrEmpty(SiteLang))
        {
            SiteLang = USite1.SiteLang;
        }
        string usid = Request["usid"];
        if (string.IsNullOrEmpty(usid))
        {
            usid = OneTSQ.Utility.WebEnvironments.GetUseSessionId()+System.Guid.NewGuid().ToString().Substring(0, 8);
        }
        string UserSessionId = usid;
        string SiteAssetLevelId = USite1.SiteAssetLevelId;
        string AssetLevelCode = USite1.AssetLevelCode;
        
     
        ORenderInfo.SiteId = SiteId;
        ORenderInfo.UserSessionId = UserSessionId;
        ORenderInfo.SiteLanguage = SiteLang;
        ORenderInfo.SiteAssetLevelId = SiteAssetLevelId;
        ORenderInfo.AssetLevelCode = AssetLevelCode;
        
        //bool IsUserLogin = OneTSQ.Utility.WebSessionUtility.IsUserLogin(ORenderInfo);
        //if (IsUserLogin == false)
        //{
        //    Response.Redirect("login.aspx");
        //}
        //ORenderInfo.OwnerId = OneTSQ.Utility.WebEnvironments.GetOwnerId(ORenderInfo);
        //ORenderInfo.RoleId = OneTSQ.Utility.WebSessionUtility.GetStdRoleAutoId(ORenderInfo);

        Response.Write(OneTSQ.WebParts.QuotationPreviewUtility.Preview(ORenderInfo).HtmlContent);
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head runat="server">
    <meta charset="utf-8" />
    <title>PREVIEW</title>
     
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />

    
</head>

<body>
        
   <form runat="server">
      <uc1:USite ID="USite1" runat="server" />
    </form>
        
    <script type="text/javascript" src = "http://code.jquery.com/jquery-latest.js" ></script>
        <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                $('img').each(function () {

                    $(this).removeAttr('width')
                    $(this).removeAttr('height');
                });
            });
		</script>
	

</body>
</html>
  
	