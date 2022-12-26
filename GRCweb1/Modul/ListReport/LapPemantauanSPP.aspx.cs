using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace GRCweb1.Modul.ListReport
{
    public partial class LapPemantauanSPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                GetTahun();
                GetBulan();
                LoadTipeSPP();
                LoadTipeSPP2();
                txtDrTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtSdTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //Preview.Enabled = false;
                //Print.Enabled = false;
            }
        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadData2();
        }
        protected void Print_Click(object sender, EventArgs e)
        {
            SPPFacade Spp = new SPPFacade();
            int Thn = int.Parse(ddlTahun.SelectedValue.ToString());
            int bln = int.Parse(ddlBulan.SelectedValue.ToString());
            int inv = int.Parse(ddlTipeBarang.SelectedValue.ToString());
            int grp = int.Parse(ddlPurchn.SelectedValue.ToString());
            string arrSpp = Spp.QueryPemantauan(Thn, bln, inv, grp);
            Session["Query"] = arrSpp;
            Session["Tahun"] = Thn;
            Session["Bulan"] = ddlBulan.SelectedItem.ToString();
            Session["Group"] = ddlPurchn.SelectedItem.ToString();
            Cetak(this);
        }
        public void GetTahun()
        {
            SPPFacade spp = new SPPFacade();
            ArrayList arrSpp = new ArrayList();
            arrSpp = spp.TahunSPP();
            foreach (SPP sp in arrSpp)
            {
                ddlTahun.Items.Add(new ListItem(sp.ID.ToString(), sp.ID.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        public void GetBulan()
        {
            ddlBulan.Items.Clear();
            int i = 0;
            for (i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        public void LoadData()
        {

            ArrayList arrSpp = new ArrayList();
            SPPFacade Spp = new SPPFacade();
            int Thn = int.Parse(ddlTahun.SelectedValue.ToString());
            int bln = int.Parse(ddlBulan.SelectedValue.ToString());
            int inv = int.Parse(ddlTipeBarang.SelectedValue.ToString());
            int grp = int.Parse(ddlPurchn.SelectedValue.ToString());
            arrSpp = Spp.RetrieveSPP1(Thn, bln, inv, grp);
            lstSPP.DataSource = arrSpp;
            lstSPP.DataBind();

        }

        public void LoadData2()
        {

            ArrayList arrSpp1 = new ArrayList();
            SPPFacade Spp = new SPPFacade();

            int crt = int.Parse(ddlCriteria.SelectedValue.ToString());
            int grouppencarian = int.Parse(ddlGroup.SelectedValue.ToString());
            int tipebarang = int.Parse(ddlTipe.SelectedValue.ToString());
            string kode = txtMasukan.Text;
            string dTglDari = DateTime.Parse(txtDrTgl.Text).ToString("yyyyMMdd");
            string dTglSampai = DateTime.Parse(txtSdTgl.Text).ToString("yyyyMMdd");



            arrSpp1 = Spp.RetrieveSPP(tipebarang, grouppencarian, crt, kode, dTglDari, dTglSampai);
            lstSPP.DataSource = arrSpp1;
            lstSPP.DataBind();





        }


        protected void lstSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView ds = e.Item.DataItem as DataRowView;
            var sb = e.Item.FindControl("lstPO") as Repeater;
            var rc = e.Item.FindControl("lstRMS") as Repeater;
            SPPFacade Spp = new SPPFacade();
            int Thn = int.Parse(ddlTahun.SelectedValue.ToString());
            int bln = int.Parse(ddlBulan.SelectedValue.ToString());
            int inv = int.Parse(ddlTipeBarang.SelectedValue.ToString());
            int grp = int.Parse(ddlPurchn.SelectedValue.ToString());

            //agus
            int crt = int.Parse(ddlCriteria.SelectedValue.ToString());
            int grouppencarian = int.Parse(ddlGroup.SelectedValue.ToString());
            int tipebarang = int.Parse(ddlTipe.SelectedValue.ToString());
            string kode = txtMasukan.Text;
            string dTglDari = DateTime.Parse(txtDrTgl.Text).ToString("yyyyMMdd");
            string dTglSampai = DateTime.Parse(txtSdTgl.Text).ToString("yyyyMMdd");

            //arrPO = Spp.RetrievePO3(tipebarang, grouppencarian, crt, kode, dTglDari, dTglSampai);
            //sb.DataSource = arrPO;
            //sb.DataBind();

            if (sb != null)
            {
                SPPPantau sp = e.Item.DataItem as SPPPantau;
                ArrayList arrPO = new ArrayList();
                ArrayList arrSPP = new ArrayList();
                ArrayList arrRC = new ArrayList();
                if (sp.ID > 0)
                {


                    arrPO = Spp.RetrievePO(Thn, bln, inv, sp.ID, grp);
                    sb.DataSource = arrPO;
                    sb.DataBind();

                    arrRC = Spp.RetrieveRMS(Thn, bln, inv, sp.ID, grp);
                    rc.DataSource = arrRC;
                    rc.DataBind();




                }
            }

        }
        private void LoadTipeSPP()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            int itemtypeid = 0;
            itemtypeid = int.Parse(ddlTipeBarang.SelectedValue.ToString());
            arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).DeptID, ((Users)Session["Users"]).GroupID, itemtypeid);
            ddlPurchn.Items.Clear();
            ddlPurchn.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlPurchn.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void LoadTipeSPP2()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            int itemtypeid = 0;
            itemtypeid = int.Parse(ddlTipe.SelectedValue.ToString());
            arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).DeptID, ((Users)Session["Users"]).GroupID, itemtypeid);
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        protected void ddlPurchn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlTipeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preview.Enabled = true;
            Print.Enabled = true;
            LoadTipeSPP();
        }
        protected void ddlTipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTipeSPP2();
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=LapPantau', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}