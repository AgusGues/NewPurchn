using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessFacade
{
    public class ReportPelFacade 
    {

        public string ViewPembelianBarang(string ketQtyBlnLalu, string ketAvgBlnLalu, int yearPeriod, string thBl, int itemTypeID, int groupID)
        {
            return "select ItemID,ItemCode,ItemName,UOMCode," + ketQtyBlnLalu + " as QtySaldo," + ketAvgBlnLalu + " as HppSaldo," + ketQtyBlnLalu + "*" + ketAvgBlnLalu + " as TotSaldo," +
                "QtyReceipt as QtyMasuk,AvgPriceReceipt as AvgHargaBeli,QtyReceipt*AvgPriceReceipt as AvgTotBeli," +
                "QtyPakai," + ketAvgBlnLalu + " as HppSaldoPakai,QtyPakai*" + ketAvgBlnLalu + " as TotHppSaldoPakai," +
                "QtyAdjustTambah," + ketAvgBlnLalu + " as HppSaldoAdjustTambah,QtyAdjustTambah*" + ketAvgBlnLalu + " as TotHppSaldoQtyAdjustTambah," +
                "QtyAdjustKurang," + ketAvgBlnLalu + " as HppSaldoAdjustKurang,QtyAdjustKurang*" + ketAvgBlnLalu + " as TotHppSaldoQtyAdjustKurang," +
                "QtyRetur," + ketAvgBlnLalu + " as HppSaldoRetur,QtyAdjustKurang*" + ketAvgBlnLalu + " as TotHppSaldoQtyRetur," +
                "cast((" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) as EndStok," +
                "case when (" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) = 0 then 0 else " +
                "cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
                "(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/(" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
                "end AvgPrice," +
                "case when (" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) = 0 then 0 else " +
                "cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
                "(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/(" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
                "* cast((" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
                "end TotAvgPrice " +

                "from(select A0.ItemID,A1.ItemCode,A1.ItemName,A2.UOMCode,cast(isnull(A0." + ketQtyBlnLalu + ",0) as decimal(16,2)) as " + ketQtyBlnLalu + ",cast(isnull(A0." + ketAvgBlnLalu + ",0) as decimal) as " + ketAvgBlnLalu + ", " +
                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total " +
                "from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyReceipt," +

                //"case when A0.ItemID>0 then (select cast(isnull(sum(Total)/SUM(Quantity),0) as decimal(16,2)) from "+
                                //"(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total "+
                                //"from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and "+
                                //"B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end AvgPriceReceipt," +
                "case when A0.ItemID>0 then (select cast(isnull(SUM(NetPrice)/SUM(Quantity),0) as decimal(16,2)) from (select B.ItemID,B.Quantity,B.Price*B.Quantity as Total," +
                "case when C.Disc>0 then (D.Price*B.Quantity)-((D.Price*B.Quantity)*(C.Disc/100)) else (D.Price*B.Quantity) End NetPrice " +
                "from Receipt as A,ReceiptDetail as B,POPurchn as C,POPurchnDetail as D " +
                "where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and B.POID=C.ID and B.PODetailID=D.ID and C.ID=D.POID and C.Status>-1 " +
                "and D.Status>-1 and B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID ) as P) else 0 end AvgPriceReceipt," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Pakai as A,PakaiDetail as B where A.ID=B.PakaiID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyPakai," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Tambah' and B.ItemID=A0.ItemID) as P ) else 0 " +
                "end QtyAdjustTambah," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Kurang' and B.ItemID=A0.ItemID) as P ) else 0 " +
                "end QtyAdjustKurang," +
                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from ReturPakai as A,ReturPakaiDetail as B where A.ID=B.ReturID and A.Status>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReturDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyRetur " +
                "from SaldoInventory as A0,Inventory as A1, UOM as A2 where A1.ID=A0.ItemID and A1.GroupID=" + groupID + " and A1.ItemTypeID=" + itemTypeID + " and A1.UOMID=A2.ID and " +
                "A0.YearPeriod=" + yearPeriod + " and A0.ItemTypeID=" + itemTypeID + " and A0.GroupID=" + groupID + ") as A1 " +
                "where (" + ketQtyBlnLalu + " + QtyReceipt + QtyPakai + QtyAdjustTambah + QtyAdjustKurang + QtyRetur)>0 order by ItemCode";
        }


        public string ViewHarian5(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            invGroupID = "Inventory.GroupID in (" + groupID + ")";

            return
                //                "--lap harian utk tgl 2 keatas
"select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, CASE WHEN SaldoInventory.ItemID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SaldoInventory.ItemID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventory.ItemID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal from SaldoInventory where SaldoInventory.YearPeriod=2011 and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 " +
                //--pemasukan, urutan = 2
"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) " +
"end StokAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan) end StokAkhir,0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +

"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal," +
"CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and " +

"convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and " +
"ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID)  END NoDoc " +
"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemasukan>0 " +

"union " +
                //--pemakaian, urutan = 3
"select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoawal
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan) " +
"end StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoakhir
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal-Pemakaian+Pemasukan) " +
"end StokAkhir ,DeptID,DeptCode,NoDoc,3 as Urutan,GroupID from " +
"(SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END DeptID, " +
"CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID)  END DeptCode," +
"CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END NoDoc, " +

"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
                //--add pemasukan dulu
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                //--
"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemakaian>0 " +

"union " +
                //--adjustkurang, urutan = 4
"select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan, kurang pemakaian pada saldoawal
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan-Pemakaian) " +
"end StokAwal ,0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
                //--add pemasukan, kurang pemakaian pada saldoakhir
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal-AdjustKurang+Pemasukan-Pemakaian) " +
"end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
"4 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +
                //--
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
                //--
"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal " +
"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustKurang>0 " +

"union " +
                //--adjusttambah, urutan = 5
"select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
                //--add & kurang pada stokawal & stokakhir utk pemasukan,pemakaian,adjustkurang
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan-Pemakaian-AdjustKurang) " +
"end StokAwal ,0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+AdjustTambah+Pemasukan-Pemakaian-AdjustKurang) " +
"end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
"5 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                //--
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                //--
"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustTambah>0 " +

"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) end StokAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Retur) end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur,CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Retur>0 " +

"order by ItemCode,Urutan";

        }
        public string ViewHarian5a(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            invGroupID = "Inventory.GroupID in (" + groupID + ")";

            return
                //                "--lap harian utk tgl 2 keatas
"select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal) as StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,(SAwal) as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
"from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID," +
"case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID," +
"CASE WHEN SaldoInventory.ItemID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SaldoInventory.ItemID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventory.ItemID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal " +
"from SaldoInventory where SaldoInventory.YearPeriod=2011 and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 " +
                //--pemasukan, urutan = 2
"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)) " +
"end StokAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan) end StokAkhir,0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +

//"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal," +

"CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and " +
"convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and " +
"ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID)  END NoDoc " +
"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemasukan>0 " +

"union " +
                //--pemakaian, urutan = 3
"select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoawal
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan) " +
"end StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoakhir
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Pemakaian+Pemasukan) " +
"end StokAkhir ,DeptID,DeptCode,NoDoc,3 as Urutan,GroupID from " +
"(SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END DeptID, " +
"CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID)  END DeptCode," +
"CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END NoDoc, " +

"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
                //--add pemasukan dulu
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan " +
                //--
                //"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal "+

"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemakaian>0 " +

"union " +
                //--adjustkurang, urutan = 4
"select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan, kurang pemakaian pada saldoawal
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan-Pemakaian) " +
"end StokAwal ,0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
                //--add pemasukan, kurang pemakaian pada saldoakhir
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-AdjustKurang+Pemasukan-Pemakaian) " +
"end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
"4 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +
                //--
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                //--
                //"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal " +

"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustKurang>0 " +

"union " +
                //--adjusttambah, urutan = 5
"select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
                //--add & kurang pada stokawal & stokakhir utk pemasukan,pemakaian,adjustkurang
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan-Pemakaian-AdjustKurang) " +
"end StokAwal ,0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+AdjustTambah+Pemasukan-Pemakaian-AdjustKurang) " +
"end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
"5 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                //--
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang " +
                //--
                //"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal "+

"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustTambah>0 " +

"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)) end StokAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Retur) end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur " +
                //"CASE WHEN Inventory.ID>0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal "+
"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Retur>0 " +

"order by ItemCode,Urutan";

        }

        
        public string ViewHarianA3(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            return "select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode," +
"case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " +
ketBlnLalu + " as StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID from SaldoInventory where SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in " +
"(select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  Pemasukan," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambah," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurang," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  Retur, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  Pemakaian, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2," +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 " +
"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + "))) as AA " +
"where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) " +

"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Pemasukan end StokAkhir,0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID " +
"from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan,CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID)  END NoDoc FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where Pemasukan>0 " +

"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Retur end StokAkhir " +
",0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where Retur>0 " +

"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
"case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Pemakaian end StokAkhir " +
",DeptID,DeptCode,NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END DeptID, CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID)  END DeptCode,CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END NoDoc, " +
"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
"FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where Pemakaian>0 " +

