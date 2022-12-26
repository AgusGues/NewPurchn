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
    public partial class LTransitH : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtFromPostingPeriod0.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod0.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtFromPostingPeriod1.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod1.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
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
            string periodeAwal0 = DateTime.Parse(txtFromPostingPeriod0.Text).ToString("yyyyMMdd");
            string periodeAkhir0 = DateTime.Parse(txtToPostingPeriod0.Text).ToString("yyyyMMdd");
            string periodeAwal1 = DateTime.Parse(txtFromPostingPeriod1.Text).ToString("yyyyMMdd");
            string periodeAkhir1 = DateTime.Parse(txtToPostingPeriod1.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;
            string tglProduksi = string.Empty;
            string tglSerah = string.Empty;

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

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            int tgltype = 0;
            if (RBTglProduksi.Checked == true)
                tgltype = 1;
            else
                tgltype = 2;
            if (ChkTglProduksi.Checked == true)//&& ChkTglSerah.Checked == false
            {
                tgltype = 1;
                Session["periode"] = "Tgl. Produksi : " + DateTime.Parse(txtFromPostingPeriod.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod.Text).ToString("dd/MM/yyyy");
                tglProduksi = " and CONVERT(char(8), A.TglProduksi, 112)>='" + periodeAwal + "' AND CONVERT(char(8), A.TglProduksi, 112)<='" + periodeAkhir + "' ";
            }
            if (ChkTglSerah.Checked == true)//&& ChkTglProduksi.Checked==false
            {
                tgltype = 2;
                Session["periode"] = "Tgl. Serah : " + DateTime.Parse(txtFromPostingPeriod0.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod0.Text).ToString("dd/MM/yyyy");
                tglSerah = " and CONVERT(char(8), B.tglserah, 112)>='" + periodeAwal0 + "' AND CONVERT(char(8),  B.tglserah, 112)<='" + periodeAkhir0 + "' ";
            }
            if (ChkTglInput.Checked == true)//&& ChkTglProduksi.Checked==false
            {
                tgltype = 2;
                Session["periode"] = "Tgl. Serah : " + DateTime.Parse(txtFromPostingPeriod0.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod0.Text).ToString("dd/MM/yyyy");
                tglSerah = " and CONVERT(char(8), C.Createdtime, 112)>='" + periodeAwal1 + "' AND CONVERT(char(8),  C.Createdtime, 112)<='" + periodeAkhir1 + "' ";
            }
            if (ChkTglSerah.Checked == true && ChkTglProduksi.Checked == true)
            {
                Session["periode"] = "Tgl. Produksi : " + DateTime.Parse(txtFromPostingPeriod.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod.Text).ToString("dd/MM/yyyy") +
                ", Tgl. Serah : " + DateTime.Parse(txtFromPostingPeriod0.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod0.Text).ToString("dd/MM/yyyy");
            }

            string partno = string.Empty;
            if (txtPartno.Text.Trim().Length > 10)
                partno = " and (I1.partno='" + txtPartno.Text.Trim() + "' OR I2.partno=' " + txtPartno.Text.Trim() + "') ";
            strQuery = reportFacade.ViewLTransitH(tglProduksi, tglSerah, tgltype, partno);
            Session["Query"] = strQuery;

            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LTransitH', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
        protected void ChkTglProduksi_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglProduksi.Checked == true)
            {
                txtFromPostingPeriod.Visible = true;
                txtToPostingPeriod.Visible = true;
            }
            else
            {
                txtFromPostingPeriod.Visible = false;
                txtToPostingPeriod.Visible = false;
            }
        }
        protected void ChkTglSerah_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglSerah.Checked == true)
            {
                txtFromPostingPeriod0.Visible = true;
                txtToPostingPeriod0.Visible = true;
            }
            else
            {
                txtFromPostingPeriod0.Visible = false;
                txtToPostingPeriod0.Visible = false;
            }
        }
        protected void txtFromPostingPeriod0_TextChanged(object sender, EventArgs e)
        {
            txtToPostingPeriod0.Text = txtFromPostingPeriod0.Text;
        }
        protected void ChkTglInput_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglInput.Checked == true)
            {
                txtFromPostingPeriod1.Visible = true;
                txtToPostingPeriod1.Visible = true;
            }
            else
            {
                txtFromPostingPeriod1.Visible = false;
                txtToPostingPeriod1.Visible = false;
            }
        }
    }
}