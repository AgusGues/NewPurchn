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

using DataAccessLayer;
using Factory;
using Cogs;
using System.Web.Services;
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
//using DefectFacade;
using System.IO;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapPakaiNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");

                LoadTipeSPP();
                LoadDept();
            }
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 450, 200 , 20 ,false); </script>", false);
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadTipeSPP()
        {
            ddlTipeSPP.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            //arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).GroupID);
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();

            //ddlTipeSPP.Items.Add(new ListItem("-- ALL --", string.Empty));
            ddlTipeSPP.Items.Add(new ListItem("-- ALL --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }

        }

        private void LoadDept()
        {
            ddlDeptName.Items.Clear();

            ArrayList arrDeptName = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDeptName = deptFacade.RetrieveDept();
            int n = 0;
            ddlDeptName.Items.Add(new ListItem("-- ALL --"));
            foreach (Dept dept in arrDeptName)
            {
                n = n + 1;
                ddlDeptName.Items.Add(new ListItem(dept.Alias));
                ddlDeptName.Items[n].Attributes.Add("title", dept.Alias);
            }
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
            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            Session["drTgl"] = PdrTgl;
            Session["sdTgl"] = PsdTgl;
            Session["deptName"] = ddlTipeSPP.SelectedItem;
            Session["alias"] = ddlDeptName.SelectedItem;

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            ReportFacade reportFacade = new ReportFacade();
            Users users = (Users)Session["Users"];
            //if (users.ViewPrice > 0)
            //{
            //    if (users.ViewPrice == 1)
            //        //allQuery = reportFacade.ViewRekapPakai(drTgl, sdTgl, ddlTipeSPP.SelectedIndex, Convert.ToInt32(ddlDeptName.Text));
            //        allQuery = reportFacade.ViewRekapPakai1(drTgl, sdTgl,Convert.ToInt32(ddlTipeSPP.SelectedValue), ddlDeptName.SelectedItem.Text);
            //    if (users.ViewPrice == 2)
            //        //allQuery = reportFacade.ViewRekapPakai3(drTgl, sdTgl, Convert.ToInt32(ddlTipeSPP.SelectedValue), ddlDeptName.SelectedItem.Text);
            //        allQuery = reportFacade.ViewRekapPakai3(drTgl, sdTgl, Convert.ToInt32(ddlTipeSPP.SelectedValue), ddlDeptName.SelectedItem.Text);
            //}
            //else
            //{
            //    //allQuery = reportFacade.ViewRekapPakaiByPrice0(drTgl, sdTgl, ddlTipeSPP.SelectedIndex, Convert.ToInt32(ddlDeptName.Text));
            //    allQuery = reportFacade.ViewRekapPakaiByPrice01(drTgl, sdTgl, Convert.ToInt32(ddlTipeSPP.SelectedValue), ddlDeptName.SelectedItem.Text);
            //}
            string strSQuery = string.Empty;
            if (RBDetail.Checked == true)
            {
                Session["type"] = "detail";
                //strSQuery = reportFacade.ViewRekapSPB(drTgl, sdTgl, ddlTipeSPP.SelectedIndex, Convert.ToInt32(ddlDeptName.Text), 0);
                strSQuery = reportFacade.ViewRekapSPB1(drTgl, sdTgl, Convert.ToInt32(ddlTipeSPP.SelectedValue), ddlDeptName.SelectedItem.Text, 0);
            }
            else
            {
                Session["type"] = "rekap";
                //strSQuery = reportFacade.ViewRekapSPBR(drTgl, sdTgl, ddlTipeSPP.SelectedIndex, Convert.ToInt32(ddlDeptName.Text), 0);
                strSQuery = reportFacade.ViewRekapSPBR1(drTgl, sdTgl, Convert.ToInt32(ddlTipeSPP.SelectedValue), ddlDeptName.SelectedItem.Text, 0);
            }
            Session["Query"] = strSQuery;
            Session["VPrice"] = ((Users)Session["Users"]).Apv.ToString();
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=RekapPakai', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";

            //if (ddlDeptName.SelectedIndex == 0)
            //    return "Pilih Dept tidak boleh kosong";

            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void ddlDeptName_DatabOund(object sender, EventArgs e)
        {
            DropDownList dd = sender as DropDownList;

            foreach (ListItem li in dd.Items)
            {
                li.Attributes["title"] = li.Text;
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void LoadListPakai()
        //private void LoadListPakai(int ID, int DeptID, Repeater rps)
        {
            Users users = (Users)Session["Users"];
            LblTgl1.Text = string.Empty;
            string drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            string sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            int Bulan = int.Parse(drTgl.Substring(4, 2));
            int Tahun = int.Parse(drTgl.Substring(0, 4));
            if (Bulan == 1) { Tahun = Tahun - 1; }
            string jam = DateTime.Now.ToString("yyMMss");
            string SaldoAwal = string.Empty;
            SaldoAwal = Global.nBulan((Bulan == 1) ? 12 : Bulan - 1, true) + "AvgPrice";
            int vprice = users.ViewPrice;
            string harga = "0";
            string total = "0";
            if (vprice == 1)
            {
                harga = "";
                total = "sum(Total)";
            }

            //ArrayList arrData = new ArrayList();
            //ZetroView zl = new ZetroView();
            //zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = 
            string GroupID = string.Empty;
            string DeptID = string.Empty;
            if (ddlTipeSPP.SelectedIndex > 0)
                GroupID = " and GroupID=" + ddlTipeSPP.SelectedValue + " ";
            if (ddlDeptName.SelectedIndex > 0)
                DeptID = " and DeptID in (select ID from dept where alias='" + ddlDeptName.SelectedValue + "') ";
            string strSQL = string.Empty;
            if (RBDetail.Checked == true)
                strSQL =
                    "select (case when GROUPING(DeptName) = 0 and " +
                    "      GROUPING(GroupDescription) = 1 and  " +
                    "		  GROUPING(PakaiNo) = 1 and GROUPING(PakaiDate) = 1 and GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                    "		  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
                    "then 'Sub Total ' + DeptName  " +
                    " when GROUPING(DeptName) = 1 and " +
                    "      GROUPING(GroupDescription) = 1 and  " +
                    "	  GROUPING(PakaiNo) = 1 and GROUPING(PakaiDate) = 1 and GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                    "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1 then 'Grand Total' " +
                    " else DeptName " +
                    "    end) as DeptName,(case when GROUPING(DeptName) = 0 and " +
                    "      GROUPING(GroupDescription) = 0 and  " +
                    "		  GROUPING(PakaiNo) = 1 and GROUPING(PakaiDate) = 1 and GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                    "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
                    " then 'Sub Total ' + GroupDescription else GroupDescription end )GroupDescription, " +
                     "PakaiNo,PakaiDate,ItemCode,ItemName,UOMCode,Jumlah," + harga + " Harga," + total + " Total ,Keterangan,Status,NoPol " +
                    "    from ( " +
                    "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                    "  case when crc >1 THEN (Harga) " +
                    "   else " +
                    "   isnull(Harga,0) end Harga,Harga*SUM(Quantity) Total, Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
                    "   GroupID,GroupDescription,DeptName,Status, " +
                    "   crc,ItemID,ISNULL(NoPol,'')NoPol " +
                    "    from  " +
                    "  (SELECT  " +
                    "      1 crc, " +
                    "      CASE PakaiDetail.ItemTypeID   " +
                    "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                    "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                    "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                    "      CASE PakaiDetail.ItemTypeID   " +
                    "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                    "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                    "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                    "      CASE PakaiDetail.ItemTypeID   " +
                    "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                    "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                    "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                    "      CASE WHEN PakaiDetail.ItemTypeID=3 THEN  avgprice " +
                    //"          ( " +
                    //"          SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID  " +
                    //"          and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=Pakai.PakaiDate order by id desc " +
                    //"          ) " +
                    "      ELSE " +
                    "      ISNULL(PakaiDetail.AvgPrice,  " +
                    "      (select top 1 " + SaldoAwal + " From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=year(Pakai.PakaiDate)))	  " +
                    "      END Harga,        " +
                    "      CASE when PakaiDetail.GroupID>0 THEN  " +
                    "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
                    "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
                    "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                    "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
                    "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine,PakaiDetail.NoPol  " +
                    "      FROM Pakai  " +
                    "      INNER JOIN PakaiDetail  " +
                    "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                    "      INNER JOIN UOM  " +
                    "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                    "      INNER JOIN Dept  " +
                    "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                    "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + drTgl + "' and   " +
                    "      convert(varchar,Pakai.PakaiDate,112)<='" + sdTgl + "'" + GroupID + DeptID + " ) as AA  " +
                    "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
                    "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine,NoPol " +
                    "      ) s group by grouping sets((GroupDescription,Deptname,PakaiNo,PakaiDate,ItemCode,ItemName," +
                    "UOMCode,Jumlah,Harga,Keterangan,Status,NoPol),(GroupDescription,DeptName),(DeptName),()) ";
            else
                strSQL =
                "select (case when GROUPING(DeptName) = 0 and " +
                "      GROUPING(GroupDescription) = 1 and  " +
                "		  GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                "		  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
                "then 'Sub Total ' + DeptName  " +
                " when GROUPING(DeptName) = 1 and  GROUPING(GroupDescription) = 1 and  " +
                "	  GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1 then 'Grand Total' " +
                " else DeptName " +
                "    end) as DeptName,(case when GROUPING(DeptName) = 0 and " +
                "      GROUPING(GroupDescription) = 0 and  " +
                "	   GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
                " then 'Sub Total ' + GroupDescription else GroupDescription end )GroupDescription, " +
                " ItemCode,ItemName,UOMCode,Jumlah," + harga + " Harga," + total + " Total from ( " +
                "select GroupID,GroupDescription,ItemCode,ItemName,UOMCode,sum(Jumlah)Jumlah,case when sum(jumlah)>0 then " +
                "sum(Harga* Jumlah)/sum(jumlah) else 0 end Harga,case when sum(jumlah)>0 then " +
                "(sum(Harga* Jumlah)/sum(jumlah))*sum(Jumlah) else 0 end Total,DeptName from ( " +
                "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                "  case when crc >1 THEN (Harga) " +
                "   else " +
                "   isnull(Harga,0) end Harga,Harga*SUM(Quantity) Total, Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
                "   GroupID,GroupDescription,DeptName,Status, " +
                "   crc,ItemID,ISNULL(NoPol,'')NoPol " +
                "    from  " +
                "  (SELECT  " +
                "      1 crc, " +
                "      CASE PakaiDetail.ItemTypeID   " +
                "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                "      CASE PakaiDetail.ItemTypeID   " +
                "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                "      CASE PakaiDetail.ItemTypeID   " +
                "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                "      CASE WHEN PakaiDetail.ItemTypeID=3 THEN avgprice " +
                //"          ( " +
                //"          SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID  " +
                //"          and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=Pakai.PakaiDate order by id desc " +
                //"          ) " +
                "      ELSE " +
                "      ISNULL(PakaiDetail.AvgPrice,  " +
                "      (select top 1 " + SaldoAwal + " From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=year(Pakai.PakaiDate)))	  " +
                "      END Harga,        " +
                "      CASE when PakaiDetail.GroupID>0 THEN  " +
                "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
                "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
                "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
                "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine,PakaiDetail.NoPol  " +
                "      FROM Pakai  " +
                "      INNER JOIN PakaiDetail  " +
                "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                "      INNER JOIN UOM  " +
                "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                "      INNER JOIN Dept  " +
                "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + drTgl + "' and   " +
                "      convert(varchar,Pakai.PakaiDate,112)<='" + sdTgl + "'" + GroupID + DeptID + " ) as AA  " +
                "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
                "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine,NoPol " +
                "      ) A group by GroupID,DeptName,GroupDescription,ItemCode,ItemName,UOMCode,Harga " +
                "      ) s group by grouping sets((GroupDescription,Deptname,ItemCode,ItemName," +
                "UOMCode,Jumlah,Harga),(GroupDescription,DeptName),(DeptName),()) ";
            //try
            //{
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
            string col1 = string.Empty;
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                bfield.DataFormatString = "{0:N0}";

                if (bfield.HeaderText.Trim() == "Jumlah" || bfield.HeaderText.Trim() == "Harga" || bfield.HeaderText.Trim() == "Total")
                { bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right; bfield.DataFormatString = "{0:N1}"; }
                else
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                if (bfield.HeaderText.Trim() == "PakaiDate")
                    bfield.DataFormatString = "{0:d}";

                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            LblTgl1.Text = txtTgl1.Text + " s/d " + txtTgl2.Text;

            //}
            //catch { }
            //SqlDataReader sdr = zl.Retrieve();
            //RP_PakaiFacade rf= new RP_PakaiFacade();
            //if (sdr.HasRows)
            //{
            //while (sdr.Read())
            //{
            //    arrData.Add(new RP_Pakai 
            //    {
            //        PakaiNo = sdr["PakaiNo"].ToString(),
            //        PakaiDate = sdr["PakaiDate"].ToString(),
            //        ItemCode = sdr["ItemCode"].ToString(),
            //        ItemName = sdr["ItemName"].ToString(),
            //        Satuan = sdr["UOMCode"].ToString(),
            //        Jumlah = decimal.Parse( sdr["Jumlah"].ToString()),
            //        Harga = decimal.Parse(sdr["Harga"].ToString()),
            //        Total = decimal.Parse(sdr["Total"].ToString()),
            //        Status = sdr["Status"].ToString(),
            //        Keterangan = sdr["Keterangan"].ToString(),
            //        NoPol = sdr["NoPol"].ToString()
            //    });
            //}
            //    while (sdr.Read())
            //    {
            //        arrData.Add(rf.GenerateObject(sdr));
            //    }    
            //}
            //rps.DataSource = arrData;
            //rps.DataBind();
        }
        private string col0 = string.Empty;
        private string col1 = string.Empty;
        protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = Convert.ToInt32(e.Row.RowIndex.ToString());
                if (e.Row.Cells[0].Text.Trim() == col0)
                    e.Row.Cells[0].Text = "";
                else
                    col0 = e.Row.Cells[0].Text.Trim();
                if (e.Row.Cells[1].Text.Trim() == col1)
                    e.Row.Cells[1].Text = "";
                else
                    col1 = e.Row.Cells[1].Text.Trim();
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //LoadDataDept();
            LoadListPakai();
        }
        //protected void lstType_DataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Users users = (Users)Session["Users"];
        //    RP_Dept Dept = (RP_Dept)e.Item.DataItem;
        //    Repeater rps = (Repeater)e.Item.FindControl("lstPrs");
        //    LoadListGroup(Dept.ID, rps);
        //}
        //protected void lstPrs_DataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Users users = (Users)Session["Users"];
        //    RP_Group Grup = (RP_Group)e.Item.DataItem;
        //    Repeater rps = (Repeater)e.Item.FindControl("lstDetail");
        //    LoadListPakai(Grup.ID, Grup.DeptID, rps);
        //}

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Rekap_Pemakaian_" + DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd") +
                "sd" + DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd") + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            //GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            //{
            //    GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}
            Panel1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}