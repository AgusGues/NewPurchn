using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Domain;
using BusinessFacade;


namespace GRCweb1.Modul.ListReport
{
    public partial class LapNonDeathStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime FromTgl = DateTime.Now.AddMonths(-3);
                txtTgl1.Text = FromTgl.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                LoadTipeSPP();
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string allQuery = string.Empty;
            //string drTgl = string.Empty;
            //string sdTgl = string.Empty;
            //string PdrTgl = string.Empty;
            //string PsdTgl = string.Empty;
            //drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            //sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            //PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            //PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");

            //Session["drTgl"] = PdrTgl;
            //Session["sdTgl"] = PsdTgl;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ReportFacade reportFacade = new ReportFacade();
            strQuery = reportFacade.ViewDeathStock("NO", ddlTipeSPP.SelectedValue.ToString());
            Session["Query"] = strQuery;
            GroupsPurchn groupsPurchn = new GroupsPurchn();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            groupsPurchn = groupsPurchnFacade.RetrieveById(Convert.ToInt32(ddlTipeSPP.Text));
            string strstock = string.Empty;
            switch (ddlJenisLapBul.SelectedValue)
            {
                case "All":
                    strstock = " (ALL)";
                    break;
                case "Stok":
                    strstock = " (STOCK)";
                    break;
                case "Non Stok":
                    strstock = "  (NON STOCK)";
                    break;
            }

            //Session["Periode"] = PdrTgl + " s/d " +PsdTgl;
            Session["Group"] = groupsPurchn.GroupDescription + strstock;
            Session["termasuk"] = " Barang Belum Masuk Kriteria ";
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapDeathStock', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";
            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        private void LoadTipeSPP()
        {
            //ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                if (groupsPurchn.ID != 6)
                    ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        protected void ddlTipeSPP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void RB3Bulan_CheckedChanged(object sender, EventArgs e)
        {
            if (RB3Bulan.Checked == true)
            {
                DateTime FromTgl = DateTime.Now.AddMonths(-3);
                txtTgl1.Text = FromTgl.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }
        }
        protected void RB6Bulan_CheckedChanged(object sender, EventArgs e)
        {
            DateTime FromTgl = DateTime.Now.AddMonths(-6);
            txtTgl1.Text = FromTgl.Date.ToString("dd-MMM-yyyy");
            txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }

    }
}