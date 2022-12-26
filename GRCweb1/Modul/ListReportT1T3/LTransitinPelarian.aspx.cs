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
    public partial class LTransitinPelarian : System.Web.UI.Page
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
                txtFromPostingPeriod2.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod2.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
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
            string periodeAwal2 = DateTime.Parse(txtFromPostingPeriod2.Text).ToString("yyyyMMdd");
            string periodeAkhir2 = DateTime.Parse(txtToPostingPeriod2.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            string tglProduksi = string.Empty;
            string tglJemur = string.Empty;

            string kriteria = string.Empty;
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
            string periode = string.Empty;
            if (ChkTglInput.Checked == true)//&& ChkTglProduksi.Checked==false
            {
                periode = periode + " Tgl. Input : " + DateTime.Parse(txtFromPostingPeriod2.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod2.Text).ToString("dd/MM/yyyy");
                kriteria = kriteria + " and CONVERT(char(8),createdtime, 112)>='" + periodeAwal2 + "' AND CONVERT(char(8), createdtime, 112)<='" + periodeAkhir2 + "' ";
            }
            if (ChkTglProduksi.Checked == true) //&& ChkTglSerah.Checked ==false 
            {
                periode = periode + " Tgl. Produksi : " + DateTime.Parse(txtFromPostingPeriod.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod.Text).ToString("dd/MM/yyyy");
                kriteria = kriteria + " and CONVERT(char(8),TglProduksi, 112)>='" + periodeAwal + "' AND CONVERT(char(8), TglProduksi, 112)<='" +
                    periodeAkhir + "' ";
            }
            if (ChkTglSerah.Checked == true)//&& ChkTglProduksi.Checked==false
            {
                periode = periode + " Tgl. Serah : " + DateTime.Parse(txtFromPostingPeriod0.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod0.Text).ToString("dd/MM/yyyy");
                kriteria = kriteria + " and CONVERT(char(8), Tglserah, 112)>='" + periodeAwal0 + "' AND CONVERT(char(8), Tglserah, 112)<='" + periodeAkhir0 + "' ";
            }
            if (ChkTglJemur.Checked == true)//&& ChkTglProduksi.Checked==false
            {
                periode = periode + " Tgl. Jemur : " + DateTime.Parse(txtFromPostingPeriod1.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod1.Text).ToString("dd/MM/yyyy");
                kriteria = kriteria + " and CONVERT(char(8),TglJemur, 112)>='" + periodeAwal1 + "' AND CONVERT(char(8), TglJemur, 112)<='" + periodeAkhir1 + "' ";
            }
            string partNo = string.Empty;
            if (txtPartno.Text.Trim().Length > 10)
                partNo = " and (PartNo3='" + txtPartno.Text.Trim() + "') ";
            strQuery = reportFacade.ViewLTransitInPel(kriteria + partNo);
            Session["Query"] = strQuery;
            Session["periode"] = periode;
            Cetak(this);
        }
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LTransitInPelarian', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
        protected void ChkTglJemur_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglJemur.Checked == true)
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
        protected void txtFromPostingPeriod1_TextChanged(object sender, EventArgs e)
        {
            txtToPostingPeriod1.Text = txtFromPostingPeriod1.Text;
        }
        protected void ChkTglInput_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglInput.Checked == true)
            {
                txtFromPostingPeriod2.Visible = true;
                txtToPostingPeriod2.Visible = true;
            }
            else
            {
                txtFromPostingPeriod2.Visible = false;
                txtToPostingPeriod2.Visible = false;
            }
        }
        protected void txtFromPostingPeriod2_TextChanged(object sender, EventArgs e)
        {
            txtToPostingPeriod2.Text = txtFromPostingPeriod2.Text;
        }
    }
}