"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
"case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal " +
",0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-AdjustTambah end StokAkhir " +
",0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where AdjustTambah>0 " +

"union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
"SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal " +
",0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
"case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-AdjustKurang end StokAkhir " +
",0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where AdjustKurang>0 " +
"order by ItemCode,Urutan";

        }

        public string ViewHarianA2(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            return
                //utk saldo awal
            "select ItemID," +
            "case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) " +
            "else '' end ItemCode," +
            "case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) " +
            "else '' end ItemName,0 as UomID," +
            "case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and " +
            "Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SaldoAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
            "from SaldoInventory where SaldoInventory.YearPeriod=" + yearPeriod + " and " +
            "SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID " +
            "from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND " +
            "ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  Pemasukan," +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambah," +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurang," +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN " +
            "(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) " +
            "END  Retur, " +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and " +
            "PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  Pemakaian " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Pemasukan>0 or AdjustKurang>0 or AdjustTambah>0 or Pemakaian>0 or Retur>0) " +
            "union " +

            //utk Pemasukan
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND " +
            "ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
            "CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID) " +
            " END NoDoc " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Pemasukan>0 " +
            "union " +

            //--utk retur
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN " +
            "(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) " +
            "END  Retur " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Retur>0 " +
            "union " +

            //--utk pemakaian
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
            "0 as StokAkhir,DeptID,DeptCode,NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID) " +
            " END DeptID, " +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID) " +
            " END DeptCode," +
            "CASE WHEN Inventory.ID > 0 THEN 	(select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID) " +
            " END NoDoc, " +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and " +
            "PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Pemakaian>0 " +
            "union " +

            //--utk adjustTambah
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where AdjustTambah>0 " +
            "union " +

            //--utk adjustKurang
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where AdjustKurang>0 " +

            "order by ItemCode,Urutan";
        }

        
        public string ViewRekapPakai(string tgl1, string tgl2, int groupID, int deptID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and PakaiDetail.GroupID=" + groupID;

            return "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah,isnull(Harga,0) as Harga,Keterangan,GroupID,GroupDescription,DeptName from (" +
                "SELECT CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,  " +
                "CASE WHEN PakaiDetail.ItemID>0 THEN (SELECT top 1 isnull(POPurchnDetail.Price,0) FROM POPurchn,POPurchnDetail  " +
                "WHERE POPurchn.ID=POPurchnDetail.POID and POPurchnDetail.ItemID=PakaiDetail.ItemID and POPurchnDetail.GroupID=PakaiDetail.GroupID and POPurchn.Status>-1 and " +
                "POPurchnDetail.Status>-1 order by POPurchnDate Desc) else 0 end Harga," +
                "CASE when PakaiDetail.GroupID>0 THEN (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  " +
                "ELSE 'xxx' END AS GroupDescription,  " +
                "PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName FROM Pakai " +
                "INNER JOIN PakaiDetail ON Pakai.ID = PakaiDetail.PakaiID      and PakaiDetail.RowStatus>-1 " +
                "INNER JOIN UOM ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1 " +
                "INNER JOIN Dept ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1 " +
                "where convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and  convert(varchar,Pakai.PakaiDate,112)<=" + tgl2 +
                " and Pakai.DeptID=" + deptID + strGroupID + ") as AA group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Keterangan order by GroupID,ItemCode";
        }

        public string ViewSlipPakai(string strNo)
        {
            return "select row_number() OVER (ORDER BY PakaiID) AS RowNumber,A.PakaiNo,convert(varchar,A.PakaiDate,106) as TglPakai,C.DeptName + ' ' + C.DeptCode as Dept, " +
                   "case B.ItemTypeID " +
                   "When 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                   "When 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                   "When 3 then (select ItemCode from Biaya where ID = B.ItemID) end KodeBarang, " +
                   "case B.ItemTypeID " +
                   "When 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                   "When 2 then (select ItemName from Asset where ID = B.ItemID) "+
                   "When 3 then (select ItemName from Biaya where ID = B.ItemID) end NamaBarang, " +
                   "D.UOMCode as Satuan,B.Quantity as Jumlah,B.Keterangan " +
                   "from Pakai as A,PakaiDetail as B,Dept as C,UOM as D " +
                   "where A.PakaiNo = '" + strNo + "' and B.PakaiID = A.ID and A.DeptID = C.ID " +
                   "and B.UomID = D.ID and B.RowStatus > -1";
        }

        public string ViewSlipRetur(string strNo)
        {
            return "select row_number() OVER (ORDER BY PakaiID) AS RowNumber,A.ReturNo as PakaiNo,convert(varchar,A.ReturDate,106) as " +
                    "TglPakai,C.DeptName + ' ' + C.DeptCode as Dept, " +
                    "case B.ItemTypeID " +
                    "When 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                    "When 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                    "When 3 then (select ItemCode from Biaya where ID = B.ItemID) end KodeBarang, " +
                    "case B.ItemTypeID " +
                    "When 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                    "When 2 then (select ItemName from Asset where ID = B.ItemID) " +
                    "When 3 then (select ItemName from Biaya where ID = B.ItemID) end NamaBarang, " +
                    "D.UOMCode as Satuan,B.Quantity as Jumlah,B.Keterangan " +
                    "from ReturPakai as A,ReturPakaiDetail as B,Dept as C,UOM as D " +
                    "where A.ReturNo = '" + strNo + "' and B.ReturID = A.ID and A.DeptID = C.ID " +
                    "and B.UomID = D.ID and B.RowStatus > -1";
        }

        public string ViewSlipReceipt(string strNo)
        {
            return "select A.ReceiptNo,convert(varchar,A.ReceiptDate,106) as ReceiptDate,C.SupplierName,C.Alamat as Address,B.PONo," +
                    "case B.ItemTypeID " +
                    "when 1 then(select ItemCode from Inventory where Inventory.ID = B.ItemID and Inventory.Aktif = 1) " +
                    "when 2 then(select ItemCode from Asset where Asset.ID = B.ItemID and Asset.Aktif = 1) " +
                    "when 3 then(select ItemCode from Biaya where Biaya.ID = B.ItemID and Biaya.Aktif = 1) end ItemCode, " +
                    "case B.ItemTypeID " +
                    "when 1 then(select ItemName from Inventory where Inventory.ID = B.ItemID and Inventory.Aktif = 1) " +
                    "when 2 then(select ItemName from Asset where Asset.ID = B.ItemID and Asset.Aktif = 1) " +
                    "when 3 then(select ItemName from Biaya where Biaya.ID = B.ItemID and Biaya.Aktif = 1) end ItemName, " +
                    "D.UOMCode,B.Quantity,B.SPPNo,E.PaymentType as CaCr,E.ItemFrom as LoIm,B.Keterangan " +
                    "from Receipt as A,ReceiptDetail as B,SuppPurch as C,UOM as D,POPurchn as E " +
                    "where A.ReceiptNo = '" + strNo + "' and B.ReceiptID = A.ID and A.SupplierId = C.ID " +
                    "and B.UomID = D.ID and A.POID = E.ID";
        }
        
        public string ViewSlipSPP(string strNo)
        {
            return "select TOP 9 A.ID,A.NoSPP,convert(varchar,A.CreatedTime,103) as Tanggal,B.Keterangan,B.Quantity,C.UOMCode,A.CreatedBy,A.DepoID," +
                    "case A.PermintaanType when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Schedule' else '' end TipePermintaan,"+
                    "case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=B.ItemID) "+
                    "when 2 then (select ItemName from Asset where ID=B.ItemID) "+
                    "when 3 then (select ItemName from Biaya where ID=B.ItemID) end Description,"+
                    "case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=B.ItemID) "+
                    "when 2 then (select ItemCode from Asset where ID=B.ItemID) "+
                    "when 3 then (select ItemCode from Biaya where ID=B.ItemID) end ItemCode "+
                    "from SPP as A, SPPDetail as B, UOM as C "+
                    "where A.ID=B.SPPID and B.UOMID=C.ID and B.Status>-1 and A.NoSPP='" + strNo + "'";
        }

        public string ViewLapBul(int userID, int groupID, string awalReport, string akhirReport)
        {
            return "select ItemCode,ItemName,UomCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,isnull([010],0) as [010],isnull([012],0) as [012],"+
                "ISNULL([021],0) as [021], ISNULL([022],0) as [022], ISNULL([031],0) as [031], ISNULL([032],0) as [032], ISNULL([033],0) as [033], "+
                "ISNULL([034],0) as [034], ISNULL([041],0) as [041], ISNULL([042],0) as [042], ISNULL([051],0) as [051], "+
                "ISNULL([052],0) as [052], ISNULL([061],0) as [061], ISNULL([070],0) as [070], ISNULL([091],0) as [091],"+
                "ISNULL([101],0) as [101], ISNULL([111],0) as [111], ISNULL([131],0) as [131], ISNULL([132],0) as [132] , ISNULL([133],0) as [133] "+
                "from (select ItemCode,ItemName,UomCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian,DeptCode "+
                "from LaporanBulanan where UserID="+userID+" and GroupID="+groupID+" and TglCetak>='"+awalReport+"' and TglCetak<='"+akhirReport+"' and " +
                "(StokAwal>0 or Pemasukan>0 or Retur >0 or AdjustTambah > 0 or AdjustKurang > 0 or Pemakaian>0) " +
                ") up pivot (sum(pemakaian) for DeptCode in ([010],[012],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052],[061],[070],[091],"+
                "[101],[111],[131],[132],[133])) as A1 order by ItemCode";
        }

        public string ViewLapBul2ForRepackOnly(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            if (groupID == 4)
            {
                strItemTypeID = " and ItemTypeID = 2";
                strJenisBrg = "Asset";
            }
            if (groupID == 5)
            {
                strItemTypeID = " and ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                strItemTypeID = " and ItemTypeID = 1";
                strJenisBrg = "Inventory";
            }

            return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +

                                "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=" + strJenisBrg + ".ID and Convertan.RowStatus>-1 and " +
                                "convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan, " +


            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + strJenisBrg + ".ID and RowStatus>-1  " + strItemTypeID + " AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1  AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN  " +
            "(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Adjust.status > -1 and " +
            "convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +


            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail WHERE ItemID = " + strJenisBrg + ".ID AND ReturID IN (SELECT ID FROM ReturPakai WHERE ReturPakai.status > -1 AND convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' )) END  Retur, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID 	IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='010') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [010], " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='021') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [021], CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='022') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [022], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='031') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [031], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='032') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [032],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='033') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [033],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='034') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [034],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='041') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [041],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='042') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [042],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='051') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [051],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='052') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [052],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='061') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [061],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='062') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [062],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='070') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [070],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='091') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [091],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='101') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [101],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='111') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [111],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='012') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [012],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='131') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [131],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='132') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [132],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID 		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='133') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [133] FROM  " + strJenisBrg + " INNER JOIN       UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID + ") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";
            }

        public string ViewAsset(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;

            if (groupID == 5)
            {
                strItemTypeID = " and ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }

            return "SELECT ItemCode,ItemName, UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian  from (SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT isnull(sum(" + ketBlnLalu +
            "),0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" +
            yearPeriod + strItemTypeID + ") END StokAwal, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" +
            strJenisBrg + ".ID and RowStatus>-1  " + strItemTypeID + " AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 +
            "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " +
            strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1  AND convert(varchar,AdjustDate,112) >= '" +
            tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " +
            strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN  " +
            "(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Adjust.status > -1 and " +
            "convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail WHERE rowstatus>-1   " + strItemTypeID + " and ItemID = " +
            strJenisBrg + ".ID AND ReturID IN (SELECT ID FROM ReturPakai WHERE ReturPakai.status > -1 AND convert(varchar,ReturDate,112) >= '" +
            tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' )) END  Retur, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + 
            strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID IN( SELECT ID FROM Pakai WHERE Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + 
            "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  Pemakaian FROM  " + strJenisBrg +
            " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" +
            strJenisBrg + ".GroupID = " + groupID + ") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";
        }

        public string ViewLapBul2(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            
            if (groupID == 5)
            {
                strItemTypeID = " and ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            
            //return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052]," +
            //        "[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (" +
            //        "SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + strJenisBrg + ".ID and RowStatus>-1 " + strItemTypeID + " AND ReceiptID IN " +
            //        "		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE RowStatus>-1 " + strItemTypeID + " AND AdjustID IN " +
            //        "		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1 and ItemID = " + strJenisBrg + ".ID AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE RowStatus>-1 " + strItemTypeID + " AND AdjustID IN " +
            //        "		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Adjust.status > -1 and ItemID = " + strJenisBrg + ".ID AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QTY),0) FROM  ReturDetail WHERE  ReturID IN " +
            //        "		(SELECT ID FROM ReturPakai WHERE ID = " + strJenisBrg + ".ID AND ReturPakai.status > -1 AND convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' )) END  Retur," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='010') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [010]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='021') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [021], " +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='022') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [022]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='031') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [031]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='032') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [032]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='033') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [033]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='034') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [034]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='041') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [041]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='042') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [042]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='051') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [051]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='052') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [052]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='061') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [061]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='062') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [062]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='070') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [070]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='091') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [091]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='101') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [101]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='111') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [111]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='012') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [012]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='131') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [131]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID  " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='132') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [132]," +
            //        "CASE WHEN " + strJenisBrg + ".ID > 0 THEN " +
            //        "	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1 " + strItemTypeID + " AND PakaiID " +
            //        "		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='133') " +
            //        "			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [133] " +
            //        "FROM  " + strJenisBrg + " INNER JOIN " +
            //        "      UOM ON " + strJenisBrg + ".UOMID = UOM.ID " +
            //        "WHERE (" + strJenisBrg + ".GroupID = " + groupID + ") " +
            //    //") AS AA where (StokAwal+Pemasukan+Retur+AdjustTambah-AdjustKurang-[010]-[021]-[022]-[031]-[032]-[033]-[034]-[041]-[042]-[051]-[052]-"+
            //    //"[061]-[062]-[070]-[091]-[101]-[111]-[012]-[131]-[132]-[133])>0"+
            //    //" ORDER BY ItemCode";
            //        ") AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode";
           
            return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + strJenisBrg + ".ID and RowStatus>-1  " + strItemTypeID + " AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1  AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN  " +
"(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Adjust.status > -1 and " +
"convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +


"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail WHERE ItemID = " + strJenisBrg + ".ID AND ReturID IN (SELECT ID FROM ReturPakai WHERE ReturPakai.status > -1 AND convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' )) END  Retur, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID 	IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='010') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [010], " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='021') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [021], CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='022') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [022], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='031') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [031], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='032') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [032],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='033') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [033],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='034') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [034],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='041') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [041],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='042') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [042],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='051') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [051],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='052') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [052],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='061') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [061],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='062') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [062],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='070') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [070],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='091') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [091],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='101') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [101],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='111') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [111],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='012') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [012],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='131') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [131],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='132') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [132],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID 		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='133') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [133] FROM  " + strJenisBrg + " INNER JOIN       UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID + ") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";

        }

        public string ViewLapBul2ForAtkOnly(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            if (groupID == 4)
            {
                strItemTypeID = " and ItemTypeID = 2";
                strJenisBrg = "Asset";
            }
            if (groupID == 5)
            {
                strItemTypeID = " and ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                strItemTypeID = " and ItemTypeID = 1";
                strJenisBrg = "Inventory";
            }

            return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + strJenisBrg + ".ID and RowStatus>-1  " + strItemTypeID + " AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1  AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN  " +
"(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Adjust.status > -1 and " +
"convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +


"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail WHERE ItemID = " + strJenisBrg + ".ID AND ReturID IN (SELECT ID FROM ReturPakai WHERE ReturPakai.status > -1 AND convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' )) END  Retur, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID 	IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='010') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [010], " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='021') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [021], CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='022') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [022], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='031') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [031], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='032') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [032],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='033') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [033],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='034') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [034],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='041') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [041],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='042') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [042],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='051') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [051],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='052') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [052],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='061') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [061],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='062') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [062],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='070') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [070],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='091') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [091],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='101') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [101],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='111') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [111],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='012') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [012],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='131') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [131],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='132') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [132],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID 		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='133') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [133] FROM  " + strJenisBrg + " INNER JOIN       UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID + " and left(Inventory.ItemCode,5)='AT-OF' ) ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";

        }

        public string ViewLapBul2stock(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            if (groupID == 4)
            {
                strItemTypeID = " and ItemTypeID = 2";
                strJenisBrg = "Asset";
            }
            if (groupID == 5)
            {
                strItemTypeID = " and ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                strItemTypeID = " and ItemTypeID = 1";
                strJenisBrg = "Inventory";
            }

            return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + strJenisBrg + ".ID and RowStatus>-1  " + strItemTypeID + " AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1  AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN  " +
"(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Adjust.status > -1 and " +
"convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +


"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail WHERE ItemID = " + strJenisBrg + ".ID AND ReturID IN (SELECT ID FROM ReturPakai WHERE ReturPakai.status > -1 AND convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' )) END  Retur, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID 	IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='010') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [010], " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='021') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [021], CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='022') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [022], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='031') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [031], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='032') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [032],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='033') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [033],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='034') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [034],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='041') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [041],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='042') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [042],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='051') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [051],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='052') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [052],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='061') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [061],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='062') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [062],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='070') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [070],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='091') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [091],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='101') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [101],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='111') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [111],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='012') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [012],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='131') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [131],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='132') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [132],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID 		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='133') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [133] FROM  " + strJenisBrg + " INNER JOIN       UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID + " and  " + strJenisBrg + ".Stock = 1" + ") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";

        }

        public string ViewLapBul2nonstock(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            if (groupID == 4)
            {
                strItemTypeID = " and ItemTypeID = 2";
                strJenisBrg = "Asset";
            }
            if (groupID == 5)
            {
                strItemTypeID = " and ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                strItemTypeID = " and ItemTypeID = 1";
                strJenisBrg = "Inventory";
            }


            return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" + strJenisBrg + ".ID and RowStatus>-1  " + strItemTypeID + " AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1  AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = " + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN  " +
"(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Adjust.status > -1 and " +
"convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +


"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail WHERE ItemID = " + strJenisBrg + ".ID AND ReturID IN (SELECT ID FROM ReturPakai WHERE ReturPakai.status > -1 AND convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' )) END  Retur, " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID 	IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='010') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [010], " +

