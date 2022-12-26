using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.planningform
{
    public partial class ProductionPlanning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                //base.LogActivity("Planning Page", true);
                LoadBulan();
                LoadTahun();
                LoadData();
            }
        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            Planning pl = new Planning();
            PlanningFacade pf = new PlanningFacade();
            pl.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
            pl.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
            pl.RunningLine = ddlRunning.SelectedValue.ToString();
            pl.Keterangan = txtKeterangan.Text;
            pl.Revision = (GetPlanningRevision());
            int rst = pf.Insert(pl);
            if (rst > 0)
            {
                LoadData();
                txtKeterangan.Text = "";
            }
        }

        private int GetPlanningRevision()
        {
            int result = 0;
            string periode = " AND Bulan=" + ddlBulan.SelectedValue + " and Tahun=" + ddlTahun.SelectedValue;
            ArrayList arrData = new PlanningFacade().Retrieve(periode, true);
            if (arrData.Count > 0)
            {
                foreach (Planning p in arrData)
                {
                    result = p.Revision + 1;
                }
            }
            return result;
        }
        protected void btnList_Click(object sender, EventArgs e)
        {

        }
        protected void lstPlanning_Command(object sender, RepeaterCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            Users user = (Users)Session["Users"];
            Planning p = new Planning();
            PlanningFacade pf = new PlanningFacade();
            int result = 0;
            switch (e.CommandName)
            {
                case "delete":

                    p.ID = int.Parse(ID.ToString());
                    p.RowStatus = -1;
                    p.Keterangan = "Deleted by " + user.UserName;
                    p.CreatedBy = user.UserID;
                    result = pf.Delete(p);
                    if (result > 0)
                    {
                        LoadData();
                    }
                    break;
                case "closing":
                    p = new Planning();
                    pf = new PlanningFacade();
                    p.ID = int.Parse(ID.ToString());
                    result = pf.ClosingPlaning(p.ID, ddlTahun.SelectedValue, ddlBulan.SelectedValue);
                    if (result > 0)
                    {
                        DisplayAJAXMessage(this, "closing planing selesai, cek hasil di cost monitoring");
                    }
                    break;
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void lstPlanning_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Planning p = (Planning)e.Item.DataItem;
            //check ID sudah pernah di pakai apa belum
            //jika sudah button delete di hied
            PakaiFacade pk = new PakaiFacade();
            ((Image)e.Item.FindControl("hps")).Visible = (pk.GetPlanningIDUsed(p.ID) > 0) ? false : true;
        }
        private void LoadData()
        {
            ArrayList arrData = new ArrayList();
            string Criteria = " AND Bulan=" + ddlBulan.SelectedValue.ToString();
            Criteria += " AND Tahun=" + ddlTahun.SelectedValue.ToString();
            arrData = new PlanningFacade().Retrieve(Criteria);
            lstPlanning.DataSource = arrData;
            lstPlanning.DataBind();
        }
        private void LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            PlanningFacade pnf = new PlanningFacade();
            ddlTahun.Items.Clear();
            arrData = pnf.RetrieveTahun();
            if (arrData.Count > 0)
            {
                foreach (Planning p in arrData)
                {
                    ddlTahun.Items.Add(new ListItem(p.Tahun.ToString(), p.Tahun.ToString()));
                }
            }
            else
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        protected void btnClosing_Click(object sender, EventArgs e)
        {

        }
    }
}