<%@ WebHandler Language="C#" Class="PreviewHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Domain;
using BusinessFacade;

public class PreviewHandler : IHttpHandler {

    private byte[] bytes;
    public void ProcessRequest(HttpContext context)
    {
        int id = 0;
        int.TryParse(context.Request.QueryString["ba"].ToString(),out id);
        if (id > 0)
        {
            string fileName = "";
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select * from BeritaAcaraAttachment where RowStatus>-1 and ID=" + id.ToString();
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    bytes = (byte[])sdr["Attachment"];
                    fileName = sdr["FileName"].ToString();

                }
            }
        
        context.Response.Buffer = true;
        context.Response.Charset = "";
        if (context.Request.QueryString["dl"] == "1")
        {
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        }
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "application/pdf";
        //context.Response.WriteFile("D:\\UPD_PDF\\" + Convert.ToString(fileName));
        context.Response.BinaryWrite(bytes);
        context.Response.Flush();
        context.Response.End();
        }
        else
        {

        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}