using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class UploadFileWO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string WOID = (Request.QueryString["wo"] != null) ? Request.QueryString["wo"].ToString() : "";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            WorkOrder work = new WorkOrder();
            WorkOrderFacade workF = new WorkOrderFacade();

            if (Upload1.HasFile)
            {
                Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                Upload1.PostedFile.SaveAs(Path.Combine(@"D:\DATA LAMPIRAN PURCHN\WO MTC\", filename));

                if (ext.ToLower() == ".pdf")
                {
                    Stream fs = Upload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);

                    work.WOID = int.Parse(Request.QueryString["wo"].ToString());
                    int ID = work.WOID;
                    if (ID != 0)
                    {
                        int intResult = 0;
                        work.WOID = ID;
                        work.FileName = filename.ToString();
                        work.CreatedBy = users.UserName;

                        intResult = workF.insertLampiranWO(work);
                    }

                    Response.Write("<script language='javascript'>window.close();</script>");
                }
            }
            //if (Upload1.HasFile)
            //{
            //    string WOID = (Request.QueryString["wo"] != null) ? Request.QueryString["wo"].ToString() : "";

            //    string FilePath = Upload1.PostedFile.FileName;
            //    string filename = Path.GetFileName(FilePath);
            //    string ext = Path.GetExtension(filename);
            //    if (ext.ToLower() == ".pdf")
            //    {
            //        Upload1.SaveAs(Server.MapPath(Path.Combine("~/App_Data/BA/", filename)));

            //        if (WOID != "")
            //        {
            //            WorkOrder work = new WorkOrder();
            //            WorkOrderFacade workF = new WorkOrderFacade();

            //            zl.hlp = new AttachmentBA();
            //            zl.Option = "Insert";
            //            zl.TableName = "BeritaAcaraAttachment";
            //            zl.Criteria = "FileName,RowStatus,DocName,NoSJ,IPAddress,LastModifiedBy,LastModifiedTime";
            //            zl.StoreProcedurName = "spBeritaAcaraAtt_Insert_22";
            //            string rst = zl.CreateProcedure();
            //            if (rst == string.Empty)
            //            {
            //                AttachmentBA ba = new AttachmentBA();
            //                ba.NoSJ = (Request.QueryString["nosj"].ToString());
            //                ba.FileName = filename.ToString();
            //                //ba.Attachment = bytes;
            //                ba.DocName = ddlDocument.SelectedValue.ToString();
            //                ba.RowStatus = 0;
            //                ba.LastModifiedBy= ((Users)Session["Users"]).UserName;
            //                ba.LastModifiedTime = DateTime.Now;
            //                ba.IPAddress = Global.GetIPAddress();
            //                zl.hlp = ba;
            //                int rs = zl.ProcessData();
            //                if (rs > 0)
            //                {
            //                    Response.Write("<script language='javascript'>window.close();</script>");
            //                }
            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Hanya file pdf yng bisa di uploadf')", true);
            //        return;
            //    }
            //}

        }


        private bool CheckAttachment(string DocName, string BAID)
        {
            bool rst = false;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select DocName from BeritaAcaraAttachment where RowStatus >-1 and DocName='" + DocName + "' and BAID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    rst = true;
                }
            }
            return rst;
        }
    }
}