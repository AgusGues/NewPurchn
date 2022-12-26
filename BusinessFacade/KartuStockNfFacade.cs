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
    public class KartuStockNfFacade : AbstractTransactionFacade
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

        public static List<KartuStockNf.ParamItem> GetListItem(int StatusItem, string ItemName)
        {
            List<KartuStockNf.ParamItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string aktif = "";
                if (StatusItem == 1) { aktif = " and A.aktif >=1 "; }
                string query;
                try
                {
                    query = "SELECT A.ID,A.ItemCode,'(Inventory)' + A.ItemName as ItemName,A.ItemTypeID from Inventory as A,UOM as C where A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + ItemName + "%' " + aktif +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,'(Asset)'+ A.ItemName as ItemName,A.ItemTypeID from Asset as A,UOM as C where A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + ItemName + "%' " + aktif +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,'(Biaya)'+A.ItemName as ItemName,A.ItemTypeID from Biaya as A,UOM as C where A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + ItemName + "%' " + aktif + " order by itemname";
                    AllData = connection.Query<KartuStockNf.ParamItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<KartuStockNf.ParamInfoItem> GetInfoItem(string strbar)
        {
            List<KartuStockNf.ParamInfoItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT A.ItemCode,A.ItemName ,A.Reorder, C.UOMDesc,A.MinStock,A.MaxStock from Inventory as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strbar + "'" +
                 "UNION " +
                 " SELECT A.ItemCode,A.ItemName ,A.Reorder, C.UOMDesc,A.MinStock,A.MaxStock from Asset as A,UOM as C where A.aktif >=1 and A.RowStatus>-1 and A.UOMID = C.ID and  " + strbar + "'" +
                 "UNION " +
                 " SELECT A.ItemCode,A.ItemName ,A.Reorder, C.UOMDesc,A.MinStock,A.MaxStock from Biaya as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strbar + "'";
                    AllData = connection.Query<KartuStockNf.ParamInfoItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<KartuStockNf.ParamData> GetListData(string strsql)
        {
            List<KartuStockNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = strsql;
                    AllData = connection.Query<KartuStockNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static KartuStockNf.ParamInv KartuStockItemID(string strbar)
        {
            KartuStockNf.ParamInv AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT A.ID,A.ItemCode,A.ItemName ,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strbar + "'" +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode, A.ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,A.LeadTime from Asset as A,UOM as C where A.aktif >=1 and A.RowStatus>-1 and A.UOMID = C.ID and  " + strbar + "'" +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,A.ItemName ,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,A.LeadTime from Biaya as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strbar + "'";
                    AllData = connection.QueryFirstOrDefault<KartuStockNf.ParamInv>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string ViewKartuStock(string tgl1, string tgl2, string itemid, 
            string itemtypeid, string tglSA, string yearSA, string monthSA)
        {
            string strSQL;
            strSQL = "SELECT * FROM (select 0 as Tipe,1 as urut, '0' as id,cast('" + tglSA + "' as DATE) as tanggal, '-' as Faktur," + monthSA + " as masuk,0 as keluar,'Saldo Awal' as keterangan from SaldoInventory where YearPeriod =" + yearSA + " and ItemID = '" + itemid + "' and ItemTypeID=" + itemtypeid +
            "union " +

            "SELECT 1 as Tipe,1 as urut,convert(char(8),Receipt.ReceiptDate ,112) + '1' + CAST(ReceiptDetail.ID as CHAR(10))as id, " +
            "Receipt.ReceiptDate, Receipt.ReceiptNo, ReceiptDetail.Quantity AS masuk, 0 AS keluar, SuppPurch.SupplierName AS keterangan " +
            "FROM ReceiptDetail INNER JOIN Receipt ON ReceiptDetail.ReceiptID = Receipt.ID INNER JOIN SuppPurch ON Receipt.SupplierId = SuppPurch.ID " +
            "WHERE (ReceiptDetail.ItemTypeID = " + itemtypeid + ") AND (ReceiptDetail.ItemID = " + itemid +
            ") AND (ReceiptDetail.RowStatus >= 0) AND (Receipt.Status >= 0) AND (convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 +
            "' AND convert(varchar,Receipt.ReceiptDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 1 as Tipe,1 as urut,CAST((convert(char(8),convertan.createdtime ,112) + '1' + " +
            "(RIGHT('000000000'+RTRIM(CAST(convertan.ID as CHAR)),10)))AS bigint) as id, convertan.createdtime, convertan.RepackNo,  " +
            "convertan.toqty AS masuk, 0 AS keluar, '-' AS keterangan " +
            "FROM convertan WHERE (convertan.toItemID = " + itemid +
            ") AND (convertan.RowStatus >= 0) AND (convert(varchar,convertan.createdtime,112) >= '" + tgl1 +
            "' AND convert(varchar,convertan.createdtime,112) < '" + tgl2 + "') " +
            "union " +

            "SELECT 2 as Tipe,1 as urut,convert(char(8),Pakai.PakaiDate ,112) + '2' + CAST(PakaiDetail.ID as CHAR(10))as id,Pakai.PakaiDate, Pakai.PakaiNo, 0 as masuk,PakaiDetail.Quantity as keluar, Dept.DeptCode as keterangan " +
            "FROM PakaiDetail INNER JOIN Pakai ON PakaiDetail.PakaiID = Pakai.ID INNER JOIN Dept ON Pakai.DeptID = Dept.ID " +
            "WHERE (PakaiDetail.ItemTypeID = " + itemtypeid + ") AND (PakaiDetail.ItemID = " + itemid + ") AND (PakaiDetail.RowStatus >= 0) AND (Pakai.Status >= 3) AND (convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 3 as Tipe,1 as urut,CONVERT(char(8), Adjust.AdjustDate, 112) + '3'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, 'Adjust In -' +AdjustDetail.keterangan, AdjustDetail.Quantity AS masuk, 0 AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE AdjustDetail.apv>0 and  (Adjust.AdjustType  = 'Tambah') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) <= '" + tgl2 + "') " +
            "union " +
            "SELECT 4 as Tipe,1 as urut,CONVERT(char(8), Adjust.AdjustDate, 112) + '4'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, 'Adjust Out -' + AdjustDetail.keterangan, 0 AS masuk, AdjustDetail.Quantity AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE AdjustDetail.apv>0 AND (nonstok IS null OR nonstok != 1) and  (Adjust.AdjustType  = 'Kurang') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 5 as Tipe,1 as urut,CONVERT(char(8), returpakai.returDate, 112) + '5'  + CAST(returpakaiDetail.ID as CHAR(10))AS id, returpakai.returDate, returpakai.returNo,returpakaiDetail.Quantity AS masuk, 0 AS keluar,'Retur Pakai' as keterangan " +
            "FROM returpakaiDetail INNER JOIN returpakai ON returpakaiDetail.returID = returpakai.ID " +
            "WHERE (returpakaiDetail.ItemTypeID = " + itemtypeid + ") AND (returpakaiDetail.ItemID = " + itemid + ") AND (returpakaiDetail.RowStatus >= 0) AND (returpakai.Status >= 0) AND (convert(varchar,returpakai.returDate,112) >= '" + tgl1 + "' AND convert(varchar,returpakai.returDate,112) < '" + tgl2 + "') " +
            "UNION ALL " +
            "SELECT 6 as Tipe,2 as urut, CAST((convert(char(8),GETDATE() ,112) + '6' + (RIGHT('000000000'+RTRIM(CAST(pd.ItemID as CHAR)),10)))AS bigint) as id, cast('" + tglSA + "' as DATE) as tanggal," +
            "(select dbo.ItemCodeInv(itemid,pd.ItemTypeID)) ItemCode,0 AS masuk, SUM(Quantity) AS keluar,'Ending Stock -'+ p.PakaiNo as keterangan " +
            "FROM PakaiDetail pd LEFT JOIN pakai p on p.ID=pd.PakaiID " +
            "WHERE itemID= " + itemid + " and pd.ItemTypeID= " + itemtypeid + "  AND Status in(0,1,2) AND " +
            "RowStatus>-1 GROUP by ItemID,pd.ItemTypeID,p.PakaiNo ) as x order by Tanggal,Tipe ";
            return strSQL;
        }

        public static string ViewKartuStockRepack(string tgl1, string tgl2, string itemid, string itemtypeid, string tglSA, string yearSA, string monthSA)
        {
            string strSQL;
            strSQL =
            "select 0 as Tipe,1 as urut,'0' as id,cast('" + tglSA + "' as DATE) as tanggal, '-' as Faktur," + monthSA + " as masuk,0 as keluar,'saldo awal' as keterangan from SaldoInventory where YearPeriod =" + yearSA + " and ItemID = '" + itemid + "' and ItemTypeID=" + itemtypeid +
            "union " +
            "SELECT 1 as Tipe,1 as urut,CAST((convert(char(8),convertan.createdtime ,112) + '1' + (RIGHT('000000000'+RTRIM(CAST(convertan.ID as CHAR)),10)))AS bigint) as id, convertan.createdtime, convertan.RepackNo, convertan.toqty AS masuk, 0 AS keluar, '-' AS keterangan " +
            "FROM convertan " +
            "WHERE (convertan.toItemID = " + itemid + ") AND (convertan.RowStatus >= 0) AND (convert(varchar,convertan.createdtime,112) >= '" + tgl1 + "' AND convert(varchar,convertan.createdtime,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 2 as Tipe,1 as urut,CAST((convert(char(8),Pakai.PakaiDate ,112) + '2' + (RIGHT('000000000'+RTRIM(CAST(PakaiDetail.ID as CHAR)),10)))AS bigint) as id,Pakai.PakaiDate, Pakai.PakaiNo, 0 as masuk,PakaiDetail.Quantity as keluar, Dept.DeptCode as keterangan " +
            "FROM PakaiDetail INNER JOIN Pakai ON PakaiDetail.PakaiID = Pakai.ID INNER JOIN Dept ON Pakai.DeptID = Dept.ID " +
            "WHERE (PakaiDetail.ItemTypeID = " + itemtypeid + ") AND (PakaiDetail.ItemID = " + itemid + ") AND (PakaiDetail.RowStatus >= 0) AND (Pakai.Status >= 3) AND (convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 3 as Tipe,1 as urut,CAST((convert(char(8),Adjust.AdjustDate ,112) + '3' + (RIGHT('000000000'+RTRIM(CAST(AdjustDetail.ID as CHAR)),10)))AS bigint) as id, Adjust.AdjustDate, 'Adjust In ' +AdjustDetail.keterangan, AdjustDetail.Quantity AS masuk, 0 AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE  AdjustDetail.apv>0 and (Adjust.AdjustType  = 'Tambah') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) <= '" + tgl2 + "') " +
            "union " +
            "SELECT 4 as Tipe,1 as urut,CAST((convert(char(8),Adjust.AdjustDate ,112) + '4' + (RIGHT('000000000'+RTRIM(CAST(AdjustDetail.ID as CHAR)),10)))AS bigint) as id, Adjust.AdjustDate, 'Adjust Out ' + AdjustDetail.keterangan, 0 AS masuk, AdjustDetail.Quantity AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE  AdjustDetail.apv>0 and (Adjust.AdjustType  = 'Kurang') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 5 as Tipe,1 as  urut,CAST((convert(char(8),returpakai.returDate ,112) + '5' + (RIGHT('000000000'+RTRIM(CAST(returpakaiDetail.ID as CHAR)),10)))AS bigint) as id, returpakai.returDate, returpakai.returNo,returpakaiDetail.Quantity AS masuk, 0 AS keluar,'retur pakai' as keterangan " +
            "FROM returpakaiDetail INNER JOIN returpakai ON returpakaiDetail.returID = returpakai.ID " +
            "WHERE (returpakaiDetail.ItemTypeID = " + itemtypeid + ") AND (returpakaiDetail.ItemID = " + itemid + ") AND (returpakaiDetail.RowStatus >= 0) AND (returpakai.Status >= 0) AND (convert(varchar,returpakai.returDate,112) >= '" + tgl1 + "' AND convert(varchar,returpakai.returDate,112) < '" + tgl2 + "') " +
            //"union select CONVERT(char(8), GETDATE(), 112) + '4'  + CAST(ID as CHAR(10))AS id, " +
            //"cast('" + tglSA + "' as DATE) as tanggal, ItemCode,0 AS masuk, JmlTransit AS keluar,'Ending Stock' as keterangan  " +
            //"from Inventory where ID = " + itemid + " and ItemTypeID= " + itemtypeid + " and RowStatus>-1 and JmlTransit>0 ";
            "UNION ALL " +
            "SELECT 6 as Tipe,1 as urut, CAST((convert(char(8),GETDATE() ,112) + '6' + (RIGHT('000000000'+RTRIM(CAST(PakaiDetail.ItemID as CHAR)),10)))AS bigint) as id, cast('" + tglSA + "' as DATE) as tanggal," +
            "(select dbo.ItemCodeInv(itemid,ItemTypeID)) ItemCode,0 AS masuk, SUM(Quantity) AS keluar,'Ending Stock' as keterangan " +
            "FROM PakaiDetail WHERE itemID= " + itemid + " and ItemTypeID= " + itemtypeid + " and PakaiID in(" +
            "SELECT ID FROM Pakai WHERE LEFT(convert(varchar,PakaiDate,112),6)='" + yearSA + tglSA.Substring(0, 2) + "' AND Status BETWEEN 0 AND 1) AND " +
            "RowStatus>-1 GROUP by ItemID,ItemTypeID ";
            return strSQL;
        }

    }

}
