<%@ WebHandler Language="C#" Class="PreviewHandlerLapBulT13" %>

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


public class PreviewHandlerLapBulT13 : IHttpHandler {

    private byte[] bytes;
    
    public void ProcessRequest(HttpContext context)
    {
        int id = int.Parse(context.Request.QueryString["grp"]);                     
        string fileName = "";

        LaporanBulanan Group1 = new LaporanBulanan();
        LaporanBulananFacade GroupF = new LaporanBulananFacade();

        Group1.ID = int.Parse(context.Request.QueryString["grp"]);
        ArrayList arrList = GroupF.RetrieveFileNameT13(id.ToString());

        foreach (LaporanBulanan md in arrList)
        {
            //bytes = md.attachfile;
            fileName = md.FileName;
            
            
        }
     
        context.Response.Buffer = true;
        context.Response.Charset = "";
        if (context.Request.QueryString["dl"] == "1")
        {
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName );
        }
        
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "application/pdf";
        context.Response.WriteFile("D:\\Laporan\\" + Convert.ToString(fileName));
        //context.Response.BinaryWrite(bytes);
        context.Response.Flush();
        context.Response.End();
        
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}