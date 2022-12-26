using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class RepostingFacade : AbstractFacade
    {
        private Reposting objReposting= new Reposting();
        private ArrayList arrReposting;
        private List<SqlParameter> sqlListParam;

        public RepostingFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public ArrayList RetrieveReposting(int groupID,string YM)
        {
            string strSQL = "select ItemTypeID, GroupID, tahun, bulan, tanggal, YMD, YM, ItemID, quantity, ID, process from vw_StockPurchn where /*" +
                "isnull(avgprice,0)=0 and*/ YM='" + YM + "' and GroupID=" + groupID + " and process='ReceiptDetail'  order by itemid,tanggal ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReposting = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReposting.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReposting.Add(new Reposting());

            return arrReposting;
        }
        /**
         * Cari ItemID yang bergerak di bulan posting
         * Untuk Posting Average Price nya
         */
        public string ItemWithTrans(int GroupID, string YM,int ItemTypeID)
        {
            string result = string.Empty;
            string strSQL = "select Distinct ItemID from vw_StockPurchn where YM='" + YM + "' and GroupID=" + GroupID + " and ItemtypeID=" + ItemTypeID ;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result += sdr["ItemID"].ToString() + ",";
                }
            }
            return (result.Length > 1) ? result.Substring(0, (result.Length - 1)) : "0";
        }
        /**
         * Cari ItemID yang tidak bergerak di bulan posting
         * berfungsi untuk update langsung dari bln sebelumnya
         */
        public ArrayList RepostingItemID(int groupID, string YM, string ItemID,bool NotIn)
        {
            string strSQL = "select ItemTypeID,itemid from SaldoInventory " +
                            "where YearPeriod=" + YM.Substring(0, 4) + " and GroupID=" + groupID +
                            " and ItemID in(select ID from Inventory where GroupID=" + groupID + " and aktif=1 and len(Itemcode)=15 " +
                            " and ItemID in(" + ItemID + ")) order by itemid ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            arrReposting = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReposting.Add(new Reposting
                    {
                        ItemID = Convert.ToInt32(sqlDataReader["ItemID"].ToString()),
                        ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString())
                    });
                }
            }
            return arrReposting;
        }
        public ArrayList RepostingItemID(int groupID, string YM, string ItemID,string Field)
        {
            string WithoutZero = (groupID == 9) ? " and (" + Field + " >0 or " + Field.Replace("Qty", "AvgPrice") + ">0" : "";
            string strSQL = " select ItemTypeID,itemid  from (select * from SaldoInventory " +
                            " where YearPeriod=" + YM.Substring(0, 4) + " and GroupID=" + groupID +
                            " and ItemID in(select ID from Inventory where GroupID=" + groupID + " and aktif=1 and len(Itemcode)=15 )" +
                            " ) as x where  x.ItemID Not in(" + ItemID + ") " + WithoutZero + " order by x.itemid ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            arrReposting = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReposting.Add(new Reposting
                    {
                        ItemID = Convert.ToInt32(sqlDataReader["ItemID"].ToString()),
                        ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString())
                    });
                }
            }
            return arrReposting;
        }
        public ArrayList RepostingItemID(int groupID, string YM,string ItemID)
        {
            string WithoutZero = (groupID == 9) ? " and " : "";
            string strSQL = " select * from (select ItemTypeID,itemid from SaldoInventory " +
                            " where YearPeriod=" + YM.Substring(0, 4) + " and GroupID=" + groupID +
                            " and ItemID in(select ID from Inventory where GroupID="+groupID+" and aktif=1 and len(Itemcode)=15 )"+
                            " ) as x where  x.ItemID Not in(" + ItemID + ") order by x.itemid ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            arrReposting = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReposting.Add(new Reposting
                    {
                        ItemID = Convert.ToInt32(sqlDataReader["ItemID"].ToString()),
                        ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString())
                    });
                }
            }
            return arrReposting;
        }
        public ArrayList RepostingItemID(int groupID, string YM)
        {
            string strSQL = "select ItemID,ItemTypeID from (select ItemTypeID, ItemID from vw_StockPurchn where /*" +
                            "isnull(avgprice,0)=0 and*/ YM='" + YM + "' and GroupID=" + groupID + /*and process='ReceiptDetail'*/
                            " union all select ItemTypeID,itemid from SaldoInventory " +
                            "where YearPeriod=" + YM.Substring(0, 4) + " and GroupID=" + groupID +
                            " and ItemID in(Select ID from Inventory where aktif=1 and groupid=" + groupID + ")" +
                            ") as x group by ItemID,ItemTypeID  order by itemid ";
            //string strSQL = "select ItemID,ItemTypeID from (select ItemTypeID,itemid from SaldoInventory " +
            //               "where YearPeriod=" + YM.Substring(0, 4) + " and GroupID=" + groupID +
            //               " and ItemID in(34792)" +
            //               ") as x group by ItemID,ItemTypeID  order by itemid ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            arrReposting = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReposting.Add(new Reposting
                    {
                        ItemID = Convert.ToInt32(sqlDataReader["ItemID"].ToString()),
                        ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString())
                    });
                }
            }
            return arrReposting;
        }

        public decimal GetPrice(string tanggal, int itemtypeid, int groupid, int itemid)
        {
            decimal price = 0;
            string strSQL = "exec spListAveragePriceByID '" + tanggal + "'," + itemtypeid + "," + groupid + "," + itemid + ",1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReposting = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    price = Convert.ToDecimal(sqlDataReader["LastPrice"]);
                }
            }
            
            return price;
        }
        public decimal GetPriceForReceipt(string Bln,string Tahun,int ID,int ItemTypeID)
        {
            decimal price = 0;
            string strSQL = string.Empty;
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           #region Old Query diganti dengan query average price accounting report
            /*"select ID,ItemTypeID,GroupID,tahun,bulan,tanggal,YMD,YM,Crc,ItemID,ItemID,Quantity,case when Price =0 then (select isnull(sum(hargasatuan),0) from tabelhargabankout where receiptdetailID=P.ID ) else price end Price  from ( " +
                "select B.id,B.ItemTypeID,B.GroupID,YEAR(A.ReceiptDate) as tahun,month(A.ReceiptDate) as bulan, A.ReceiptDate as tanggal, "+
                "CONVERT(varchar,A.ReceiptDate,112) as YMD,left(CONVERT(varchar,A.ReceiptDate,112),6) as YM,PO.Crc, B.ItemID,sum(B.Quantity) as Quantity, "+
                "case when PO.Crc >1 then isnull((select top 1 kurs from MataUangKurs where muid=PO.crc and RowStatus>=0 and drTgl <=A.ReceiptDate   " +
                "and sdTgl >=A.ReceiptDate ),1) * avg(POD.Price- (POD.Price * (isnull(PO.Disc,0)/100))) "+
                "else (avg(POD.Price-(POD.Price * (isnull(PO.Disc,0)/100)))) end Price "+
                "from Receipt A INNER JOIN ReceiptDetail B ON A.ID = B.ReceiptID and B.RowStatus > -1 INNER JOIN   "+
                "POPurchn PO ON A.POID = PO.ID and B.RowStatus>-1 INNER JOIN SuppPurch S ON PO.SupplierID = S.ID INNER JOIN MataUang MU ON PO.Crc = MU.ID INNER JOIN   "+
                "UOM ON B.UomID = UOM.ID INNER JOIN GroupsPurchn G ON B.GroupID = G.ID  inner join POPurchnDetail POD on PO.ID=POD.POID and B.PODetailID=POD.ID  "+
                " group by B.ID,B.ItemTypeID,B.GroupID,A.ReceiptDate,B.ItemID,PO.Crc  ) as P where ID=" + ID;*/
            #endregion
            ReportFacadeAcc rfa = new ReportFacadeAcc();
            rfa.MaterialGroup = ItemTypeID.ToString();
            string strSQL2 = rfa.ViewMutasiStockByName(Bln, Tahun, ID.ToString());
            int idx = strSQL2.LastIndexOf("/** generate final report */");
            int idx2 = strSQL2.LastIndexOf("/** Delete temporary table */");
            strSQL = strSQL2.Insert(idx, "/*");
            strSQL = strSQL.Insert(idx2, "*/ select case when hs=0 then case when prodhs=0 then isnull(belihs,0) else isnull(prodhs,0) end else isnull(hs,0) end as Price from zd_" + created + "_mutasisaldo");
            strSQL = (ItemTypeID > 1) ? strSQL.Replace("si.ItemTypeID='1'", "si.ItemTypeID=" + ItemTypeID) : strSQL;
            strSQL = strSQL.Replace("SELECT ID FROM Inventory WHERE  ID=", "");
            strSQL = (ItemTypeID > 1) ? strSQL.Replace("pod.ItemTypeID='1'", "pod.ItemTypeID=" + ItemTypeID) : strSQL;
            switch (ItemTypeID)
            {
                case 2: strSQL = strSQL.Replace("SELECT ID FROM Inventory", "SELECT ID FROM Asset"); break;
                case 3: strSQL = strSQL.Replace("SELECT ID FROM Inventory", "SELECT ID FROM Biaya"); break;
            }
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrReposting = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        price = Convert.ToDecimal(sqlDataReader["Price"]);
                    }
                }
                if (price > 0)
                {
                    return price;
                }
                else
                {
                    return GetPriceFromPrevious(Bln, Tahun, ID, ItemTypeID);
                }
            
        }
        public decimal GetPriceForReceipt(string Bln, string Tahun, int ID, int ItemTypeID,bool FromPosting)
        {
            decimal price = 0;
            string strSQL = string.Empty;
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            #region Old Query diganti dengan query average price accounting report
            /*"select ID,ItemTypeID,GroupID,tahun,bulan,tanggal,YMD,YM,Crc,ItemID,ItemID,Quantity,case when Price =0 then (select isnull(sum(hargasatuan),0) from tabelhargabankout where receiptdetailID=P.ID ) else price end Price  from ( " +
                "select B.id,B.ItemTypeID,B.GroupID,YEAR(A.ReceiptDate) as tahun,month(A.ReceiptDate) as bulan, A.ReceiptDate as tanggal, "+
                "CONVERT(varchar,A.ReceiptDate,112) as YMD,left(CONVERT(varchar,A.ReceiptDate,112),6) as YM,PO.Crc, B.ItemID,sum(B.Quantity) as Quantity, "+
                "case when PO.Crc >1 then isnull((select top 1 kurs from MataUangKurs where muid=PO.crc and RowStatus>=0 and drTgl <=A.ReceiptDate   " +
                "and sdTgl >=A.ReceiptDate ),1) * avg(POD.Price- (POD.Price * (isnull(PO.Disc,0)/100))) "+
                "else (avg(POD.Price-(POD.Price * (isnull(PO.Disc,0)/100)))) end Price "+
                "from Receipt A INNER JOIN ReceiptDetail B ON A.ID = B.ReceiptID and B.RowStatus > -1 INNER JOIN   "+
                "POPurchn PO ON A.POID = PO.ID and B.RowStatus>-1 INNER JOIN SuppPurch S ON PO.SupplierID = S.ID INNER JOIN MataUang MU ON PO.Crc = MU.ID INNER JOIN   "+
                "UOM ON B.UomID = UOM.ID INNER JOIN GroupsPurchn G ON B.GroupID = G.ID  inner join POPurchnDetail POD on PO.ID=POD.POID and B.PODetailID=POD.ID  "+
                " group by B.ID,B.ItemTypeID,B.GroupID,A.ReceiptDate,B.ItemID,PO.Crc  ) as P where ID=" + ID;*/
            #endregion
            ReportFacadeAcc rfa = new ReportFacadeAcc();
            rfa.FromProsesPosting = (FromPosting == true) ? "yes" : string.Empty;
            rfa.MaterialGroup = ItemTypeID.ToString();
            string strSQL2 = rfa.ViewMutasiStockByName(Bln, Tahun, ID.ToString());
            int idx = strSQL2.LastIndexOf("/** generate final report */");
            int idx2 = strSQL2.LastIndexOf("/** Delete temporary table */");
            strSQL = strSQL2.Insert(idx, "/*");
            strSQL = strSQL.Insert(idx2, "*/ select case when hs=0 then case when prodhs=0 then isnull(belihs,0) else isnull(prodhs,0) end else isnull(hs,0) end as Price from zd_" + created + "_mutasisaldo");
            strSQL = (ItemTypeID > 1) ? strSQL.Replace("si.ItemTypeID='1'", "si.ItemTypeID=" + ItemTypeID) : strSQL;
            strSQL = strSQL.Replace("SELECT ID FROM Inventory WHERE /*GroupID IN('1','2') AND*/ ID=", "");
            strSQL = (ItemTypeID > 1) ? strSQL.Replace("pod.ItemTypeID='1'", "pod.ItemTypeID=" + ItemTypeID) : strSQL;
            switch (ItemTypeID)
            {
                case 2: strSQL = strSQL.Replace("SELECT ID FROM Inventory", "SELECT ID FROM Asset"); break;
                case 3: strSQL = strSQL.Replace("SELECT ID FROM Inventory", "SELECT ID FROM Biaya"); break;
            }
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReposting = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    price = Convert.ToDecimal(sqlDataReader["Price"]);
                }
            }
            if (price > 0)
            {
                return price;
            }
            else
            {
                return GetPriceFromPrevious(Bln, Tahun, ID, ItemTypeID);
            }

        }
        public decimal GetPriceFromPrevious(string Bulan, string Tahun, int ItemID,int itemTypeID)
        {
            string field=string.Empty;
            string prdTahun=string.Empty;
            decimal price = 0;
            int fldBln = int.Parse(Bulan);
            switch (fldBln)
            {
                case 1: field = "DesAvgPrice"; prdTahun = (int.Parse(Tahun) - 1).ToString(); break;
                case 2: field = "JanAvgPrice"; prdTahun = Tahun; break;
                case 3: field = "FebAvgPrice"; prdTahun = Tahun; break;
                case 4: field = "MarAvgPrice"; prdTahun = Tahun; break;
                case 5: field = "AprAvgPrice"; prdTahun = Tahun; break;
                case 6: field = "MeiAvgPrice"; prdTahun = Tahun; break;
                case 7: field = "JunAvgPrice"; prdTahun = Tahun; break;
                case 8: field = "JulAvgPrice"; prdTahun = Tahun; break;
                case 9: field = "AguAvgPrice"; prdTahun = Tahun; break;
                case 10: field = "SepAvgPrice"; prdTahun = Tahun; break;
                case 11: field = "OktAvgPrice"; prdTahun = Tahun; break;
                case 12: field = "NovAvgPrice"; prdTahun = Tahun; break;

            }
            string strSQL = "Select " + field + " from SaldoInventory Where YearPeriod=" + prdTahun + " and ItemID=" + ItemID + " and ItemTypeID="+itemTypeID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    price= Convert.ToDecimal(sqlDataReader[field].ToString());
                }
            }
            
            return price;
        }
        public string ResetPrice(int groupid,string YM)
        {
            string strSQL = "update PakaiDetail set AvgPrice=0 where groupid=" + groupid  + " and PakaiID in(select ID from Pakai where left(CONVERT(varchar,PakaiDate,112),6)='" + YM + "') " +
                "update receiptDetail  set AvgPrice=0 where  groupid=" + groupid + " and receiptID  in(select ID from receipt where left(CONVERT(varchar,receiptDate,112),6)='" + YM + "') " +
                "update AdjustDetail  set AvgPrice=0 where  groupid=" + groupid + " and AdjustID  in(select ID from adjust where left(CONVERT(varchar,AdjustDate,112),6)='" + YM + "') " +
                "update ReturPakaiDetail set AvgPrice=0 where  groupid=" + groupid + " and ReturID  in(select ID from ReturPakai where left(CONVERT(varchar,ReturDate,112),6)='" + YM + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string PostingPrice(int ID,string TableName,decimal price)
        {
            string strprice = price.ToString().Replace(",",".");
            string strSQL=string.Empty;
            if (TableName.Trim() == "ReceiptDetail")
            {
                strSQL = "update " + TableName + " set avgprice=" + strprice + " where ID=" + ID;
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
            }
            return strError;
        }
        public string PostingPriceSameWithPrevious(string Tahun, int GroupID, string MonthPrev, string MonthExisting, string ItemID)
        {
            string YM = string.Empty;
            YM = (MonthPrev == "DesQty") ? (int.Parse(Tahun) - 1).ToString() : Tahun;
            string result = string.Empty;
            string strSQL = "update SaldoInventory set " + MonthExisting + "= "+
                            "(Select " + MonthPrev.Replace("Qty","AvgPrice") + " from SaldoInventory as sl Where sl.ItemID=SaldoInventory.ItemID " +
                            " and YearPeriod=" + YM + ") where YearPeriod=" + Tahun + " and GroupID= " + GroupID +
                            " and ItemID in(select ID from Inventory where GroupID=" + GroupID + " and aktif=1 and len(Itemcode)=15 ) and  ItemID Not in(" +
                          ItemID + ")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            result = sqlDataReader.RecordsAffected.ToString();
            return result;
        }
        public string PostingPriceSameWithPrevious(string Tahun, int GroupID, string MonthPrev, string MonthExisting, string ItemID, string ItemTypeID)
        {
            string YM = string.Empty;
            YM = (MonthPrev == "DesQty") ? (int.Parse(Tahun) - 1).ToString() : Tahun;
            string result = string.Empty;
            string strSQL = "update SaldoInventory set " + MonthExisting + "= ( "+
                            "Select " + MonthPrev.Replace("Qty", "AvgPrice") + " from SaldoInventory as sl Where sl.ItemID=SaldoInventory.ItemID " +
                            " and YearPeriod=" + YM + " and GroupID= " + GroupID +
                            " and ItemTypeID=" + ItemTypeID + ") where YearPeriod=" + Tahun + " and GroupID= " + GroupID +
                            " and ItemTypeID=" + ItemTypeID + " and  ItemID Not in(" + ItemID + ")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            result = sqlDataReader.RecordsAffected.ToString();
            return result;
        }
        public string PostingSaldoPrice(string tahun, int groupid, decimal price, string awal,int itemid)
        {
            string strSQL = string.Empty;
            string strprice = price.ToString().Replace(",",".");
            strSQL = "update saldoinventory set " + awal + "=" + strprice + " where itemid=" + itemid + " and groupid=" + groupid + " and yearperiod=" + tahun;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = sqlDataReader.RecordsAffected.ToString();
            return strError;
        }
        public Reposting GenerateObject(SqlDataReader sqlDataReader)
        {
            objReposting = new Reposting();
            objReposting.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objReposting.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objReposting.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);
            objReposting.Bulan = Convert.ToInt32(sqlDataReader["Bulan"]);
            objReposting.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            objReposting.YMD = sqlDataReader["YMD"].ToString();
            objReposting.YM = sqlDataReader["YM"].ToString();
            objReposting.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objReposting.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReposting.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReposting.Process = sqlDataReader["Process"].ToString();
            return objReposting;
        }
    }
}
