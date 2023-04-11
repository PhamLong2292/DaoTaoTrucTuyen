<%@ WebHandler Language="C#" Class="UploadHandler" %>
using System;
using System.Web;

public class UploadHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        OneTSQ.UploadUtility.ProcessUploadHandlerUtility.ProcessRequestSessionUploadFile(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

