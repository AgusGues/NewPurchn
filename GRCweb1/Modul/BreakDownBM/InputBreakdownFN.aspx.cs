using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;

namespace GRCweb1.Modul.BreakDownBM
{
    public partial class InputBreakdownFN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["ListBreakDownFN"] = null;
                txtTglBreak.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtFrek.Text = "0";
                txtWaktu.Text = "0";
                txtOps.Text = "0";
                rekapList.Visible = false;
                LoadNamaOven();
                LoadNamaSGroup();
                LoadKategori();
            }
        }

        private void LoadNamaSGroup()
        {
            ArrayList arrNamaSGroup = new ArrayList();
            BreakDownFNFacade breakDFn = new BreakDownFNFacade();
            arrNamaSGroup = breakDFn.RetrieveSGroup();

            ddlsGroup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (BreakDownFN naSgroup in arrNamaSGroup)
            {
                ddlsGroup.Items.Add(new ListItem(naSgroup.NamaGroupOven, naSgroup.ID.ToString()));
            }
        }

        private void LoadNamaOven()
        {
            ArrayList arrNamaOven = new ArrayList();
            BreakDownFNFacade breakDFn = new BreakDownFNFacade();
            arrNamaOven = breakDFn.RetrieveNamaOven();

            ddlNamaOven.Items.Add(new ListItem("-- Pilih Oven --", "0"));
            foreach (BreakDownFN naOven in arrNamaOven)
            {
                ddlNamaOven.Items.Add(new ListItem(naOven.NamaOven, naOven.ID.ToString()));
            }
        }

        private void LoadKategori()
        {
            ArrayList arrNamaKat = new ArrayList();
            BreakDownFNFacade breakDFn = new BreakDownFNFacade();
            arrNamaKat = breakDFn.RetrieveKategori();

            ddlKategori.Items.Add(new ListItem("-- Pilih Kategori --", "0"));
            foreach (BreakDownFN naKat in arrNamaKat)
            {
                ddlKategori.Items.Add(new ListItem(naKat.UraianCat, naKat.ID.ToString()));
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("BreakNo");
            Session.Remove("ListBreakDownFN");
            ClearForm();
        }

        private void ClearForm()
        {
            ViewState["id"] = null;
            Session.Remove("id");
            Session["BreakNo"] = null;
            Session["ListBreakDownFN"] = null;
            txtTglBreak.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            if (ddlNamaOven.SelectedIndex > 0) ddlNamaOven.SelectedIndex = 0;
            if (ddlsGroup.SelectedIndex > 0) ddlsGroup.SelectedIndex = 0;
            txtUraian.Text = string.Empty;
            txtFrek.Text = "0";
            txtWaktu.Text = "0";
            txtOps.Text = "0";
            if (ddlKategori.SelectedIndex > 0) ddlKategori.SelectedIndex = 0;
            ArrayList arrData = new ArrayList();
            lstBdtFn.DataSource = arrData;
            lstBdtFn.DataBind();
            ddlNamaOven.Enabled = true;
            txtTglBreak.Enabled = true;
            txtOps.Enabled = true;
            btnUpdate.Enabled = true;
            txtNoBreak.Text = string.Empty;

        }
        protected void btnUpdate_serverClick(object sender, EventArgs e)
        {
            int intResult = 0;
            string strEvent = "Insert";
            int maxID = 0;
            BreakDownFN cekIDTerakhir = new BreakDownFN();
            BreakDownFN bdtfnx = new BreakDownFN();
            BreakDownFNFacade breakdfn = new BreakDownFNFacade();
            breakdfn.Tahun = DateTime.Parse(txtTglBreak.Text).Year;
            maxID = breakdfn.RetrieveMaxId();

            breakdfn = new BreakDownFNFacade();
            if (ViewState["id"] != null)
            {
                bdtfnx.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            if (Session["ListBreakDownFN"] == null)
            {
                DisplayAJAXMessage(this, "ListBreakDownFN Masih Kosong");
                return;
            }
            ArrayList arrData = (ArrayList)Session["ListBreakDownFN"];
            foreach (BreakDownFN bdtfn in arrData)
            {
                intResult = 0;
                bdtfnx.NoUrut = maxID + 1;
                bdtfnx.BreakNo = (maxID + 1).ToString().PadLeft(3, '0') + "/FN/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                bdtfnx.TanggalBreak = bdtfn.TanggalBreak;
                bdtfnx.OvenID = bdtfn.OvenID;
                bdtfnx.GroupOvenID = bdtfn.GroupOvenID;
                bdtfnx.Uraian = bdtfn.Uraian;
                bdtfnx.Frek = bdtfn.Frek;
                bdtfnx.Waktu = bdtfn.Waktu;
                bdtfnx.NmMasterCatID = bdtfn.NmMasterCatID;
                bdtfnx.WaktuOprsnl = bdtfn.WaktuOprsnl;
                bdtfnx.CreatedBy = ((Users)Session["Users"]).UserName;
                bdtfnx.LastModifiedBy = ((Users)Session["Users"]).UserName;

                if (bdtfnx.ID > 0)
                {
                    intResult = breakdfn.Update(bdtfnx);
                    InsertLog(strEvent);
                }
                else
                {
                    intResult = breakdfn.Insert(bdtfnx);
                    if (intResult > 0)
                    {
                        DisplayAJAXMessage(this, "Data Telah Disimpan");
                        txtNoBreak.Text = bdtfnx.BreakNo;
                        btnUpdate.Enabled = false;
                        //clearForm();
                        InsertLog(strEvent);

                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Simpan");
                    }
                }
            }
            Session["ListBreakDownFN"] = null;
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListBreakDownFN"] = null;
            Session["BreakNo"] = null;
            Response.Redirect("ListBreakdownFN.aspx?xXx=>" + (((Users)Session["Users"]).DeptID));
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnRekap_ServerClick(object sender, EventArgs e)
        {
            Session["ListBreakDownFN"] = null;
            Session["BreakNo"] = null;
            Response.Redirect("RekapBreakdownFN.aspx?xXx=>" + (((Users)Session["Users"]).DeptID));
        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            ArrayList arrItem = (Session["ListBreakDownFN"] == null) ? new ArrayList() : (ArrayList)Session["ListBreakDownFN"];
            BreakDownFN bdtFn = new BreakDownFN();
            if (ddlNamaOven.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Nama Oven belum di pilih");
                return;
            }
            if (ddlsGroup.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Nama Shift / Group belum di pilih");
                return;
            }
            if (txtUraian.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Uraian Harus di isi");
                return;
            }

            int parsedValue;
            if (txtFrek.Text == string.Empty || !int.TryParse(txtFrek.Text, out parsedValue))
            {
                DisplayAJAXMessage(this, "Frek Harus di isi / Frek Harus Angka");
                return;
            }
            if (txtWaktu.Text == string.Empty || !int.TryParse(txtWaktu.Text, out parsedValue))
            {
                DisplayAJAXMessage(this, "Waktu Harus di isi / Waktu Harus Angka");
                return;
            }
            if (txtOps.Text == string.Empty || !int.TryParse(txtOps.Text, out parsedValue))
            {
                DisplayAJAXMessage(this, "Waktu Operasional Harus di isi / Waktu Operasional Harus Angka");
                return;
            }
            if (ddlKategori.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Kategori harus belum di pilih");
                return;
            }

            bdtFn.ID = int.Parse(txtID.Value);
            bdtFn.OvenID = int.Parse(ddlNamaOven.SelectedValue);
            bdtFn.NamaOven = ddlNamaOven.SelectedItem.ToString();
            bdtFn.GroupOvenID = int.Parse(ddlsGroup.SelectedValue);
            bdtFn.NamaGroupOven = ddlsGroup.SelectedItem.ToString();
            bdtFn.Uraian = txtUraian.Text.ToString();
            bdtFn.Frek = int.Parse(txtFrek.Text);
            bdtFn.Waktu = int.Parse(txtWaktu.Text);
            bdtFn.NmMasterCatID = int.Parse(ddlKategori.SelectedValue);
            bdtFn.UraianCat = ddlKategori.SelectedItem.ToString();
            bdtFn.TanggalBreak = DateTime.Parse(txtTglBreak.Text);
            bdtFn.WaktuOprsnl = int.Parse(txtOps.Text);
            arrItem.Add(bdtFn);
            Session["ListBreakDownFN"] = arrItem;
            ddlNamaOven.Enabled = false;
            txtTglBreak.Enabled = false;
            txtOps.Enabled = false;
            lstBdtFn.DataSource = arrItem;
            lstBdtFn.DataBind();

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void lstBdtFn_Databound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void lstBdtFn_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "BreakdownFN";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtNoBreak.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            //if (eventLogFacade.Error == string.Empty)
            //    //clearForm();
        }
    }
}