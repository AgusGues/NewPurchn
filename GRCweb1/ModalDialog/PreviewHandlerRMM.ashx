<%@ WebHandler Language="C#" Class="PreviewHandlerRMM" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Domain;
using BusinessFacade;
using System.Collections;
using Factory;


public class PreviewHandlerRMM : IHttpHandler {    
    
    public void ProcessRequest(HttpContext context)
    {
        int idRMM = int.Parse(context.Request.QueryString["ba"]);                     
        string fileName = "";
        try
        {
            RMM_Lampiran rmmL = new RMM_Lampiran();
            RMM_LampiranFacade rmmLF = new RMM_LampiranFacade();
            rmmL.ID = int.Parse(context.Request.QueryString["ba"]);
            ArrayList arrRMM = rmmLF.RetrieveLampiran(idRMM.ToString());

            foreach (RMM_Lampiran md in arrRMM)
            {
                fileName = md.FileName;
            }

            context.Response.Buffer = true;
            context.Response.Charset = "";

            if (context.Request.QueryString["dl"] == "1")
            {
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            }

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/pdf";
            context.Response.WriteFile("D:\\RMMfile\\" + Convert.ToString(fileName));
            context.Response.Flush();
            context.Response.End();
        }
        catch { }       
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}