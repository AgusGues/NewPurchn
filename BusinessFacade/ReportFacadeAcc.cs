using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Domain;
using System.Data;
using System.Data.SqlClient;
namespace BusinessFacade
{
    public class ReportFacadeAcc
    {
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public string ViewMutasiWIP(string Bln, string Thn, string partno)
        {
            string strSQL = string.Empty;
            string prd = partno.Substring(0, 3);
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            int fldBln = Convert.ToInt16(Bln);
            switch (fldBln)
            {
                case 1:
                    SaldoLaluQty = "DesQty";
                    SaldoLaluPrice = "DesAvgPrice";
                    periodeTahun = (Convert.ToInt16(Thn) - 1).ToString();
                    break;
                case 2:
                    SaldoLaluQty = "JanQty";
                    SaldoLaluPrice = "JanAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 3:
                    SaldoLaluQty = "FebQty";
                    SaldoLaluPrice = "FebAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 4:
                    SaldoLaluQty = "MarQty";
                    SaldoLaluPrice = "MarAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 5:
                    SaldoLaluQty = "AprQty";
                    SaldoLaluPrice = "AprAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 6:
                    SaldoLaluQty = "MeiQty";
                    SaldoLaluPrice = "MeiAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 7:
                    SaldoLaluQty = "JunQty";
                    SaldoLaluPrice = "JunAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 8:
                    SaldoLaluQty = "JulQty";
                    SaldoLaluPrice = "JulAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 9:
                    SaldoLaluQty = "AguQty";
                    SaldoLaluPrice = "AguAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 10:
                    SaldoLaluQty = "SepQty";
                    SaldoLaluPrice = "SepAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 11:
                    SaldoLaluQty = "OktQty";
                    SaldoLaluPrice = "OktAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 12:
                    SaldoLaluQty = "NovQty";
                    SaldoLaluPrice = "NovAvgPrice";
                    periodeTahun = Thn;
                    break;
            }
            string bln = fldBln.ToString();
            string s = new string('0', (2 - bln.Length));
            int lastDay = DateTime.DaysInMonth((Convert.ToInt16(periodeTahun)), fldBln);
            string d = new string('0', (2 - lastDay.ToString().Length));
            int thawal = 0;
            if (SaldoLaluQty == "DesQty")
                thawal = Convert.ToInt16((Thn + s + bln).Substring(0, 4)) - 1;
            else
                thawal = Convert.ToInt16((Thn + s + bln).Substring(0, 4));
            string tglA = (Thn + s + bln).Substring(4, 2) + "/1/" + (Thn + s + bln).Substring(0, 4);
            if (partno.Trim().Substring(3, 3) != "-1-" )//|| partno.Trim().Substring(4, 3) != "-1-" || partno.Trim().Substring(5, 3) != "-1-")
            {
                strSQL = "create table #adminkswip(idrec varchar(100),tanggal datetime,itemid0 int,qty int,lokid int) " +
                    "declare @idrec varchar(100) " +
                    "declare @tanggal datetime " +
                    "declare @itemid0 int " +
                    "declare @qty int " +
                    "declare @lokid int " +
                    "declare kursor cursor for " +
                    "select * from vw_KartustockWIP where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + Thn + s + bln + "' and  itemid0  " +
                    "in(select id from fc_items where PartNo = '" + partno + "') " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                    "	insert into #adminkswip(idrec,tanggal,itemid0,qty,lokid)values(@idrec,@tanggal,@itemid0,@qty,@lokid) " +
                    "	FETCH NEXT FROM kursor " +
                    "	INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
                    "END " +
                    " CLOSE kursor " +
                    "DEALLOCATE kursor " +
                    "select ID, tanggal, keterangan as nodocument , awal as awalQty, " +
                    "case when keterangan not like 'adjust%' then Penerimaan else 0 end inprodQty,isnull(hpp,0) as InProdHS,0 as InProdAMT,  " +
                    "case when keterangan not like 'adjust%' and (substring(partno,4,3)!='-1-') then pengeluaran else 0 end outProdQty,isnull(hpp,0) as OutProdHS,0 as OutProdAMT, 0 as InLariQty,0 as InLariHS,0 as InLariAmt, " +
                    "case when keterangan ='adjustin' then Penerimaan else 0 end inAdjustQty,isnull(hpp,0) as InAdjustHS,0 as InAdjustAMT,  " +
                    "case when keterangan not like 'adjust%' and (substring(partno,4,3)='-1-' or substring(partno,5,3)='-1-' or substring(partno,6,3)='-1-') then pengeluaran else 0 end outLariQty, 0 as outLariHS,0 as outLariAmt,  " +
                    "case when keterangan ='adjustout' then pengeluaran else 0 end outAdjustQty,isnull(hpp,0) as OutAdjustHS,0 as OutAdjustAMT,  " +
                    "awal+Penerimaan-pengeluaran as saldo from  ( " +
                    "select ID, tanggal,Partno,keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "select " + SaldoLaluQty + " as qty  from SaldoInventoryT1 A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " " +
                    "	UNION ALL " +
                    "    SELECT SUM(qty) as qty from #adminkswip where itemid0=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + Thn + s + bln + "' " +
                    "     )as ain0) end awal, Penerimaan,pengeluaran,hpp   " +
                    "	from (  " +
                    "    select '0'as ID ,ItemID ,'" + tglA + "' as tanggal, '-' as partno,'saldo awal' as keterangan, " +
                    "	0  as penerimaan,0 as pengeluaran,0 as hpp  from SaldoInventoryT1 A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + (Thn + s + bln).Substring(0, 4) + " " +
                    "	UNION ALL " +
                    "    SELECT CONVERT(varchar, TglProduksi, 112) + '0' + RTRIM(CAST(A.ItemID AS varchar(10))) + RTRIM(CAST(A.ItemID AS varchar(10))) AS ID,A.itemID as itemid,A.TglProduksi as tanggal" +
                    "    ,B.PartNo, RTRIM(B.PartNo) AS keterangan, sum(A.Qty) AS Penerimaan, 0 AS Pengeluaran, avg(A.HPP) AS hpp FROM FC_Items AS B INNER JOIN  " +
                    "    BM_Destacking AS A ON B.ID = A.ItemID  WHERE (LEFT(CONVERT(varchar, A.TglProduksi, 112), 6) = '" + Thn + s + bln + "') " +
                    "    and A.rowstatus>-1 AND (B.PartNo = '" + partno + "') group by A.TglProduksi,A.ItemID,B.PartNo  " +
                    "	UNION ALL " +
                    "SELECT CONVERT(varchar, A.TglJemur, 112) + '1' + CONVERT(varchar, C.TglProduksi, 112) + RTRIM(CAST(C.ItemID AS varchar(10)))  + RTRIM(CAST(A.ItemID0 AS varchar(10))) AS ID, " +
                    "C.itemID as itemid,A.TglJemur as tanggal,B.PartNo, RTRIM(B.PartNo)  + ' - ' +  " +
                    "rtrim(convert(varchar,C.TglProduksi,110)) AS keterangan, 0 AS Penerimaan,  " +
                    "sum(A.QtyIn) AS Pengeluaran, avg(A.HPP) AS hpp FROM FC_Items AS B INNER JOIN   " +
                    "T1_JemurLg AS A ON B.ID = A.ItemID0 inner join BM_Destacking C on C.ID=A.DestID inner join FC_Items D on C.ItemID=D.ID  " +
                    "WHERE (LEFT(CONVERT(varchar, A.TglJemur, 112), 6) = '" + Thn + s + bln + "')  " +
                    "and A.status>-1 AND (D.PartNo = '" + partno + "') group by C.TglProduksi,A.TglJemur,C.ItemID,A.ItemID0,B.PartNo " +
                    "UNION ALL  " +
                    "SELECT  CONVERT(varchar, TglSerah, 112) + '2' + rtrim(CAST(B.itemID AS varchar(10)))  + rtrim(CAST(A.ItemID AS varchar(10))) AS ID,B.itemID as itemid,  " +
                    "A.TglSerah as tanggal, C.PartNo, C.PartNo AS Keterangan,   " +
                    "0 AS Penerimaan, ISNULL(sum(A.QtyIn), 0) AS Pengeluaran, avg(A.HPp) AS HPP FROM Vw_T1_Serah AS A INNER JOIN  " +
                    "BM_Destacking AS B ON A.DestID = B.ID INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Items AS C1 ON B.ItemID = C1.ID  " +
                    "WHERE A.itemtypeID0=1 and B.qty>0 and  (A.Status > - 1) AND (B.RowStatus > - 1) and   " +
                    "(LEFT(CONVERT(varchar, A.TglSerah, 112), 6) = '" + Thn + s + bln + "' and C1.PartNo = '" + partno + "')  " +
                    "GROUP BY  A.TglSerah,B.itemid,A.itemid, C1.PartNo, C.PartNo "+
                    //"UNION ALL "+
                    //"SELECT CONVERT(varchar, A.AdjustDate , 112) + '3' + rtrim(CAST(B.itemID AS varchar(10)))+ rtrim(B.AdjustType) AS ID,B.itemID as itemid,   "+
                    //"A.AdjustDate as tanggal, C.PartNo, 'adjustin' AS Keterangan,   ISNULL(sum(B.QtyIn ), 0) AS Penerimaan, 0 AS Pengeluaran, avg(B.HPp) AS HPP  "+
                    //"FROM T1_Adjust AS A INNER JOIN T1_AdjustDetail AS B ON A.ID = B.AdjustID INNER JOIN FC_Items AS C ON B.ItemID = C.ID "+
                    //"where C.PartNo = '" + partno + "' and B.RowStatus > - 1 and LEFT(CONVERT(varchar, A.AdjustDate, 112), 6) = '" + Thn + s + bln + "' and B.AdjustType='In'  " +
                    //"group by A.AdjustDate,B.itemid, C.PartNo,B.AdjustType "+
                    //"UNION ALL "+
                    //"SELECT CONVERT(varchar, A.AdjustDate , 112) + '4' + rtrim(CAST(B.itemID AS varchar(10)))+ rtrim(B.AdjustType) AS ID,B.itemID as itemid,   "+
                    //"A.AdjustDate as tanggal, C.PartNo, 'adjustout' AS Keterangan,   0 AS Penerimaan, ISNULL(sum(B.QtyOut ), 0) AS Pengeluaran, avg(B.HPp) AS HPP "+ 
                    //"FROM T1_Adjust AS A INNER JOIN T1_AdjustDetail AS B ON A.ID = B.AdjustID INNER JOIN FC_Items AS C ON B.ItemID = C.ID "+
                    //"where C.PartNo = '" + partno + "' and B.RowStatus > - 1 and LEFT(CONVERT(varchar, A.AdjustDate, 112), 6) = '" + Thn + s + bln + "' and B.AdjustType='out'  " +
                    //"group by A.AdjustDate,B.itemid, C.PartNo,B.AdjustType " +
                    ") as ain1 ) as atALL  order by ID  " +
                    "drop table #adminkswip";
            }
            else
            {
                 strSQL = "create table #adminkswip(idrec varchar(100),tanggal datetime,itemid0 int,qty int,lokid int) " +
                    "declare @idrec varchar(100) " +
                    "declare @tanggal datetime " +
                    "declare @itemid0 int " +
                    "declare @qty int " +
                    "declare @lokid int " +
                    "declare kursor cursor for " +
                    "select * from vw_KartustockWIP2 where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + Thn + s + bln + "' and  itemid0  " +
                    "in(select id from fc_items where PartNo = '" + partno + "') " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                    "	insert into #adminkswip(idrec,tanggal,itemid0,qty,lokid)values(@idrec,@tanggal,@itemid0,@qty,@lokid) " +
                    "	FETCH NEXT FROM kursor " +
                    "	INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
                    "END " +
                    " CLOSE kursor " +
                    "DEALLOCATE kursor " +
                    "select ID, tanggal, keterangan as nodocument , awal as awalQty, 0 as inprodQty,isnull(hpp,0) as InProdHS,0 as InProdAMT, " +
                    "case when keterangan not like 'adjust%' then pengeluaran else 0 end outProdQty,isnull(hpp,0) as OutProdHS,0 as OutProdAMT, " +
                    "case when keterangan not like 'adjust%' then Penerimaan else 0 end InLariQty,0 as InLariHS,0 as InLariAmt, " +
                    "case when keterangan ='adjustin' then Penerimaan else 0 end inAdjustQty,isnull(hpp,0) as InAdjustHS,0 as InAdjustAMT, " +
                    "0 as outLariQty, 0 as outLariHS,0 as outLariAmt, " +
                    "case when keterangan ='adjustout' then pengeluaran else 0 end outAdjustQty,isnull(hpp,0) as OutAdjustHS,0 as OutAdjustAMT, " +
                    "awal+Penerimaan-pengeluaran as saldo from ( " +
                    "select ID, tanggal,keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "select " + SaldoLaluQty + " as qty  from SaldoInventoryT1 A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " " +
                    "	UNION ALL " +
                    "    SELECT SUM(qty) as qty from #adminkswip where itemid0=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + Thn + s + bln + "' " +
                    "     )as ain0) end awal, Penerimaan,pengeluaran,hpp   " +
                    "	from (  " +
                    "    select '0'as ID ,ItemID ,'" + tglA + "' as tanggal, '-' as partno,'saldo awal' as keterangan, " +
                    "	0  as penerimaan,0 as pengeluaran,0 as hpp  from SaldoInventoryT1 A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + (Thn + s + bln).Substring(0, 4) + " " +
                    "	UNION ALL " +
                    "SELECT CONVERT(varchar, TglJemur, 112) + '0'+  RTRIM(CAST(A.ItemID0 AS varchar(10)))  + CONVERT(varchar, C.TglProduksi, 112) +RTRIM(CAST(A.ItemID AS varchar(10))) "+
                    " +rtrim((select nopalet from bm_palet where id in(select paletID from bm_destacking where id = A.Destid))) AS ID, A.itemID0 as itemid, " +
                    "A.TglJemur as tanggal,B.PartNo, 'dr produksi ' + RTRIM(D.PartNo) + ' -' +rtrim(convert(varchar,C.TglProduksi,110)) AS keterangan,   " +
                    "sum(A.QtyIn) AS Penerimaan, 0 AS Pengeluaran, avg(A.HPP) AS hpp FROM FC_Items AS B INNER JOIN T1_JemurLg AS A ON B.ID = A.ItemID0   " +
                    "inner join BM_Destacking C on C.ID=A.DestID inner join FC_Items D on C.ItemID=D.ID WHERE (LEFT(CONVERT(varchar, A.TglJemur, 112), 6) = '" + Thn + s + bln + "')  " +
                    " and A.status>-1 AND (B.PartNo = '" + partno + "')  group by C.TglProduksi,A.TglJemur,A.itemID0,A.ItemID,D.PartNo,B.PartNo          " +
                    " UNION ALL   " +
                    " SELECT  CONVERT(varchar, TglSerah, 112)+ '1'  + CONVERT(varchar, B.TglProduksi, 112)+ rtrim(CAST(B.itemID AS varchar(10))) + rtrim(CAST(A.itemID0 AS varchar(10))) + CAST(A.ItemID AS varchar(10)) AS ID,A.itemID0 as itemid,   " +
                    " A.TglSerah as tanggal, C.PartNo, 'trasf ke ' + rtrim(C.PartNo) + ' -' +rtrim(convert(varchar,B.TglProduksi,110)) AS Keterangan,    " +
                    " 0 AS Penerimaan, ISNULL(sum(A.QtyIn), 0) AS Pengeluaran, avg(A.HPp) AS HPP FROM T1_Serah AS A INNER JOIN  BM_Destacking AS B ON A.DestID = B.ID   " +
                    " INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Items AS C1 ON A.ItemID0 = C1.ID  WHERE  B.qty>0 and  (A.Status > - 1) AND (B.Status > - 1) and    " +
                    " (LEFT(CONVERT(varchar, A.TglSerah, 112), 6) = '" + Thn + s + bln + "' and C1.PartNo = '" + partno + "')  GROUP BY B.TglProduksi, " +
                    " A.TglSerah,B.itemid,A.itemID0,A.itemid, C1.PartNo, C.PartNo " +
                    //"UNION ALL " +
                    //"SELECT CONVERT(varchar, A.AdjustDate , 112) + '3' + rtrim(CAST(B.itemID AS varchar(10)))+ rtrim(B.AdjustType) AS ID,B.itemID as itemid, " +
                    //"A.AdjustDate as tanggal, C.PartNo, 'adjustin' AS Keterangan,   ISNULL(sum(B.QtyIn ), 0) AS Penerimaan, 0 AS Pengeluaran, avg(B.HPp) AS HPP " +
                    //"FROM T1_Adjust AS A INNER JOIN T1_AdjustDetail AS B ON A.ID = B.AdjustID INNER JOIN FC_Items AS C ON B.ItemID = C.ID " +
                    //"where C.PartNo = '" + partno + "' and B.RowStatus > - 1 and LEFT(CONVERT(varchar, A.AdjustDate, 112), 6) = '" + Thn + s + bln + "' and B.AdjustType='In' " +
                    //"group by A.AdjustDate,B.itemid, C.PartNo,B.AdjustType " +
                    //"UNION ALL " +
                    //"SELECT CONVERT(varchar, A.AdjustDate , 112) + '4' + rtrim(CAST(B.itemID AS varchar(10)))+ rtrim(B.AdjustType) AS ID,B.itemID as itemid, " +
                    //"A.AdjustDate as tanggal, C.PartNo, 'adjustout' AS Keterangan,   0 AS Penerimaan, ISNULL(sum(B.QtyOut ), 0) AS Pengeluaran, avg(B.HPp) AS HPP " +
                    //"FROM T1_Adjust AS A INNER JOIN T1_AdjustDetail AS B ON A.ID = B.AdjustID INNER JOIN FC_Items AS C ON B.ItemID = C.ID " +
                    //"where C.PartNo = '" + partno + "' and B.RowStatus > - 1 and LEFT(CONVERT(varchar, A.AdjustDate, 112), 6) = '" + Thn + s + bln + "' and B.AdjustType='out' " +
                    //"group by A.AdjustDate,B.itemid, C.PartNo,B.AdjustType " +
                    ") as ain1 ) as atALL  order by ID  " +
                    "drop table #adminkswip";
            }

            return strSQL;
        }
        public string ViewMutasiWIPRekap(string Bln, string Thn, string PartNo)
        {
            #region prepared data
            string strSQL = string.Empty;
            //string prd = PartNo.Substring(0, 3);
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            int fldBln = Convert.ToInt16(Bln);
            switch (fldBln)
            {
                case 1:
                    SaldoLaluQty = "DesQty";
                    SaldoLaluPrice = "DesAvgPrice";
                    periodeTahun = (Convert.ToInt16(Thn) - 1).ToString();
                    break;
                case 2:
                    SaldoLaluQty = "JanQty";
                    SaldoLaluPrice = "JanAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 3:
                    SaldoLaluQty = "FebQty";
                    SaldoLaluPrice = "FebAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 4:
                    SaldoLaluQty = "MarQty";
                    SaldoLaluPrice = "MarAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 5:
                    SaldoLaluQty = "AprQty";
                    SaldoLaluPrice = "AprAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 6:
                    SaldoLaluQty = "MeiQty";
                    SaldoLaluPrice = "MeiAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 7:
                    SaldoLaluQty = "JunQty";
                    SaldoLaluPrice = "JunAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 8:
                    SaldoLaluQty = "JulQty";
                    SaldoLaluPrice = "JulAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 9:
                    SaldoLaluQty = "AguQty";
                    SaldoLaluPrice = "AguAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 10:
                    SaldoLaluQty = "SepQty";
                    SaldoLaluPrice = "SepAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 11:
                    SaldoLaluQty = "OktQty";
                    SaldoLaluPrice = "OktAvgPrice";
                    periodeTahun = Thn;
                    break;
                case 12:
                    SaldoLaluQty = "NovQty";
                    SaldoLaluPrice = "NovAvgPrice";
                    periodeTahun = Thn;
                    break;
            }
            string bln = fldBln.ToString();
            string s = new string('0', (2 - bln.Length));
            int lastDay = DateTime.DaysInMonth((Convert.ToInt16(periodeTahun)), fldBln);
            string d = new string('0', (2 - lastDay.ToString().Length));
            #endregion
            strSQL = "select * from ( " +
               "select itemID,HMY,partno as NoDocument,0 as InProdQty,0 as InProdHS,0 as InProdAMT, " +
               "(SELECT isnull(SUM(A.qtyin),0) FROM  FC_Items AS B INNER JOIN T1_JemurLg AS A ON B.ID = A.ItemID0 INNER JOIN BM_Destacking C ON C.ID = A.DestID "+
               "INNER JOIN FC_Items D ON C.ItemID = D .ID WHERE A.status > - 1 and A.ItemID0=M1.ItemID and LEFT(CONVERT(varchar,TglJemur,112),6) = M1.HMY) as InLariQty, " +
               "0 as InLariHS,0 as InLariAmt,(SELECT ISNULL(SUM(B.QtyIn), 0) AS qty FROM T1_AdjustDetail AS B INNER JOIN T1_Adjust AS A ON B.AdjustID = A.ID where B.ItemID=M1.ItemID and " +
               "LEFT(CONVERT(varchar, A.AdjustDate ,112), 6) = M1.HMY and B.AdjustType='In') as InAdjustQty,0 as InAdjustHS,0 as InAdjustAMT, "+
               "0 as OutProdQty,0 as OutProdHS,0 as OutProdAMT, "+
               "(SELECT ISNULL(SUM(B.QtyOut), 0) AS qty FROM T1_AdjustDetail AS B INNER JOIN T1_Adjust AS A ON B.AdjustID = A.ID where B.ItemID=M1.ItemID and "+
               "LEFT(CONVERT(varchar, A.AdjustDate ,112), 6) = M1.HMY and B.AdjustType='Out') as outAdjustQty,0 as OutAdjustHS,0 as OutAdjustAMT,"+
               "(SELECT  ISNULL(sum(A.QtyIn), 0) FROM T1_Serah AS A INNER JOIN BM_Destacking AS B ON A.DestID = B.ID INNER JOIN FC_Items AS C ON A.ItemID = C.ID "+
               "INNER JOIN FC_Items AS C1 ON A.ItemID0 = C1.ID WHERE B.qty > 0 AND (A.Status > - 1) AND (B.Status > - 1) AND C1.ItemTypeID > 1 and A.itemID0=M1.ItemID and LEFT(CONVERT(varchar, A.TglSerah ,112),  6) = M1.HMY) as outLariQty, " +
               "0 as outLariHS,0 as outLariAmt,(select isnull(sum(" + SaldoLaluQty + "),0) from SaldoInventoryT1 where ItemID=M1.ItemID and yearperiod=" + periodeTahun + ")as AwalQty, 0 as AwalHS , 0 as AwalAMT from ( " +
               "select distinct A.ID as ItemID,A.partno, '" + Thn + s + bln + "' as HMY from FC_Items A  where SUBSTRING(A.PartNo,4,3)='-1-' ) as M1) as M2  " +
               "where Awalqty<>0 or InProdQty<>0 or OutProdQty<>0 or InLariQty<>0 or outLariQty<>0  "+
               "UNION ALL " +
               "select * from ( " +
               "select itemID,HMY,partno as NoDocument, " +
               "(select isnull(SUM(qty),0) from BM_Destacking where LEFT(CONVERT(varchar,TglProduksi, 112), 6) = M1.HMY and ItemID=M1.ItemID and RowStatus>-1) as InProdQty,0 as InProdHS,0 as InProdAMT, " +
               "0 as InLariQty, " +
               "0 as InLariHS,0 as InLariAmt, (SELECT ISNULL(SUM(B.QtyIn), 0) AS qty FROM T1_AdjustDetail AS B INNER JOIN T1_Adjust AS A ON B.AdjustID = A.ID where B.ItemID=M1.ItemID and "+
               " LEFT(CONVERT(varchar, A.AdjustDate ,112),  6) = M1.HMY and B.AdjustType='In') as InAdjustQty,0 as InAdjustHS,0 as InAdjustAMT, "+
               "(select SUM(qty) from ( " +
               "select isnull(SUM(qtyin),0) as qty from T1_Serah A, BM_Destacking B where A.destid=B.ID and B.itemID=M1.ItemID and  " +
               "left(convert(varchar,A.tglserah,112),6)=M1.HMY and A.Status>-1 AND B.RowStatus > - 1 AND B.Qty > 0 " +
               "UNION ALL " +
               "select isnull(SUM(qtyin),0)  as qty  from T1_JemurLg  A, BM_Destacking B where A.destid=B.ID and B.itemID=M1.ItemID and  " +
               "left(convert(varchar,A.tgljemur,112),6)=M1.HMY and A.Status>-1 AND B.RowStatus > - 1 AND B.Qty > 0) as K) as OutProdQty,0 as OutProdHS,0 as OutProdAMT, " +
               "(SELECT ISNULL(SUM(B.QtyOut), 0) AS qty FROM T1_AdjustDetail AS B INNER JOIN T1_Adjust AS A ON B.AdjustID = A.ID where B.ItemID=M1.ItemID and "+
               " LEFT(CONVERT(varchar, A.AdjustDate ,112), 6) = M1.HMY and B.AdjustType='Out') as outAdjustQty,0 as OutAdjustHS,0 as OutAdjustAMT, "+
               "0 as outLariQty, 0 as outLariHS,0 as outLariAmt,(select isnull(sum(" + SaldoLaluQty + "),0)  from SaldoInventoryT1 where ItemID=M1.ItemID and yearperiod=" + periodeTahun + ")as AwalQty, 0 as AwalHS , 0 as AwalAMT from ( " +
               "select distinct A.ID as ItemID,A.partno, '" + Thn + s + bln + "' as HMY from FC_Items A  where A.ItemTypeID =1 ) as M1) as M2  " +
               "where Awalqty<>0 or InProdQty<>0 or OutProdQty<>0 or InLariQty<>0 or outLariQty<>0  order by M2.NoDocument ";

        return strSQL;

        }
        private string NyangBikin { get; set; }
        public string ViewMutasiStock(string Dari, string Sampai, string GroupPurch, string Tahun)
        {
            #region Chenge Log
            /**
             * Last Update : 10-02-2016
             * 1. Harga Kertas dikembalikan lagi ke TableHargaBankOut base on ReceiptDetailID return HargaSatuan
             * 2. Kode id table hargabankout HO :2 BPAS,100 CPD,6 SBR
             * 
             * Last Update : 04-02-2016
             * 1. Perubahan pada konversi matauang
             *      1. Jika vendor luar SuppPurch.flag=2, maka ambil kurs dari kurst tengah BI (JISDOR) base on receipt date
             *      2. Jika vendor dalam negeri tetapi menggunakan matauang asing ambil kurs dar POPurchn.NilaiKurs
             * 2. Perubahan Harga Kertas
             *      1. Untuk Supplier Kertas yang sudah tergabung dalam group Perusahaan Baru
             *         Nilai / Harga di ambil dari Table HargaKertas base on SubCompanyID
             *      2. Untuk Supplier Kertas yang belum tergabung dengan group perusahaan baru masih seperti biasa
             * on publis on 07-02-2016
             * -----------------------------------------------------------------
             * Last Update  :03-02-2015
             * Change log
             * 1. Pengambilan nilai avgprice di table ReceiptDetail dari field avgprice sebelumnya dari Price
             * 2. Penambahan query pada sessi update mengambil nilai avg price dari table saldoinventory bulan sebelumnya jika
             *    ditabel tidak ada saldo awal
             *    new
             *    Add Get itemid from data transaksi
             */
            #endregion
            #region Prepared Data
            string GrpID = GroupPurch;
            string strSQL = string.Empty;
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            string ItemTypeIDs = string.Empty;
            string NextSaldo = string.Empty;
            int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
            string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
            string periodeBlnThn = Dari.Substring(0, 6).ToString();
            switch (int.Parse(GroupPurch))
            {
                case 4: ItemTypeIDs = "2"; break;
                case 5: ItemTypeIDs = "3"; break;
                default: ItemTypeIDs = "1"; break;
            }
            #region Pemilihan bulan
            switch (fldBln)
            {
                case 1:
                    SaldoLaluQty = "DesQty";
                    SaldoLaluPrice = "DesAvgPrice";
                    NextSaldo = "JanAvgPrice";
                    periodeTahun = (Convert.ToInt16(Tahun) - 1).ToString();
                    break;
                case 2:
                    SaldoLaluQty = "JanQty";
                    SaldoLaluPrice = "JanAvgPrice";
                    NextSaldo = "FebAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 3:
                    SaldoLaluQty = "FebQty";
                    SaldoLaluPrice = "FebAvgPrice";
                    NextSaldo = "MarAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 4:
                    SaldoLaluQty = "MarQty";
                    SaldoLaluPrice = "MarAvgPrice";
                    NextSaldo = "AprAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 5:
                    SaldoLaluQty = "AprQty";
                    SaldoLaluPrice = "AprAvgPrice";
                    NextSaldo = "MeiAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 6:
                    SaldoLaluQty = "MeiQty";
                    SaldoLaluPrice = "MeiAvgPrice";
                    NextSaldo = "JunAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 7:
                    SaldoLaluQty = "JunQty";
                    SaldoLaluPrice = "JunAvgPrice";
                    NextSaldo = "JulAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 8:
                    SaldoLaluQty = "JulQty";
                    SaldoLaluPrice = "JulAvgPrice";
                    NextSaldo = "AguAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 9:
                    SaldoLaluQty = "AguQty";
                    SaldoLaluPrice = "AguAvgPrice";
                    NextSaldo = "SepAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 10:
                    SaldoLaluQty = "SepQty";
                    SaldoLaluPrice = "SepAvgPrice";
                    NextSaldo = "OktAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 11:
                    SaldoLaluQty = "OktQty";
                    SaldoLaluPrice = "OktAvgPrice";
                    NextSaldo = "NovAvgPrice";
                    periodeTahun = Tahun;
                    break;
                case 12:
                    SaldoLaluQty = "NovQty";
                    SaldoLaluPrice = "NovAvgPrice";
                    NextSaldo = "DesAvgPrice";
                    periodeTahun = Tahun;
                    break;
            }
            #endregion
            #endregion
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString() ;
            #region Query proses - hapus tabel temporary
            strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmp] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpxx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasisaldo] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapsaldoawal] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport1] /**/ ";
            #endregion
            #region Kumpulkan Data dari saldoawal,receipt, pemakain,return, adjustmen
            strSQL += 
                    "SELECT * INTO z_" + created + "_lapmutasitmp FROM( " +
                    "     (SELECT '0' AS Tipe,'" + frm + "' AS Tanggal,'Saldo Awal' AS DocNo,si.ItemID,ROUND(si." + SaldoLaluPrice + ",4) AS SaldoHS," +
                    "     si." + SaldoLaluQty + " AS SaldoQty,CASE WHEN si." + SaldoLaluPrice +
                    "     !=0  THEN ROUND(si." + SaldoLaluPrice + ",4) ELSE ROUND(si." + SaldoLaluPrice + ",4) END " +
                    "     AvgPrice,(si." + SaldoLaluQty + "* ROUND(si." + SaldoLaluPrice + ",4)) AS TotalPrice " +
                    "     FROM SaldoInventory AS si " +
                    "     WHERE si.YearPeriod='" + periodeTahun + "'" +
                    "     AND si.ItemTypeID='" + ItemTypeIDs + "' AND GroupID="+GrpID+" AND (si." + SaldoLaluQty +
                    "    !=0 OR si.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")) " +
                    "  ) UNION ALL ( " +
                          GetHargaReceipt(Dari, Sampai, GrpID, "A") +
                    "   ) UNION ALL ( " +
                    "       SELECT '2' AS Tipe,Convert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,ROUND(pk.AvgPrice,4)AvgPrice,pk.Quantity," +
                    "       ROUND(pk.AvgPrice,4) AS AvgPrice,(pk.Quantity*ROUND(pk.AvgPrice,4)) AS TotalPrice FROM Pakai AS k " +
                    "       LEFT JOIN PakaiDetail AS pk " +
                    "       ON pk.PakaiID=k.ID " +
                    "       WHERE  (cONvert(VARCHAR,k.PakaiDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "       AND pk.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + " ) AND k.Status >-1 AND pk.RowStatus >-1" +
                    "       AND pk.ItemTypeID=" + ItemTypeIDs +
                    "   ) UNION ALL  ( " +
                    "        SELECT '3' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,ROUND(rpd.AvgPrice,4)AvgPrice,rpd.Quantity, " +
                    "        ROUND(rpd.AvgPrice,4) AS AvgPrice,(rpd.Quantity*ROUND(rpd.AvgPrice,4)) AS TotalPrice FROM ReturPakai AS rp " +
                    "        LEFT JOIN ReturPakaiDetail AS rpd " +
                    "        ON rpd.ReturID=rp.ID " +
                    "        WHERE  (cONvert(VARCHAR,rp.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "        AND rpd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + " ) AND rp.Status >-1 AND rpd.RowStatus >-1 " +
                    "        AND rpd.ItemTypeID=" + ItemTypeIDs +
                    "   ) UNION ALL ( " +
                    "       SELECT '4' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Tambah' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ROUND(ad.AvgPrice,4) As SaldoHS, " +
                    "       CASE When a.AdjustType='Tambah' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPrice, " +
                    "       (ad.Quantity*ROUND(ad.AvgPrice,4)) AS TotalPrice " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Tambah'" +
                    "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND a.Status >-1 AND ad.RowStatus >-1" +
                    "       AND ad.ItemTypeID=" + ItemTypeIDs +
                    "    )UNION ALL ( " +
                    "       SELECT '5' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Kurang' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ROUND(ad.AvgPrice,4)AvgPrice, " +
                    "       CASE When a.AdjustType='Kurang' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPriceK, " +
                    "       (ad.Quantity*ROUND(ad.AvgPrice,4)) AS TotalPriceK " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Kurang' " +
                    "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND a.Status >-1 AND ad.RowStatus >-1" +
                    "       AND ad.ItemTypeID=" + ItemTypeIDs +
                    "   )UNION ALL ( " +
                    "   SELECT '6' AS Tipe, " +
                    "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                    "    CAST('0' AS Decimal(18,6)) AS AvgPrice ,CAST('0' AS Decimal(18,6)) AS Totalprice " +
                    "    FROM ReturSupplier AS rs " +
                    "    LEFT JOIN ReturSupplierDetail AS rsd " +
                    "    ON rsd.ReturID=rs.ID " +
                    "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "    AND rsd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND rs.Status >-1  " +
                    "    AND rsd.RowStatus >-1 AND rsd.ItemTypeID=" + ItemTypeIDs + " ) " +
                    ") as K /**/";
            #endregion
            #region Susun sesuai kolom
            strSQL += "/** susun sesuai dengan kolom laporan */ " +
                    "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, " +
                    "    BeliQty,BeliHS,TotalPB as BeliAmt, " +
                    "    AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, " +
                    "    ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt, " +
                    "    ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, " +
                    "    AdjProdQty,AdjProdHS AS AdjProdHS,(AdjProdQty*HPP) as AdjPAmt,RetSupQty, " +
                    "    RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                    "    (SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty, " +
                    "    ((SaldoAwalQty*HPP)+TotalPB+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                    "   (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                    "   INTO z_" + created + "_lapmutasitmpx " +
                    " FROM ( " +
                    " SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0)  ELSE 0 END SaldoAwalQty, " +
                    "     CASE WHEN Tipe='0' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(18,4))  ELSE 0 END HPP, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0)  ELSE 0 END BeliQty, " +
                    "     CASE WHEN Tipe='1' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(18,4))  ELSE 0 END BeliHS, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPB, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0)  ELSE 0 END ProdQty, " +
                    "     CASE WHEN Tipe='2' THEN CAST(ISNULL(SaldoHS,0)  AS Decimal(18,4)) ELSE 0 END ProdHS, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAd, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0)  ELSE 0 END ReturnQty, " +
                    "     CASE WHEN Tipe='3' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(18,4))  ELSE 0 END ReturHS, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPP, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjustQty, " +
                    "     CASE WHEN Tipe='4' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(18,4))  ELSE 0 END AdjustHS, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPR, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjProdQty, " +
                    "     CASE WHEN Tipe='5' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(18,4))  ELSE 0 END AdjProdHS, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAdjP, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0)  ELSE 0 END RetSupQty, " +
                    "     CASE WHEN Tipe='6' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(18,4))  ELSE 0 END RetSupHS, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalRetSup " +
                    "  FROM z_" + created + "_lapmutasitmp as x /*where ItemID in(Select ID from Inventory where GroupID=" + GrpID + ")*/" +
                    "  ) AS Z ORDER BY z.Tanggal ";
            #endregion
            #region susun data berdasarkan item id dan bentuk id baru
            strSQL +=
                    "   SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) as IDn,* INTO z_" + created + "_lapmutasitmpxx " +
                    "   FROM z_" + created + "_lapmutasitmpx " +
                    "   /**Susun  data tabular */ " +
                    "    SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo, " +
                    "    BeliQty,BeliHS, BeliAmt,AdjustQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS, " +
                    "    ProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, " +
                    "    AdjProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, " +
                    "    A.ReturnQty, " +
                    "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ReturnHS, " +
                    "    A.RetSupQty, " +
                    "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty, " +
                    "    CASE WHEN A.ID>1 THEN  " +
                    "       CASE WHEN (SELECT SUM(totalqty)FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN " +
                    "        ((SELECT SUM(totalamt) FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )/ " +
                    "        (ABS((SELECT SUM(totalqty)FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID  AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt " +
                    "    INTO z_" + created + "_lapmutasireport " +
                    "    FROM z_" + created + "_lapMutasitmpxx as A " +
                    "    ORDER by itemID,A.Tanggal,A.IDn,A.Tipe,A.DocNo  ";
            #endregion
            #region Generate Detail Report without saldo akhir 
            strSQL += "  SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                    "           l.BeliAmt,l.AdjustQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, " +
                    "           l.ProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                    "           l.AdjProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt, " +
                    "           l.ReturnQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ReturnHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                    "           l.RetSupQty, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                    "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                    "    INTO z_" + created + "_mutasireport " +
                    "  FROM z_" + created + "_lapmutasireport AS L " +
                    "  ORDER BY L.itemID,L.Tipe,L.Tanggal ";
            #endregion
            #region update colom amt dan colom hs
            strSQL += "  select row_number() over(order by itemID) as IDn,itemid into z_" + created + "_mutasireport1 " +
                     "  from z_" + created + "_mutasireport group by itemID order by itemid " +
                     "   declare @i int " +
                     "   declare @b int " +
                     "   declare @hs decimal(18,6) " +
                     "   declare @amt decimal(18,6) " +
                     "   declare @avgp decimal(18,6) " +
                     "   declare @itemTypeID int " +
                     "   declare @c int " +
                     "   declare @itm int " +
                     "   declare @itmID int " +
                     "   set @c=1 " +
                     "   set @itm=(select COUNT(IDn) from z_" + created + "_mutasireport1) " +
                     "   set @itemTypeID=" + GrpID +
                     "   IF @itemTypeID<>9 " +
                     "   BEGIN   " +
                     "   While @c <=@itm " +
                     "   Begin " +
                     "   set @itmID=(select itemID from z_" + created + "_mutasireport1 where IDn=@c) " +
                     "       set @b=1 " +
                     //"       set @avgp=(select top 1 " + SaldoLaluPrice + " from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                     "       set @avgp=(SELECT HPP from z_" + created + "_lapmutasitmpx where ItemID=@itmID AND DocNo='Saldo Awal' AND Tipe=0) " +
                     "       IF ISNULL(@avgp,0)=0 OR @avgp=0 " +
                                "begin " +
                                "set @avgp=(select top 1 HS from z_" + created + "_mutasireport where itemid=@itmID and HS>0 ) " +
                                "end " +
                     "       set @i=(select COUNT(id) from z_" + created + "_mutasireport where itemid=@itmID) " +
                     "       While @b<=@i  " +
                     "       Begin " +
                     "       set @hs=CASE WHEN @b >1 THEN (select hs from z_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "               ELSE CASE WHEN(SELECT hs from z_" + created + "_mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                     "		         (SELECT hs from z_" + created + "_mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                     "		         END  " +
                     "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from z_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "                ELSE (SELECT TotalAmt from z_" + created + "_mutasireport where ID=1 and itemid=@itmID)  " +
                     "                END  " +
                     "       /** update semua hs */ " +
                     "       update z_" + created + "_mutasireport  " +
                     "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdHS		=CASE WHEN (SELECT ProdQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdAmt    =(ProdQty*@hs), " +
                     "           AdjustAmt  =(AdjustQty*@hs), " +
                     "           AdjProdAmt =(AdjProdQty*@hs), " +
                     "           returnAmt  =(ReturnQty*@hs), " +
                     "           RetSupAmt  =(RetSupQty*@hs), " +
                     "           totalamt   =ROUND((Beliamt+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)),4), " +
                     "           hs=case when abs(SaldoAwalQty)>0 then " +
                     "             ROUND(ABS(((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty),4) else ROUND(@avgp,4) end  " +
                     "              where ID=(@b+1) and itemid=@itmID " +
                     "       set @b=@b+1 " +
                     "       END " +
                     "   set @c=@c+1 " +
                     "   END " +
                     " END ";
            #endregion
            #region Generate Saldo Awal 
            strSQL += "  SELECT ItemID,SaldoAwalQty,HS,TotalAmt INTO z_" + created + "_lapsaldoawal FROM z_" + created + "_mutasireport as m " +
                     "  WHERE m.DocNo='Saldo Awal'" +

                     "   /** Generate Saldo Akhir */ " +
                     "       SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, " +
                     "           /** pembelian */ " +
                     "             (SUM(m.BeliQty)) As BeliQty, " +
                     "             CASE WHEN SUM(m.BeliAmt) > 0 THEN SUM(m.BeliAmt)/SUM(m.BeliQty) ELSE 0 END BeliHS, " +
                     "             (SUM(m.BeliAmt)) As BeliAmt, " +
                     "           /** Ajdut Plust */ " +
                     "             (SUM(m.AdjustQty)) As AdjustQty, " +
                     "             CASE WHEN SUM(m.AdjustAmt) > 0 THEN SUM(m.AdjustAmt)/SUM(m.AdjustQty)ELSE 0 END AdjustHS, " +
                     "             (SUM(m.AdjustAmt)) As AdjustAmt, " +
                     "           /** Pemakaian Produksi */ " +
                     "             (SUM(m.ProdQty)) As ProdQty, " +
                     "             CASE WHEN SUM(m.ProdAmt) > 0 THEN SUM(m.ProdAmt)/SUM(m.ProdQty)ELSE 0 END ProdHS, " +
                     "             (SUM(m.ProdAmt)) As ProdAmt, " +
                     "           /** Adjut minus */ " +
                     "             (SUM(m.AdjProdQty)) As AdjProdQty, " +
                     "             CASE WHEN SUM(m.AdjProdAmt) > 0 THEN SUM(m.AdjProdAmt)/SUM(m.AdjProdQty) ELSE 0 END AdjProdHS, " +
                     "             (SUM(m.AdjProdAmt)) As AdjProdAmt, " +
                     "           /** Return */ " +
                     "             (SUM(m.ReturnQty)) As ReturnQty, " +
                     "             CASE WHEN SUM(m.returnAmt) > 0 THEN SUM(m.returnAmt)/SUM(m.ReturnQty) ELSE 0 END ReturnHS, " +
                     "             (SUM(m.returnAmt)) As returnAmt, " +
                     "            /** Return Supplier */ " +
                     "            (SUM(m.RetSupQty)) As RetSupQty, " +
                     "            CASE WHEN SUM(m.RetSupQty) > 0 THEN SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty))ELSE 0 END RetSupHS, " +
                     "            (SUM(m.RetSupAmt)) As RetSupAmt, " +
                     "  /** Saldo Akhir */ " +
                     "  (SELECT TOP 1 SaldoAwalQty FROM z_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                     "  (SELECT TOP 1 CAST(ISNULL(HS,0) AS Decimal(18,2)) FROM z_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +

                     "  CASE  when (SELECT  TOP  1 SaldoAwalQty FROM  z_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID  DESC)!=0 then  " +
                     "  (SELECT  TOP  1 TotalAmt FROM  z_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID DESC) " +
                     "  ELSE  0 END  As  TotalAmt " +
                     "  INTO z_" + created + "_mutasisaldo  " +
                     "  FROM z_" + created + "_mutasireport AS m  " +
                     "  GROUP BY m.ItemID /**/ ";
            #endregion
            #region generate report final 

            strSQL += "/*select isnull((SUM(ProdAmt)+SUM(AdjProdAmt)-SUM(AdjustAmt)),0) as Harga from z_" + created + "_mutasisaldo */" +
                    "/** generate report final */" +
                    " SELECT mds.ID,(SELECT dbo.ItemCodeInv(sa.ItemID," + ItemTypeIDs + "))+' - '+(SELECT dbo.ItemNameInv(sa.ItemID," + ItemTypeIDs + ")) as ItemID," +
                    "    sa.SaldoAwalQty,sa.HS as SaldoAwalHS,sa.TotalAmt as SaldoAwalAmt,mds.BeliQty,mds.BeliHS,mds.BeliAmt,mds.AdjustQty, " +
                    "    mds.AdjustHS,mds.AdjustAmt,mds.ProdQty,mds.ProdHS,mds.ProdAmt, " +
                    "    mds.AdjProdQty,mds.AdjProdHS,mds.AdjProdAmt,mds.ReturnQty,mds.ReturnHS,mds.returnAmt, " +
                    "    mds.RetSupQty,mds.RetSupHS,mds.RetSupAmt, " +
                    "    mds.SaldoAwalQty as SaldoAkhirQty,mds.HS As SaldoAkhirHS,mds.TotalAmt As SaldoAkhirAmt " +
                    "    FROM z_" + created + "_mutASisaldo AS mds " +
                    "    INNER JOIN z_" + created + "_lapsaldoawal as sa " +
                    "    ON mds.ItemID=sa.ItemID " +
                    "    WHERE sa.SaldoAwalQty != 0 OR mds.BeliQty > 0 OR mds.AdjustQty >0 OR mds.ProdQty >0 OR mds.AdjProdQty >0 OR mds.ReturnQty >0 " +
                    "    Order By itemid ";
            #endregion
            #region update SaldoAwal Bulan berikut nya 
            strSQL += (((Users)HttpContext.Current.Session["Users"]).UserName == "admin") ? "declare @sqlS nvarchar(max) " +
                     "  set @sqlS ='update A set A." + NextSaldo + "=ROUND(ISNULL(B.HS,0),4)   from SaldoInventory  A inner join z_" + created + "_mutASisaldo B  on A.ItemID=B.itemid " +
                     "  where A.YearPeriod=" + periodeTahun + "' exec sp_executesql @sqlS, N'' " : "";
            #endregion
            #region Hapus table temporari

            strSQL += "/** return supplier  no avgprice*/" +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmp] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpx] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpxx] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasireport] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasisaldo] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapsaldoawal] ";
            #endregion
            return strSQL;
        }
        public string ViewMutasiStockLapbul(string Dari, string Sampai, string GroupPurch, string Tahun)
        {
            #region Chenge Log
            /**
             * Added for LapBul type Potrait
             * Last Update : 10-02-2016
             * 1. Harga Kertas dikembalikan lagi ke TableHargaBankOut base on ReceiptDetailID return HargaSatuan
             * 2. Kode id table hargabankout HO :2 BPAS,100 CPD,6 SBR
             * 
             * Last Update : 04-02-2016
             * 1. Perubahan pada konversi matauang
             *      1. Jika vendor luar SuppPurch.flag=2, maka ambil kurs dari kurst tengah BI (JISDOR) base on receipt date
             *      2. Jika vendor dalam negeri tetapi menggunakan matauang asing ambil kurs dar POPurchn.NilaiKurs
             * 2. Perubahan Harga Kertas
             *      1. Untuk Supplier Kertas yang sudah tergabung dalam group Perusahaan Baru
             *         Nilai / Harga di ambil dari Table HargaKertas base on SubCompanyID
             *      2. Untuk Supplier Kertas yang belum tergabung dengan group perusahaan baru masih seperti biasa
             * on publis on 07-02-2016
             * -----------------------------------------------------------------
             * Last Update  :03-02-2015
             * Change log
             * 1. Pengambilan nilai avgprice di table ReceiptDetail dari field avgprice sebelumnya dari Price
             * 2. Penambahan query pada sessi update mengambil nilai avg price dari table saldoinventory bulan sebelumnya jika
             *    ditabel tidak ada saldo awal
             *    new
             *    Add Get itemid from data transaksi
             */
            #endregion
            #region Prepared Data
            string GrpID = GroupPurch;
            string strSQL = string.Empty;
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            int ItemTypeID=0;
            int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
            string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
            string periodeBlnThn = Dari.Substring(0, 6).ToString();
            switch(int.Parse(GrpID))
            {
                case 5: ItemTypeID = 3; break;
                case 4: ItemTypeID = 2; break;
                case 12: ItemTypeID = 2; break;
                default: ItemTypeID = 1; break;
            }
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
            #endregion
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasitmp] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasitmpx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasitmpxx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_mutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_mutasisaldo] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapsaldoawal] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_mutasireport1] " +
                    "/** Kumpulkan data dari beberapa tabel dengan UNION ALL all */ " +
                    "SELECT * INTO lapbul_" + created + "_lapmutasitmp FROM( " +
                    "     (SELECT '0' AS Tipe,'" + frm + "' AS Tanggal,'Saldo Awal' AS DocNo,si.ItemID,si." + SaldoLaluPrice + " AS SaldoHS," +
                    "     si." + SaldoLaluQty + " AS SaldoQty,CASE WHEN si." + SaldoLaluPrice + " >=0  THEN si." + SaldoLaluPrice + " ELSE 0 END " +
                    "     AvgPrice,(si." + SaldoLaluQty + "*si." + SaldoLaluPrice + ") AS TotalPrice " +
                    "     FROM SaldoInventory AS si " +
                    "     WHERE si.groupID="+ GrpID +" and si.YearPeriod='" + periodeTahun + "' /*AND ) */" +
                    "     AND si.ItemTypeID='" + ItemTypeID + "' and (si." + SaldoLaluQty + "!=0 ) " +
                    "  ) UNION ALL ( " +
                          GetHargaReceipt(Dari, Sampai, GrpID, "A") +
                    "   ) UNION ALL ( " +
                    "       SELECT '2' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity," +
                    "       (pk.AvgPrice) AS AvgPrice,(pk.Quantity*pk.AvgPrice) AS TotalPrice FROM Pakai AS k " +
                    "       LEFT JOIN PakaiDetail AS pk " +
                    "       ON pk.PakaiID=k.ID " +
                    "       WHERE  pk.groupID=" + GrpID + " and (cONvert(VARCHAR,k.PakaiDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "        AND pk.ItemTypeID=" + ItemTypeID + " AND k.Status >-1 AND pk.RowStatus >-1" +
                    "   ) UNION ALL  ( " +
                    "        SELECT '3' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity, " +
                    "        (rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp " +
                    "        LEFT JOIN ReturPakaiDetail AS rpd " +
                    "        ON rpd.ReturID=rp.ID " +
                    "        WHERE  rpd.groupID=" + GrpID + " and (cONvert(VARCHAR,rp.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "         AND rpd.ItemTypeID=" + ItemTypeID + " AND rp.Status >-1 AND rpd.RowStatus >-1 " +
                    "   ) UNION ALL ( " +
                    "       SELECT '4' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Tambah' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice As SaldoHS, " +
                    "       CASE When a.AdjustType='Tambah' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPrice, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPrice " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  ad.groupID=" + GrpID + " and (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Tambah'" +
                    "        AND ad.ItemTypeID=" + ItemTypeID + " AND a.Status >-1 AND ad.RowStatus >-1" +
                    "    )UNION ALL ( " +
                    "       SELECT '5' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Kurang' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "       CASE When a.AdjustType='Kurang' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPriceK, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPriceK " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID inner join (" + GetItemID(periodeBlnThn, GrpID) + ")ii on ad.itemid=ii.itemid " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Kurang' " +
                    "        AND ad.ItemTypeID=" + ItemTypeID + " AND a.Status >-1 AND ad.RowStatus >-1" +
                    "   )UNION ALL ( " +
                    "   SELECT '6' AS Tipe, " +
                    "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                    "    CAST('0' AS Decimal(18,6)) AS AvgPrice ,CAST('0' AS Decimal(18,6)) AS Totalprice " +
                    "    FROM ReturSupplier AS rs " +
                    "    LEFT JOIN ReturSupplierDetail AS rsd " +
                    "    ON rsd.ReturID=rs.ID " +
                    "    where rsd.groupID=" + GrpID + " and (convert(varchar,rs.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "     AND rs.Status >-1  " +
                    "    AND rsd.RowStatus >-1 AND rsd.ItemTypeID=" + ItemTypeID + " ) " +
                    ") as K " +
                    "/** susun sesuai dengan kolom laporan */ " +
                    "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, " +
                    "    BeliQty,BeliHS,TotalPB as BeliAmt, " +
                    "    AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, " +
                    "    ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt, " +
                    "    ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, " +
                    "    AdjProdQty,AdjProdHS AS AdjProdHS,(AdjProdQty*HPP) as AdjPAmt,RetSupQty, " +
                    "    RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                    "    (SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty, " +
                    "    ((SaldoAwalQty*HPP)+TotalPB+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                    "   (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                    "   INTO lapbul_" + created + "_lapmutasitmpx " +
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
                    "  FROM lapbul_" + created + "_lapmutasitmp as x where ItemID in(" + GetItemID(periodeBlnThn, GrpID) + ")) AS Z ORDER BY z.Tanggal " +
                    "   /** susun data berdasarkan item id dan bentuk id baru */ " +
                //"   SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY itemID,ID,DocNo) as IDn,* INTO lapmutasitmpxx " +
                    "   SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) as IDn,* INTO lapbul_" + created + "_lapmutasitmpxx " +
                    "   FROM lapbul_" + created + "_lapmutasitmpx " +
                    "   /**Susun  data tabular */ " +
                    "    SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo, " +
                    "    BeliQty,BeliHS, BeliAmt,AdjustQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS, " +
                    "    ProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, " +
                    "    AdjProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, " +
                    "    A.ReturnQty, " +
                    "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ReturnHS, " +
                    "    A.RetSupQty, " +
                    "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty, " +
                    "    CASE WHEN A.ID>1 THEN  " +
                    "       CASE WHEN (SELECT SUM(totalqty)FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN " +
                    "        ((SELECT SUM(totalamt) FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )/ " +
                    "        (ABS((SELECT SUM(totalqty)FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM lapbul_" + created + "_lapMutasitmpxx WHERE ID <=A.ID  AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt " +
                    "    INTO lapbul_" + created + "_lapmutasireport " +
                    "    FROM lapbul_" + created + "_lapMutasitmpxx as A " +
                    "    ORDER by itemID,A.Tanggal,A.IDn,A.Tipe,A.DocNo  " +

                    "   /** Generate Detail Report without saldo akhir */ " +
                    "  SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                    "           l.BeliAmt,l.AdjustQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, " +
                    "           l.ProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                    "           l.AdjProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt, " +
                    "           l.ReturnQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ReturnHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                    "           l.RetSupQty, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT ISNULL(HS,0) FROM lapbul_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                    "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                    "    INTO lapbul_" + created + "_mutasireport " +
                    "  FROM lapbul_" + created + "_lapmutasireport AS L " +
                    "  /*WHERE l.ItemID IN (" + GetItemID(periodeBlnThn, GrpID) + ")*/ ORDER BY L.itemID,L.Tipe,L.Tanggal " +

                     "  /** update colom amt dan colom hs */ " +
                     "  select row_number() over(order by itemID) as IDn,itemid into lapbul_" + created + "_mutasireport1 " +
                     "  from lapbul_" + created + "_mutasireport group by itemID order by itemid " +
                     "   declare @i int " +
                     "   declare @b int " +
                     "   declare @hs decimal(18,6) " +
                     "   declare @amt decimal(18,6) " +
                     "   declare @avgp decimal(18,6) " +
                     "   declare @itemTypeID int " +
                     "   declare @c int " +
                     "   declare @itm int " +
                     "   declare @itmID int " +
                     "   set @c=1 " +
                     "   set @itm=(select COUNT(IDn) from lapbul_" + created + "_mutasireport1) " +
                     "   set @itemTypeID=(select GroupID from Inventory where ID=(select top 1 ItemID from lapbul_" + created + "_mutasireport))" +
                     "  IF @itemTypeID<>9 " +
                     "   BEGIN   " +
                     "   While @c <=@itm " +
                     "   Begin " +
                     "   set @itmID=(select itemID from lapbul_" + created + "_mutasireport1 where IDn=@c) " +
                     "       set @b=1 " +
                     "       set @avgp=(select top 1 " + SaldoLaluPrice + " from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                      "       IF ISNULL(@avgp,0)=0 OR @avgp=0 " +
                                "begin " +
                                "set @avgp=(select top 1 HS from lapbul_" + created + "_mutasireport where itemid=@itmID and HS>0 ) " +
                                "end " +
                     "       set @i=(select COUNT(id) from lapbul_" + created + "_mutasireport where itemid=@itmID) " +
                     "       While @b<=@i  " +
                     "       Begin " +
                     "       set @hs=CASE WHEN @b >1 THEN (select hs from lapbul_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "               ELSE CASE WHEN(SELECT hs from lapbul_" + created + "_mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                     "		         (SELECT hs from lapbul_" + created + "_mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                     "		         END  " +
                     "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from lapbul_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "                ELSE (SELECT TotalAmt from lapbul_" + created + "_mutasireport where ID=1 and itemid=@itmID)  " +
                     "                END  " +
                     "       /** update semua hs */ " +
                     "       update lapbul_" + created + "_mutasireport  " +
                     "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM lapbul_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdHS		=CASE WHEN (SELECT ProdQty FROM lapbul_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM lapbul_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM lapbul_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM lapbul_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdAmt    =(ProdQty*@hs), " +
                     "           AdjustAmt  =(AdjustQty*@hs), " +
                     "           AdjProdAmt =(AdjProdQty*@hs), " +
                     "           returnAmt  =(ReturnQty*@hs), " +
                     "           RetSupAmt  =(RetSupQty*@hs), " +
                     "           totalamt   =(Beliamt+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)), " +
                     "           hs=case when abs(SaldoAwalQty)>0 then " +
                     "             ABS(((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty) else @avgp end  " +
                     "              where ID=(@b+1) and itemid=@itmID " +
                     "       set @b=@b+1 " +
                     "       END " +
                     "   set @c=@c+1 " +
                     "   END " +
                     " END " +

                    "  /** Generate Saldo Awal */ " +
                    "  SELECT ItemID,SaldoAwalQty,HS,TotalAmt " +
                    "  INTO lapbul_" + created + "_lapsaldoawal " +
                    "  FROM lapbul_" + created + "_mutasireport as m " +
                    "  WHERE m.DocNo='Saldo Awal' /*AND m.ItemID in(" + GetItemID(periodeBlnThn, GrpID) + ")*/ " +

                     "   /** Generate Saldo Akhir */ " +
                     "       SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, " +
                     "           /** pembelian */ " +
                     "             (SUM(m.BeliQty)) As BeliQty, " +
                     "             CASE WHEN SUM(m.BeliAmt) > 0 THEN SUM(m.BeliAmt)/SUM(m.BeliQty) ELSE 0 END BeliHS, " +
                     "             (SUM(m.BeliAmt)) As BeliAmt, " +
                     "           /** Ajdut Plust */ " +
                     "             (SUM(m.AdjustQty)) As AdjustQty, " +
                     "             CASE WHEN SUM(m.AdjustAmt) > 0 THEN SUM(m.AdjustAmt)/SUM(m.AdjustQty)ELSE 0 END AdjustHS, " +
                     "             (SUM(m.AdjustAmt)) As AdjustAmt, " +
                     "           /** Pemakaian Produksi */ " +
                     "             (SUM(m.ProdQty)) As ProdQty, " +
                     "             CASE WHEN SUM(m.ProdAmt) > 0 THEN SUM(m.ProdAmt)/SUM(m.ProdQty)ELSE 0 END ProdHS, " +
                     "             (SUM(m.ProdAmt)) As ProdAmt, " +
                     "           /** Adjut minus */ " +
                     "             (SUM(m.AdjProdQty)) As AdjProdQty, " +
                     "             CASE WHEN SUM(m.AdjProdAmt) > 0 THEN SUM(m.AdjProdAmt)/SUM(m.AdjProdQty) ELSE 0 END AdjProdHS, " +
                     "             (SUM(m.AdjProdAmt)) As AdjProdAmt, " +
                     "           /** Return */ " +
                     "             (SUM(m.ReturnQty)) As ReturnQty, " +
                     "             CASE WHEN SUM(m.returnAmt) > 0 THEN SUM(m.returnAmt)/SUM(m.ReturnQty) ELSE 0 END ReturnHS, " +
                     "             (SUM(m.returnAmt)) As returnAmt, " +
                     "            /** Return Supplier */ " +
                     "            (SUM(m.RetSupQty)) As RetSupQty, " +
                     "            CASE WHEN SUM(m.RetSupQty) > 0 THEN SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty))ELSE 0 END RetSupHS, " +
                     "            (SUM(m.RetSupAmt)) As RetSupAmt, " +
                     "  /** Saldo Akhir */ " +
                     "  (SELECT TOP 1 SaldoAwalQty FROM lapbul_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                     "  (SELECT TOP 1 HS FROM lapbul_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +

                     "  CASE  when (SELECT  TOP  1 SaldoAwalQty FROM  lapbul_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID  DESC)>0 then  " +
                     "  (SELECT  TOP  1 TotalAmt FROM  lapbul_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID DESC) " +
                     "  ELSE  0 END  As  TotalAmt " +
                     "  INTO lapbul_" + created + "_mutasisaldo  " +
                     "  FROM lapbul_" + created + "_mutasireport AS m  " +
                     "  /*WHERE m.ItemID in(" + GetItemID(periodeBlnThn, GrpID) + ") */" +
                     "  GROUP BY m.ItemID  " ;
            return strSQL;
        }
        public string ViewMutasiRepack(string Dari, string Sampai, string GroupPurch, string Tahun)
        {
            /**
             * Last Update  :17-04-2013
             * Change log
             * 1. Pengambilan nilai avgprice di table Convertan dari field avgprice sebelumnya dari Price
             * 2. Penambahan query pada sessi update mengambil nilai avg price dari table saldoinventory bulan sebelumnya jika
             *    ditabel tidak ada saldo awal
             */

            #region Prepared Data
            string GrpID = GroupPurch;
            string strSQL = string.Empty;
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
            string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
            string periodeBlnThn = Dari.Substring(0, 6).ToString();
            
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
            #endregion
            #region Query String
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
                    "     si." + SaldoLaluQty + " AS SaldoQty,CASE WHEN si." + SaldoLaluQty + " >0  THEN si." + SaldoLaluPrice + " ELSE 0 END " +
                    "     AvgPrice,(si." + SaldoLaluQty + "*si." + SaldoLaluPrice + ") AS TotalPrice " +
                    "     FROM SaldoInventory AS si " +
                    "     WHERE si.YearPeriod='" + periodeTahun + "' /*AND ) */" +
                    "     AND si.ItemTypeID='1' and (si." + SaldoLaluQty + ">=0 or si.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")) " +
                    "  ) UNION ALL ( " +
                    "     (SELECT '1' AS Tipe, cONvert(VARCHAR,p.CreatedTime,105) AS Tanggal,p.RepackNo ,p.ToItemID as ItemID, " +
                    "     isnull((select top 1 ReceiptDetail.AvgPrice from ReceiptDetail where ItemID=p.FromItemID and ReceiptDetail.AvgPrice>0 order by ID desc),0) as Price ,p.ToQty, " +
                    "     isnull((select top 1 ReceiptDetail.AvgPrice from ReceiptDetail where ItemID=p.FromItemID and ReceiptDetail.AvgPrice>0 order by ID desc),0) AS AvgPrice,  " +
                    "     (p.ToQty * isnull((select top 1 ReceiptDetail.AvgPrice from ReceiptDetail where ItemID=p.FromItemID and ReceiptDetail.AvgPrice>0 order by ID desc),0)) AS TotalPrice  " +
                    "     FROM Convertan AS p  " +
                    "     /*LEFT JOIN ReceiptDetail AS rd  " +
                    "     ON rd.ItemID=p.FromItemID " +
                    "     INNER JOIN Receipt as r " +
                    "     ON r.ID=rd.ReceiptID  */" +
                    "     WHERE  (cONvert(VARCHAR,p.CreatedTime,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "     AND p.ToItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND p.RowStatus >-1 /*AND rd.RowStatus >-1*/ " +
                    "     ) " +
                    "   ) UNION ALL ( " +
                    "       SELECT '2' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity," +
                    "       (pk.AvgPrice) AS AvgPrice,(pk.Quantity*pk.AvgPrice) AS TotalPrice FROM Pakai AS k " +
                    "       LEFT JOIN PakaiDetail AS pk " +
                    "       ON pk.PakaiID=k.ID " +
                    "       WHERE  (cONvert(VARCHAR,k.PakaiDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "       AND pk.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND k.Status >-1 AND pk.RowStatus >-1" +
                    "   ) UNION ALL  ( " +
                    "        SELECT '3' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity, " +
                    "        (rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp " +
                    "        LEFT JOIN ReturPakaiDetail AS rpd " +
                    "        ON rpd.ReturID=rp.ID " +
                    "        WHERE  (cONvert(VARCHAR,rp.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "        AND rpd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND rp.Status >-1 AND rpd.RowStatus >-1 " +
                    "   ) UNION ALL ( " +
                    "       SELECT '4' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Tambah' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice As SaldoHS, " +
                    "       CASE When a.AdjustType='Tambah' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPrice, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPrice " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Tambah'" +
                    "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND a.Status >-1 AND ad.RowStatus >-1" +
                    "    )UNION ALL ( " +
                    "       SELECT '5' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Kurang' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "       CASE When a.AdjustType='Kurang' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPriceK, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPriceK " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Kurang' " +
                    "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND a.Status >-1 AND ad.RowStatus >-1" +
                    "   )UNION ALL ( " +
                    "   SELECT '6' AS Tipe, " +
                    "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                    "    CAST('0' AS Decimal(18,6)) AS AvgPrice ,CAST('0' AS Decimal(18,6)) AS Totalprice " +
                    "    FROM ReturSupplier AS rs " +
                    "    LEFT JOIN ReturSupplierDetail AS rsd " +
                    "    ON rsd.ReturID=rs.ID " +
                    "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "    AND rsd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND rs.Status >-1  " +
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
                    "  FROM lapmutasitmp as x where ItemID in(Select ID from Inventory where GroupID=" + GrpID + ")) AS Z ORDER BY z.Tanggal " +
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
                    "    ORDER by itemID,A.Tanggal,a.Tipe,a.DocNo  " +

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
                    "  /*WHERE l.ItemID IN (" + GetItemID(periodeBlnThn, GrpID) + ")*/ ORDER BY L.itemID,L.Tipe,L.Tanggal " +

                    "  /** update colom amt dan colom hs */ " +
                     "  select row_number() over(order by itemID) as IDn,itemid into mutasireport1 " +
                     "  from mutasireport group by itemID order by itemid " +
                     "   declare @i int " +
                     "   declare @b int " +
                     "   declare @hs decimal(18,6) " +
                     "   declare @amt decimal(18,6) " +
                     "   declare @avgp decimal(18,6) " +
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
                     "       set @avgp=(select top 1 "+SaldoLaluPrice+" from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
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
                    "  WHERE m.DocNo='Saldo Awal' /*AND m.ItemID in("+ GetItemID(periodeBlnThn,GrpID)+ ")*/ " +

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
                     "  /*WHERE m.ItemID in(" + GetItemID(periodeBlnThn, GrpID) + ")*/ " +
                     "  GROUP BY m.ItemID  " +

            "  /** generate report final */ " +

            "    SELECT mds.ID,(SELECT (ItemCode +' '+ ItemName) as ItemName From Inventory WHERE ID=mds.ItemID) as ItemID,sa.SaldoAwalQty,sa.HS as SaldoAwalHS,sa.TotalAmt as SaldoAwalAmt,mds.BeliQty,mds.BeliHS,mds.BeliAmt,mds.AdjustQty, " +
            "    mds.AdjustHS,mds.AdjustAmt,mds.ProdQty,mds.ProdHS,mds.ProdAmt, " +
            "    mds.AdjProdQty,mds.AdjProdHS,mds.AdjProdAmt,mds.ReturnQty,mds.ReturnHS,mds.returnAmt, " +
            "    mds.RetSupQty,mds.RetSupHS,mds.RetSupAmt, " +
            "    mds.SaldoAwalQty as SaldoAkhirQty,mds.HS As SaldoAkhirHS,mds.TotalAmt As SaldoAkhirAmt " +
            "    FROM mutASisaldo AS mds " +
            "    INNER JOIN lapsaldoawal as sa " +
            "    ON mds.ItemID=sa.ItemID " +
            "    WHERE sa.SaldoAwalQty > 0 OR mds.BeliQty > 0 OR mds.AdjustQty >0 OR mds.ProdQty >0 OR mds.AdjProdQty >0 OR mds.ReturnQty >0 " +
            "    Order By itemid " +
            "/** return supplier  no avgprice*/" +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmp] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpx] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpxx] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasireport] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[mutasireport] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[mutasisaldo] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[lapsaldoawal] ";
            #endregion
            return strSQL;
        }
        public string FromProsesPosting { get; set; }
        public string ViewMutasiStockByName(string Bln, string Tahun, string IDiTems)
        {
            /** added on 15-10-2014
             * Query Mutasi Stok bahan baku detail
             * Last Update 12-11-2019
             * Skenario :
             * 1. Saldo Awal di ambil dari Data di table SaldoInventory (Qty dan AvgPrice) bulan sebelumnya
             * 2. Rumus Perhitungan Saldo Alkhir Qty dan Amt
             *    (Saldo Awal + Receipt + Penyesuaian Tambah - Pakai - Penyesuaian Kurang + ReturnPakai - ReturnSuplier)
             * 3. Saldo Akhir HS = SaldoAkhirQty / SaldoAkhirAmt
             * 4. Nilai HS selain nilai HS Receipt adalah SaldoAkhirHS Baris sebelumnya ( Jika baris pertama maka nilai
             *    SaldoAkhirHS adalah AvgPrice Saldo Awal bulan sebelumnya
             * 5. Nilai AMT di masing - masing kolom adalah hasil dari  masing - masing kolom Qty * HS
             * 6. Baris Total Qty adalah sum(kolom Qty) kecuali kolom saldo akhir qty adalah nilai dari data qty terakhir
             * 7. Baris Total Amt Adalah sum(kolom Amt) kecuali kolom saldo akhir amt adalah nilai dari data amt terakhir
             * 8. Baris Total Hs adalah sum(kolom amt)/sum(kolom qty) kecuali saldo akhir hs adalan nilai dari hs terakhir
             * 
             */
            #region prepared data
            string strSQL = string.Empty;
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            string FirstPeriod = string.Empty;
            string LastPeriod = string.Empty;
            int fldBln = Convert.ToInt16(Bln);
            string LockPeriode = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LockPeriod", "COGS");
            string periodeBlnThn = Tahun + Bln.PadLeft(2, '0');
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
            string bln = fldBln.ToString();
            string s = new string('0', (2 - bln.Length));
            int lastDay = DateTime.DaysInMonth((Convert.ToInt16(periodeTahun)), fldBln);
            string d = new string('0', (2 - lastDay.ToString().Length));
            FirstPeriod = Tahun + s + fldBln + "01";
            LastPeriod = Tahun + s + fldBln + d + lastDay;
            #endregion
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            #region Query String
            strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmp] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmpx] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasisaldo] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport1] " +
                    "SELECT * INTO zd_" + created + "_lapmutasitmp FROM(  " +
                    "(SELECT '0' as Tipe,'01-" + s + fldBln + '-' + Tahun + "' as Tanggal,'Saldo Awal' as DocNo,si.ItemID,si." + SaldoLaluPrice + " as SaldoHS, " +
                    "    si." + SaldoLaluQty + " as SaldoQty,si." + SaldoLaluPrice + " as AvgPrice,(si." + SaldoLaluQty + "*si." + SaldoLaluPrice + ") as TotalPrice " +
                    "    from SaldoInventory as si " +
                    "    where si.YearPeriod='" + periodeTahun + "' AND si.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') " +
                    "    AND si.ItemTypeID='1' " +

                    ") UNION ALL  (" +
                    GetHargaReceipt(FirstPeriod, LastPeriod, this.MaterialGroup, IDiTems) +
                    ") UNION ALL ( " +
                    "SELECT '2' as Tipe,convert(varchar,k.PakaiDate,105) as Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity, " +
                    "    (pk.AvgPrice) as AvgPrice,(pk.Quantity*pk.AvgPrice) as TotalPrice from Pakai as k  " +
                    "    left join PakaiDetail as pk  " +
                    "    on pk.PakaiID=k.ID " +
                    "    where  (convert(varchar,k.PakaiDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND pk.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') " +
                    "    AND k.Status >-1 AND pk.RowStatus >-1 AND pk.ItemTypeID=1" +
                    ") UNION ALL  ( " +
                    "SELECT '3' as Tipe, CONVERT(varchar,rp.ReturDate,105) as Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity, " +
                    "    (rpd.AvgPrice) as AvgPrice,(rpd.Quantity*rpd.AvgPrice) as TotalPrice from ReturPakai as rp " +
                    "    left join ReturPakaiDetail as rpd " +
                    "    on rpd.ReturID=rp.ID " +
                    "    where  (convert(varchar,rp.ReturDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND rpd.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') " +
                    "    AND rp.Status >-1 AND rpd.RowStatus>-1 AND rp.ItemTypeID=1" +
                    ") UNION ALL ( " +
                    "SELECT '4' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='Tambah' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='Tambah' Then ad.Quantity else ad.Quantity END AdjQty,'0' as AvgPrice, " +
                    "    (ad.Quantity*ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='Tambah' AND ad.ItemTypeID=1" +
                    ")UNION ALL ( " +
                    "SELECT '5' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='Kurang' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='Kurang' Then ad.Quantity else ad.Quantity END AdjQty,'0' as AvgPrice, " +
                    "    (ad.Quantity*ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='Kurang' AND ad.ItemTypeID=1" +
                    ")UNION ALL( " +

                    "SELECT '4' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='TambahHarga' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='TambahHarga' Then ad.Quantity else ad.Quantity END AdjQty,ad.AvgPrice as AvgPrice, " +
                    "    (ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='TambahHarga' AND ad.ItemTypeID=1" +
                    ")UNION ALL ( " +
                    "SELECT '5' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='KurangHarga' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='KurangHarga' Then ad.Quantity else ad.Quantity END AdjQty,ad.AvgPrice  as AvgPrice, " +
                    "    (ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='KurangHarga' AND ad.ItemTypeID=1" +
                    ")UNION ALL( " +

                    "SELECT '6' AS Tipe, " +
                    "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                    "    CAST('0' AS Decimal(18,6)) AS AvgPrice ,CAST('0' AS Decimal(18,6)) AS Totalprice " +
                    "    FROM ReturSupplier AS rs " +
                    "    LEFT JOIN ReturSupplierDetail AS rsd " +
                    "    ON rsd.ReturID=rs.ID " +
                    "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND rsd.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND rs.Status >-1  " +
                    "    AND rsd.RowStatus >-1  AND rsd.ItemTypeID=1)" +
                    ") as K " +

                    "/** susun sesuai dengan kolom laporan */ " +
                    "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, " +
                    "    BeliQty,BeliHS,TotalPB as BeliAmt, " +
                    "    AdjustQty,AdjustHS AS AdjustHS,case when docno<>'Adjust Harga' then (AdjustQty*HPP) else (HPP) end AdjAmt, " +
                    "    ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt, " +
                    "    ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, " +
                    "    AdjProdQty,AdjProdHS AS AdjProdHS,case when docno<>'Adjust Harga' then (AdjProdQty*HPP) else (HPP) end AdjPAmt,RetSupQty, " +
                    "    RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                    "    ((SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty)) as TotalQty, " +
                    "    case when Docno<>'Adjust Harga' then ((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                    "   (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) else " + 
                    "   ((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustHS)-(ProdQty*ProdHS)-(AdjProdHS)-(RetSupQty*RetSupHs)) end TotalAmt  " +
                    "   INTO zd_" + created + "_lapmutasitmpx " +
                    " FROM ( " +
                    " SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe) as ID,Tipe,itemID,Tanggal,DocNo, " +
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
                    "  FROM zd_" + created + "_lapmutasitmp as x) AS Z ORDER BY z.Tanggal,z.Tipe " +

                    "   /**Susun  data tabular */ " +
                    "    SELECT ROW_NUMBER() OVER(Partition BY itemID order by Tanggal,Tipe) AS ID,Tipe,itemID,Tanggal,DocNo, " +
                    "    BeliQty,BeliHS, BeliAmt,AdjustQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) when A.ID>1 AND A.DocNo='Adjust Harga' THEN A.AdjustHS ELSE A.HPP END AdjustHS, " +
                    "    ProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) ELSE A.HPP END ProdHS, " +
                    "    AdjProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID)  when A.ID>1 AND A.DocNo='Adjust Harga' THEN A.AdjProdHS ELSE A.HPP END AdjProdHS, " +
                    "    A.ReturnQty, " +
                    "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) ELSE A.HPP END ReturHS, " +
                    "    A.RetSupQty, " +
                    "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) ELSE A.HPP END RetSupHS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )ELSE TotalQty END SaldoAwalQty, " +
                    "    CASE WHEN A.ID>1 THEN  " +
                    "       CASE WHEN (SELECT SUM(totalqty)FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )>=1 THEN " +
                    "        ((SELECT SUM(totalamt) FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )/ " +
                    "        (ABS((SELECT SUM(totalqty)FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID ))))ELSE 0 END ELSE HPP END HS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )ELSE Totalamt END TotalAmt " +
                    "    INTO zd_" + created + "_lapmutasireport " +
                    "    FROM zd_" + created + "_lapmutasitmpx as A " +
                    "    order by A.Tanggal,a.Tipe " +

                    "   /** Generate Detail Report without saldo akhir */ " +
                    "  SELECT ROW_NUMBER() OVER(Partition BY itemID order by Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                    "           l.BeliAmt,l.AdjustQty, " +
                    "           CASE WHEN L.ID >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID) when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjustHS ELSE 0 END AdjustHS, " +
                    "           CASE WHEN L.ID >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.AdjustQty when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjustHS ELSE 0 END AdjustAmt, " +
                    "           l.ProdQty, " +
                    "           CASE WHEN L.ID >1 AND l.ProdQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                    "           CASE WHEN L.ID >1 AND l.ProdQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                    "           l.AdjProdQty, " +
                    "           CASE WHEN L.ID >1 AND l.AdjProdQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)  when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjProdHS  ELSE 0 END AdjProdHS, " +
                    "           CASE WHEN L.ID >1 AND l.AdjProdQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.AdjProdQty  when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjProdHS ELSE 0 END AdjProdAmt, " +
                    "           l.ReturnQty, " +
                    "           CASE WHEN L.ID >1 AND l.ReturnQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID) when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjustHS  ELSE 0 END ReturnHS, " +
                    "           CASE WHEN L.ID >1 AND l.ReturnQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                    "           l.RetSupQty, " +
                    "           CASE WHEN L.ID >1 AND l.RetSupQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                    "           CASE WHEN L.ID >1 AND l.RetSupQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                    "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                    "    INTO zd_" + created + "_mutasireport " +
                    "    FROM zd_" + created + "_lapmutasireport AS L  " +
                    "    WHERE l.ItemID IN (SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') ORDER BY L.Tanggal,L.Tipe " +
            #region 
                    "  /** update colom amt dan colom hs */ " +
                     "  select row_number() over(order by itemID) as IDn,itemid into zd_" + created + "_mutasireport1 " +
                     "  from zd_" + created + "_mutasireport group by itemID order by itemid " +
                     "   declare @i int " +
                     "   declare @b int " +
                     "   declare @hs decimal(18,6) " +
                     "   declare @amt decimal(18,6) " +
                     "   declare @avgp decimal(18,6) " +
                     "   declare @itemTypeID int " +
                     "   declare @c int " +
                     "   declare @itm int " +
                     "   declare @itmID int " +
                     "   set @c=1 " +
                     "   set @itm=(select COUNT(IDn) from zd_" + created + "_mutasireport1) " +
                     "   set @itemTypeID=(select GroupID from Inventory where ID=(select top 1 ItemID from zd_" + created + "_mutasireport))" +
                     "  IF @itemTypeID<>9 " +
                     "   BEGIN   " +
                     "   While @c <=@itm " +
                     "   Begin " +
                     "   set @itmID=(select itemID from zd_" + created + "_mutasireport1 where IDn=@c) " +
                     "       set @b=1 " +
                     "       set @avgp=(select top 1 " + SaldoLaluPrice + " from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                     "       IF ISNULL(@avgp,0)=0 OR @avgp=0 " +
                                "begin " +
                                "set @avgp=(select top 1 HS from zd_" + created + "_mutasireport where itemid=@itmID and HS>0 ) " +
                                "end " + 
                     "       set @i=(select COUNT(id) from zd_" + created + "_mutasireport where itemid=@itmID) " +
                     "       While @b<=@i  " +
                     "       Begin " +
                     "       set @hs=CASE WHEN @b >1 THEN (select hs from zd_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "               ELSE CASE WHEN(SELECT hs from zd_" + created + "_mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                     "		         (SELECT hs from zd_" + created + "_mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                     "		         END  " +
                     "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from zd_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "                ELSE (SELECT TotalAmt from zd_" + created + "_mutasireport where ID=1 and itemid=@itmID)  " +
                     "                END  " +
                     "       /** update semua hs */ " +
                     "       update zd_" + created + "_mutasireport  " +
                     "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdHS		=CASE WHEN (SELECT ProdQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdAmt    =(ProdQty*@hs), " +
                     "           AdjustAmt  =(AdjustQty*@hs), " +
                     "           AdjProdAmt =(AdjProdQty*@hs), " +
                     "           returnAmt  =(ReturnQty*@hs), " +
                     "           RetSupAmt  =(RetSupQty*@hs), " +
                     "           totalamt   =(Beliamt+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)), " +
                     "           hs=case when abs(SaldoAwalQty)>0 then " +
                     "             ABS(((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty) else @avgp end  " +
                     "              where ID=(@b+1) and itemid=@itmID " +
                     "       set @b=@b+1 " +
                     "       END " +
                     "   set @c=@c+1 " +
                     "   END " +
                     " END " +

            #endregion
                     "   /** Generate Saldo Akhir */ " +
                     "       SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, " +
                     "           /** pembelian */ " +
                     "             (SUM(m.BeliQty)) As BeliQty, " +
                     "             CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, " +
                     "             (SUM(m.BeliAmt)) As BeliAmt, " +
                     "           /** Ajdut Plust */ " +
                     "             (SUM(m.AdjustQty)) As AdjustQty, " +
                     "             CASE WHEN SUM(m.AdjustQty) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty)) WHEN SUM(m.AdjustQty)=0 and SUM(m.AdjustAmt)>0 THEN (SUM(m.AdjustAmt)) ELSE 0 END AdjustHS, " +
                     "             (SUM(m.AdjustAmt)) As AdjustAmt, " +
                     "           /** Pemakaian Produksi */ " +
                     "             (SUM(m.ProdQty)) As ProdQty, " +
                     "             CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, " +
                     "             (SUM(m.ProdAmt)) As ProdAmt, " +
                     "           /** Adjut minus */ " +
                     "             (SUM(m.AdjProdQty)) As AdjProdQty, " +
                     "             CASE WHEN  SUM(m.AdjProdQty) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty)) WHEN  SUM(m.AdjProdQty) = 0 and SUM(m.AdjProdAmt)>0 THEN (SUM(m.AdjProdAmt)) ELSE 0 END AdjProdHS, " +
                     "             (SUM(m.AdjProdAmt)) As AdjProdAmt, " +
                     "           /** Return */ " +
                     "             (SUM(m.ReturnQty)) As ReturnQty, " +
                     "             CASE WHEN SUM(m.returnAmt) > 0 THEN (SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, " +
                     "             (SUM(m.returnAmt)) As returnAmt, " +
                     "            /** Return Supplier */ " +
                     "            (SUM(m.RetSupQty)) As RetSupQty, " +
                     "            CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, " +
                     "            (SUM(m.RetSupAmt)) As RetSupAmt, " +
                     "           /** Saldo Akhir */ " +
                     "             (SELECT TOP 1 SaldoAwalQty FROM zd_" + created + "_mutasireport as b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                     "             (SELECT TOP 1 HS FROM zd_" + created + "_mutasireport as b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +
                     "             (SELECT TOP 1 TotalAmt FROM zd_" + created + "_mutasireport as b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As TotalAmt " +
                     "       INTO zd_" + created + "_mutasisaldo " +
                     "       FROM zd_" + created + "_mutasireport as m " +
                     "       GROUP BY m.ItemID ";
            if (int.Parse(periodeBlnThn) > int.Parse(LockPeriode))
            {
                strSQL +=(this.FromProsesPosting==string.Empty || this.FromProsesPosting==null)?"":
                 "/*Update table transaksi */ " +
                  "UPDATE p  " +
                  "SET p.AvgPrice=a.AvgPrice " +
                  "FROM PakaiDetail as p " +
                  "INNER JOIN UpdateAvgPrice as a " +
                  "ON P.PakaiID=a.ID " +
                  "WHERE a.Tabel='PakaiDetail' and p.ItemID=a.itemID " +
                  "/** penerimaan*/ " +
                  "UPDATE p  " +
                  "SET p.AvgPrice=a.AvgPrice " +
                  "FROM ReceiptDetail as p " +
                  "INNER JOIN UpdateAvgPrice as a " +
                  "ON P.ReceiptID=a.ID " +
                  "WHERE a.Tabel='ReceiptDetail' and p.ItemID=a.itemID " +
                  "/**penyesuaian produksi */ " +
                  "UPDATE p  " +
                  "SET p.AvgPrice=a.AvgPrice " +
                  "FROM ReturPakaiDetail as p " +
                  "INNER JOIN UpdateAvgPrice as a " +
                  "ON P.ReturID=a.ID " +
                  "WHERE a.Tabel='ReturPakaiDetail' and p.ItemID=a.itemID " +
                  "/** adjust Tambah */ " +
                  "UPDATE p  " +
                  "SET p.AvgPrice=a.AvgPrice " +
                  "FROM AdjustDetail as p " +
                  "INNER JOIN UpdateAvgPrice as a " +
                  "ON P.AdjustID=a.ID " +
                  "WHERE a.Tabel='AdjustDetailT' and p.ItemID=a.itemID  " +
                  "/** Adjust Kurang */ " +
                  "UPDATE p " +
                  "SET p.AvgPrice=a.AvgPrice " +
                  "FROM AdjustDetail as p " +
                  "INNER JOIN UpdateAvgPrice as a " +
                  "ON P.AdjustID=a.ID " +
                  "WHERE a.Tabel='AdjustDetailK' and p.ItemID=a.itemID ";
            }
            strSQL += " /** generate final report */ " +
                     "   SELECT XZ.*, ROW_NUMBER() OVER(ORDER BY Tanggal) AS ID FROM ( " +
                     "       (SELECT md.ID,md.Tanggal,md.DocNo,md.BeliQty,md.BeliHS,md.BeliAmt,md.AdjustQty,md.AdjustHS,md.AdjustAmt,md.ProdQty,md.ProdHS,md.ProdAmt, " +
                     "               md.RetSupQty,md.RetSupHS,md.RetSupAmt, " +
                     "               md.AdjProdQty,md.AdjProdHS,md.AdjProdAmt,md.ReturnQty,md.ReturnHS,md.returnAmt,md.SaldoAwalQty,md.HS As SaldoAwalHS,md.TotalAmt As SaldoAwalAmt " +
                     "               FROM zd_" + created + "_mutasireport as md " +
                     "       )UNION ALL ( " +
                     "       SELECT dm.ID,dm.Tanggal,dm.DocNo,dm.BeliQty,dm.BeliHS,dm.BeliAmt,dm.AdjustQty,dm.AdjustHS,dm.AdjustAmt,dm.ProdQty," +
                     "              dm.ProdHS,dm.ProdAmt,dm.RetSupQty,dm.RetSupHS,dm.RetSupAmt,dm.AdjProdQty,dm.AdjProdHS,dm.AdjProdAmt," +
                     "              dm.ReturnQty,dm.ReturnHS,dm.returnAmt,dm.SaldoAwalQty,dm.HS As SaldoAwalHS,dm.TotalAmt As SaldoAwalQty " +
                     "              FROM zd_" + created + "_mutasisaldo as dm " +
                     "       ) " +
                     "   ) AS XZ " +
                     "   ORDER BY XZ.ID " +
                     " /** Delete temporary table */ " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmp] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmpx] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport1] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasisaldo] ";

            #endregion
            return strSQL;
        }
        public string GetHargaReceipt(string Dari, string Sampai, string GroupPurch, string Tahun)
        {
            /**
             * Harga Kertas di rubah pengambilan datanya
             * dari TableHargaBankOut ke Field Price2 di table PurchnDetail
             * Yang isi Price2 di table PurchnDetail adalah accounting
             * Dirubah per 13-05-2016
             **/
            string ItemTypeIDs = "";
            switch (int.Parse(GroupPurch))
            {
                case 4: ItemTypeIDs = "2"; break;
                case 5: ItemTypeIDs = "3"; break;
                default: ItemTypeIDs = "1"; break;
            }
            string periodeTahun = Dari.Substring(0, 6).ToString();
            string isRekap = (Tahun == "A") ? "(" + GetItemID(periodeTahun, GroupPurch) + " )" : "('" + Tahun + "')";
            string strSQL = (int.Parse(GroupPurch) == 5) ? this.GetHargaBiaya(Dari, Sampai) :
                         " /* Check Curency dan harga Kertas */ " +
                         "SELECT Tipe,CONVERT(CHAR,Tanggal,105) AS Tanggal,ReceiptNo,ItemID, " +
                         "CASE WHEN x.Price >0 THEN  " +
                         "   CASE WHEN  x.crc >1 then  " +
                         "       CASE WHEN x.Flag =2 Then  " +
                                /** untuk data po mulai dari 092016 kur di ambil dari kolom nilai kurs di table POPurchn */
                         "        CASE WHEN LEFT(CONVERT(CHAR,x.POPurchnDate,112),6)>='201609' THEN " +
                         "          ISNULL((x.NilaiKurs * x.Price),0) ELSE " +
                         "       ((select top 1 isnull(kurs,1)kurs from matauangkurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/ )* x.Price) " +
                         "       END ELSE  " +
                         "           CASE WHEN x.NilaiKurs >1 THEN (x.NilaiKurs * x.Price) ELSE " +
                         "           ((select top 1 isnull(kurs,1)kurs from matauangkurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/)* x.Price) " +
                         "           END    " +
                         "       END   " +
                         "   ELSE x.Price   " +
                         " END  " +
                         "   ELSE " +
                         "       CASE WHEN x.Crc <=1 THEN " +
                         "       x.HargaSatuan	 " +
                         "       END  " +
                         "   END Price,Quantity,  " +
                         "   /*Average price */ " +
                         "   CASE  WHEN x.Price > 0 THEN   " +
                         "       CASE  WHEN (x.Crc >1 ) THEN  " +
                         "           CASE WHEN x.flag=2 THEN  " +
                         "               CASE WHEN LEFT(CONVERT(CHAR,x.POPurchnDate,112),6)>='201609' THEN " +
                         "                ISNULL((x.NilaiKurs * x.Price),0) ELSE " +
                         "                ((select top 1 isnull(kurs,1)kurs from MataUangKurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/)*(isnull((x.Price),0))) " +
                         "            END ELSE " +
                         "               CASE WHEN x.NilaiKurs>1 THEN (x.NilaiKurs * x.Price) ELSE " +
                         "               ((select top 1 isnull(kurs,1)kurs from MataUangKurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/)*(isnull((x.Price),0))) " +
                         "               END " +
                         "           END " +
                         "       ELSE isnull((x.Price),0)  " +
                         "       END  " +
                         "   ELSE " +
                         "       CASE WHEN(x.Crc <=1) THEN " +
                         "       x.HargaSatuan END  " +
                         "   END AvgPrice,   " +
                         "   /*Total Price*/ " +
                         "   CASE  WHEN(x.Crc >1 ) THEN " +
                         "      CASE WHEN x.flag=2 THEN " +
                                /** untuk data po mulai dari 092016 kur di ambil dari kolom nilai kurs di table POPurchn */
                         "        CASE WHEN LEFT(CONVERT(CHAR,x.POPurchnDate,112),6)>='201609' AND x.NilaiKurs >1 THEN " +
                         "          ISNULL((x.NilaiKurs * x.Price*x.Quantity),0) ELSE " +
                         "       ((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* (isnull((x.Price*x.Quantity),0)))" +
                         "         END  "+
                         "        ELSE " +
                         "          CASE WHEN x.NilaiKurs>1 THEN (x.NilaiKurs * x.Price*x.Quantity) ELSE " +
                         "          ((select top 1 isnull(kurs,1)kurs from MataUangKurs where  muID=x.crc and drTgl = x.Tanggal)*(isnull((x.Price*x.Quantity),0)))" +
                         "          END " +
                         "      END " +
                         "   ELSE " +
                         "       CASE WHEN x.Price>0 THEN isnull((x.Price*x.Quantity),0)  " +
                         "       ELSE (x.HargaSatuan * x.Quantity)  " +
                         "       END " +
                         "   END TotalPrice FROM(   " +
                         "   SELECT '1' as Tipe, p.ReceiptDate as Tanggal,p.ReceiptNo ,rd.ItemID, Case When pod.Price=0 then pod.Price2 else pod.Price end Price,rd.Quantity,  " +
                         "   pod.crc,pod.flag,pod.NilaiKurs,ISNULL(pod.SubCompanyID,0)SubCompanyID,p.SupplierID,  " +
                         "   (pod.Price*rd.Quantity) as TotalPrice,rd.POID,rd.ID as ReceiptID,pod.Price2 as HargaSatuan,bo.Qty,pod.POPurchnDate " +
                         "   FROM Receipt as p   " +
                         "   LEFT JOIN ReceiptDetail as rd  on rd.ReceiptID=p.ID    " +
                         "   LEFT JOIN vw_PObukanRP as pod on pod.PODetailID=rd.PODetailID " +
                         "   LEFT JOIN TabelHargaBankOut AS bo ON bo.ReceiptDetailID=rd.ID " +
                         "  WHERE  (convert(varchar,p.ReceiptDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "')" +
                         "  AND rd.ItemID IN " +
                          isRekap +
                         "  AND rd.ItemTypeID=" + ItemTypeIDs +
                         "  AND p.Status >-1 AND rd.RowStatus >-1 ) " +
                         "  as x /* sampai di sini check harga nya */";
            return strSQL;
        }
        private string HargaKertas()
        {
            return "(Select top 1 isnull(Harga,0)Harga from HargaKertas where SupplierID= "+
                   "x.SupplierID and ItemID=x.ItemID and Aktif=1 and CompanyID=1 order by ID desc)";
        }
        private string HargaKertasBankOut()
        {
            return "(select ISNULL(HargaSatuan,0)Harga From TableHargaBankOut where ReceiptDetailID=x.ReceiptID)";
        }
        private string HargaKertas2(string SubCoiD)
        {
            return "SELECT TOP 1 ISNULL(Harga," + HargaKertas() + ")Harga FROM HargaKertas WHERE SubCompanyID= " + SubCoiD + " order by ID desc";
        }
        public string PostingAvgPrice(string Dari, string Sampai, string GroupPurch, string Tahun)
        {
            /**
             * Last Update  :11-11-2013
             * Change log
             * 1. Pengambilan nilai avgprice di table ReceiptDetail dari field avgprice sebelumnya dari Price
             * Jika avrg price blm muncul kemungkinan ada masalah approval di pemakaian
             */
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            string UserView = "avg_" + ((Users)System.Web.HttpContext.Current.Session["Users"]).ID.ToString();
            string ViewUsers = "avg_" + ((Users)System.Web.HttpContext.Current.Session["Users"]).ID.ToString();
            //#region Query nya

        #region variable data
            string LockPeriode = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LockPeriod", "COGS");
            string GrpID = GroupPurch;
            string strSQL = string.Empty;
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
            string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
            string periodeBlnThn = Tahun + Dari.Substring(4, 2).ToString();
            #region prepared data
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
            #endregion
        #endregion
            #region Query String
            strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasitmp] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasitmpx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasitmpxx] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_mutasireport] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_mutasisaldo] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapsaldoawal] " +
                    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_mutasireport1] " +
                    "/** Kumpulkan data dari beberapa tabel dengan UNION ALL all */ " +
                    "SELECT * INTO zp_" + created + "_lapmutASitmp FROM( " +
                    "     (SELECT '0' AS Tipe,'" + frm + "' AS Tanggal,'Saldo Awal' AS DocNo,si.ItemID,si." + SaldoLaluPrice + " AS SaldoHS," +
                    "     si." + SaldoLaluQty + " AS SaldoQty,CASE WHEN si." + SaldoLaluPrice + " >0  THEN si." + SaldoLaluPrice + " ELSE 0 END " +
                    "     AvgPrice,(si." + SaldoLaluQty + "*si." + SaldoLaluPrice + ") AS TotalPrice " +
                    "     FROM SaldoInventory AS si " +
                    "     WHERE si.YearPeriod='" + periodeTahun + "' /*AND ) */" +
                    "     AND si.ItemTypeID='1' and (/*si." + SaldoLaluQty + ">0 or*/ si.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")) " +
                    "  ) UNION ALL ( " +
                          GetHargaReceipt(Dari, Sampai, GrpID, "A") +
                    "   ) UNION ALL ( " +
                    "       SELECT '2' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity," +
                    "       (pk.AvgPrice) AS AvgPrice,(pk.Quantity*pk.AvgPrice) AS TotalPrice FROM Pakai AS k " +
                    "       LEFT JOIN PakaiDetail AS pk " +
                    "       ON pk.PakaiID=k.ID " +
                    "       WHERE  (cONvert(VARCHAR,k.PakaiDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "       AND pk.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + " ) AND k.Status >-1 AND pk.RowStatus >-1" +
                    "   ) UNION ALL  ( " +
                    "        SELECT '3' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity, " +
                    "        (rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp " +
                    "        LEFT JOIN ReturPakaiDetail AS rpd " +
                    "        ON rpd.ReturID=rp.ID " +
                    "        WHERE  (cONvert(VARCHAR,rp.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "        AND rpd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + " ) AND rp.Status >-1 AND rpd.RowStatus >-1 " +
                    "   ) UNION ALL ( " +
                    "       SELECT '4' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Tambah' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice As SaldoHS, " +
                    "       CASE When a.AdjustType='Tambah' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPrice, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPrice " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Tambah'" +
                    "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND a.Status >-1 AND ad.RowStatus >-1" +
                    "    )UNION ALL ( " +
                    "       SELECT '5' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                    "       CASE when a.AdjustType='Kurang' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "       CASE When a.AdjustType='Kurang' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPriceK, " +
                    "       (ad.Quantity*ad.AvgPrice) AS TotalPriceK " +
                    "       FROM Adjust AS a " +
                    "       LEFT JOIN AdjustDetail AS ad " +
                    "       ON ad.AdjustID=a.ID " +
                    "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Kurang' " +
                    "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND a.Status >-1 AND ad.RowStatus >-1" +
                    "   )UNION ALL ( " +
                    "   SELECT '6' AS Tipe, " +
                    "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                    "    CAST('0' AS Decimal(18,6)) AS AvgPrice ,CAST('0' AS Decimal(18,6)) AS Totalprice " +
                    "    FROM ReturSupplier AS rs " +
                    "    LEFT JOIN ReturSupplierDetail AS rsd " +
                    "    ON rsd.ReturID=rs.ID " +
                    "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                    "    AND rsd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND rs.Status >-1  " +
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
                    "   INTO zp_" + created + "_lapmutasitmpx " +
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
                    "  FROM zp_" + created + "_lapmutasitmp as x) AS Z ORDER BY z.Tanggal " +
                    "   /** susun data berdasarkan item id dan bentuk id baru */ " +
                    "   SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY itemID,ID,DocNo) as IDn,* INTO zp_" + created + "_lapmutasitmpxx " +
                    "   FROM zp_" + created + "_lapmutasitmpx " +
                    "   /**Susun  data tabular */ " +
                    "    SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo, " +
                    "    BeliQty,BeliHS,(BeliQty*BeliHS) As BeliAmt,AdjustQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM zp_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS, " +
                    "    ProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM zp_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, " +
                    "    AdjProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM zp_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, " +
                    "    A.ReturnQty, " +
                    "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM zp_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ReturnHS, " +
                    "    A.RetSupQty, " +
                    "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM zp_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM zp_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty, " +
                    "    CASE WHEN A.ID>1 THEN  " +
                    "       CASE WHEN (SELECT SUM(totalqty)FROM zp_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN " +
                    "        ((SELECT SUM(totalamt) FROM zp_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )/ " +
                    "        (ABS((SELECT SUM(totalqty)FROM zp_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM zp_" + created + "_lapMutasitmpxx WHERE ID <=A.ID  AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt " +
                    "    INTO zp_" + created + "_lapmutasireport " +
                    "    FROM zp_" + created + "_lapMutasitmpxx as A " +
                    "    ORDER by itemID,A.Tanggal,A.IDn,a.Tipe,a.DocNo  " +

                    "   /** Generate Detail Report without saldo akhir */ " +
                    "  SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                    "           (l.BeliQty*l.BeliHS) as BeliAmt,l.AdjustQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, " +
                    "           l.ProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                    "           l.AdjProdQty, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS, " +
                    "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt, " +
                    "           l.ReturnQty, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ReturnHS, " +
                    "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                    "           l.RetSupQty, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                    "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT HS FROM zp_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                    "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                    "    INTO zp_" + created + "_mutasireport " +
                    "  FROM zp_" + created + "_lapmutasireport AS L " +
                    " /* WHERE l.ItemID IN (" + GetItemID(periodeBlnThn, GrpID) + ")*/ ORDER BY L.itemID,L.Tipe,L.Tanggal ";
           
                strSQL += "  /** update colom amt dan colom hs */ " +
                    "  select row_number() over(order by itemID) as IDn,itemid into zp_" + created + "_mutasireport1 " +
                    "  from zp_" + created + "_mutasireport group by itemID order by itemid " +
                    "   declare @i int " +
                    "   declare @b int " +
                    "   declare @hs decimal(18,6) " +
                    "   declare @amt decimal(18,6) " +
                    "   declare @avgp decimal(18,6) " +
                    "   declare @c int " +
                    "   declare @itm int " +
                    "   declare @itmID int " +
                    "   set @c=1 " +
                    "   set @itm=(select COUNT(IDn) from zp_" + created + "_mutasireport1) " +
                    "   While @c <=@itm " +
                    "   Begin " +
                    "   set @itmID=(select itemID from zp_" + created + "_mutasireport1 where IDn=@c) " +
                    "       set @b=1 " +
                    "       set @avgp=(select top 1 " + SaldoLaluPrice + " from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                    "       set @i=(select COUNT(id) from zp_" + created + "_mutasireport where itemid=@itmID) " +
                    "       While @b<=@i  " +
                    "       Begin " +
                    "       set @hs=CASE WHEN @b >1 THEN (select hs from zp_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                    "               ELSE CASE WHEN(SELECT hs from zp_" + created + "_mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                    "		         (SELECT hs from zp_" + created + "_mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                    "		         END  " +
                    "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from zp_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                    "                ELSE (SELECT TotalAmt from zp_" + created + "_mutasireport where ID=1 and itemid=@itmID)  " +
                    "                END  " +
                    "       /** update semua hs */ " +
                    "       update zp_" + created + "_mutasireport  " +
                    "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM zp_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                    "           ProdHS		=CASE WHEN (SELECT ProdQty FROM zp_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                    "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM zp_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                    "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM zp_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                    "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM zp_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
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
                    "   END "+
                    "  /** Generate Saldo Awal */ " +
                    "  SELECT ItemID,SaldoAwalQty,HS,TotalAmt " +
                    "  INTO zp_" + created + "_lapsaldoawal " +
                    "  FROM zp_" + created + "_mutasireport as m " +
                    "  WHERE m.DocNo='Saldo Awal' /*AND m.ItemID in("+ GetItemID(periodeBlnThn,GrpID)+ ")*/ " +

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
                     "  (SELECT TOP 1 SaldoAwalQty FROM zp_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                     "  (SELECT TOP 1 HS FROM zp_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +

                     "  CASE  when (SELECT  TOP  1 SaldoAwalQty FROM  zp_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID  DESC)>0 then  " +
                     "  (SELECT  TOP  1 TotalAmt FROM  zp_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID DESC) " +
                     "  ELSE  0 END  As  TotalAmt " +
                     "  INTO zp_" + created + "_mutasisaldo  " +
                     "  FROM zp_" + created + "_mutasireport AS m  " +
                     " /* WHERE m.ItemID in(" + GetItemID(periodeBlnThn, GrpID) + ") */" +
                     "  GROUP BY m.ItemID  " +
                //"  /** generate report final */ " +

                    //"    SELECT mds.ID,(SELECT (ItemCode +' '+ ItemName) as ItemName From Inventory WHERE ID=mds.ItemID) as ItemID,sa.SaldoAwalQty,sa.HS as SaldoAwalHS,sa.TotalAmt as SaldoAwalAmt,mds.BeliQty,mds.BeliHS,mds.BeliAmt,mds.AdjustQty, " +
                //"    mds.AdjustHS,mds.AdjustAmt,mds.ProdQty,mds.ProdHS,mds.ProdAmt, " +
                //"    mds.AdjProdQty,mds.AdjProdHS,mds.AdjProdAmt,mds.ReturnQty,mds.ReturnHS,mds.returnAmt, " +
                //"    mds.RetSupQty,mds.RetSupHS,mds.RetSupAmt, " +
                //"    mds.SaldoAwalQty as SaldoAkhirQty,mds.HS As SaldoAkhirHS,mds.TotalAmt As SaldoAkhirAmt " +
                //"    FROM mutASisaldo AS mds " +
                //"    INNER JOIN lapsaldoawal as sa " +
                //"    ON mds.ItemID=sa.ItemID " +
                //"    WHERE sa.SaldoAwalQty > 0 OR mds.BeliQty > 0 OR mds.AdjustQty >0 OR mds.ProdQty >0 OR mds.AdjProdQty >0 OR mds.ReturnQty >0 " +
                //"    Order By itemid " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_UpdateAvgPrice]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_UpdateAvgPrice] " +

                    "/** Kumpulkan data update average price*/ " +
                    "SELECT " +
                    "    CASE WHEN m.ProdQty >0 THEN (SELECT TOP 1 ID FROM Pakai WHERE Pakai.PakaiNo=m.DocNo) " +
                    "         WHEN m.AdjustQty >0 THEN (SELECT TOP 1  ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Tambah') " +
                    "         WHEN m.AdjProdQty >0 THEN (SELECT TOP 1  ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Kurang') " +
                    "         WHEN m.ReturnQty >0 THEN (SELECT TOP 1  ID FROM ReturPakai WHERE ReturPakai.ReturNo=m.DocNo) " +
                    "         WHEN m.RetSupQty >0 THEN (SELECT TOP 1  ID FROM ReturSupplier WHERE ReturSupplier.ReturNo=m.DocNo) " +
                    "         WHEN m.BeliQty > 0 THEN (SELECT TOP 1  ID FROM Receipt WHERE Receipt.ReceiptNo=m.DocNo) " +
                    "    END ID, " +
                    "    CASE WHEN m.ProdQty >0 THEN m.ItemID " +
                    "         WHEN m.AdjustQty >0 THEN m.ItemID  " +
                    "         WHEN m.AdjProdQty >0 THEN m.ItemID " +
                    "         WHEN m.ReturnQty >0 THEN m.ItemID  " +
                    "         WHEN m.RetSupQty >0 THEN m.ItemID  " +
                    "         WHEN m.BeliQty >0 THEN m.ItemID  " +
                    "    END itemID, " +
                    "    CASE WHEN m.ProdQty >0 THEN m.ProdHS  " +
                    "         WHEN m.AdjustQty >0 THEN m.AdjustHS " +
                    "         WHEN m.AdjProdQty >0 THEN m.AdjProdHS " +
                    "         WHEN m.ReturnQty >0 THEN m.ReturnHS " +
                    "         WHEN m.RetSupQty >0 THEN m.RetSupHS " +
                    "         WHEN m.BeliQty >0 THEN m.BeliHS " +
                    "    END AvgPrice, " +
                    "    CASE  " +
                    "        WHEN m.ProdQty >0 THEN 'PakaiDetail'  " +
                    "        WHEN m.AdjustQty>0 THEN 'AdjustDetailT' " +
                    "        WHEN m.AdjProdQty>0 THEN 'AdjustDetailK' " +
                    "        WHEN m.ReturnQty >0 THEN 'ReturPakaiDetail' " +
                    "        WHEN m.RetSupQty>0 THEN 'ReturSupplierDetail' " +
                    "        WHEN m.BeliQty>0 THEN 'ReceiptDetail' " +
                    "    END Tabel " +
                    "INTO zp_" + created + "_UpdateAvgPrice " +
                    "FROM zp_" + created + "_mutasireport as m " +
                    "/** update avgprice setiap tabel */  " +
                    "/** Produksi */ " ;
                if (int.Parse(periodeBlnThn) > int.Parse(LockPeriode))
                {
                    strSQL += "UPDATE p  " +
                      "SET p.AvgPrice=a.AvgPrice " +
                      "FROM PakaiDetail as p " +
                      "INNER JOIN zp_" + created + "_UpdateAvgPrice as a " +
                      "ON P.PakaiID=a.ID " +
                      "WHERE a.Tabel='PakaiDetail' and p.ItemID=a.itemID " +
                      "/** penerimaan*/ " +
                      "UPDATE p  " +
                      "SET p.AvgPrice=a.AvgPrice " +
                      "FROM ReceiptDetail as p " +
                      "INNER JOIN zp_" + created + "_UpdateAvgPrice as a " +
                      "ON P.ReceiptID=a.ID " +
                      "WHERE a.Tabel='ReceiptDetail' and p.ItemID=a.itemID " +
                      "/**penyesuaian produksi */ " +
                      "UPDATE p  " +
                      "SET p.AvgPrice=a.AvgPrice " +
                      "FROM ReturPakaiDetail as p " +
                      "INNER JOIN zp_" + created + "_UpdateAvgPrice as a " +
                      "ON P.ReturID=a.ID " +
                      "WHERE a.Tabel='ReturPakaiDetail' and p.ItemID=a.itemID " +
                      "/** adjust Tambah */ " +
                      "UPDATE p  " +
                      "SET p.AvgPrice=a.AvgPrice " +
                      "FROM AdjustDetail as p " +
                      "INNER JOIN zp_" + created + "_UpdateAvgPrice as a " +
                      "ON P.AdjustID=a.ID " +
                      "WHERE a.Tabel='AdjustDetailT' and p.ItemID=a.itemID  " +
                      "/** Adjust Kurang */ " +
                      "UPDATE p " +
                      "SET p.AvgPrice=a.AvgPrice " +
                      "FROM AdjustDetail as p " +
                      "INNER JOIN zp_" + created + "_UpdateAvgPrice as a " +
                      "ON P.AdjustID=a.ID " +
                      "WHERE a.Tabel='AdjustDetailK' and p.ItemID=a.itemID " +
                      "declare @sqlS nvarchar(max)  " +
                      "set @sqlS ='update A set A.'+@CurAvgPrice+'=B.HS   from SaldoInventory  A inner join Auto_mutasisaldo_"+UserView+" B  on A.ItemID=B.itemid where A.YearPeriod='+@thnCur " +
                      "exec sp_executesql @sqlS, N'' ";
                }

                strSQL += " select * from zp_" + created + "_mutasisaldo order by itemid " +
                    "/** return supplier  no avgprice*/" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasitmp] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasitmpx] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasitmpxx] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapmutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_mutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_mutasisaldo] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zp_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[zp_" + created + "_lapsaldoawal] /**/ ";
            #endregion
                #region NewQueryString
                strSQL = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmp_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmp_" + ViewUsers + "] " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasireport_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasisaldo_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasisaldo_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport1_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport1_" + ViewUsers + "]  " +
                        "/** Kumpulkan data dari beberapa tabel dengan UNION ALL all */ " +
                        "declare @thbln char(6)  " +
                        "declare @GroupID varchar(max) " +
                        "declare @itemtypeID char(6) " +
                           " set @thbln='" + periodeBlnThn + "'  " +
                           " set @GroupID ='" + GroupPurch + "' " +
                           " set @itemtypeID =(select itemtypeid from GroupsPurchn where ID=" + GroupPurch  + ") " +

                        "declare @thnbln0 varchar(6)  " +
                        "declare @tgl datetime  " +
                        "set @tgl=CAST( (@thbln+ '01') as datetime)  " +
                        "set @tgl= DATEADD(month,-1,@tgl)  " +
                        "set @thnbln0=LEFT(convert(char,@tgl,112),6)  " +
                        "declare @thnAwal  varchar(4) " +
                        "declare @blnAwal varchar(2) " +
                        "declare @AwalQty varchar(7) " +
                        "declare @AwalAvgPrice varchar(11) " +
                        "declare @tglawal varchar(10) " +
                        "set @tglawal='01-' + right(@thbln,2)+'-'+ LEFT(@thbln,4)  " +
                        "set @thnAwal =left(@thnbln0,4) " +
                        "set @blnAwal=RIGHT(@thnbln0,2) " +
                        "if right(@blnAwal,2)='01' begin set @AwalQty='janqty' set @AwalAvgPrice='janAvgprice'  end " +
                        "if right(@blnAwal,2)='02' begin set @AwalQty='febqty' set @AwalAvgPrice='febAvgprice'  end " +
                        "if right(@blnAwal,2)='03' begin set @AwalQty='marqty' set @AwalAvgPrice='marAvgprice'  end " +
                        "if right(@blnAwal,2)='04' begin set @AwalQty='aprqty' set @AwalAvgPrice='aprAvgprice'  end " +
                        "if right(@blnAwal,2)='05' begin set @AwalQty='meiqty' set @AwalAvgPrice='meiAvgprice'  end " +
                        "if right(@blnAwal,2)='06' begin set @AwalQty='junqty' set @AwalAvgPrice='junAvgprice'  end " +
                        "if right(@blnAwal,2)='07' begin set @AwalQty='julqty' set @AwalAvgPrice='julAvgprice'  end " +
                        "if right(@blnAwal,2)='08' begin set @AwalQty='aguqty' set @AwalAvgPrice='aguAvgprice'  end " +
                        "if right(@blnAwal,2)='09' begin set @AwalQty='sepqty' set @AwalAvgPrice='sepAvgprice'  end " +
                        "if right(@blnAwal,2)='10' begin set @AwalQty='oktqty' set @AwalAvgPrice='oktAvgprice'  end " +
                        "if right(@blnAwal,2)='11' begin set @AwalQty='novqty' set @AwalAvgPrice='novAvgprice'  end " +
                        "if right(@blnAwal,2)='12' begin set @AwalQty='desqty' set @AwalAvgPrice='desAvgprice'  end " +

                        "declare @thnCur  varchar(4) " +
                        "declare @blnCur varchar(2) " +
                        "declare @CurQty varchar(7) " +
                        "declare @CurAvgPrice varchar(11) " +
                        "set  @thnCur =left(@thbln,4) " +
                        "set @blnCur=RIGHT(@thbln,2) " +
                        "if right(@blnCur,2)='01' begin set @CurQty='janqty' set @CurAvgPrice='janAvgprice'  end " +
                        "if right(@blnCur,2)='02' begin set @CurQty='febqty' set @CurAvgPrice='febAvgprice'  end " +
                        "if right(@blnCur,2)='03' begin set @CurQty='marqty' set @CurAvgPrice='marAvgprice'  end " +
                        "if right(@blnCur,2)='04' begin set @CurQty='aprqty' set @CurAvgPrice='aprAvgprice'  end " +
                        "if right(@blnCur,2)='05' begin set @CurQty='meiqty' set @CurAvgPrice='meiAvgprice'  end " +
                        "if right(@blnCur,2)='06' begin set @CurQty='junqty' set @CurAvgPrice='junAvgprice'  end " +
                        "if right(@blnCur,2)='07' begin set @CurQty='julqty' set @CurAvgPrice='julAvgprice'  end " +
                        "if right(@blnCur,2)='08' begin set @CurQty='aguqty' set @CurAvgPrice='aguAvgprice'  end " +
                        "if right(@blnCur,2)='09' begin set @CurQty='sepqty' set @CurAvgPrice='sepAvgprice'  end " +
                        "if right(@blnCur,2)='10' begin set @CurQty='oktqty' set @CurAvgPrice='oktAvgprice'  end " +
                        "if right(@blnCur,2)='11' begin set @CurQty='novqty' set @CurAvgPrice='novAvgprice'  end " +
                        "if right(@blnCur,2)='12' begin set @CurQty='desqty' set @CurAvgPrice='desAvgprice'  end " +

                        "declare @sqlP nvarchar(max) " +
                        "set @sqlP='SELECT * into Auto1_lapmutASitmp_" + ViewUsers + " FROM(  " +
                        "(SELECT ''0'' AS Tipe,'''+@tglawal+''' AS Tanggal,''Saldo Awal'' AS DocNo,si.ItemID,si.'+@AwalAvgPrice+' AS SaldoHS,  " +
                        "si.'+@AwalQty+' AS SaldoQty,CASE WHEN ISNULL(si.'+@AwalAvgPrice+',0) >0 THEN ISNULL(si.'+@AwalAvgPrice+',0) ELSE ISNULL(si.'+@AwalAvgPrice+',0) END AvgPrice,(si.'+@AwalQty+'*ISNULL(si.'+@AwalAvgPrice+',0)) AS TotalPrice  " +
                        "FROM SaldoInventory AS si WHERE si.YearPeriod='+ @thnAwal +'  AND si.ItemTypeID='''+ @itemtypeID +'''  and GroupID in ('+ @groupID +') AND (si.'+@AwalQty+'!=0 OR si.ItemID in(SELECT ID FROM Inventory Where Aktif=1 AND GroupID in('+@groupID+'))))  " +
                        "UNION ALL  " +
                        "(SELECT ''1'' AS Tipe,CONVERT(CHAR,Tanggal,105) AS Tanggal,ReceiptNo,ItemID, " +
                        "CASE WHEN x.Price >0 THEN " + //jika price=0
                        " CASE WHEN x.crc >1 then CASE WHEN x.Flag =2 Then  " + //jika kurs bukan rp dan supp harus bayar dolar ambil dari Matauangkurs
                        "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " + //di tambahkan untuk transaksi stlah bln ags 2016
                        "       (x.NilaiKurs * x.Price) ELSE " + //kurs diambil dari nilai kurs
                        "   ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal )* x.Price) END ELSE " +
                        "   CASE WHEN x.NilaiKurs >0  " + //jika nilaikurs di table popurchn >0 kalikan dengan nilai kurs
                        "       THEN (x.NilaiKurs * x.Price) ELSE " + // jika nilai kurs =0 ambil dari table mataunga kurs base on tgl receipt
                        "       ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal)* x.Price)  " +
                        "       END END ELSE x.Price END " +
                        "ELSE CASE WHEN x.Crc <=1 THEN x.HargaSatuan END END Price,Quantity,  " +
                        "CASE WHEN x.Price > 0 THEN " +
                        "CASE WHEN (x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN " +
                        "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                        "       (x.NilaiKurs * x.Price) ELSE ( " +
                        "           (select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)*(isnull((x.Price),0))) " +
                        "       END ELSE CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price) ELSE  " +
                        "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                        "(isnull((x.Price),0))) END END ELSE isnull((x.Price),0) END ELSE CASE WHEN(x.Crc <=1) THEN  " +
                        "x.HargaSatuan END END AvgPrice," +
                        "CASE WHEN(x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN  " +
                        "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                        "       (x.NilaiKurs * x.Price * x.Quantity) ELSE " +
                        "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                        "(isnull((x.Price*x.Quantity),0))) END ELSE  " +
                        "CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price*x.Quantity) ELSE  " +
                        "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl = x.Tanggal)*(isnull((x.Price*x.Quantity),0)))  " +
                        "END END ELSE CASE WHEN x.Price>0 THEN isnull((x.Price*x.Quantity),0) ELSE (x.HargaSatuan * x.Quantity)  " +
                        "END END TotalPrice " +
                        "FROM( SELECT ''1'' as Tipe, p.ReceiptDate as Tanggal,p.ReceiptNo ,rd.ItemID,  " +
                        "Case When pod.Price=0 then pod.Price2 else pod.Price end Price,rd.Quantity,  " +
                        "pod.crc,pod.flag,pod.NilaiKurs,ISNULL(pod.SubCompanyID,0)SubCompanyID,p.SupplierID,  " +
                        "(pod.Price*rd.Quantity) as TotalPrice,rd.POID,rd.ID as ReceiptID,pod.Price2 as HargaSatuan,bo.Qty  " +
                        "FROM Receipt as p LEFT JOIN ReceiptDetail as rd on rd.ReceiptID=p.ID LEFT JOIN vw_PObukanRP as pod on rd.PODetailID=pod.PODetailID  " +
                        "LEFT JOIN TabelHargaBankOut AS bo ON bo.ReceiptDetailID=rd.ID WHERE (left(convert(varchar,p.ReceiptDate,112),6)='+ @thbln +')  " +
                        "AND rd.ItemID IN (select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') )  " +
                        "AND pod.ItemTypeID='''+ @itemtypeID +''' AND p.Status >-1 AND rd.RowStatus >-1 ) as x )  " +

                        "UNION ALL  " +
                        "(SELECT ''2'' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity,  " +
                        "(pk.AvgPrice) AS AvgPrice,(pk.Quantity*pk.AvgPrice) AS TotalPrice FROM Pakai AS k LEFT JOIN PakaiDetail AS pk ON pk.PakaiID=k.ID  " +
                        "WHERE (left(cONvert(VARCHAR,k.PakaiDate,112),6)='+ @thbln +') AND pk.ItemID IN( " +
                        "select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') )AND k.Status >-1 AND pk.RowStatus >-1 )  " +
                        "UNION ALL  " +
                        "(SELECT ''3'' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity,  " +
                        "(rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp LEFT JOIN ReturPakaiDetail AS rpd  " +
                        "ON rpd.ReturID=rp.ID WHERE (left(cONvert(VARCHAR,rp.ReturDate,112),6)='+ @thbln +')  " +
                        "AND rpd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') ) AND rp.Status >-1 AND rpd.RowStatus >-1 )  " +
                        "UNION ALL  " +
                        "(SELECT ''4'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal,  " +
                        "CASE when a.AdjustType=''Tambah'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice As SaldoHS,  " +
                        "CASE When a.AdjustType=''Tambah'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPrice, (ad.Quantity*ad.AvgPrice) AS TotalPrice  " +
                        "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                        "AND a.AdjustType=''Tambah'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') )  " +
                        "AND a.Status >-1 AND ad.RowStatus >-1 ) " +
                        "UNION ALL  " +
                        "(SELECT ''5'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105)  " +
                        "AS Tanggal, CASE when a.AdjustType=''Kurang'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice,  " +
                        "CASE When a.AdjustType=''Kurang'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPriceK, (ad.Quantity*ad.AvgPrice) AS TotalPriceK  " +
                        "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                        "AND a.AdjustType=''Kurang'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') )  " +
                        "AND a.Status >-1 AND ad.RowStatus >-1 ) " +
                        "UNION ALL  " +
                        "(SELECT ''6'' AS Tipe,CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity,  " +
                        "CAST(''0'' AS Decimal(18,6)) AS AvgPrice ,CAST(''0'' AS Decimal(18,6)) AS Totalprice FROM ReturSupplier AS rs LEFT JOIN ReturSupplierDetail AS rsd  " +
                        "ON rsd.ReturID=rs.ID where (left(convert(varchar,rs.ReturDate,112),6)='+ @thbln +')  " +
                        "AND rsd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') ) AND rs.Status >-1  " +
                        "AND rsd.RowStatus >-1 ) ) as K' " +
                        "exec sp_executesql @sqlP, N'' " +


                        "/** susun sesuai dengan kolom laporan */  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') AND type in (N'U')) " +
                        "DROP TABLE [dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "] " +
                        "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, BeliQty,BeliHS," +
                        "(BeliQty*BeliHS) as BeliAmt,  " +
                        "AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, " +
                        "ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt,  " +
                        "ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, AdjProdQty,AdjProdHS AS AdjProdHS," +
                        "(AdjProdQty*HPP) as AdjPAmt,RetSupQty,  " +
                        "RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                        "(SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty,  " +
                        "((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                        "(ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                        "INTO Auto1_lapmutasitmpx_" + ViewUsers + " FROM ( " +
                        "SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo,  " +
                        "CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0) ELSE 0 END SaldoAwalQty,  " +
                        "CASE WHEN Tipe='0' THEN ISNULL(SaldoHS,0) ELSE 0 END HPP,  " +
                        "CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, " +
                        "CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0) ELSE 0 END BeliQty,  " +
                        "CASE WHEN Tipe='1' THEN ISNULL(SaldoHS,0) ELSE 0 END BeliHS,  " +
                        "CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPB,  " +
                        "CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0) ELSE 0 END ProdQty,  " +
                        "CASE WHEN Tipe='2' THEN ISNULL(SaldoHS,0) ELSE 0 END ProdHS,  " +
                        "CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAd,  " +
                        "CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0) ELSE 0 END ReturnQty,  " +
                        "CASE WHEN Tipe='3' THEN ISNULL(SaldoHS,0) ELSE 0 END ReturHS,  " +
                        "CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPP,  " +
                        "CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjustQty,  " +
                        "CASE WHEN Tipe='4' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjustHS,  " +
                        "CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPR,  " +
                        "CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjProdQty,  " +
                        "CASE WHEN Tipe='5' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjProdHS,  " +
                        "CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAdjP,  " +
                        "CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0) ELSE 0 END RetSupQty,  " +
                        "CASE WHEN Tipe='6' THEN ISNULL(SaldoHS,0) ELSE 0 END RetSupHS,  " +
                        "CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalRetSup FROM Auto1_lapmutasitmp_" + ViewUsers + " as x  " +
                        "where itemid in (select distinct ID from Inventory where LEN(itemcode)>11 )) AS Z ORDER BY z.Tanggal  " +

                        "/** susun data berdasarkan item id dan bentuk id baru */  " +
                        "/*IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') " +
                        "AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]*/ " +
                        "SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) as IDn,* " +
                        "INTO Auto1_lapmutasitmpxx_" + ViewUsers +
                        " FROM Auto1_lapmutasitmpx_" + ViewUsers +

                        " /**Susun data tabular */  " +
                        "/*--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasireport_" + ViewUsers + "]') " +
                        "AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasireport_" + ViewUsers + "]*/ " +
                        "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo,  " +
                        "BeliQty,BeliHS,(BeliQty*BeliHS) As BeliAmt,AdjustQty, " +
                        "CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                        "   THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                        "   WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS,ProdQty, " +
                        "CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN " +
                        "   (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                        "   FROM Auto1_lapMutasitmpxx_" + ViewUsers + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, AdjProdQty,  " +
                        "CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 " +
                        "   THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                        "   FROM Auto1_lapMutasitmpxx_" + ViewUsers + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, A.ReturnQty, " +
                        "CASE WHEN A.ID>1 AND A.ReturnQty >0  " +
                        "   THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END " +
                        "FROM Auto1_lapMutasitmpxx_" + ViewUsers + " WHERE ID <A.ID AND ItemID=A.ItemID)  " +
                        "ELSE A.HPP END ReturnHS, A.RetSupQty, " +
                        "CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                        "   THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                        "   WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS,  " +
                        "CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                        "   WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty,  " +
                        "CASE WHEN A.ID>1 THEN CASE WHEN (SELECT SUM(totalqty)FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                        "   WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN  " +
                        "   ((SELECT SUM(totalamt) FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                        "   WHERE ID <=A.ID AND ItemID=A.ItemID )/ (ABS((SELECT SUM(totalqty)FROM Auto1_lapMutasitmpxx_" + ViewUsers + "  " +
                        "   WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                        "CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM Auto1_lapMutasitmpxx_" + ViewUsers + "  " +
                        "   WHERE ID <=A.ID AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt INTO Auto1_lapmutasireport_" + ViewUsers +
                        " FROM Auto1_lapMutasitmpxx_" + ViewUsers + " as A  " +
                        "ORDER by itemID,A.Tanggal,A.IDn,a.Tipe,a.DocNo  " +

                        "/** Generate Detail Report without saldo akhir */  " +
                       // "/*--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport_" + ViewUsers + "]') " +
                       // "AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport_" + ViewUsers + "]*/ " +
                        "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS,  " +
                        "(l.BeliQty*l.BeliHS) as BeliAmt,l.AdjustQty, " +
                        "CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1)  " +
                        "   AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                        "CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1)  " +
                        "   AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, l.ProdQty, " +
                        "CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                        "CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, l.AdjProdQty,  " +
                        "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN " +
                        "   (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS,  " +
                        "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN " +
                        "   (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt,  " +
                        "l.ReturnQty, " +
                        "CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0  " +
                        "   END ReturnHS, " +
                        "CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) " +
                        "   AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, l.RetSupQty, " +
                        "CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1)  " +
                        "   AND ItemID=L.ItemID)ELSE 0 END RetSupHS, CASE WHEN L.IDn >1 AND l.RetSupQty >0 " +
                        "   THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND  " +
                        "   ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, l.SaldoAwalQty,l.HS,l.TotalAmt " +
                        "INTO Auto1_mutasireport_" + ViewUsers + " FROM Auto1_lapmutasireport_" + ViewUsers + " AS L  " +
                        "ORDER BY L.itemID,L.Tipe,L.Tanggal  " +

                        "select row_number() over(order by itemID) as IDn,itemid into Auto1_mutasireport1_" + ViewUsers + " from Auto1_mutasireport_" + ViewUsers + " group by itemID order by itemid  " +
                        "declare @sqlSet nvarchar(max) " +
                        "set @sqlSet =' " +
                        "declare @i int " +
                        "declare @b int " +
                        "declare @hs decimal(18,6) " +
                        "declare @amt decimal(18,6) " +
                        "declare @avgp decimal(18,6)  " +
                        "declare @c int " +
                        "declare @itm int " +
                        "declare @itmID int " +
                        "set @b=0 set @c=0 " +
                        "set @itm=(select COUNT(IDn) from Auto1_mutasireport1_" + ViewUsers + ")  " +
                        "While @c <=@itm " +
                        "   Begin set @itmID=(select isnull(itemID,0) from Auto1_mutasireport1_" + ViewUsers + " where IDn=@c)  " +
                        "   set @avgp=(select top 1 '+ @AwalAvgPrice +' from SaldoInventory where ItemID = @itmID and YearPeriod='+ @thnAwal +') " +
                        "   IF ISNULL(@avgp,0)=0 OR @avgp=0" +
                                "begin " +
                                "set @avgp=(select top 1 HS from Auto1_mutasireport_" + UserView + " where itemid=@itmID and HS>0 ) " +
                                "end " + 
                        "   set @i=(select COUNT(id) from Auto1_mutasireport_" + ViewUsers + " where itemid=@itmID) " +
                        "   set  @b=0 While @b<=@i  " +
                        "Begin set @hs=CASE WHEN @b >1 THEN (select hs from Auto1_mutasireport_" + ViewUsers + " where ID=(@b) and itemid=@itmID)  " +
                        "ELSE CASE WHEN(SELECT hs from Auto1_mutasireport_" + ViewUsers + " where ID=1 and itemid=@itmID)>0 THEN 	  " +
                        "(SELECT hs from Auto1_mutasireport_" + ViewUsers + " where ID=1 and itemid=@itmID)ELSE @avgp END 	 END  " +
                        "set @amt=CASE WHEN @b >1 THEN (select TotalAmt from Auto1_mutasireport_" + ViewUsers + " where ID=(@b) and itemid=@itmID)  " +
                        "ELSE (SELECT TotalAmt from Auto1_mutasireport_" + ViewUsers + " where ID=1 and itemid=@itmID) END  " +

                        "/** update semua hs */  " +
                        "update Auto1_mutasireport_" + ViewUsers + " " +
                        "set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0  " +
                        "THEN @hs ELSE 0 END, " +
                        "ProdHS	=CASE WHEN (SELECT ProdQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                        "ReturnHS=CASE WHEN (SELECT ReturnQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                        "AdjProdHS=CASE WHEN (SELECT AdjProdQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                        "RetSupHS=CASE WHEN (SELECT RetSupQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                        "ProdAmt=(ProdQty*@hs), AdjustAmt =(AdjustQty*@hs), AdjProdAmt =(AdjProdQty*@hs), returnAmt =(ReturnQty*@hs),  " +
                        "RetSupAmt =(RetSupQty*@hs), " +
                        "totalamt =((BeliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)),  " +
                        "hs=case when abs(SaldoAwalQty)>0 then " +
                        " (((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty)  " +
                        "else @avgp end where ID=(@b+1) and itemid=@itmID set @b=@b+1 END set @c=@c+1 END' " +
                        "exec sp_executesql @sqlSet, N'' " +

                        "/** Generate Saldo Awal */  " +
                        "SELECT ItemID,SaldoAwalQty,HS,TotalAmt INTO Auto1_lapsaldoawal_" + ViewUsers + " FROM Auto1_mutasireport_" + ViewUsers + " as m WHERE m.DocNo='Saldo Awal'  " +

                        "/** Generate Saldo Akhir */ " +
                        "SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, /** pembelian */ (SUM(m.BeliQty)) As BeliQty, " +
                        "CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, (SUM(m.BeliAmt)) As BeliAmt, " +
                        "/** Ajdut Plust */ (SUM(m.AdjustQty)) As AdjustQty, CASE WHEN SUM(m.AdjustAmt) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty))ELSE 0 END " +
                        "AdjustHS, (SUM(m.AdjustAmt)) As AdjustAmt,  " +
                        "/** Pemakaian Produksi */  " +
                        "(SUM(m.ProdQty)) As ProdQty, " +
                        "CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, (SUM(m.ProdAmt)) As ProdAmt, " +
                        "/** Adjut minus */ " +
                        "(SUM(m.AdjProdQty)) As AdjProdQty, CASE WHEN SUM(m.AdjProdAmt) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty))ELSE 0 END AdjProdHS,  " +
                        "(SUM(m.AdjProdAmt)) As AdjProdAmt, /** Return */ (SUM(m.ReturnQty)) As ReturnQty, CASE WHEN SUM(m.returnAmt) > 0 THEN  " +
                        "(SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, (SUM(m.returnAmt)) As returnAmt, " +
                        "/** Return Supplier */ " +
                        "(SUM(m.RetSupQty)) As RetSupQty, CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, (SUM(m.RetSupAmt))  " +
                        "As RetSupAmt,  " +
                        "/** Saldo Akhir */ " +
                        "(SELECT TOP 1 SaldoAwalQty FROM Auto1_mutasireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As SaldoAwalQty, " +
                        "(SELECT TOP 1 HS FROM Auto1_mutASireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +
                        "CASE when (SELECT TOP 1 SaldoAwalQty FROM Auto1_mutASireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC)>0 then " +
                        "(SELECT TOP 1 TotalAmt FROM Auto1_mutASireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) ELSE 0 END As TotalAmt " +
                        "INTO Auto1_mutasisaldo_" + ViewUsers + " FROM Auto1_mutasireport_" + ViewUsers + " AS m GROUP BY m.ItemID " +

                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_UpdateAvgPrice_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_UpdateAvgPrice_" + ViewUsers + "]  " +
                        "/** Kumpulkan data update average price*/  " +
                        "SELECT  " +
                        "CASE WHEN m.ProdQty >0 THEN (SELECT TOP 1 ID FROM Pakai WHERE Pakai.PakaiNo=m.DocNo)  " +
                        "WHEN m.AdjustQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Tambah')  " +
                        "WHEN m.AdjProdQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Kurang')  " +
                        "WHEN m.ReturnQty >0 THEN (SELECT TOP 1 ID FROM ReturPakai WHERE ReturPakai.ReturNo=m.DocNo)  " +
                        "WHEN m.RetSupQty >0 THEN (SELECT TOP 1 ID FROM ReturSupplier WHERE ReturSupplier.ReturNo=m.DocNo)  " +
                        "WHEN m.BeliQty > 0 THEN (SELECT TOP 1 ID FROM Receipt WHERE Receipt.ReceiptNo=m.DocNo)END ID,  " +
                        "CASE WHEN m.ProdQty >0 THEN m.ItemID WHEN m.AdjustQty >0 THEN m.ItemID WHEN m.AdjProdQty >0 THEN m.ItemID WHEN m.ReturnQty >0 THEN m.ItemID " +
                        "WHEN m.RetSupQty >0 THEN m.ItemID WHEN m.BeliQty >0 THEN m.ItemID END itemID,  " +
                        "CASE WHEN m.ProdQty >0 THEN m.ProdHS WHEN m.AdjustQty >0 THEN m.AdjustHS " +
                        "WHEN m.AdjProdQty >0 THEN m.AdjProdHS WHEN m.ReturnQty >0 THEN m.ReturnHS WHEN m.RetSupQty >0 THEN m.RetSupHS WHEN m.BeliQty >0 THEN m.BeliHS END AvgPrice, " +
                        "CASE WHEN m.ProdQty >0 THEN 'PakaiDetail' WHEN m.AdjustQty>0 THEN 'AdjustDetailT' WHEN m.AdjProdQty>0 THEN 'AdjustDetailK'  " +
                        "WHEN m.ReturnQty >0 THEN 'ReturPakaiDetail' WHEN m.RetSupQty>0 THEN 'ReturSupplierDetail' WHEN m.BeliQty>0 THEN 'ReceiptDetail' " +
                        "END Tabel INTO Auto1_UpdateAvgPrice_" + ViewUsers + " FROM Auto1_mutasireport_" + ViewUsers + " as m  " +
                        "/** update avgprice setiap tabel */ /** Produksi */  " +
                        "UPDATE p SET p.AvgPrice=a.AvgPrice FROM PakaiDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.PakaiID=a.ID WHERE a.Tabel='PakaiDetail' and  " +
                        "p.ItemID=a.itemID  if @itemtypeID='3' begin " +
                        "update PakaiDetail set AvgPrice=(SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID    " +
                        "and vw_HargaReceipt.ItemTypeID=PakaiDetail.ItemTypeID  and vw_HargaReceipt.ReceiptDate<=(select pakaidate from Pakai where ID=PakaiDetail.PakaiID)order by ID Desc)   " +
                        "where PakaiID in (select ID from Pakai where ItemTypeID=3 and LEFT(convert(char,pakaidate,112),6)='" + periodeBlnThn + "') end " +
                        "/** penerimaan*/  " +
                        "UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReceiptDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.ReceiptID=a.ID  " +
                        "WHERE a.Tabel='ReceiptDetail' and p.ItemID=a.itemID /**penyesuaian produksi */  " +
                        "UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReturPakaiDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a  " +
                        "ON P.ReturID=a.ID WHERE a.Tabel='ReturPakaiDetail' and p.ItemID=a.itemID  " +
                        "/** adjust Tambah */  " +
                        "UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.AdjustID=a.ID WHERE a.Tabel='AdjustDetailT'  " +
                        "and p.ItemID=a.itemID  " +
                        "/** Adjust Kurang */ UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.AdjustID=a.ID  " +
                        "WHERE a.Tabel='AdjustDetailK' and p.ItemID=a.itemID  " +

                        "/*declare @sqlS nvarchar(max) */" +
                        //"set @sqlS =''''"+
                        //"update A set A.'+@CurAvgPrice+'=B.HS   from SaldoInventory  A inner join Auto_mutasisaldo_" + UserView + " B  on A.ItemID=B.itemid where A.YearPeriod='+@thnCur " +
                        //"exec sp_executesql @sqlS, N'' " +
                       // "/*--select * from Auto_mutasisaldo_" + UserView + " where itemid in (select ID from inventory where itemcode='090108022000701')*/ " +
                        "/** return supplier no avgprice*/ " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmp_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmp_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasireport_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasisaldo_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasisaldo_" + ViewUsers + "]  " +
                        "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]  ";
                        #endregion
                return strSQL;
        }
        public string GetItemID(string BlnTahun, string GroupID)
        {
            return "select distinct ItemID from vw_StockPurchn where YM=" + BlnTahun + " and GroupID=" + GroupID.ToString()+" group by itemid";
        }
        public string RekapAdjustment(string fDate, string tDate, int DeptID)
        {
            //string Dept = (DeptID == 24 || DeptID == 14 || DeptID==12) ? "" : " and w.DeptID='" + DeptID + "'";
            string Dept = "" ;
            string strSQL = "SELECT * FROM (SELECT AdjustDate,AdjustNo,a.Keterangan1,ad.ItemTypeID,ad.ItemID, "+
                            "CASE ad.ItemTypeID "+
	                        "    WHEN 1 THEN (Select ItemCode From Inventory where Inventory.ID=Ad.ItemID /* AND Aktif=*/)"+
                            "    WHEN 2 THEN (Select ItemCode From Asset where Asset.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "    WHEN 3 THEN (Select ItemCode From Biaya where Biaya.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "END as ItemCode,"+
                            "CASE ad.ItemTypeID "+
                            "    WHEN 1 THEN (Select ItemName From Inventory where Inventory.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "    WHEN 2 THEN (Select ItemName From Asset where Asset.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "    WHEN 3 THEN (Select ItemName From Biaya where Biaya.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "END as ItemName, "+
                            "CASE WHEN a.AdjustType='Tambah' THEN ad.quantity else 0 END AdjustIn,"+
                            "CASE WHEN a.AdjustType='Kurang' or a.AdjustType='disposal' Then ad.Quantity else 0 END AdjustOut," +
                            "(SELECT UOMCode from UOM where ID=ad.UomID) as Unit,"+
                            "Ad.Keterangan, "+
                            "(select top 1 DeptID from Users where UserName=a.CreatedBy and Users.RowStatus >-1) as DeptID,"+
                            "a.CreatedBy as Username "+
                            "FROM Adjust as a "+
                            "INNER JOIN AdjustDetail as ad "+
                            "on ad.AdjustID=a.ID "+
                            "WHERE ad.RowStatus >-1) AS W "+
                            "WHERE CONVERT(Varchar,W.AdjustDate,112) BETWEEN '" + fDate+ "' and '"+ tDate+"' "+Dept+
                            "ORDER BY W.AdjustNo,W.AdjustDate";

            return strSQL;
        }
        public string RekapAjdustmentT1T3(string frDate, string toDate,string Proses)
        {
            string strSQL =(Proses=="T3")? 
                            "select A.AdjustDate,A.AdjustNo,A.NoBA as Keterangan, " +
                            "(Select FC_Items.PartNo from FC_Items where ID=T1.ItemID) as ItemCode, " +
                            "(Select FC_Items.ItemDesc from FC_Items where ID=T1.ItemID) as ItemName, " +
                            " T1.QtyIn as AdjustIn,T1.QtyOut as AdjustOut,'LBR' As Unit, " +
                            " A.Keterangan Keterangan1 from T3_AdjustDetail as T1 " +
                            "left Join T3_Adjust as A " +
                            "on A.ID=t1.AdjustID " +
                            "WHERE CONVERT(VARCHAR,A.AdjustDate,112) between '"+frDate+"' and '"+toDate+"' " +
                            " and A.RowStatus >-1 and T1.Apv >0 and T1.RowStatus>-1 order by A.AdjustDate" :

                            "select A.AdjustDate,A.AdjustNo,A.NoBA as Keterangan, " +
                            "(Select FC_Items.PartNo from FC_Items where ID=T1.ItemID) as ItemCode, " +
                            "(Select FC_Items.ItemDesc from FC_Items where ID=T1.ItemID) as ItemName, " +
                            " T1.QtyIn as AdjustIn,T1.QtyOut as AdjustOut,'LBR' As Unit " +
                            "from T1_AdjustDetail as T1 " +
                            "left Join T1_Adjust as A " +
                            "on A.ID=t1.AdjustID " +
                            "WHERE CONVERT(VARCHAR,A.AdjustDate,112) between '" + frDate + "' and '" + toDate + "' " +
                            "and A.RowStatus >-1 and T1.Apv >0 and T1.RowStatus>-1 order by A.AdjustDate"
                            ;
            return strSQL;
        }
        public string Criteria { get; set; }
        public string CheckPOHargaNol()
        {
            string Query = "select R.ID,R.ReceiptDate,R.ReceiptNo,R.SupplierId,RC.ItemID, " +
                        "(Select Dbo.ItemNameInv(RC.ItemID,1))ItemName,RC.Quantity,PO.NoPO,PO.Crc,PO.Price,SupplierName " +
                        "FROM Receipt R " +
                        "LEFT JOIN ReceiptDetail as RC ON RC.ReceiptID=R.ID " +
                        "LEFT JOIN  vw_PObukanRP PO ON PO.PODetailID=RC.PODetailID " +
                        "WHERE RC.RowStatus>-1 AND R.Status>-1 " +
                        "AND PO.Price <=0 AND RC.ItemID NOT IN(SELECT ItemID FROM POPurchnKadarAir where RowStatus>-1 GROUP BY ITEMID) " +
                        this.Criteria;
            return Query;
        }
        public string CheckKurs()
        {
            return string.Empty;
        }
        public string GetHargaBiaya(string Dari, string Sampai)
        {
            string query = string.Empty;
            query = "SELECT 1 as Tipe,Convert(Char,R.ReceiptDate,112)Tanggal,R.ReceiptNo,RD.ItemID,Po.Price," +
                    "RD.Quantity,((RD.Quantity*PO.Price)/RD.Quantity)AvgPrice, " +
                    "(RD.Quantity*PO.Price)TotalPrice  " +
                    "FROM ReceiptDetail AS RD " +
                    "LEFT JOIN Receipt AS R ON R.ID=RD.ReceiptID " +
                    "LEFT JOIN POPurchnDetail AS PO ON PO.ID=RD.PODetailID " +
                    "LEFT JOIN Biaya AS B ON B.ID=RD.ItemID " +
                    "LEFT JOIN UOM AS U ON U.ID=RD.UomID " +
                    "LEFT JOIN SPPDetail AS S ON S.ID=PO.SppDetailID " +
                    "WHERE RD.RowStatus>-1 AND Rd.ItemID>0 AND RD.ItemTypeID=3 AND RD.GroupID=5" +
                    "AND (CONVERT(Char,R.ReceiptDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "')";
            return query;
        }
        public string MaterialGroup { get; set; }
        public string PostingPriceSaldoAwal(string Periode, string GroupIDne)
        {
            int ItemTypeIDs = 1;
            string result = string.Empty;
            string ViewUsers = ((Users)System.Web.HttpContext.Current.Session["Users"]).ID.ToString() + "_PSA";
            this.NyangBikin = ViewUsers;
            switch (int.Parse(GroupIDne))
            {
                case 5: ItemTypeIDs = 3; break;
                case 4: ItemTypeIDs = 2; break;
                default: ItemTypeIDs = 1; break;
            }
                    
            #region Query nya
            #region prepared data - decalaraion variable
            string strSQL = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmp_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmp_" + ViewUsers + "] " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasireport_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasisaldo_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasisaldo_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport1_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport1_" + ViewUsers + "]  " +
                "declare @thbln char(6)  " +
                "declare @GroupID varchar(max) " +
                "declare @itemtypeID char(6) " +
                "set @thbln='" + Periode + "'  " +
                "set @GroupID ='" + GroupIDne + "' " +
                "set @itemtypeID =(select ItemTypeID from GroupsPurchn where ID=" + GroupIDne + ") " +

                "declare @thnbln0 varchar(6)  " +
                "declare @tgl datetime  " +
                "set @tgl=CAST( (@thbln+ '01') as datetime)  " +
                "set @tgl= DATEADD(month,-1,@tgl)  " +
                "set @thnbln0=LEFT(convert(char,@tgl,112),6)  " +
                "declare @thnAwal  varchar(4) " +
                "declare @blnAwal varchar(2) " +
                "declare @AwalQty varchar(7) " +
                "declare @AwalAvgPrice varchar(11) " +
                "declare @tglawal varchar(10) " +
                "set @tglawal='01-' + right(@thbln,2)+'-'+ LEFT(@thbln,4)  " +
                "set @thnAwal =left(@thnbln0,4) " +
                "set @blnAwal=RIGHT(@thnbln0,2) " +
                "if right(@blnAwal,2)='01' begin set @AwalQty='janqty' set @AwalAvgPrice='janAvgprice'  end " +
                "if right(@blnAwal,2)='02' begin set @AwalQty='febqty' set @AwalAvgPrice='febAvgprice'  end " +
                "if right(@blnAwal,2)='03' begin set @AwalQty='marqty' set @AwalAvgPrice='marAvgprice'  end " +
                "if right(@blnAwal,2)='04' begin set @AwalQty='aprqty' set @AwalAvgPrice='aprAvgprice'  end " +
                "if right(@blnAwal,2)='05' begin set @AwalQty='meiqty' set @AwalAvgPrice='meiAvgprice'  end " +
                "if right(@blnAwal,2)='06' begin set @AwalQty='junqty' set @AwalAvgPrice='junAvgprice'  end " +
                "if right(@blnAwal,2)='07' begin set @AwalQty='julqty' set @AwalAvgPrice='julAvgprice'  end " +
                "if right(@blnAwal,2)='08' begin set @AwalQty='aguqty' set @AwalAvgPrice='aguAvgprice'  end " +
                "if right(@blnAwal,2)='09' begin set @AwalQty='sepqty' set @AwalAvgPrice='sepAvgprice'  end " +
                "if right(@blnAwal,2)='10' begin set @AwalQty='oktqty' set @AwalAvgPrice='oktAvgprice'  end " +
                "if right(@blnAwal,2)='11' begin set @AwalQty='novqty' set @AwalAvgPrice='novAvgprice'  end " +
                "if right(@blnAwal,2)='12' begin set @AwalQty='desqty' set @AwalAvgPrice='desAvgprice'  end " +

                "declare @thnCur  varchar(4) " +
                "declare @blnCur varchar(2) " +
                "declare @CurQty varchar(7) " +
                "declare @CurAvgPrice varchar(11) " +
                "set @thnCur =LEFT(@thbln,4) " +
                "set @blnCur=RIGHT(@thbln,2) " +
                "if right(@blnCur,2)='01' begin set @CurQty='janqty' set @CurAvgPrice='janAvgprice'  end " +
                "if right(@blnCur,2)='02' begin set @CurQty='febqty' set @CurAvgPrice='febAvgprice'  end " +
                "if right(@blnCur,2)='03' begin set @CurQty='marqty' set @CurAvgPrice='marAvgprice'  end " +
                "if right(@blnCur,2)='04' begin set @CurQty='aprqty' set @CurAvgPrice='aprAvgprice'  end " +
                "if right(@blnCur,2)='05' begin set @CurQty='meiqty' set @CurAvgPrice='meiAvgprice'  end " +
                "if right(@blnCur,2)='06' begin set @CurQty='junqty' set @CurAvgPrice='junAvgprice'  end " +
                "if right(@blnCur,2)='07' begin set @CurQty='julqty' set @CurAvgPrice='julAvgprice'  end " +
                "if right(@blnCur,2)='08' begin set @CurQty='aguqty' set @CurAvgPrice='aguAvgprice'  end " +
                "if right(@blnCur,2)='09' begin set @CurQty='sepqty' set @CurAvgPrice='sepAvgprice'  end " +
                "if right(@blnCur,2)='10' begin set @CurQty='oktqty' set @CurAvgPrice='oktAvgprice'  end " +
                "if right(@blnCur,2)='11' begin set @CurQty='novqty' set @CurAvgPrice='novAvgprice'  end " +
                "if right(@blnCur,2)='12' begin set @CurQty='desqty' set @CurAvgPrice='desAvgprice'  end " +

                "declare @sqlP nvarchar(max) ";
            #endregion
            #region pengumpulan data
            string Dari = Periode + "01";
            string Sampai = Periode + (DateTime.DaysInMonth(int.Parse(Periode.Substring(0, 4).ToString()), int.Parse(Periode.Substring(4, 2).ToString()))).ToString().PadLeft(2, '0');
            string Tahun = Periode.Substring(0, 4);
            string[] Query = this.ViewMutasiStock(Dari, Sampai, GroupIDne, Tahun).Split(new string[] { "/**/" }, StringSplitOptions.None);
            //strSQL += "SET @sqlP='" + Query[1].Replace("'", "''") + "exec sp_executesql @sqlP, N''' ";
            //strSQL += Query[2].ToString();
            #endregion
            #region Query Lama
            strSQL += "set @sqlP='SELECT * into Auto1_lapmutASitmp_" + ViewUsers + " FROM(  " +

                "(SELECT ''0'' AS Tipe,'''+@tglawal+''' AS Tanggal,''Saldo Awal'' AS DocNo,si.ItemID,si.'+@AwalAvgPrice+' AS SaldoHS,  " +
                "si.'+@AwalQty+' AS SaldoQty,CASE WHEN si.'+@AwalAvgPrice+' >0 THEN si.'+@AwalAvgPrice+' ELSE si.'+@AwalAvgPrice+' END AvgPrice,(si.'+@AwalQty+'*si.'+@AwalAvgPrice+') AS TotalPrice  " +
                "FROM SaldoInventory AS si WHERE si.YearPeriod='+ @thnAwal +'  AND si.ItemTypeID='''+ @itemtypeID +'''  and GroupID in ('+ @groupID +') )  " +
                "UNION ALL  " +
                "(SELECT ''1'' AS Tipe,CONVERT(CHAR,Tanggal,105) AS Tanggal,ReceiptNo,ItemID, " +
                "CASE WHEN x.Price >0 THEN " + //jika price=0
                " CASE WHEN x.crc >1 then CASE WHEN x.Flag =2 Then  " + //jika kurs bukan rp dan supp harus bayar dolar ambil dari Matauangkurs
                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " + //di tambahkan untuk transaksi stlah bln ags 2016
                "       (x.NilaiKurs * x.Price) ELSE " + //kurs diambil dari nilai kurs
                "   ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal )* x.Price) END ELSE " +
                "   CASE WHEN x.NilaiKurs >0  " + //jika nilaikurs di table popurchn >0 kalikan dengan nilai kurs
                "       THEN (x.NilaiKurs * x.Price) ELSE " + // jika nilai kurs =0 ambil dari table mataunga kurs base on tgl receipt
                "       ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal)* x.Price)  " +
                "       END END ELSE x.Price END " +
                "ELSE CASE WHEN x.Crc <=1 THEN x.HargaSatuan END END Price,Quantity,  " +
                "CASE WHEN x.Price > 0 THEN " +
                "CASE WHEN (x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN " +
                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                "       (x.NilaiKurs * x.Price) ELSE ( " +
                "           (select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)*(isnull((x.Price),0))) " +
                "       END ELSE CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price) ELSE  " +
                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                "(isnull((x.Price),0))) END END ELSE isnull((x.Price),0) END ELSE CASE WHEN(x.Crc <=1) THEN  " +
                "x.HargaSatuan END END AvgPrice," +
                "CASE WHEN(x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN  " +
                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                "       (x.NilaiKurs * x.Price * x.Quantity) ELSE " +
                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                "(isnull((x.Price*x.Quantity),0))) END ELSE  " +
                "CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price*x.Quantity) ELSE  " +
                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl = x.Tanggal)*(isnull((x.Price*x.Quantity),0)))  " +
                "END END ELSE CASE WHEN x.Price>0 THEN isnull((x.Price*x.Quantity),0) ELSE (x.HargaSatuan * x.Quantity)  " +
                "END END TotalPrice " +
                "FROM( SELECT ''1'' as Tipe, p.ReceiptDate as Tanggal,p.ReceiptNo ,rd.ItemID,  " +
                "Case When pod.Price=0 then pod.Price2 else pod.Price end Price,rd.Quantity,  " +
                "pod.crc,pod.flag,pod.NilaiKurs,ISNULL(pod.SubCompanyID,0)SubCompanyID,p.SupplierID,  " +
                "(pod.Price*rd.Quantity) as TotalPrice,rd.POID,rd.ID as ReceiptID,pod.Price2 as HargaSatuan,bo.Qty  " +
                "FROM Receipt as p LEFT JOIN ReceiptDetail as rd on rd.ReceiptID=p.ID LEFT JOIN vw_PObukanRP as pod on rd.PODetailID=pod.PODetailID  " +
                "LEFT JOIN TabelHargaBankOut AS bo ON bo.ReceiptDetailID=rd.ID WHERE (left(convert(varchar,p.ReceiptDate,112),6)='+ @thbln +')  " +
                "AND rd.ItemID IN (select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') )  " +
                "AND pod.ItemTypeID='+ @itemtypeID +' AND p.Status >-1 AND rd.RowStatus >-1 ) as x )  " +
                "UNION ALL  " +
                "(SELECT ''2'' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity,  " +
                "(pk.AvgPrice) AS AvgPrice,(pk.Quantity*pk.AvgPrice) AS TotalPrice " +
                "FROM Pakai AS k LEFT JOIN PakaiDetail AS pk ON pk.PakaiID=k.ID  " +
                "WHERE (left(cONvert(VARCHAR,k.PakaiDate,112),6)='+ @thbln +') AND pk.ItemID IN( " +
                "select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') ) " +
                "AND k.Status >-1 AND pk.RowStatus >-1 AND pk.ItemTypeID='+@itemtypeID+')  " +
                "UNION ALL  " +
                "(SELECT ''3'' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity,  " +
                "(rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice " +
                "FROM ReturPakai AS rp LEFT JOIN ReturPakaiDetail AS rpd  " +
                "ON rpd.ReturID=rp.ID WHERE (left(cONvert(VARCHAR,rp.ReturDate,112),6)='+ @thbln +')  " +
                "AND rpd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') ) " +
                "AND rp.Status >-1 AND rpd.RowStatus >-1 AND rpd.ItemTypeID='+@itemtypeID+')  " +
                "UNION ALL  " +
                "(SELECT ''4'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal,  " +
                "CASE when a.AdjustType=''Tambah'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice As SaldoHS,  " +
                "CASE When a.AdjustType=''Tambah'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPrice, (ad.Quantity*ad.AvgPrice) AS TotalPrice  " +
                "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID " +
                "WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                "AND a.AdjustType=''Tambah'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') )  " +
                "AND a.Status >-1 AND ad.RowStatus >-1 AND ad.ItemTypeID='+@itemtypeID+' ) " +
                "UNION ALL  " +
                "(SELECT ''5'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105)  " +
                "AS Tanggal, CASE when a.AdjustType=''Kurang'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice,  " +
                "CASE When a.AdjustType=''Kurang'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPriceK, (ad.Quantity*ad.AvgPrice) AS TotalPriceK  " +
                "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                "AND a.AdjustType=''Kurang'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') )  " +
                "AND a.Status >-1 AND ad.RowStatus >-1 AND ad.ItemTypeID='+@itemtypeID+') " +
                "UNION ALL  " +
                "(SELECT ''6'' AS Tipe,CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity,  " +
                "CAST(''0'' AS Decimal(18,6)) AS AvgPrice ,CAST(''0'' AS Decimal(18,6)) AS Totalprice FROM ReturSupplier AS rs LEFT JOIN ReturSupplierDetail AS rsd  " +
                "ON rsd.ReturID=rs.ID where (left(convert(varchar,rs.ReturDate,112),6)='+ @thbln +')  " +
                "AND rsd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' and GroupID in ('+ @groupID +') ) AND rs.Status >-1  " +
                "AND rsd.RowStatus >-1 AND rsd.ItemTypeID='+@itemtypeID+' ) ) as K' " +
                "exec sp_executesql @sqlP, N'' " +


                "/** susun sesuai dengan kolom laporan */  " +
                // "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') AND type in (N'U')) "+
                // "DROP TABLE [dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "] " +
                "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, BeliQty,BeliHS," +
                "(BeliQty*BeliHS) as BeliAmt,  " +
                "AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, " +
                "ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt,  " +
                "ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, AdjProdQty,AdjProdHS AS AdjProdHS," +
                "(AdjProdQty*HPP) as AdjPAmt,RetSupQty,  " +
                "RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                "(SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty,  " +
                "((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                "(ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                "INTO Auto1_lapmutasitmpx_" + ViewUsers + " FROM ( " +
                "SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo,  " +
                "CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0) ELSE 0 END SaldoAwalQty,  " +
                "CASE WHEN Tipe='0' THEN ISNULL(SaldoHS,0) ELSE 0 END HPP,  " +
                "CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, " +
                "CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0) ELSE 0 END BeliQty,  " +
                "CASE WHEN Tipe='1' THEN ISNULL(SaldoHS,0) ELSE 0 END BeliHS,  " +
                "CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPB,  " +
                "CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0) ELSE 0 END ProdQty,  " +
                "CASE WHEN Tipe='2' THEN ISNULL(SaldoHS,0) ELSE 0 END ProdHS,  " +
                "CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAd,  " +
                "CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0) ELSE 0 END ReturnQty,  " +
                "CASE WHEN Tipe='3' THEN ISNULL(SaldoHS,0) ELSE 0 END ReturHS,  " +
                "CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPP,  " +
                "CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjustQty,  " +
                "CASE WHEN Tipe='4' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjustHS,  " +
                "CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPR,  " +
                "CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjProdQty,  " +
                "CASE WHEN Tipe='5' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjProdHS,  " +
                "CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAdjP,  " +
                "CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0) ELSE 0 END RetSupQty,  " +
                "CASE WHEN Tipe='6' THEN ISNULL(SaldoHS,0) ELSE 0 END RetSupHS,  " +
                "CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalRetSup FROM Auto1_lapmutasitmp_" + ViewUsers + " as x  " +
                "/*where itemid in (select distinct ID from Inventory where LEN(itemcode)>11 )*/) AS Z ORDER BY z.Tanggal  " +

                "/** susun data berdasarkan item id dan bentuk id baru */  " +
                // "/*IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') "+
                // "AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]*/ " +
                "SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) as IDn,* " +
                "INTO Auto1_lapmutasitmpxx_" + ViewUsers +
                " FROM Auto1_lapmutasitmpx_" + ViewUsers +

                " /**Susun data tabular */  " +
                //"/*--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasireport_" + ViewUsers + "]') "+
                //"AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasireport_" + ViewUsers + "]*/ " +
                "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo,  " +
                "BeliQty,BeliHS,(BeliQty*BeliHS) As BeliAmt,AdjustQty, " +
                "CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                "   THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                "   WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS,ProdQty, " +
                "CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN " +
                "   (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                "   FROM Auto1_lapMutasitmpxx_" + ViewUsers + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, AdjProdQty,  " +
                "CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 " +
                "   THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                "   FROM Auto1_lapMutasitmpxx_" + ViewUsers + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, A.ReturnQty, " +
                "CASE WHEN A.ID>1 AND A.ReturnQty >0  " +
                "   THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END " +
                "FROM Auto1_lapMutasitmpxx_" + ViewUsers + " WHERE ID <A.ID AND ItemID=A.ItemID)  " +
                "ELSE A.HPP END ReturnHS, A.RetSupQty, " +
                "CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                "   THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                "   WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS,  " +
                "CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                "   WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty,  " +
                "CASE WHEN A.ID>1 THEN CASE WHEN (SELECT SUM(totalqty)FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                "   WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN  " +
                "   ((SELECT SUM(totalamt) FROM Auto1_lapMutasitmpxx_" + ViewUsers +
                "   WHERE ID <=A.ID AND ItemID=A.ItemID )/ (ABS((SELECT SUM(totalqty)FROM Auto1_lapMutasitmpxx_" + ViewUsers + "  " +
                "   WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                "CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM Auto1_lapMutasitmpxx_" + ViewUsers + "  " +
                "   WHERE ID <=A.ID AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt INTO Auto1_lapmutasireport_" + ViewUsers +
                " FROM Auto1_lapMutasitmpxx_" + ViewUsers + " as A  " +
                "ORDER by itemID,A.Tanggal,A.IDn,a.Tipe,a.DocNo  " +

                "/** Generate Detail Report without saldo akhir */  " +
                //"/*--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport_" + ViewUsers + "]') "+
                //"AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport_" + ViewUsers + "]*/ " +
                "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS,  " +
                "(l.BeliQty*l.BeliHS) as BeliAmt,l.AdjustQty, " +
                "CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1)  " +
                "   AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                "CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1)  " +
                "   AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, l.ProdQty, " +
                "CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT ISNULL(HS,0)  " +
                "   FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                "CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT ISNULL(HS,0)  " +
                "   FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, l.AdjProdQty,  " +
                "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN " +
                "   (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS,  " +
                "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN " +
                "   (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt,  " +
                "l.ReturnQty, " +
                "CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0  " +
                "   END ReturnHS, " +
                "CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) " +
                "   AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, l.RetSupQty, " +
                "CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1)  " +
                "   AND ItemID=L.ItemID)ELSE 0 END RetSupHS, CASE WHEN L.IDn >1 AND l.RetSupQty >0 " +
                "   THEN(SELECT ISNULL(HS,0) FROM Auto1_lapmutasireport_" + ViewUsers + " WHERE IDn=(L.IDn-1) AND  " +
                "   ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, l.SaldoAwalQty,l.HS,l.TotalAmt " +
                "INTO Auto1_mutasireport_" + ViewUsers + " FROM Auto1_lapmutasireport_" + ViewUsers + " AS L  " +
                "ORDER BY L.itemID,L.Tipe,L.Tanggal  " +

                "select row_number() over(order by itemID) as IDn,itemid into Auto1_mutasireport1_" + ViewUsers + " from Auto1_mutasireport_" + ViewUsers + " group by itemID order by itemid  " +
                "declare @sqlSet nvarchar(max) " +
                "set @sqlSet =' " +
                "declare @i int " +
                "declare @b int " +
                "declare @hs decimal(18,6) " +
                "declare @amt decimal(18,6) " +
                "declare @avgp decimal(18,6)  " +
                "declare @c int " +
                "declare @itm int " +
                "declare @itmID int " +
                "set @b=0 set @c=0 " +
                "set @itm=(select COUNT(IDn) from Auto1_mutasireport1_" + ViewUsers + ")  " +
                "While @c <=@itm " +
                "   Begin set @itmID=(select isnull(itemID,0) from Auto1_mutasireport1_" + ViewUsers + " where IDn=@c)  " +
                "   set @avgp=(select top 1 '+ @AwalAvgPrice +' from SaldoInventory where ItemID = @itmID and YearPeriod='+ @thnAwal +' ) " +
                "if ISNULL(@avgp,0)=0 OR @avgp=0 " +
                                "begin " +
                                "set @avgp=(select top 1 HS from Auto1_mutasireport_" + ViewUsers + " where itemid=@itmID and HS>0 ) " +
                                "end " +
                "   set @i=(select COUNT(id) from Auto1_mutasireport_" + ViewUsers + " where itemid=@itmID) " +
                "   set  @b=0 While @b<=@i  " +
                "Begin set @hs=CASE WHEN @b >1 THEN (select hs from Auto1_mutasireport_" + ViewUsers + " where ID=(@b) and itemid=@itmID)  " +
                "ELSE CASE WHEN(SELECT hs from Auto1_mutasireport_" + ViewUsers + " where ID=1 and itemid=@itmID)>0 THEN 	  " +
                "(SELECT hs from Auto1_mutasireport_" + ViewUsers + " where ID=1 and itemid=@itmID)ELSE @avgp END 	 END  " +
                "set @amt=CASE WHEN @b >1 THEN (select TotalAmt from Auto1_mutasireport_" + ViewUsers + " where ID=(@b) and itemid=@itmID)  " +
                "ELSE (SELECT TotalAmt from Auto1_mutasireport_" + ViewUsers + " where ID=1 and itemid=@itmID) END  " +

                "/** update semua hs */  " +
                "update Auto1_mutasireport_" + ViewUsers + " " +
                "set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0  " +
                "THEN @hs ELSE 0 END, " +
                "ProdHS	=CASE WHEN (SELECT ProdQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                "ReturnHS=CASE WHEN (SELECT ReturnQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                "AdjProdHS=CASE WHEN (SELECT AdjProdQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                "RetSupHS=CASE WHEN (SELECT RetSupQty FROM Auto1_mutasireport_" + ViewUsers + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                "ProdAmt=(ProdQty*@hs), AdjustAmt =(AdjustQty*@hs), AdjProdAmt =(AdjProdQty*@hs), returnAmt =(ReturnQty*@hs),  " +
                "RetSupAmt =(RetSupQty*@hs), " +
                "totalamt =((BeliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)),  " +
                "hs=case when abs(SaldoAwalQty)>0 then " +
                " (((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty)  " +
                "else @avgp end where ID=(@b+1) and itemid=@itmID set @b=@b+1 END set @c=@c+1 END' " +
                "exec sp_executesql @sqlSet, N'' " +

                "/** Generate Saldo Awal */  " +
                "SELECT ItemID,SaldoAwalQty,HS,TotalAmt INTO Auto1_lapsaldoawal_" + ViewUsers + " FROM Auto1_mutasireport_" + ViewUsers + " as m WHERE m.DocNo='Saldo Awal'  " +

                "/** Generate Saldo Akhir */ " +
                "SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, /** pembelian */ (SUM(m.BeliQty)) As BeliQty, " +
                "CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, (SUM(m.BeliAmt)) As BeliAmt, " +
                "/** Ajdut Plust */ (SUM(m.AdjustQty)) As AdjustQty, CASE WHEN SUM(m.AdjustAmt) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty))ELSE 0 END " +
                "AdjustHS, (SUM(m.AdjustAmt)) As AdjustAmt,  " +
                "/** Pemakaian Produksi */  " +
                "(SUM(m.ProdQty)) As ProdQty, " +
                "CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, (SUM(m.ProdAmt)) As ProdAmt, " +
                "/** Adjut minus */ " +
                "(SUM(m.AdjProdQty)) As AdjProdQty, CASE WHEN SUM(m.AdjProdAmt) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty))ELSE 0 END AdjProdHS,  " +
                "(SUM(m.AdjProdAmt)) As AdjProdAmt, /** Return */ (SUM(m.ReturnQty)) As ReturnQty, CASE WHEN SUM(m.returnAmt) > 0 THEN  " +
                "(SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, (SUM(m.returnAmt)) As returnAmt, " +
                "/** Return Supplier */ " +
                "(SUM(m.RetSupQty)) As RetSupQty, CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, (SUM(m.RetSupAmt))  " +
                "As RetSupAmt,  " +
                "/** Saldo Akhir */ " +
                "(SELECT TOP 1 SaldoAwalQty FROM Auto1_mutasireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As SaldoAwalQty, " +
                "(SELECT TOP 1 HS FROM Auto1_mutASireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +
                "CASE when (SELECT TOP 1 SaldoAwalQty FROM Auto1_mutASireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC)>0 then " +
                "(SELECT TOP 1 TotalAmt FROM Auto1_mutASireport_" + ViewUsers + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) ELSE 0 END As TotalAmt " +
                "INTO Auto1_mutasisaldo_" + ViewUsers + " FROM Auto1_mutasireport_" + ViewUsers + " AS m GROUP BY m.ItemID " +
                "";
            #endregion
            #region Process Update kd data transaksi
            strSQL +="IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_UpdateAvgPrice_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_UpdateAvgPrice_" + ViewUsers + "]  " +
                "/** Kumpulkan data update average price*/  " +
                "SELECT  " +
                "CASE WHEN m.ProdQty >0 THEN (SELECT TOP 1 ID FROM Pakai WHERE Pakai.PakaiNo=m.DocNo)  " +
                "WHEN m.AdjustQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Tambah')  " +
                "WHEN m.AdjProdQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Kurang')  " +
                "WHEN m.ReturnQty >0 THEN (SELECT TOP 1 ID FROM ReturPakai WHERE ReturPakai.ReturNo=m.DocNo)  " +
                "WHEN m.RetSupQty >0 THEN (SELECT TOP 1 ID FROM ReturSupplier WHERE ReturSupplier.ReturNo=m.DocNo)  " +
                "WHEN m.BeliQty > 0 THEN (SELECT TOP 1 ID FROM Receipt WHERE Receipt.ReceiptNo=m.DocNo)END ID,  " +
                "CASE WHEN m.ProdQty >0 THEN m.ItemID WHEN m.AdjustQty >0 THEN m.ItemID WHEN m.AdjProdQty >0 THEN m.ItemID WHEN m.ReturnQty >0 THEN m.ItemID " +
                "WHEN m.RetSupQty >0 THEN m.ItemID WHEN m.BeliQty >0 THEN m.ItemID END itemID,  " +
                "CASE WHEN m.ProdQty >0 THEN m.ProdHS WHEN m.AdjustQty >0 THEN m.AdjustHS " +
                "WHEN m.AdjProdQty >0 THEN m.AdjProdHS WHEN m.ReturnQty >0 THEN m.ReturnHS WHEN m.RetSupQty >0 THEN m.RetSupHS WHEN m.BeliQty >0 THEN m.BeliHS END AvgPrice, " +
                "CASE WHEN m.ProdQty >0 THEN 'PakaiDetail' WHEN m.AdjustQty>0 THEN 'AdjustDetailT' WHEN m.AdjProdQty>0 THEN 'AdjustDetailK'  " +
                "WHEN m.ReturnQty >0 THEN 'ReturPakaiDetail' WHEN m.RetSupQty>0 THEN 'ReturSupplierDetail' WHEN m.BeliQty>0 THEN 'ReceiptDetail' " +
                "END Tabel INTO Auto1_UpdateAvgPrice_" + ViewUsers + " FROM Auto1_mutasireport_" + ViewUsers + " as m  " +
                "/** update avgprice setiap tabel */"+
                "/** Produksi */  " +
                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM PakaiDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.PakaiID=a.ID WHERE a.Tabel='PakaiDetail' and  " +
                "p.ItemID=a.itemID  and p.ItemTypeID=" + ItemTypeIDs + 
                "if @itemtypeID=3 begin " +
                "update PakaiDetail set AvgPrice=(SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID    " +
                "and vw_HargaReceipt.ItemTypeID="+ItemTypeIDs+" and vw_HargaReceipt.ReceiptDate<=(select pakaidate from Pakai where ID=PakaiDetail.PakaiID)order by ID Desc)   " +
                "where PakaiID in (select ID from Pakai where ItemTypeID="+ItemTypeIDs+" and LEFT(convert(char,pakaidate,112),6)='" + Periode + "') end " +
                        
                "/** penerimaan*/  " +
                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReceiptDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.ReceiptID=a.ID  " +
                "WHERE a.Tabel='ReceiptDetail' and p.ItemID=a.itemID and p.ItemTypeID="+ItemTypeIDs+
                " /**penyesuaian produksi */  " +
                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReturPakaiDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a  " +
                "ON P.ReturID=a.ID WHERE a.Tabel='ReturPakaiDetail' and p.ItemID=a.itemID and p.ItemTypeID=" + ItemTypeIDs + " " +
                "/** adjust Tambah */  " +
                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.AdjustID=a.ID WHERE a.Tabel='AdjustDetailT'  " +
                "and p.ItemID=a.itemID and p.ItemTypeID=" + ItemTypeIDs + "  " +
                "/** Adjust Kurang */ "+
                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto1_UpdateAvgPrice_" + ViewUsers + " as a ON P.AdjustID=a.ID  " +
                "WHERE a.Tabel='AdjustDetailK' and p.ItemID=a.itemID and p.ItemTypeID=" + ItemTypeIDs + "  " +

                "declare @sqlS nvarchar(max) " +
                "set @sqlS ='update A set A.'+@CurAvgPrice+'=ROUND(ISNULL(B.HS,0),2)   from SaldoInventory  A inner join Auto1_mutasisaldo_" + ViewUsers + " B  on A.ItemID=B.itemid where A.YearPeriod='+@thnCur " +
                "exec sp_executesql @sqlS, N'' " + 

                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmp_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmp_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpx_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasitmpxx_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapmutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapmutasireport_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasireport_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasireport_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_mutasisaldo_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_mutasisaldo_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_UpdateAvgPrice_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_UpdateAvgPrice_" + ViewUsers + "]  " +
                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto1_lapsaldoawal_" + ViewUsers + "]  ";
            #endregion
            #endregion
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.RecordsAffected > 0)
            {
                result = sdr.RecordsAffected.ToString("###,###") + " Records Updated ";
            }
            else
            {
                result = da.Error.ToString();
            }
            return result;
        }
    }

}
