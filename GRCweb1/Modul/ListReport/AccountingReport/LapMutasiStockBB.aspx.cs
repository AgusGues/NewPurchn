using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReport.AccountingReport
{
    public partial class LapMutasiStockBB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();
                txtKode.Focus();
                LoadTipeSPP();
            }

        }

        private void getYear()
        {
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
                ddTahun.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            prv.Visible = true;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {

            Users users = (Users)Session["Users"];
            if (txtKode.Text == string.Empty && RB_Detail.Checked == true)
            {
                DisplayAJAXMessage(this, "Untuk preview data detail isi terlebih dahulu nama barangnya");
                return;
            }

            ReportFacadeAcc reportFacade = new ReportFacadeAcc();
            string bln = ddlBulan.SelectedValue;
            string thn = ddTahun.SelectedValue;
            string txtJudul = string.Empty;
            string bulane = ddlBulan.SelectedItem.ToString();
            var xx = new System.Text.RegularExpressions.Regex("|");
            string[] IDitems = txtKode.Text.ToString().Split(' ');
            string[] txt = txtKode.Text.ToString().Split('-');
            string frmtPrint = string.Empty;
            string strQuery = string.Empty;
            string periode = string.Empty;
            string judul = string.Empty;
            var nm = string.Join(",", IDitems);
            string nama = string.Empty;
            string txtStock = jStock.SelectedItem.ToString();
            for (int i = 0; i < IDitems.Length - 1; i++)
            {
                nama += IDitems[i] + " ";
            }
            /* dialhikan ke price2 
             * price 2 di isi oleh accounting
             */
            //if (CheckHargaNol(bln, thn) > 0) {
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "CheckDo()", true); return;
            //}
            if (RB_Detail.Checked == true)
            {

                InventoryFacade inv = new InventoryFacade();
                int ItemID = inv.GetItemID(txtKode.Text.ToString(), "Inventory");
                txtJudul = txtStock.ToString();
                Session["periode"] = bulane + " " + thn;
                Session["nm_barang"] = txtKode.Text.ToString();
                reportFacade.MaterialGroup = jStock.SelectedValue;
                strQuery = reportFacade.ViewMutasiStockByName(bln, thn, ItemID.ToString());
                Session["judul"] = txtJudul.ToUpper();
                Session["Query"] = strQuery;
                Cetak(this);
            }
            else
            {
                string FirstPeriod = string.Empty;
                string LastPeriod = string.Empty;
                string blne = bln.ToString();
                string s = new string('0', (2 - bln.Length));
                Int32 lastDay = DateTime.DaysInMonth(Convert.ToInt32(thn), Convert.ToInt32(blne));
                string d = new string('0', (2 - lastDay.ToString().Length));
                string jnStok = jStock.SelectedValue;
                FirstPeriod = thn + s + bln + "01";
                LastPeriod = thn + s + bln + d + lastDay;
                Session["periode"] = bulane + " " + thn;
                Session["judul"] = txtStock.ToUpper();
                strQuery = (jnStok == "10") ? reportFacade.ViewMutasiRepack(FirstPeriod, LastPeriod, jnStok, thn) :
                        reportFacade.ViewMutasiStock(FirstPeriod, LastPeriod, jnStok, thn);
                Session["Query"] = strQuery;
                Cetak2(this);
            }
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ListReport/AccountingReport/Report.aspx?IdReport=LMutasiTransStock','','resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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

        static public void Cetak2(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ListReport/AccountingReport/Report.aspx?IdReport=LMutasiStock', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak2();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private void LoadTipeSPP()
        {
            jStock.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            ArrayList arrGroupsPurchn2 = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(0, 1, "1,2,3", true);
            jStock.Items.Add(new ListItem("-- Pilih Group --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                jStock.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }

            jStock.SelectedValue = "1";
        }
        protected void txtKode_TextChanged(object sender, EventArgs e)
        {
            string itemName = txtKode.Text;
            InventoryFacade inv = new InventoryFacade();
            Inventory itemID = inv.RetrieveByName(itemName);
            txtItemID.Text = itemID.ID.ToString();
        }
        private int CheckHargaNol(string Bulan, string Tahun)
        {
            int result = 0;
            string Message = string.Empty;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            ReportFacadeAcc rpt = new ReportFacadeAcc();
            rpt.Criteria = " and MONTH(R.ReceiptDate)=" + Bulan + " AND YEAR(R.ReceiptDate)=" + Tahun;
            SqlDataReader sdr = da.RetrieveDataByString(rpt.CheckPOHargaNol());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                int n = 0;
                Message = "<div id='msg'><table  style='width:100%;border-collapse:collapse; font-size:z-small'>";
                Message += "<tr class='total'><th class='kotak'>#</th>";
                Message += "<th class='kotak'>PO No</th><th class='kotak'>ItemName</th>";
                Message += "<th class='kotak'>Crc</th><th class='kotak'>Price</th></tr>";
                while (sdr.Read())
                {
                    n++;
                    Message += ((n % 2) == 0) ? "<tr class='EvenRows baris'>" : "<tr class='OddRows baris'>";
                    Message += "<td class='kotak tengah'>" + n + "</td>";
                    Message += "<td class='kotak tengah'>" + sdr["NoPO"].ToString() + "</td>";
                    Message += "<td class='kotak'>" + sdr["ItemName"].ToString() + "</td>";
                    Message += "<td class='kotak tengah'>" + sdr["Crc"].ToString() + "</td>";
                    Message += "<td class='kotak angka'>" + sdr["Price"].ToString() + "</td>";
                    Message += "</tr>";
                }
                Message += "</table></div>";
                mess.InnerHtml = new EncryptPasswordFacade().EncryptToString(Message);

                result = 1;
            }
            return result;
        }
    }
}