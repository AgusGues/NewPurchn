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
    public partial class ListReceiptMRS : System.Web.UI.Page
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
                Global.link = "~/Default.aspx";

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
                ReceiptFacade receiptFacade = new ReceiptFacade();
                ArrayList arrReceipt = new ArrayList();

                if (Request.QueryString["approve"].ToString().Substring(1, 1) == "C")
                    arrReceipt = receiptFacade.RetrieveOpenStatusCriteriaForAsset(txtSearch.Text, Request.QueryString["approve"].ToString());
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "B")
                    arrReceipt = receiptFacade.RetrieveOpenStatusCriteriaForBiaya(txtSearch.Text, Request.QueryString["approve"].ToString());
                else
                    arrReceipt = receiptFacade.RetrieveOpenStatusCriteria(txtSearch.Text, Request.QueryString["approve"].ToString());
                if (receiptFacade.Error == string.Empty)
                {
                    GridView1.DataSource = arrReceipt;
                    GridView1.DataBind();
                }
            }
        }

        private void LoadData(string strApprove)
        {
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrReceipt = new ArrayList();
            if (strApprove.Substring(0, 1) == "X")
                arrReceipt = receiptFacade.RetrieveOpenStatusApprove(strApprove.Substring(1, 1));
            else
                arrReceipt = receiptFacade.RetrieveOpenStatus(strApprove);

            if (receiptFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrReceipt;
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Pageindex = 0;

            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                if (GridView1.PageIndex > 0)
                    Pageindex = (GridView1.PageIndex * GridView1.PageSize);
                index = index + Pageindex;
                //if (Request.QueryString["approve"].ToString() == "KS")
                if (Request.QueryString["approve"].ToString().Substring(1, 1) == "S")
                    Response.Redirect("FormReceiptMRS.aspx?ReceiptNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "O")
                    Response.Redirect("FormReceiptMRS.aspx?ReceiptNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "P")
                    Response.Redirect("FormReceiptRMS.aspx?ReceiptNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "A")
                    Response.Redirect("FormReceiptORS.aspx?ReceiptNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "T")
                    Response.Redirect("FormReceiptMKT.aspx?ReceiptNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "C")
                    Response.Redirect("FormReceiptARS.aspx?ReceiptNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "B")
                    Response.Redirect("FormReceiptBiaya.aspx?ReceiptNo=" + row.Cells[0].Text);
                else if (Request.QueryString["approve"].ToString().Substring(0, 1) == "X")
                    Response.Redirect("ApprovePenerimaan.aspx?ReceiptNo=" + row.Cells[0].Text);

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
            if (Request.QueryString["approve"].ToString().Substring(1, 1) == "S")
                Response.Redirect("FormReceiptMRS.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "O")
                Response.Redirect("FormReceiptMRS.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "P")
                Response.Redirect("FormReceiptRMS.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "A")
                Response.Redirect("FormReceiptORS.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "T")
                Response.Redirect("FormReceiptMKT.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "C")
                Response.Redirect("FormReceiptARS.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(1, 1) == "B")
                Response.Redirect("FormReceiptBiaya.aspx");
            else if (Request.QueryString["approve"].ToString().Substring(0, 1) == "X")
                Response.Redirect("ApprovePenerimaan.aspx");

            //    Response.Redirect("ApproveScheduled.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
    }
}