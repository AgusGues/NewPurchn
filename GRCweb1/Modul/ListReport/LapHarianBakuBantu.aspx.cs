using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapHarianBakuBantu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                LoadTipeSPP();
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string UnitKerja = users.UnitKerjaID.ToString();
            Session["UnitKerja"] = UnitKerja;

            txtToPostingPeriod.Text = DateTime.Now.Date.ToString();

            if (ddlTipeSPP.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Tipe Laporan");
                return;
            }
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            //DateTime thisDate1 = new DateTime(2011, 6, 10);
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            DateTime tgl1 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, 1);
            DateTime tgl2 = new DateTime();
            if (DateTime.Parse(txtFromPostingPeriod.Text).Day == 1)
                tgl2 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, 1);
            else
                tgl2 = new DateTime(DateTime.Parse(txtFromPostingPeriod.Text).Year, DateTime.Parse(txtFromPostingPeriod.Text).Month, DateTime.Parse(txtFromPostingPeriod.Text).Day - 1);

            string tglAwal = tgl1.ToString("yyyyMMdd");
            string tglAkhir = tgl2.ToString("yyyyMMdd");

            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtFromPostingPeriod.Text;

            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;

            string ketBlnLalu = string.Empty;
            if (blnLalu - 1 == 0)
            {
                ketBlnLalu = "DesQty";
                thn = thn - 1;
            }
            else if (blnLalu - 1 == 1)
                ketBlnLalu = "JanQty";
            else if (blnLalu - 1 == 2)
                ketBlnLalu = "FebQty";
            else if (blnLalu - 1 == 3)
                ketBlnLalu = "MarQty";
            else if (blnLalu - 1 == 4)
                ketBlnLalu = "AprQty";
            else if (blnLalu - 1 == 5)
                ketBlnLalu = "MeiQty";
            else if (blnLalu - 1 == 6)
                ketBlnLalu = "JunQty";
            else if (blnLalu - 1 == 7)
                ketBlnLalu = "JulQty";
            else if (blnLalu - 1 == 8)
                ketBlnLalu = "AguQty";
            else if (blnLalu - 1 == 9)
                ketBlnLalu = "SepQty";
            else if (blnLalu - 1 == 10)
                ketBlnLalu = "OktQty";
            else if (blnLalu - 1 == 11)
                ketBlnLalu = "NovQty";

            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["lapbul"] = null;
            Session["jenis"] = null;


            LapBulAll lapBulAll = new LapBulAll();
            int itemtypeID = 1;
            int userID = ((Users)Session["Users"]).ID;
            int groupID = Convert.ToInt32(ddlTipeSPP.Text);
            if (groupID == 4)
                itemtypeID = 2;
            if (groupID == 5)
                itemtypeID = 3;

            ReportFacade reportFacade = new ReportFacade();
            string strQuery = string.Empty;

            if (DateTime.Parse(txtFromPostingPeriod.Text).Day == 1)
                strQuery = reportFacade.ViewHarianBakuBantu3a(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID, itemtypeID);
            else
                strQuery = reportFacade.ViewHarianBakuBantu3(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID, itemtypeID);
            Session["groupid"] = groupID;
            Session["Query"] = strQuery;
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["lapbul"] = "PENUNJANG";
            Session["jenis"] = ddlTipeSPP.SelectedItem.Text;
            string LapBul = "LapBul";
            Session["Elap"] = LapBul;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapLapBul', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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

        private void LoadTipeSPP()
        {
            ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            //arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).GroupID);
            //arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe Barang --", string.Empty));
            ddlTipeSPP.Items.Add(new ListItem("Bahan Baku", "1"));
            ddlTipeSPP.Items.Add(new ListItem("Bahan Bantu", "2"));
            ddlTipeSPP.Items.Add(new ListItem("Marketing", "7"));
            ddlTipeSPP.Items.Add(new ListItem("Repack", "10"));
            ddlTipeSPP.Items.Add(new ListItem("Bahan Bakar", "11"));
            ddlTipeSPP.Items.Add(new ListItem("Barang Non GRC", "13"));
            ddlTipeSPP.Items.Add(new ListItem("Repack Non GRC", "14"));
            //foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            //{
            //    ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            //}

            ddlTipeSPP.SelectedIndex = 1;
        }
        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTipeSPP.SelectedIndex > 0)
            //{
            //    if (ddlTipeSPP.SelectedIndex == 5)
            //        ddlTipeBarang.SelectedIndex = 2;
            //    else if (ddlTipeSPP.SelectedIndex == 6)
            //        ddlTipeBarang.SelectedIndex = 3;
            //    else
            //        ddlTipeBarang.SelectedIndex = 1;
            //}
            //else
            //    ddlTipeBarang.SelectedIndex = 0;

        }

    }
}