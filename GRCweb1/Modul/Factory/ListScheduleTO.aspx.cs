using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GRCweb1.Modul.Factory
{
    public partial class ListScheduleTO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                LoadDataViaApi();
        }
        private void LoadDataViaApi()
        {
            Users users = (Users)Session["Users"];

            ScheduleOPFacade scheduleOPFacade = new ScheduleOPFacade();
            ArrayList arrScheduleOP = new ArrayList();

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsScheduleOP = cpdWebService.sjTORetrieveOutStandingTO2(users.UnitKerjaID);
            foreach (DataRow row in dsScheduleOP.Tables[0].Rows)
            {
                ScheduleTO scheOP = new ScheduleTO();
                scheOP.ScheduleNo = row["ScheduleNo"].ToString();
                scheOP.ScheduleDate = DateTime.Parse(row["ScheduleDate"].ToString());
                scheOP.TransferOrderNo = row["TransferOrderNo"].ToString();
                scheOP.TransferOrderDate = DateTime.Parse(row["TransferOrderDate"].ToString());
                scheOP.FromDepo = row["FromDepoName"].ToString();
                scheOP.ToDepo = row["ToDepoName"].ToString();
                scheOP.FromDepoAddress = row["FromDepoAddress"].ToString();
                scheOP.ToDepoAddress = row["ToDepoAddress"].ToString();

                arrScheduleOP.Add(scheOP);
            }

            if (arrScheduleOP.Count > 0)
            {
                GridView1.DataSource = arrScheduleOP;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Response.Redirect("InputSuratJalanToCpd.aspx?ScheduleNo=" + row.Cells[0].Text + "&TONo=" + row.Cells[2].Text + "&Address1=" + row.Cells[6].Text + "&Address2=" + row.Cells[7].Text);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToString("dd-MMM-yyyy");
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("InputSuratJalanToCpd.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadDataViaApi();
        }
    }
}