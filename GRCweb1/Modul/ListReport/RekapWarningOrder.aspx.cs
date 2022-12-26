using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;
/** for export to exec **/
using System.IO;
using System.Data;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapWarningOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Global.link = "~/Default.aspx";
                fromDate.ReadOnly = false;
                fromDate.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                Print.Visible = false;
                ExportToExcel.Visible = false;
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(ExportToExcel);

        }
        protected void Preview_Click(object sender, EventArgs e)
        {
            string Hari = DateTime.Now.Day.ToString();
            string Bulan1 = DateTime.Now.Month.ToString();
            int Bulan2 = Convert.ToInt32(Bulan1);
            string Tahun1 = DateTime.Now.Year.ToString();

            if (Bulan2 < 10)
            { string PBulan = "0" + Bulan2; Session["PBulan"] = PBulan; }
            else { string PBulan = Bulan2.ToString(); Session["PBulan"] = PBulan; }

            string PBulan3 = Session["PBulan"].ToString();
            string AwalPeriode = Tahun1 + PBulan3 + "01";
            string AkhirPeriode = Tahun1 + PBulan3 + Hari;

            Session["PeriodeAwal"] = AwalPeriode;
            Session["PeriodeTahun"] = Tahun1;
            Session["PeriodeBulan"] = Bulan1;
            Session["PeriodeAkhir"] = AkhirPeriode;

            string SA1 = Session["PeriodeBulan"].ToString();

            if (SA1 != "0")
            {
                if (SA1 == "1") { string SA = "DesQty"; Session["SA"] = SA; }
                else if (SA1 == "2") { string SA = "JanQty"; Session["SA"] = SA; }
                else if (SA1 == "3") { string SA = "FebQty"; Session["SA"] = SA; }
                else if (SA1 == "4") { string SA = "MarQty"; Session["SA"] = SA; }
                else if (SA1 == "5") { string SA = "AprQty"; Session["SA"] = SA; }
                else if (SA1 == "6") { string SA = "MeiQty"; Session["SA"] = SA; }
                else if (SA1 == "7") { string SA = "JunQty"; Session["SA"] = SA; }
                else if (SA1 == "8") { string SA = "JulQty"; Session["SA"] = SA; }
                else if (SA1 == "9") { string SA = "AguQty"; Session["SA"] = SA; }
                else if (SA1 == "10") { string SA = "SepQty"; Session["SA"] = SA; }
                else if (SA1 == "11") { string SA = "OktQty"; Session["SA"] = SA; }
                else if (SA1 == "12") { string SA = "NovQty"; Session["SA"] = SA; }
            }

            string PeriodeAkhir = Session["PeriodeAkhir"].ToString();
            string PeriodeAwal = Session["PeriodeAwal"].ToString();
            string SaldoAwal = Session["SA"].ToString();
            //string Tahun = Session["PeriodeTahun"].ToString();

            if (SaldoAwal == "DesQty")
            {
                int Thn1 = Convert.ToInt32(Tahun1) - 1;
                string Thn2 = Thn1.ToString(); Session["Tahun4"] = Thn2;
            }
            else { string Thn3 = Tahun1; Session["Tahun4"] = Thn3; }

            string Tahun = Session["Tahun4"].ToString();
            string periode = fromDate.Text.ToString();
            string spGroup = ddlGroupID.SelectedItem.ToString();
            string GroupID = ddlGroupID.SelectedValue.ToString();
            string strSQL = GetQuery(GroupID, SaldoAwal, Tahun, PeriodeAwal, PeriodeAkhir);

            Session["Query"] = strSQL;
            Session["Periode"] = periode;
            Session["spGroup"] = spGroup;
            Cetak(this);
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=lapWarnOrder', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void Print_Click(object sender, EventArgs e)
        {
            string Hari = DateTime.Now.Day.ToString();
            string Bulan1 = DateTime.Now.Month.ToString();
            int Bulan2 = Convert.ToInt32(Bulan1);
            string Tahun11 = DateTime.Now.Year.ToString();

            if (Bulan2 < 10)
            { string PBulan = "0" + Bulan2; Session["PBulan"] = PBulan; }
            else { string PBulan = Bulan2.ToString(); Session["PBulan"] = PBulan; }

            string PBulan3 = Session["PBulan"].ToString();
            string AwalPeriode = Tahun11 + PBulan3 + "01";
            string AkhirPeriode = Tahun11 + PBulan3 + Hari;

            Session["PeriodeAwal"] = AwalPeriode;
            Session["PeriodeTahun"] = Tahun11;
            Session["PeriodeBulan"] = Bulan1;
            Session["PeriodeAkhir"] = AkhirPeriode;

            string SA1 = Session["PeriodeBulan"].ToString();

            if (SA1 != "0")
            {
                if (SA1 == "1") { string SA = "DesQty"; Session["SA"] = SA; }
                else if (SA1 == "2") { string SA = "JanQty"; Session["SA"] = SA; }
                else if (SA1 == "3") { string SA = "FebQty"; Session["SA"] = SA; }
                else if (SA1 == "4") { string SA = "MarQty"; Session["SA"] = SA; }
                else if (SA1 == "5") { string SA = "AprQty"; Session["SA"] = SA; }
                else if (SA1 == "6") { string SA = "MeiQty"; Session["SA"] = SA; }
                else if (SA1 == "7") { string SA = "JunQty"; Session["SA"] = SA; }
                else if (SA1 == "8") { string SA = "JulQty"; Session["SA"] = SA; }
                else if (SA1 == "9") { string SA = "AguQty"; Session["SA"] = SA; }
                else if (SA1 == "10") { string SA = "SepQty"; Session["SA"] = SA; }
                else if (SA1 == "11") { string SA = "OktQty"; Session["SA"] = SA; }
                else if (SA1 == "12") { string SA = "NovQty"; Session["SA"] = SA; }
            }

            string PeriodeAkhir = Session["PeriodeAkhir"].ToString();
            string Tahun1 = Session["PeriodeTahun"].ToString();
            string PeriodeAwal = Session["PeriodeAwal"].ToString();
            string SaldoAwal = Session["SA"].ToString();
            string ROItem = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)Session["Users"]).UnitKerjaID.ToString());
            string RemarksPO = (ROItem == string.Empty) ? string.Empty : ",case when sp.Status =2 and sp.ItemID IN(" + ROItem + ") then " +
                             "(select Delivery From POPurchn where POPurchn.ID=(Select top 1 POID from POPurchnDetail p " +
                             "where ItemID=i.ID and p.Status=0  ))  else '' end POd";
            string strSQL = GetQuery(ddlGroupID.SelectedValue.ToString(), SaldoAwal, Tahun1, PeriodeAwal, PeriodeAkhir);
            int add1 = strSQL.IndexOf("OtPO") + 4;
            int add2 = strSQL.IndexOf("end PO") + 51;
            //strSQL = strSQL.Insert(add1, ",POd");
            //strSQL = strSQL.Insert(add2, RemarksPO);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            string strError = dataAccess.Error;
            ArrayList arrData = new ArrayList();
            if (strError == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(new RekapOrder
                    {
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        UOMName = sqlDataReader["UOMName"].ToString(),
                        MinStock = Convert.ToDecimal(sqlDataReader["MinStock"].ToString()),
                        ReOrder = Convert.ToDecimal(sqlDataReader["ReOrder"].ToString()),
                        Stock = Convert.ToDecimal(sqlDataReader["Jumlah"].ToString()),
                        OtSPP = Convert.ToDecimal(sqlDataReader["OtSPP"].ToString()),
                        OtPO = Convert.ToDecimal(sqlDataReader["OtPO"].ToString()),
                        //Remark=sqlDataReader["POd"].ToString()
                    });
                }

            }
            lstRekapWn.DataSource = arrData;
            lstRekapWn.DataBind();
            ExportToExcel.Visible = (ddlGroupID.SelectedIndex == 0) ? true : false;
        }
        private string GetQuery(string GroupID, string SA, string Tahun, string PeriodeAwal, string PeriodeAkhir)
        {
            #region remark
            //return "select ROW_NUMBER() OVER(ORDER BY Itemname) as ID,ItemCode,ItemName,(Select UOMDesc from UOM where UOM.ID=w.UOMID) AS UOMName," +
            //                "MinStock,ReOrder,Jumlah,Ord as OtSPP, (PO-ISNULL(Recep,0)) as OtPO from ( " +
            //                "select ROW_NUMBER() OVER(PARTITION BY ID ORDER BY ID) as Nom,* FROM( " +
            //                "select i.ID,ItemCode,ItemName, i.Jumlah,i.UOMID, minstock,maxstock,ReOrder, " +
            //                //"case sp.Status when 2 then  " +
            //                " isnull((select sum (p.Qty) from POPurchnDetail p where ItemID=i.ID and p.Status=0 GROUP by ItemID,status),0)  PO, "+
            //                "(select SUM((Quantity-QtyPO)) FROM SPPDetail where ItemID=i.ID AND ItemTypeID=i.ItemTypeID AND Status>-1)Ord,  " +
            //                "(select SUM(Quantity) from ReceiptDetail where PODetailID in("+
            //                "select ID from POPurchnDetail p where ItemID="+GetConvertMaterial("i.ID")+
            //                " and p.Status=0)) as Recep " +
            //                "from Inventory  as i " +
            //                "/*inner join SPPDetail as sp " +
            //                "on sp.ItemID=i.ID " +
            //                "inner join SPP as s " +
            //                "on s.ID=sp.SPPID */" +
            //                "where i.Jumlah < i.ReOrder and /*s.Status>-1 and*/ i.Aktif=1 /*and s.Approval>2*/ and i.GroupID in(" + GroupID + ") " +
            //                ") as x " +
            //                ") as w " +
            //                "/*where w.Nom=1 */" +
            //                "order by w.ItemName";

            //return "select ROW_NUMBER() OVER(ORDER BY Itemname) as ID,ItemCode,ItemName,(Select UOMDesc from UOM where UOM.ID=w.UOMID) AS UOMName," +
            //                " MinStock,MaxStock,ReOrder," +
            //                " ISNULL((StockAwal+StockAkhir),0)Jumlah,"+
            //                " Ord as OtSPP, (PO-ISNULL(Recep,0)) as OtPO from ( " +
            //                " select ROW_NUMBER() OVER(PARTITION BY ID ORDER BY ID) as Nom,* FROM( " +                       
            //                " select i.ID,ItemCode,ItemName, " +
            //                " (select "+SA+" from SaldoInventory as sa where sa.ItemID=i.ID and sa.ItemTypeID=i.ItemTypeID and sa.GroupID=i.GroupID and YearPeriod="+Tahun+")StockAwal,"+
            //                " (select SUM(quantity)Jumlah2 from vw_StockPurchn as vw where vw.ItemID=i.ID and vw.ItemTypeID=i.ItemTypeID "+
            //                " and vw.GroupID=i.GroupID and LEFT(convert(char,tanggal,112),8)>='" + PeriodeAwal + "' and " +
            //                " LEFT(convert(char,tanggal,112),8)<='" + PeriodeAkhir + "' group by vw.ItemID)StockAkhir," +
            //                "i.UOMID, minstock,maxstock,ReOrder, " +            
            //                " isnull((select sum (p.Qty) from POPurchnDetail p where ItemID=i.ID and p.Status=0 and p.POID in (select ID from POPurchn where Status>-1) GROUP by ItemID,status),0)  PO, " +
            //                "(select SUM((Quantity-QtyPO)) FROM SPPDetail where ItemID=i.ID AND ItemTypeID=i.ItemTypeID AND Status>-1)Ord,  " +
            //                "(select SUM(Quantity) from ReceiptDetail where PODetailID in(" +
            //                "select ID from POPurchnDetail p where ItemID=" + GetConvertMaterial("i.ID") +
            //                " and p.Status=0)) as Recep " +
            //                "from Inventory  as i " +
            //                "/*inner join SPPDetail as sp " +
            //                "on sp.ItemID=i.ID " +
            //                "inner join SPP as s " +
            //                "on s.ID=sp.SPPID */" +
            //                "where  i.Aktif=1 and i.stock=1 and i.GroupID in(" + GroupID + ") " +
            //                ") as x " +
            //                ") as w where ((StockAwal+StockAkhir)<w.ReOrder or ((StockAwal+StockAkhir=0 and w.ReOrder=0)))" +
            //                "/*where w.Nom=1 */" +
            //                "order by w.ItemName";
            #endregion
            return " select ROW_NUMBER() OVER(ORDER BY Itemname) as ID,* from (select ItemCode,ItemName,UOMName,MinStock,MaxStock," +
                   " (StockAwal+StockAkhir)Jumlah,ReOrder,OtSPP,OtPO from (select ItemCode,ItemName,(Select UOMDesc from UOM where " +
                   " UOM.ID=w.UOMID) AS UOMName, MinStock,MaxStock,ReOrder,ISNULL(StockAwal,0)StockAwal,ISNULL(StockAkhir,0)StockAkhir, " +

                   " ISNULL(Ord,0) as OtSPP," +

                   "(PO-ISNULL(Recep,0)) as OtPO from (  select ROW_NUMBER() OVER(PARTITION BY ID ORDER BY ID) as Nom,* " +
                   " FROM(  select i.ID,ItemCode,ItemName, " +

                   "(select " + SA + " from SaldoInventory as sa where sa.ItemID=i.ID and " +
                   " sa.ItemTypeID=i.ItemTypeID and sa.GroupID=i.GroupID and YearPeriod=" + Tahun + ")StockAwal, " +

                   "(select ISNULL(SUM(quantity),0)Jumlah2 from vw_StockPurchn as vw where vw.ItemID=i.ID and vw.ItemTypeID=i.ItemTypeID  " +

                   " and vw.GroupID=i.GroupID and LEFT(convert(char,tanggal,112),8)>='" + PeriodeAwal + "' and  LEFT(convert(char,tanggal,112),8)<='" + PeriodeAkhir + "' " +
                   " group by vw.ItemID)StockAkhir, " +

                   " i.UOMID, minstock,maxstock,ReOrder, " +

                   " isnull((select sum (p.Qty) from POPurchnDetail p where ItemID=i.ID and p.Status=0 " +

                   " and p.POID in (select ID from POPurchn where Status>-1) GROUP by ItemID,status),0)  PO, " +

                   " (select SUM((Quantity-QtyPO)) FROM SPPDetail where ItemID=i.ID AND ItemTypeID=i.ItemTypeID AND Status>-1)Ord," +

                   " (select SUM(Quantity) from ReceiptDetail where PODetailID in(select ID from POPurchnDetail p where ItemID=" + GetConvertMaterial("i.ID") +
                   " and p.Status=0)) as Recep " +

                   " from Inventory  as i " +
                   " where  i.Aktif=1 and i.stock=1 and i.GroupID in (" + GroupID + ") ) as x ) as w  ) as Dataa ) as Data2 where Jumlah<=ReOrder or " +
                   " (Jumlah = 0 and ReOrder = 0)  order by ItemName ";
        }
        private string GetConfig(string Key, string Section)
        {
            var conf = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini"));
            return conf.Read(Key, Section);
        }
        private string GetConvertMaterial(string ItemID)
        {
            string Item = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(ItemID, "ReceiptConvert" + ((Users)Session["Users"]).UnitKerjaID.ToString());
            return (Item == string.Empty) ? "i.ID" : Item;
        }
        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapWarningOrder.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            lstRekapWn.RenderControl(hw);
            Response.Write("<table border='1'>");
            Response.Write(sw.ToString());
            Response.Write("</table>");
            Response.Flush();
            Response.End();

        }
        protected void ddlGroupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Print.Visible = (ddlGroupID.SelectedIndex == 0) ? true : false;
            // ExportToExcel.Visible = (ddlGroupID.SelectedIndex == 0) ? true : false;
            Preview.Visible = (ddlGroupID.SelectedIndex != 0) ? true : false;
        }
    }
}