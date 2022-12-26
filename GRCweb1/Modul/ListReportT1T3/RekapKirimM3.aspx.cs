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
    public partial class RekapKirimM3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            //ddTahun.Items.Clear();
            //ArrayList arrTahun = new ArrayList();
            //T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            //arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            //ddTahun.Items.Clear();
            //ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            //foreach (T3_MutasiWIP bmTahun in arrTahun)
            //{
            //    ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            //}

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select YEAR(tglkirim)as Tahun from t3_kirim where YEAR(tglkirim)>year(GETDATE())-3  group by YEAR(tglkirim)";
            SqlDataReader sdr = zl.Retrieve();
            ddTahun.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddTahun.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
                }
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string periodeBulan = ddlBulan.SelectedValue;
            //string txtBulan = ddlBulan.SelectedItem.ToString();
            //string periodeTahun = ddTahun.SelectedValue;
            //string frmtPrint = string.Empty;
            //string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            //Users users = (Users)Session["Users"];
            //int userID = ((Users)Session["Users"]).ID;
            //ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            //string strQuery = "select tglkirim, SUM(M3_crnt) Karawang,SUM(M3_Other) Citeureup from ( "+
            //    "select B.TglKirim, A.Qty*((I.Tebal*I.Lebar*I.Panjang)/1000000000) M3_crnt,0 M3_Other   "+
            //    "from  T3_KirimDetail A inner join T3_Kirim B on A.KirimID =B.ID  "+
            //    "inner join  FC_Items I on A.ItemID=I.ID  where A.RowStatus>-1 and B.RowStatus>-1 "+
            //    "and LEFT(convert(char,B.tglkirim,112),6)='" + periodeTahun + padbulan + "' " +
            //    "union all "+
            //    "select B.TglKirim,0 M3_crnt, A.Qty*((I.Tebal*I.Lebar*I.Panjang)/1000000000) M3_Other   "+
            //    "from  vw_T3_KirimDetail_Other A inner join vw_T3_Kirim_Other B on A.KirimID =B.ID  "+
            //    "inner join  vw_FC_Items_Other I on A.ItemID=I.ID  where A.RowStatus>-1 and B.RowStatus>-1 " +
            //    "and LEFT(convert(char,B.tglkirim,112),6)='" + periodeTahun + padbulan + "') D group by TglKirim order by TglKirim ";
            //Session["Query"] = strQuery;
            //Session["periode"] = periodeTahun + padbulan;
            //Cetak(this);
            loadDynamicGrid();
        }
        private void loadDynamicGrid()
        {
            string periodeBulan = ddlBulan.SelectedValue;
            string txtBulan = ddlBulan.SelectedItem.ToString();
            string periodeTahun = ddTahun.SelectedValue;
            string frmtPrint = string.Empty;
            Users users = (Users)Session["Users"];
            int userID = ((Users)Session["Users"]).ID;
            string plant0 = string.Empty;
            string plant1 = string.Empty;
            string plant2 = string.Empty;
            string dbplant1 = string.Empty;
            string dbplant2 = string.Empty;
            string db1 = string.Empty;
            string db2 = string.Empty;
            if (users.UnitKerjaID == 1)
            {
                plant0 = "Citeureup";
                plant1 = "Karawang";
                plant2 = "Jombang";
                dbplant1 = "[sqlkrwg.grcboard.com]";
                dbplant2 = "[sqljombang.grcboard.com]";
                db1 = "bpaskrwg";
                db2 = "bpasjombang";
            }
            if (users.UnitKerjaID == 7)
            {
                plant0 = "Karawang";
                plant1 = "Citeureup";
                plant2 = "Jombang";
                dbplant1 = "[sqlctrp.grcboard.com]";
                dbplant2 = "[sqljombang.grcboard.com]";
                db1 = "bpasctrp";
                db2 = "bpasjombang";
            }
            if (users.UnitKerjaID == 13)
            {
                plant0 = "Jombang";
                plant1 = "Citeureup";
                plant2 = "Karawang";
                dbplant1 = "[sqlctrp.grcboard.com]";
                dbplant2 = "[sqlkrwg.grcboard.com]";
                db1 = "bpasctrp";
                db2 = "bpaskrwg";
            }
            string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            string strSQL = "with pengiriman as ( " +
                "select  ROW_NUMBER() OVER(ORDER BY tglkirim) ID,tglkirim, SUM(M3_crnt) " + plant0 + ", " +
                "SUM(M3_Other1) " + plant1 + ", SUM(M3_Other2) " + plant2 + ",SUM(M3_crnt)+SUM(M3_Other1)+SUM(M3_Other2) Total from ( " +
                "select B.TglKirim, A.Qty*((I.Tebal*I.Lebar*I.Panjang)/1000000000) M3_crnt,0 M3_Other1,0 M3_Other2   " +
                "from  T3_KirimDetail A inner join T3_Kirim B on A.KirimID =B.ID  " +
                "inner join  FC_Items I on A.ItemID=I.ID  where A.RowStatus>-1 and B.RowStatus>-1 " +
                "and LEFT(convert(char,B.tglkirim,112),6)='" + periodeTahun + padbulan + "' " +
                "union all " +
                "select * from openquery(" + dbplant1 + ", " +
                "'select B.TglKirim,0 M3_crnt, A.Qty*((I.Tebal*I.Lebar*I.Panjang)/1000000000) M3_Other1,0 M3_Other2   " +
                "from  " + db1 + ".dbo.T3_KirimDetail A inner join " + db1 + ".dbo.T3_Kirim B on A.KirimID =B.ID  " +
                "inner join  " + db1 + ".dbo.FC_Items I on A.ItemID=I.ID  where A.RowStatus>-1 and B.RowStatus>-1 " +
                "and LEFT(convert(char,B.tglkirim,112),6)=''" + periodeTahun + padbulan + "''') " +
                "union all " +
                "select * from openquery(" + dbplant2 + ", " +
                "'select B.TglKirim,0 M3_crnt, 0 M3_Other1,A.Qty*((I.Tebal*I.Lebar*I.Panjang)/1000000000) M3_Other2   " +
                "from  " + db2 + ".dbo.T3_KirimDetail A inner join " + db2 + ".dbo.T3_Kirim B on A.KirimID =B.ID  " +
                "inner join  " + db2 + ".dbo.FC_Items I on A.ItemID=I.ID  where A.RowStatus>-1 and B.RowStatus>-1 " +
                "and LEFT(convert(char,B.tglkirim,112),6)=''" + periodeTahun + padbulan + "''') " +
                ") D group by TglKirim), " +
                "pengiriman1 as (select * from pengiriman  " +
                "union  " +
                "select 100 ID,'1/1/1900' tglkirim,SUM(" + plant0 + ") " + plant0 + ",SUM(" + plant1 + ") " + plant1 + ",SUM(" + plant2 + ") " + plant2 + ",SUM(Total) Total  " +
                "from pengiriman)select * from pengiriman1 order by ID";

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
            string formdeci = "{0:N1}";
            //if (txtDecimal.Text.Trim() == "2") formdeci = "{0:N2}";
            //if (txtDecimal.Text.Trim() == "3") formdeci = "{0:N3}";
            //if (txtDecimal.Text.Trim() == "4") formdeci = "{0:N4}";
            //if (txtDecimal.Text.Trim() == "5") formdeci = "{0:N5}";
            //if (txtDecimal.Text.Trim() == "6") formdeci = "{0:N6}";
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = col.ColumnName;
                bfield.DataField = col.ColumnName;
                if (col.ColumnName.Substring(0, 1) == "t")
                {
                    // bfield.HeaderText = "%";
                    bfield.DataFormatString = "{0:d}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    bfield.DataFormatString = "{0:N2}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                if (col.ColumnName.Substring(0, 2) == "ID")
                {
                    bfield.HeaderText = "No";
                    bfield.DataFormatString = "{0:N0}";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            LblPeriode.Text = ddlBulan.SelectedItem.Text.Trim() + " " + ddTahun.SelectedItem.Text.Trim();
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Rekapkirim2plant" + ddlBulan.SelectedItem.Text.Trim() + ddTahun.SelectedItem.Text.Trim() + ".xls"));
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LRekapKirimM3', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getYear();
        }

        protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strvalue = string.Empty;

                for (int i = 0; i < e.Row.Cells[0].Text.Length; i++)
                {
                    if (e.Row.Cells[0].Text.Substring(i, 1) != "." && e.Row.Cells[0].Text.Substring(i, 1) != ",")
                        strvalue = strvalue + e.Row.Cells[0].Text.Substring(i, 1);
                }
                int value = Convert.ToInt32(strvalue);
                if (value >= 100)
                {
                    //e.Row.Cells[0].BackColor = Color.FromName("White");
                    //e.Row.Cells[0].ForeColor = Color.FromName("White");
                }
                if (Convert.ToDateTime(e.Row.Cells[1].Text).ToString("yyyyMMdd") == "19000101")
                    e.Row.Cells[1].Text = "Total";
            }
        }
    }
}