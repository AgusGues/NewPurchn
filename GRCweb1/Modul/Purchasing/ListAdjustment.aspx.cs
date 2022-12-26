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
    public partial class ListAdjustment : System.Web.UI.Page
    {
        public enum Status
        {
            Open = 0,
            Approval = 1,
            Scheduled = 2,
            Shipment = 3,
            Receive = 4,
            Cancelled = -1
        }

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
                AdjustFacade adjustFacade = new AdjustFacade();
                ArrayList arrReceipt = new ArrayList();

                arrReceipt = adjustFacade.RetrieveOpenStatusByNo(txtSearch.Text);
                if (adjustFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrReceipt;
                    GridView1.DataBind();
                }
            }
        }

        private void LoadData(string strApprove)
        {
            AdjustFacade adjustFacade = new AdjustFacade();
            ArrayList arrAdjust = new ArrayList();

            arrAdjust = adjustFacade.RetrieveOpenStatus();

            if (adjustFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrAdjust;
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("FormAdjustment.aspx?AdjustNo=" + row.Cells[0].Text);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                //e.Row.Cells[5].Text = ((Status)decimal.Parse(e.Row.Cells[5].Text)).ToString("N2");
                e.Row.Cells[5].Text = Convert.ToDecimal(e.Row.Cells[5].Text).ToString("N2");

            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("FormAdjustment.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
    }
}