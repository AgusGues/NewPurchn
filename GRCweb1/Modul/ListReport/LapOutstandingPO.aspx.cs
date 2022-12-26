using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapOutstandingPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                if (RadioButton1.Checked == true)
                { LoadTipeSPP(); }
                else
                { LoadDocNoPref(); }
                txtFromPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;
            if (RbtStock.Checked == false && RbtNonStock.Checked == false)
            {
                if (ddlTipeSPP.SelectedIndex == 0 & RadioButton3.Checked == false)
                {
                    DisplayAJAXMessage(this, "Pilih Tipe Laporan");
                    return;
                }
            }
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }
            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["xjudul"] = null;
            string strQuery = string.Empty;
            ReportFacade reportFacade = new ReportFacade();
            if (ddlTipeprint.SelectedIndex == 0)
            {
                if (RadioButton1.Checked == true && RbtStock.Checked == false && RbtNonStock.Checked == false)
                    strQuery = reportFacade.ViewOutstandingPO(periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue));
                if (RadioButton2.Checked == true && RbtStock.Checked == false && RbtNonStock.Checked == false)
                    strQuery = reportFacade.ViewOutstandingPO1(periodeAwal, periodeAkhir, ddlTipeSPP.SelectedValue);
                if (RadioButton3.Checked == true && RbtStock.Checked == false && RbtNonStock.Checked == false)
                {
                    if (TxtSupplier.Text.Trim() == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Supplier tidak boleh kosong");
                        return;
                    }
                    strQuery = reportFacade.ViewOutstandingPObySup(periodeAwal, periodeAkhir, TxtSupplier.Text);
                }
                if (RbtStock.Checked == true && RadioButton1.Checked == true)
                {
                    strQuery = reportFacade.ViewOutstandingPO2(periodeAwal, periodeAkhir);
                }
                if (RbtNonStock.Checked == true && RadioButton1.Checked == true)
                {
                    strQuery = reportFacade.ViewOutstandingPO3(periodeAwal, periodeAkhir);
                }
            }
            else
            {
                if (RadioButton1.Checked == true && RbtStock.Checked == false && RbtNonStock.Checked == false)
                    strQuery = reportFacade.ViewOutstandingPOLS(periodeAwal, periodeAkhir, int.Parse(ddlTipeSPP.SelectedValue));
                if (RadioButton2.Checked == true && RbtStock.Checked == false && RbtNonStock.Checked == false)
                    strQuery = reportFacade.ViewOutstandingPO1LS(periodeAwal, periodeAkhir, ddlTipeSPP.SelectedValue);
                if (RadioButton3.Checked == true && RbtStock.Checked == false && RbtNonStock.Checked == false)
                {
                    if (TxtSupplier.Text.Trim() == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Supplier tidak boleh kosong");
                        return;
                    }
                    strQuery = reportFacade.ViewOutstandingPObySupLS(periodeAwal, periodeAkhir, TxtSupplier.Text);
                }
                if (RbtStock.Checked == true && RadioButton1.Checked == true)
                {
                    strQuery = reportFacade.ViewOutstandingPO2LS(periodeAwal, periodeAkhir);
                }
                if (RbtNonStock.Checked == true && RadioButton1.Checked == true)
                {
                    strQuery = reportFacade.ViewOutstandingPO3LS(periodeAwal, periodeAkhir);
                }
            }
            Session["Query"] = strQuery;
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["xjudul"] = "Outstanding PO";
            Session["print"] = ddlTipeprint.SelectedValue;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapOutstandingSPP', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadTipeSPP()
        {
            ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Group Purchn --", string.Empty));
            ddlTipeSPP.Items.Add(new ListItem("ALL", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void LoadDocNoPref()
        {
            ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.RetrieveCode();
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Doc No Prefix --", string.Empty));
            ddlTipeSPP.Items.Add(new ListItem("ALL", "0"));
            ddlTipeSPP.Items.Add(new ListItem(kd + "I", kd + "I"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(kd + groupsPurchn.GroupCode, kd + groupsPurchn.GroupCode));
            }
        }

        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTipeSPP.SelectedIndex > 0)
            //{
            //    if (ddlTipeSPP.SelectedIndex == 5)
            //        ddlTipeBarang.SelectedIndex = 2;
            //    else if (ddlTipeSPP.SelectedIndex == 6)
            //        ddlTipeBarang.SelectedIndex = 3;
            //    else
            //        ddlTipeBarang.SelectedIndex = 1;
            //}
            //else
            //    ddlTipeBarang.SelectedIndex = 0;

        }
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LoadTipeSPP();
            TxtSupplier.Visible = false;
            ddlTipeSPP.Visible = true;
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LoadDocNoPref();
            TxtSupplier.Visible = false;
            ddlTipeSPP.Visible = true;
        }
        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            TxtSupplier.Visible = true;
            ddlTipeSPP.Visible = false;
        }
    }
}