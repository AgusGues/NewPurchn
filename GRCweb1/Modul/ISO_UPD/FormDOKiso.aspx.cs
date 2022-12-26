using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormDOKiso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDataGridViewItem(LoadDataGrid());
                clearForm();
                txtBerlaku.Text = "Tentukan tanggal Berlaku";
            }
        }

        private void LoadDataGridViewItem(ArrayList arrUPD)
        {
            this.GridView1.DataSource = arrUPD;
            this.GridView1.DataBind();
        }

        private ArrayList LoadDataGrid()
        {
            PanelGrid.Visible = false;
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            arrUPD = updF.RetrieveISO();
            Session["arrUPD"] = arrUPD;
            if (arrUPD.Count > 0)
            {
                return arrUPD;
            }

            arrUPD.Add(new ISO_Upd());
            return arrUPD;

        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtNO.Text = string.Empty;
            txtISO.Text = string.Empty;
            txtNama.Text = string.Empty;
            txtBerlaku.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            txtRev.Text = string.Empty;
            txtDOK.Text = string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "1")
                {
                    e.Row.Cells[4].Text = "Pedoman Mutu";
                }
                if (e.Row.Cells[4].Text == "2")
                {
                    e.Row.Cells[4].Text = "Instruksi Kerja";
                }
                if (e.Row.Cells[4].Text == "3")
                {
                    e.Row.Cells[4].Text = "Form";
                }
                if (e.Row.Cells[4].Text == "4")
                {
                    e.Row.Cells[4].Text = "Prosedur";
                }
                if (e.Row.Cells[4].Text == "5")
                {
                    e.Row.Cells[4].Text = "Rencana Mutu";
                }
                if (e.Row.Cells[4].Text == "6")
                {
                    e.Row.Cells[4].Text = "Standar";
                }
                if (e.Row.Cells[4].Text == "9")
                {
                    e.Row.Cells[4].Text = "Bagan Alir";
                }
                if (e.Row.Cells[5].Text == "2")
                {
                    e.Row.Cells[5].Text = "BoardMill";
                }
                if (e.Row.Cells[5].Text == "3")
                {
                    e.Row.Cells[5].Text = "Finishing";
                }
                if (e.Row.Cells[5].Text == "6")
                {
                    e.Row.Cells[5].Text = "Log. Produk Jadi";
                }
                if (e.Row.Cells[5].Text == "7")
                {
                    e.Row.Cells[5].Text = "HRD";
                }
                if (e.Row.Cells[5].Text == "9")
                {
                    e.Row.Cells[5].Text = "QA";
                }
                if (e.Row.Cells[5].Text == "10")
                {
                    e.Row.Cells[5].Text = "Log. Bahan Baku";
                }
                if (e.Row.Cells[5].Text == "11")
                {
                    e.Row.Cells[5].Text = "PPIC";
                }
                if (e.Row.Cells[5].Text == "14")
                {
                    e.Row.Cells[5].Text = "EDP(ELECTRONONIC DATA PROCESSING)";
                }
                if (e.Row.Cells[5].Text == "15")
                {
                    e.Row.Cells[5].Text = "Purchasing";
                }
                if (e.Row.Cells[5].Text == "23")
                {
                    e.Row.Cells[5].Text = "ISO";
                }
                if (e.Row.Cells[5].Text == "26")
                {
                    e.Row.Cells[5].Text = "Delivery";
                }
                if (e.Row.Cells[5].Text == "13")
                {
                    e.Row.Cells[5].Text = "Marketing";
                }
                if (e.Row.Cells[5].Text == "4")
                {
                    e.Row.Cells[5].Text = "Maintenance";
                }
                if (e.Row.Cells[5].Text == "19")
                {
                    e.Row.Cells[5].Text = "Maintenance";
                }
                if (e.Row.Cells[5].Text == "30" || e.Row.Cells[5].Text == "31")
                {
                    e.Row.Cells[5].Text = "RnD";
                }
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            LoadDataGridViewItem(LoadDataGrid());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            ISO_Upd upd1 = new ISO_Upd();
            ArrayList arrUPDNew = new ArrayList();
            arrUPDNew = (ArrayList)Session["arrUPD"];

            if (e.CommandName == "Add")
            {
                PanelGrid.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                int IDMaster = int.Parse(row.Cells[0].Text);

                ISO_DMDFacade FacadeUPD0 = new ISO_DMDFacade();
                ISO_UpdDMD DomainUPD0 = new ISO_UpdDMD();
                string NamaFileDok = FacadeUPD0.RetrieveNamaFile(int.Parse(row.Cells[0].Text));

                ISO_DMDFacade FacadeUPD2 = new ISO_DMDFacade();
                ISO_UpdDMD DomainUPD2 = new ISO_UpdDMD();
                DomainUPD2.ID = IDMaster;
                int Cek = FacadeUPD2.CekShare(IDMaster);

                if (Cek > 0)
                {
                    ISO_DMDFacade FacadeUPD = new ISO_DMDFacade();
                    ISO_UpdDMD DomainUPD = new ISO_UpdDMD();
                    DomainUPD = FacadeUPD.RetrieveData(IDMaster);

                    Session["StatusShare"] = 0;
                    //Session["StatusShare"] = DomainUPD.StatusShare; 
                    Session["IDMaster"] = IDMaster;
                    Session["CategoryUPD"] = DomainUPD.CategoryUPD;
                    Session["DeptID"] = DomainUPD.Dept;
                    Session["UpdID"] = 0;
                    //Session["PlantID"] = users.UnitKerjaID;
                    Session["PlantID"] = DomainUPD.PlantID;

                    txtKategoriUPD.Value = DomainUPD.CategoryUPD;
                    txtType.Value = DomainUPD.Type;
                    txtDeptID.Value = DomainUPD.Dept;
                }
                else
                {

                    ISO_DMDFacade FacadeUPD3 = new ISO_DMDFacade();
                    ISO_UpdDMD DomainUPD3 = new ISO_UpdDMD();
                    //int Rev = FacadeUPD3.RetrieveRev(IDMaster);
                    DomainUPD3 = FacadeUPD3.RetrieveRev(IDMaster);

                    Session["Revisi"] = DomainUPD3.RevisiNo;
                    Session["UpdID"] = int.Parse(row.Cells[0].Text).ToString();
                    Session["StatusShare"] = 11;
                    Session["PlantID"] = users.UnitKerjaID;
                    Session["IDmaster"] = DomainUPD3.IDmaster;

                    txtKategoriUPD.Value = DomainUPD3.CategoryUPD;
                    txtType.Value = DomainUPD3.Type;
                    txtDeptID.Value = DomainUPD3.DeptID;
                }

                int Revisi = Convert.ToInt32(Session["Revisi"]);

                if (Revisi > 0)
                {
                    int i = 0;
                    i = Revisi + 1;
                    txtRev.Text = i.ToString();
                }
                else
                {
                    txtRev.Text = "0";
                }

                //if (arrUPDNew.Count > 0)
                //{
                //    foreach (ISO_Upd Data in arrUPDNew)
                //    {
                //        txtIDMaster.Value = Data.IDmaster.ToString();
                //        txtType.Value = Data.Type.ToString();
                //        txtDeptName.Value = Data.DeptName.Trim();
                //        txtDeptID.Value = Data.DeptID.ToString();
                //        txtNamaFile2.Value = Data.NamaFile.Trim().Replace("&","dan");
                //        txtKategoriUPD.Value = Data.IDCatUPD.ToString();  
                //    }
                //} 

                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtNO.Text = CekString(row.Cells[1].Text);
                txtNama.Text = CekString(row.Cells[3].Text).Replace("&amp;", "&");
                txtDOK.Text = CekString(row.Cells[4].Text);
                txtISO.Text = CekString(row.Cells[2].Text);
                Session["NamaDokumen"] = txtNamaFile2.Value;
                Session["NamaFileLampiran"] = NamaFileDok.Trim();

                if (NamaFileDok == "")
                {
                    PanelGrid.Visible = false;
                }
                else if (NamaFileDok != "")
                {
                    LoadFile();
                    PanelGrid.Visible = true;
                }
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            int userDeptID = users.DeptID;
            string NamaDept = string.Empty;
            string DeptID1 = Session["DeptID1"].ToString();


            if (DeptID1 != "0")
            {
                if (DeptID1 == "23") { NamaDept = "ISO"; }
                else if (DeptID1 == "14") { NamaDept = "IT"; }
                else if (DeptID1 == "7") { NamaDept = "HRD"; }
                else if (DeptID1 == "15") { NamaDept = "Purchasing"; }
                else if (DeptID1 == "26") { NamaDept = "Transportation"; }
                else if (DeptID1 == "13") { NamaDept = "Marketing"; }
                else if (DeptID1 == "11") { NamaDept = "PPIC"; }
                else if (DeptID1 == "6") { NamaDept = "LogistikBJ"; }
                else if (DeptID1 == "10") { NamaDept = "LogistikBB"; }
                else if (DeptID1 == "3") { NamaDept = "Finishing"; }
                else if (DeptID1 == "2") { NamaDept = "BoardMill"; }
                else if (DeptID1 == "30") { NamaDept = "RnD"; }
                else if (DeptID1 == "31") { NamaDept = "RnD"; }
                else if (DeptID1 == "19" || DeptID1 == "4" || DeptID1 == "5" || DeptID1 == "18") { NamaDept = "Maintenance"; }
                else { NamaDept = "QA"; }
            }

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "filename="
                + e.CommandArgument);
            Response.TransmitFile(Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept + "/") + e.CommandArgument);
            Response.Flush();
            LoadFile();
            Response.Clear();
        }

        private void LoadFile()
        {
            Users users = (Users)Session["Users"];
            int userDeptID = users.DeptID;
            string NamaDept = string.Empty;
            string UpdID = Session["UpdID"].ToString();

            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();

            upd1 = dmdF.cekDept(UpdID);
            Session["DeptID1"] = upd1.DeptID;

            if (upd1.DeptID != 0)
            {
                if (upd1.DeptID == 23) { NamaDept = "ISO"; }
                else if (upd1.DeptID == 14) { NamaDept = "IT"; }
                else if (upd1.DeptID == 7) { NamaDept = "HRD"; }
                else if (upd1.DeptID == 15) { NamaDept = "Purchasing"; }
                else if (upd1.DeptID == 26) { NamaDept = "Transportation"; }
                else if (upd1.DeptID == 13) { NamaDept = "Marketing"; }
                else if (upd1.DeptID == 11) { NamaDept = "PPIC"; }
                else if (upd1.DeptID == 6) { NamaDept = "LogistikBJ"; }
                else if (upd1.DeptID == 10) { NamaDept = "LogistikBB"; }
                else if (upd1.DeptID == 3) { NamaDept = "Finishing"; }
                else if (upd1.DeptID == 2) { NamaDept = "BoardMill"; }
                else if (upd1.DeptID == 30) { NamaDept = "RnD"; }
                else if (upd1.DeptID == 31) { NamaDept = "RnD"; }
                else if (upd1.DeptID == 19 || upd1.DeptID == 4 || upd1.DeptID == 5 || upd1.DeptID == 18) { NamaDept = "Maintenance"; }
                else { NamaDept = "QA"; }
            }

            //Session["NamaDokumen"] = txtNamaFile2.Value;
            //string Nama = txtNamaFile2.Value;
            string Nama = Session["NamaFileLampiran"].ToString();

            if (Nama != string.Empty)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("File");
                dt.Columns.Add("Date");

                //DirectoryInfo info = new DirectoryInfo(Server.MapPath("~/App_Data/UploadUPD/" + NamaDept + "/"));
                DirectoryInfo info = new DirectoryInfo(Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept));
                FileInfo[] files = info.GetFiles(Nama);

                foreach (FileInfo file in files)
                {
                    dt.Rows.Add(file.Name, file.CreationTime);
                }

                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("File");
                dt1.Columns.Add("Date");

                GridView2.DataSource = dt1;
                GridView2.DataBind();
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbn1 = e.Row.FindControl("LinkButton1") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lbn1);
                }
            }
            catch (Exception ex)
            { }
        }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int StatusShare = Convert.ToInt32(Session["StatusShare"]);
            int ID = Convert.ToInt32(Session["IDMaster"]);
            int intResult = 0;
            string strEvent = "Insert";
            Users users = (Users)Session["Users"];
            ISO_UpdDMD updDMD = new ISO_UpdDMD();
            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();
            ISO_MasterUPD Md = new ISO_MasterUPD();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            if (ViewState["id"] != null)
            {
                updDMD.ID = int.Parse(ViewState["id"].ToString());
                updDMD.NoDocument = txtNO.Text;
            }

            upd1.ID = ID;
            updDMD.ID = Convert.ToInt32(Session["UpdID"]);
            updDMD.DocName = txtNama.Text;
            updDMD.NoDocument = txtISO.Text;

            if (txtBerlaku.Text == "Tentukan tanggal Berlaku")
            {
                DisplayAJAXMessage(this, "Tanggal Berlaku Belum di tentukan ! "); return;
            }

            updDMD.TglBerlaku = DateTime.Parse(txtBerlaku.Text);
            updDMD.CreatedBy = ((Users)Session["Users"]).UserName;
            updDMD.LastModifiedBy = ((Users)Session["Users"]).UserName;
            upd1.NoDokumen = txtISO.Text;
            updDMD.RevisiNo = txtRev.Text;
            upd1.LastModifiedBy = ((Users)Session["Users"]).UserName;
            updDMD.IDmaster = ID;
            updDMD.LinkID = int.Parse(ViewState["id"].ToString());
            updDMD.PlantID = users.UnitKerjaID;
            updDMD.StatusShare = Convert.ToInt32(Session["StatusShare"]);
            string Kategory = txtDOK.Text;
            updDMD.PlantID = Convert.ToInt32(Session["PlantID"]);
            updDMD.UnitKerjaID = users.UnitKerjaID;

            updDMD.CategoryUPD = txtKategoriUPD.Value;
            //upd1.IDmaster = txtIDMaster.Value;  
            upd1.IDmaster = Session["IDMaster"].ToString();
            updDMD.Type = txtType.Value;

            updDMD.DeptID = txtDeptID.Value;

            if (StatusShare == 2 || StatusShare == 4 || StatusShare == 0)
            {
                intResult = dmdF.InsertShare(updDMD);
                intResult = Md.UpdateShare(upd1);
                //intResult = dmdF.Insert(updDMD);
                intResult = Md.Update(upd1);
            }
            else
            {
                intResult = dmdF.Insert(updDMD);      // Insert ke table iso_upddmd 1
                intResult = dmdF.Update(upd1);        // Update status menjadi 1 di table iso_upd 
                intResult = Md.Update(upd1);          // Update aktif menjadi 0 di table iso_upddmd ( non aktif ) 2
                intResult = dmdF.UpdateApvISO(upd1);  // Update apv menjadi 6 di table iso_upd level approval terakhir      

            }

            clearForm();
            LoadDataGridViewItem(LoadDataGrid());

        }

        protected void btnHapus_ServerClick(object sender, EventArgs e)
        {
            {

                DisplayAJAXMessage(this, "Anda akan menon Aktifkan Dokumen ini");

            }


            int intResult = 0;
            string strEvent = "Insert";
            Users users = new Users();
            ISO_UpdDMD updDMD = new ISO_UpdDMD();
            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();
            ISO_MasterUPD Md = new ISO_MasterUPD();
            if (ViewState["id"] != null)
            {
                updDMD.ID = int.Parse(ViewState["id"].ToString());
                updDMD.NoDocument = txtNO.Text;
                //strEvent = "Edit";
            }


            updDMD.DocName = txtNama.Text;
            updDMD.NoDocument = txtISO.Text;
            updDMD.TglBerlaku = DateTime.Parse(txtBerlaku.Text);
            updDMD.CreatedBy = ((Users)Session["Users"]).UserName;
            updDMD.LastModifiedBy = ((Users)Session["Users"]).UserName;
            upd1.NoDokumen = txtISO.Text;
            updDMD.RevisiNo = txtRev.Text;
            updDMD.CategoryUPD = txtDOK.Text;
            if (updDMD.CategoryUPD == "Pedoman Mutu")
                updDMD.CategoryUPD = "1";
            if (updDMD.CategoryUPD == "Instruksi Kerja")
                updDMD.CategoryUPD = "2";
            if (updDMD.CategoryUPD == "Form")
                updDMD.CategoryUPD = "3";
            if (updDMD.CategoryUPD == "Prosedure")
                updDMD.CategoryUPD = "4";
            if (updDMD.CategoryUPD == "Rencana Mutu")
                updDMD.CategoryUPD = "5";
            if (updDMD.CategoryUPD == "Standard")
                updDMD.CategoryUPD = "6";
            //upd1.IDmaster = txtMaster.Text;
            // txtMaster.Text = updDMD.ID.ToString();
            upd1.IDmaster = txtIDMaster.Value;


            //intResult = dmdF.Insert(updDMD);
            //intResult = dmdF.UpdateH(upd1);
            intResult = Md.UpdateHapus(upd1);


            clearForm();
            LoadDataGridViewItem(LoadDataGrid());

        }
    }
}