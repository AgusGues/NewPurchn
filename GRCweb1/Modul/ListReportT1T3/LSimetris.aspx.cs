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
    public partial class LSimetris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            string frmtPrint = string.Empty;

            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);
            //string deptname = string.Empty;
            //if (dept.DeptName != string.Empty)
            //    deptname = dept.DeptName.Substring(0, 3).ToUpper();
            //else
            //    deptname = " ";

            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            int userID = ((Users)Session["Users"]).ID;
            Session["report"] = "BS";
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            if (RBTgl.Checked == true)
                if (ChkAutoBS.Checked == true)
                {

                    strQuery = reportFacade.ViewLSimetrisAutoBS(periodeAwal, periodeAkhir, 1);
                }
                else
                {
                    Session["report"] = "biasa";
                    strQuery = reportFacade.ViewLSimetris(periodeAwal, periodeAkhir, 1);
                }
            else
                if (ChkAutoBS.Checked == true)
            {

                strQuery = reportFacade.ViewLSimetrisAutoBS(periodeAwal, periodeAkhir, 0);
            }
            else
            {
                Session["report"] = "biasa";
                strQuery = reportFacade.ViewLSimetris(periodeAwal, periodeAkhir, 0);
            }
            Session["Query"] = strQuery;
            Session["periode"] = "Periode : " + txPeriodeAwal + " s/d " + txPeriodeAkhir;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LSimetris', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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