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
    public partial class LapOpname : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                LoadGroup();
                Global.link = "~/Default.aspx";
                string strFirst = "1/1/" + DateTime.Now.Year.ToString();
                DateTime dateFirst = DateTime.Parse(strFirst);
                ddlBulan.SelectedIndex = int.Parse(DateTime.Now.Month.ToString());
                txtTahun.Text = DateTime.Now.Year.ToString();
                txtFromPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtFromPostingPeriod1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                Users users = (Users)Session["Users"];
                string deptID = users.DeptID.ToString();
                Tahun();

                if (deptID == "10")
                { RB1.Visible = true; }
                else
                { RB1.Visible = false; }

            }
        }

        private void LoadGroup()
        {
            ReportOpnameFacade ROF = new ReportOpnameFacade();
            ArrayList arrOpname = ROF.getGroup();
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih --", "0"));

            foreach (GroupsPurchn group in arrOpname)
            {
                ddlGroup.Items.Add(new ListItem(group.GroupDescription, group.ID.ToString()));
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeopname = DateTime.Parse(txtFromPostingPeriod1.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;
            string txPeriodeopname = DateTime.Parse(txtFromPostingPeriod1.Text).ToString("dd MMMM yyyy");
            int DeptID = ((Users)Session["Users"]).DeptID;
            GroupsPurchn grp = new GroupsPurchn();
            ReportOpnameFacade ROF = new ReportOpnameFacade();
            string strError = string.Empty;

            int thn = 0;
            string periodebulan = ddlBulan.SelectedValue;

            if (ddlBulan.SelectedValue != string.Empty)
            {
                if (ddlBulan.SelectedValue == "DesQty")
                {
                    thn = DateTime.Now.Year - 1;
                }
                else
                {
                    thn = int.Parse(txtTahun.Text);
                }
            }

            int groupID = int.Parse(ddlGroup.SelectedValue);

            string strQuery = string.Empty;

            //string itemOil = string.Empty;

            //if (groupID == 9)
            //{
            //    if (RB1.Checked == true)
            //    { itemOil = "1"; }            
            //    else
            //    { itemOil = "0"; }
            //}

            //else

            //{ itemOil = "0"; }

            strQuery = ROF.ViewLapOpname(periodeAwal, periodeAkhir, groupID, periodebulan, thn);

            Session["Query"] = strQuery;
            Session["group"] = " ";
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["groupid"] = groupID;
            Session["periode"] = ddlBulan1.SelectedItem.Text.ToUpper() + " " + ddlTahun.SelectedValue.ToString();
            Session["periodeopname"] = txPeriodeopname;
            //Session["lokasi"] = (((Users)Session["Users"]).UnitKerjaID == 1) ? "Citeureup" : "Karawang";
            if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                Session["lokasi"] = "Citeureup";
            }
            else if (((Users)Session["Users"]).UnitKerjaID == 7)
            {
                Session["lokasi"] = "Karawang";
            }
            else if (((Users)Session["Users"]).UnitKerjaID == 13)
            {
                Session["lokasi"] = "Jombang";
            }

            Cetak(this);

        }

        private void Tahun()
        {
            ArrayList arrData = new ArrayList();
            arrData = new SaldoInventoryFacade().GetTahun();
            ddlTahun.Items.Clear();
            foreach (SaldoInventory si in arrData)
            {
                ddlTahun.Items.Add(new ListItem(si.YearPeriod.ToString(), si.YearPeriod.ToString()));
            }

            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=LapOpname', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
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

        protected void RB1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}