<%@ WebHandler Language="C#" Class="PreviewHandlerWO" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Domain;
using BusinessFacade;
using System.Collections;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;


public class PreviewHandlerWO : IHttpHandler {

    private byte[] bytes;
    
    public void ProcessRequest(HttpContext context)
    {
        int idLampiran = int.Parse(context.Request.QueryString["wrk"]);                     
        string fileName = "";

        WorkOrderFacade_New updF = new WorkOrderFacade_New();
        WorkOrder_New zw = new WorkOrder_New();
        zw.IDLampiran = int.Parse(context.Request.QueryString["wrk"]);
        ArrayList arrWO = updF.RetrieveLampiranWO(idLampiran.ToString());

        foreach (WorkOrder_New md in arrWO)
        {           
            fileName = md.FileName;
            bytes = md.FileLampiranOP;
        }
     
        context.Response.Buffer = true;
        context.Response.Charset = "";
        if (context.Request.QueryString["dl"] == "1")
        {
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName );
        }
        
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "application/pdf";
        context.Response.WriteFile("D:\\DATA LAMPIRAN PURCHN\\WO MTC\\" + Convert.ToString(fileName));
    
        context.Response.BinaryWrite(bytes);
        context.Response.Flush();
        context.Response.End();
        
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}