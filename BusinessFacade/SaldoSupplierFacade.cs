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
    public class SaldoSupplierFacade : AbstractFacade
    {
        private SaldoSupplier objSaldoSupplier = new SaldoSupplier();
        private ArrayList arrSaldoSupplier;
        private List<SqlParameter> sqlListParam;

        public SaldoSupplierFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSaldoSupplier = (SaldoSupplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierID", objSaldoSupplier.SupplierID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoSupplier.YearPeriod));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSaldoSupplier");
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
                objSaldoSupplier = (SaldoSupplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierID", objSaldoSupplier.SupplierID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoSupplier.YearPeriod));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoSupplier.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Price", objSaldoSupplier.Price));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoSupplier.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoSupplier");

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
                objSaldoSupplier = (SaldoSupplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoSupplier.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoSupplier.YearPeriod));

                int intResult = dataAccess.ProcessData(sqlListParam, "spKosongkanSaldoSupplier");

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
                objSaldoSupplier = (SaldoSupplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierID", objSaldoSupplier.SupplierID));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoSupplier.YearPeriod));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoSupplier.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Price", objSaldoSupplier.Price));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoSupplier.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusSaldoSupplier");

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
                objSaldoSupplier = (SaldoSupplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@YearPeriod", objSaldoSupplier.YearPeriod));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objSaldoSupplier.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@Posting", objSaldoSupplier.Posting));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSaldoSupplierBlnLalu");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoSupplier");
            strError = dataAccess.Error;
            arrSaldoSupplier = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSaldoSupplier.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSaldoSupplier.Add(new SaldoSupplier());

            return arrSaldoSupplier;

        }

        //public SaldoSupplier RetrieveByItemID(int itemID, int thn, int itemtypeID)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoSupplier where ItemId=" + itemID + " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID);
        //    strError = dataAccess.Error;
        //    arrSaldoSupplier = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject(sqlDataReader);
        //        }
        //    }

        //    return new SaldoSupplier();
        //}

        //public ArrayList RetrieveByGroupID()
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SaldoSupplier");
        //    strError = dataAccess.Error;
        //    arrSaldoSupplier = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSaldoSupplier.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSaldoSupplier.Add(new SaldoSupplier());

        //    return arrSaldoSupplier;

        //}

        //public ArrayList RetrieveSaldoInventory(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, CASE WHEN SaldoInventory.ItemID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SaldoInventory.ItemID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventory.ItemID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from SaldoSupplier where SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ");
        //    strError = dataAccess.Error;
        //    arrSaldoSupplier = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSaldoSupplier.Add(GenerateObject2(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSaldoSupplier.Add(new SaldoSupplier());

        //    return arrSaldoSupplier;

        //}
        //public ArrayList RetrieveSaldoInventory1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal) as StokAwal,(SAwal) as StokAkhir from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
        //    "from SaldoSupplier where SaldoInventory.YearPeriod= " + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ");
        //    strError = dataAccess.Error;
        //    arrSaldoSupplier = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSaldoSupplier.Add(GenerateObject2(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSaldoSupplier.Add(new SaldoSupplier());

        //    return arrSaldoSupplier;

        //}

        //public ArrayList RetrieveSaldoInventoryRePack1(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal) as StokAwal,(SAwal) as StokAkhir from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
        //    "from SaldoSupplier where SaldoInventory.YearPeriod= " + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=Inventory.ID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ");
        //    strError = dataAccess.Error;
        //    arrSaldoSupplier = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSaldoSupplier.Add(GenerateObject2(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSaldoSupplier.Add(new SaldoSupplier());

        //    return arrSaldoSupplier;

        //}
        //public ArrayList RetrieveSaldoInventoryRePack(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAwal,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, CASE WHEN SaldoInventory.ItemID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=SaldoInventory.ItemID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tglAkhir + "' ) end PemasukanAwal, " +
        //        "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventory.ItemID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from SaldoSupplier where SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=Inventory.ID and Convertan.RowStatus>-1 and convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan2, " +
        //        "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE Inventory.GroupID in (" + groupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 ");
        //    strError = dataAccess.Error;
        //    arrSaldoSupplier = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSaldoSupplier.Add(GenerateObject2(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSaldoSupplier.Add(new SaldoSupplier());

        //    return arrSaldoSupplier;

        //}

        //public ArrayList RetrieveAllTableForPriceSaldoInventory(string ketQtyBlnLalu, string ketAvgBlnLalu, int yearPeriod, string thBl, int itemTypeID, int groupID)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID," + ketQtyBlnLalu + "," + ketAvgBlnLalu + ",QtyReceipt,AvgPriceReceipt,QtyPakai,QtyAdjustTambah," +
        //        "QtyAdjustKurang,QtyRetur,cast((" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(10,2)) as EndStok," +

        //        "case when (" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) = 0 then " +
        //        //"cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
        //        //"(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/1 as decimal(16,2)) " +
        //        "0 " +
        //        "else cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
        //        "(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/(" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
        //        "end AvgPrice from(select A0.ItemID,cast(isnull(A0." + ketQtyBlnLalu + ",0) as decimal(10,2)) as " + ketQtyBlnLalu + ",cast(isnull(A0." + ketAvgBlnLalu + ",0) as decimal(16,2)) as " + ketAvgBlnLalu + "," +

        //        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
        //        "(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total " +
        //        "from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and " +
        //        "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyReceipt," +

        //        //"case when A0.ItemID>0 then (select cast(isnull(sum(Total)/SUM(Quantity),0) as decimal(16,2)) from " +
        //        //"(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total " +
        //        //"from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and " +
        //        //"B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end AvgPriceReceipt," +
        //        //
        //        "case when A0.ItemID>0 then (select cast(isnull(SUM(NetPrice)/SUM(Quantity),0) as decimal(16,2)) from (select B.ItemID,B.Quantity,B.Price*B.Quantity as Total," +
        //        "case when C.Disc>0 then (D.Price*B.Quantity)-((D.Price*B.Quantity)*(C.Disc/100)) else (D.Price*B.Quantity) End NetPrice " +
        //        "from Receipt as A,ReceiptDetail as B,POPurchn as C,POPurchnDetail as D " +
        //        "where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and B.POID=C.ID and B.PODetailID=D.ID and C.ID=D.POID and C.Status>-1 " +
        //        "and D.Status>-1 and B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end AvgPriceReceipt," +

        //        //
        //        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
        //        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
        //        "from Pakai as A,PakaiDetail as B where A.ID=B.PakaiID and A.Status>-1  and B.RowStatus>-1 and " +
        //        "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyPakai," +

        //        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
        //        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
        //        "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
        //        "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Tambah' and B.ItemID=A0.ItemID) as P ) else 0 " +
        //        "end QtyAdjustTambah," +

        //        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
        //        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
        //        "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
        //        "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Kurang' and B.ItemID=A0.ItemID) as P ) else 0 " +
        //        "end QtyAdjustKurang," +

        //        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(10,2)) from " +
        //        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
        //        "from ReturPakai as A,ReturPakaiDetail as B where A.ID=B.ReturID and A.Status>-1 and " +
        //        "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and LEFT(convert(varchar,A.ReturDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyRetur " +

        //        "from SaldoSupplier as A0 where A0.YearPeriod=" + yearPeriod + " and A0.ItemTypeID=" + itemTypeID + " and A0.GroupID=" + groupID + ") as A1 order by ItemID");
        //    strError = dataAccess.Error;
        //    arrSaldoSupplier = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSaldoSupplier.Add(GenerateObject3(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSaldoSupplier.Add(new SaldoSupplier());

        //    return arrSaldoSupplier;

        //}

        //public SaldoSupplier GenerateObject3(SqlDataReader sqlDataReader)
        //{
        //    objSaldoSupplier = new SaldoSupplier();
        //    objSaldoSupplier .ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
        //    objSaldoSupplier .AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"]);

        //    return objSaldoSupplier ;
        //}

        //public SaldoSupplier GenerateObject2(SqlDataReader sqlDataReader)
        //{
        //    objSaldoSupplier = new SaldoSupplier();
        //    objSaldoSupplier .ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
        //    objSaldoSupplier .ItemCode = sqlDataReader["ItemCode"].ToString();
        //    objSaldoSupplier .ItemName = sqlDataReader["ItemName"].ToString();
        //    objSaldoSupplier .UomID = Convert.ToInt32(sqlDataReader["UomID"]);
        //    objSaldoSupplier .UOMCode = sqlDataReader["UOMCode"].ToString();
        //    objSaldoSupplier .StokAwal = Convert.ToDecimal(sqlDataReader["StokAwal"]);
        //    objSaldoSupplier .StokAkhir = Convert.ToDecimal(sqlDataReader["StokAkhir"]);

        //    return objSaldoSupplier ;
        //}

        public SaldoSupplier GenerateObject(SqlDataReader sqlDataReader)
        {
            objSaldoSupplier = new SaldoSupplier();
            objSaldoSupplier.SupplierID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSaldoSupplier.YearPeriod = Convert.ToInt32(sqlDataReader["YearPeriod"]);
            objSaldoSupplier.Saldo = Convert.ToDecimal(sqlDataReader["Saldo"]);
            objSaldoSupplier.Jan = Convert.ToDecimal(sqlDataReader["Jan"]);
            objSaldoSupplier.Feb = Convert.ToDecimal(sqlDataReader["Feb"]);
            objSaldoSupplier.Mar = Convert.ToDecimal(sqlDataReader["Mar"]);
            objSaldoSupplier.Apr = Convert.ToDecimal(sqlDataReader["Apr"]);
            objSaldoSupplier.Mei = Convert.ToDecimal(sqlDataReader["Mei"]);
            objSaldoSupplier.Jun = Convert.ToDecimal(sqlDataReader["Jun"]);
            objSaldoSupplier.Jul = Convert.ToDecimal(sqlDataReader["Jul"]);
            objSaldoSupplier.Agu = Convert.ToDecimal(sqlDataReader["Agu"]);
            objSaldoSupplier.Sep = Convert.ToDecimal(sqlDataReader["Sep"]);
            objSaldoSupplier.Okt = Convert.ToDecimal(sqlDataReader["Okt"]);
            objSaldoSupplier.Nov = Convert.ToDecimal(sqlDataReader["Nov"]);
            objSaldoSupplier.Des = Convert.ToDecimal(sqlDataReader["Des"]);

            return objSaldoSupplier ;
        }
    }
}
