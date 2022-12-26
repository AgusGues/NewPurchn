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
    public partial class UploadFileOrg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDept();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                if (ext.ToLower() == ".pdf")
                {
                    //if (ddlDept.SelectedIndex == 0) { return; }
                    Upload1.SaveAs(Server.MapPath(Path.Combine("~/App_Data/Org/", filename)));
                    #region not userd
                    /**
                     * di hilangkan karena database jadi besar simpan filenya
                     * 24/11/2016
                     */
                    int fileSize = Upload1.PostedFile.ContentLength;
                    //if (fileSize > 1024000)
                    //{
                    //    string txt = "File " + filename + " = " + (fileSize / 1024000).ToString("###,###.#0") + " MB";
                    //    lblMessage.Text = "Upload Document "+ddlDocument.SelectedValue+" gagal. Upload file size max 1 MB. " + txt;
                    //    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //    lblMessage.Font.Italic = true;
                    //    return;
                    //}
                    //Stream fs = Upload1.PostedFile.InputStream;

                    //BinaryReader br = new BinaryReader(fs);
                    //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    int tglMulai = 0;
                    int.TryParse(txtRevisi.Text, out tglMulai);
                    ZetroLib zl = new ZetroLib();
                    zl.hlp = new PESOrg();
                    zl.Option = "Insert";
                    zl.TableName = "ISO_ORG";
                    zl.Criteria = "DeptID,Revisi,MulaiBerlaku,FileName,CreatedBy,CreatedTime,RowStatus";
                    zl.StoreProcedurName = "spISO_ORG_Insert";
                    string rst = zl.CreateProcedure();
                    if (rst == string.Empty)
                    {
                        PESOrg ba = new PESOrg();
                        ba.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        ba.FileName = filename.ToString();
                        ba.MulaiBerlaku = DateTime.Now;
                        ba.RowStatus = 0;
                        ba.Revisi = tglMulai;
                        ba.CreatedBy = ((Users)Session["Users"]).UserName;
                        ba.CreatedTime = DateTime.Now;
                        zl.hlp = ba;
                        int rs = zl.ProcessData();
                        if (rs > 0)
                        {
                            Response.Write("<script language='javascript'>window.close();</script>");
                        }
                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Hanya file pdf yng bisa di uploadf')", true);
                    return;
                }
            }

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
        private void LoadDept()
        {
            ISO_PES ip = new ISO_PES();
            ArrayList arrData = new ArrayList();
            ip.Tahun = 2016;// int.Parse(ddlTahun.SelectedValue.ToString());
            arrData = ip.LoadDept();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--Pilih Dept--", "0"));
            foreach (PES2016 d in arrData)
            {
                ddlDept.Items.Add(new ListItem(d.DeptName, d.DeptID.ToString()));
            }
            ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
    }
}