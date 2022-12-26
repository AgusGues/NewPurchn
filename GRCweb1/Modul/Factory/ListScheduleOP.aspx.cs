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
    public partial class ListScheduleOP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDataViaApi();

            }
        }
        private void LoadDataViaApi()
        {
            Users users = (Users)Session["Users"];

            if (users.TypeUnitKerja == 2)
            {
                ScheduleOPFacade scheduleOPFacade = new ScheduleOPFacade();
                ArrayList arrScheduleOP = new ArrayList();

                WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
                DataSet dsScheduleOP = cpdWebService.sj_RetrieveOutStandingOP2(users.UnitKerjaID);
                foreach (DataRow row in dsScheduleOP.Tables[0].Rows)
                {
                    ScheduleOP scheOP = new ScheduleOP();
                    scheOP.ScheduleNo = row["ScheduleNo"].ToString();
                    scheOP.ScheduleDate = DateTime.Parse(row["ScheduleDate"].ToString());
                    scheOP.OPNo = row["OPNo"].ToString();
                    scheOP.OPDate = DateTime.Parse(row["OPDate"].ToString());
                    scheOP.AlamatLain = row["AlamatLain"].ToString();
                    scheOP.Address = row["Address"].ToString();
                    scheOP.TokoCustName = row["TokoCustName"].ToString();

                    if (row["CustomerType"].ToString() == "1")
                        scheOP.CustomerType = "Toko";
                    else
                        scheOP.CustomerType = "Individual";
                    //scheOP.CustomerType = int.Parse(row["CustomerType"].ToString());

                    arrScheduleOP.Add(scheOP);
                }

                if (arrScheduleOP.Count > 0)
                {
                    GridView1.DataSource = arrScheduleOP;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("InputSuratJalanCPD.aspx?ScheduleNo=" + row.Cells[0].Text + "&OPNo=" + row.Cells[2].Text);
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            ScheduleOPFacade scheduleOPFacade = new ScheduleOPFacade();
            ArrayList arrScheduleOP = new ArrayList();

            if (txtSearch.Text != string.Empty)
            {
                WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
                if (ddlSearch.SelectedIndex == 0)
                {
                    DataSet dsScheduleOP = cpdWebService.sj_RetrieveOutStandingOPByOPNo2(users.UnitKerjaID, txtSearch.Text);
                    foreach (DataRow row in dsScheduleOP.Tables[0].Rows)
                    {
                        ScheduleOP scheOP = new ScheduleOP();
                        scheOP.ScheduleNo = row["ScheduleNo"].ToString();
                        scheOP.ScheduleDate = DateTime.Parse(row["ScheduleDate"].ToString());
                        scheOP.OPNo = row["OPNo"].ToString();
                        scheOP.OPDate = DateTime.Parse(row["OPDate"].ToString());
                        scheOP.AlamatLain = row["AlamatLain"].ToString();
                        if (row["CustomerType"].ToString() == "1")
                            scheOP.CustomerType = "Toko";
                        else
                            scheOP.CustomerType = "Individual";

                        //scheOP.CustomerType = int.Parse(row["CustomerType"].ToString());
                        scheOP.Address = row["Address"].ToString();
                        scheOP.TokoCustName = row["TokoCustName"].ToString();

                        arrScheduleOP.Add(scheOP);
                    }
                }
                else
                {
                    DataSet dsScheduleOP = cpdWebService.sj_RetrieveOutStandingOPByScheduleNo2(users.UnitKerjaID, txtSearch.Text);
                    foreach (DataRow row in dsScheduleOP.Tables[0].Rows)
                    {
                        ScheduleOP scheOP = new ScheduleOP();
                        scheOP.ScheduleNo = row["ScheduleNo"].ToString();
                        scheOP.ScheduleDate = DateTime.Parse(row["ScheduleDate"].ToString());
                        scheOP.OPNo = row["OPNo"].ToString();
                        scheOP.OPDate = DateTime.Parse(row["OPDate"].ToString());
                        scheOP.AlamatLain = row["AlamatLain"].ToString();
                        if (row["CustomerType"].ToString() == "1")
                            scheOP.CustomerType = "Toko";
                        else
                            scheOP.CustomerType = "Individual";
                        //scheOP.CustomerType = int.Parse(row["CustomerType"].ToString());
                        scheOP.Address = row["Address"].ToString();
                        scheOP.TokoCustName = row["TokoCustName"].ToString();

                        arrScheduleOP.Add(scheOP);
                    }
                }
                if (arrScheduleOP.Count > 0)
                {
                    GridView1.DataSource = arrScheduleOP;
                    GridView1.DataBind();
                }

            }
            else
                LoadDataViaApi();

        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("InputSuratJalanCPD.aspx");
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToString("dd-MMM-yyyy");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadDataViaApi();
        }
    }
}