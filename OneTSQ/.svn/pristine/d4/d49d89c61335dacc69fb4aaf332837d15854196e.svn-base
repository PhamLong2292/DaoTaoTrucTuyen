<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register src="UserControls/USite.ascx" tagname="USite" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {

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
            string hangdoithuongid=OneTSQ.Utility.WebEnvironments.Request("hangdoithuongid");
            string hangdoiyeucauid=OneTSQ.Utility.WebEnvironments.Request("hangdoiyeucauid");
            string basiid=OneTSQ.Utility.WebEnvironments.Request("bacsiid");
            string q = HttpUtility.HtmlDecode(OneTSQ.Utility.WebEnvironments.Request("thongbao"));

            OneTSQ.Core.Model.IWebScreenRender
            WebScreenRender = OneTSQ.Utility.WebScreenRenderUtility.GetDefaultWebScreenRender(ORenderInfo);


            placeHolderContent.Controls.Clear();
            WebScreenRender.RegisterAjaxPro(ORenderInfo,this);
            OneTSQ.WebParts.ManHinhCho web = new OneTSQ.WebParts.ManHinhCho();
            web.RegAjaxPro(ORenderInfo, this);
            OneTSQ.Core.Model.AjaxOut ha = web.DrawManHinhCho(ORenderInfo, hangdoithuongid, hangdoiyeucauid,basiid,q);
            placeHolderContent.Controls.Clear();

            placeHolderContent.Controls.Add(new LiteralControl(ha.HtmlContent));

        }
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<!-- BEGIN HEAD -->
<head runat="server">
    <meta charset="utf-8" />
    <title>OneTSQ</title>
     
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	
    <link href="../../../../themes/css/bootstrap.min.css" rel="stylesheet">
   

    <link href="../../../../themes/css/hangchongang.css" rel="stylesheet" />
    <link href="../../../../themes/css/plugins/toastr/toastr.min.css" rel="stylesheet">


</head>

<body>
   <form runat="server">
      <uc1:USite ID="USite1" runat="server" />sss
    </form>
    
     <!-- Mainly scripts -->
    <script type="text/javascript" src="../../../../themes/js/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../../../../themes/js/bootstrap.min.js"></script>
    
    <script src="../../../../themes/js/pub-sub-client.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/3.1.7/signalr.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
    
    <!-- Toastr -->
    <script src="../../../../themes/js/plugins/toastr/toastr.min.js"></script>

    <asp:PlaceHolder ID="placeHolderContent" runat="server"></asp:PlaceHolder>
    
    


</body>
</html>
  
	