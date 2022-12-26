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
using Factory;

namespace GRCweb1.ModalDialog
{
    public partial class UploadFileTPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string idTPP = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
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

                if (ext.ToLower() != ".pdf")
                {
                    lblMessage.Text = "Upload Document gagal. Upload file harus PDF. ";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Font.Italic = true;
                    return;
                }
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                Upload1.PostedFile.SaveAs("D:\\TPPfile\\" + UploadedFileName);
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

                    DateTime MyDate = DateTime.Now;
                    String MyString = pdfUrl;

                    // Upload Foto
                    TPP_Lampiran tppLampir = new TPP_Lampiran();
                    TPP tpp = new TPP();
                    TPP_LampiranFacade tppLampiranF = new TPP_LampiranFacade();

                    tppLampir.CreatedBy = users.UserName;
                    tppLampir.TPP_ID = int.Parse(Request.QueryString["ba"].ToString());
                    tppLampir.FileName = MyString;

                    int intResult = 0;
                    intResult = tppLampiranF.insertLampiran(tppLampir);

                    if (tppLampiranF.Error == string.Empty && intResult > 0)
                    {
                        DisplayAJAXMessage(this, "Upload Berhasil");
                        CloseWindow(this);
                    }
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

        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
    }
}