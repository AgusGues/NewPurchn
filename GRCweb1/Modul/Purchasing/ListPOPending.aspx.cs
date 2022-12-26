using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ListPOPending : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string grp = (Request.QueryString["pogroup"] != null) ? Request.QueryString["pogroup"].ToString() : "";
            txtGroup.Value = grp.ToString();
            if (!Page.IsPostBack)
            {
                string aprove = (Request.QueryString["approve"] != null) ? Request.QueryString["approve"].ToString() : "0";
                LoadData(aprove, txtGroup.Value);
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                string aprove = (Request.QueryString["approve"] != null) ? Request.QueryString["approve"].ToString() : "0";
                //string grp = (Request.QueryString["pogroup"] != null) ? Request.QueryString["pogroup"].ToString() : "";
                //txtGroup.Value = grp.ToString();
                LoadData(aprove, txtGroup.Value);
            }
            else
            {
                //POPurchnFacade pOPurchnFacade = new POPurchnFacade();
                //ArrayList arrPOPurchnDetail = new ArrayList();

                //arrPOPurchnDetail = pOPurchnFacade.RetrieveAllPO(0, "and A.NoPO like ", txtSearch.Text);

                //if (pOPurchnFacade.Error == string.Empty)
                //{
                //    GridView1.DataSource = arrPOPurchnDetail;
                //    GridView1.DataBind();
                //}
            }
        }

        private void LoadData(string strGroupID, string GroupApp)
        {
            Users users = (Users)Session["Users"];
            string[] groupApp = txtGroup.Value.Split(',');
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();

            ArrayList arrNewPOpurchn = new ArrayList();
            ArrayList arrPOPurchn = new ArrayList();
            //arrPOPurchn = pOPurchnFacade.RetrieveOpenStatusByDepo(users.Apv - 1, users.UnitKerjaID);
            arrPOPurchn = pOPurchnFacade.ViewGridPOWithApproval(users.Apv - 1, users.UnitKerjaID);
            if (pOPurchnFacade.Error == string.Empty)
            {
                foreach (POPurchn pp in arrPOPurchn)
                {
                    int pos = Array.IndexOf(groupApp, pp.GroupID.ToString());
                    if (pos != -1)
                    {
                        POPurchn pop = new POPurchn();
                        pop.ID = pp.ID;
                        pop.NoPO = pp.NoPO;
                        pop.NOSPP = pp.NOSPP;
                        pop.ItemCode = pp.ItemCode;
                        pop.NamaBarang = pp.NamaBarang;
                        pop.Qty = pp.Qty;
                        pop.Satuan = pp.Satuan;
                        pop.Approval = pp.Approval;
                        pop.Price = pp.Price;
                        arrNewPOpurchn.Add(pop);
                    }
                }
                GridView1.DataSource = (arrNewPOpurchn.Count == 0) ? arrPOPurchn : arrNewPOpurchn;
                GridView1.DataBind();

            }

            //POPurchnFacade poPurchnFacade = new POPurchnFacade();
            //ArrayList arrPOPurchnDetail = new ArrayList();

            //arrPOPurchnDetail = poPurchnFacade.RetrieveAllPO(int.Parse(strGroupID), "and A.NoPO like ", txtSearch.Text);

            //if (poPurchnFacade.Error == string.Empty)
            //{
            //    GridView1.DataSource = arrPOPurchnDetail;
            //    GridView1.DataBind();
            //}

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Users usr = ((Users)Session["Users"]);
                if (usr.Apv > 0 && usr.DeptID == 15)
                {
                    Response.Redirect("ApprovalPONew2.aspx?PONo=" + row.Cells[0].Text);
                }

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sts = e.Row.Cells[7].Text.ToString();
                switch (sts)
                {
                    case "0":
                        e.Row.Cells[7].Text = "Open";
                        break;
                    case "1":
                        e.Row.Cells[7].Text = "Head";
                        break;
                    case "2":
                        e.Row.Cells[7].Text = "Mgr";
                        break;
                    default:
                        e.Row.Cells[7].Text = "Cancel";
                        break;
                }
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
            Response.Redirect("ApprovalPONew2.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData(Request.QueryString["approve"].ToString(), "");
        }
        private void LoadOpenPO()
        {
            Users users = (Users)Session["Users"];
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            POApprovalFacade2 poApp = new POApprovalFacade2();
            ArrayList arrPOPurchn = new ArrayList();
            /**
             * Pemisahan List Open PO yang muncul base on userid
             * Pengaturan group approval ada tabel usersapproval
             */
            string AppGroup = pOPurchnFacade.GetAppGroup(users.ID);
            poApp.Criteria = " and (AlasanNotApproval='' or AlasanNotApproval is null) and Approval =" + (users.Apv - 1);
            poApp.Criteria += (AppGroup != string.Empty) ? " and Pod.GroupID in(" + AppGroup + ")" : string.Empty;
            poApp.OrderBy = (users.Apv > 2) ? "Order By ID Desc" : "Order By ID ";
            arrPOPurchn = poApp.RetrieveOpenPO();
            lstPO.DataSource = arrPOPurchn;
            lstPO.DataBind();

        }
        private ArrayList LoadOpenPO(string NoPO)
        {
            ArrayList arrPO = new ArrayList();
            Users users = (Users)Session["Users"];
            POApprovalFacade2 poApp = new POApprovalFacade2();
            poApp.Criteria = String.Format(" and NoPO='{0}'", NoPO);
            poApp.Criteria += " and (AlasanNotApproval='' or AlasanNotApproval is null)";
            arrPO = poApp.RetrieveOpenPO();
            return arrPO;
        }
        protected void lstPO_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //GridViewRow row = (GridViewRow)lstPO.Rows[e.Row.RowIndex];
                string NoPO = lstPO.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gridD = (GridView)e.Row.FindControl("GridView1");
                gridD.DataSource = LoadOpenPO(NoPO);
                gridD.DataBind();
            }
        }
    }
    public class POApprovalFacade2 : POPurchnFacade
    {
        POPurchn objPO = new POPurchn();
        ArrayList arrData = new ArrayList();
        public string Criteria { get; set; }
        public string OrderBy { get; set; }

        public ArrayList RetrieveOpenPO()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT DISTINCT TOP 100 PO.*,SP.SupplierName,SP.UP " +
                          "FROM POPurchn PO  " +
                          "LEFT JOIN POPurchnDetail POD ON POD.POID=PO.ID " +
                          "LEFT JOIN SuppPurch SP ON SP.ID=PO.SupplierID " +
                          "WHERE PO.Status =0 and POD.Status>-1 " + this.Criteria +
                          this.OrderBy;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GetObject(sdr, GenerateObjectNodetail(sdr)));
                }
            }
            return arrData;
        }
        public POPurchn GetObject(SqlDataReader sdr, POPurchn objP)
        {
            objPO = (POPurchn)objP;
            objPO.SupplierName = sdr["SupplierName"].ToString();
            objPO.UP = sdr["UP"].ToString();
            objPO.ItemFrom = int.Parse(sdr["ItemFrom"].ToString());
            return objPO;
        }
        public decimal TotalPricePO(string POID)
        {
            decimal total = 0;
            string sqlQuery = "Select ISNULL(SUM(Qty*Price),0)Price From POPurchnDetail Where Status>-1 and POID=" + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(sqlQuery);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    total = Convert.ToDecimal(sdr["Price"].ToString());
                }
            }
            return total;
        }
    }
}