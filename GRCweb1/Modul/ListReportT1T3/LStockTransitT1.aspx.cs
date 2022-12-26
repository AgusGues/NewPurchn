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
    public partial class LStockTransitT11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                Users users = (Users)Session["Users"];
                if (users.UnitKerjaID == 1)
                {
                    txtFromPostingPeriod.Text = "01-06-2013";
                }
                else
                {
                    txtFromPostingPeriod.Text = "01-12-2013";
                }
                txtToPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string periodeAwal = string.Empty;
            string periodeAkhir = string.Empty;
            string txPeriodeAwal = string.Empty;
            string txPeriodeAkhir = string.Empty;
            string ThBl = string.Empty;
            string ThBlA = string.Empty;
            if (txtFromPostingPeriod.Text != string.Empty || txtToPostingPeriod.Text != string.Empty)
            {
                periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
                periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
                txPeriodeAwal = txtFromPostingPeriod.Text;
                txPeriodeAkhir = txtToPostingPeriod.Text;
            }
            else
            {
                periodeAwal = DateTime.Now.AddYears(-10).ToString("yyyyMMdd");
                periodeAkhir = DateTime.Now.ToString("yyyyMMdd");
                txPeriodeAwal = DateTime.Now.AddYears(-10).ToString("dd MMMM yyyy");
                txPeriodeAkhir = DateTime.Now.ToString("dd MMMM yyyy");
            }

            ThBlA = string.Empty;

            string strError = string.Empty;
            int thn = 0;
            int blnLalu = 0;
            if (txtFromPostingPeriod.Text != string.Empty || txtToPostingPeriod.Text != string.Empty)
            {
                thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
                blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            }
            else
            {
                thn = DateTime.Now.AddYears(-10).Year;
                blnLalu = DateTime.Now.Month;
            }
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

            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string strQuery1 = string.Empty;
            string strcriteria = string.Empty;
            strQuery = reportFacade.ViewLSaldoTransit_T1();
            Session["Query"] = strQuery;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LSaldoTransit_T1', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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

        protected void txtFromPostingPeriod_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            DateTime golive = DateTime.Parse("01-Jun-2013");
            DateTime tglawal = DateTime.Parse(txtFromPostingPeriod.Text);
            if (tglawal < golive)
                txtFromPostingPeriod.Text = "01-Jun-2013";
            //txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
            //}
            //catch { }
        }


        protected void txtFromPostingPeriod_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                DateTime golive = DateTime.Parse("01-Jun-2013");
                DateTime tglawal = DateTime.Parse(txtFromPostingPeriod.Text);
                if (tglawal < golive)
                    txtFromPostingPeriod.Text = "01-Jun-2013";
                txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
            }
            catch { }
        }

        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        {
            txtLokasi.Focus();
        }
    }
}