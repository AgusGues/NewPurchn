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
    public partial class ListPostSuratJalanNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            Users users = (Users)Session["Users"];
            SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
            ArrayList arrSuratJalan = new ArrayList();
            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsarrSuratJalan = cpdWebService.sj_RetrieveReceivedNoByDepoOnSchedule(users.UnitKerjaID);
            foreach (DataRow row in dsarrSuratJalan.Tables[0].Rows)
            {
                SuratJalan sJ = new SuratJalan();

                sJ.SuratJalanNo = row["SuratJalanNo"].ToString();
                sJ.CreatedTime = DateTime.Parse(row["CreatedTime"].ToString());
                sJ.OPNo = row["OPNo"].ToString();
                sJ.ScheduleNo = row["ScheduleNo"].ToString();
                sJ.PoliceCarNo = row["PoliceCarNo"].ToString();
                sJ.DriverName = row["DriverName"].ToString();
                sJ.CreatedBy = row["CreatedBy"].ToString();
                sJ.Status = int.Parse(row["Status"].ToString());

                arrSuratJalan.Add(sJ);
            }
            if (arrSuratJalan.Count > 0)
            {
                GridView1.DataSource = arrSuratJalan;
                GridView1.DataBind();
            }


            //if (Request.QueryString["receive"].ToString() == "yes")
            //    arrSuratJalan = suratJalanFacade.RetrieveReceivedByDepoOnSchedule(users.UnitKerjaID);
            //else if (Request.QueryString["receive"].ToString() == "ok")
            //    arrSuratJalan = suratJalanFacade.RetrieveNoReceivedByDepoOnSchedule2(users.UnitKerjaID);
            //else
            //    arrSuratJalan = suratJalanFacade.RetrieveReceivedNoByDepoOnSchedule(users.UnitKerjaID);
            //if (suratJalanFacade.Error == string.Empty)
            //{
            //    GridView1.DataSource = arrSuratJalan;
            //    GridView1.DataBind();
            //}

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                Session["ketloading"] = null;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                //if (Request.QueryString["receive"].ToString() == "yes")
                //    Response.Redirect("PostingReceiveSuratJalanOP.aspx?SJNo=" + row.Cells[0].Text);
                //else if (Request.QueryString["receive"].ToString() == "no")
                Response.Redirect("PostingSuratJalanCPD.aspx?SJNo=" + row.Cells[0].Text);

                //else
                //    Response.Redirect("PostingReceiveSuratJalanOP.aspx?SuratJalanNo=" + row.Cells[0].Text);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");
                e.Row.Cells[7].Text = Global.Status(int.Parse(e.Row.Cells[7].Text));
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            //if (Request.QueryString["receive"].ToString() == "no")
            Response.Redirect("PostingSuratJalanCPD.aspx");
            //else
            //    Response.Redirect("PostingReceiveSuratJalanOP.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text != string.Empty)
                LoadDataByCriteria();
            else
                LoadData();
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
                LoadDataByCriteria();
            else
                LoadData();

        }

        private void LoadDataByCriteria()
        {
            string strField = ddlSearch.SelectedValue;
            string strValue = txtSearch.Text;

            if (ddlSearch.SelectedIndex == 3)
            {
                if (strValue == "Open")
                    strValue = "0";
                else if (strValue == "Shipment")
                    strValue = "1";
                else if (strValue == "Received")
                    strValue = "2";
                else if (strValue == "Invoice")
                    strValue = "3";

                strField = "A.Status";
            }

            SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
            ArrayList arrSuratJalan = new ArrayList();

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsarrSuratJalan = cpdWebService.sj_RetrieveReceivedNoByCriteria(strField, strValue, ((Users)Session["Users"]).UnitKerjaID);

            foreach (DataRow row in dsarrSuratJalan.Tables[0].Rows)
            {
                SuratJalan sJ = new SuratJalan();

                sJ.SuratJalanNo = row["SuratJalanNo"].ToString();
                sJ.CreatedTime = DateTime.Parse(row["CreatedTime"].ToString());
                sJ.OPNo = row["OPNo"].ToString();
                sJ.ScheduleNo = row["ScheduleNo"].ToString();
                sJ.PoliceCarNo = row["PoliceCarNo"].ToString();
                sJ.DriverName = row["DriverName"].ToString();
                sJ.CreatedBy = row["CreatedBy"].ToString();
                sJ.Status = int.Parse(row["Status"].ToString());

                arrSuratJalan.Add(sJ);
            }
            if (arrSuratJalan.Count > 0)
            {
                GridView1.DataSource = arrSuratJalan;
                GridView1.DataBind();
            }

            //if (Request.QueryString["receive"].ToString() == "yes")
            //    arrSuratJalan = suratJalanFacade.RetrieveReceivedByCriteria(strField, strValue, ((Users)Session["Users"]).UnitKerjaID);
            //else if (Request.QueryString["receive"].ToString() == "ok")
            //    arrSuratJalan = suratJalanFacade.RetrieveNoReceivedByCriteria2(strField, strValue, ((Users)Session["Users"]).UnitKerjaID);
            //else
            //    arrSuratJalan = suratJalanFacade.RetrieveReceivedNoByCriteria(strField, strValue, ((Users)Session["Users"]).UnitKerjaID);

            //if (suratJalanFacade.Error == string.Empty)
            //{
            //    GridView1.DataSource = arrSuratJalan;
            //    GridView1.DataBind();
            //}
        }
    }
}