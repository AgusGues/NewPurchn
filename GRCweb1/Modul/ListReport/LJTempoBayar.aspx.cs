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
    public partial class LJTempoBayar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                //LoadTipeSPP();
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            //txtToPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            //if (ddlGroup.SelectedIndex == 0)
            //{
            //    DisplayAJAXMessage(this, "Pilih Tipe Laporan");
            //    return;
            //}
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }
            string strQuery = string.Empty;
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string txperiodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("dd/MM/yyyy");
            string txperiodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("dd/MM/yyyy");
            int type = 0;
            ReportFacade reportFacade = new ReportFacade();
            if (RadioButton4.Checked == false)
            {
                if (RadioButton1.Checked == true)
                    type = 0;
                if (RadioButton2.Checked == true)
                    type = 1;
                if (RadioButton3.Checked == true)
                    type = 2;
                strQuery = reportFacade.ViewLJTempoBayar(periodeAwal, periodeAkhir, type);
                Session["Query"] = strQuery;
                Session["periode"] = txperiodeAwal + " s/d " + txperiodeAkhir;
                if (RadioButton2.Checked == true)
                    Session["typetgl"] = "TANGGAL PO ";
                else
                    Session["typetgl"] = "TANGGAL RECEIPT ";
                Cetak(this);
            }
            else
            {
                strQuery = reportFacade.ViewLPPnBM(periodeAwal, periodeAkhir);
                Session["Query"] = strQuery;
                Session["periode"] = "Periode PPnBM : " + txperiodeAwal + " s/d " + txperiodeAkhir;
                CetakPPn(this);
            }
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=LJTempoBayar', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void CetakPPn(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=LPPnBM', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "CetakPPn();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        //private void LoadTipeSPP()
        //{
        //    ddlGroup.Items.Clear();

        //    ArrayList arrGroupsPurchn = new ArrayList();
        //    GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
        //    //arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).GroupID);
        //    arrGroupsPurchn = groupsPurchnFacade.Retrieve();

        //    ddlGroup.Items.Add(new ListItem("All", "0"));
        //    foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
        //    {
        //        ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
        //    }

        //    //ddlGroup.SelectedIndex = 1;
        //}

        //protected void ddlJenis_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlJenis.SelectedIndex > 1)
        //    {
        //        ddlGroup.Enabled = false;
        //        ddlGroup.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        ddlGroup.Enabled = true;
        //        ddlGroup.SelectedIndex = 0;
        //    }
        //}

    }
}