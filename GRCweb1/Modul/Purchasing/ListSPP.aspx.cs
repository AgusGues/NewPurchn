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
    public partial class ListSPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData(Request.QueryString["approve"].ToString());
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                LoadData(Request.QueryString["approve"].ToString());
            }
            else
            {
                Users users = (Users)Session["Users"];

                SPPFacade sppFacade = new SPPFacade();
                ArrayList arrSPPDetail = new ArrayList();

                UsersHeadFacade usersHeadFacade = new UsersHeadFacade();
                UsersHead usersHead = usersHeadFacade.RetrieveByUserID(users.ID.ToString());
                if (usersHeadFacade.Error == string.Empty)
                {
                    //arrSPPDetail = sppFacade.RetrieveByAllWithStatus(int.Parse(Request.QueryString["approve"]), "A.NoSPP", txtSearch.Text);
                    arrSPPDetail = sppFacade.RetrieveByAllWithStatus((users.Apv == 0) ? usersHead.HeadID : users.ID, "A.NoSPP", txtSearch.Text);

                    if (sppFacade.Error == string.Empty)
                    {
                        GridView1.DataSource = arrSPPDetail;
                        GridView1.DataBind();
                    }

                }
                else
                {
                    DisplayAJAXMessage(this, "Head-ID tidak ada ..!");
                    return;
                }

            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadData(string strGroupID)
        {
            SPPFacade sppFacade = new SPPFacade();
            ArrayList arrSPPDetail = new ArrayList();

            //review dulu baru ubah ke headID
            Users usr = new Users();
            usr.Apv = ((Users)Session["Users"]).Apv;
            usr.ID = ((Users)Session["Users"]).ID;
            arrSPPDetail = sppFacade.RetrieveByAll(int.Parse(strGroupID));

            if (sppFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrSPPDetail;
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("TransaksiSPP3.aspx?NoSPP=" + row.Cells[0].Text);

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
            Response.Redirect("TransaksiSPP3.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
    }
}