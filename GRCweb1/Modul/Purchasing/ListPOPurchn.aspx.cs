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
    public partial class ListPOPurchn : System.Web.UI.Page
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

        private void LoadData(string strGroupID)
        {
            POPurchnFacade poPurchnFacade = new POPurchnFacade();
            ArrayList arrPOPurchnDetail = new ArrayList();
            arrPOPurchnDetail = poPurchnFacade.RetrieveAllPO(int.Parse(strGroupID), "and A.Notawar like ", txtSearch.Text);

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

                Response.Redirect("FormPOPurchn.aspx?PONo=" + row.Cells[0].Text);

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");

                //if (e.Row.Cells[5].Text == "0")
                //{
                //    e.Row.Cells[5].Text = "Open";
                //}
                //if (e.Row.Cells[5].Text == "1")
                //{
                //    e.Row.Cells[5].Text = "Parsial";
                //}
                //if (e.Row.Cells[5].Text == "2")
                //{
                //    e.Row.Cells[5].Text = "Full PO";
                //}
                //// next what status

                //if (e.Row.Cells[6].Text == "0")
                //{
                //    e.Row.Cells[6].Text = "Open";
                //}
                //if (e.Row.Cells[6].Text == "1")
                //{
                //    e.Row.Cells[6].Text = "Head";
                //}
                //if (e.Row.Cells[6].Text == "2")
                //{
                //    e.Row.Cells[6].Text = "Manager";
                //}

                ////e.Row.Cells[6].Text = ((Status)int.Parse(e.Row.Cells[6].Text)).ToString();
                ////6
                ////e.Row.Cells[5].Text = ((Status)int.Parse(e.Row.Cells[5].Text)).ToString();
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("FormPOPurchn.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
        public string GetAppGroup(int IDUsers)
        {
            string result = string.Empty;
            try
            {
                string strSQL = "Select AppGroup from UsersApp where RowStatus>-1 and UserID=" + IDUsers.ToString();
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = sdr["AppGroup"].ToString();
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }
    }
}