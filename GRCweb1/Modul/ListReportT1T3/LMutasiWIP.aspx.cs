using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LMutasiWIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();
                txtNoProduksi.Focus();
                txtTanggal.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                txtTanggal0.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            }
        }

        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["Elapbul"] = "No";
            if (txtNoProduksi.Text.Trim() == string.Empty && RB_Rekap.Checked == false)
            {
                DisplayAJAXMessage(this, "No. Produksi harus di isi");
                return;
            }
            string periodeBulan = ddlBulan.SelectedValue;
            string txtBulan = ddlBulan.SelectedItem.ToString();
            string periodeTahun = ddTahun.SelectedValue;
            string noProduksi = txtNoProduksi.Text.Trim();
            string frmtPrint = string.Empty;
            string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            Users users = (Users)Session["Users"];
            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            DateTime tanggal = DateTime.Parse(txtTanggal.Text);
            DateTime tanggal0 = DateTime.Parse(txtTanggal0.Text);
            string paramTanggal = tanggal.ToString("dd MMM yyyy");
            string paramTanggal0 = tanggal0.ToString("dd MMM yyyy");
            string intTanggal = tanggal.ToString("yyyyMMdd");
            string intTanggal0 = tanggal0.ToString("yyyyMMdd");
            if (RB_Detail.Checked == true)
            {
                Session["periode"] = txtBulan + " " + periodeTahun;
                Session["partno"] = txtNoProduksi.Text;
                strQuery = reportFacade.ViewMutasiWIP(periodeBulan, periodeTahun, noProduksi);
                Session["Query"] = strQuery;
                Session["barang"] = txtNoProduksi.Text;
                Cetak(this);
            }
            else
            {
                Session["periode"] = txtBulan + " " + periodeTahun;
                if (RBBulanan.Checked == true)
                {
                    strQuery = reportFacade.ViewMutasiWIPRekap(periodeBulan, periodeTahun, noProduksi);
                }
                else
                {
                    strQuery = reportFacade.ViewMutasiWIPRekapHarian(intTanggal, intTanggal0);
                    Session["periode"] = paramTanggal0;
                }
                Session["Query"] = strQuery;

                Cetak2(this);
            }

            Session["yearmonth"] = periodeTahun + padbulan;
            if (RBLembar.Checked == true)
                Session["Satuan"] = "lembar";
            else
                Session["Satuan"] = "M3";
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiWIP', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
        static public void Cetak2(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiWIPRekap', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak2();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getYear();
        }
        protected void RBBulanan_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBulanan.Checked == true)
            {
                Panel3.Visible = true;
                Panel1.Visible = false;
            }
        }
        protected void RBHarian_CheckedChanged(object sender, EventArgs e)
        {
            if (RBHarian.Checked == true)
            {
                Panel3.Visible = false;
                Panel1.Visible = true;
            }
        }
    }
}