using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace Cogs
{
    public class COGSHpp : AbstractFacade3
    {
        private HPP objHpp = new HPP();
        private ArrayList arrHPP = new ArrayList();
        private List<SqlParameter> sqlListParam;

        public COGSHpp(object Domain)
            : base()
        {

        }
        public COGSHpp()
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
        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public decimal GetHarga(int groupID, int Bulan, int Tahun)
        {
            /** Gunakan query report accounting rekap*/
            #region Query Lama
            string strSQL = "select SUM(Total) as Harga from ( " +
                          "  select ItemCode,sum(jumlah) as jml, AVG(harga) as avgPrice,(sum(jumlah)*AVG(harga)) as Total from( " +
                          "  select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                          "   case when crc >1 THEN " +
                          "   ((select Top 1 Kurs from MataUangKurs where MUID=crc and  sdTgl<=PakaiDate order by ID desc)*Harga) " +
                          "   else " +
                          "   isnull(Harga,0) end Harga,Keterangan,GroupID,GroupDescription,DeptName,Status, " +
                          "   crc,ItemID " +
                          "    from  " +
                          "  (SELECT  " +
                          "      isnull((select top 1 crc from POPurchn where POPurchn.ID in(select (POID) from ReceiptDetail where ReceiptDetail.ItemID=PakaiDetail.ItemID  " +
                          "      and ReceiptID in(select ID from Receipt where month(receipt.ReceiptDate) <= month(pakai.PakaiDate) and  " +
                          "      YEAR(receipt.ReceiptDate)<=YEAR(pakai.PakaiDate ))) order by POPurchn.ID),1)as crc, " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                          "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                          "      CASE WHEN PakaiDetail.ItemID>0 THEN  " +

                          "           CASE WHEN PakaiDetail.ItemID IN (16868,35735,16868,16869,16871,16872,16873,16874) THEN " +
                          "              (SELECT top 1 hb.HargaSatuan From TabelHargaBankOut hb where hb.ItemID=PakaiDetail.ItemID order by hb.ID desc) " +
                          "           ELSE " +
                          "              ISNULL((select Top 1 Price from ReceiptDetail where ItemID=PakaiDetail.ItemID and ReceiptDetail.ReceiptID in  " +
                          "              (select ID from Receipt where MONTH(Receipt.ReceiptDate)=MONTH(Pakai.PakaiDate) and YEAR(Receipt.ReceiptDate)=YEAR(Pakai.PakaiDate)) " +
                          "              order by ReceiptDetail.ID Desc), " +
                          "              (select top 1 MeiAvgprice From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=YEAR(Pakai.PakaiDate))) " +
                          "           END " +
                          "            end Harga, " +
                          "      CASE when PakaiDetail.GroupID>0 THEN  " +
                          "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription, " +
                          "      CASE Pakai.Status  WHEN 0 THEN ('Open')  " +
                          "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                          "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName  " +
                          "      FROM Pakai  " +
                          "      INNER JOIN PakaiDetail  " +
                          "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                          "      INNER JOIN UOM  " +
                          "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                          "      INNER JOIN Dept  " +
                          "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                          "      where Pakai.status>-1 and  " +
                          "      month(Pakai.PakaiDate)='" + Bulan + "' and Year(Pakai.PakaiDate)=" + Tahun +
                          "      and Pakai.DeptID=2  and PakaiDetail.GroupID=" + groupID + ") as AA  " +
                          "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Status,Keterangan,ItemID,crc " +
                          "      ) as BB " +
                          "      group by ItemCode " +
                          "      ) as xx";
            #endregion
            string formulaBB = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("FormulaBB", "COGS");
            string bln = Bulan.ToString();
            string dari = Tahun.ToString()+bln.PadLeft(2,'0')+"01";
            string sampai = Tahun.ToString()+bln.PadLeft(2,'0')+DateTime.DaysInMonth(Tahun,Bulan).ToString();
            this.SetModul = "Purchn";
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            //string strNewQuery = ViewMutasiStock(dari, sampai, groupID.ToString(), Tahun.ToString());
            string strNewQuery = string.Empty;
            strNewQuery = new ReportFacadeAcc().ViewMutasiStock(dari, sampai, groupID.ToString(), Tahun.ToString());
            strNewQuery = strNewQuery.Replace("/** generate report final */", "/** generate report final *//*");
            strNewQuery = strNewQuery.Replace("/** return supplier  no avgprice*/", "*/ select isnull((" + formulaBB + "),0) as Harga from z_" + created + "_mutasisaldo ");

            //strNewQuery += (this.CheckStatusClosing(Bulan.ToString(), Tahun.ToString()) == 1) ?
            //    strNewQuery.Replace("/** update colom amt dan colom hs */", "/** update colom amt dan colom hs */") : strNewQuery;
            //strNewQuery += (this.CheckStatusClosing(Bulan.ToString(), Tahun.ToString()) == 1) ?
            //    strNewQuery.Replace("/** Generate Saldo Awal */ ", "/**//** Generate Saldo Awal */ ") : strNewQuery;

            //if (CheckHargaNol(strNewQuery) > 0)
            //{
            //    return -1;
            //}
            DataAccess dataAcces = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAcces.RetrieveDataByString(strNewQuery);
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Harga"].ToString());
                }
            }
            return 0;
        }
        private int CheckHargaNol(string query)
        {
            string created =  ((Users)HttpContext.Current.Session["Users"]).ID.ToString() ;
            int result = 0;
            
            string formulaBB = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("FormulaBB", "COGS");
            string strQ = query.Replace("select isnull(" + formulaBB + ",0) as Harga from z_ "+ created +"_mutasisaldo", "select Count(ItemID)Jml from mutasisaldo where HS<=0");
            string test = "select isnull((" + formulaBB + "),0) as Harga from z_ ";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strQ);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public decimal GetAdjust(int GroupID, int Bulan, int Tahun)
        {
            string strSQL = "select (SUM(Kurang)-SUM(Tambah)) as Harga from( " +
                          "  select ItemID,isnull([Tambah],0)Tambah,isnull([Kurang],0) as Kurang from( " +
                          "  select ItemID,Quantity,AvgPrice,(Quantity*AvgPrice) as Harga, (select Adjust.AdjustType from Adjust " +
                          "  where ID=AdjustID)AdjType from AdjustDetail where GroupID=" + GroupID + " and ItemTypeID=1 and AdjustID in(select ID from Adjust " +
                          "  where Month(AdjustDate)=" + Bulan +
                          "  and YEAR(AdjustDate)=" + Tahun + " and Adjust.Status>-1) and AdjustDetail.RowStatus>-1 and Apv >0  " +
                          "  ) as x " +
                          "  pivot(sum(harga) for AdjType in([Tambah],[Kurang])) as pvt " +
                          "  ) as w";
            DataAccess dataAcces = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAcces.RetrieveDataByString(strSQL);
            strError = dataAcces.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Harga"].ToString());
                }
            }
            return 0;
        }
        public ArrayList GetVolumeProd(int Bulan, int Tahun)
        {
            string VolCalc = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Calcvolume", "COGS");
            string strSQL = "select ItemID,T,REPLACE(RTRIM(CAST(T as Char)),'.00','')+' mm x '+REPLACE(RTRIM(CAST(L as Char)),'.00','')+' x '+ " +
                          "REPLACE(RTRIM(CAST(P as Char)),'.00','') as PartNo,Formula,Vol,Juml as Jumlah,(Juml*Vol) as TotVol from( " +
                          "select ItemID,(select FormulaCode from BM_formula where id=formulaid) Formula, " +
                          "(select Partno from FC_Items where ID=ItemID) PartNo, " +
                          "(select Tebal from FC_Items where ID=ItemID) T, " +
                          "(select Lebar from FC_Items where ID=ItemID) L,  " +
                          "(select Panjang from FC_Items where ID=ItemID) P, " +
                          "(select "+VolCalc+" from FC_Items where ID=ItemID) Vol, " +
                          "SUM(Qty) as Juml from BM_Destacking where MONTH(tglProduksi)=" + Bulan + " and YEAR(tglProduksi)=" + Tahun + " and RowStatus>-1 " +
                          "Group by ItemID,FormulaID " +
                          ") as w " +
                          "order by w.T,w.Formula,w.PartNo";
            DataAccess dataAcces = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAcces.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrHPP = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrHPP.Add(new HPP
                    {
                        ID = n,
                        JenisProduk = sqlDataReader["PartNo"].ToString(),
                        Volume = Convert.ToDecimal(sqlDataReader["Vol"]),
                        Lembar = Convert.ToInt32(sqlDataReader["Jumlah"]),
                        TotalProd = Convert.ToDecimal(sqlDataReader["TotVol"]),
                        Formula = sqlDataReader["Formula"].ToString()
                        //TotalVolume=SumTotalProd
                    });
                }
            }
            else
                arrHPP.Add(new HPP());

            return arrHPP;
        }

        public string ViewMutasiStock(string Dari, string Sampai, string GroupPurch, string Tahun)
        {
            /**
             * Last Update  :17-04-2013
             * Change log
             * 1. Pengambilan nilai avgprice di table ReceiptDetail dari field avgprice sebelumnya dari Price
             * 2. Penambahan query pada sessi update mengambil nilai avg price dari table saldoinventory bulan sebelumnya jika
             *    ditabel tidak ada saldo awal
             */


            string GrpID = GroupPurch;
            string strSQL = string.Empty;
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
            string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
            switch (fldBln)
            {
                case 1:
                    SaldoLaluQty = "DesQty";
                    SaldoLaluPrice = "DesAvgPrice";
                    periodeTahun = (Convert.ToInt16(Tahun) - 1).ToString();
                    break;
                case 2:
                    SaldoLaluQty = "JanQty";
                    SaldoLaluPrice = "JanAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 3:
                    SaldoLaluQty = "FebQty";
                    SaldoLaluPrice = "FebAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 4:
                    SaldoLaluQty = "MarQty";
                    SaldoLaluPrice = "MarAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 5:
                    SaldoLaluQty = "AprQty";
                    SaldoLaluPrice = "AprAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 6:
                    SaldoLaluQty = "MeiQty";
                    SaldoLaluPrice = "MeiAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 7:
                    SaldoLaluQty = "JunQty";
                    SaldoLaluPrice = "JunAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 8:
                    SaldoLaluQty = "JulQty";
                    SaldoLaluPrice = "JulAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 9:
                    SaldoLaluQty = "AguQty";
                    SaldoLaluPrice = "AguAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 10:
                    SaldoLaluQty = "SepQty";
                    SaldoLaluPrice = "SepAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 11:
                    SaldoLaluQty = "OktQty";
                    SaldoLaluPrice = "OktAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 12:
                    SaldoLaluQty = "NovQty";
                    SaldoLaluPrice = "NovAvgPrice";
                    periodeTahun = Tahun;
                    break;
            }
            strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmp] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpxx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[mutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[mutasisaldo] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[lapsaldoawal] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[mutasireport1] " +
                    "/** Kumpulkan data dari beberapa tabel dengan UNION ALL all */ " +
                    "SELECT * INTO lapmutASitmp FROM( " +
                    "     (SELECT '0' AS Tipe,'" + frm + "' AS Tanggal,'Saldo Awal' AS DocNo,si.ItemID,si." + SaldoLaluPrice + " AS SaldoHS," +
                    "     si." + SaldoLaluQty + " AS SaldoQty,CASE WHEN si." + SaldoLaluPrice + "<0 THEN si." + SaldoLaluPrice + " ELSE 1 END AvgPrice,(si." + SaldoLaluQty + "*si." + SaldoLaluPrice + ") AS TotalPrice " +
                    "     FROM SaldoInventory AS si " +
                    "     WHERE si.YearPeriod='" + periodeTahun + "' AND si.ItemID IN(SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "') AND itemTypeID='1' AND Aktif=1) " +
                    "     AND si.ItemTypeID='1' and si." + SaldoLaluQty + ">=0 " +
                    "  ) UNION ALL ( /*" +
                    "     SELECT '1' AS Tipe, cONvert(VARCHAR,p.ReceiptDate,105) AS Tanggal,p.ReceiptNo ,rd.ItemID, (ISNULL(rd.AvgPrice,0)) as Price ,rd.Quantity," +
                    "     ISNULL(rd.AvgPrice,0) AS AvgPrice," +
                    "     (rd.Quantity * ISNULL(rd.AvgPrice,0)) AS TotalPrice " +
                    "     FROM Receipt AS p " +
                    "     INNER JOIN ReceiptDetail AS rd " +
                    "     ON rd.ReceiptID=p.ID " +
                    "     WHERE  (cONvert(VARCHAR,p.ReceiptDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "     AND rd.ItemID IN(SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "') AND Aktif=1)AND p.Status >-1 AND rd.RowStatus >-1 */" +
                    GetHargaReceipts(Dari, Sampai, GrpID, "A") +
                    "   ) UNION ALL ( " +
                    "       SELECT '2' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity," +
                    "       (pk.AvgPrice) AS AvgPrice,(pk.Quantity*pk.AvgPrice) AS TotalPrice FROM Pakai AS k " +
                    "       LEFT JOIN PakaiDetail AS pk " +
                    "       ON pk.PakaiID=k.ID " +
                    "       WHERE  (cONvert(VARCHAR,k.PakaiDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "       AND pk.ItemID IN(SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "') AND Aktif=1) AND k.Status >-1 AND pk.RowStatus >-1" +
                    "   ) UNION ALL  ( " +
                    "        SELECT '3' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity, " +
                    "        (rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp " +
                    "        LEFT JOIN ReturPakaiDetail AS rpd " +
                    "        ON rpd.ReturID=rp.ID " +
                    "        WHERE  (cONvert(VARCHAR,rp.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "        AND rpd.ItemID IN(SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "') AND Aktif=1) AND rp.Status >-1 AND rpd.RowStatus >-1 " +
                    "   ) UNION ALL ( " +
                    "       SELECT '4' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Tambah' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice As SaldoHS, " +
                    "       CASE When a.AdjustType='Tambah' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPrice, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPrice " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Tambah'" +
                    "       AND ad.ItemID IN(SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "') AND Aktif=1) AND a.Status >-1 AND ad.RowStatus >-1" +
                    "    )UNION ALL ( " +
                    "       SELECT '5' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Kurang' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "       CASE When a.AdjustType='Kurang' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPriceK, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPriceK " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Kurang' " +
                    "       AND ad.ItemID IN(SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "') AND Aktif=1) AND a.Status >-1 AND ad.RowStatus >-1" +
                    "   )UNION ALL ( " +
                    "   SELECT '6' AS Tipe, " +
                    "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                    "    CAST('0' AS Decimal(10,2)) AS AvgPrice ,CAST('0' AS Decimal(10,2)) AS Totalprice " +
                    "    FROM ReturSupplier AS rs " +
                    "    LEFT JOIN ReturSupplierDetail AS rsd " +
                    "    ON rsd.ReturID=rs.ID " +
                    "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "    AND rsd.ItemID IN(SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "') AND Aktif=1) AND rs.Status >-1  " +
                    "    AND rsd.RowStatus >-1 ) " +
                    ") as K " +
                    "/** susun sesuai dengan kolom laporan */ " +
                    "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, " +
                    "    BeliQty,BeliHS,(BeliQty*BeliHS) as BeliAmt, " +
                    "    AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, " +
                    "    ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt, " +
                    "    ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, " +
                    "    AdjProdQty,AdjProdHS AS AdjProdHS,(AdjProdQty*HPP) as AdjPAmt,RetSupQty, " +
                    "    RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                    "    (SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty, " +
                    "    ((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                    "   (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                    "   INTO lapmutasitmpx " +
                    " FROM ( " +
                    " SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0)  ELSE 0 END SaldoAwalQty, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(SaldoHS,0)  ELSE 0 END HPP, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0)  ELSE 0 END BeliQty, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(SaldoHS,0)  ELSE 0 END BeliHS, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPB, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0)  ELSE 0 END ProdQty, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(SaldoHS,0)  ELSE 0 END ProdHS, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAd, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0)  ELSE 0 END ReturnQty, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(SaldoHS,0)  ELSE 0 END ReturHS, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPP, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjustQty, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(SaldoHS,0)  ELSE 0 END AdjustHS, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPR, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjProdQty, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(SaldoHS,0)  ELSE 0 END AdjProdHS, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAdjP, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0)  ELSE 0 END RetSupQty, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(SaldoHS,0)  ELSE 0 END RetSupHS, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalRetSup " +
                    "  FROM lapmutasitmp as x) AS Z ORDER BY z.Tanggal " +
                    "   /** susun data berdasarkan item id dan bentuk id baru */ " +
                    "   SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY itemID,ID,DocNo) as IDn,* INTO lapmutasitmpxx " +
                    "   FROM lapmutasitmpx " +
                    "   /**Susun  data tabular */ " +
                    "    SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo, " +
                    "    BeliQty,BeliHS,(BeliQty*BeliHS) As BeliAmt,AdjustQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS, " +
                    "    ProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, " +
                    "    AdjProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, " +
                    "    A.ReturnQty, " +
                    "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ReturnHS, " +
                    "    A.RetSupQty, " +
                    "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty, " +
                    "    CASE WHEN A.ID>1 THEN  " +
                    "       CASE WHEN (SELECT SUM(totalqty)FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN " +
                    "        ((SELECT SUM(totalamt) FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )/ " +
                    "        (ABS((SELECT SUM(totalqty)FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM lapMutasitmpxx WHERE ID <=A.ID  AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt " +
                    "    INTO lapmutasireport " +
                    "    FROM lapMutasitmpxx as A " +
                    "    ORDER by itemID,A.Tanggal,A.IDn,A.Tipe,A.DocNo  " +

                    "   /** Generate Detail Report without saldo akhir */ " +
                    "  SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                    "           (l.BeliQty*l.BeliHS) as BeliAmt,l.AdjustQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, " +
                    "           l.ProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                    "           l.AdjProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt, " +
                    "           l.ReturnQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ReturnHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                    "           l.RetSupQty, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                    "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                    "    INTO mutasireport " +
                    "  FROM lapmutasireport AS L " +
                    "  WHERE l.ItemID IN (SELECT ID FROM Inventory WHERE GroupID IN('" + GrpID + "')) ORDER BY L.itemID,L.Tipe,L.Tanggal " +

                    "  /** update colom amt dan colom hs */ " +
                     "  select row_number() over(order by itemID) as IDn,itemid into mutasireport1 " +
                     "  from mutasireport group by itemID order by itemid " +
                     "   declare @i int " +
                     "   declare @b int " +
                     "   declare @hs decimal(18,2) " +
                     "   declare @amt decimal(18,2) " +
                     "   declare @avgp decimal(18,2) " +
                     "   declare @itemTypeID int " +
                     "   declare @c int " +
                     "   declare @itm int " +
                     "   declare @itmID int " +
                     "   set @c=1 " +
                     "   set @itm=(select COUNT(IDn) from mutasireport1) " +
                     "   set @itemTypeID=(select GroupID from Inventory where ID=(select top 1 ItemID from mutasireport))" +
                     "  IF @itemTypeID<>9 " +
                     "   BEGIN   " +
                     "   While @c <=@itm " +
                     "   Begin " +
                     "   set @itmID=(select itemID from mutasireport1 where IDn=@c) " +
                     "       set @b=1 " +
                     "       set @avgp=(select top 1 DesAvgPrice from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                     "       set @i=(select COUNT(id) from mutasireport where itemid=@itmID) " +
                     "       While @b<=@i  " +
                     "       Begin " +
                     "       set @hs=CASE WHEN @b >1 THEN (select hs from mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "               ELSE CASE WHEN(SELECT hs from mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                     "		         (SELECT hs from mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                     "		         END  " +
                     "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "                ELSE (SELECT TotalAmt from mutasireport where ID=1 and itemid=@itmID)  " +
                     "                END  " +
                     "       /** update semua hs */ " +
                     "       update mutasireport  " +
                     "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdHS		=CASE WHEN (SELECT ProdQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdAmt    =(ProdQty*@hs), " +
                     "           AdjustAmt  =(AdjustQty*@hs), " +
                     "           AdjProdAmt =(AdjProdQty*@hs), " +
                     "           returnAmt  =(ReturnQty*@hs), " +
                     "           RetSupAmt  =(RetSupQty*@hs), " +
                     "           totalamt   =((BeliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)), " +
                     "           hs=case when abs(SaldoAwalQty)>0 then " +
                     "             (((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty) else @avgp end  " +
                     "              where ID=(@b+1) and itemid=@itmID " +
                     "       set @b=@b+1 " +
                     "       END " +
                     "   set @c=@c+1 " +
                     "   END " +
                     " END " +

                    "  /** Generate Saldo Awal */ " +
                    "  SELECT ItemID,SaldoAwalQty,HS,TotalAmt " +
                    "  INTO lapsaldoawal " +
                    "  FROM mutasireport as m " +
                    "  WHERE m.DocNo='Saldo Awal' AND m.ItemID in(SELECT iv.ID FROM Inventory AS iv WHERE iv.GroupID IN('" + GrpID + "') AND iv.ItemTypeID='1') " +

                     "   /** Generate Saldo Akhir */ " +
                     "       SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, " +
                     "           /** pembelian */ " +
                     "             (SUM(m.BeliQty)) As BeliQty, " +
                     "             CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, " +
                     "             (SUM(m.BeliAmt)) As BeliAmt, " +
                     "           /** Ajdut Plust */ " +
                     "             (SUM(m.AdjustQty)) As AdjustQty, " +
                     "             CASE WHEN SUM(m.AdjustAmt) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty))ELSE 0 END AdjustHS, " +
                     "             (SUM(m.AdjustAmt)) As AdjustAmt, " +
                     "           /** Pemakaian Produksi */ " +
                     "             (SUM(m.ProdQty)) As ProdQty, " +
                     "             CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, " +
                     "             (SUM(m.ProdAmt)) As ProdAmt, " +
                     "           /** Adjut minus */ " +
                     "             (SUM(m.AdjProdQty)) As AdjProdQty, " +
                     "             CASE WHEN SUM(m.AdjProdAmt) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty))ELSE 0 END AdjProdHS, " +
                     "             (SUM(m.AdjProdAmt)) As AdjProdAmt, " +
                     "           /** Return */ " +
                     "             (SUM(m.ReturnQty)) As ReturnQty, " +
                     "             CASE WHEN SUM(m.returnAmt) > 0 THEN (SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, " +
                     "             (SUM(m.returnAmt)) As returnAmt, " +
                     "            /** Return Supplier */ " +
                     "            (SUM(m.RetSupQty)) As RetSupQty, " +
                     "            CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, " +
                     "            (SUM(m.RetSupAmt)) As RetSupAmt, " +
                     "  /** Saldo Akhir */ " +
                     "  (SELECT TOP 1 SaldoAwalQty FROM mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                     "  (SELECT TOP 1 HS FROM mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +

                     "  CASE  when (SELECT  TOP  1 SaldoAwalQty FROM  mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID  DESC)>0 then  " +
                     "  (SELECT  TOP  1 TotalAmt FROM  mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID DESC) " +
                     "  ELSE  0 END  As  TotalAmt " +
                     "  INTO mutasisaldo  " +
                     "  FROM mutasireport AS m  " +
                     "  WHERE m.ItemID in(SELECT iv.ID FROM Inventory AS iv WHERE iv.GroupID IN('" + GrpID + "') AND iv.ItemTypeID='1') " +
                     "  GROUP BY m.ItemID  " +

            " /** Get Pemaikaian Total */ " +
            " select (SUM(ProdAMT)+SUM(AdjProdAMT))-(sum(AdjustAMT)) as Harga from mutasisaldo " +
            "/** return supplier  no avgprice*/" +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmp] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpx] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpxx] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasireport] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[mutasireport] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[mutasisaldo] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[lapsaldoawal] ";
            return strSQL;
        }
        private string GetHargaReceipts(string Dari, string Sampai, string GrpID, string Tahun)
        {
            ReportFacadeAcc rp = new ReportFacadeAcc();
            string query = rp.GetHargaReceipt(Dari, Sampai, GrpID, Tahun);
            return query;
        }
        private string SetModul { get; set; }
        private int CheckStatusClosing(string Bulan, string Tahun)
        {
            string strSQL = "Select Status from AccClosingPeriode where Tahun=" + Tahun + " and Bulan=" + Bulan + " and Modul='" + this.SetModul + "'";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            int status = 0;
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    status = Convert.ToInt32(sdr["Status"].ToString());
                }
            }
            return status;
        }
        
    }
}
