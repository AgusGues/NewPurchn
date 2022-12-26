using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ListSPPApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
                //LoadData(Request.QueryString["approve"].ToString());
                LoadData(spp, " order by A.NoSPP ");
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('lstRepeater', 500, 100 , 21 ,false); </script>", false);
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
                LoadData(spp, " order by A.NoSPP ");
            }
            else
            {
                Users users = (Users)Session["Users"];
                //string strHeadID1 = string.Empty; 

                //UsersHeadFacade usersHeadFacade = new UsersHeadFacade();
                //ArrayList arrUsersHead = usersHeadFacade.RetrieveByManagerID(users.ID);
                //if (usersHeadFacade.Error == string.Empty)
                //{
                //    foreach (UsersHead usersHead in arrUsersHead)
                //    {
                //        strHeadID1 = strHeadID1 + users.HeadID + ",";

                //    }
                //}

                //string strHeadID2 = strHeadID1; 

                SPPFacade sppFacade = new SPPFacade();
                ArrayList arrSPPDetail = new ArrayList();
                //arrSPPDetail = sppFacade.RetrieveByAllWithStatus(int.Parse(Request.QueryString["approve"]), "A.NoSPP", txtSearch.Text);
                arrSPPDetail = sppFacade.RetrieveByAllWithStatus(users.ID, "A.NoSPP", txtSearch.Text);

                if (sppFacade.Error == string.Empty)
                {
                    //GridView1.DataSource = arrSPPDetail;
                    //GridView1.DataBind();
                    lstSPP.DataSource = arrSPPDetail;
                    lstSPP.DataBind();
                }
            }
        }

        private void LoadData(string strGroupID, string SortBy)
        {
            Users users = (Users)Session["Users"];

            SPPFacade sppFacade = new SPPFacade();
            ArrayList arrSPPDetail = new ArrayList();
            //for open only & liat level approval
            //users.Apv - 1
            //arrSPPDetail = sppFacade.RetrieveByAll2(int.Parse(strGroupID), users.Apv - 1);

            string strHeadID1 = string.Empty;
            string strHeadID2 = string.Empty;

            UsersHeadFacade usersHeadFacade = new UsersHeadFacade();
            ArrayList arrUsersHead = new ArrayList();
            //ArrayList arrUsersHead = usersHeadFacade.RetrieveByManagerID(users.ID);
            if (users.Apv == 1)
            {
                //UsersHead usersHeadaja = usersHeadFacade.RetrieveByUserID(users.ID.ToString());
                arrUsersHead = usersHeadFacade.RetrieveByHeadID(users.ID.ToString());
                if (usersHeadFacade.Error == string.Empty)
                {
                    foreach (UsersHead usersHead in arrUsersHead)
                    {
                        strHeadID1 = strHeadID1 + usersHead.UserID + ",";
                    }

                }
                // strHeadID2 = strHeadID1.Substring(0, (strHeadID1.Length) - 1) + ") and HeadID=(" + users.ID;

                strHeadID2 = users.ID.ToString();
            }
            else if (users.Apv == 2)
            {
                string Usered = (users.ID == 169) ? "(Select ID from Users where DeptID in(1,7,8,14,15,24))" : "(Select ID from Users where DeptID not in(1,7,8,14,15,24))";
                arrUsersHead = usersHeadFacade.RetrieveByManagerID(users.ID, Usered);
                //kl approval utk head & manager beda

                if (usersHeadFacade.Error == string.Empty)
                {
                    foreach (UsersHead usersHead in arrUsersHead)
                    {
                        strHeadID1 = strHeadID1 + usersHead.HeadID + ",";

                    }
                }

                strHeadID2 = strHeadID1.Substring(0, (strHeadID1.Length) - 1);
            }

            Session["Limit"] = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "SPP").ToString();
            strGroupID = strGroupID.Replace(",", "','");
            strHeadID2 = " and A.NoSPP in('" + strGroupID + "')";
            arrSPPDetail = sppFacade.RetrieveByAll3(strHeadID2, users.Apv - 1, SortBy);

            if (sppFacade.Error == string.Empty)
            {
                //GridView1.DataSource = arrSPPDetail;
                //GridView1.DataBind();
                lstSPP.DataSource = arrSPPDetail;
                lstSPP.DataBind();
            }
            Session["Limit"] = "";
        }
        protected void lstSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            SPP sp = (SPP)e.Item.DataItem;
            string Stat = string.Empty; string Apvr = string.Empty; string nama = string.Empty;
            switch (sp.Status)
            {
                case 0: Stat = "Open"; break;
                case 1: Stat = "Parsial PO"; break;
                case 2: Stat = "Full PO"; break;
            }
            switch (sp.Approval)
            {
                case 0: Apvr = "Open"; nama = UserSPP(sp.UserID); break;
                case 1: Apvr = "Head"; nama = UserSPP(sp.HeadID); break;
                case 2: Apvr = "Manager"; nama = ""; break;
            }

            ((Label)e.Item.FindControl("txtSts")).Text = Stat;
            ((Label)e.Item.FindControl("txtApv")).Text = Apvr + " [" + nama + "]";
        }
        protected void lstSPP_Command(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Pilih")
            {

                Response.Redirect("ApproveSPPNew.aspx?NoSPP=" + e.CommandArgument.ToString());

            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("ApproveSPPNew.aspx?NoSPP=" + row.Cells[0].Text);

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");

                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "Open";
                }
                if (e.Row.Cells[5].Text == "1")
                {
                    e.Row.Cells[5].Text = "Parsial";
                }
                if (e.Row.Cells[5].Text == "2")
                {
                    e.Row.Cells[5].Text = "Full PO";
                }
                // next what status

                if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "Open";
                }
                if (e.Row.Cells[6].Text == "1")
                {
                    e.Row.Cells[6].Text = "Head";
                }
                if (e.Row.Cells[6].Text == "2")
                {
                    e.Row.Cells[6].Text = "Manager";
                }

                //e.Row.Cells[6].Text = ((Status)int.Parse(e.Row.Cells[6].Text)).ToString();
                //6
                //e.Row.Cells[5].Text = ((Status)int.Parse(e.Row.Cells[5].Text)).ToString();
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ApproveSPPNew.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString(), " order by A.NoSPP ");
        }
        private string UserSPP(int UserID)
        {
            UsersFacade usr = new UsersFacade();
            Users user = usr.RetrieveById(UserID);
            return user.UserName;
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by A.NoSPP,ItemName ");
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by A.NoSPP Desc,ItemName ");
        }
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by A.Minta,A.NoSPP ");
        }
        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by A.Minta Desc,A.NoSPP ");
        }
        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by ItemCode,A.NoSPP ");
        }
        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by ItemCode Desc,A.NoSPP ");
        }
        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by ItemName,A.NoSPP ");
        }
        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            string spp = (Request.QueryString["sp"] != null) ? Request.QueryString["sp"].ToString() : "";
            LoadData(spp, " order by ItemName Desc,A.NoSPP ");
        }
    }
}