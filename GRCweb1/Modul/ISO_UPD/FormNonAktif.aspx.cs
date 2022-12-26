using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;
using System.Collections;
using System.Net;
using DataAccessLayer;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormNonAktif : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDataGridViewItem(LoadDataGrid());
                clearForm();
                txtBerlaku.Text = "Tentukan tanggal Non Aktif !!"; btnHapus.Disabled = true;
            }
        }

        private void LoadDataGridViewItem(ArrayList arrUPD)
        {
            this.GridView1.DataSource = arrUPD;
            this.GridView1.DataBind();
        }

        protected void RBManualHapus_CheckedChanged(object sender, EventArgs e)
        {
            PanelPilihan.Visible = true; Panel1.Visible = false; RBBiasa.Checked = false; RBKhusus.Checked = false; RBAwal.Checked = false;
            PanelLampiran.Visible = false; PanelButtonHapus.Visible = false; PanelGridPanel1.Visible = false;
        }

        protected void RBBiasa_CheckedChanged(object sender, EventArgs e)
        {
            Panel2Biasa.Visible = true; Panel2Khusus.Visible = false; RBKhusus.Checked = false; btnHapusBiasa.Disabled = true;
        }

        protected void RBKhusus_CheckedChanged(object sender, EventArgs e)
        {
            Panel2Biasa.Visible = false; Panel2Khusus.Visible = true; RBBiasa.Checked = false; btnHapusKhusus.Disabled = true;
        }

        protected void RBAwal_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataGridViewItem(LoadDataGrid());
            Panel1.Visible = true; Panel2Biasa.Visible = false; Panel2Khusus.Visible = false; PanelPilihan.Visible = false; RBManualHapus.Checked = false;
            PanelLampiran.Visible = false; PanelButtonHapus.Visible = true; PanelGridPanel1.Visible = true;
        }

        private ArrayList LoadDataGrid()
        {
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            arrUPD = updF.RetrieveISOMusnah();
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
            //txtRev.Text = string.Empty;
            //txtMaster.Text = string.Empty;
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

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            LoadDataGridViewItem(LoadDataGrid());
        }
        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string Nama = e.CommandArgument.ToString();
                string embed = "<object data=\"{0}{1}\"type=\"application/pdf\" width=\"100%\" height=\"550px\" >";
                embed += "</object>";
                pdfView.Text = string.Format(embed, ResolveUrl("~/ModalDialog/PreviewHandlerUPDShare.ashx?ba="), Nama);
            }
        }
        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbn2 = e.Row.FindControl("LinkButton2") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lbn2);
                }
            }
            catch (Exception ex)
            { }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                btnHapus.Disabled = false;
                LabelLampiran.Visible = true;
                PanelLampiran.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtNO.Text = CekString(row.Cells[1].Text);
                txtNama.Text = CekString(row.Cells[3].Text);
                txtDOK.Text = CekString(row.Cells[4].Text);
                txtISO.Text = CekString(row.Cells[2].Text);

                ISO_UpdDMD DomainDMD = new ISO_UpdDMD();
                ISO_DMDFacade FacadeDMD = new ISO_DMDFacade();
                DomainDMD = FacadeDMD.RetrieveAlasan(CekString(row.Cells[1].Text));
                txtAlasan.Text = DomainDMD.Alasan.Trim(); txtAlasan.Enabled = false;
                Session["idmaster"] = DomainDMD.IDmaster;
                Session["IDupd"] = DomainDMD.ID;

                ISO_UpdFacade upd4 = new ISO_UpdFacade();
                ISO_Upd sp4 = new ISO_Upd();
                ArrayList arrUPD4 = upd4.RetrieveFilePendukung(DomainDMD.IDmaster);

                GridView4.DataSource = arrUPD4;
                GridView4.DataBind();
            }
        }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int intResult = 0;
            string strEvent = "Insert";
            Users users = (Users)Session["Users"];
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
            //updDMD.RevisiNo = txtRev.Text;
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
            //txtMaster.Text = updDMD.ID.ToString();

            intResult = dmdF.Insert(updDMD);
            intResult = dmdF.Update(upd1);
            intResult = Md.Update(upd1);

            clearForm();
            LoadDataGridViewItem(LoadDataGrid());

        }
        protected void btncek_ServerClick(object sender, EventArgs e)
        {
            ISO_UpdDMD DomainDMD2 = new ISO_UpdDMD();
            ISO_DMDFacade FacadeDMD2 = new ISO_DMDFacade();
            DomainDMD2 = FacadeDMD2.RetrieveBiasa(txtNomor.Text.Trim());

            if (DomainDMD2.IDmaster > 0)
            {
                txtNomorisi.Text = DomainDMD2.NoDocument;
                txtNamaisi.Text = DomainDMD2.DocName;
                txtKategoriisi.Text = DomainDMD2.CategoryUPD;
                txtDeptisi.Text = DomainDMD2.DeptName;
                txtNonAktifisi.Text = "Tanggal harus di pilih !!";
                txtIDmaster2.Text = DomainDMD2.IDmaster.ToString();
                btnHapusBiasa.Disabled = false;
            }
            else
            {
                DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                return;
            }
        }
        protected void btncekkhusus_ServerClick(object sender, EventArgs e)
        {
            ISO_UpdDMD DomainDMD4 = new ISO_UpdDMD();
            ISO_DMDFacade FacadeDMD4 = new ISO_DMDFacade();
            DomainDMD4 = FacadeDMD4.RetrieveKhusus(txtKhusus.Text.Trim());

            if (DomainDMD4.IDmaster > 0)
            {
                txtNomorisiKhusus.Text = DomainDMD4.NoDocument;
                txtNamaisiKhusus.Text = DomainDMD4.DocName;
                txtKategoriisiKhusus.Text = DomainDMD4.CategoryUPD;
                txtDeptisiKhusus.Text = DomainDMD4.DeptName;
                txtTglNonisiKhusus.Text = "Tanggal harus di pilih !!";
                txtNamaDokumen.Text = DomainDMD4.DocName;
                btnHapusKhusus.Disabled = false;
            }
            else
            {
                DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                return;
            }
        }
        protected void btnHapusBiasa_ServerClick(object sender, EventArgs e)
        {
            if (txtNonAktifisi.Text == "Tanggal harus di pilih !!")
            {
                DisplayAJAXMessage(this, "Tanggal Non Aktif belum di pilih !!");
                return;
            }

            if (txtAlasanisi.Text == "")
            {
                DisplayAJAXMessage(this, "Alasan harus diisi !!");
                return;
            }

            Users users = (Users)Session["Users"];
            ISO_Upd DomainDMD3 = new ISO_Upd();
            ISO_MasterUPD FacadeDMD3 = new ISO_MasterUPD();
            int intResult = 0;
            DomainDMD3.IDmaster = txtIDmaster2.Text;
            DomainDMD3.LastModifiedBy = users.UserName;
            DomainDMD3.Alasan = txtAlasanisi.Text;

            intResult = FacadeDMD3.UpdateHapusISO(DomainDMD3);

            if (intResult > -1)
            {
                LabelSuksesSimpan.Visible = true; LabelSuksesSimpan.Text = "Proses non aktif berhasil !!!";
                txtNomorisi.Text = string.Empty; txtNamaisi.Text = string.Empty; txtNonAktifisi.Text = string.Empty;
                txtKategoriisi.Text = string.Empty; txtAlasanisi.Text = string.Empty; txtDeptisi.Text = string.Empty;
            }

        }
        protected void btnHapus_ServerClick(object sender, EventArgs e)
        {
            if (txtBerlaku.Text == "Tentukan tanggal Non Aktif !!")
            {
                DisplayAJAXMessage(this, "Tanggal Non Aktif belum di pilih !!");
                return;
            }

            //Session["idmaster"] = DomainDMD.IDmaster;
            //Session["IDupd"] = DomainDMD.ID;

            int intResult = 0;
            string strEvent = "Insert";
            Users users = (Users)Session["Users"];
            ISO_UpdDMD updDMD = new ISO_UpdDMD();
            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();
            ISO_MasterUPD Md = new ISO_MasterUPD();
            //if (ViewState["id"] != null)
            //{
            //    updDMD.ID = int.Parse(ViewState["id"].ToString());
            //    updDMD.NoDocument = txtNO.Text;           
            //}
            updDMD.ID = Convert.ToInt32(Session["idmaster"].ToString());
            updDMD.NoDocument = txtNO.Text;
            // upd1.ID = int.Parse(ViewState["id"].ToString());
            updDMD.DocName = txtNama.Text;
            updDMD.NoDocument = txtISO.Text;
            //updDMD.TglBerlaku = DateTime.Parse(txtBerlaku.Text);
            updDMD.CreatedBy = ((Users)Session["Users"]).UserName;
            upd1.LastModifiedBy = users.UserName;
            upd1.NoDokumen = txtISO.Text;
            //updDMD.RevisiNo = txtRev.Text;
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
            upd1.IDmaster = Session["idmaster"].ToString();
            upd1.TglNonAktif = DateTime.Parse(txtBerlaku.Text);
            //txtMaster.Text = updDMD.ID.ToString();
            upd1.ID = Convert.ToInt32(Session["IDupd"]);
            upd1.Alasan = txtAlasan.Text;

            //intResult = dmdF.Insert(updDMD);
            //intResult = dmdF.UpdateH(upd1);
            intResult = Md.UpdateHapus(upd1);

            if (intResult > -1)
            {
                string Query1 = string.Empty;
                ISO_UPD2Facade updF11 = new ISO_UPD2Facade();
                ISO_UpdDMD files11 = new ISO_UpdDMD();
                //int SShare = updF11.RetrieveSShare(Convert.ToInt32(Session["idmaster"]));

                //if (SShare == 11)
                //{
                //    Query1 = " (select A.JenisUPD from ISO_UPD A where A.IDmaster=Data1.ID and RowStatus>-1)JenisUPD";

                //}
                //else if (SShare == 1)
                //{
                //    Query1 = " (select jenisupd from iso_upd upd where upd.ID=idupd and rowstatus>-1) ";
                //}
                //if (SShare > -1)
                //{
                //    Query1 = " (select A.JenisUPD from ISO_UPD A where A.IDmaster=Data1.ID and RowStatus>-1 and A.Status=0)JenisUPD, " +
                //        //" (select Alasan from ISO_UpdDetail where UPDid in (select ID from ISO_UPD A1 where A1.IDmaster=Data1.ID))Alasan, ";
                //             "  (select Alasan from ISO_UpdDetail where UPDid in (select ID from ISO_UPD A1 where A1.IDmaster=Data1.ID and A1.RowStatus>-1 and A1.Status=0) and RowStatus>-1 )Alasan ";
                //}

                Query1 = " (select A.JenisUPD from ISO_UPD A where A.IDmaster=Data1.ID and RowStatus>-1 and A.Status=0)JenisUPD, " +
                             //" (select Alasan from ISO_UpdDetail where UPDid in (select ID from ISO_UPD A1 where A1.IDmaster=Data1.ID))Alasan, ";
                             "  (select Alasan from ISO_UpdDetail where UPDid in (select ID from ISO_UPD A1 where A1.IDmaster=Data1.ID and A1.RowStatus>-1 and A1.Status=0) and RowStatus>-1 )Alasan, ";

                ISO_UPD2Facade updF1 = new ISO_UPD2Facade();
                ISO_UpdDMD files1 = new ISO_UpdDMD();
                //files1 = updF1.AmbilData(int.Parse(Request.QueryString["ba"].ToString()), Query1);

                files1 = updF1.AmbilDataNonAktif(Convert.ToInt32(Session["idmaster"]), Query1);

                Session["NoDocument"] = files1.NoDocument.Trim(); //1
                Session["DocName"] = files1.DocName.Trim(); //2
                Session["RevisiNo"] = files1.RevisiNo.Trim(); //3
                Session["CreatedBy"] = files1.CreatedBy.Trim(); //4
                Session["CategoryUPD"] = files1.CategoryUPD.Trim(); //5
                Session["Type"] = files1.Type.Trim(); //6
                Session["DeptID"] = files1.DeptID.Trim(); //7               
                Session["FileName"] = files1.FileName.Trim();
                Session["IDmaster"] = Convert.ToInt32(upd1.IDmaster); //9
                Session["JenisUPD"] = files1.JenisUPD; //10              
                                                       //Session["PlantID"] = files1.PlantID; //11
                Session["PlantID"] = users.UnitKerjaID;
                Session["Alasan"] = files1.Alasan; //12
                                                   //Session["CreatedTime"] = files1.TglShare; //12

                //if (files1.StatusShare == 11 && files1.Aktif == "2" ||  files1.Aktif == "-2")
                if (files1.Aktif == "-2")
                {
                    try
                    {
                        ShareUPD(); // WebService

                        //ZetroView zUpdate = new ZetroView();
                        //zUpdate.QueryType = Operation.CUSTOM;

                        //if (users.UnitKerjaID == 7)
                        //{
                        //    zUpdate.CustomQuery =
                        //    " update [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UpdDMD set CreatedTime=" + DateTime.Parse(txtBerlaku.Text) + " where  " +
                        //    " NoDocument='" + files1.NoDocument.Trim() + "' and DocName='" + files1.DocName.Trim() + "' and PlantID=" + users.UnitKerjaID + " " +
                        //    " and  and aktif=1 and StatusShare=1 and RowStatus>-1  ";
                        //}

                        //SqlDataReader sdSave = zUpdate.Retrieve();  

                        ISO_UPD2Facade UPDFacadeShare = new ISO_UPD2Facade();
                        ISO_UpdDMD UPDShare = new ISO_UpdDMD();
                        UPDShare.NamaFile = files1.FileName.Trim();
                        UPDShare.Unit = users.UnitKerjaID.ToString();

                        int intResult2 = 0;
                        intResult2 = UPDFacadeShare.KirimFileOtherPlant(UPDShare);
                    }
                    catch { }

                    //ISO_UPD2Facade updF3 = new ISO_UPD2Facade();
                    //ISO_UpdDMD files3 = new ISO_UpdDMD();

                    //int Result = 0;
                    //files3.ID = int.Parse(Request.QueryString["ba"].ToString());
                    //files3.LastModifiedBy = users.UserName;

                    //Result = updF3.UpdateDataShare(files3);
                }
            }

            clearForm();
            LoadDataGridViewItem(LoadDataGrid());

        }
        protected void btnHapusKhusus_ServerClick(object sender, EventArgs e)
        {
            if (txtTglNonisiKhusus.Text == "Tanggal harus di pilih !!")
            {
                DisplayAJAXMessage(this, "Tanggal Non Aktif belum di pilih !!");
                return;
            }

            if (txtAlasanisiKhusus.Text == "")
            {
                DisplayAJAXMessage(this, "Alasan harus diisi !!");
                return;
            }

            Users users = (Users)Session["Users"];
            ISO_Upd DomainDMD5 = new ISO_Upd();
            ISO_MasterUPD FacadeDMD5 = new ISO_MasterUPD();
            int intResult = 0;
            DomainDMD5.NamaDokumen = txtNamaDokumen.Text;
            DomainDMD5.LastModifiedBy = users.UserName;
            DomainDMD5.Alasan = txtAlasanisiKhusus.Text;

            intResult = FacadeDMD5.UpdateHapusISOKhusus(DomainDMD5);

            if (intResult > -1)
            {
                LabelSuksesSimpanKhusus.Visible = true; LabelSuksesSimpanKhusus.Text = "Proses non aktif berhasil !!!";
                txtNomorisiKhusus.Text = string.Empty; txtNamaisiKhusus.Text = string.Empty; txtTglNonisiKhusus.Text = string.Empty;
                txtKategoriisiKhusus.Text = string.Empty; txtAlasanisiKhusus.Text = string.Empty; txtDeptisiKhusus.Text = string.Empty;
            }

        }
        protected void ShareUPD()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoDocument", typeof(string)); //1
            dt.Columns.Add("DocName", typeof(string)); //2
            dt.Columns.Add("RevisiNo", typeof(int)); //3
            dt.Columns.Add("CreatedBy", typeof(string)); //4
            dt.Columns.Add("CategoryUPD", typeof(int)); //5
            dt.Columns.Add("Type", typeof(int)); //6
            dt.Columns.Add("DeptID", typeof(int)); //7               
            dt.Columns.Add("FileName", typeof(string)); //8
            dt.Columns.Add("IDmaster", typeof(string)); //9 
            dt.Columns.Add("JenisUPD", typeof(string)); //10         
            dt.Columns.Add("PlantID", typeof(string)); //11
            dt.Columns.Add("Alasan", typeof(string)); //12

            DataRow row = dt.NewRow();
            row["NoDocument"] = Session["NoDocument"].ToString().Trim(); //1
            row["DocName"] = Session["DocName"].ToString().Trim(); //2
            row["RevisiNo"] = Convert.ToInt32(Session["RevisiNo"]); //3
            row["CreatedBy"] = Session["CreatedBy"].ToString().Trim(); //4
            row["CategoryUPD"] = Convert.ToInt32(Session["CategoryUPD"]); //5
            row["Type"] = Convert.ToInt32(Session["Type"]); //6
            row["DeptID"] = Convert.ToInt32(Session["DeptID"]); //7                
            row["FileName"] = Session["FileName"].ToString().Trim(); //8
            row["IDmaster"] = Convert.ToInt32(Session["IDmaster"]); //9
            row["JenisUPD"] = Convert.ToInt32(Session["JenisUPD"]); //10       
            row["PlantID"] = Convert.ToInt32(Session["PlantID"]); //11
            row["Alasan"] = Session["Alasan"].ToString().Trim(); //12

            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                try
                {
                    WebReference_Krwg.Service1 bpas2 = new WebReference_Krwg.Service1();
                    string intResult = bpas2.InsertShareUPD(dt);

                    WebReference_Jmb.Service1 bpas3 = new WebReference_Jmb.Service1();
                    string intResult2 = bpas3.InsertShareUPD(dt);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Karawang / Jombang ada masalah");
                }

            }
            else if (((Users)Session["Users"]).UnitKerjaID == 13)
            {
                try
                {
                    WebReference_Krwg.Service1 bpas2 = new WebReference_Krwg.Service1();
                    string intResult = bpas2.InsertShareUPD(dt);

                    WebReference_Ctrp.Service1 bpas3 = new WebReference_Ctrp.Service1();
                    string intResult2 = bpas3.InsertShareUPD(dt);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup / Karawang ada masalah");
                }
            }
            else if (((Users)Session["Users"]).UnitKerjaID == 7)
            {
                try
                {
                    WebReference_Ctrp.Service1 bpas2 = new WebReference_Ctrp.Service1();
                    string intResult = bpas2.InsertShareUPD(dt);

                    WebReference_Jmb.Service1 bpas3 = new WebReference_Jmb.Service1();
                    string intResult2 = bpas3.InsertShareUPD(dt);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup / Jombang ada masalah");
                }
            }
        }
    }
}