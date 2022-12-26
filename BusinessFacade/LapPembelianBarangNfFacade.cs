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
    public class LapPembelianBarangNfFacade : AbstractTransactionFacade
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

        public static List<LapPembelianBarangNf.ParamLaporanItem> GetListLaporanItem()
        {
            List<LapPembelianBarangNf.ParamLaporanItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID,GroupDescription from GroupsPurchn where RowStatus >-1";
                    AllData = connection.Query<LapPembelianBarangNf.ParamLaporanItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<LapPembelianBarangNf.ParamData> GetListData(string ketQtyBlnLalu, string ketAvgBlnLalu, int yearPeriod, string thBl, int itemTypeID, int groupID)
        {
            List<LapPembelianBarangNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    string[] arrDept = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewPriceKertas", "COGS").Split(',');
                    query = "select ItemID,ItemCode,ItemName,UOMCode," + ketQtyBlnLalu + " as QtySaldo," + ketAvgBlnLalu + " as HppSaldo," + ketQtyBlnLalu + "*" + ketAvgBlnLalu + " as TotSaldo," +
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
                        "end TotAvgPrice,FakturPajak " +

                        "from(select A0.ItemID,A1.ItemCode,A1.ItemName,A2.UOMCode,cast(isnull(A0." + ketQtyBlnLalu + ",0) as decimal(16,2)) as " + ketQtyBlnLalu + ",cast(isnull(A0." + ketAvgBlnLalu + ",0) as decimal) as " + ketAvgBlnLalu + ", " +
                        "case when A0.ItemID>0 then (select top 1 A.FakturPajak  " +
                        "from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1   " +
                        "and B.RowStatus>-1 and B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "'  " +
                        "and B.ItemID=A0.ItemID) else '' end FakturPajak, " +
                        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                        "(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total " +
                        "from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and " +
                        "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyReceipt," +                       
                        "case when A0.ItemID>0 then (select cast(isnull(SUM(NetPrice)/SUM(Quantity),0) as decimal(16,2)) from (select B.ItemID,B.Quantity,B.Price*B.Quantity as Total," +
                        "case when C.Disc>0 then (D.Price*B.Quantity)-((D.Price*B.Quantity)*(C.Disc/100)) else " +
                        "case when D.Price>0 and C.Disc =0 then D.Price*B.Quantity else ";
                    if (arrDept.Contains(((Users)HttpContext.Current.Session["Users"]).DeptID.ToString()))
                    {
                        query += "((ISNULL((select top 1 Harga from HargaKertas where ItemID=D.ItemID and SupplierID=C.SupplierID order by ID desc),0)*B.Quantity)) ";
                    }
                    else
                    {
                        query += "0 ";
                    }
                    query += "End End NetPrice " +
                        "from Receipt as A,ReceiptDetail as B,POPurchn as C,POPurchnDetail as D " +
                        "where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and B.POID=C.ID and B.PODetailID=D.ID and C.ID=D.POID and C.Status>-1 " +
                        "and D.Status>-1 and B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' " +
                        "and B.ItemID=A0.ItemID ) as P) else 0 end AvgPriceReceipt," +

                        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                        "from Pakai as A,PakaiDetail as B where A.ID=B.PakaiID and A.Status>-1  and B.RowStatus>-1 and " +
                        "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyPakai," +

                        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                        "from Adjust as A,AdjustDetail as B where B.apv>0 and  A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                        "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Tambah' and B.ItemID=A0.ItemID) as P ) else 0 " +
                        "end QtyAdjustTambah," +

                        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                        "from Adjust as A,AdjustDetail as B where B.apv>0 and A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                        "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Kurang' and B.ItemID=A0.ItemID) as P ) else 0 " +
                        "end QtyAdjustKurang," +
                        "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                        "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                        "from ReturPakai as A,ReturPakaiDetail as B where A.ID=B.ReturID and A.Status>-1 and " +
                        "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReturDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyRetur " +
                        "from SaldoInventory as A0,Inventory as A1, UOM as A2 where A1.ID=A0.ItemID and A1.GroupID=" + groupID + " and A1.ItemTypeID=" + itemTypeID + " and A1.UOMID=A2.ID and " +
                        "A0.YearPeriod=" + yearPeriod + " and A0.ItemTypeID=" + itemTypeID + " and A0.GroupID=" + groupID + ") as A1 " +
                        "where (" + ketQtyBlnLalu + " + QtyReceipt + QtyPakai + QtyAdjustTambah + QtyAdjustKurang + QtyRetur)>0 order by ItemCode";
                    AllData = connection.Query<LapPembelianBarangNf.ParamData>(query).ToList();
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
