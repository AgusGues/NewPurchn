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
    public partial class LStockT1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtTahun.Text = DateTime.Now.Year.ToString();
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
                ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            }
            else
            {
                periodeAwal = DateTime.Now.AddYears(-10).ToString("yyyyMMdd");
                periodeAkhir = DateTime.Now.ToString("yyyyMMdd");
                txPeriodeAwal = DateTime.Now.AddYears(-10).ToString("dd MMMM yyyy");
                txPeriodeAkhir = DateTime.Now.ToString("dd MMMM yyyy");
                ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            }

            ThBlA = string.Empty;
            if (ddlBulan.SelectedIndex > 1)
                ThBlA = txtTahun.Text.ToString() + (ddlBulan.SelectedIndex - 1).ToString().PadLeft(2, '0');
            else
            {
                ThBlA = (int.Parse(txtTahun.Text) - 1).ToString() + "12";
            }

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

            //if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            //{
            //    DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
            //    return;
            //}

            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string strQuery1 = string.Empty;
            string strcriteria = string.Empty;
            int tgltype = 0;
            if (RBTgl.Checked == true)
            {
                if (txtNoProduksi.Text != string.Empty)
                    strcriteria = " and partno='" + txtNoProduksi.Text + "' ";
                if (txtLokasi.Text != string.Empty)
                    strcriteria = strcriteria + " and lokasi='" + txtLokasi.Text + "' ";
                strQuery = reportFacade.ViewT1StockPerLokasiTgl(periodeAwal, periodeAkhir, ThBlA);
                Session["periode"] = txPeriodeAwal + " s/d " + txPeriodeAkhir;// +" (Pemetaan)";
                Session["report"] = "stock";
                Session["Query"] = strQuery;
                Cetak(this);
            }
            if (RBBln.Checked == true)
            {
                strQuery = reportFacade.ViewT1StockPerLokasi(ThBlA, ThBl, 1);
                Session["periode"] = ddlBulan.SelectedValue + " " + txtTahun.Text;// +" (Kartu Stock)";
                Session["report"] = "stock";
                Session["Query"] = strQuery;
                Cetak(this);
            }
            //if (RBBln0.Checked == true)
            //{
            //    //strQuery = reportFacade.ViewT1StockPerLokasi(ThBlA, ThBl, 1);
            //    strQuery1 = reportFacade.ViewT1StockPerLokasiDet(ThBlA, ThBl,"curing");
            //    Session["periode"] = null;
            //    Session["periode"] = "CURING  PERIODE : " + ddlBulan.SelectedValue + " " + txtTahun.Text;// +" (Kartu Stock)";
            //    Session["report"] = "stock";
            //    Session["Query"] = strQuery;
            //    Session["Query1"] = strQuery1;
            //    Cetak1(this);
            //}
            //if (RBBln1.Checked == true)
            //{
            //    //strQuery = reportFacade.ViewT1StockPerLokasi(ThBlA, ThBl, 1);
            //    strQuery1 = reportFacade.ViewT1StockPerLokasiDet(ThBlA, ThBl, "jemur");
            //    Session["periode"] = null;
            //    Session["periode"] = "JEMUR  PERIODE : " + ddlBulan.SelectedValue + " " + txtTahun.Text;// +" (Kartu Stock)";
            //    Session["report"] = "stock";
            //    Session["Query"] = strQuery;
            //    Session["Query1"] = strQuery1;
            //    Cetak1(this);
            //}
        }
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LT1SaldoPerLokasi', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak1(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LT1SaldoPerLokasiDet', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
        protected void RBTgl_CheckedChanged(object sender, EventArgs e)
        {
            if (RBTgl.Checked == true)
            {
                Panel3.Visible = true;
                Panel4.Visible = false;
            }
        }
        protected void RBBln_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBln.Checked == true)
            {
                Panel3.Visible = false;
                Panel4.Visible = true;
            }
        }
        protected void txtFromPostingPeriod_TextChanged1(object sender, EventArgs e)
        {
            //try
            //{
            DateTime golive = DateTime.Parse("01-Jun-2013");
            DateTime tglawal = DateTime.Parse(txtFromPostingPeriod.Text);
            if (tglawal < golive)
                txtFromPostingPeriod.Text = "01-Jun-2013";
            txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
            ddlBulan.SelectedIndex = Convert.ToDateTime(txtFromPostingPeriod.Text).Month;
            //}
            //catch { }
        }
        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        {
            txtLokasi.Focus();
        }
    }
}