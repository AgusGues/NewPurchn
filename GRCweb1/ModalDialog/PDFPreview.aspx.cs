using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using System.Configuration;
using System.Net;
using System.IO;

namespace GRCweb1.ModalDialog
{
    public partial class PDFPreview : System.Web.UI.Page
    {
        private byte[] bytes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string BAID = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";

                string NoSJ = (Request.QueryString["nosj"] != null) ? Request.QueryString["nosj"].ToString() : "";
                if (NoSJ != "")
                {
                    LoadPDF(NoSJ, true);
                }
                else
                {
                    if (BAID != "")
                    {
                        LoadPDF(BAID);
                    }
                }
                //Preview(BAID);
            }
        }
        private void LoadPDF(string ba)
        {
            string fileName = "";
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select * from BeritaAcaraAttachment where RowStatus>-1 and ID=" + ba;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (sdr["Attachment"] == DBNull.Value)
                    {
                        try
                        {
                            string FilePath = Server.MapPath(@"~/App_Data/BA/" + sdr["FileName"].ToString());
                            WebClient ws = new WebClient();
                            byte[] FileBuffer = ws.DownloadData(FilePath);
                            if (FileBuffer != null)
                            {
                                //Response.Clear();
                                //Response.Buffer = true;
                                //Response.Charset = "";
                                //Response.ContentType = "application/pdf";
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.BinaryWrite(FileBuffer);
                                Response.Flush();
                                //Response.End();
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    else
                    {
                        Preview(ba, true);
                    }
                    fileName = sdr["FileName"].ToString();

                }
            }
            // WebClient User = new WebClient();


        }
        private void LoadPDF(string nosj, bool depo)
        {
            DepoKertas dk = new DepoKertas();
            string fName = dk.CheckAtthConfirmasi(nosj);
            Preview(fName, false);
        }
        private void Preview(string FileName, bool db)
        {
            if (db == true)
            {
                string embed = "<object data=\"{0}{1}\" type=\"application/pdf\" width=\"100%\" height=\"550px\" style=\"overflow:auto\">";
                embed += "</object>";
                pdfView.Text = string.Format(embed, ResolveUrl("~/ModalDialog/PreviewHandler.ashx?ba="), FileName);
            }
            else
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"550px\" style=\"overflow:auto\">";
                embed += "</object>";
                pdfView.Text = string.Format(embed, ResolveUrl("~/App_Data/BA/" + FileName));
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}