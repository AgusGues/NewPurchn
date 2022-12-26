using BusinessFacade;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class SPKDbystocker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtdrtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadStocker();
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 500, 100 , 20 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        private void LoadStocker()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select distinct isnull(stocker,'')stocker from FC_LinkItemMkt where isnull(stocker,'') <>'' and RowStatus>-1 order by stocker";
            SqlDataReader sdr = zl.Retrieve();
            ddlStocker.Items.Clear();
            ddlStocker.Items.Add(new ListItem("ALL", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlStocker.Items.Add(new ListItem(sdr["stocker"].ToString(), sdr["stocker"].ToString().TrimEnd()));
                }
            }
            ddlStocker.SelectedValue = user.UnitKerjaID.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LblTgl1.Text = txtdrtanggal.Text;
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"));
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid(string tgl1)
        {
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty;
            if (ddlStocker.SelectedValue != "0")
                strStocker = "where stocker='" + ddlStocker.SelectedItem.Text.Trim() + "'";
            string strSQL = "select Scheduleno No_Schedule,OPNo NO_OP, barang Nama_Barang,qty Jumlah_Barang,stocker,CarType,Keterangan from (select S.ID, S.Scheduleno,SD.documentno OPNo,I.[Description] Barang,SD.QTY , " +
                "(select top 1 IM.stocker from FC_LinkItemMkt IM where IM.ItemIDMkt =SD.itemid) stocker,ED.CarType,S.Keterangan  " +
                "from [sql1.grcboard.com].grcboard.dbo.schedule S inner join [sql1.grcboard.com].grcboard.dbo.scheduledetail SD on S.ID=SD.scheduleid  " +
                "inner join [sql1.grcboard.com].grcboard.dbo.Items I on SD.itemid=I.ID left join OP on SD.documentid=OP.ID  " +
                "inner join [sql1.grcboard.com].grcboard.dbo.ExpedisiDetail ED on S.ExpedisiDetailID=ED.ID where I.Tebal>0 and S.status>-1 and SD.status>-1 and S.depoid=" + users.UnitKerjaID + " and convert(char,S.scheduledate,112)='" + tgl1 + "')A " +
                "  " + strStocker + " order by stocker, No_Schedule";
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

                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "spkd by stocker.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            {
                GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            Panel1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();

                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);



            }
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
    }
}