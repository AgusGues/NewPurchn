using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Domain;
using BusinessFacade;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapImprovement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadGroupPurchn();
                LoadTahun();
                ddlPlant.SelectedValue = ((Users)Session["Users"]).UnitKerjaID.ToString();
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                rkp.Visible = (rbRekap.Checked == true) ? true : false;
                lst.Visible = (rbDetail.Checked == true) ? true : false;
                ddlBulan.Enabled = (rbRekap.Checked == true) ? false : true;
                GroupID.Enabled = (rbRekap.Checked == true) ? false : true;
            }
        }

        protected void GroupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cari.Enabled = true;
        }
        public void LoadGroupPurchn()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();

            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                if (groupsPurchn.ID != 4 && groupsPurchn.ID != 5)
                    GroupID.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        public void LoadTahun()
        {
            ArrayList arrTahun = new ArrayList();
            ImprovementFacade imp = new ImprovementFacade();
            arrTahun = imp.GetTahunImprovement();
            ddlTahun.DataSource = arrTahun;
            ddlTahun.DataBind();

            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadData()
        {
            ArrayList arrLst = new ArrayList();
            ArrayList newArr = new ArrayList();
            ImprovementFacade imp = new ImprovementFacade();
            int Thn = int.Parse(ddlTahun.SelectedValue.ToString());
            int Bln = int.Parse(ddlBulan.SelectedValue.ToString());
            int Grp = int.Parse(GroupID.SelectedValue.ToString());
            int pln = int.Parse(ddlPlant.SelectedValue.ToString());

            arrLst = imp.RetrieveForReportDetail(Thn, Bln, Grp, pln, ddlCriteria.SelectedValue.ToString());
            int n = 0;
            #region
            //foreach (Improvement inv in arrLst)
            //{
            //    Improvement objInv = new Improvement();
            //        n++;
            //        objInv.ID = n;
            //        objInv.ItemCode = inv.ItemCode;
            //        objInv.ItemName = inv.ItemName;
            //        objInv.UomCode = inv.UomCode;
            //        objInv.Keterangan = inv.Keterangan;
            //        objInv.UnitKerja = inv.UnitKerja;
            //        objInv.Approval = inv.Approval;
            //        newArr.Add(objInv);
            //}
            #endregion
            ListStock.DataSource = arrLst;
            ListStock.DataBind();
        }
        private void LoadDataRekap()
        {
            ArrayList arrRkp = new ArrayList();
            ImprovementFacade imp = new ImprovementFacade();
            int Thn = int.Parse(ddlTahun.SelectedValue.ToString());
            int pln = int.Parse(ddlPlant.SelectedValue.ToString());
            arrRkp = imp.RetrieveForRekap(Thn, pln);

            rkpList.DataSource = arrRkp;
            rkpList.DataBind();
        }
        protected void preview_Click(object sender, EventArgs e)
        {
            if (rbRekap.Checked == true)
            {
                LoadDataRekap();
            }
            else
            {
                LoadData();
            }
        }
        protected void rbDetail_CheckedChanged(object sender, EventArgs e)
        {
            rkp.Visible = false;
            lst.Visible = true;
            rbRekap.Checked = (rbDetail.Checked == true) ? false : true;
            ddlBulan.Enabled = (rbRekap.Checked == true) ? false : true;
            GroupID.Enabled = (rbRekap.Checked == true) ? false : true;
        }
        protected void rbRekap_CheckedChanged(object sender, EventArgs e)
        {
            rkp.Visible = true;
            lst.Visible = false;
            rbDetail.Checked = (rbRekap.Checked == true) ? false : true;
            ddlBulan.Enabled = (rbRekap.Checked == true) ? false : true;
            GroupID.Enabled = (rbRekap.Checked == true) ? false : true;
        }
        protected void printer_Click(object sender, EventArgs e)
        {
            ImprovementFacade imp = new ImprovementFacade();
            ReportFacade reportFacade = new ReportFacade();
            string strQuery = string.Empty;
            int Thn = int.Parse(ddlTahun.SelectedValue.ToString());
            int Bln = int.Parse(ddlBulan.SelectedValue.ToString());
            int Grp = int.Parse(GroupID.SelectedValue.ToString());
            int pln = int.Parse(ddlPlant.SelectedValue.ToString());
            string stk = ddlCriteria.SelectedValue.ToString();
            strQuery = (rbDetail.Checked == true) ?
                reportFacade.LapImpDetail(Thn, Bln, Grp, pln, stk) :
                reportFacade.LapImpRekap(Thn, pln);
            Session["Query"] = strQuery;
            Session["Tahun"] = Thn;
            Session["Bulan"] = imp.nBulan(Bln);
            Session["Plant"] = ddlPlant.SelectedItem.ToString();
            Cetak(this, (rbDetail.Checked == true) ? "ImproveDetail" : "ImproveRekap");
        }

        static public void Cetak(Control page, string PostQuery)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report2.aspx?IdReport=" + PostQuery + "', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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