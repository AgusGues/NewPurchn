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
    public class SaldoInventoryT1Facade : AbstractFacade
    {
        private SaldoInventoryT1 objSaldoInventoryT1 = new SaldoInventoryT1();
        private ArrayList arrSaldoInventoryT1;
        private List<SqlParameter> sqlListParam;

        public SaldoInventoryT1Facade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSaldoInventoryT1 = (SaldoInventoryT1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryT1.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryT1.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryT1.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryT1.ItemTypeID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSaldoInventoryT1");
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
            if (objSaldoInventoryT1.ItemID == 7)
            {
                string c = string.Empty;
            }
            try
            {
                objSaldoInventoryT1 = (SaldoInventoryT1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryT1.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryT1.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryT1.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryT1.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryT1.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventoryT1.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventoryT1.Posting));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSaldoInventoryT1.SaldoPrice ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoInventoryT1");
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
                objSaldoInventoryT1 = (SaldoInventoryT1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryT1.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryT1.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryT1.GroupID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spKosongkanSaldoInventoryT1");

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
                objSaldoInventoryT1 = (SaldoInventoryT1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryT1.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryT1.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryT1.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryT1.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryT1.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventoryT1.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventoryT1.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusSaldoInventoryT1");

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
                objSaldoInventoryT1 = (SaldoInventoryT1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryT1.ItemID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryT1.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryT1.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSaldoInventoryT1.AvgPrice));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryT1.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryT1.YearPeriod));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoAvgPriceBlnIniT1");

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
                objSaldoInventoryT1 = (SaldoInventoryT1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objSaldoInventoryT1.ItemID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoInventoryT1.YearPeriod));
                sqlListParam.Add(new SqlParameter("@GroupID", objSaldoInventoryT1.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSaldoInventoryT1.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoInventoryT1.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Quantity", objSaldoInventoryT1.Quantity));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoInventoryT1.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoBlnLaluT1");

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
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoT1Null");
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventoryT1");
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryT1.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryT1.Add(new SaldoInventoryT1());

            return arrSaldoInventoryT1;

        }

        public SaldoInventoryT1 RetrieveByItemID(int itemID, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventoryT1 where ItemId=" + itemID + " and YearPeriod=" + thn + " and ItemTypeID="+itemtypeID);
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SaldoInventoryT1();
        }

        public int GetPrice(int itemID, string MonthAvgPrice, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " + MonthAvgPrice + 
                " from SaldoInventoryT1 where ItemId=" + itemID + " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID);
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader[MonthAvgPrice]);
                }
            }

            return 0;
        }
        public int CekRow(int itemID, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventoryT1 where ItemId=" + itemID + 
                " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID);
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoInventoryT1");
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryT1.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryT1.Add(new SaldoInventoryT1());

            return arrSaldoInventoryT1;

        }

        public ArrayList RetrieveSaldoInventoryT1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string strsql = string.Empty;
            if (groupID == 4)
            {
                strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) " +
                    " as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID, " +
                    "case when SaldoInventoryT1.ItemID>0 then (select ASSET.ItemCode from ASSET where ASSET.ID=SaldoInventoryT1.ItemID) else '' end ItemCode, " +
                    "case when SaldoInventoryT1.ItemID>0 then (select ASSET.ItemName from ASSET where ASSET.ID=SaldoInventoryT1.ItemID) else '' end ItemName, " +
                    "0 as UomID, " +
                    "case when SaldoInventoryT1.ItemID>0 then (select UOM.UOMCode from ASSET,UOM where ASSET.UOMID=UOM.ID and ASSET.ID=SaldoInventoryT1.ItemID)  " +
                    "else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian, " + 
                    ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID,  " + 
                    "CASE WHEN SaldoInventoryT1.ItemID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SaldoInventoryT1.ItemID " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal, " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventoryT1.ItemID  " +
                    "and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + 
                    "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal,  " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID  " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal, " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID and groupid=" + groupID + " and  " +
                    "RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal, " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventoryT1.ItemID " +
                    "and PakaiDetail.groupid=" + groupID + " and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + 
                    "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from SaldoInventoryT1 where  " +
                    "SaldoInventoryT1.YearPeriod=" + yearPeriod + " and SaldoInventoryT1.ITEMtypeid=2 and SaldoInventoryT1.ItemID in (select InvID from (SELECT ASSET.id as InvID,ASSET.ItemCode, ASSET.ItemName, UOM.UOMCode,ASSET.GroupID, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=ASSET.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = ASSET.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = ASSET.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=ASSET.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN ASSET.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=ASSET.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  ASSET INNER JOIN UOM ON ASSET.UOMID = UOM.ID WHERE ASSET.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
               
            }
            else
                if (groupID == 5)
                {
                strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) " + 
                    "as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID, " + 
                    "case when SaldoInventoryT1.ItemID>0 then (select biaya.ItemCode from biaya where biaya.ID=SaldoInventoryT1.ItemID) else '' end ItemCode, " + 
                    "case when SaldoInventoryT1.ItemID>0 then (select biaya.ItemName from biaya where biaya.ID=SaldoInventoryT1.ItemID) else '' end ItemName,0  " + 
                    "as UomID, " + 
                    "case when SaldoInventoryT1.ItemID>0 then (select UOM.UOMCode from biaya,UOM where biaya.UOMID=UOM.ID and biaya.ID=SaldoInventoryT1.ItemID) " + 
                    "else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian, " + 
                    ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, CASE WHEN SaldoInventoryT1.ItemID>0 THEN " + 
                    "(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SaldoInventoryT1.ItemID and groupid=" + groupID + 
                    " and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal, " + 
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventoryT1.ItemID  " +
                    "and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + 
                    "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal,  " + 
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID  " +
                    "and RowStatus>-1 and groupid=" + groupID + " AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND  " + 
                    "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,  " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID and groupid=" + 
                    groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,  " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventoryT1.ItemID  " + 
                    "and groupid=" + groupID + " and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + 
                    tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from SaldoInventoryT1 where   " +
                    "SaldoInventoryT1.YearPeriod=" + yearPeriod + " and SaldoInventoryT1.ItemTypeID=3 and SaldoInventoryT1.ItemID in (select InvID from   " +
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
                    "CASE WHEN biaya.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=biaya.ID and ReturID IN   " +
                    "(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + 
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
                    "case when SaldoInventoryT1.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventoryT1.ItemID) else ''  " +
                    "end ItemCode, " +
                    "case when SaldoInventoryT1.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventoryT1.ItemID) else ''  " +
                    "end ItemName,0 as UomID, " +
                    "case when SaldoInventoryT1.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and  " +
                    "Inventory.ID=SaldoInventoryT1.ItemID) else '' end UOMCode, " + 
                     ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah, " +
                    "0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID,  " +
                    "CASE WHEN SaldoInventoryT1.ItemID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE " +
                    "ItemID=SaldoInventoryT1.ItemID and groupid=" + groupID +  
                    " and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" +
                    tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal, " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventoryT1.ItemID  " + 
                    "and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal +
                    "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal,  " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID  " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND  " +
                    "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal, " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID  " +
                    "and groupid=" + groupID + " and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND   " +
                    "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal, " +
                    "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE  " +
                    "PakaiDetail.ItemID=SaldoInventoryT1.ItemID and PakaiDetail.groupid=" + groupID + " and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar, " +
                    "PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal  " +
                    "from SaldoInventoryT1 where SaldoInventoryT1.YearPeriod=" + yearPeriod + " and SaldoInventoryT1.ItemTypeID=1 and SaldoInventoryT1.ItemID in  " +
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
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID  " + 
                    "IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 +
                    "' and Status>-1 )) END  Retur2,  " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.groupid=" + groupID + " and  " + 
                    "PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 +
                    "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON  " + 
                    "Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + 
                    ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
                }
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql );
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryT1.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryT1.Add(new SaldoInventoryT1());

            return arrSaldoInventoryT1;

        }
        public ArrayList RetrieveSaldoInventoryT11(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
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
        "case when SaldoInventoryT1.ItemID>0 then (select " + inv + ".ItemCode from " + inv + " where " + inv + ".ID=SaldoInventoryT1.ItemID) else '' end ItemCode, " +
        "case when SaldoInventoryT1.ItemID>0 then (select " + inv + ".ItemName from " + inv + " where " + inv + ".ID=SaldoInventoryT1.ItemID) else '' end ItemName, " +
        "0 as UomID,case when SaldoInventoryT1.ItemID>0 then (select UOM.UOMCode from " + inv + ",UOM where " + inv + ".UOMID=UOM.ID and  " +
        "" + inv + ".ID=SaldoInventoryT1.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah," +
        "0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
        "from SaldoInventoryT1 where SaldoInventoryT1.YearPeriod= " + yearPeriod + " and SaldoInventoryT1.GroupID=" + groupID + " and SaldoInventoryT1.ItemID in " +
        "(select InvID from (SELECT " + inv + ".id as InvID," + inv + ".ItemCode, " + inv + ".ItemName, UOM.UOMCode," + inv + ".GroupID, " +
        "CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + inv + ".ID and RowStatus>-1 AND " +
        "ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 +
        "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN " + inv + ".ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) " +
        "FROM  AdjustDetail WHERE ItemID = " + inv + ".ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' " +
        "and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, " +
        "CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + inv + ".ID and RowStatus>-1 AND " +
        "AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=" + inv + ".ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN " + inv + ".ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=" + inv + ".ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  " + inv + " INNER JOIN UOM ON " + inv + ".UOMID = UOM.ID WHERE " + inv + ".GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryT1.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryT1.Add(new SaldoInventoryT1());

            return arrSaldoInventoryT1;

        }

        public ArrayList RetrieveSaldoInventoryT1RePack1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal) as StokAwal,(SAwal) as StokAkhir from (select ItemID,case when SaldoInventoryT1.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventoryT1.ItemID) else '' end ItemCode,case when SaldoInventoryT1.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventoryT1.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventoryT1.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventoryT1.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
            "from SaldoInventoryT1 where SaldoInventoryT1.YearPeriod= " + yearPeriod + " and SaldoInventoryT1.ItemTypeID=1 and SaldoInventoryT1.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=Inventory.ID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql );
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryT1.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryT1.Add(new SaldoInventoryT1());

            return arrSaldoInventoryT1;

        }
        public ArrayList RetrieveSaldoInventoryT1RePack(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string strsql = "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID,case when SaldoInventoryT1.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventoryT1.ItemID) else '' end ItemCode,case when SaldoInventoryT1.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventoryT1.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventoryT1.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventoryT1.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=SaldoInventoryT1.ItemID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tglAkhir + "' ) end PemasukanAwal, " +
                "CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventoryT1.ItemID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventoryT1.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventoryT1.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventoryT1.ItemID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from SaldoInventoryT1 where SaldoInventoryT1.YearPeriod=" + yearPeriod + " and SaldoInventoryT1.ItemTypeID=1 and SaldoInventoryT1.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=Inventory.ID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan2, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryT1.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryT1.Add(new SaldoInventoryT1());

            return arrSaldoInventoryT1;

        }

        public ArrayList RetrieveAllTableForPriceSaldoInventoryT1(string ketQtyBlnLalu, string ketAvgBlnLalu, int yearPeriod, string thBl, int itemTypeID, int groupID)
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

                "from SaldoInventoryT1 as A0 where A0.YearPeriod=" + yearPeriod + " and A0.ItemTypeID=" + itemTypeID + " and A0.GroupID=" + groupID + ") as A1 order by ItemID");
            strError = dataAccess.Error;
            arrSaldoInventoryT1 = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoInventoryT1.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrSaldoInventoryT1.Add(new SaldoInventoryT1());

            return arrSaldoInventoryT1;

        }

        public SaldoInventoryT1 GenerateObject3(SqlDataReader sqlDataReader)
        {
            objSaldoInventoryT1 = new SaldoInventoryT1();
            objSaldoInventoryT1.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventoryT1.AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"]);

            return objSaldoInventoryT1;
        }

        public SaldoInventoryT1 GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSaldoInventoryT1 = new SaldoInventoryT1();
            objSaldoInventoryT1.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventoryT1.PartNo  = sqlDataReader["ItemCode"].ToString();
            objSaldoInventoryT1.ItemDesc  = sqlDataReader["ItemName"].ToString();
            objSaldoInventoryT1.StokAwal = Convert.ToDecimal(sqlDataReader["StokAwal"]);
            objSaldoInventoryT1.StokAkhir = Convert.ToDecimal(sqlDataReader["StokAkhir"]);

            return objSaldoInventoryT1;
        }

        public SaldoInventoryT1 GenerateObject(SqlDataReader sqlDataReader)
        {
            objSaldoInventoryT1 = new SaldoInventoryT1();
            objSaldoInventoryT1.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoInventoryT1.YearPeriod = Convert.ToInt32(sqlDataReader["YearPeriod"]);
            objSaldoInventoryT1.SaldoQty = Convert.ToDecimal(sqlDataReader["SaldoQty"]);
            objSaldoInventoryT1.SaldoPrice = Convert.ToDecimal(sqlDataReader["SaldoPrice"]);
            objSaldoInventoryT1.JanQty = Convert.ToDecimal(sqlDataReader["JanQty"]);
            objSaldoInventoryT1.JanAvgPrice = Convert.ToDecimal(sqlDataReader["JanAvgPrice"]);
            objSaldoInventoryT1.FebQty = Convert.ToDecimal(sqlDataReader["FebQty"]);
            objSaldoInventoryT1.FebAvgPrice = Convert.ToDecimal(sqlDataReader["FebAvgPrice"]);
            objSaldoInventoryT1.MarQty = Convert.ToDecimal(sqlDataReader["MarQty"]);
            objSaldoInventoryT1.MarAvgPrice = Convert.ToDecimal(sqlDataReader["MarAvgPrice"]);
            objSaldoInventoryT1.AprQty = Convert.ToDecimal(sqlDataReader["AprQty"]);
            objSaldoInventoryT1.AprAvgPrice = Convert.ToDecimal(sqlDataReader["AprAvgPrice"]);
            objSaldoInventoryT1.MeiQty = Convert.ToDecimal(sqlDataReader["MeiQty"]);
            objSaldoInventoryT1.MeiAvgPrice = Convert.ToDecimal(sqlDataReader["MeiAvgPrice"]);
            objSaldoInventoryT1.JunQty = Convert.ToDecimal(sqlDataReader["JunQty"]);
            objSaldoInventoryT1.JunAvgPrice = Convert.ToDecimal(sqlDataReader["JunAvgPrice"]);
            objSaldoInventoryT1.JulQty = Convert.ToDecimal(sqlDataReader["JulQty"]);
            objSaldoInventoryT1.JulAvgPrice = Convert.ToDecimal(sqlDataReader["JulAvgPrice"]);
            objSaldoInventoryT1.AguQty = Convert.ToDecimal(sqlDataReader["AguQty"]);
            objSaldoInventoryT1.AguAvgPrice = Convert.ToDecimal(sqlDataReader["AguAvgPrice"]);
            objSaldoInventoryT1.SepQty = Convert.ToDecimal(sqlDataReader["SepQty"]);
            objSaldoInventoryT1.SepAvgPrice = Convert.ToDecimal(sqlDataReader["SepAvgPrice"]);
            objSaldoInventoryT1.OktQty = Convert.ToDecimal(sqlDataReader["OktQty"]);
            objSaldoInventoryT1.OktAvgPrice = Convert.ToDecimal(sqlDataReader["OktAvgPrice"]);
            objSaldoInventoryT1.NovQty = Convert.ToDecimal(sqlDataReader["NovQty"]);
            objSaldoInventoryT1.NovAvgPrice = Convert.ToDecimal(sqlDataReader["NovAvgPrice"]);
            objSaldoInventoryT1.DesQty = Convert.ToDecimal(sqlDataReader["DesQty"]);
            objSaldoInventoryT1.DesAvgPrice = Convert.ToDecimal(sqlDataReader["DesAvgPrice"]);

            return objSaldoInventoryT1;
        }

    }
}
