using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Cogs;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GRCweb1.Modul.ListReport.AccountingReport
{
    public partial class CountingSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Pjadi.Visible = false;
            }
        }
        protected void Curing_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrCr = new ArrayList();
            PemantauanWIP Cr = new PemantauanWIP();
            arrCr = Cr.CuringSheet(((Users)Session["Users"]).UnitKerjaID);
            pnCuring.Visible = true;
            pnJemur.Visible = false;
            pnTransit.Visible = false;
            lstCuring.DataSource = arrCr;
            lstCuring.DataBind();
            printsheet.Enabled = true;

        }
        protected void Jemur_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrCr = new ArrayList();
            PemantauanWIP Cr = new PemantauanWIP();
            arrCr = Cr.JemurSheet(((Users)Session["Users"]).UnitKerjaID);
            pnJemur.Visible = true;
            pnCuring.Visible = false;
            pnTransit.Visible = false;
            lstJemur.DataSource = arrCr;
            lstJemur.DataBind();
            printsheet.Enabled = true;
        }
        protected void Transit_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrCr = new ArrayList();
            PemantauanWIP Cr = new PemantauanWIP();
            arrCr = Cr.TransitSheet(((Users)Session["Users"]).UnitKerjaID);
            lstTransit.DataSource = arrCr;
            lstTransit.DataBind();
            pnJemur.Visible = false;
            pnCuring.Visible = false;
            pnTransit.Visible = true;
            printsheet.Enabled = true;
        }
        protected void Pjadi_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrCr = new ArrayList();
            PemantauanWIP Cr = new PemantauanWIP();
            pnJemur.Visible = false;
            pnCuring.Visible = false;

        }
        static public void Cetak(Control page, string proses)
        {
            //string myScript = "var wn = window.showModalDialog('../../ListReport/AccountingReport/Report.aspx?IdReport=CountSheet&tp=" + proses + "','','resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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
        protected void printsheet_Click(object sender, EventArgs e)
        {
            PemantauanWIP Cr = new PemantauanWIP();
            if (Curing.Checked == true)
            {
                string Query = Cr.QueryCuring(((Users)Session["Users"]).UnitKerjaID);
                Session["Query"] = Query;
                Cetak(this, "Curing");
            }
            else if (Jemur.Checked == true)
            {
                string Query = Cr.QueryJemur(((Users)Session["Users"]).UnitKerjaID);
                Session["Query"] = Query;
                Cetak(this, "Jemur");
            }
            else if (Transit.Checked == true)
            {
                string Query = Cr.QueryTransit(((Users)Session["Users"]).UnitKerjaID);
                Session["Query"] = Query;
                Cetak(this, "Transit");
            }

        }
    }
}