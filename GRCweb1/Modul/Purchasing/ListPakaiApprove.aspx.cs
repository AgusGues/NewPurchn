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
    public partial class ListPakaiApprove : System.Web.UI.Page
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
            if (txtSearch.Text == string.Empty)
            {
                LoadData();
            }
            else
            {
                PakaiFacade pakaiFacade = new PakaiFacade();
                ArrayList arrPakai = new ArrayList();
                //deactive on 22/07/2014
                //if (Request.QueryString["approve"].ToString().Substring(1, 1) == "B")
                //    arrPakai = pakaiFacade.BiayaRetrieveOpenStatusCriteria(txtSearch.Text, Request.QueryString["approve"].ToString());
                //if (Request.QueryString["approve"].ToString().Substring(1, 1) == "C")
                //    arrPakai = pakaiFacade.AssetRetrieveOpenStatusCriteria(txtSearch.Text, Request.QueryString["approve"].ToString());
                //else
                arrPakai = pakaiFacade.RetrieveOpenStatusCriteria(txtSearch.Text, Request.QueryString["approve"].ToString());

                if (pakaiFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrPakai;
                    GridView1.DataBind();
                }
            }
        }

        private void LoadData()
        {
            PakaiFacade pakaiFacade = new PakaiFacade();
            ArrayList arrPakai = new ArrayList();
            Users users = (Users)Session["Users"];
            ArrayList arrDept = new DeptFacade().GetDeptFromHead(users.ID);
            int Apv = (users.DeptID == users.DeptID && arrDept.Count == 0) ? 2 : 1;
            arrPakai = pakaiFacade.RetrieveOpenStatusForLogistikList(users.ID, Apv, users.GroupID);
            if (pakaiFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrPakai;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Response.Redirect("ApprovePakaiNew.aspx?PakaiNo=" + row.Cells[0].Text);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                //e.Row.Cells[6].Text = ((Status)int.Parse(e.Row.Cells[6].Text)).ToString();
                //e.Row.Cells[5].Text = ((Status)int.Parse(e.Row.Cells[5].Text)).ToString();
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ApprovePakaiNew.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }
    }
}