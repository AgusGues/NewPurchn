using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class ListUPDJobDesk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            //if (txtSearch.Text == string.Empty)
            //{
            //    LoadData(Request.QueryString["approve"].ToString());
            //}
            //else
            //{
            //    Users users = (Users)Session["Users"];

            //    JobDeskFacade jobdeskFacade = new JobDeskFacade();
            //    ArrayList arrJobDeskDetail = new ArrayList();

            //    UsersHeadFacade usersHeadFacade = new UsersHeadFacade();
            //    UsersHead usersHead = usersHeadFacade.RetrieveByUserID(users.ID.ToString());
            //    if (usersHeadFacade.Error == string.Empty)
            //    {
            //        //arrJobDeskDetail = jobdeskFacade.RetrieveByAllWithStatus(int.Parse(Request.QueryString["approve"]), "A.NoSPP", txtSearch.Text);
            //        arrJobDeskDetail = jobdeskFacade.RetrieveByAllWithStatus((users.Apv == 0) ? usersHead.HeadID : users.ID, "A.NoSPP", txtSearch.Text);

            //        if (jobdeskFacade.Error == string.Empty)
            //        {
            //            GridView1.DataSource = arrJobDeskDetail;
            //            GridView1.DataBind();
            //        }

            //    }
            //    else
            //    {
            //        DisplayAJAXMessage(this, "Head-ID tidak ada ..!");
            //        return;
            //    }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadData()
        {
            JobDeskFacade jobdeskFacade = new JobDeskFacade();
            ArrayList arrJobDeskDetail = new ArrayList();

            Users usr = new Users();
            usr.Apv = ((Users)Session["Users"]).Apv;
            usr.ID = ((Users)Session["Users"]).ID;
            jobdeskFacade.Criteria = " DeptID IN (SELECT DeptID FROM Users WHERE UserName='" + ((Users)Session["Users"]).UserName + "') ";
            arrJobDeskDetail = jobdeskFacade.RetrieveByAll();

            if (jobdeskFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrJobDeskDetail;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("JobDeskInput.aspx?ID=" + row.Cells[0].Text);

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "0")
                {
                    e.Row.Cells[4].Text = "Open";
                }
                if (e.Row.Cells[4].Text == "1")
                {
                    e.Row.Cells[4].Text = "Approved HRD";
                }
                if (e.Row.Cells[4].Text == "2")
                {
                    e.Row.Cells[4].Text = "Approved Manager";
                }
                if (e.Row.Cells[4].Text == "3")
                {
                    e.Row.Cells[4].Text = "Approved PM / Corp Manager";
                }
                if (e.Row.Cells[4].Text == "4")
                {
                    e.Row.Cells[4].Text = "Approved Corp HRD Manager";
                }
                if (e.Row.Cells[4].Text == "5")
                {
                    e.Row.Cells[4].Text = "Approved Iso";
                }
                if (e.Row.Cells[4].Text == "6")
                {
                    e.Row.Cells[4].Text = "Distributed By Iso";
                }
                if (e.Row.Cells[4].Text == "7")
                {
                    e.Row.Cells[4].Text = "Aktivasi By Iso";
                }


                if (e.Row.Cells[2].Text == "2")
                {
                    e.Row.Cells[2].Text = "Boardmill";
                }
                if (e.Row.Cells[2].Text == "3")
                {
                    e.Row.Cells[2].Text = "Finishing";
                }
                if (e.Row.Cells[2].Text == "6")
                {
                    e.Row.Cells[2].Text = "Logistik Produk Jadi";
                }
                if (e.Row.Cells[2].Text == "7")
                {
                    e.Row.Cells[2].Text = "Hrd & GA";
                }
                if (e.Row.Cells[2].Text == "9")
                {
                    e.Row.Cells[2].Text = "Quality Assurance";
                }
                if (e.Row.Cells[2].Text == "10")
                {
                    e.Row.Cells[2].Text = "Logistik Bahan Baku";
                }
                if (e.Row.Cells[2].Text == "11")
                {
                    e.Row.Cells[2].Text = "Ppic";
                }
                if (e.Row.Cells[2].Text == "12")
                {
                    e.Row.Cells[2].Text = "Finance";
                }
                if (e.Row.Cells[2].Text == "13")
                {
                    e.Row.Cells[2].Text = "Marketing";
                }
                if (e.Row.Cells[2].Text == "14")
                {
                    e.Row.Cells[2].Text = "IT";
                }
                if (e.Row.Cells[2].Text == "15")
                {
                    e.Row.Cells[2].Text = "Purchasing";
                }
                if (e.Row.Cells[2].Text == "19")
                {
                    e.Row.Cells[2].Text = "Maintenance";
                }
                if (e.Row.Cells[2].Text == "22")
                {
                    e.Row.Cells[2].Text = "Project Sipil";
                }
                if (e.Row.Cells[2].Text == "23")
                {
                    e.Row.Cells[2].Text = "Iso";
                }
                if (e.Row.Cells[2].Text == "24")
                {
                    e.Row.Cells[2].Text = "Accounting";
                }
                if (e.Row.Cells[2].Text == "26")
                {
                    e.Row.Cells[2].Text = "Armada";
                }
                if (e.Row.Cells[2].Text == "27")
                {
                    e.Row.Cells[2].Text = "Manager Corporate";
                }
                if (e.Row.Cells[2].Text == "28")
                {
                    e.Row.Cells[2].Text = "Product Development";
                }
                if (e.Row.Cells[2].Text == "30")
                {
                    e.Row.Cells[2].Text = "Research & Development";
                }
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("JobDeskInput.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }
    }
}