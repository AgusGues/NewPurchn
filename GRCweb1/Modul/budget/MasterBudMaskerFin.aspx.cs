using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Budgeting
{
    public partial class MasterBudMaskerFin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
            }
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--Pilih--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            PakaiFacade pd = new PakaiFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
        private int nomor = 0;
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            CostCenterFacade cfd = new CostCenterFacade();
            ArrayList arrData = new ArrayList();
            cfd.Bulan = ddlBulan.SelectedValue.ToString();
            cfd.Tahun = ddlTahun.SelectedValue.ToString();
            cfd.Criteria = " AND Bulan=" + ddlBulan.SelectedValue.ToString();
            cfd.Criteria += " AND Tahun=" + ddlTahun.SelectedValue.ToString();
            arrData = cfd.RetrieveBudgetFinishing(true);
            nomor = 0;
            lstBudget.DataSource = arrData;
            lstBudget.DataBind();
        }
        protected void lstBudget_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string[] YngDiKolomBarang = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BudgetDiKolomBarang", "CostControl").Split(',');
            CostFIN cf = (CostFIN)e.Item.DataItem;
            int posx = Array.IndexOf(YngDiKolomBarang, cf.ItemID.ToString());
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr1");
            TextBox txtB = (TextBox)e.Item.FindControl("txtBarang");
            TextBox txtL = (TextBox)e.Item.FindControl("txtlembar");
            TextBox txtR = (TextBox)e.Item.FindControl("txtRupiah");
            nomor = (cf.RowStatus == 2) ? nomor + 1 : nomor;
            tr.Visible = (cf.GroupName == "LAIN - LAIN" && cf.RowStatus == 1) ? false : true;
            tr.Attributes.Add("class", (cf.RowStatus == 2) ? "Line3 baris" : "EvenRows baris");
            //tr.Cells[0].InnerHtml = (cf.RowStatus == 2) ? nomor.ToString() : cf.ItemID.ToString();
            tr.Cells[0].Align = "Center";
            txtB.Visible = ((cf.RowStatus == 2 || cf.GroupName == "LAIN - LAIN")) ? false : true;
            txtL.Visible = ((cf.RowStatus == 2 || cf.GroupName == "LAIN - LAIN")) ? false : true;
            txtR.Visible = ((cf.RowStatus == 2 && cf.GroupName == "LAIN - LAIN")) ? true : false;
            txtB.Visible = (posx > -1 && cf.RowStatus == 1) ? true : false;// txtB.Visible;
            txtL.Visible = (posx == -1 && cf.RowStatus == 1) ? true : false;
            txtR.Visible = (posx == -1 && cf.RowStatus == 1) ? false : txtR.Visible;
            Image btn = (Image)e.Item.FindControl("btnEdit");
            btn.Visible = (cf.MaterialCCID > 0) ? false : false;
        }
        protected void btnBaru_Click(Object sender, EventArgs e)
        {

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            CostCenterFacade csd = new CostCenterFacade();
            ArrayList arrData = new ArrayList();
            for (int i = 0; i < lstBudget.Items.Count; i++)
            {
                CostFIN cfd = (CostFIN)lstBudget.Items[i].DataItem;
                CostFIN cfj = new CostFIN();
                TextBox txtB = (TextBox)lstBudget.Items[i].FindControl("txtBarang");
                TextBox txtL = (TextBox)lstBudget.Items[i].FindControl("txtlembar");
                TextBox txtR = (TextBox)lstBudget.Items[i].FindControl("txtRupiah");
                HiddenField hdd = (HiddenField)lstBudget.Items[i].FindControl("txtMcID");
                HiddenField hID = (HiddenField)lstBudget.Items[i].FindControl("txtID");
                if (decimal.Parse(txtB.Text) > 0 ||
                    decimal.Parse(txtL.Text) > 0 ||
                    decimal.Parse(txtR.Text) > 0)
                {
                    cfj.MatCCMatrixFinID = int.Parse(hdd.Value);
                    cfj.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                    cfj.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                    cfj.Barang = decimal.Parse(txtB.Text.ToString());
                    cfj.Lembar = decimal.Parse(txtL.Text.ToString());
                    cfj.RupiahPerBln = decimal.Parse(txtR.Text.ToString());
                    cfj.RowStatus = 0;
                    cfj.CreatedBy = ((Users)Session["Users"]).UserName;
                    cfj.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    cfj.CreatedTime = DateTime.Now;
                    cfj.LastModifiedTime = DateTime.Now;
                    cfj.ID = int.Parse(hID.Value);
                    arrData.Add(cfj);
                }
            }
            int result = csd.InsertBudgetFin(arrData);
        }
    }
}