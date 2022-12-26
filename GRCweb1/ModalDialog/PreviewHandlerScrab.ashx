<%@ WebHandler Language="C#" Class="PreviewHandlerScrab" %>

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


public class PreviewHandlerScrab : IHttpHandler
{
    private byte[] bytes;
    
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            int id = int.Parse(context.Request.QueryString["ba"]);
            string fileName = string.Empty;

            ScrubFacade updF = new ScrubFacade();
            Scrub zw = new Scrub();
            zw.ID = int.Parse(context.Request.QueryString["ba"]);
            ArrayList arrUPD = updF.RetrievePDF(id.ToString());

            foreach (Scrub md in arrUPD)
            {
                //bytes = md.attachfile;
                fileName = md.filename;


            }

            context.Response.Buffer = true;
            context.Response.Charset = "";
            if (context.Request.QueryString["dl"] == "1")
            {
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName.Trim());
            }

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/pdf";
            context.Response.WriteFile("D:\\DATA LAMPIRAN PURCHN\\SCRAB\\" + Convert.ToString(fileName));
            //context.Response.BinaryWrite(bytes);
            context.Response.Flush();
            context.Response.End();

           
            
        }
        catch (Exception ex)
        {
            throw;

        }
        
        
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    static public void DisplayAJAXMessage(Control page, string msg)
    {
        string myScript = "alert('" + msg + "');";
        ScriptManager.RegisterStartupScript(page, page.GetType(),
            "MyScript", myScript, true);
    }

}