"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='021') AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [021], CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='022') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [022], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='031') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [031], " +
"CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='032') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [032],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='033') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [033],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='034') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [034],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" + strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='041') 			AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [041],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='042') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [042],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='051') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [051],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='052') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [052],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='061') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [061],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='062') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [062],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='070') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [070],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='091') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [091],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='101') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [101],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='111') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [111],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='012') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [012],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='131') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [131],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID  		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='132') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [132],CASE WHEN " + strJenisBrg + ".ID > 0 THEN 	(SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE RowStatus>-1  " + strItemTypeID + " AND PakaiID 		IN( SELECT ID FROM Pakai WHERE DeptID IN (SELECT ID FROM Dept WHERE DeptCode='133') 			AND ItemID=" + strJenisBrg + ".ID AND Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  [133] FROM  " + strJenisBrg + " INNER JOIN       UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID + " and  " + strJenisBrg + ".Stock = 0" + ") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";


        }

        public string ViewHarianBakuBantu(int userID, int groupID, string awalReport, string akhirReport)
        {
            return "select A.ItemCode,A.ItemName,A.UomCode,A.StokAwal,A.Pemasukan,A.Retur,A.AdjustTambah,A.AdjustKurang,A.Pemakaian,A.DeptCode,B.Jumlah as EndingStok,A.GroupID "+
                "from LaporanBulanan as A, Inventory as B where A.ItemID=B.ID and A.UserID="+userID+" and A.GroupID in (1,2) and A.TglCetak>='"+awalReport+"' and A.TglCetak<='"+akhirReport+"' "+
                "and (A.StokAwal+A.Pemasukan+A.Retur+AdjustTambah+A.AdjustTambah+A.Pemakaian+B.Jumlah)>0 order by A.GroupID,A.ItemCode";
        }

        public string ViewHarianBakuBantu3a(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in (" + groupID + ")";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock from (SELECT Inventory.minstock, Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal)>0 or (stokawal+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal)>0 or (stokawal+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
        }


        public string ViewHarianBakuBantu3(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in (" + groupID + ")";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tglAwal + "' AND convert(varchar,createdtime,112) <= '" + tglAkhir + "' ) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+AdjKurangAwal+PakaiAwal)>0 or ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal+AdjKurangAwal+PakaiAwal)+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID ,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Status>-1 and  convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+AdjKurangAwal+PakaiAwal)>0 or ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal+AdjKurangAwal+PakaiAwal)+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
        }

        //
        public string ViewWarningOrdera(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in (" + groupID + ")";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock from (SELECT Inventory.minstock, Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where (stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<minstock ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where (stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<minstock ORDER BY ItemCode";
            }
        }


        public string ViewWarningOrder(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in (" + groupID + ")";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tglAwal + "' AND convert(varchar,createdtime,112) <= '" + tglAkhir + "' ) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<minstock ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID ,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN 		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Status>-1 and  convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN 		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN 		(SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN 	(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN 		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<minstock ORDER BY ItemCode";
            }
        }

        public string ViewHarianBakuBantu2(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in ("+groupID+")";

            return "select ItemCode,ItemName,UOMCode,stokawal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian,(stokawal+Pemasukan+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok,GroupID " +
                "from (SELECT Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN "+
                "	(SELECT cast(isnull("+ketBlnLalu+",0) as decimal) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod="+yearPeriod+" and ItemTypeID=1) END stokawal,"+
                "CASE WHEN Inventory.ID > 0 THEN "+
                "	(SELECT cast(ISNULL(SUM(QUANTITY),0) as decimal) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN "+
                "		(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN "+
                "	(SELECT cast(ISNULL(SUM(QUANTITY),0) as decimal) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN "+
                "		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN "+
                "	(SELECT cast(ISNULL(SUM(QUANTITY),0) as decimal) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN "+
                "		(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN "+
                "	(SELECT cast(ISNULL(SUM(QTY),0) as decimal) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN "+
                "		(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +	
                "CASE WHEN Inventory.ID > 0 THEN "+
                "	(SELECT cast(ISNULL(SUM(Quantity),0) as decimal) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN "+
                "		(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 +"' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN "+
                "      UOM ON Inventory.UOMID = UOM.ID "+
                "WHERE ("+invGroupID+") "+
                ") as AA where stokawal>0 or Pemasukan>0 or AdjustKurang>0 or AdjustTambah>0 or Retur>0 or Pemakaian>0 "+
                "ORDER BY ItemCode";
        }

        public string ViewHarianA(int userID, int groupID, string awalReport)
        {
            //return "select A.ItemCode,A.ItemName,A.UomCode,A.NoDoc,A.StokAwal,A.Pemasukan,A.Retur,A.AdjustTambah,A.AdjustKurang,A.Pemakaian,A.DeptCode,A.StokAkhir,A.GroupID,A.Urutan,((A.StokAwal+A.Pemasukan+A.Retur+A.AdjustTambah)-(A.AdjustKurang+A.Pemakaian)) as SaldoAkhir " +
            //    "from LaporanHarian as A, Inventory as B where A.ItemID=B.ID and A.UserID=" + userID + " and A.GroupID in (1,2) and A.TglCetak>='" + awalReport + "' and A.TglCetak<='" + akhirReport + "' " +
            //    "and (A.StokAwal+A.Pemasukan+A.Retur+AdjustTambah+A.AdjustTambah+A.Pemakaian+A.StokAkhir)>0 order by A.ItemCode,A.Urutan";
            return "select A.ItemCode,A.ItemName,A.UomCode,A.NoDoc,A.StokAwal,A.Pemasukan,A.Retur,A.AdjustTambah,A.AdjustKurang,A.Pemakaian,A.DeptCode,A.StokAkhir,A.GroupID,A.Urutan,A.ID,recid,((A.StokAwal+A.Pemasukan+A.Retur+A.AdjustTambah)-(A.AdjustKurang+A.Pemakaian)) as SaldoAkhir " +
                "from LaporanHarian as A, Inventory as B where A.ItemID=B.ID and A.UserID=" + userID + " and A.GroupID in (" + groupID + ") and A.TglCetak='" + awalReport + "' " +
                "and (A.StokAwal>0 or A.Pemasukan>0 or A.Retur>0 or AdjustTambah>0 or A.AdjustTambah>0 or A.Pemakaian>0 or A.StokAkhir>0 ) order by ItemCode,Urutan,StokAkhir desc";
                //" order by ItemCode,Urutan,StokAkhir desc";
        }

        public string ViewOutstandingSPP(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and B.GroupId =" + groupID;
            else
                strGroupID = " ";

            return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) "+
                "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) "+
                "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID) "+
                "else '' end ItemName," +
                "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead  from SPP as A, SPPDetail as B where A.ID=B.SPPID and " +
                "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=2 and B.ID not in (select sppdetailid from popurchndetail where poid in(select id from popurchn where convert(varchar,createdtime,112) >='" + drTgl + "' )) " +
                " and convert(varchar,A.Minta,112) >='" + drTgl + "' and convert(varchar,A.Minta,112)<='" + sdTgl + "' " + strGroupID + " order by A.NoSPP";
        }

        public string ViewOutstandingSPP1(string drTgl, string sdTgl, string  docPref)
        {
            string strGroupID = string.Empty;
            if (docPref == "0")
                strGroupID = " ";
            else
                strGroupID = " and A.nospp like '%" + docPref + "%' ";

            return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) " +
                "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) " +
                "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID) " +
                "else '' end ItemName," +
                "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead  from SPP as A, SPPDetail as B where A.ID=B.SPPID and " +
                "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=2 and B.ID not in (select sppdetailid from popurchndetail where poid in(select id from popurchn where convert(varchar,createdtime,112) >='" + drTgl + "' )) " +
                " and convert(varchar,A.Minta,112) >='" + drTgl + "' and convert(varchar,A.Minta,112)<='" + sdTgl + "' " + strGroupID + " order by A.NoSPP";
        }

        public string ViewOutstandingPO(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and B.GroupId =" + groupID;
            else
                strGroupID = " ";
            //return "select A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo, " +
            //"C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID,B.Qty as QtyPO,D.Quantity as QtyTerima, " +
            //"B.Qty-D.Quantity as QtySisa,case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " + 
            //"when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " + 
            //"when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID) " + 
            //"else '' end ItemName, UPPER(G.UserName) as NamaHead " +
            //"from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G " +
            //"where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and " +
            //"C.ID=D.ReceiptID and C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and F.HeadID = G.ID " +
            //"and D.RowStatus>-1 and B.Qty-D.Quantity> 0  and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' " +
            //"and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + " order by A.NoPO";

            return  "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,ReceiptNo,ReceiptDate,GroupID, " +
            "QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,QtyTerima,SumQtyTerima,QtyPO-QtyPOCrnt-QtyTerima as QtySisa,ItemName,NamaHead from ( " +
            "select d.ID , A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " +
            "C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID, " +
            "case when B.ItemID>0 then (select ISNULL(SUM(quantity),0) from ReceiptDetail C " +
            "where C.PONo=A.NoPO and C.ItemID=B.ItemID ) end SumQtyTerima, " +
            "case when d.ID >0 then (select isnull(SUM(quantity),0) from ReceiptDetail  " +
            "	where itemid = B.ItemID and POID in(select ID from POPurchn where NoPO=A.NoPO) and ID<d.ID) end QtyPOCrnt, " +
            "B.Qty as QtyPO,D.Quantity as QtyTerima, B.Qty-D.Quantity as QtySisa, " +
            "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            "	when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " +
            "	when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " +
            "	else '' end ItemName,  " +
            "UPPER(G.UserName) as NamaHead " +
            "from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G  " +
            "where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and C.ID=D.ReceiptID and  " +
            "C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and F.HeadID = G.ID and D.RowStatus>-1 and B.Qty-D.Quantity> 0  " +
            "and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + " ) as OutStanding  where qtyPO>SumQtyTerima " + 
            "union " + 
            "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,'-' as ReceiptNo,ReceiptDate,GroupID, " +
            "QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,0 as QtyTerima,0 as SumQtyTerima, QtyPO as QtySisa,ItemName,NamaHead from  " + 
            "(select B.ID , A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " + 
            " '-' as ReceiptNo,'-' as ReceiptDate,B.GroupID,0 as QtyPOCrnt, " + 
            "B.Qty as QtyPO,0 as QtyTerima, 0 as QtySisa, " + 
            "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " + 
            "	when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " + 
            "	when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " + 
            "	else '' end ItemName,  " + 
            "UPPER(G.UserName) as NamaHead " + 
            "from POPurchn as A, POPurchnDetail as B, Supppurch as E,SPP as F, Users as G  " + 
            "where A.ID=B.POID and A.Status>-1 and B.Status>-1 and   A.SupplierID=E.ID and F.ID = B.SPPID  " +
            "and F.HeadID = G.ID and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID +  
            " and B.ID not in(select podetailid from receiptdetail where pono in (select PONo  from Receipt where convert(varchar,receiptdate,112) >='" + drTgl + "' ))) as OutStanding2  " + 
            " order by NoPO,ID ";
        }

        public string ViewOutstandingPO1(string drTgl, string sdTgl, string  groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == "0")
                strGroupID = " ";
            else
                strGroupID = " and A.NoPO like '%" + groupID + "%'";

            return "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,ReceiptNo,ReceiptDate,GroupID, " +
            "QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,QtyTerima,SumQtyTerima,QtyPO-QtyPOCrnt-QtyTerima as QtySisa,ItemName,NamaHead from ( " +
            "select d.ID , A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " +
            "C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID, " +
            "case when B.ItemID>0 then (select ISNULL(SUM(quantity),0) from ReceiptDetail C " +
            "where C.PONo=A.NoPO and C.ItemID=B.ItemID ) end SumQtyTerima, " +
            "case when d.ID >0 then (select isnull(SUM(quantity),0) from ReceiptDetail  " +
            "	where  itemid = B.ItemID and  POID in(select ID from POPurchn where NoPO=A.NoPO) and ID<d.ID) end QtyPOCrnt, " +
            "B.Qty as QtyPO,D.Quantity as QtyTerima, B.Qty-D.Quantity as QtySisa, " +
            "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            "	when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " +
            "	when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " +
            "	else '' end ItemName,  " +
            "UPPER(G.UserName) as NamaHead " +
            "from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G  " +
            "where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and C.ID=D.ReceiptID and  " +
            "C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and F.HeadID = G.ID and D.RowStatus>-1 and B.Qty-D.Quantity> 0  " +
            "and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + " ) as OutStanding  where qtyPO>SumQtyTerima " +
            "union " +
            "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,'-' as ReceiptNo,ReceiptDate,GroupID, " +
            "QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,0 as QtyTerima,0 as SumQtyTerima, QtyPO as QtySisa,ItemName,NamaHead from  " +
            "(select B.ID , A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " +
            " '-' as ReceiptNo,'-' as ReceiptDate,B.GroupID,0 as QtyPOCrnt, " +
            "B.Qty as QtyPO,0 as QtyTerima, 0 as QtySisa, " +
            "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            "	when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " +
            "	when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " +
            "	else '' end ItemName,  " +
            "UPPER(G.UserName) as NamaHead " +
            "from POPurchn as A, POPurchnDetail as B, Supppurch as E,SPP as F, Users as G  " +
            "where A.ID=B.POID and A.Status>-1 and B.Status>-1 and   A.SupplierID=E.ID and F.ID = B.SPPID  " +
            "and F.HeadID = G.ID and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID +
            "and B.ID not in(select podetailid from receiptdetail where pono in (select PONo  from Receipt where convert(varchar,receiptdate,112) >='" + drTgl + "' ))) as OutStanding2  " + 
            " order by NoPO,ID ";
        }

        public string ViewOutstandingPObySup(string drTgl, string sdTgl, string groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == "0")
                strGroupID = " ";
            else
                strGroupID = " and E.SupplierName like '%" + groupID + "%'";

            return "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,ReceiptNo,ReceiptDate,GroupID, " +
            "QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,QtyTerima,SumQtyTerima,QtyPO-QtyPOCrnt-QtyTerima as QtySisa,ItemName,NamaHead from ( " +
            "select d.ID , A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " +
            "C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID, " +
            "case when B.ItemID>0 then (select ISNULL(SUM(quantity),0) from ReceiptDetail C " +
            "where C.PONo=A.NoPO and C.ItemID=B.ItemID ) end SumQtyTerima, " +
            "case when d.ID >0 then (select isnull(SUM(quantity),0) from ReceiptDetail  " +
            "	where  itemid = B.ItemID and  POID in(select ID from POPurchn where NoPO=A.NoPO) and ID<d.ID) end QtyPOCrnt, " +
            "B.Qty as QtyPO,D.Quantity as QtyTerima, B.Qty-D.Quantity as QtySisa, " +
            "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            "	when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " +
            "	when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " +
            "	else '' end ItemName,  " +
            "UPPER(G.UserName) as NamaHead " +
            "from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G  " +
            "where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and C.ID=D.ReceiptID and  " +
            "C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and F.HeadID = G.ID and D.RowStatus>-1 and B.Qty-D.Quantity> 0  " +
            "and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + " ) as OutStanding  where qtyPO>SumQtyTerima " +
            "union " +
            "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,'-' as ReceiptNo,ReceiptDate,GroupID, " +
            "QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,0 as QtyTerima,0 as SumQtyTerima, QtyPO as QtySisa,ItemName,NamaHead from  " +
            "(select B.ID , A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " +
            " '-' as ReceiptNo,'-' as ReceiptDate,B.GroupID,0 as QtyPOCrnt, " +
            "B.Qty as QtyPO,0 as QtyTerima, 0 as QtySisa, " +
            "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            "	when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " +
            "	when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " +
            "	else '' end ItemName,  " +
            "UPPER(G.UserName) as NamaHead " +
            "from POPurchn as A, POPurchnDetail as B, Supppurch as E,SPP as F, Users as G  " +
            "where A.ID=B.POID and A.Status>-1 and B.Status>-1 and   A.SupplierID=E.ID and F.ID = B.SPPID  " +
            "and F.HeadID = G.ID and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID +
            "and B.ID not in(select podetailid from receiptdetail where pono in (select PONo  from Receipt where convert(varchar,receiptdate,112) >='" + drTgl + "' ))) as OutStanding2  " +
            " order by NoPO,ID ";
        }

        public string ViewPOPurchn(int id)
        {
            return "select A.ID,A.POID,B.NoPO,Convert(varchar,B.POPurchnDate,106) as CreatedTime,B.Termin,B.Delivery,C.NoSPP,D.UOMCode,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah, " +
                   "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, " +
                   "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                   "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                   "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end ItemName, " +
                   "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                   "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                   "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode " +
                   "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                   "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID = " + id;
        }

        public string ViewTawarReport(int id)
        {
            return "select CONVERT(varchar(11),TglTawar,113) as TglPenawaran,C.SupplierName as NamaSupplier,C.UP,C.Fax,C.Telepon as Telp, " +
                "A.NoTawar as NoPenawaran,DATENAME(month,A.TglTawar)+' '+DATENAME(YEAR,A.TglTawar) as BulanKirim,D.NoSPP, " +
                "case B.ItemTypeID " +
                "when 1 then(select ItemName from Inventory where ID = B.ItemID) " +
                "when 2 then(select ItemName from Asset where ID = B.ItemID) " +
                "when 3 then(select ItemName from Biaya where ID = B.ItemID) end NamaBarang, " +
                "B.Qty,E.UOMCode as Sat " +
                "from Tawar as A,TawarDetail as B,SuppPurch as C,SPP as D,UOM as E " +
                "where A.ID = B.TawarID and A.SupplierID = C.ID and B.SPPID = D.ID and B.UOMID = E.ID and A.ID = " + id;
        }

        public string ViewRekapSPP(string tgl1, string tgl2)
        {
            //return "SELECT SPP.NoSPP, case SPP.Approval " +
            //        "when 0 then 'Open' when 1 then 'Head' when 2 then 'Manager' when 3 then 'Purchasing' end Approval, " +
            //        "CONVERT(varchar,SPP.ApproveDate1,103) as TglApprove, " +
            //        "Inventory.ItemCode, Inventory.ItemName, SPPDetail.Quantity, " + 
            //        "SPPDetail.Quantity - SPPDetail.QtyPO AS SISA, UOM.UOMCode," +
            //        "SPPDetail.Keterangan, CONVERT(varchar,SPP.CreatedTime,103) as Minta " +
            //        "FROM         SPP INNER JOIN SPPDetail ON SPP.ID = SPPDetail.SPPID INNER JOIN " +
            //        "Inventory ON SPPDetail.ItemID = Inventory.ID INNER JOIN " +
            //        "UOM ON SPPDetail.UOMID = UOM.ID " +
            //        "where convert(varchar,SPP.CreatedTime,112)>='" + tgl1 + "' and  convert(varchar,SPP.CreatedTime,112)<='" + 
            //            tgl2 + "' ORDER BY SPP.NoSPP";

            return "SELECT SPP.NoSPP, case SPP.Approval " +
                   "when 0 then 'Open' when 1 then 'Head' when 2 then 'Manager' when 3 then 'Purchasing' end Approval, " +
                     "CONVERT(varchar,SPP.ApproveDate1,103) as TglApprove,CONVERT(varchar,SPP.LastModifiedTime,113) as LastModified, " +
                     "case SPPDetail.ItemTypeID when 1  then (select ItemName from Inventory where ID=SPPDetail.ItemID and RowStatus > -1)  " +
                     "when 2 then (select ItemName from Asset where ID=SPPDetail.ItemID and RowStatus > -1) " +
                     "else (select ItemName from Biaya where ID=SPPDetail.ItemID and RowStatus > -1) end ItemName, " +
                     "case SPPDetail.ItemTypeID when 1  then (select ItemCode from Inventory where ID=SPPDetail.ItemID and RowStatus > -1) " +
                     "when 2 then (select ItemCode from Asset where ID=SPPDetail.ItemID and RowStatus > -1) " +
                     "else (select ItemCode from Biaya where ID=SPPDetail.ItemID and RowStatus > -1) end ItemCode, " +
                     "SPPDetail.Quantity, " +
                     "SPPDetail.Quantity - SPPDetail.QtyPO AS SISA, UOM.UOMCode, " +
                     "SPPDetail.Keterangan, CONVERT(varchar,SPP.CreatedTime,103) as Minta  " +
                     "FROM SPP INNER JOIN SPPDetail ON SPP.ID = SPPDetail.SPPID and SPP.Status>-1 and SPPDetail.Status>-1 " +
                     "INNER JOIN UOM ON SPPDetail.UOMID = UOM.ID " +
                     "where convert(varchar,SPP.CreatedTime,112)>='" + tgl1 + "' and  convert(varchar,SPP.CreatedTime,112)<='" +
                     tgl2 + "' ORDER BY SPP.NoSPP";
        }

        public string ViewRekapPO(string tgl1, string tgl2)
        {
            //return "SELECT POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.PPN, MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
            //       "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " +
            //       "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
            //       "(SELECT ItemName FROM Biaya WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) END AS ItemName, SPP.NoSPP, POPurchnDetail.Price, POPurchnDetail.Qty,  " +
            //       "UOM.UOMCode, POPurchnDetail.Disc, POPurchnDetail.Price * POPurchnDetail.Qty AS TOTAL, POPurchn.POPurchnDate " +
            //       "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            //       "SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN " +
            //       "UOM ON POPurchnDetail.UOMID = UOM.ID INNER JOIN " +
            //       "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            //       "MataUang ON POPurchn.Crc = MataUang.ID " +
            //        "where  POPurchn.status>-1 and convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 +
            //        "' and  convert(varchar,POPurchn.POPurchndate,112)<='" +
            //        tgl2 + "' order by POPurchn.NoPO";

            return "SELECT POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
              "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " + "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
              "(SELECT ItemName FROM Biaya WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) END AS ItemName, SPP.NoSPP, POPurchnDetail.Price as Price2, POPurchnDetail.Qty,  " +
              "UOM.UOMCode, POPurchn.Disc, case when POPurchn.Disc>0 then " +
              "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
              "else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then " +
              "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
              "else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total, " +
              "POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc " +
              "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID " +
              "INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID " +
              "INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID " +
              "INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
              "convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + tgl2 + "' order by POPurchn.NoPO";
          
        }

        public string ViewRekapReceipt(string tgl1, string tgl2)
        {
            return "SELECT Receipt.PONo, Receipt.ReceiptNo, SuppPurch.SupplierName, POPurchn.PaymentType, POPurchn.ItemFrom, ReceiptDetail.Keterangan," +
                    "CASE ReceiptDetail.ItemTypeID  " +
                    "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = ReceiptDetail.ItemID)  " +
                    "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = ReceiptDetail.ItemID)  " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemCode,  " +
                    "CASE ReceiptDetail.ItemTypeID  " +
                    "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = ReceiptDetail.ItemID)  " +
                    "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = ReceiptDetail.ItemID)  " +
                    "ELSE (SELECT ItemName FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemName,  " +
                    "ReceiptDetail.Quantity,ReceiptDetail.SPPNo, UOM.UOMCode, Receipt.ReceiptDate " +
                    "FROM Receipt INNER JOIN " +
                    "ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID and ReceiptDetail.RowStatus > -1  INNER JOIN " +
                    "POPurchn ON Receipt.POID = POPurchn.ID and ReceiptDetail.RowStatus>-1 INNER JOIN " +
                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                    "UOM ON ReceiptDetail.UomID = UOM.ID " +
                    "where  Receipt.status>-1 and convert(varchar,Receipt.Receiptdate,112)>='" + tgl1 +
                    "' and  convert(varchar,Receipt.Receiptdate,112)<='" + tgl2 + "'";
        }

        public string ViewRekapReceipt2(string tgl1, string tgl2, string grup)
        {
            string strgrup= string.Empty ;
            if (grup != "0")
                if (IsNumeric(grup)==false )
                    strgrup = " and Receipt.receiptno like '%" + grup + "%'  ";
                else
                    strgrup = " and ReceiptDetail.GroupID =  " + grup ;
                    
            else
                strgrup = "";

            return "SELECT Receipt.PONo, Receipt.ReceiptNo, SuppPurch.SupplierName, POPurchn.PaymentType, POPurchn.ItemFrom, MataUang.Nama as CRC ,ReceiptDetail.Keterangan, " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemCode,   " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE (SELECT ItemName FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemName,   " +
                    "ReceiptDetail.Quantity,ReceiptDetail.SPPNo, UOM.UOMCode, UPPER(GroupsPurchn.GroupDescription) as NamaGrup, Receipt.ReceiptDate, " +
                    "ReceiptDetail.PRICE as Harga, ReceiptDetail.disc, ((ReceiptDetail.PRICE * ReceiptDetail.quantity)-((ReceiptDetail.price * ReceiptDetail.disc * ReceiptDetail.quantity) / 100)) as total,popurchn.PPN,ReceiptDetail.Keterangan as remark " +
                    "FROM Receipt INNER JOIN  " +
                    "ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID and ReceiptDetail.RowStatus > -1 " + strgrup + " INNER JOIN  " +
                    "POPurchn ON Receipt.POID = POPurchn.ID and ReceiptDetail.RowStatus>-1 INNER JOIN  " +
                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID  INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID  INNER JOIN  " +
                    "UOM ON ReceiptDetail.UomID = UOM.ID INNER JOIN GroupsPurchn ON ReceiptDetail.GroupID = GroupsPurchn.ID  " +
                    "where  Receipt.status>-1 and convert(varchar,Receipt.Receiptdate,112)>='" + tgl1 + "' and  convert(varchar,Receipt.Receiptdate,112)<='" + tgl2 + "'" ;
        }

        public string ViewRekapReceipt3(string tgl1, string tgl2, string grup)
        {
            string strgrup = string.Empty;
            if (grup != "0")
                if (IsNumeric(grup) == false)
                    strgrup = " and Receipt.receiptno like '%" + grup + "%'  ";
                else
                    strgrup = " and ReceiptDetail.GroupID =  " + grup;
            else
                strgrup = "";
            return "SELECT Receipt.PONo, Receipt.ReceiptNo, SuppPurch.SupplierName, POPurchn.PaymentType, POPurchn.ItemFrom, ReceiptDetail.Keterangan,MataUang.Nama as CRC, " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemCode,   " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE (SELECT ItemName FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemName,   " +
                    "ReceiptDetail.Quantity,ReceiptDetail.SPPNo, UOM.UOMCode, UPPER(GroupsPurchn.GroupDescription) as NamaGrup, Receipt.ReceiptDate, " +
                    "0 as Harga, 0 as disc, 0 as total,0 as PPN,ReceiptDetail.Keterangan as remark " +
                    "FROM Receipt INNER JOIN  " +
                    "ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID and ReceiptDetail.RowStatus > -1 " + strgrup + " INNER JOIN  " +
                    "POPurchn ON Receipt.POID = POPurchn.ID and ReceiptDetail.RowStatus>-1 INNER JOIN  " +
                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN  " +
                    "UOM ON ReceiptDetail.UomID = UOM.ID INNER JOIN GroupsPurchn ON ReceiptDetail.GroupID = GroupsPurchn.ID  " +
                    "where  Receipt.status>-1 and convert(varchar,Receipt.Receiptdate,112)>='" + tgl1 + "' and  convert(varchar,Receipt.Receiptdate,112)<='" + tgl2 + "'";
        }

        public string ViewLapPemantauanPurchn(string drTgl, string sdTgl, int groupID)
        {
            return "select A.NoSPP, Convert(varchar,A.Minta,103) as TglSPP, " +
                   "case A.Approval " +
                   "when 0 then 'user' " +
                   "when 1 then 'head' " +
                   "when 2 then 'manager' " +
                   "when 3 then 'purchasing' end ApprovalSPP, " +
                   "case B.ItemTypeID " +
                   "when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID and RowStatus > -1) " +
                   "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID and RowStatus > -1) " +
                   "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID and RowStatus > -1) end NamaBarang, " +
                   "case B.ItemTypeID " +
                   "when 1 then (select ItemCode from Inventory where B.ItemID=Inventory.ID and RowStatus > -1) " +
                   "when 2 then (select ItemCode from Asset where B.ItemID=Asset.ID and RowStatus > -1) " +
                   "when 3 then (select ItemCode from Biaya where B.ItemID=Biaya.ID and RowStatus > -1) end KodeBarang, G.UOMCode as Satuan, B.Quantity as JumlahSPP, A.CreatedBy as UserName, " +
                   "C.NoPO as NoPO, Convert(varchar,C.POPurchnDate,103) as TglPO, Convert(varchar,C.ApproveDate1,103) as TglApprovalPO, D.Qty as JumlahPO, B.Quantity - D.Qty as SisaSPP, C.Indent, E.ReceiptNo as NoReceipt, " +
                   "case E.Status when 0 then 'Open' when 1 then 'App Head' when 2 then 'Parsial' when 3 then 'Buat Giro' when 4 then 'Serah Terima' when 5 then 'Release' end StatusReceipt,E.status as stReceipt, Convert(varchar,E.ReceiptDate,103) as TglReceipt, " +
                   "Convert(varchar,E.ApproveDate,103) as TglApprovalReceipt, F.Quantity as JumlahReceipt, D.Qty - F.Quantity as SisaPO " +
                   "from SPP as A, SPPDetail as B, POPurchn as C, POPurchnDetail as D, Receipt as E, ReceiptDetail as F, UOM as G " +
                   "where A.ID=B.SPPID and C.ID=D.POID and E.ID=F.ReceiptID and A.ID=D.SPPID  and C.ID=E.POID and B.UOMID=G.ID " +
                   "and D.ID=F.PODetailID and B.ID=D.SppDetailID and A.Status > -1 and B.Status > -1 and C.Status > -1 and D.Status > -1 and E.Status  > -1 and F.RowStatus  > -1 and G.RowStatus > -1 " +
                   "and Convert(varchar,A.Minta,112) >='" + drTgl + "' and Convert(varchar,A.Minta,112) <='" + sdTgl + "' order by C.POPurchnDate";
        }

        public string ViewRekapPakaiDeptItem(string tgl1, string tgl2, int ItemID, int deptID)
        {

            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode, " +
                     "case when B.ItemID>0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and D.ID = B.UomID and B.ItemID = " + ItemID + " and A.DeptID =" + deptID;


        }

        public string ViewRekapPakaiDept(string tgl1, string tgl2, int deptID)
        {

            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate,C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                    "B.Quantity,D.UOMCode,  " +
                     "case when B.ItemID>0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B ,Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and D.ID = B.UomID and A.DeptID =" + deptID;
        }

        public string ViewLapPelarianPlan(string plan, string drTgl, string sdTgl)
        {

            return "select PlanName,(left(NamaType,1) +' '+ Ukuran) as Ukuran,ReguCode,round(isnull([3A],0),0) as [3A],round(isnull([3B],0),0) as [3B],round(isnull([4A],0),0) as [4A],round(isnull([4B],0),0) as [4B],round(isnull([4C],0),0) as [4C],round(isnull([5B],0),0) as [5B],round(isnull([5C],0),0) as [5C],round(isnull([6B],0),0) as [6B],round(isnull([6C],0),0) as [6C],round(isnull([7B],0),0) as [7B], " +
                   "round(isnull([7C],0),0) as [7C],round(isnull([8B],0),0) as [8B],round(isnull([8C],0),0) as [8C],round(isnull([9B],0),0) as [9B],round(isnull([9C],0),0) as [9C],round(isnull([10B],0),0) as [10B],round(isnull([10C],0),0) as [10C],round(isnull([11B],0),0) as [11B],round(isnull([11C],0),0) as [11C],round(isnull([12B],0),0) as [12B], " +
                   " round(isnull([12C],0),0) as [12C],round(isnull([13B],0),0) as [13B],"+
                   " round(isnull([13C],0),0) as [13C],round(isnull([14B],0),0) as [14B],"+
                   " round(isnull([14C],0),0) as [14C],round(isnull([15B],0),0) as [15B],"+
                   " round(isnull([15C],0),0) as [15C],round(isnull([16B],0),0) as [16B],round(isnull([16C],0),0) as [16C],"+
                   " round(isnull([17B],0),0) as [17B],round(isnull([17C],0),0) as [17C],"+
                   " round(isnull([18B],0),0) as [18B],round(isnull([19B],0),0) as [19B],"+
                   " round(isnull([20B],0),0) as [20B],round(isnull([20C],0),0) as [20C],"+
                   " round(isnull([21B],0),0) as [21B],round(isnull([21C],0),0) as [21C],"+
                   " round(isnull([22B],0),0) as [22B],round(isnull([22C],0),0) as [22C],Tebal from ( " +
                   "select * from( " +
                   "select A.KodePelarian as Kode, A.Jumlah,C.PlanName,A.ReguCode,B.Ukuran,A.NamaType,B.Tebal from Pel_Transaksi as A, MasterUkuran as B, Pel_MasterRegu as C " +
                //"where A.IDRegu = C.ID and A.IDUkuran = B.ID and A.RowStatus > -1 and PlanName = '" + plan + "' and A.CreatedTime >= '" + drTgl + "' and A.CreatedTime <= '" + sdTgl + "') as aa ) " +
                   "where A.IDRegu = C.ID and A.IDUkuran = B.ID and A.RowStatus > -1 and PlanName = '" + plan + "' and A.TglTransaksi >= '" + drTgl + "' and A.TglTransaksi <= '" + sdTgl + "') as aa ) " +
                   "up pivot (sum(Jumlah) for Kode in ([3A] ,[3B],[4A],[4B],[4C],[5B],[5C],[6B],[6C],[7B],[7C],[8B],[8C],[9B],"+
                   "[9C],[10B],[10C],[11B],[11C],[12B],[12C],[13B],[13C],[14B],[14C],[15B],[15C],[16B],[16C],"+
                   "[17B],[17C],[18B],[18C],[19B],[19C],[20B],[20C],[21B],[21C],[22B],[22C])) " +
                   " as pvt ";
        }

        public string ViewLapPelarianISO(string plan, string drTgl, string sdTgl)
        {
            return "select *, ([3a]+[3b]+[4a]+[4b]+[4c]+[5b]+[5c]+[6b]+[6c]+[7b]+[7c]+[8b]+[8c]+[9b]+[9c]+[10b]+[10c]+[11b]+[11c]+  " +
                    "[12b]+[12c]+[13b]+[13c]+[14b]+[14c]+[15b]+[15c]+[16b]+[16c]) as Qty from ( " +
                    "select PlanName,(left(NamaType,1) +' '+ Ukuran) as Ukuran,ReguCode, " +
                    "SUM(round(isnull([3A],0),0))as [3A],SUM(round(isnull([3B],0),0)) as [3B],SUM(round(isnull([4A],0),0)) as [4A], " +
                    "SUM(round(isnull([4B],0),0)) as [4B],SUM(round(isnull([4C],0),0)) as [4C],SUM(round(isnull([5B],0),0)) as [5B], " +
                    "SUM(round(isnull([5C],0),0)) as [5C],SUM(round(isnull([6B],0),0)) as [6B],SUM(round(isnull([6C],0),0)) as [6C], " +
                    "SUM(round(isnull([7B],0),0)) as [7B], SUM(round(isnull([7C],0),0)) as [7C],SUM(round(isnull([8B],0),0)) as [8B], " +
                    "SUM(round(isnull([8C],0),0)) as [8C],SUM(round(isnull([9B],0),0)) as [9B],SUM(round(isnull([9C],0),0)) as [9C], " +
                    "SUM(round(isnull([10B],0),0)) as [10B],SUM(round(isnull([10C],0),0)) as [10C],SUM(round(isnull([11B],0),0)) as [11B], " +
                    "SUM(round(isnull([11C],0),0)) as [11C],SUM(round(isnull([12B],0),0)) as [12B],SUM( round(isnull([12C],0),0)) as [12C], " +
                    "SUM(round(isnull([13B],0),0)) as [13B],SUM(round(isnull([13C],0),0)) as [13C],SUM(round(isnull([14B],0),0)) as [14B], " +
                    "SUM(round(isnull([14C],0),0)) as [14C],SUM(round(isnull([15B],0),0)) as [15B],SUM(round(isnull([15C],0),0)) as [15C], " +
                    "SUM(round(isnull([16B],0),0)) as [16B],SUM(round(isnull([16C],0),0)) as [16C],CONVERT(decimal(2,1),Tebal) as Tebal  " +
                    "from (select * from( select A.KodePelarian as Kode, A.Jumlah,C.PlanName,A.ReguCode,B.Ukuran,A.NamaType,B.Tebal " +
                    "from Pel_Transaksi as A, MasterUkuran as B, Pel_MasterRegu as C  " +
                    "where A.IDRegu = C.ID and A.IDUkuran = B.ID and A.RowStatus > -1 and C.PlanName = '" + plan + "' " +
                    "and A.TglTransaksi >= '" + drTgl + "' and A.TglTransaksi <= '" + sdTgl + "') as aa) up pivot  " +
                    "(sum(Jumlah)for Kode in([3A] ,[3B],[4A],[4B],[4C],[5B],[5C],[6B],[6C],[7B],[7C],[8B],[8C],[9B],[9C],[10B], " +
                    "[10C],[11B],[11C],[12B],[12C],[13B],[13C],[14B],[14C],[15B],[15C],[16B],[16C])) as pvt group by ReguCode,Ukuran,NamaType,PlanName,Tebal ) as bb ";

        }
        public string ViewLapPelarianPlanTgl(string plan, string drTgl, string sdTgl)
        {
            return "select PlanName,(left(NamaType,1) +' '+ Ukuran) as Ukuran,KodePelarian as Tebal,ReguCode,round(isnull([1],0),0) as [1],round(isnull([2],0),0) as [2],round(isnull([3],0),0) as [3],round(isnull([4],0),0) as [4],round(isnull([5],0),0) as [5],round(isnull([6],0),0) as [6],round(isnull([7],0),0) as [7],round(isnull([8],0),0) as [8],round(isnull([9],0),0) as [9],round(isnull([10],0),0) as [10], " +
                   "round(isnull([11],0),0) as [11],round(isnull([12],0),0) as [12],round(isnull([13],0),0) as [13],round(isnull([14],0),0) as [14],round(isnull([15],0),0) as [15],round(isnull([16],0),0) as [16],round(isnull([17],0),0) as [17],round(isnull([18],0),0) as [18],round(isnull([19],0),0) as [19],round(isnull([20],0),0) as [20], " +
                   "round(isnull([21],0),0) as [21],round(isnull([22],0),0) as [22],round(isnull([23],0),0) as [23],round(isnull([24],0),0) as [24],round(isnull([25],0),0) as [25],round(isnull([26],0),0) as [26],round(isnull([27],0),0) as [27],round(isnull([28],0),0) as [28],round(isnull([29],0),0) as [29],round(isnull([30],0),0) as [30],round(isnull([31],0),0) as [31] from ( " +
                   "select * from( " +
                   "select DAY(A.TglProduksi) as Tgl, A.Jumlah,NamaType,C.PlanName,A.KodePelarian,A.ReguCode,B.Ukuran from Pel_Transaksi as A, MasterUkuran as B, Pel_MasterRegu as C " +
                   //"where A.IDRegu = C.ID and A.IDUkuran = B.ID and A.RowStatus > -1 and PlanName = '" + plan + "' and A.CreatedTime >= '" + drTgl + "' and A.CreatedTime <= '" + sdTgl + "') as aa ) " +
                   "where A.IDRegu = C.ID and A.IDUkuran = B.ID and A.RowStatus > -1 and PlanName = '" + plan + "' and A.TglProduksi >= '" + drTgl + "' and A.TglProduksi <= '" + sdTgl + "') as aa ) " +
                   "up pivot (sum(Jumlah) for Tgl in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) as pvt " ;
        }

        public string ViewLapPelarianPlanTebal(string plan, string drTgl, string sdTgl)
        {
            return "select PlanName,(left(NamaType,1) +' '+ Ukuran) as Ukuran,KodePelarian as Tebal,ReguCode,round(isnull([1],0),0) as [1],round(isnull([2],0),0) as [2],round(isnull([3],0),0) as [3],round(isnull([4],0),0) as [4],round(isnull([5],0),0) as [5],round(isnull([6],0),0) as [6],round(isnull([7],0),0) as [7],round(isnull([8],0),0) as [8],round(isnull([9],0),0) as [9],round(isnull([10],0),0) as [10], " +
                   "round(isnull([11],0),0) as [11],round(isnull([12],0),0) as [12],round(isnull([13],0),0) as [13],round(isnull([14],0),0) as [14],round(isnull([15],0),0) as [15],round(isnull([16],0),0) as [16],round(isnull([17],0),0) as [17],round(isnull([18],0),0) as [18],round(isnull([19],0),0) as [19],round(isnull([20],0),0) as [20], " +
                   "round(isnull([21],0),0) as [21],round(isnull([22],0),0) as [22],round(isnull([23],0),0) as [23],round(isnull([24],0),0) as [24],round(isnull([25],0),0) as [25],round(isnull([26],0),0) as [26],round(isnull([27],0),0) as [27],round(isnull([28],0),0) as [28],round(isnull([29],0),0) as [29],round(isnull([30],0),0) as [30],round(isnull([31],0),0) as [31] from ( " +
                   "select * from( " +
                   "select DAY(A.TglProduksi) as Tgl, A.Jumlah,NamaType,C.PlanName,A.KodePelarian,A.ReguCode,B.Ukuran from Pel_Transaksi as A, MasterUkuran as B, Pel_MasterRegu as C " +
                   //"where A.IDRegu = C.ID and A.IDUkuran = B.ID and A.RowStatus > -1 and PlanName = '" + plan + "' and A.CreatedTime >= '" + drTgl + "' and A.CreatedTime <= '" + sdTgl + "') as aa ) " +
                   "where A.IDRegu = C.ID and A.IDUkuran = B.ID and A.RowStatus > -1 and PlanName = '" + plan + "' and A.TglTransaksi >= '" + drTgl + "' and A.TglTransaksi <= '" + sdTgl + "') as aa ) " +
                   "up pivot (sum(Jumlah) for Tgl in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) as pvt ";
        }

        public string ViewRekapPakaiItem(string tgl1, string tgl2, int ItemID)
        {

            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode,  " +
                     "case when B.ItemID>0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and D.ID = B.UomID and B.ItemID =" + ItemID;
        }

        public string ViewLapBarang(int dgItemTypeID, int valstock, int valgroup, int valaktif, string tipeBarang)
        {
            string cmdQuery = string.Empty;
            string cmdTipeBarang = string.Empty;
            if (valstock == 0 && valaktif == 0)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }

            if (valstock == 0 && valaktif == 1)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 0 && valaktif == 2)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 0)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Stock = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 1)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 1 and A.Stock = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 2)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 0 and A.Stock = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }

            if (valstock == 2 && valaktif == 0)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Stock = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 2 && valaktif == 1)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 1 and A.Stock = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 2 && valaktif == 2)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 0 and A.Stock = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }

            if (tipeBarang == "Inventory")
            {
                cmdTipeBarang = "from Inventory as A, UOM as B, GroupsPurchn as C ";
            }
            if (tipeBarang == "Asset")
            {
                cmdTipeBarang = "from Asset as A, UOM as B, GroupsPurchn as C ";
            }
            if (tipeBarang == "Biaya")
            {
                cmdTipeBarang = "from Biaya as A, UOM as B, GroupsPurchn as C ";
            }




            return "select A.ItemCode,A.ItemName,A.Jumlah,B.UOMCode,C.GroupDescription, " +
                   "case when A.ID > 1 then isnull((select top 1 StockMax from MinMax where MinMax.ItemID = A.ID),0) else 0 end StockMax, " +
                   "case when A.ID > 1 then isnull((select top 1 StockMin from MinMax where MinMax.ItemID = A.ID),0) else 0 end StockMin, " +
                   "case when A.ID > 1 then isnull((select top 1 ReOrder from MinMax where MinMax.ItemID = A.ID),0) else 0 end ReOrder,A.CreatedBy " + cmdTipeBarang +
                   "where A.UOMID = B.ID and A.GroupID = C.ID and A.RowStatus > -1 " + cmdQuery;
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }	
    }

    
}

