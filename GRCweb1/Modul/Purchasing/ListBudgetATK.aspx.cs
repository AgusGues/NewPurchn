using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ListBudgetATK : System.Web.UI.Page
    {
        private string linkFrom = string.Empty;
        public string Expr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Session["SPP"] = null;
                Global.link = "~/Default.aspx";
                string[] DeptCreateSPP = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("CreateSPP", "BudgetATK").Split(',');
                linkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";

                btnBack.Visible = (linkFrom == "1") ? true : false;
                btnApproval.Visible = (linkFrom == "3" && ((Users)Session["Users"]).Apv > 0) ? true : false;
                btnApproval.Enabled = (linkFrom == "3" && ((Users)Session["Users"]).Apv > 0) ? true : false;
                btnToSPP.Visible = (linkFrom == "4" && DeptCreateSPP.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
                filter.Visible = (linkFrom == "2") ? true : false;
                flToSPP.Visible = (linkFrom == "4") ? true : false;
                approv.Visible = (linkFrom != "4") ? true : false;
                ToSPP.Visible = (linkFrom == "4") ? true : false;

                if (linkFrom == "2")
                {
                    ctn.Attributes.Add("style", "height:410px");
                    GetBulan(ddlBulan);
                    GetTahun(ddlTahun);
                    LoadDept();
                    LoadList("2");
                }
                else if (linkFrom == "4")
                {
                    string BackMonth = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SPPBackMonth", "BudgetATK");
                    ddlBulan1.Enabled = (BackMonth == "1") ? true : false;
                    ddlBulan1.AutoPostBack = (BackMonth == "1") ? true : false;
                    ddlTahun1.Enabled = (BackMonth == "1") ? true : false;
                    GetBulan(ddlBulan1);
                    GetTahun(ddlTahun1);
                    int bUlan = (DateTime.Now.Month + 1);
                    int tAhun = (bUlan == 1) ? (DateTime.Now.Year) + 1 : DateTime.Now.Year;
                    ddlBulan1.SelectedValue = (bUlan > 12) ? "1" : bUlan.ToString();
                    //ddlTahun1.SelectedValue = tAhun.ToString() + 1;
                    if (ddlBulan1.SelectedValue == "1")
                    {
                        ddlTahun1.SelectedValue = (tAhun + 1).ToString();
                    }
                    else
                    {
                        ddlTahun1.SelectedValue = tAhun.ToString() + 1;
                    }
                    //ddlTahun1.SelectedValue = (tAhun + 1).ToString();
                    ctn.Attributes.Add("style", "height:420px");
                    LoadListForSPP();

                }
                else
                {
                    ctn.Attributes.Add("style", "height:450px");
                    LoadList(linkFrom);
                }
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            linkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            if (linkFrom == "4")
            {
                ctn.Attributes.Add("style", "height:420px");
                LoadListForSPP();
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Expr = "print";
            foreach (RepeaterItem rpt in lstBulan.Items)
            {
                Repeater lsDept = (Repeater)rpt.FindControl("lstDept");
                foreach (RepeaterItem dep in lsDept.Items)
                {
                    Repeater lsATK = (Repeater)dep.FindControl("lstATK");
                    foreach (RepeaterItem atk in lsATK.Items)
                    {
                        ((TextBox)atk.FindControl("txtQty")).Visible = false;
                        ((TextBox)atk.FindControl("txtApvQty")).Visible = false;
                        ((Label)atk.FindControl("lblQty")).Visible = true;
                        ((Label)atk.FindControl("lblApv")).Visible = true;
                    }
                }
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListBudget.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>ListBudget</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            approv.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void GetBulan(DropDownList ddl)
        {
            ddl.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddl.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddl.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void GetTahun(DropDownList ddl)
        {
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "Tahun";
            bg.Prefix = " Top 3 ";
            ArrayList arrData = bg.Retrieve();
            ddl.Items.Clear();
            //ddl.Items.Add(new ListItem(((DateTime.Now.Year)).ToString(), ((DateTime.Now.Year)).ToString()));
            ddl.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));
            foreach (Budget b in arrData)
            {
                ddl.Items.Add(new ListItem(b.Tahun.ToString(), b.Tahun.ToString()));
            }
            ddl.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadDept()
        {
            ArrayList arrData = new ArrayList();
            arrData = new DeptFacade().Retrieve();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--Pilih Dept--", "0"));
            foreach (Dept dp in arrData)
            {
                ddlDept.Items.Add(new ListItem(dp.DeptName, dp.ID.ToString()));
            }
            if (((Users)Session["Users"]).DeptID == 10 || ((Users)Session["Users"]).DeptID == 14)
            {
                ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
            }
            else
            {
                ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
                ddlDept.Enabled = false;
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadList("2");
        }
        private void LoadListForSPP()
        {
            Session["SPP"] = null;
            ArrayList arrData = new ArrayList();
            BudgetingFacade bdg = new BudgetingFacade();
            bdg.Pilihan = "ToSPP";
            bdg.Prefix = " Order by ItemName";
            bdg.Criteria = " and Tahun=" + ddlTahun1.SelectedValue + " and Bulan=" + ddlBulan1.SelectedValue.ToString();
            bdg.Criteria += " and (SPPID is null or SPPID <1)  ";
            bdg.Criteria += " and SPPID not in(Select ID from SPP where ID=BudgetATKDetail.SPPID and RowStatus>-1)";
            switch (((Users)Session["Users"]).DeptID)
            {
                case 10:
                    bdg.Field = " and ItemID in((Select ID from Inventory where Head=1)) and Approval=2";
                    break;
                case 7:
                    bdg.Field = " and ItemID in((Select ID from Inventory where Head=2)) and Approval=2";
                    break;
                default:
                    bdg.Field = "";
                    break;
            }
            arrData = bdg.Retrieve();
            lsttoSPP.DataSource = arrData;
            lsttoSPP.DataBind();
            Session["SPP"] = arrData;

        }
        private void LoadList(string LinkFrom)
        {
            string UserIDUser = ((Users)Session["Users"]).ID.ToString();
            ArrayList arrData = new ArrayList();
            BudgetingFacade bg = new BudgetingFacade();
            //check user yang approve dari field head di table dept
            DeptFacade dp = new DeptFacade();
            string DeptYngDiApprove = dp.RetrieveDeptForApproval(((Users)Session["Users"]).ID);
            string[] viewAllApv0 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllApv0", "BudgetATK").Split(',');
            string[] viewAllApv1 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllApv1", "BudgetATK").Split(',');
            string[] UserIDEndApv = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserID", "BudgetATK").Split(',');
            //Tambahan Feb 2018
            string[] UserIDLog = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserIDLog", "BudgetATK").Split(',');

            //string blnApprove = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BulanApprove", "BudgetATK");
            //blnApprove = (blnApprove == string.Empty) ? ">=" : blnApprove;

            if (UserIDLog.Contains(UserIDUser))
            {
                string blnApprove2 = ">";
                Session["blnApprove"] = blnApprove2;
                btnApproval.Visible = false;
            }
            else
            {
                string blnApprove2 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BulanApprove", "BudgetATK");
                blnApprove2 = (blnApprove2 == string.Empty) ? ">=" : blnApprove2;
                Session["blnApprove"] = blnApprove2;
            }

            string blnApprove = Session["blnApprove"].ToString();

            bg.Pilihan = "BulanBudget";
            bg.Field = "SELECT DISTINCT Bulan,Tahun From ( ";

            if (LinkFrom == "2")
            {
                bg.Criteria = (ddlDept.SelectedIndex == 0 && viewAllApv0.Contains(((Users)Session["Users"]).DeptID.ToString())) ? "" : " and DeptID=" + ddlDept.SelectedValue.ToString();
            }
            else if (LinkFrom == "3")
            {
                if (UserIDEndApv.Contains(UserIDUser))
                {
                    bg.Criteria = "and (bad.Approval=1 or (bad.approval=0 and bg.DeptiD in (6,10)))";
                }
                else if (UserIDLog.Contains(UserIDUser))
                {
                    bg.Criteria = "and (bad.Approval>=0)";
                }
                else
                {
                    bg.Criteria = (viewAllApv0.Contains(((Users)Session["Users"]).DeptID.ToString())) ? "" :
                        //" and DeptID in(" + ddlDept.SelectedValue + ") and bad.Approval <= 1";
                        " and DeptID in(" + ((Users)Session["Users"]).DeptID + ") and bad.Approval <= 1";
                }
            }

            bg.Criteria += (LinkFrom == "2") ? " and Tahun=" + ddlTahun.SelectedValue.ToString() : "";
            bg.Criteria += (LinkFrom == "2") ? " and Bulan=" + ddlBulan.SelectedValue.ToString() : "";
            bg.Criteria += ") as xx ";
            bg.Criteria += (LinkFrom == "3" || linkFrom == "1") ? " where Thn " + blnApprove + " " + DateTime.Now.Year : "";
            bg.Criteria += (LinkFrom == "3" || linkFrom == "1") ? (DateTime.Now.Month < 10) ? DateTime.Now.Month.ToString().PadLeft(2, '0') : DateTime.Now.Month.ToString() : "";
            bg.Criteria += " order by xx.Bulan Desc";

            arrData = bg.Retrieve();
            lstBulan.DataSource = arrData;
            lstBulan.DataBind();
        }

        protected void lstBulan_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string UserIDUser = ((Users)Session["Users"]).ID.ToString();
            string[] UserIDEndApv = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserID", "BudgetATK").Split(',');

            DeptFacade dp = new DeptFacade();
            string DeptYngDiApprove = dp.RetrieveDeptForApproval(((Users)Session["Users"]).ID);
            linkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            ArrayList arrData = new ArrayList();
            BudgetingFacade bg = new BudgetingFacade();
            Budget d = (Budget)e.Item.DataItem;
            Repeater lstDept = (Repeater)e.Item.FindControl("lstDept");
            string[] viewAllApv0 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllApv0", "BudgetATK").Split(',');
            string[] viewAllApv1 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllApv1", "BudgetATK").Split(',');

            //Tambahan Feb 2018
            string[] UserIDLog = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserIDLog", "BudgetATK").Split(',');

            //string blnApprove = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BulanApprove", "BudgetATK");
            //blnApprove = (blnApprove == string.Empty) ? ">=" : blnApprove; --- :Before 1
            //blnApprove = (blnApprove == string.Empty) ? ">" : blnApprove; // After : Beny

            if (UserIDLog.Contains(UserIDUser))
            {
                string blnApprove2 = ">";
                Session["blnApprove"] = blnApprove2;
            }
            else
            {
                string blnApprove2 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BulanApprove", "BudgetATK");
                blnApprove2 = (blnApprove2 == string.Empty) ? ">" : blnApprove2;
                Session["blnApprove"] = blnApprove2;
            }

            string blnApprove = Session["blnApprove"].ToString();

            string DeptID = ((Users)Session["Users"]).DeptID.ToString(); // Beny

            bg.Pilihan = "DeptBudget";
            bg.LinkFrom = linkFrom;
            bg.Field = "SELECT DISTINCT DeptID,(Select DeptName from Dept Where ID=DeptID)DeptName";
            bg.Field += (linkFrom == "3") ? " From ( " : " From (";

            if (linkFrom == "2")
            {
                bg.Criteria = (ddlDept.SelectedIndex == 0 && viewAllApv0.Contains(((Users)Session["Users"]).DeptID.ToString())) ? "" : " and DeptID=" + ddlDept.SelectedValue.ToString();
            }
            else if (linkFrom == "3")
            {

                if (UserIDEndApv.Contains(UserIDUser))
                {
                    bg.Criteria = "and (bad.Approval=1 or (bad.approval=0 and bg.DeptiD in (6,10)))";
                }
                else
                {
                    bg.Criteria = (viewAllApv0.Contains(((Users)Session["Users"]).DeptID.ToString())) ? "" :
                        //" and DeptID in(" + DeptYngDiApprove + ") and bad.Approval <= 1";
                        " and DeptID in(" + ((Users)Session["Users"]).DeptID + ") and bad.Approval <= 1";
                }
            }

            bg.Criteria += (linkFrom == "2") ? " and Tahun=" + ddlTahun.SelectedValue.ToString() : "";
            bg.Criteria += (linkFrom == "2") ? " and Bulan=" + ddlBulan.SelectedValue.ToString() : "";


            if (UserIDEndApv.Contains(UserIDUser))
            {
                bg.Criteria += ") as xx ";
            }
            else if (UserIDLog.Contains(UserIDUser))
            {
                bg.Criteria = "and (bad.Approval>=0)) as xx";
            }
            else
            {
                if (linkFrom == "3")
                {
                    bg.Criteria += " and bg.DeptiD=" + ((Users)Session["Users"]).DeptID + ") as xx ";
                }
                else
                {
                    bg.Criteria += " and bg.DeptiD=" + ddlDept.SelectedValue + ") as xx ";
                }
            }


            bg.Criteria += (linkFrom == "3" || linkFrom == "1") ? " where Thn " + blnApprove + " " + DateTime.Now.Year : "";
            bg.Criteria += (linkFrom == "3" || linkFrom == "1") ? (DateTime.Now.Month < 10) ? DateTime.Now.Month.ToString().PadLeft(2, '0') : DateTime.Now.Month.ToString() : "";
            arrData = bg.Retrieve();
            lstDept.DataSource = arrData;
            lstDept.DataBind();
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            int apvusr = ((Users)Session["Users"]).Apv;
            //string BulanAprove = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BulanApprove", "BudgetATK");
            //BulanAprove = (BulanAprove == string.Empty) ? ">" : BulanAprove;
            string BulanAprove = "=";
            string[] ViewAllApv0 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllApv0", "BudgetATK").Split(',');
            string[] ViewAllApv1 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllApv1", "BudgetATK").Split(',');
            string[] EndAppDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EndAppBudget", "BudgetATK").Split(',');
            BudgetingFacade bg = new BudgetingFacade();
            Repeater lstDept = (Repeater)e.Item.FindControl("lstATK");
            Repeater lstBln = (Repeater)e.Item.Parent.Parent.FindControl("lstBulan");
            CheckBox chk = (CheckBox)e.Item.FindControl("chkAll");
            var bln = ((RepeaterItem)e.Item.Parent.Parent).DataItem as Budget;
            Budget bdg = (Budget)e.Item.DataItem;
            bool isDept = (EndAppDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;



            chk.Visible = (apvusr <= 3 && bdg.Approval == 0 && linkFrom == "3") ? true : false; // Beny
            chk.Checked = (apvusr <= 3 && bdg.Approval == 0 && linkFrom == "3") ? true : false; // Beny

            chk.Visible = (apvusr == 2 && bdg.Approval == 0 && linkFrom == "3") ? true : false;
            chk.Visible = (apvusr == 2 && bdg.Approval == 0 && linkFrom == "3" && isDept == true) ? true : chk.Visible;
            chk.Visible = (bdg.Approval == 0 && linkFrom == "3" && isDept == false) ? true : chk.Visible;
            chk.Visible = (apvusr == 2 && bdg.Approval == 0 && linkFrom == "3" && isDept == false) ? true : chk.Visible;
            chk.Checked = (bdg.Approval == 0 && apvusr == 2) ? true : false;
            chk.Checked = (bdg.Approval == 0 && apvusr > 1) ? true : chk.Checked;


            bg.Pilihan = "DetailBudget";
            bg.Field = " select * from (";
            bg.Criteria = " and DeptID=" + bdg.DeptID.ToString();
            bg.Criteria += (linkFrom == "3") ? " and Bulan " + BulanAprove + bln.Bulan.ToString() : " and Bulan=" + bln.Bulan.ToString();
            bg.Criteria += " and Tahun=" + bln.Tahun.ToString();
            bg.Criteria += ") as xx ";

            switch (((Users)Session["Users"]).DeptID)
            {
                case 6:
                case 10:
                    bg.Criteria += " where xx.ItemID in((Select ID from Inventory where Head=1))";
                    break;
                case 7:
                    bg.Criteria += (apvusr > 1 && isUserIsHead(((Users)Session["Users"]).ID) == true) ? " where xx.ItemID in((Select ID from Inventory where Head in (1,2)))" : "";
                    break;
                default:
                    bg.Criteria += "";
                    break;
            }
            bg.Criteria += " order by xx.Itemname ";
            arrData = bg.Retrieve();
            chk.Visible = (arrData.Count > 0) ? true : false;
            lstDept.DataSource = arrData;
            lstDept.DataBind();
            btnApproval.Enabled = (linkFrom == "3" && ((Users)Session["Users"]).Apv >= 1) ? true : false;
            btnApproval.Enabled = (linkFrom == "3" && ((Users)Session["Users"]).Apv > 1 && isDept == true) ? true : btnApproval.Enabled;
        }
        protected void lstATK_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string UserIDUser = ((Users)Session["Users"]).ID.ToString();
            string UserLogBB = "10";
            string UserLogBJ = "6";
            string[] UserIDEndApv = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserID", "BudgetATK").Split(',');

            string[] EndAppDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EndAppBudget", "BudgetATK").Split(',');
            Budget bg = (Budget)e.Item.DataItem;
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            CheckBox chkP = (CheckBox)e.Item.Parent.Parent.FindControl("chkAll");
            TextBox txtApv = (TextBox)e.Item.FindControl("txtApvQty");
            TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");
            Label lblQ = (Label)e.Item.FindControl("lblQty");
            Label lblA = (Label)e.Item.FindControl("lblApv");
            Image edit = (Image)e.Item.FindControl("edit");
            int apvusr = ((Users)Session["Users"]).Apv;
            int pos = Array.IndexOf(EndAppDept, ((Users)Session["Users"]).DeptID.ToString());
            bool isDept = (pos > -1) ? true : false;

            chkP.Visible = (apvusr == 2 && bg.Approval == 1 && linkFrom == "3") ? true : false;
            chkP.Visible = (apvusr == 2 && bg.Approval == 0 && linkFrom == "3" && isDept == true) ? true : chkP.Visible;
            txtApv.Text = (bg.Approval > 0) ? txtApv.Text : "";
            edit.Visible = (apvusr == 0 && bg.Approval == 0 && bg.DeptID == ((Users)Session["Users"]).DeptID) ? true : false;

            if (UserIDEndApv.Contains(UserIDUser)) // Mgr Log : End Approval Plus Approve LogBB dan LogBJ
            {
                chk.Visible = (UserIDEndApv.Contains(UserIDUser) && bg.Approval == 1
                              || UserIDEndApv.Contains(UserIDUser) && bg.DeptID == 6 && bg.Approval == 0
                              || UserIDEndApv.Contains(UserIDUser) && bg.DeptID == 10 && bg.Approval == 0);

                chk.Checked = (UserIDEndApv.Contains(UserIDUser) && bg.Approval == 1
                              || UserIDEndApv.Contains(UserIDUser) && bg.DeptID == 6 && bg.Approval == 0
                              || UserIDEndApv.Contains(UserIDUser) && bg.DeptID == 10 && bg.Approval == 0);
            }
            else if (apvusr > 2) // Like Mgr IT
            {
                chk.Visible = (apvusr <= 3 && bg.Approval == 0 && linkFrom == "3") ? true : false;
                chk.Checked = (apvusr <= 3 && bg.Approval == 0 && linkFrom == "3") ? true : false;
            }
            else if (apvusr == 2) // Mgr Dept
            {
                chk.Checked = (bg.Approval == 0 && apvusr == 2) ? true : false;
                chk.Visible = (apvusr == 2 && bg.Approval == 0 && linkFrom == "3") ? true : false;
            }

            lblA.Text = txtApv.Text;
            lblQ.Text = txtQty.Text;
        }
        protected void lstSPP_DataBind(object sender, RepeaterItemEventArgs e)
        {
            #region info pemilik budget
            TextBox txt = (TextBox)e.Item.FindControl("txtQty");
            Label lbl = (Label)e.Item.FindControl("txtStk");
            string Infog = string.Empty;
            string Info = string.Empty;
            Budget bd = (Budget)e.Item.DataItem;
            ArrayList arrData = new ArrayList();
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "DetailBudget";
            bg.Criteria = " and ItemID=" + bd.ItemID;
            bg.Criteria += " and Bulan=" + ddlBulan1.SelectedValue;
            bg.Criteria += " and Tahun=" + ddlTahun1.SelectedValue;
            arrData = bg.Retrieve();
            Info = "List Permintaan Material\n";
            Info += "".PadRight(30, '_');
            Info += "\n";
            Info += "No.".PadRight(4);
            Info += "| Dept".PadRight(25);
            Info += "| Qty\n";
            Info += "".PadRight(30, '_');
            Info += "\n";
            Infog = "Budget vs SPB\n";
            Infog += "".PadRight(30, '_');
            Infog += "\nDept ".PadRight(15);
            Infog += "Budget ".PadRight(15);
            Infog += "SPB \n";
            Infog += "".PadRight(30, '_');
            Infog += "\n";
            int n = 0; decimal total = 0; decimal totalspb = 0;
            foreach (Budget b in arrData)
            {
                Budget bdg = new Budget();
                BudgetingFacade de = new BudgetingFacade();
                de.Pilihan = "DeptBudget";
                de.Field = "SELECT DISTINCT DeptID,(Select DeptCode from Dept Where ID=DeptID)DeptName";
                de.Field += " from(";
                de.Criteria = ") as xx where DeptID=" + b.DeptID;
                bdg = de.Retrieve(true);
                n = n + 1;
                Info += n.ToString().PadRight(10);
                Info += "| " + bdg.DeptName.PadRight(20 - (bdg.DeptName.Length));
                Info += "| " + b.Quantity.ToString();
                Info += "\n";
                BudgetingFacade bgx = new BudgetingFacade();
                Budget bud = new Budget();
                int bulan = (int.Parse(ddlBulan1.SelectedValue) == 1) ? 12 : int.Parse(ddlBulan1.SelectedValue) - 1;
                int tahun = (bulan == 12) ? (int.Parse(ddlTahun1.SelectedValue.ToString()) - 1) : int.Parse(ddlTahun1.SelectedValue);
                bgx.Pilihan = "DetailBudget";
                bgx.Criteria = " and ItemID=" + bd.ItemID;
                bgx.Criteria += " and Bulan=" + bulan.ToString();
                bgx.Criteria += " and Tahun=" + tahun.ToString();
                bgx.Criteria += " and DeptID=" + bdg.DeptID;
                bud = bgx.Retrieve(true);
                decimal pkd = ((PakaiDetail)new PakaiDetailFacade().GetDateilSPB(bud.PakaiDetailID)).Quantity;
                Infog += bdg.DeptName.PadRight(20 - (bdg.DeptName.Length));
                Infog += bud.AppvQty.ToString().PadRight(25 - (bud.AppvQty.ToString().Length));
                Infog += pkd.ToString();
                Infog += "\n";
                total += bud.AppvQty;
                totalspb += pkd;
            }
            Info += "".PadRight(30, '_');
            Infog += "".PadRight(30, '_');
            Infog += "\nTotal ".PadRight(17) + total.ToString().PadRight(27 - (total.ToString().Length));
            Infog += totalspb.ToString();
            txt.ToolTip = Info;
            lbl.ToolTip = Infog;
            #endregion
        }
        protected void lsttoSPP_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            #region info pemilik budget
            TextBox txt = (TextBox)e.Item.FindControl("txtQty");
            Label lbl = (Label)e.Item.FindControl("txtStk");
            string Infog = string.Empty;
            string Info = string.Empty;
            string ItemID = e.CommandArgument.ToString();
            Budget bd = (Budget)e.Item.DataItem;
            ArrayList arrData = new ArrayList();
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "DetailBudget";
            bg.Criteria = " and ItemID=" + ItemID;
            bg.Criteria += " and Bulan=" + ddlBulan1.SelectedValue;
            bg.Criteria += " and Tahun=" + ddlTahun1.SelectedValue;
            arrData = bg.Retrieve();
            Info = "List Permintaan Material\\n";
            Info += "".PadRight(30, '_');
            Info += "\\n";
            Info += "No.".PadRight(8);
            Info += "| Dept".PadRight(15);
            Info += "| Qty\\n";
            Info += "".PadRight(30, '_');
            Info += "\\n";
            Infog = "Budget vs SPB\\n";
            Infog += "".PadRight(30, '_');
            Infog += "\\nDept ".PadRight(15);
            Infog += "Budget ".PadRight(15);
            Infog += "| SPB \\n";
            Infog += "".PadRight(30, '_');
            Infog += "\\n";
            int n = 0; decimal total = 0; decimal totalspb = 0;
            foreach (Budget b in arrData)
            {
                Budget bdg = new Budget();
                BudgetingFacade de = new BudgetingFacade();
                de.Pilihan = "DeptBudget";
                de.Field = "SELECT DISTINCT DeptID,(Select DeptCode from Dept Where ID=DeptID)DeptName";
                de.Field += " from(";
                de.Criteria = ") as xx where DeptID=" + b.DeptID;
                bdg = de.Retrieve(true);
                n = n + 1;
                Info += n.ToString().PadRight(5);
                Info += "| " + bdg.DeptName.PadRight(20 - (bdg.DeptName.Length));
                Info += "| " + b.Quantity.ToString();
                Info += "\\n";
                BudgetingFacade bgx = new BudgetingFacade();
                Budget bud = new Budget();
                int bulan = (int.Parse(ddlBulan1.SelectedValue) == 1) ? 12 : int.Parse(ddlBulan1.SelectedValue) - 1;
                int tahun = (bulan == 12) ? (int.Parse(ddlTahun1.SelectedValue.ToString()) - 1) : int.Parse(ddlTahun1.SelectedValue);
                bgx.Pilihan = "DetailBudget";
                bgx.Criteria = " and ItemID=" + ItemID;
                bgx.Criteria += " and Bulan=" + bulan.ToString();
                bgx.Criteria += " and Tahun=" + tahun.ToString();
                bgx.Criteria += " and DeptID=" + bdg.DeptID;
                bud = bgx.Retrieve(true);
                decimal pkd = ((PakaiDetail)new PakaiDetailFacade().GetDateilSPB(bud.PakaiDetailID)).Quantity;
                Infog += bdg.DeptName.PadRight(20 - (bdg.DeptName.Length));
                Infog += bud.AppvQty.ToString().PadRight(25 - (bud.AppvQty.ToString().Length));
                Infog += pkd.ToString();
                Infog += "\\n";
                total += bud.AppvQty;
                totalspb += pkd;
            }
            Info += "".PadRight(30, '_');
            Infog += "".PadRight(30, '_');
            Infog += "\\nTotal ".PadRight(17) + total.ToString().PadRight(27 - (total.ToString().Length));
            Infog += totalspb.ToString();
            #endregion
            DisplayAJAXMessage(this, Info + "\\n\\n List " + Infog);
        }
        protected void lstATK_Command(object sender, RepeaterCommandEventArgs e)
        {
        }
        protected void chk_Checked(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            int xx = 0;
            for (int z = 0; z < lstBulan.Items.Count; z++)
            {
                Repeater rpt = (Repeater)lstBulan.Items[z].FindControl("lstDept");
                int x = 0;
                for (int n = 0; n < rpt.Items.Count; n++)
                {
                    CheckBox chA = (CheckBox)rpt.Items[n].FindControl("chkAll");
                    Repeater lstAtk = (Repeater)rpt.Items[n].FindControl("lstATK");

                    for (int i = 0; i < lstAtk.Items.Count; i++)
                    {
                        CheckBox ch = (CheckBox)lstAtk.Items[i].FindControl("chk");
                        if (ch.Checked == true) { xx = xx + 1; }// else { xx = xx - 1; }
                        if (ch.Checked == true) { x = x + 1; } //else { x = x - 1; }
                    }
                    chA.Checked = (x > 0) ? true : chk.Checked;
                }
            }
            btnApproval.Enabled = (xx > 0) ? true : false;
        }
        protected void ChkAll_Checked(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            int idx = int.Parse(chk.ToolTip.ToString());
            Repeater rpt = (Repeater)lstBulan.Items[idx].FindControl("lstDept");
            for (int n = 0; n < rpt.Items.Count; n++)
            {
                //CheckBox cb = (CheckBox)rpt.Items[n].FindControl("chkAll");
                Repeater lstAtk = (Repeater)rpt.Items[n].FindControl("lstATK");
                for (int i = 0; i < lstAtk.Items.Count; i++)
                {
                    CheckBox cxd = (CheckBox)lstAtk.Items[i].FindControl("chk");
                    //cxd.Checked = false;
                    cxd.Checked = (chk.Checked == true) ? true : false;

                }
            }
            btnApproval.Enabled = (chk.Checked == true) ? true : false; ;
        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            int DeptID = ((Users)Session["Users"]).DeptID;
            for (int i = 0; i < lstBulan.Items.Count; i++)
            {
                Repeater rpt = (Repeater)lstBulan.Items[i].FindControl("lstDept");
                for (int n = 0; n < rpt.Items.Count; n++)
                {
                    Repeater dpt = (Repeater)rpt.Items[n].FindControl("LstATK");
                    for (int z = 0; z < dpt.Items.Count; z++)
                    {

                        TextBox txtCode = (TextBox)dpt.Items[z].FindControl("ItemCode");
                        CheckBox chk = (CheckBox)dpt.Items[z].FindControl("chk");
                        TextBox txt = (TextBox)dpt.Items[z].FindControl("txtApvQty");
                        TextBox qty = (TextBox)dpt.Items[z].FindControl("txtQty");
                        TextBox dtlID = (TextBox)dpt.Items[z].FindControl("txtDetailID");

                        BudgetingFacade bgF = new BudgetingFacade();
                        Budget bg1 = new Budget();
                        string Kode = txtCode.Text.ToString();
                        string cek = bgF.cekkode(Kode);

                        if (chk.Checked == true)
                        {

                            Budget bdg = new Budget();
                            bdg.BudgetID = int.Parse(chk.ToolTip.ToString());
                            switch (DeptID)
                            {
                                case 10:
                                    bdg.Approval = ((Users)Session["Users"]).Apv;
                                    break;
                                case 7:
                                    if (cek == "1")
                                    {
                                        //bdg.Approval = (isUserIsHead(((Users)Session["Users"]).ID) == true) ? ((Users)Session["Users"]).Apv : 1; 
                                        bdg.Approval = 1;
                                    }
                                    else if (cek == "2")
                                    {
                                        //bdg.Approval = (isUserIsHead(((Users)Session["Users"]).ID) == true) ? ((Users)Session["Users"]).Apv : 2;
                                        bdg.Approval = 2;
                                    }

                                    break;
                                default:
                                    bdg.Approval = 1;
                                    break;
                            }
                            bdg.ApprovalBy = ((Users)Session["Users"]).UserID.ToString();
                            bdg.AppvQty = (txt.Text == string.Empty) ? decimal.Parse(qty.Text) : decimal.Parse(txt.Text);
                            bdg.ID = int.Parse(dtlID.Text);
                            //arrData.Add(bdg);
                            int rst = new BudgetingFacade().Approval(bdg);

                        }

                    }
                    if ((n + 1) == rpt.Items.Count) { Response.Redirect("ListBudgetATK.aspx?fr=3"); }
                }
            }
        }
        protected void txtApvQty_Change(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            for (int i = 0; i < lstBulan.Items.Count; i++)
            {
                Repeater rpt = (Repeater)lstBulan.Items[i].FindControl("lstDept");
                for (int n = 0; n < rpt.Items.Count; n++)
                {
                    Repeater dpt = (Repeater)rpt.Items[n].FindControl("LstATK");
                    for (int z = 0; z < dpt.Items.Count; z++)
                    {
                        decimal QtyMaster = 0;
                        decimal QtyBudget = 0;
                        CheckBox chk = (CheckBox)dpt.Items[z].FindControl("chk");
                        TextBox txt = (TextBox)dpt.Items[z].FindControl("txtApvQty");
                        TextBox qty = (TextBox)dpt.Items[z].FindControl("txtQty");
                        TextBox dtlID = (TextBox)dpt.Items[z].FindControl("txtDetailID");
                        Label DptID = (Label)dpt.Items[z].FindControl("txtDept");
                        Label thn = (Label)dpt.Items[z].FindControl("txtThn");
                        if (chk.Checked == true)
                        {
                            QtyMaster = this.CheckMasterBudget(txt.ToolTip.Trim(), DptID.Text, thn.Text);
                            QtyBudget = (txt.Text == string.Empty) ? 0 : decimal.Parse(txt.Text);
                            if (QtyBudget > QtyMaster)
                            {
                                DisplayAJAXMessage(this, "Quantity tidak boleh melebihi Qty Master Budget  untuk item tersebut\\nAtau Master Budget Belum di tentukan");
                                txt.Text = QtyMaster.ToString();
                                return;
                            }
                        }
                    }
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormBudgetingATK.aspx");
        }
        protected void btnToSPP_Click(object sender, EventArgs e)
        {
            Session["SPP"] = null;
            ArrayList arrData = new ArrayList();
            for (int i = 0; i < lsttoSPP.Items.Count; i++)
            {
                Budget bg = new Budget();
                TextBox itemCode = (TextBox)lsttoSPP.Items[i].FindControl("txtItemCode");
                TextBox ItemName = (TextBox)lsttoSPP.Items[i].FindControl("txtItemName");
                TextBox UomCode = (TextBox)lsttoSPP.Items[i].FindControl("txtUomCode");
                TextBox ItemID = (TextBox)lsttoSPP.Items[i].FindControl("txtItemID");
                TextBox Qty = (TextBox)lsttoSPP.Items[i].FindControl("txtQty");
                TextBox UomID = (TextBox)lsttoSPP.Items[i].FindControl("txtUomID");
                if (Qty.Text.Trim() != "")
                {
                    if (decimal.Parse(Qty.Text) > CheckMasterBudget(ItemID.Text, true))
                    {
                        DisplayAJAXMessage(this, "Qty " + ItemName.Text + " Melebihi Master Budget,\\n Please Check Lagi");
                        return;
                    }
                    if (decimal.Parse(Qty.Text) > 0)
                    {
                        bg.ItemID = int.Parse(ItemID.Text);
                        bg.Quantity = decimal.Parse(Qty.Text);
                        bg.UomID = int.Parse(UomID.Text);
                        bg.ItemCode = itemCode.Text;
                        bg.ItemName = ItemName.Text;
                        bg.UomCode = UomCode.Text;


                        arrData.Add(bg);
                    }
                }
            }
            Session["SPP"] = arrData;
            Response.Redirect("TransaksiSPP3.aspx?fr=budget&bln=" + ddlBulan1.SelectedValue.ToString() + "&thn=" + ddlTahun1.SelectedValue.ToString());
        }
        private decimal CheckMasterBudget(string itemID)
        {
            decimal master = 0;
            BudgetingFacade bg = new BudgetingFacade();
            Budget bu = new Budget();
            bg.Pilihan = "MasterBudget";
            bg.Criteria = " and Tahun=" + ddlTahun.SelectedValue;
            bg.Criteria += " and DeptID=" + ddlDept.SelectedValue;
            bg.Criteria += " and ItemID=" + itemID;
            bu = bg.Retrieve(true);
            master = bu.Quantity;
            return master;
        }
        private decimal CheckMasterBudget(string itemID, string DeptID, string Tahun)
        {
            /**
             * Check Master if Change qty on Logistic Manager Approval
             */
            decimal master = 0;
            BudgetingFacade bg = new BudgetingFacade();
            Budget bu = new Budget();
            bg.Pilihan = "MasterBudget";
            bg.Criteria = " and Tahun=" + Tahun;
            bg.Criteria += " and DeptID=" + DeptID;
            bg.Criteria += " and ItemID=" + itemID;
            bu = bg.Retrieve(true);
            master = bu.Quantity;
            return master;
        }
        private decimal CheckMasterBudget(string itemID, bool forSPP)
        {
            /**
             * Check Master where change qty on cretaed spp
             */
            decimal master = 0;
            BudgetingFacade bg = new BudgetingFacade();
            Budget bu = new Budget();
            bg.Pilihan = "MasterBudgetTotal";
            bg.Criteria = " and Tahun=" + ddlTahun1.SelectedValue;
            //bg.Criteria += " and DeptID=" + ddlDept.SelectedValue;
            bg.Criteria += " and ItemID=" + itemID;
            bu = bg.Retrieve(true);
            master = bu.Quantity;
            return master;
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
        private bool isUserIsHead(int userid)
        {
            DeptFacade df = new DeptFacade();
            ArrayList arrData = new ArrayList();
            arrData = df.GetDeptFromHead(userid);
            return (arrData.Count > 0) ? true : false;
        }
        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }
    }
}