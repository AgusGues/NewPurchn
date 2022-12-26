using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapPO : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");

            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 99 , 20 ,false); </script>", false);
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string allQuery = string.Empty;
            string drTgl = string.Empty;
            string sdTgl = string.Empty;
            string PdrTgl = string.Empty;
            string PsdTgl = string.Empty;
            Users users = (Users)Session["Users"];
            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            Session["drTgl"] = PdrTgl;
            Session["sdTgl"] = PsdTgl;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            //ReportFacade reportFacade = new ReportFacade();
            //allQuery = reportFacade.ViewRekapPO(drTgl, sdTgl,users.ViewPrice );
            //strQuery = allQuery;
            //Session["Query"] = strQuery;
            //Cetak(this);
            ViewRekapPO(drTgl, sdTgl, users.ViewPrice);
        }
        private string ItemSPPBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select ItemName from Biaya where Biaya.ID=" + TableName + ".ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=" + TableName + ".SPPDetailID and " +
                " SPPDetail.SPPID=" + TableName + ".SPPID) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        private void ViewRekapPO(string tgl1, string tgl2, int viewPrice)
        {
            string strSQL = string.Empty;
            if (viewPrice == 0)
                strSQL = "SELECT POPurchn.ID,POPurchn.NoPO, SuppPurch.SupplierName,case when POPurchn.Approval=0 then 'Open' " +
                    "when POPurchn.Approval=1 then 'Head' when POPurchn.Approval=2 then 'Manager Corp.' end Approval, " +
                    "POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
              "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " +
              "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
              ItemSPPBiayaNew("POPurchnDetail") + " END AS ItemName, SPP.NoSPP,  POPurchnDetail.Qty,  " +
              "UOM.UOMCode Satuan, POPurchn.Disc, 0 Price,0 as Total, " +
              "POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc,POPurchn.Cetak " +
              "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID " +
              "INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID " +
              "INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID " +
              "INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
              "convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + tgl2 +
              "' and POPurchnDetail.Status >-1 order by groupdesc,POPurchn.NoPO";
            if (viewPrice == 1)
                strSQL = "select ID,NoPO,SupplierName,Approval,PPN,PPH,M_Uang,ItemName,NoSPP" +
             ",Qty,UOMCode Satuan,Disc,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  " +
             "then Price else 0 end Price,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  " +
             "then Total else 0 end Total,POPurchnDate,groupdesc,Cetak from ( " +
             "SELECT POPurchn.ID,popurchndetail.itemid, POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak," +
             "case when POPurchn.Approval=0 then 'Open' when POPurchn.Approval=1 then 'Head' when POPurchn.Approval=2 " +
             "then 'Manager Corp.' end  Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
             "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " +
             "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
             ItemSPPBiayaNew("POPurchnDetail") + " END AS ItemName, SPP.NoSPP,POPurchnDetail.Qty,  " +
             "UOM.UOMCode, POPurchn.Disc, case when POPurchn.Disc>0 then " +
             "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
             "else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then " +
             "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
             "else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total, " +
             " POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc " +
             "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID " +
             "INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID " +
             "INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID " +
             "INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
             "convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + tgl2 +
             "' and POPurchnDetail.Status >-1 )as A order by groupdesc,NoPO";
            if (viewPrice == 2)
                strSQL = "SELECT POPurchn.ID,POPurchn.NoPO, SuppPurch.SupplierName,case when POPurchn.Approval=0 then 'Open' " +
                    "when POPurchn.Approval=1 then 'Head' when POPurchn.Approval=2 then 'Manager Corp.' end  Approval, " +
                    "POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
              "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " +
              "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
              ItemSPPBiayaNew("POPurchnDetail") + " END AS ItemName, SPP.NoSPP,POPurchnDetail.Qty,  " +
              "UOM.UOMCode Satuan, POPurchn.Disc, case when POPurchn.Disc>0 then " +
              "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
              "else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then " +
              "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
              "else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total, " +
              " POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc,POPurchn.Cetak " +
              "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID " +
              "INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID " +
              "INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID " +
              "INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
              "convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + tgl2 + "' and POPurchnDetail.Status >-1  order by groupdesc,POPurchn.NoPO";

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
                if (col.ColumnName == "Price" || col.ColumnName == "Total" || col.ColumnName == "TOT2" || col.ColumnName == "PPN"
                    || col.ColumnName == "PPH" || col.ColumnName == "Disc" || col.ColumnName == "Qty" || col.ColumnName == "Price2")
                {
                    bfield.HeaderText = col.ColumnName;
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    //bfield.DataFormatString = "{0:N0}";
                    bfield.HeaderText = col.ColumnName;
                }
                if (col.ColumnName == "POPurchnDate")
                {
                    bfield.DataFormatString = "{0:d}";
                }
                if (col.ColumnName == "Ukuran")
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                if (col.ColumnName == "tebal")
                {
                    bfield.HeaderText = "Tebal";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    bfield.DataFormatString = "{0:N1}";
                }
                GrdDynamic.Columns.Add(bfield);


            }
            ButtonField btnPrint = new ButtonField();
            btnPrint.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            btnPrint.CommandName = "cetak";
            btnPrint.ButtonType = ButtonType.Button;
            btnPrint.Text = "Print";
            btnPrint.Visible = true;
            GrdDynamic.Columns.Add(btnPrint);
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapPO', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private void Cetak2(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report2.aspx?IdReport=POPurchn', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak2();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";
            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        protected void btnPrint0_Click(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string allQuery = string.Empty;
            string drTgl = string.Empty;
            string sdTgl = string.Empty;
            string PdrTgl = string.Empty;
            string PsdTgl = string.Empty;
            Users users = (Users)Session["Users"];
            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            Session["drTgl"] = PdrTgl;
            Session["sdTgl"] = PsdTgl;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ReportFacade reportFacade = new ReportFacade();
            allQuery = reportFacade.ViewRekapPO(drTgl, sdTgl, users.ViewPrice);
            strQuery = allQuery;
            Session["Query"] = strQuery;
            Cetak(this);
        }
        protected void GrdDynamic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GrdDynamic.Rows[index];
            if (e.CommandName == "cetak")
            {
                Session["ID"] = row.Cells[0].Text;
                Cetak2(this);
            }
        }
        protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int rowindex = Convert.ToInt32(e.Row.RowIndex.ToString());
                    Button btn = (Button)e.Row.Cells[17].Controls[0];
                    if (Convert.ToInt32((e.Row.Cells[16].Text)) == 0 && e.Row.Cells[3].Text == "Manager Corp.")
                        btn.Enabled = true;
                    else
                        btn.Enabled = false;

                }
            }
            catch { }
            //if (e.CommandName == "cetak")
            //{
            //    Session["ID"] = 
            //    Cetak2(this);
            //}
        }
    }
}