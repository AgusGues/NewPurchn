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
    public partial class UploadFileUPDrev : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string IDmaster = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
                Session["IDmaster"] = IDmaster;

                ISO_UPD2Facade FacadeUPD = new ISO_UPD2Facade();
                ISO_UpdDMD DomainUPD = new ISO_UpdDMD();
                DomainUPD = FacadeUPD.RetrieveData(int.Parse(Request.QueryString["ba"].ToString()));

                string Revisi = DomainUPD.RevisiNo;
                Session["Revisi"] = Revisi;
                txtRevisiNo.Text = Revisi;
                txtTglBerlaku.Text = DomainUPD.TglBerlakuS;
                txtNamaDokumen.Text = DomainUPD.DocName.Trim();
                txtNo.Text = DomainUPD.NoDocument.Trim();

                string NamaFile = DomainUPD.NamaFile;
                Session["NamaFile"] = NamaFile;
                string TglBerlaku2 = DomainUPD.TglBerlakuSS;
                Session["TglBerlaku2"] = TglBerlaku2;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ISO_UpdDMD files = new ISO_UpdDMD();

            if (Upload1.HasFile)
            {
                Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                Upload1.PostedFile.SaveAs(Path.Combine(@"D:\UPD_PDF\", filename));

                if (ext.ToLower() == ".pdf")
                {
                    Stream fs = Upload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    ArrayList arrUPD = (ArrayList)Session["ListOfDept"];

                    files.IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                    int IDis = int.Parse(Request.QueryString["ba"].ToString());

                    int intResult = 0;
                    files.FileName = filename.ToString();
                    intResult = updF.UpdateDisFile(files);

                    if (intResult > 0)
                    {
                        int intResult2 = 0;
                        files.RevisiNo = txtUpdateRev.Text;
                        files.TglBerlaku = Convert.ToDateTime(txtUpdateTglBerlaku.SelectedDate);

                        intResult2 = updF.UpdateDisFile2(files);
                    }

                    CloseWindow(this);
                }
            }
            else
            {
                int RevisiNo = Convert.ToInt32(Session["Revisi"]);
                string NamaFile1 = Session["NamaFile"].ToString().Trim();
                int ID = Convert.ToInt32(Session["IDmaster"]);
                int intResult2 = 0;

                files.IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                //files.RevisiNo = txtUpdateRev.Text;

                if (txtUpdateNo.Text == "")
                {
                    files.NoDocument = txtNo.Text.Trim();
                }
                else
                {
                    files.NoDocument = txtUpdateNo.Text.Trim();
                }

                if (txtUpdateNamaDokumen.Text == "")
                {
                    files.DocName = txtNamaDokumen.Text.Trim();
                }
                else
                {
                    files.DocName = txtUpdateNamaDokumen.Text.Trim();
                }

                if (txtUpdateRev.Text == "")
                {
                    files.RevisiNo = RevisiNo.ToString();
                }
                else
                {
                    files.RevisiNo = txtUpdateRev.Text;
                }


                if (txtUpdateTglBerlaku.SelectedValue == null)
                {
                    files.TglBerlaku = Convert.ToDateTime(Session["TglBerlaku2"]);
                }
                else
                {
                    files.TglBerlaku = Convert.ToDateTime(txtUpdateTglBerlaku.SelectedDate);
                }

                files.NamaFile = NamaFile1.Trim();

                intResult2 = updF.UpdateDisFile2(files);

                if (intResult2 > 0)
                {
                    CloseWindow(this);
                }
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void MyButton_Click(object sender, EventArgs e)
        {

        }

        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
    }
}