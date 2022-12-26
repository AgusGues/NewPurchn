using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;

namespace GRCweb1.ModalDialog
{
    public partial class InputUangMukaOP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!Page.IsPostBack)
            {
                if (Session["id"] != null)
                {
                    Session["NilaiUangMuka"] = null;
                    LoadData();
                }

            }
        }

        private void LoadData()
        {
            int intBankOutID = int.Parse(Session["id"].ToString());
            txtNoPO.Text = Session["NoPO"].ToString();
        }

        protected void btnBatal_ServerClick(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>window.close();</script>");
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {

            ArrayList arrData = new ArrayList();
            TerminBayar p = new TerminBayar();
            decimal uangmuka = 0;
            decimal t1 = 0; decimal t2 = 0;
            decimal t3 = 0; decimal t4 = 0;
            decimal.TryParse(txtUangMuka.Text, out uangmuka);
            decimal.TryParse(txtUangMuka1.Text, out t1);
            decimal.TryParse(txtUangMuka2.Text, out t2);
            decimal.TryParse(txtUangMuka3.Text, out t3);
            decimal.TryParse(txtUangMuka4.Text, out t4);
            if (uangmuka > 0)
            {
                p.DP = 0;
                p.TerminKe = 0;
                p.JmlTermin = 0;
                p.Persentase = uangmuka;
                p.TotalBayar = 0;
                arrData.Add(p);
            }
            if (t1 > 0)
            {
                p = new TerminBayar();
                p.DP = 0;
                p.TerminKe = 1;
                p.JmlTermin = 0;
                p.Persentase = t1;
                p.TotalBayar = 0;
                arrData.Add(p);
            }
            if (t2 > 0)
            {
                p = new TerminBayar();
                p.DP = 0;
                p.TerminKe = 2;
                p.JmlTermin = 0;
                p.Persentase = t2;
                p.TotalBayar = 0;
                arrData.Add(p);
            }
            if (t3 > 0)
            {
                p = new TerminBayar();
                p.DP = 0;
                p.TerminKe = 3;
                p.JmlTermin = 0;
                p.Persentase = t3;
                p.TotalBayar = 0;
                arrData.Add(p);
            }
            if (t4 > 0)
            {
                p = new TerminBayar();
                p.DP = 0;
                p.TerminKe = 4;
                p.JmlTermin = 0;
                p.Persentase = t4;
                p.TotalBayar = 0;
                arrData.Add(p);
            }
            Session["NilaiUangMuka"] = arrData;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        protected void txtJbayar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}