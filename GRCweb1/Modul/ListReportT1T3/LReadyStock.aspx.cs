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
    public partial class LReadyStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTanggal.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                txtTanggal0.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            }
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            DateTime tanggal = DateTime.Parse(txtTanggal.Text);
            DateTime tanggal0 = DateTime.Parse(txtTanggal0.Text);
            string intTanggal = tanggal.ToString("yyyyMMdd");
            string intTanggal0 = tanggal0.ToString("yyyyMMdd");
            string paramTanggal = tanggal.ToString("dd MMM yyyy");
            string paramTanggal0 = tanggal0.ToString("dd MMM yyyy");
            string strHQtyLastMonth = string.Empty;

            strQuery = reportFacade.ViewLReadyStock(intTanggal, intTanggal0, users.UnitKerjaID);
            Session["periode"] = paramTanggal0;
            Session["Query"] = strQuery;
            Cetak1(this);
        }
        static public void Cetak1(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LReadyStock', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak1();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void RBRekap_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void txtFromPostingPeriod_TextChanged1(object sender, EventArgs e)
        {

        }
    }
}