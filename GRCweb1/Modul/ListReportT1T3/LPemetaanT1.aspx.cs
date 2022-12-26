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
    public partial class LPemetaanT1 : System.Web.UI.Page
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
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');

            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);
            string deptname = string.Empty;
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
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            int tgltype = 0;
            string strcriteria = string.Empty;
            if (txtNoProduksi.Text != string.Empty)
                strcriteria = " and IA.partno='" + txtNoProduksi.Text + "' ";
            if (txtLokasi.Text != string.Empty)
                strcriteria = strcriteria + " and LA.lokasi='" + txtLokasi.Text + "' ";
            if (txtNoPalet.Text != string.Empty)
                strcriteria = strcriteria + " and P.nopalet='" + txtNoPalet.Text + "' ";
            if (RBTgl.Checked == true)
                strQuery = reportFacade.ViewPemetaanT1(periodeAwal, periodeAkhir, tgltype, strcriteria);
            else
                strQuery = reportFacade.ViewPemetaanT1(ThBl, periodeAkhir, 1, strcriteria);
            Session["Query"] = strQuery;
            Session["periode"] = txtFromPostingPeriod.Text + " s/d " + txtToPostingPeriod.Text;
            Cetak(this);
        }
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LPemetaanT1', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
            txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
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
            txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
        }
        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        {
            txtLokasi.Focus();
        }
        protected void txtNoPalet_TextChanged(object sender, EventArgs e)
        {
            txtNoPalet.Focus();
        }
        protected void txtLokasi_TextChanged(object sender, EventArgs e)
        {
            btnPrint.Focus();
        }
    }
}