using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class SaldoInventoryFacade : AbstractFacade
    {
        private SaldoInventory objSaldoInventory = new SaldoInventory();
        private ArrayList arrSaldoInventory;
        private List<SqlParameter> sqlListParam;

        public SaldoInventoryFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSaldoInventory = (SaldoInventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventory.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventory.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventory.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventory.ItemTypeID));

                //sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventory.Quantity));
                //sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventory.Posting));
                //sqlListParam.Add(new SqlParameter("@JanQty", objSaldoInventory.JanQty));
                //sqlListParam.Add(new SqlParameter("@JanAvgPrice", objSaldoInventory.JanAvgPrice));
                //sqlListParam.Add(new SqlParameter("@FebQty", objSaldoInventory.FebQty));
                //sqlListParam.Add(new SqlParameter("@FebAvgPrice", objSaldoInventory.FebAvgPrice));
                //sqlListParam.Add(new SqlParameter("@MarQty", objSaldoInventory.MarQty));
                //sqlListParam.Add(new SqlParameter("@MarAvgPrice", objSaldoInventory.MarAvgPrice));
                //sqlListParam.Add(new SqlParameter("@AprQty", objSaldoInventory.AprQty));
                //sqlListParam.Add(new SqlParameter("@AprAvgPrice", objSaldoInventory.AprAvgPrice));
                //sqlListParam.Add(new SqlParameter("@MeiQty", objSaldoInventory.MeiQty));
                //sqlListParam.Add(new SqlParameter("@MeiAvgPrice", objSaldoInventory.MeiAvgPrice));
                //sqlListParam.Add(new SqlParameter("@JunQty", objSaldoInventory.JunQty));
                //sqlListParam.Add(new SqlParameter("@JunAvgPrice", objSaldoInventory.JunAvgPrice));
                //sqlListParam.Add(new SqlParameter("@JulQty", objSaldoInventory.JulQty));
                //sqlListParam.Add(new SqlParameter("@JulAvgPrice", objSaldoInventory.JulAvgPrice));
                //sqlListParam.Add(new SqlParameter("@AguQty", objSaldoInventory.AguQty));
                //sqlListParam.Add(new SqlParameter("@AguAvgPrice", objSaldoInventory.AguAvgPrice));
                //sqlListParam.Add(new SqlParameter("@SepQty", objSaldoInventory.SepQty));
                //sqlListParam.Add(new SqlParameter("@SepAvgPrice", objSaldoInventory.SepAvgPrice));
                //sqlListParam.Add(new SqlParameter("@OktQty", objSaldoInventory.OktQty));
                //sqlListParam.Add(new SqlParameter("@OktAvgPrice", objSaldoInventory.OktAvgPrice));
                //sqlListParam.Add(new SqlParameter("@NovQty", objSaldoInventory.NovQty));
                //sqlListParam.Add(new SqlParameter("@NovAvgPrice", objSaldoInventory.NovAvgPrice));
                //sqlListParam.Add(new SqlParameter("@DesQty", objSaldoInventory.DesQty));
                //sqlListParam.Add(new SqlParameter("@DesAvgPrice", objSaldoInventory.DesAvgPrice));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSaldoInventory");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(object objDomain)
        {
            try
            {
                
                    objSaldoInventory = (SaldoInventory)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventory.ItemID));
                    sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventory.YearPeriod));
                    sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventory.GroupID));
                    sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventory.ItemTypeID));
                    sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventory.MonthPeriod));
                    sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventory.Quantity));
                    sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventory.Posting));
                    sqlListParam.Add(new SqlParameter("@AvgPrice", objSaldoInventory.SaldoPrice));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoInventory");
                    strError = dataAccess.Error;
                
                return intResult;
                
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Delete(object objDomain)
        {

            return -1;
        }
        public int KosongkanSaldo(object objDomain)
        {

            try
            {
                objSaldoInventory = (SaldoInventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventory.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventory.YearPeriod));

                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventory.GroupID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spKosongkanSaldoInventory");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int MinusSaldo(object objDomain)
        {

            try
            {
                objSaldoInventory = (SaldoInventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventory.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventory.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventory.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventory.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventory.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventory.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventory.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusSaldoInventory");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateSaldoAvgPriceBlnIni(object objDomain)
        {

            try
            {
                objSaldoInventory = (SaldoInventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventory.ItemID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventory.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventory.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSaldoInventory.AvgPrice));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventory.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventory.YearPeriod));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoAvgPriceBlnIni");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateSaldoBlnLalu(object objDomain)
        {

            try
            {
                objSaldoInventory = (SaldoInventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventory.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventory.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventory.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventory.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventory.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventory.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventory.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoBlnLalu");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateSaldoNull(int tahun, int bulan)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@YearPeriod", tahun));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", bulan));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoNull");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventory");
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSaldoInventory.Add(new SaldoInventory());

            return arrSaldoInventory;

        }
        public SaldoInventory RetrieveByItemID(int itemID, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventory where ItemId=" + itemID + " and YearPeriod=" + thn + " and ItemTypeID="+itemtypeID);
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SaldoInventory();
        }
        public int GetPrice(int itemID, string MonthAvgPrice, int thn, int itemtypeID)
        {
            //proses aveerage price biar langsung dari proses posting average
            //yang dilakukan oleh accounting tiap minggu
            // on 01-06-2016
            string strSQL="select " + MonthAvgPrice + " from SaldoInventory where ItemId=" + itemID + 
                          " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return 0;// (Convert.ToInt32(sqlDataReader[MonthAvgPrice]) == 0) ?
                         //GetPriceFromPO(itemID, itemtypeID) :
                         //Convert.ToInt32(sqlDataReader[MonthAvgPrice]);
                }
            }

            return 0;// GetPriceFromPO(itemID, itemtypeID);
        }
        public int GetPriceFromPO(int ItemID, int ItemTypeID)
        {
            //masih blm mengakomodir mata uang selain rupiah
            string strSQL = "Select Top 1 (Price/Qty) as AvgPrice From POPurchnDetail Where ItemID="+ItemID+" and ItemTypeID="+ItemTypeID+" order by ID Desc";
            // ini yang sudah mengakomodir matauang asing tapi mengambil data kurs terakhir input
            /*
            string strSQL="select top 1 case h.crc when 1 then (price/Qty) else "+
                          "((select Top 1 Kurs from MataUangKurs where MUID=h.Crc order by MataUangKurs.ID desc )*(price/qty)) end AvgPrice "+
                          "from POPurchnDetail as p "+
                          "inner join POPurchn as h "+
                          "on h.ID=p.POID "+
                          "where p.ItemID=" + ItemID + " and p.ItemTypeID=" + ItemTypeID + " order by p.ID desc";
             */
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["AvgPrice"]);
                }
            }

            return 0;
        }
        public int CekRow(int itemID, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventory where ItemId=" + itemID + " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                return 1;
            }

            return 0;
        }
        public ArrayList RetrieveByGroupID()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventory");
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSaldoInventory.Add(new SaldoInventory());

            return arrSaldoInventory;

        }
        public ArrayList RetrieveSaldoInventory(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string strsql = string.Empty;
            if (groupID == 4)
            {
                strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) " +
                    " as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID, " +
                    "case when SALDOINVENTORY.ItemID>0 then (select ASSET.ItemCode from ASSET where ASSET.ID=SALDOINVENTORY.ItemID) else '' end ItemCode, " +
                    "case when SALDOINVENTORY.ItemID>0 then (select ASSET.ItemName from ASSET where ASSET.ID=SALDOINVENTORY.ItemID) else '' end ItemName, " +
                    "0 as UomID, " +
                    "case when SALDOINVENTORY.ItemID>0 then (select UOM.UOMCode from ASSET,UOM where ASSET.UOMID=UOM.ID and ASSET.ID=SALDOINVENTORY.ItemID)  " +
                    "else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian, " + 
                    ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID,  " + 
                    "CASE WHEN SALDOINVENTORY.ItemID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SALDOINVENTORY.ItemID " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal, " +
                    "CASE WHEN SALDOINVENTORY.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM ReturpakaiDetail WHERE ReturpakaiDetail.ItemID=SALDOINVENTORY.ItemID  " +
                    "and ReturID IN (SELECT ID FROM Returpakai WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + 
                    "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal,  " +
                    "CASE WHEN SALDOINVENTORY.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SALDOINVENTORY.ItemID  " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal, " +
                    "CASE WHEN SALDOINVENTORY.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SALDOINVENTORY.ItemID and groupid=" + groupID + " and  " +
                    "RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal, " +
                    "CASE WHEN SALDOINVENTORY.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SALDOINVENTORY.ItemID " +
                    "and PakaiDetail.groupid=" + groupID + " and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + 
                    "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from SALDOINVENTORY where  " +
                    "SALDOINVENTORY.YearPeriod=" + yearPeriod + " and SALDOINVENTORY.ITEMtypeid=2 and SALDOINVENTORY.ItemID in (select InvID from (SELECT ASSET.id as InvID,ASSET.ItemCode, ASSET.ItemName, UOM.UOMCode,ASSET.GroupID, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=ASSET.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = ASSET.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = ASSET.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM ReturpakaiDetail WHERE ReturpakaiDetail.ItemID=ASSET.ID and ReturID IN (SELECT ID FROM Returpakai WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=ASSET.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  ASSET INNER JOIN UOM ON ASSET.UOMID = UOM.ID WHERE ASSET.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
               
            }
            else
                if (groupID == 5)
                {
                strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) " + 
                    "as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID, " + 
                    "case when saldoinventory.ItemID>0 then (select biaya.ItemCode from biaya where biaya.ID=saldoinventory.ItemID) else '' end ItemCode, " + 
                    "case when saldoinventory.ItemID>0 then (select biaya.ItemName from biaya where biaya.ID=saldoinventory.ItemID) else '' end ItemName,0  " + 
                    "as UomID, " + 
                    "case when saldoinventory.ItemID>0 then (select UOM.UOMCode from biaya,UOM where biaya.UOMID=UOM.ID and biaya.ID=saldoinventory.ItemID) " + 
                    "else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian, " + 
                    ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, CASE WHEN saldoinventory.ItemID>0 THEN " + 
                    "(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=saldoinventory.ItemID and groupid=" + groupID + 
                    " and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal, " +
                    "CASE WHEN saldoinventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=saldoinventory.ItemID  " +
                    "and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + 
                    "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal,  " + 
                    "CASE WHEN saldoinventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = saldoinventory.ItemID  " +
                    "and RowStatus>-1 and groupid=" + groupID + " AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND  " + 
                    "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,  " +
                    "CASE WHEN saldoinventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = saldoinventory.ItemID and groupid=" + 
                    groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,  " +
                    "CASE WHEN saldoinventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=saldoinventory.ItemID  " + 
                    "and groupid=" + groupID + " and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from saldoinventory where   " +
                    "saldoinventory.YearPeriod=" + yearPeriod + " and saldoinventory.ItemTypeID=3 and saldoinventory.ItemID in (select InvID from   " +
                    "(SELECT biaya.id as InvID,biaya.ItemCode, biaya.ItemName, UOM.UOMCode,biaya.GroupID,   " +
                    "CASE WHEN biaya.ID > 0 THEN  (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=biaya.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt " +
                    "WHERE ID = ReceiptDetail.ReceiptID and ReceiptDetail.groupid=" + groupID + " AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + 
                    "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN biaya.ID > 0 THEN   " +
                    "(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = biaya.ID and groupid=" + groupID + "and RowStatus>-1 AND AdjustID   " +
                    "IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + 
                    "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN biaya.ID > 0 THEN   " +
                    "(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = biaya.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust   " +
                    "WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + 
                    tgl2 + "' )) END  AdjustKurang2,   " +
                    "CASE WHEN biaya.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM ReturpakaiDetail WHERE ReturpakaiDetail.ItemID=biaya.ID and ReturID IN   " +
                    "(SELECT ID FROM ReturPakai WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + 
                    "' and Status>-1 )) END  Retur2, " +
                    "CASE WHEN biaya.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=biaya.ID and PakaiDetail.groupid=" + 
                    groupID + "and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + 
                    "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  biaya INNER JOIN UOM ON biaya.UOMID =   " +
                    "UOM.ID WHERE biaya.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or   " +
                    "Retur2>0)) ) as AA1 ";
                }
                else
                {
                    strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) " +
                    "as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID, " +
                    "case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else ''  " +
                    "end ItemCode, " +
                    "case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else ''  " +
                    "end ItemName,0 as UomID, " +
                    "case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and  " +
                    "Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + 
                     ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah, " +
                    "0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID,  " +
                    "CASE WHEN SaldoInventory.ItemID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE " +
                    "ItemID=SaldoInventory.ItemID and groupid=" + groupID +  
                    " and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" +
                    tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal, " +
                    "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM ReturpakaiDetail WHERE ReturPakaiDetail.ItemID=SaldoInventory.ItemID  " + 
                    "and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal +
                    "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal,  " +
                    "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID  " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND  " +
                    "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal, " +
                    "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID  " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND   " +
                    "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal, " +
                    "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE  " +
                    "PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.groupid=" + groupID + " and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar, " +
                    "PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal  " +
                    "from SaldoInventory where SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in  " +
                    "(select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID,  " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND  " + 
                    "ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 +
                    "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1  " + 
                    "AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 +
                    "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2,  " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1  " + 
                    "AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 +
                    "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2,  " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM returpakaidetail WHERE returpakaidetail.ItemID=Inventory.ID and ReturID  " + 
                    "IN (SELECT ID FROM ReturPakai WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 +
                    "' and Status>-1 )) END  Retur2,  " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.groupid=" + groupID + " and  " + 
                    "PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 +
                    "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON  " + 
                    "Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + 
                    ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
                }
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql );
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventory.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventory.Add(new SaldoInventory());

            return arrSaldoInventory;

        }
        public ArrayList RetrieveSaldoInventory1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string inv = string.Empty;
            switch (groupID)
            {
                case 4:
                    inv = "Asset";
                    break;
                case 5:
                    inv = "Biaya";
                    break;
                default:
                    inv = "Inventory";
                    break;
            }
            string strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal) as StokAwal,(SAwal) as StokAkhir from (select ItemID," +
        "case when SaldoInventory.ItemID>0 then (select " + inv + ".ItemCode from " + inv + " where " + inv + ".ID=SaldoInventory.ItemID) else '' end ItemCode, " +
        "case when SaldoInventory.ItemID>0 then (select " + inv + ".ItemName from " + inv + " where " + inv + ".ID=SaldoInventory.ItemID) else '' end ItemName, " +
        "0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from " + inv + ",UOM where " + inv + ".UOMID=UOM.ID and  " +
        "" + inv + ".ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah," +
        "0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
        "from SaldoInventory where SaldoInventory.YearPeriod= " + yearPeriod + " and SaldoInventory.GroupID=" + groupID + " and SaldoInventory.ItemID in " +
        "(select InvID from (SELECT " + inv + ".id as InvID," + inv + ".ItemCode, " + inv + ".ItemName, UOM.UOMCode," + inv + ".GroupID, " +
        "CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + inv + ".ID and RowStatus>-1 AND " +
        "ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 +
        "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN " + inv + ".ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) " +
        "FROM  AdjustDetail WHERE ItemID = " + inv + ".ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' " +
        "and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, " +
        "CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + inv + ".ID and RowStatus>-1 AND " +
        "AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + 
        "' )) END  AdjustKurang2, CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(quantity),0) FROM ReturpakaiDetail WHERE ReturpakaiDetail.ItemID=" + inv + ".ID and ReturID IN (SELECT ID FROM Returpakai WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=" + inv + ".ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  " + inv + " INNER JOIN UOM ON " + inv + ".UOMID = UOM.ID WHERE " + inv + ".GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventory.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventory.Add(new SaldoInventory());

            return arrSaldoInventory;

        }
        public ArrayList RetrieveSaldoInventoryRePack1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal) as StokAwal,(SAwal) as StokAkhir from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
            "from SaldoInventory where SaldoInventory.YearPeriod= " + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=Inventory.ID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(quantity),0) FROM ReturpakaiDetail WHERE ReturpakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Returpakai WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql );
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventory.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventory.Add(new SaldoInventory());

            return arrSaldoInventory;

        }
        public ArrayList RetrieveSaldoInventoryRePack(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            /**
             * update query merubah pembacaan returpakai dari createdtime ke returdate di stock awal
             * 21-11-2014
             */
            string strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode," +
                            "(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAwal," +
                            "(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir " +
                            "from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory " +
                            "where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then " +
                            "(select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName," +
                            "0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM " +
                            "where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " +
                            ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
                            ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, " +
                            "CASE WHEN SaldoInventory.ItemID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=SaldoInventory.ItemID " +
                            "and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tglAwal +
                            "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tglAkhir + "' ) end PemasukanAwal, " +
                            "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(ReturpakaiDetail.Quantity),0) FROM ReturpakaiDetail " +
                            "WHERE ReturpakaiDetail.ItemID=SaldoInventory.ItemID and ReturID IN (SELECT ID FROM Returpakai " +
                            "WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" +
                            tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) " +
                            "FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN " +
                            "(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" +
                            tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                            "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID " +
                            "and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  " +
                            "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir +
                            "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) " +
                            "FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.RowStatus>-1 and PakaiID IN " +
                            "(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir +
                            "' and Status>-1)) END  PemakaianAwal from SaldoInventory where SaldoInventory.YearPeriod=" + yearPeriod +
                            " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode," +
                            "Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select ISNULL(SUM(ToQty),0) " +
                            "from Convertan where ToItemID=Inventory.ID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' " +
                            "AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan2, " +
                            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail " +
                            "WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND " +
                            "convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 +
                            "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail " +
                            "WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  " +
                            "convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, " +
                            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(ReturpakaiDetail.Quantity),0) FROM ReturpakaiDetail WHERE ReturpakaiDetail.ItemID=Inventory.ID and ReturID IN " +
                            "(SELECT ID FROM Returpakai WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 +
                            "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE " +
                            "PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		" +
                            "(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 +
                            "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 " +
                            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID +
                            ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventory.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventory.Add(new SaldoInventory());

            return arrSaldoInventory;

        }
        public ArrayList RetrieveAllTableForPriceSaldoInventory(string ketQtyBlnLalu, string ketAvgBlnLalu, int yearPeriod, string thBl, int itemTypeID, int groupID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID," + ketQtyBlnLalu + "," + ketAvgBlnLalu + ",QtyReceipt,AvgPriceReceipt,QtyPakai,QtyAdjustTambah," +
                "QtyAdjustKurang,QtyRetur,cast((" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(10,2)) as EndStok," +

                "case when (" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) = 0 then " +
                //"cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
                //"(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/1 as decimal(16,2)) " +
                "0 " +
                "else cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
                "(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/(" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
                "end AvgPrice from(select A0.ItemID,cast(isnull(A0." + ketQtyBlnLalu + ",0) as decimal(10,2)) as " + ketQtyBlnLalu + ",cast(isnull(A0." + ketAvgBlnLalu + ",0) as decimal(16,2)) as " + ketAvgBlnLalu + "," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
                "(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total " +
                "from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyReceipt," +

                //"case when A0.ItemID>0 then (select cast(isnull(sum(Total)/SUM(Quantity),0) as decimal(16,2)) from " +
                //"(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total " +
                //"from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and " +
                //"B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end AvgPriceReceipt," +
                //
                "case when A0.ItemID>0 then (select cast(isnull(SUM(NetPrice)/SUM(Quantity),0) as decimal(16,2)) from (select B.ItemID,B.Quantity,B.Price*B.Quantity as Total,"+
                "case when C.Disc>0 then (D.Price*B.Quantity)-((D.Price*B.Quantity)*(C.Disc/100)) else (D.Price*B.Quantity) End NetPrice "+
                "from Receipt as A,ReceiptDetail as B,POPurchn as C,POPurchnDetail as D "+
                "where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and B.POID=C.ID and B.PODetailID=D.ID and C.ID=D.POID and C.Status>-1 "+
                "and D.Status>-1 and B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end AvgPriceReceipt," +

                //
                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Pakai as A,PakaiDetail as B where A.ID=B.PakaiID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyPakai," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Tambah' and B.ItemID=A0.ItemID) as P ) else 0 " +
                "end QtyAdjustTambah," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Kurang' and B.ItemID=A0.ItemID) as P ) else 0 " +
                "end QtyAdjustKurang," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from ReturPakai as A,ReturPakaiDetail as B where A.ID=B.ReturID and A.Status>-1 and " +
                "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReturDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyRetur " +

                "from SaldoInventory as A0 where A0.YearPeriod=" + yearPeriod + " and A0.ItemTypeID=" + itemTypeID + " and A0.GroupID=" + groupID + ") as A1 order by ItemID");
            strError = dataAccess.Error;
            arrSaldoInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventory.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrSaldoInventory.Add(new SaldoInventory());

            return arrSaldoInventory;

        }
        public SaldoInventory GenerateObject3(SqlDataReader sqlDataReader)
        {
            objSaldoInventory = new SaldoInventory();
            objSaldoInventory.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventory.AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"]);

            return objSaldoInventory;
        }
        public SaldoInventory GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSaldoInventory = new SaldoInventory();
            objSaldoInventory.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventory.ItemCode = sqlDataReader["ItemCode"].ToString();
            objSaldoInventory.ItemName = sqlDataReader["ItemName"].ToString();
            objSaldoInventory.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objSaldoInventory.UOMCode = sqlDataReader["UOMCode"].ToString();
            objSaldoInventory.StokAwal = Convert.ToDecimal(sqlDataReader["StokAwal"]);
            objSaldoInventory.StokAkhir = Convert.ToDecimal(sqlDataReader["StokAkhir"]);

            return objSaldoInventory;
        }
        public SaldoInventory GenerateObject(SqlDataReader sqlDataReader)
        {
            objSaldoInventory = new SaldoInventory();
            objSaldoInventory.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventory.YearPeriod = Convert.ToInt32(sqlDataReader["YearPeriod"]);
            objSaldoInventory.SaldoQty = Convert.ToDecimal(sqlDataReader["SaldoQty"]);
            objSaldoInventory.SaldoPrice = Convert.ToDecimal(sqlDataReader["SaldoPrice"]);
            objSaldoInventory.JanQty = Convert.ToDecimal(sqlDataReader["JanQty"]);
            objSaldoInventory.JanAvgPrice = Convert.ToDecimal(sqlDataReader["JanAvgPrice"]);
            objSaldoInventory.FebQty = Convert.ToDecimal(sqlDataReader["FebQty"]);
            objSaldoInventory.FebAvgPrice = Convert.ToDecimal(sqlDataReader["FebAvgPrice"]);
            objSaldoInventory.MarQty = Convert.ToDecimal(sqlDataReader["MarQty"]);
            objSaldoInventory.MarAvgPrice = Convert.ToDecimal(sqlDataReader["MarAvgPrice"]);
            objSaldoInventory.AprQty = Convert.ToDecimal(sqlDataReader["AprQty"]);
            objSaldoInventory.AprAvgPrice = Convert.ToDecimal(sqlDataReader["AprAvgPrice"]);
            objSaldoInventory.MeiQty = Convert.ToDecimal(sqlDataReader["MeiQty"]);
            objSaldoInventory.MeiAvgPrice = Convert.ToDecimal(sqlDataReader["MeiAvgPrice"]);
            objSaldoInventory.JunQty = Convert.ToDecimal(sqlDataReader["JunQty"]);
            objSaldoInventory.JunAvgPrice = Convert.ToDecimal(sqlDataReader["JunAvgPrice"]);
            objSaldoInventory.JulQty = Convert.ToDecimal(sqlDataReader["JulQty"]);
            objSaldoInventory.JulAvgPrice = Convert.ToDecimal(sqlDataReader["JulAvgPrice"]);
            objSaldoInventory.AguQty = Convert.ToDecimal(sqlDataReader["AguQty"]);
            objSaldoInventory.AguAvgPrice = Convert.ToDecimal(sqlDataReader["AguAvgPrice"]);
            objSaldoInventory.SepQty = Convert.ToDecimal(sqlDataReader["SepQty"]);
            objSaldoInventory.SepAvgPrice = Convert.ToDecimal(sqlDataReader["SepAvgPrice"]);
            objSaldoInventory.OktQty = Convert.ToDecimal(sqlDataReader["OktQty"]);
            objSaldoInventory.OktAvgPrice = Convert.ToDecimal(sqlDataReader["OktAvgPrice"]);
            objSaldoInventory.NovQty = Convert.ToDecimal(sqlDataReader["NovQty"]);
            objSaldoInventory.NovAvgPrice = Convert.ToDecimal(sqlDataReader["NovAvgPrice"]);
            objSaldoInventory.DesQty = Convert.ToDecimal(sqlDataReader["DesQty"]);
            objSaldoInventory.DesAvgPrice = Convert.ToDecimal(sqlDataReader["DesAvgPrice"]);

            return objSaldoInventory;
        }
        /**
         * added on 09-05-2014
         */
        public string GetStrMonth(int monthAvgPrice)
        {
            string strAvgPrice = string.Empty;
            switch (monthAvgPrice)
            {
                case 1:
                    strAvgPrice = "janAvgPrice";
                    break;
                case 2:
                    strAvgPrice = "febAvgPrice";
                    break;
                case 3:
                    strAvgPrice = "marAvgPrice";
                    break;
                case 4:
                    strAvgPrice = "aprAvgPrice";
                    break;
                case 5:
                    strAvgPrice = "meiAvgPrice";
                    break;
                case 6:
                    strAvgPrice = "junAvgPrice";
                    break;
                case 7:
                    strAvgPrice = "julAvgPrice";
                    break;
                case 8:
                    strAvgPrice = "aguAvgPrice";
                    break;
                case 9:
                    strAvgPrice = "sepAvgPrice";
                    break;
                case 10:
                    strAvgPrice = "oktAvgPrice";
                    break;
                case 11:
                    strAvgPrice = "novAvgPrice";
                    break;
                case 12:
                    strAvgPrice = "desAvgPrice";
                    break;
            }
            return strAvgPrice;
        }
        public ArrayList GetTahun()
        {
            arrSaldoInventory = new ArrayList();
            string query = "select distinct YearPeriod from saldoinventory order by YearPeriod desc ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if(da.Error==string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSaldoInventory.Add(new SaldoInventory
                    {
                        YearPeriod = Convert.ToInt32(sdr["YearPeriod"].ToString())
                    });
                }
            }
            return arrSaldoInventory;
        }
    }
}
