using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ListReport
{
    public partial class HelpDeskRekap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                LoadDept();
                txtFromPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");

            }


        }

        private void LoadDept()
        {
            ddlDeptID.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptID.Items.Add(new ListItem("-- ALL --", "0"));
            foreach (Dept dept in arrDept)
            {
                ddlDeptID.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }
        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");

            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            //if (ddlDeptID.SelectedIndex == 0)
            //{
            //    DisplayAJAXMessage(this, "Plih Departemen *_* ");
            //}

            string strError = string.Empty;
            int bln = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            int deptid = int.Parse(ddlDeptID.SelectedValue);
            string dept = Convert.ToString(ddlDeptID.SelectedValue);
            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["xjudul"] = null;
            Session["formno"] = null;


            ReportFacade reportFacade = new ReportFacade();
            string strQuery = reportFacade.ViewRekapKomplain(periodeAwal, periodeAkhir, dept, deptid);
            Session["Query"] = strQuery;
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["xjudul"] = "LAPORAN KOMPLAIN USER UNTUK PERMASALAHAN SOFTWARE DAN HARDWARE";
            Session["formno"] = "Form No :IT/LKU/05/14/R1";

            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=LapRekapKom', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),"MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}