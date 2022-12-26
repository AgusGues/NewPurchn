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
    public partial class ListTabelConversi : System.Web.UI.Page
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
                TabelConversiFacade tcFacade = new TabelConversiFacade();
                ArrayList arrTC = new ArrayList();

                arrTC = tcFacade.RetrieveOpenStatusByNo(txtSearch.Text);
                if (tcFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrTC;
                    GridView1.DataBind();
                }
            }
        }

        private void LoadData(string strApprove)
        {
            TabelConversiFacade tcFacade = new TabelConversiFacade();
            ArrayList arrTC = new ArrayList();

            arrTC = tcFacade.RetrieveOpenStatus();

            if (tcFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrTC;
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                if (Request.QueryString["approve"].ToString() == "XP")
                    Response.Redirect("FormProcessConvert.aspx?ConversiNo=" + row.Cells[0].Text);
                else
                    Response.Redirect("FormTableConversi.aspx?ConversiNo=" + row.Cells[0].Text);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                e.Row.Cells[2].Text = Convert.ToDecimal(e.Row.Cells[2].Text).ToString("N2");
                e.Row.Cells[4].Text = Convert.ToDecimal(e.Row.Cells[4].Text).ToString("N2");
                e.Row.Cells[6].Text = Convert.ToDecimal(e.Row.Cells[6].Text).ToString("N2");

            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["approve"].ToString() == "XP")
                Response.Redirect("FormProcessConvert.aspx");
            else
                Response.Redirect("FormTableConversi.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}