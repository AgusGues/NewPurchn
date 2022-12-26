using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer;

namespace GRCweb1.Modul.Purchasing
{
    public partial class EditSPPNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
            {


                Users users = (Users)Session["Users"];

                EditSPPFacade sppFacade = new EditSPPFacade();
                ArrayList arrSPPDetail = new ArrayList();

                UsersHeadFacade usersHeadFacade = new UsersHeadFacade();
                UsersHead usersHead = usersHeadFacade.RetrieveByUserID(users.ID.ToString());

                if (usersHeadFacade.Error == string.Empty)
                {
                    //arrSPPDetail = sppFacade.RetrieveByAllWithStatus(int.Parse(Request.QueryString["approve"]), "A.NoSPP", txtSearch.Text);
                    arrSPPDetail = sppFacade.RetrieveByAllWithStatusEditSPP((users.Apv == 0) ? usersHead.HeadID : users.ID, "A.NoSPP", txtSearch.Text);

                    if (sppFacade.Error == string.Empty)
                    {
                        GridView1.DataSource = arrSPPDetail;
                        GridView1.DataBind();
                    }

                }
                else
                {
                    DisplayAJAXMessage(this, "Head-ID tidak ada ..!");
                    return;
                }

            }
        }



        private void LoadData(string strGroupID)
        {
            EditSPPFacade sppFacade = new EditSPPFacade();
            ArrayList arrSPPDetail = new ArrayList();

            //review dulu baru ubah ke headID
            Users usr = new Users();
            usr.Apv = ((Users)Session["Users"]).Apv;
            usr.ID = ((Users)Session["Users"]).ID;
            arrSPPDetail = sppFacade.RetrieveByAll(int.Parse(strGroupID));

            if (sppFacade.Error == string.Empty)
            {
                GridView1.DataSource = arrSPPDetail;
                GridView1.DataBind();
            }

        }

        private Decimal cekQtyPO(int idsppdetail)
        {
            decimal nilai = 0;

            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery = "select QtyPO from SPPDetail where ID=" + idsppdetail + " and Status >-1 ";
            SqlDataReader sdr = zv.Retrieve();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    nilai = decimal.Parse( sdr["QtyPO"].ToString());

                }
            }

            return nilai;
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                decimal nilaiQtyPO = this.cekQtyPO(int.Parse(row.Cells[0].Text));

                if (nilaiQtyPO == 0)
                {
                    Response.Redirect("FormEditSPP.aspx?ID=" + row.Cells[0].Text);
                }
                else
                {
                    DisplayAJAXMessage(this, "SPP Sudah Dibuat PO, Tidak Bisa Melakukan Edit");
                }



            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[2].Text).ToString("dd-MMM-yyyy");

                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "Open";
                }
                if (e.Row.Cells[5].Text == "1")
                {
                    e.Row.Cells[5].Text = "Parsial";
                }
                if (e.Row.Cells[5].Text == "2")
                {
                    e.Row.Cells[5].Text = "Full PO";
                }
                // next what status

                if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "Open";
                }
                if (e.Row.Cells[6].Text == "1")
                {
                    e.Row.Cells[6].Text = "Head";
                }
                if (e.Row.Cells[6].Text == "2")
                {
                    e.Row.Cells[6].Text = "Manager";
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString());
        }
    }
}