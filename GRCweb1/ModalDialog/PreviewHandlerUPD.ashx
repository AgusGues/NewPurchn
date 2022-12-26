<%@ WebHandler Language="C#" Class="PreviewHandlerUPD" %>

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


public class PreviewHandlerUPD : IHttpHandler {

    private byte[] bytes;
    
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            int id = int.Parse(context.Request.QueryString["ba"]);
            string fileName = "";

            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ISO_UpdDMD zw = new ISO_UpdDMD();
            zw.ID = int.Parse(context.Request.QueryString["ba"]);
            ArrayList arrUPD = updF.RetrievePDF(id.ToString());

            foreach (ISO_UpdDMD md in arrUPD)
            {
                //bytes = md.attachfile;
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
            context.Response.WriteFile("D:\\UPD_PDF\\" + Convert.ToString(fileName));
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