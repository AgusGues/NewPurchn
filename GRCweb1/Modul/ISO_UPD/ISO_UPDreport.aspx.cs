using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class ISO_UPDreport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                string strFirst = "1/1/" + DateTime.Now.Year.ToString();
                DateTime dateFirst = DateTime.Parse(strFirst);
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyy-MM-dd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyy-MM-dd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;
            Users users = (Users)Session["Users"];
            ReportUPDFacade reportFacade = new ReportUPDFacade();
            string strQuery = string.Empty;
            int DeptID = users.DeptID;
            strQuery = reportFacade.ViewReportUPD(periodeAwal, periodeAkhir, DeptID);
            Session["Query"] = strQuery;
            Session["prdawal"] = periodeAwal;
            Session["prdakhir"] = periodeAkhir;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/ReportUPD.aspx?IdReport=LapDokISO', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}