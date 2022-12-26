using Domain;
using Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportDefect
{
    public partial class ParetoDefectBM : System.Web.UI.Page
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
            //ViewPareto(ddTahun.SelectedItem.Text.Trim()  + ddlBulan.SelectedValue.Trim());
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            strQuery = reportFacade.ViewPareto(ddTahun.SelectedItem.Text.Trim() + ddlBulan.SelectedValue.Trim());
            Session["Query"] = strQuery;
            Session["periode"] = ddlBulan.SelectedItem.Text.Trim() + " " + ddTahun.SelectedItem.Text;
            //Cetak(this);
        }
    }
}