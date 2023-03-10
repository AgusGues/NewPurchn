using System;
using System.Collections;
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
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    public partial class DelivBeliKaList0 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users user = ((Users)Session["Users"]);
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadLokasiQA();
                string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
                string[] UserApp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "DepoKertas").Split(',');
                int posx = Array.IndexOf(UserViewAll, user.UserName.ToString());
                int posy = Array.IndexOf(UserApp, user.UserName.ToString());
                ddlPlant.SelectedValue = user.UnitKerjaID.ToString();
                ddlPlant.Enabled = (posx > -1) ? true : false;
                //btnApproval.Visible = (posy > -1) ? true : false;
                if (Request.QueryString["v"] != null)
                {
                    switch (Request.QueryString["v"].ToString())
                    {
                        case "1": btnView_Click(null, null); btnBack.Visible = true; break;//dari form inputan
                        case "2": btnBack.Visible = false; break;//list kadar air
                        case "5":
                            btnBack.Visible = false;
                            btnView.Visible = false;
                            btnView_Click(null, null);
                            ddlPlant.Enabled = false;
                            lblFind.Visible = false;
                            txtSupplier.Visible = false;
                            btnCari.Visible = false;
                            break;//approval
                    }
                }
            }
            act1.CompletionSetCount = ((Users)Session["Users"]).UnitKerjaID;
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["v"].ToString() == "5")
            {
                ListKadarAir(true);
            }
            else
            {
                ListKadarAir();
            }
        }
        protected void lstKA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];
            string[] UserApp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "DepoKertas").Split(',');
            int posx = Array.IndexOf(UserApp, user.UserName.ToString());

            QAKadarAir qa = (QAKadarAir)e.Item.DataItem;
            Label stat = (Label)e.Item.FindControl("sts");
            switch (qa.Keputusan)
            {
                case 1: stat.Text = "OK"; break;
                case 2: stat.Text = "Sortir"; break;
                case -1: stat.Text = "NO"; break;
            }
            Image ct = (Image)((Image)e.Item.FindControl("btnPrint"));//print dialog
            ct.Attributes.Add("onclick", "CetakFrom('" + qa.DocNo + "')");
            Image v = (Image)e.Item.FindControl("btnPrev");//print preview
            Image app = (Image)e.Item.FindControl("btnApp");
            app.Attributes.Add("onclick", "window.location ='DelivBeliKA0.aspx?docno=" + qa.DocNo + "'");
            Image pop = (Image)e.Item.FindControl("btnPO");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            Label lbl = (Label)e.Item.FindControl("lblNo");
            if (Request.QueryString["v"].ToString() == "5")
            {
                if (posx > -1)
                {
                    ct.Visible = false;
                    v.Visible = true;
                    chk.Visible = true;
                    lbl.Visible = false;
                    v.Attributes.Add("onclick", "CetakFrom('" + qa.DocNo + "')");
                    app.Visible = true;
                }
                else
                {
                    ct.Visible = (qa.Approval > 1) ? true : false;
                    v.Visible = false;
                    app.Visible = true;
                    chk.Visible = false;
                    lbl.Visible = true;
                }
            }
            else
            {
                string[] ngeprint = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserPrintKA", "DepoKertas").Split(',');
                int pst = Array.IndexOf(ngeprint, ((Users)Session["Users"]).UserName);
                ct.Visible = true;
                v.Visible = false;
                app.Visible = true;
                chk.Visible = false;
                lbl.Visible = true;
                string[] userpo = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserCreatePO", "DepoKertas").Split(',');
                int usrpo = Array.IndexOf(userpo, ((Users)Session["Users"]).UserName);
                //pop.Visible = (usrpo > -1 && qa.Approval > 1) ? true : false;
                // int poid=this.
                pop.Visible = (qa.POKAID > 0) ? false : false;
            }
        }
        protected void lstKA_Command(object sender, RepeaterCommandEventArgs e)
        {
            Image app = (Image)e.Item.FindControl("btnApp");
            
            switch (e.CommandName)
            {
                case "prn":

                    break;
            }
        }
        private void LoadLokasiQA()
        {
            ArrayList arrData = new ArrayList();
            DepoFacade d = new DepoFacade();
            arrData = d.Retrieve();
            ddlPlant.Items.Clear();
            ddlPlant.Items.Add(new ListItem("--Pilih Depo--", "0"));
            foreach (Depo dp in arrData)
            {
                ddlPlant.Items.Add(new ListItem(dp.DepoName, dp.ID.ToString()));
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DelivBeliKA0.aspx");
        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            string ID = "";
            Users user = (Users)Session["Users"];
            string[] AppLevel = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApprovalLevel", "DepoKertas").Split(',');
            int posx = Array.IndexOf(AppLevel, user.UserName);
            DepoKertasKA ka = new DepoKertasKA();
            QAKadarAir qa = new QAKadarAir();
            for (int i = 0; i < lstKA.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstKA.Items[i].FindControl("chk");
                if (chk.Checked == true)
                {
                    ID += chk.ToolTip.ToString() + ",";
                }
            }
            if (ID == "") { return; }
            string Appv = (posx + 1).ToString();
            int result = ka.Approval(Appv, ID);
            if (result > 0)
            {
                ListKadarAir(true);
            }
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            if (txtSupplier.Text == string.Empty) { arrData = new ArrayList(); }
            string Criteria = " and SupplierName like '%" + txtSupplier.Text + "%'";
            Criteria += " And Year(TglCheck)=" + ddlTahun.SelectedValue;
            Criteria += " And month(TglCheck)=" + ddlBulan.SelectedValue;
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            Criteria += (ddlPlant.SelectedIndex == 0) ? "" : "and PlantID=" + ddlPlant.SelectedValue.ToString();
            Criteria += ")beli Order By TglCheck Desc,ID desc";

            arrData = ka.RetrieveBeli(Criteria);
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            ddlTahun.Items.Clear();
            arrData = ka.Tahun();
            int thn = 0;
            foreach (QAKadarAir qa in arrData)
            {
                ddlTahun.Items.Add(new ListItem(qa.Tahun.ToString(), qa.Tahun.ToString()));
                thn = qa.Tahun;
            }
            if (arrData.Count == 0 || thn < DateTime.Now.Year)
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void ListKadarAir()
        {
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            string Criteria = (ddlBulan.SelectedIndex > 0) ? " And Month(TglCheck)=" + ddlBulan.SelectedValue : "";
            Criteria += " And Year(TglCheck)=" + ddlTahun.SelectedValue;
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            Criteria += (ddlPlant.SelectedIndex == 0) ? "" : "and depoID=" + ddlPlant.SelectedValue.ToString();
            Criteria += ")beli Order By DocNo Desc,ID ";

            arrData = ka.RetrieveBeli(Criteria);
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }
        private void ListKadarAir(bool Approval)
        {
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            string[] AppLevel = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApprovalLevel", "DepoKertas").Split(',');
            int posx = Array.IndexOf(AppLevel, user.UserName);
            if (posx > -1)
            {
                string Criteria = " And Approval=" + posx;
                Criteria += " And Year(TglCheck)>2016";
                string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
                //int pos = Array.IndexOf(UserViewAll, AppLevel[posx]);
                Criteria += " And DepoID=" + ddlPlant.SelectedValue.ToString();
                Criteria += ")beli Order By DocNo,ID ";
                arrData = ka.RetrieveBeli(Criteria);
            }
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }

    }
}