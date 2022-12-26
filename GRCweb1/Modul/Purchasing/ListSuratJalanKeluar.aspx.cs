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
    public partial class ListSuratJalanKeluar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadData();
                //TampilkanData(Request.QueryString["NoSJ"].ToString());

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
                Users users = (Users)Session["Users"];
                INVSuratJalanKeluarFacade invsjk = new INVSuratJalanKeluarFacade();
                ArrayList arrinvsjk = new ArrayList();
                arrinvsjk = invsjk.RetrieveByCriteria("A.NoSJ", txtSearch.Text);

                if (invsjk.Error == string.Empty)
                {
                    GridView1.DataSource = arrinvsjk;
                    GridView1.DataBind();
                }
            }
        }

        private void LoadData()
        {
            INVSuratJalanKeluarFacade sjkfd = new INVSuratJalanKeluarFacade();
            ArrayList arrsjk = new ArrayList();


            arrsjk = sjkfd.RetrieveByAll();
            if (sjkfd.Error == string.Empty)
            {
                GridView1.DataSource = arrsjk;
                GridView1.DataBind();
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("SuratJalanKeluar.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("SuratJalanKeluar.aspx?NoSJ=" + row.Cells[1].Text);

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }
    }
}