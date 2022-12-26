using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LSaldoItemB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTahun.Text = DateTime.Now.Year.ToString();
                ddlBulan.SelectedIndex = int.Parse(DateTime.Now.Month.ToString());
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            int userID = ((Users)Session["Users"]).ID;
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string blnQty = ddlBulan.SelectedValue;
            int tahun = int.Parse(txtTahun.Text);
            string jenis = string.Empty;
            if (RB1.Checked == true)
                jenis = "3";
            if (RB2.Checked == true)
                jenis = "P";
            if (RB3.Checked == true)
                jenis = "W";
            if (RB4.Checked == true)
                jenis = "S";
            strQuery = reportFacade.ViewLSaldoItemB(blnQty, tahun, jenis);
            Session["Query"] = strQuery;
            Session["periode"] = ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LSaldoItemB', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
    }
}