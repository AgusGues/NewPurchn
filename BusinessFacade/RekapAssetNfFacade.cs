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
    public class RekapAssetNfFacade : AbstractTransactionFacade
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

        public static List<RekapAssetNf.ParamData> GetListData()
        {
            List<RekapAssetNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "WITH ReceiptAsset AS ( " +
 "SELECT r.ID,rd.ID ReceiptDetailID,rd.ItemID,rd.UomID " +
 ",rd.ItemTypeID,rd.Quantity,pd.Price,p.Crc,p.NilaiKurs,r.ReceiptDate " +
 ",rd.PODetailID,rd.POID,rd.SPPID,'0'HeadID " +
 "FROM ReceiptDetail rd " +
 "LEFT JOIN Receipt r ON r.ID=rd.ReceiptID " +
 "LEFT JOIN POPurchnDetail pd ON pd.ID=rd.PODetailID " +
 "LEFT JOIN POPurchn p ON p.ID=rd.POID " +
 "WHERE rd.GroupID=4 AND rd.ItemTypeID=2 AND r.Status>-1 AND rd.RowStatus>-1 AND rd.Quantity>0 " +
 ") " +
 ",SaldoAwal AS ( " +
 "SELECT ItemID,ItemTypeID,(Select dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode, " +
 "(Select dbo.ItemNameInv(ItemID,ItemTypeID))ItemName, " +
 "(Select dbo.SatuanInv(ItemID,ItemTypeID))Unit,'SaldoAwal' AdjustType,JanQty Quantity,'SaldoAwal' Jenis ,'0'HeadID " +
 "FROM SaldoInventory WHERE GroupID=4 AND ItemTypeID=2 AND JanQty>0 AND YearPeriod=2010 " +
 ") " +
 ",ReceiptAsset2 AS ( " +
 "SELECT ItemID,ItemTypeID,(Select dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode, " +
 "(Select dbo.ItemNameInv(ItemID,ItemTypeID))ItemName, " +
 "(Select dbo.SatuanInv(ItemID,ItemTypeID))Unit,'Receipt' AdjustType,SUM(Quantity)Quantity,'Pembelian' Jenis,'0'HeadID FROM ReceiptAsset " +
 "GROUP BY ItemID,ItemTypeID " +
 ") " +
 ",AdjustIN AS ( " +
 "SELECT ItemID,ad.ItemTypeID,(Select dbo.ItemCodeInv(ItemID,ad.ItemTypeID))ItemCode, " +
 "(Select dbo.ItemNameInv(ItemID,ad.ItemTypeID))ItemName, " +
 "(Select dbo.SatuanInv(ItemID,ad.ItemTypeID))Unit,a.AdjustType,SUM(ad.Quantity)Quantity,'AdjustIN' Jenis,(select top 1 isnull(ast.Head,0) from asset ast where ast.ID=ad.ItemID and ad.ItemTypeID=ast.ItemTypeID and ast.RowStatus>-1)HeadID " +
 "FROM AdjustDetail ad " +
 "LEFT JOIN Adjust a ON a.ID=ad.adjustID " +
 "WHERE a.AdjustType='Tambah' AND ad.GroupID in (4) AND ad.ItemTypeID=2 AND ad.Apv >=1 AND Ad.RowStatus>-1 " +
 "Group By ItemID,ad.ItemTypeID,a.AdjustType " +
 ") " +
 ",AdjustOUT AS ( " +
 "SELECT ItemID,ad.ItemTypeID,(Select dbo.ItemCodeInv(ItemID,ad.ItemTypeID))ItemCode, " +
 "(Select dbo.ItemNameInv(ItemID,ad.ItemTypeID))ItemName, " +
 "(Select dbo.SatuanInv(ItemID,ad.ItemTypeID))Unit,a.AdjustType,SUM(ad.Quantity)Quantity,'AdjustOut' Jenis,(select top 1 isnull(ast.Head,0) from asset ast where ast.ID=ad.ItemID and ad.ItemTypeID=ast.ItemTypeID and ast.RowStatus>-1)HeadID " +
 "FROM AdjustDetail ad " +
 "LEFT JOIN Adjust a ON a.ID=ad.adjustID " +
 "WHERE a.AdjustType='Kurang' AND ad.GroupID in (4) AND ad.ItemTypeID=2 AND ad.Apv >=1 AND Ad.RowStatus>-1 " +
 "Group By ItemID,ad.ItemTypeID,a.AdjustType " +
 ") " +
 ",Disposal AS ( " +
 "SELECT ItemID,ad.ItemTypeID,(Select dbo.ItemCodeInv(ItemID,ad.ItemTypeID))ItemCode, " +
 "(Select dbo.ItemNameInv(ItemID,ad.ItemTypeID))ItemName, " +
 "(Select dbo.SatuanInv(ItemID,ad.ItemTypeID))Unit,a.AdjustType,SUM(ad.Quantity)Quantity,'Disposal' Jenis,'0'HeadID " +
 "FROM AdjustDetail ad " +
 "LEFT JOIN Adjust a ON a.ID=ad.adjustID " +
 "WHERE a.AdjustType='Disposal' AND ad.GroupID in (4) AND ad.ItemTypeID=2 AND ad.Apv >=1 AND Ad.RowStatus>-1 " +
 "Group By ItemID,ad.ItemTypeID,a.AdjustType " +
 ") " +
 ",SPBAsset AS( " +
 "SELECT ItemID,pd.ItemTypeID,(Select dbo.ItemCodeInv(ItemID,pd.ItemTypeID))ItemCode, " +
 "(Select dbo.ItemNameInv(ItemID,pd.ItemTypeID))ItemName, " +
 "(Select dbo.SatuanInv(ItemID,pd.ItemTypeID))Unit,'Pemakaian' AdjustType, " +
 "SUM(Quantity)Quantity,'SPB' Jenis ,'0'HeadID " +
 "FROM PakaiDetail pd " +
 "LEFT JOIN Pakai p ON p.ID=pd.PakaiID " +
 "WHERE pd.GroupID=4 AND pd.ItemTypeID=2 AND p.Status=2 AND pd.RowStatus>-1 " +
 "Group By pd.ItemID,pd.ItemTypeID " +
 ") " +
 ",DataAsset AS ( " +
 "SELECT ItemCode,ItemName,Unit,HeadID,SUM(ISNULL([SaldoAwal],0))SaldoAwal,SUM(ISNULL([Pembelian],0))Pembelian,SUM(ISNULL([AdjustIN],0))AdjustIN, " +
 "SUM(ISNULL([AdjustOut],0))AdjustOut,SUM(ISNULL([Disposal],0))Disposal,SUM(ISNULL([SPB],0))SPB  " +
 "FROM ( " +
 "SELECT * FROM SaldoAwal " +
 "UNION ALL " +
 "SELECT * FROM ReceiptAsset2  " +
 "UNION ALL  " +
 "SELECT * FROM AdjustIN " +
 "UNION ALL  " +
 "SELECT * FROM AdjustOUT " +
 "UNION ALL  " +
 "SELECT * FROM Disposal " +
 "UNION ALL " +
 "SELECT * FROM SPBAsset " +
 ") As x " +
 "PIVOT (SUM(Quantity) FOR Jenis IN([SaldoAwal],[Pembelian],[AdjustIN],[AdjustOut],[Disposal],[SPB])) As PV " +
 "GROUP BY ItemCode,ItemName,Unit,HeadID " +
 ") " +
 "SELECT ItemCode,ItemName,Unit,SaldoAwal,Pembelian,AdjustIN,(AdjustOut+Disposal)AdjustOut,StockAkhir "+ "SaldoAkhir,SPB,StockGudang ,case when HeadID in (0,2) then 'Tunggal' when HeadID=1 then 'BerKomponen' end Kategori " +
        "FROM( " +
           " SELECT *, (SaldoAwal + Pembelian + AdjustIN - AdjustOUT - Disposal)StockAkhir, ((SaldoAwal + Pembelian + " + "AdjustIN) - (SPB + AdjustOut + Disposal))StockGudang FROM DataAsset "+
            ") as xx " +
        "WHERE ItemName IS NOT NULL " +
        "ORDER By ItemName, ItemCode";
                    AllData = connection.Query<RekapAssetNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
    }
}
