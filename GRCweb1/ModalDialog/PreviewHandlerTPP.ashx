<%@ WebHandler Language="C#" Class="PreviewHandlerTPP" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Domain;
using BusinessFacade;
using System.Collections;
using Factory;


public class PreviewHandlerTPP : IHttpHandler {    
    
    public void ProcessRequest(HttpContext context)
    {
        int idTPP = int.Parse(context.Request.QueryString["ba"]);                     
        string fileName = "";
        try
        {
            TPP_Lampiran tppL = new TPP_Lampiran();
            TPP_LampiranFacade tppLF = new TPP_LampiranFacade();
            tppL.ID = int.Parse(context.Request.QueryString["ba"]);
            ArrayList arrTPP = tppLF.RetrieveLampiran(idTPP.ToString());

            foreach (TPP_Lampiran md in arrTPP)
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
            context.Response.WriteFile("D:\\TPPfile\\" + Convert.ToString(fileName));
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