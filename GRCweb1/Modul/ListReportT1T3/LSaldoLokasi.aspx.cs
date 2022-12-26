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
    public partial class LSaldoLokasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string kriteria = string.Empty;

            if (txtPartnoA0.Text != string.Empty)
                kriteria = " and C.partno='" + txtPartnoA0.Text + "'";

            strQuery = reportFacade.ViewLSaldoLokasi(kriteria);
            Session["Query"] = strQuery;
            if (RBrpt1.Checked == true)
                Session["bylokasi"] = "yes";
            else
                Session["bylokasi"] = "no";

            Cetak(this);
        }

        protected void btnPrint0_ServerClick(object sender, EventArgs e)
        {
            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string kriteria = string.Empty;

            if (txtPartnoA.Text != string.Empty)
                kriteria = " and C.partno='" + txtPartnoA.Text + "'";
            if (txtLokasi1.Text != string.Empty)
                kriteria = kriteria + " and B.lokasi='" + txtLokasi1.Text + "'";


            strQuery = reportFacade.ViewLSaldoLokasi(kriteria);
            Session["Query"] = strQuery;
            if (RBrpt1.Checked == true)
                Session["bylokasi"] = "yes";
            else
                Session["bylokasi"] = "no";
            Cetak(this);
        }
        protected void btnPrint1_ServerClick(object sender, EventArgs e)
        {
            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string kriteria = string.Empty;
            if (RbA1.Checked == true)
                kriteria = " ";
            else
                if (txtLokasi2.Text != string.Empty)
                kriteria = " and B.lokasi='" + txtLokasi2.Text.Trim() + "'";
            else
                kriteria = " ";

            strQuery = reportFacade.ViewLSaldoLokasi(kriteria);
            Session["Query"] = strQuery;
            if (RBrpt1.Checked == true)
                Session["bylokasi"] = "yes";
            else
                Session["bylokasi"] = "no";
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LSaldoLokasi', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
        protected void RBSatuPart_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSatuPart.Checked == true)
            {
                Panel3.Visible = false;
                Panel4.Visible = true;
                Panel5.Visible = false;
                btnPrint0.Disabled = false;
                btnPrint1.Disabled = true;
                btnPrint.Disabled = true;
            }

        }
        protected void RBAllPart_CheckedChanged(object sender, EventArgs e)
        {
            if (RBAllPart.Checked == true)
            {
                Panel3.Visible = true;
                Panel4.Visible = false;
                Panel5.Visible = false;
                btnPrint1.Disabled = false;
                btnPrint0.Disabled = true;
                btnPrint.Disabled = true;
            }
        }
        protected void RBLokasi_CheckedChanged(object sender, EventArgs e)
        {
            if (RBLokasi.Checked == true)
            {
                Panel3.Visible = false;
                Panel4.Visible = false;
                Panel5.Visible = true;
                btnPrint.Disabled = false;
                btnPrint0.Disabled = true;
                btnPrint1.Disabled = true;
            }
        }
        protected void RbA1_CheckedChanged(object sender, EventArgs e)
        {
            if (RbA1.Checked == true)
            {
                txtLokasi2.Visible = false;
            }
        }
        protected void RbA2_CheckedChanged(object sender, EventArgs e)
        {
            if (RbA2.Checked == true)
            {
                txtLokasi2.Visible = true;
            }
        }
        protected void txtPartnoA_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender4.ContextKey = txtPartnoA.Text;
            txtLokasi1.Focus();

        }

        protected void txtLokasi1_PreRender(object sender, EventArgs e)
        {
            if (RBSatuPart.Checked == true && txtPartnoA.Text != string.Empty)
            {
                txtLokasi1.Focus();
            }
        }
        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            btnPrint.Focus();
        }
    }
}