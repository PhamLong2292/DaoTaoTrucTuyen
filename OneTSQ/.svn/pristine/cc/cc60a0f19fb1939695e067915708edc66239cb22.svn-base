<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register src="UserControls/USite.ascx" tagname="USite" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        OneTSQ.Core.Model.RenderInfoCls ORenderInfo = new OneTSQ.Core.Model.RenderInfoCls();
        string SiteId = USite1.SiteId;
        string SiteLang=USite1.SiteLang;
        string UserSessionId = OneTSQ.Utility.WebEnvironments.GetUseSessionId() + System.Guid.NewGuid().ToString().Substring(0,8);
        string SiteAssetLevelId = USite1.SiteAssetLevelId;
        string AssetLevelCode = USite1.AssetLevelCode;

        ORenderInfo.SiteId = SiteId;
        ORenderInfo.UserSessionId = UserSessionId;
        ORenderInfo.SiteLanguage=SiteLang;
        ORenderInfo.SiteAssetLevelId = SiteAssetLevelId;
        ORenderInfo.AssetLevelCode = AssetLevelCode;

        OneTSQ.Core.Model.IWebScreenRender
            WebScreenRender = OneTSQ.Utility.WebScreenRenderUtility.GetDefaultWebScreenRender(ORenderInfo);

        placeHolderContent.Controls.Clear();
        WebScreenRender.RegisterAjaxPro(ORenderInfo,this);

        OneTSQ.Core.Model.SiteParam 
            OSiteParam = OneTSQ.Utility.WebEnvironments.CreateSiteParam(ORenderInfo);
        OneTSQ.Utility.WebSessionUtility.SetCurrentLoginUser(OSiteParam, null);
        OneTSQ.Core.Model.AjaxOut OAjaxOut = WebScreenRender.DrawLoginPage(ORenderInfo);
        placeHolderContent.Controls.Clear();
        placeHolderContent.Controls.Add(new LiteralControl(OAjaxOut.HtmlContent));

    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head runat="server">
    <meta charset="utf-8" />
    <title>OneTSQ - ONENET</title>
     
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	
    <link href="../../../../themes/css/bootstrap.min.css" type="text/css" rel="stylesheet">
    <link href="../../../../themes/font-awesome/css/font-awesome.css" type="text/css" rel="stylesheet">

    <link href="../../../../themes/css/animate.css" type="text/css" rel="stylesheet">
    <link href="../../../../themes/css/style.css" type="text/css" rel="stylesheet">

    <!-- Sweet Alert -->
    <link href="../../../../themes/css/plugins/sweetalert/sweetalert.css" type="text/css" rel="stylesheet">

    <script src="../../../../themes/js/store.min.js"></script>
</head>

    <body class="gray-bg">

    

    

    <uc1:USite ID="USite1" runat="server" />
    <form runat="server">
        
    </form>
    <asp:PlaceHolder ID="placeHolderContent" runat="server"></asp:PlaceHolder>
     <!-- Mainly scripts -->
    <script src="../../../../themes/js/jquery-2.1.1.js"></script>
    <script src="../../../../themes/js/bootstrap.min.js"></script>
    <script src="../../../../themes/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../../../../themes/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="../../../../themes/js/inspinia.js"></script>
    <script src="../../../../themes/js/plugins/pace/pace.min.js"></script>
    
    <!-- Sweet alert -->
    <script src="../../../../themes/js/plugins/sweetalert/sweetalert.min.js"></script>

    
        
</body>

</html>
  
	