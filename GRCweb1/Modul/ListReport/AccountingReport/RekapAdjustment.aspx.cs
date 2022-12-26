using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.ListReport.AccountingReport
{
    public partial class RekapAdjustment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }
        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            string fromDate = string.Empty;
            string toDate = string.Empty;
            string fDate = string.Empty;
            string tDate = string.Empty;
            string strQuery = string.Empty;
            Users users = (Users)Session["Users"];
            fromDate = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            toDate = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            fDate = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            tDate = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            Session["fromDate"] = fDate;
            Session["toDate"] = tDate;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ReportFacadeAcc reportFacade = new ReportFacadeAcc();
            strQuery = reportFacade.RekapAdjustment(fromDate, toDate, users.DeptID);
            Session["Query"] = strQuery;
            Cetak(this);
        }
        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";
            return string.Empty;
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ListReport/AccountingReport/Report.aspx?IdReport=Adjustment','','resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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