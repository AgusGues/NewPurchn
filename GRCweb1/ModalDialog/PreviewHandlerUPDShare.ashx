<%@ WebHandler Language="C#" Class="PreviewHandlerUPDShare" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Domain;
using BusinessFacade;
using System.Collections;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;



public class PreviewHandlerUPDShare : IHttpHandler
{

    private byte[] bytes;

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string NamaFile = context.Request.QueryString["ba"];
            context.Response.Buffer = true;
            context.Response.Charset = "";
            if (context.Request.QueryString["dl"] == "1")
            {
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + NamaFile);
            }
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/pdf";
            context.Response.WriteFile("D:\\UPD_PDF_Temp\\" + Convert.ToString(NamaFile));          
            context.Response.Flush();
            context.Response.End();

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Could not find file"))
            {
                context.Response.WriteFile("D:\\UPD_PDF\\" + Convert.ToString("NotFound2.pdf"));
            }
            else
            {
                string NamaFile = context.Request.QueryString["ba"];             
            }                    
        }
    }    

    public bool IsReusable {
        get {
            return false;
        }
    }

}