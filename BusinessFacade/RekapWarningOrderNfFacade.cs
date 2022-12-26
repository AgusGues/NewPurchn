using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class RekapWarningOrderNfFacade : AbstractTransactionFacade
    {
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public static List<RekapWarningOrderNf.ParamTypeItem> GetListTypeItem()
        {
            List<RekapWarningOrderNf.ParamTypeItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID,GroupDescription from GroupsPurchn where RowStatus >-1";
                    AllData = connection.Query<RekapWarningOrderNf.ParamTypeItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapWarningOrderNf.ParamData> GetListData(string strQuery)
        {
            List<RekapWarningOrderNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = strQuery;
                    AllData = connection.Query<RekapWarningOrderNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string ViewWarningOrdera(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            //if (groupID == 1 || groupID == 2)
            //{
            //    invGroupID = "Inventory.GroupID in (1,2)";
            //}
            //else
            invGroupID = "Inventory.GroupID in (" + groupID + ") and Inventory.Aktif=1 and stock=1 and MinStock>=0 ";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock,reorder from (SELECT Inventory.reorder,Inventory.minstock, Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where (stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock ,reorder from (SELECT Inventory.reorder, Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN  UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where (stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
        }
        public static string ViewWarningOrder(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            //if (groupID == 1 || groupID == 2)
            //{
            //    invGroupID = "Inventory.GroupID in (1,2)";
            //}
            //else
            invGroupID = "Inventory.GroupID in (" + groupID + ") and Inventory.Aktif=1 and Stock=1 and MinStock>=0 ";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID,MinStock ,reorder from (SELECT Inventory.reorder, Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tglAwal + "' AND convert(varchar,createdtime,112) <= '" + tglAkhir + "' ) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID ,MinStock ,reorder from (SELECT Inventory.reorder, Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Status>-1 and  convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
        }
    }
}
