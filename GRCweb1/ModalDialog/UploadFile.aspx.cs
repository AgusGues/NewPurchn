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
    public partial class UploadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string BAID = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
                if (BAID == "")
                {
                    ddlDocument.Items.Clear();
                    ddlDocument.Items.Add(new ListItem("--pilih jenis document--", ""));
                    ddlDocument.Items.Add(new ListItem("Konfirmasi", "Konfirmasi"));
                    ddlDocument.SelectedValue = "Konfirmasi";
                    ddlDocument.Enabled = false;
                }
                else
                {
                    string NoSJ = (Request.QueryString["nosj"] != null) ? Request.QueryString["nosj"].ToString() : "";
                    string[] docName = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Document", "BeritaAcara").Split(',');
                    string doc = (Request.QueryString["tp"] != null) ? Request.QueryString["tp"].ToString() : "";
                    ddlDocument.Items.Clear();
                    ddlDocument.Items.Add(new ListItem("--pilih jenis document--", ""));
                    for (int i = 0; i < docName.Count(); i++)
                    {
                        if (CheckAttachment(docName[i], BAID) == false)
                        {
                            if (docName[i].ToString() == "Konfirmasi" && isSelisih(BAID) == false)
                            {
                                continue;
                            }
                            ddlDocument.Items.Add(new ListItem(docName[i], docName[i].ToString()));
                        }
                    }

                    if (doc == "")
                    {
                        ddlDocument.SelectedValue = "Konfirmasi";
                        ddlDocument.Enabled = false;
                    }
                    else
                    {
                        ddlDocument.SelectedIndex = 0;
                    }
                }
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ddlDocument.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Document Name harus di pilih')", true);
                return;
            }

            if (Upload1.HasFile)
            {
                string BAID = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";

                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                if (ext.ToLower() == ".pdf")
                {
                    Upload1.SaveAs(Server.MapPath(Path.Combine("~/App_Data/BA/", filename)));
                    if (BAID == "")
                    {
                        ZetroLib zl = new ZetroLib();
                        zl.hlp = new AttachmentBA();
                        zl.Option = "Insert";
                        zl.TableName = "BeritaAcaraAttachment";
                        zl.Criteria = "FileName,RowStatus,DocName,NoSJ,IPAddress,LastModifiedBy,LastModifiedTime";
                        zl.StoreProcedurName = "spBeritaAcaraAtt_Insert_22";
                        string rst = zl.CreateProcedure();
                        if (rst == string.Empty)
                        {
                            AttachmentBA ba = new AttachmentBA();
                            ba.NoSJ = (Request.QueryString["nosj"].ToString());
                            ba.FileName = filename.ToString();
                            //ba.Attachment = bytes;
                            ba.DocName = ddlDocument.SelectedValue.ToString();
                            ba.RowStatus = 0;
                            ba.LastModifiedBy = ((Users)Session["Users"]).UserName;
                            ba.LastModifiedTime = DateTime.Now;
                            ba.IPAddress = Global.GetIPAddress();
                            zl.hlp = ba;
                            int rs = zl.ProcessData();
                            if (rs > 0)
                            {
                                Response.Write("<script language='javascript'>window.close();</script>");
                            }
                        }
                    }
                    else
                    {
                        #region upload from berita acara
                        /**
                     * di hilangkan karena database jadi besar simpan filenya
                     * 24/11/2016
                     */
                        int fileSize = Upload1.PostedFile.ContentLength;
                        if (fileSize > 1024000)
                        {
                            string txt = "File " + filename + " = " + (fileSize / 1024000).ToString("###,###.#0") + " MB";
                            lblMessage.Text = "Upload Document " + ddlDocument.SelectedValue + " gagal. Upload file size max 1 MB. " + txt;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Font.Italic = true;
                            return;
                        }
                        Stream fs = Upload1.PostedFile.InputStream;

                        BinaryReader br = new BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        ZetroLib zl = new ZetroLib();
                        zl.hlp = new AttachmentBA();
                        zl.Option = "Insert";
                        zl.TableName = "BeritaAcaraAttachment";
                        zl.Criteria = "BAID,FileName,RowStatus,DocName,NoSJ,CreatedBy,CreatedTime";
                        zl.StoreProcedurName = "spBeritaAcaraAtt_Insert_21";
                        string rst = zl.CreateProcedure();
                        if (rst == string.Empty)
                        {
                            AttachmentBA ba = new AttachmentBA();
                            ba.BAID = int.Parse(Request.QueryString["ba"].ToString());
                            ba.FileName = filename.ToString();
                            //ba.Attachment = bytes;
                            ba.NoSJ = CheckNosj(BAID);
                            ba.DocName = ddlDocument.SelectedValue.ToString();
                            ba.RowStatus = 0;
                            ba.CreatedBy = ((Users)Session["Users"]).UserName;
                            zl.hlp = ba;
                            int rs = zl.ProcessData();
                            if (rs > 0)
                            {
                                Response.Write("<script language='javascript'>window.close();</script>");
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Hanya file pdf yng bisa di uploadf')", true);
                    return;
                }
            }

        }

        private string CheckNosj(string BAID)
        {
            string pros = "";
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select top 1 RIGHT(NoSJ,(CHARINDEX(',', REVERSE(NoSJ)))-1)NoSJ from BeritaAcaraDetail WHERE BAID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        pros = (sdr["NoSJ"].ToString());
                    }
                }
            }
            return pros;
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
        private bool isSelisih(string BAID)
        {
            bool selisih = false;
            decimal pros = 0;
            string MaxSelisih = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaxSelisih", "BeritaAcara");
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select ProsSelisih from BeritaAcara where RowStatus >-1 and ID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        pros = Convert.ToDecimal(sdr["ProsSelisih"].ToString());
                    }
                }
            }
            selisih = (decimal.Parse(MaxSelisih) > pros);
            return selisih;
        }
    }
}