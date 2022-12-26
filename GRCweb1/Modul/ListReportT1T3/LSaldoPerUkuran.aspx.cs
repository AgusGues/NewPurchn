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
    public partial class LSaldoPerUkuran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                txtTahun.Text = DateTime.Now.Year.ToString();
                ddlBulan.SelectedIndex = DateTime.Now.Month ;
                string strFirst = "1/1/" + txtTahun.Text;
                DateTime dateFirst = DateTime.Parse(strFirst);
                txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strQtyLastMonth = string.Empty;
            string strQtyMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            string ThBl = string.Empty;
            string ThBl0 = string.Empty;

            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string strQuery1 = string.Empty;
            string strQuery2 = string.Empty;
            string thnbln = txtTahun.Text.Trim() + ddlBulan.SelectedValue.PadLeft(2, '0');
            int bln = Convert.ToInt16( ddlBulan.SelectedValue.PadLeft(2, '0'));
            if (bln == 1)
            {
                strQtyLastMonth = "DesQty";
                strQtyMonth = "JanQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (bln == 2)
            {
                strQtyMonth = "FebQty";
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (bln == 3)
            {
                strQtyMonth = "MarQty";
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (bln == 4)
            {
                strQtyMonth = "AprQty";
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (bln == 5)
            {
                strQtyMonth = "MeiQty";
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (bln == 6)
            {
                strQtyMonth = "JunQty";
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (bln == 7)
            {
                strQtyMonth = "JulQty";
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (bln == 8)
            {
                strQtyMonth = "AguQty";
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (bln == 9)
            {
                strQtyMonth = "SepQty";
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (bln == 10)
            {
                strQtyMonth = "OktQty";
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (bln == 11)
            {
                strQtyMonth = "NovQty";
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (bln == 12)
            {
                strQtyMonth = "DesQty";
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }
            if (txtFromPostingPeriod.Text == string.Empty)
                return;
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            strQuery = reportFacade.ViewSaldoPerUkuran(thnbln, periodeAwal, periodeAkhir, strQtyLastMonth);
            strQuery1 = reportFacade.ViewTotalSaldoPerUkuranBP(thnbln, periodeAwal, periodeAkhir, strQtyLastMonth);
            strQuery2 = reportFacade.ViewTotalSaldoPerUkuranOK(thnbln, periodeAwal, periodeAkhir, strQtyLastMonth);
            SaldoInventoryBJFacade saldof = new SaldoInventoryBJFacade();
            TotalSaldoUkuran saldo = new TotalSaldoUkuran();
            TotalSaldoUkuran saldo1 = new TotalSaldoUkuran();
            saldo = saldof.GetBP(strQuery1);
            saldo1 = saldof.GetOK(strQuery2);
            Session["Query"] = strQuery;
            Session["periode"] = ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
            Session["saldoawalbp"] = saldo.Saldoawalbp;
            Session["saldoawalbpkubik"] = saldo.Saldoawalbpkubik;
            Session["saldobp"] = saldo.Saldobp;
            Session["saldobpkubik"] = saldo.Saldobpkubik;
            Session["saldoawalok"] = saldo1.Saldoawalok;
            Session["saldoawalokkubik"] = saldo1.Saldoawalokkubik;
            Session["saldook"] = saldo1.Saldook;
            Session["saldookkubik"] = saldo1.Saldookkubik;
            string strFirst = "1/1/" + txtTahun.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);

            Session["tglawal"] = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue) - 1)).AddDays(-1).Date.ToString("dd-MMM-yyyy");
            Session["tglakhir"] = txtToPostingPeriod.Text;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LSaldoPerUkuran', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFirst = "1/1/" + txtTahun.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);
            if (ddlBulan.SelectedIndex == 0)
                return;
            txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex)).Date.ToString("dd-MMM-yyyy");
            txtToPostingPeriod.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
            if (ddlBulan.SelectedIndex > 0)
            {
                ddlBulan.SelectedIndex = ddlBulan.SelectedIndex;
                txtTahun.Text = txtTahun.Text;
            }
            else
            {
                ddlBulan.SelectedIndex = 12;
                txtTahun.Text = (Convert.ToInt16(txtTahun.Text) - 1).ToString();
            }
        }
    }
}