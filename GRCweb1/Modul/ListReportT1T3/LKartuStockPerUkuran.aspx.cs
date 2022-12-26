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
    public partial class LKartuStockPerUkuran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                txtTahun.Text = DateTime.Now.Year.ToString();
                getYear();
            }
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddlTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            txtTahun.Text = ddlTahun.SelectedValue;
        }
        protected void LoadUkuran()
        {
            string thnbln0 = string.Empty;
            string thnbln = string.Empty;
            if (ddlBulan.SelectedValue == "1")
            {
                thnbln0 = (Convert.ToInt32(txtTahun.Text.Trim()) - 1).ToString() + "12";
                thnbln = txtTahun.Text.Trim() + ddlBulan.SelectedValue.PadLeft(2, '0');
            }
            else
            {
                thnbln0 = txtTahun.Text.Trim() + (ddlBulan.SelectedIndex - 1).ToString().PadLeft(2, '0');
                thnbln = txtTahun.Text.Trim() + ddlBulan.SelectedValue.PadLeft(2, '0');
            }
            string vw = string.Empty;
            if (RBOK.Checked == true)
            {
                vw = "vw_T3SaldoAwalOK";
            }
            if (RBKW.Checked == true)
            {
                vw = "vw_T3SaldoAwalKW";
            }
            if (RBBP.Checked == true)
            {
                vw = "vw_T3SaldoAwalBP";
            }
            FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
            ArrayList arrItems = ItemsFacade.RetrieveByUkuran(thnbln0, thnbln, vw);
            if (ItemsFacade.Error == string.Empty)
            {
                ddlUkuran.Items.Clear();
                ddlUkuran.Items.Add(new ListItem("-- Pilih Ukuran --", "0"));
                foreach (FC_Items Items in arrItems)
                {
                    ddlUkuran.Items.Add(new ListItem(Items.Ukuran, Items.Ukuran));
                }
            }
        }
        protected void LoadItems()
        {
            FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
            ArrayList arrItems = ItemsFacade.RetrieveByItems(ddlUkuran.SelectedItem.Text);
            if (ItemsFacade.Error == string.Empty)
            {
                ddlItems.Items.Clear();
                ddlItems.Items.Add(new ListItem("All Items", "0"));
                foreach (FC_Items Items in arrItems)
                {
                    ddlItems.Items.Add(new ListItem(Items.Items, Items.Items));
                }
            }
        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string ukuran = string.Empty;
            string items = string.Empty;
            Users users = (Users)Session["Users"];
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string thnbln0 = string.Empty;
            string thnbln = string.Empty;
            if (ddlBulan.SelectedIndex <= 0 || ddlUkuran.SelectedIndex <= 0 || txtTahun.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Data belum lengkap");
                return;
            }
            if (ddlBulan.SelectedValue == "1")
            {
                thnbln0 = (Convert.ToInt32(txtTahun.Text.Trim()) - 1).ToString() + "12";
                thnbln = txtTahun.Text.Trim() + ddlBulan.SelectedValue.PadLeft(2, '0');
            }
            else
            {
                thnbln0 = txtTahun.Text.Trim() + (ddlBulan.SelectedIndex - 1).ToString().PadLeft(2, '0');
                thnbln = txtTahun.Text.Trim() + ddlBulan.SelectedValue.PadLeft(2, '0');
            }
            if (RBOK.Checked == true)
            {
                strQuery = reportFacade.ViewKartuStockPerUkuranOK(thnbln0, thnbln, ddlUkuran.SelectedItem.Text, ddlItems.SelectedItem.Text);
                Session["kwalitas"] = "OK";
            }
            if (RBKW.Checked == true)
            {
                strQuery = reportFacade.ViewKartuStockPerUkuranKW(thnbln0, thnbln, ddlUkuran.SelectedItem.Text, ddlItems.SelectedItem.Text);
                Session["kwalitas"] = "KW";
            }
            if (RBBP.Checked == true)
            {
                strQuery = reportFacade.ViewKartuStockPerUkuranBP(thnbln0, thnbln, ddlUkuran.SelectedItem.Text, ddlItems.SelectedItem.Text);
                Session["kwalitas"] = "BP";
            }
            string strFirst = "1/1/" + txtTahun.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);
            Session["Query"] = strQuery;
            Session["tglawal"] = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedValue) - 1)).AddDays(0).Date.ToString("dd-MMM-yyyy");
            Session["tglakhir"] = txtToPostingPeriod.Text;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LKartuStockPerUkuran', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
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
            txtFromPostingPeriod.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan.SelectedIndex - 1)).Date.ToString("dd-MMM-yyyy");
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
            LoadUkuran();
        }
        protected void ddlUkuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
        }
        protected void RBOK_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlBulan.SelectedIndex > 0)
                LoadUkuran();
        }
        protected void RBKW_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlBulan.SelectedIndex > 0)
                LoadUkuran();
        }
        protected void RBBP_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlBulan.SelectedIndex > 0)
                LoadUkuran();
        }
    }
}