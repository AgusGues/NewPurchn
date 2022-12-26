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
    public partial class LapStockMKubik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.AddMonths(-3).Month.ToString();
                ddlBulan0.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
                getYear();
            }
        }
        private void getYear()
        {
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            int tahun = 0;
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            ddTahun0.Items.Clear();
            ddTahun0.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
                ddTahun0.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
                tahun = bmTahun.Tahune;
            }
            ddTahun.Items.Add(new ListItem((tahun + 1).ToString(), (tahun + 1).ToString()));
            ddTahun0.Items.Add(new ListItem((tahun + 1).ToString(), (tahun + 1).ToString()));
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
            ddTahun0.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string bln = ddlBulan.SelectedValue;
            string thn = ddTahun.SelectedValue;
            string txtJudul = string.Empty;
            string bulane = ddlBulan.SelectedItem.ToString();
            var xx = new System.Text.RegularExpressions.Regex("|");
            string frmtPrint = string.Empty;
            string strQuery = string.Empty;
            string periode = string.Empty;
            string judul = string.Empty;
            DateTime bulan2 = DateTime.Parse(thn.ToString() + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + "01").AddMonths(1);
            Company comp = new Company();
            CompanyFacade compf = new CompanyFacade();
            comp = compf.RetrieveById(users.UnitKerjaID);
            if (ddTahun.SelectedIndex == ddTahun0.SelectedIndex)
                Session["periode"] = ddlBulan.SelectedItem.Text + " s/d " + ddlBulan0.SelectedItem.Text + " " + thn;
            else
                Session["periode"] = ddlBulan.SelectedItem.Text + " " + ddTahun.Text + " s/d " + ddlBulan0.SelectedItem.Text + " " + ddTahun0.Text;
            Session["Bulan1"] = ddlBulan.SelectedItem.Text;
            Session["Bulan2"] = (bulan2).ToString("MMMM");
            Session["Bulan3"] = ddlBulan0.SelectedItem.Text;
            Session["plant"] = comp.Lokasi;
            strQuery = reportFacade.ViewStockKubik(bln, thn);
            Session["Query"] = strQuery;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=RekapStockTriwulan', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBulan.SelectedIndex <= 10)
            {
                ddlBulan0.SelectedIndex = ddlBulan.SelectedIndex + 2;
                ddTahun0.SelectedIndex = ddTahun.SelectedIndex;
            }
            else
            {
                ddlBulan0.SelectedIndex = ddlBulan.SelectedIndex - 12 + 2;
                ddTahun0.SelectedIndex = ddTahun.SelectedIndex + 1;
            }
        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBulan.SelectedIndex <= 10)
            {
                ddlBulan0.SelectedIndex = ddlBulan.SelectedIndex + 2;
                ddTahun0.SelectedIndex = ddTahun.SelectedIndex;
            }
            else
            {
                ddlBulan0.SelectedIndex = ddlBulan.SelectedIndex - 12 + 2;
                ddTahun0.SelectedIndex = ddTahun.SelectedIndex + 1;
            }
        }
    }
}