using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessFacade;
using Domain;

using System.Web.Services;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using DefectFacade;
using System.IO;


namespace GRCweb1.ModalDialog
{
    public partial class ShowPOHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = Request.QueryString["txt"];
            string ItemName = Request.QueryString["itm"];
            Session["query"] = query;
            Session["itemname"] = ItemName;
            LoadData(query, ItemName);
            loadDynamicGrid();
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid()
        {
            string strSQL = " select * from (select A.ID, A.ItemCode,A.ItemName,C.UOMDesc Satuan, A.Jumlah, " +
                "A.MinStock,A.MaxStock,A.Stock Type_Stock, A.aktif,A.LeadTime  " +
                "from Inventory as A,UOM as C where A.UOMID = C.ID and A.aktif=1 union all " +
                "select A.ID, A.ItemCode,A.ItemName,C.UOMDesc Satuan, A.Jumlah, " +
                "A.MinStock,A.MaxStock,A.Stock Type_Stock, A.aktif,A.LeadTime  " +
                "from Asset as A,UOM as C where A.UOMID = C.ID and A.aktif=1  union all " +
                "select A.ID, A.ItemCode,A.ItemName,C.UOMDesc Satuan, A.Jumlah, " +
                "A.MinStock,A.MaxStock,A.Stock Type_Stock, A.aktif,A.LeadTime  " +
                "from Biaya as A,UOM as C where A.UOMID = C.ID) A where itemname like '%" + Session["itemname"].ToString().Trim() + "%'";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                bfield.DataFormatString = "{0:N0}";
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        private void LoadData(string strQry, string ItemName)
        {
            HistPOFacade histPOFacade = new HistPOFacade();
            ArrayList arrHistPO = new ArrayList();
            Users users = (Users)Session["Users"];
            string strPODetail = string.Empty;

            if (users.ViewPrice > 0)
            {
                if (users.ViewPrice == 1)
                {
                    arrHistPO = histPOFacade.ViewHistPO(strQry, ItemName, ")");
                    strPODetail = histPOFacade.ViewHistPORpt(strQry, ItemName, ")");
                }
                if (users.ViewPrice == 2)
                {
                    arrHistPO = histPOFacade.ViewHistPO2(strQry, ItemName, ")");
                    strPODetail = histPOFacade.ViewHistPORpt2(strQry, ItemName, ")");
                }
            }
            else
            {
                arrHistPO = histPOFacade.ViewHistPOByPrice0(strQry, ItemName, ")");
                strPODetail = histPOFacade.ViewHistPOByPrice0Rpt(strQry, ItemName, ")");
            }

            GridView1.DataSource = arrHistPO;
            GridView1.DataBind();
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            HistPOFacade histPOFacade = new HistPOFacade();
            ArrayList arrHistPO = new ArrayList();
            Users users = (Users)Session["Users"];
            string strPODetail = string.Empty;
            string strQry = Session["query"].ToString();
            string ItemName = Session["ItemName"].ToString();
            if (users.ViewPrice > 0)
            {
                if (users.ViewPrice == 1)
                {
                    arrHistPO = histPOFacade.ViewHistPO(strQry, ItemName, ")");
                    strPODetail = histPOFacade.ViewHistPORpt(strQry, ItemName, ")");
                }
                if (users.ViewPrice == 2)
                {
                    arrHistPO = histPOFacade.ViewHistPO2(strQry, ItemName, ")");
                    strPODetail = histPOFacade.ViewHistPORpt2(strQry, ItemName, ")");
                }
            }
            else
            {
                arrHistPO = histPOFacade.ViewHistPOByPrice0(strQry, ItemName, ")");
                strPODetail = histPOFacade.ViewHistPOByPrice0Rpt(strQry, ItemName, ")");
            }

            GridView1.DataSource = arrHistPO;
            GridView1.DataBind();
        }

    }
}