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
    public partial class ListPenawaran : System.Web.UI.Page
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
                POPurchnFacade pOPurchnFacade = new POPurchnFacade();
                ArrayList arrPOPurchnDetail = new ArrayList();

                arrPOPurchnDetail = pOPurchnFacade.RetrieveAllPO(0, "and A.NoPO like ", txtSearch.Text);

                if (pOPurchnFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrPOPurchnDetail;
                    GridView1.DataBind();
                }
            }
        }

        private void LoadData()
        {
            TawarFacade poPurchnFacade = new TawarFacade();
            ArrayList arrPOPurchnDetail = new ArrayList();

            arrPOPurchnDetail = poPurchnFacade.RetrieveAllPO();

            if (poPurchnFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrPOPurchnDetail;
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("FormPenawaran.aspx?PONo=" + row.Cells[0].Text);

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("FormPenawaran.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //LoadData(Request.QueryString["approve"].ToString());
            LoadData();
        }
    }
}