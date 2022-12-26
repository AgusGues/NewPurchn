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
    public partial class UploadRMM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string idRMM = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
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

                //if (ext.ToLower() != ".pdf")
                //{
                //    lblMessage.Text = "Upload Document gagal. Upload file harus PDF. ";
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    lblMessage.Font.Italic = true;
                //    return;
                //}
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                Upload1.PostedFile.SaveAs("D:\\RMMfile\\" + UploadedFileName);
                RMM_LampiranFacade tppLF = new RMM_LampiranFacade();
                string idRMM = (Request.QueryString["ba"]);
                string IDLampiran = tppLF.GetIDlampiran(idRMM);
                string apv = tppLF.GetApvTPP(idRMM);

                //if (apv =="0")
                //{
                //    DisplayAJAXMessage(this, " TPP belum di approve Head ");
                //    return;
                //}

                //if (IDLampiran != string.Empty)
                //{
                //    DisplayAJAXMessage(this, " Upload Lampiran cukup sekali saja ");
                //    return;
                //}

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
                    RMM_Lampiran rmmLampir = new RMM_Lampiran();
                    RMM rmm = new RMM();
                    RMM_LampiranFacade rmmLampiranF = new RMM_LampiranFacade();

                    rmmLampir.CreatedBy = users.UserName;
                    rmmLampir.RMM_ID = int.Parse(Request.QueryString["ba"].ToString());
                    rmmLampir.FileName = MyString;

                    int intResult = 0;
                    intResult = rmmLampiranF.insertLampiran(rmmLampir);

                    if (rmmLampiranF.Error == string.Empty && intResult > 0)
                    {
                        //lblMessage.Text = "Upload Berhasil";
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