using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace GRCweb1.Modul.Purchasing
{
    public partial class KasbonListRealisasi : System.Web.UI.Page
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
                KasbonFacade kasbonFacade = new KasbonFacade();
                ArrayList arrKasbonDetail = new ArrayList();

                arrKasbonDetail = kasbonFacade.RetrieveAllK(0, "k.KasbonNo like ", txtSearch.Text);

                if (kasbonFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrKasbonDetail;
                    GridView1.DataBind();
                }
            }
        }
        private void LoadData(string strGroupID)
        {
            KasbonFacade kasbonFacade = new KasbonFacade();
            ArrayList arrKasbonDetail = new ArrayList();
            arrKasbonDetail = kasbonFacade.RetrieveAllR(int.Parse(strGroupID), "and k.KasbonNo like ", txtSearch.Text);

            if (kasbonFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrKasbonDetail;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("KasbonRealisasi.aspx?NoPengajuan=" + row.Cells[0].Text);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[2].Text = DateTime.Parse(e.Row.Cells[2].Text).ToString("dd-MMM-yyyy");

                if (e.Row.Cells[8].Text == "2")
                {
                    e.Row.Cells[8].Text = "Open";
                }
                if (e.Row.Cells[8].Text == "3")
                {
                    e.Row.Cells[8].Text = "Head";
                }
                if (e.Row.Cells[8].Text == "4")
                {
                    e.Row.Cells[8].Text = "Finance";
                }
                if (e.Row.Cells[9].Text == "-1")
                {
                    e.Row.Cells[9].Text = "Cancel";
                }
            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("KasbonRealisasi.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
    }
}