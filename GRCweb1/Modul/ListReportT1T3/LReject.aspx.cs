using Domain;
using Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LReject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();
            }
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string periodeBulan = ddlBulan.SelectedValue;
            string txtBulan = ddlBulan.SelectedItem.ToString();
            string periodeTahun = ddTahun.SelectedValue;
            string frmtPrint = string.Empty;
            string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            Users users = (Users)Session["Users"];
            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            Session["periode"] = txtBulan + " " + periodeTahun;
            if (ddlDept.SelectedItem.Text == "Logistik")
            {
                if (RBBulan.Checked == true)
                    strQuery = reportFacade.ViewLRejectB(periodeTahun + padbulan);
                else
                    strQuery = reportFacade.ViewLRejectH(periodeTahun + padbulan);
            }
            else
                if (RBBulan.Checked == true)
                strQuery = reportFacade.ViewLRejectFB(periodeTahun + padbulan);
            else
                strQuery = reportFacade.ViewLRejectFH(periodeTahun + padbulan);
            if (RBBulan.Checked == true)
                Session["jenis"] = "Bulanan";
            else
                Session["jenis"] = "Harian";
            Session["Query"] = strQuery;
            Session["dept"] = ddlDept.SelectedItem.Text;
            //Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LReject', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getYear();
        }
    }
}