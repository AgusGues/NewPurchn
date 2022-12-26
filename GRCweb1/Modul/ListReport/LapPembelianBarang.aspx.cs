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
    public partial class LapPembelianBarang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                LoadTipeSPP();
                LoadTahun();
                ddlBulan.SelectedIndex = int.Parse(DateTime.Now.Month.ToString());
                txtTahun.SelectedValue = DateTime.Now.Date.Year.ToString();
            }
        }
        private void LoadTahun()
        {
            ArrayList arrSI = new ArrayList();
            PakaiFacade pakai = new PakaiFacade();
            arrSI = pakai.GetYearTrans();
            txtTahun.Items.Clear();
            foreach (Pakai objSI in arrSI)
            {
                txtTahun.Items.Add(new ListItem(objSI.Tahun.ToString(), objSI.Tahun.ToString()));
            }

        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int cekNumber = int.Parse(txtTahun.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Tahun harus angka");
                return;
            }

            if (ddlBulan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Bulan");
                return;

            }

            string strError = string.Empty;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');

            int groupID = 0;
            groupID = ddlTipeSPP.SelectedIndex;

            int itemTypeid = 0;
            if (groupID == 4)
                itemTypeid = 2;
            else if (groupID == 5)
                itemTypeid = 3;
            else
                itemTypeid = 1;

            int tahun = 0;
            if (ddlBulan.SelectedIndex == 1)
                tahun = int.Parse(txtTahun.Text) - 1;
            else
                tahun = int.Parse(txtTahun.Text);

            string strQtyLastMonth = string.Empty;
            string strAvgPriceLastMonth = string.Empty;
            if (ddlBulan.SelectedIndex == 1)
            {
                strQtyLastMonth = "DesQty";
                strAvgPriceLastMonth = "DesAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strQtyLastMonth = "JanQty";
                strAvgPriceLastMonth = "JanAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strQtyLastMonth = "FebQty";
                strAvgPriceLastMonth = "febAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strQtyLastMonth = "MarQty";
                strAvgPriceLastMonth = "MarAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strQtyLastMonth = "AprQty";
                strAvgPriceLastMonth = "AprAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strQtyLastMonth = "MeiQty";
                strAvgPriceLastMonth = "MeiAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strQtyLastMonth = "JunQty";
                strAvgPriceLastMonth = "JunAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strQtyLastMonth = "JulQty";
                strAvgPriceLastMonth = "JulAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strQtyLastMonth = "AguQty";
                strAvgPriceLastMonth = "AguAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strQtyLastMonth = "SepQty";
                strAvgPriceLastMonth = "SepAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strQtyLastMonth = "OktQty";
                strAvgPriceLastMonth = "OktAvgPrice";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strQtyLastMonth = "NovQty";
                strAvgPriceLastMonth = "NovAvgPrice";
            }

            Session["Query"] = null;
            Session["bulan"] = null;
            Session["tahun"] = null;
            Session["lapbul"] = null;

            if (ddlTipeSPP.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Tipe Laporan");
                return;
            }


            ReportFacade reportFacade = new ReportFacade();
            string strQuery = string.Empty;
            strQuery = reportFacade.ViewPembelianBarang(strQtyLastMonth, strAvgPriceLastMonth, tahun, ThBl, itemTypeid, groupID);

            Session["Query"] = strQuery;
            Session["bulan"] = ddlBulan.SelectedItem;
            Session["tahun"] = tahun;
            Session["lapbul"] = ddlTipeSPP.SelectedItem;

            Cetak(this);



        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=RekapPembelianBarang', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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
            //ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            //arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).GroupID);
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}