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
    public partial class UploadFilePenjurian : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string BAID = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                if (CheckAttachment(UploadedFileName) == true)
                {
                    DisplayAJAXMessage(this, "File Attacment pernah di upload, ganti nama file terlebih dahulu");
                    return;
                }
                Upload1.PostedFile.SaveAs("D:\\Data Lampiran Purchn\\OJD pes\\" + UploadedFileName);
                TPP_LampiranFacade tppLF = new TPP_LampiranFacade();
                string idTPP = (Request.QueryString["ba"]);
                string IDLampiran = tppLF.GetIDlampiran(idTPP);
                string apv = tppLF.GetApvTPP(idTPP);
                try
                {
                    String pdfUrl = UploadedFileName;
                    if (pdfUrl.IndexOf("/") >= 0 || pdfUrl.IndexOf("\\") >= 0)
                    {
                        Response.End();
                    }
                    string tablename = Request.QueryString["tablename"].ToString();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "insert " + tablename + "(NIP, Tgl_penjurian, Attachment, Rowstatus, CreatedBy, CreatedTime" +
                        ")values(" + int.Parse(Request.QueryString["ba"].ToString()) +
                        ",'" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy-MM-dd") + "','" + UploadedFileName + "',0,'" + users.UserName + "',getdate())";
                    SqlDataReader sdr = zl.Retrieve();
                    CloseWindow(this);
                }
                catch (Exception ex)
                { Response.Write("An error occurred - " + ex.ToString()); lblMessage.Text = "Upload Data Gagal!"; }
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private bool CheckAttachment(string DocName)
        {
            string tablename = Request.QueryString["tablename"].ToString();
            bool rst = false;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select Attachment from " + tablename + " where RowStatus >-1 and Attachment='" + DocName + "'";
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
        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

    }
}