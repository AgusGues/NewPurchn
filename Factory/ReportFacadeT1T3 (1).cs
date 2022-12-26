using BusinessFacade;
using Cogs;
using Dapper;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Factory
{
    public class ReportFacadeT1T3
    {
        private ArrayList arrReport;

        public string ViewPemantauanPelarian(string tgl1, string tgl2, int tgltype, string partno)
        {
            string strSQL;
            if (tgltype == 1)
            {
                strSQL = "SELECT ID,DestID, TglJemur, TglSerah, PartNo1, Lokasi1, kubik1, PartNo2, Lokasi2, qty, kubik2, kubik1 - kubik2 AS sisa " +
                        "FROM (SELECT ID,DestID, TglJemur, PartNo1, Lokasi1, PartNo2, Lokasi2, qty, [user],  " +
                        "(select top 1 tglserah from T1_Serah where DestID=P.DestID and JemurID=P.ID) as TglSerah, Tebal, Lebar, Panjang, Tebal2, Lebar2, Panjang2,  " +
                        "Tebal * Panjang * Lebar / 1000000000 * qty AS kubik1, Tebal2 * Panjang2 * Lebar2 / 1000000000 * qty AS kubik2 " +
                        "FROM (SELECT DISTINCT A.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, A.QtyIn AS qty, A.CreatedBy AS [user], I1.Tebal, I1.Lebar,  " +
                        "I1.Panjang, I2.Tebal AS Tebal2, I2.Lebar AS Lebar2, I2.Panjang AS Panjang2, A.DestID, A.ID " +
                        "FROM T1_JemurLg AS A LEFT OUTER JOIN FC_Items AS I2 ON A.ItemID = I2.ID LEFT OUTER JOIN FC_Lokasi AS L2 ON A.LokID = L2.ID LEFT OUTER JOIN " +
                        "FC_Lokasi AS L1 ON A.LokID0 = L1.ID LEFT OUTER JOIN FC_Items AS I1 ON A.ItemID0 = I1.ID " +
                        "WHERE  A.status>-1 and A.DestID>0 and (CONVERT(char(8), A.TglJemur, 112) >= '" + tgl1 + "') AND (CONVERT(char(8), A.TglJemur, 112) <= '" + tgl2 + "')) as P) as P1 ORDER BY TglSerah";
            }
            else
            {
                strSQL = "SELECT ID,DestID, TglJemur, TglSerah, PartNo1, Lokasi1, kubik1, PartNo2, Lokasi2, qty, kubik2, kubik1 - kubik2 AS sisa " +
                        "FROM (SELECT ID,DestID, TglJemur, PartNo1, Lokasi1, PartNo2, Lokasi2, qty, [user],  " +
                        "(select top 1 tglserah from T1_Serah where DestID=P.DestID and JemurID=P.ID) as TglSerah, Tebal, Lebar, Panjang, Tebal2, Lebar2, Panjang2,  " +
                        "Tebal * Panjang * Lebar / 1000000000 * qty AS kubik1, Tebal2 * Panjang2 * Lebar2 / 1000000000 * qty AS kubik2 " +
                        "FROM (SELECT DISTINCT A.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, A.QtyIn AS qty, A.CreatedBy AS [user], I1.Tebal, I1.Lebar,  " +
                        "I1.Panjang, I2.Tebal AS Tebal2, I2.Lebar AS Lebar2, I2.Panjang AS Panjang2, A.DestID, A.ID " +
                        "FROM T1_JemurLg AS A LEFT OUTER JOIN FC_Items AS I2 ON A.ItemID = I2.ID LEFT OUTER JOIN FC_Lokasi AS L2 ON A.LokID = L2.ID LEFT OUTER JOIN " +
                        "FC_Lokasi AS L1 ON A.LokID0 = L1.ID LEFT OUTER JOIN FC_Items AS I1 ON A.ItemID0 = I1.ID " +
                        "WHERE  A.status>-1 and A.DestID>0 and (CONVERT(char(8), S.TglSerah, 112) >= '" + tgl1 + "') AND (CONVERT(char(8),S.TglSerah, 112) <= '" + tgl2 + "')) as P) as P1 ORDER BY TglSerah";
            }
            return strSQL;
        }

        public string ViewLTransitH(string tgl1, string tgl2, int tgltype, string partno)
        {
            string strSQL;
            //if (tgltype == 1)
            //{
            //    strSQL = "SELECT  A.TglProduksi, C.Tgltrans as TglSerah, I1.PartNo as partno1, L1.Lokasi as Lokasi1, I2.PartNo  as partno2, L2.Lokasi AS Lokasi2, C.QtyIn as Qty, C.CreatedBy " +
            //        "FROM  T3_Rekap AS C LEFT OUTER JOIN T1_Serah AS B ON C.DestID = B.DestID  and B.ItemID =C.t1sitemid  INNER JOIN " +
            //        "FC_Lokasi AS L1 ON B.LokID = L1.ID INNER JOIN FC_Items AS I1 ON B.ItemID = I1.ID INNER JOIN FC_Items AS I2 ON C.ItemID = I2.ID INNER JOIN " +
            //        "FC_Lokasi AS L2 ON C.LokID = L2.ID inner JOIN BM_Destacking AS A ON C.DestID = A.ID where CONVERT(char(8), A.TglProduksi, 112)>='" +
            //        tgl1 + "' AND CONVERT(char(8), A.TglProduksi, 112)<='" + tgl2 + "' " + partno + " ORDER BY A.TglProduksi";
            //}
            //else
            //{
            strSQL = "SELECT  distinct C.createdtime as tglinput, C.ID,A.TglProduksi, B.TglSerah, I1.PartNo as partno1, L1.Lokasi as Lokasi1, I2.PartNo  as partno2, L2.Lokasi AS Lokasi2, C.QtyIn as Qty, C.CreatedBy " +
               "FROM  T3_Rekap AS C LEFT OUTER JOIN T1_Serah AS B ON C.T1SerahID = B.ID INNER JOIN " +
               "FC_Lokasi AS L1 ON B.LokID = L1.ID INNER JOIN FC_Items AS I1 ON B.ItemID = I1.ID INNER JOIN FC_Items AS I2 ON C.ItemID = I2.ID INNER JOIN " +
               "FC_Lokasi AS L2 ON C.LokID = L2.ID inner JOIN BM_Destacking AS A ON C.DestID = A.ID where C.rowstatus>-1 " +
               tgl1 + tgl2 + partno + "";
            //}
            return strSQL;
        }

        public string ViewLTransitHPel(string tgl1, string tgl2, int tgltype, string Partno)
        {
            string strSQL;
            if (tgltype == 1)
            {
                strSQL = "SELECT  C.CreatedTime AS TglProduksi, C.Tgltrans as TglSerah, I1.PartNo as partno1, L1.Lokasi as Lokasi1, I2.PartNo  as partno2, " +
                    "L2.Lokasi AS Lokasi2, C.QtyIn as Qty, C.CreatedBy " +
                    "FROM  T1_Serah INNER JOIN T1_JemurLg ON T1_Serah.JemurID = T1_JemurLg.ID AND T1_Serah.DestID = T1_JemurLg.DestID INNER JOIN " +
                    "T3_Rekap AS C INNER JOIN FC_Items AS I2 ON C.ItemID = I2.ID INNER JOIN FC_Lokasi AS L2 ON C.LokID = L2.ID ON T1_Serah.ID = C.T1SerahID INNER JOIN " +
                    "FC_Lokasi AS L1 ON T1_JemurLg.LokID = L1.ID INNER JOIN FC_Items AS I1 ON T1_JemurLg.ItemID = I1.ID  where CONVERT(char(8), C.CreatedTime, 112)>='" +
                    tgl1 + "' AND CONVERT(char(8), C.CreatedTime, 112)<='" + tgl2 + "' " + Partno + " ORDER BY C.CreatedTime";
            }
            else
            {
                strSQL = "SELECT  C.CreatedTime AS TglProduksi, T1_JemurLg.Tgljemur as TglSerah, I1.PartNo as partno1, L1.Lokasi as Lokasi1, I2.PartNo  as partno2, " +
                    "L2.Lokasi AS Lokasi2, C.QtyIn as Qty, C.CreatedBy " +
                    "FROM  T1_Serah INNER JOIN T1_JemurLg ON T1_Serah.JemurID = T1_JemurLg.ID AND T1_Serah.DestID = T1_JemurLg.DestID INNER JOIN " +
                    "T3_Rekap AS C INNER JOIN FC_Items AS I2 ON C.ItemID = I2.ID INNER JOIN FC_Lokasi AS L2 ON C.LokID = L2.ID ON T1_Serah.ID = C.T1SerahID INNER JOIN " +
                    "FC_Lokasi AS L1 ON T1_JemurLg.LokID = L1.ID INNER JOIN FC_Items AS I1 ON T1_JemurLg.ItemID = I1.ID  where CONVERT(char(8), T1_JemurLg.Tgljemur, 112)>='" +
                    tgl1 + "' AND CONVERT(char(8), T1_JemurLg.Tgljemur, 112)<='" + tgl2 + "' ORDER BY C.Tgltrans";
            }
            return strSQL;
        }

        public string ViewLTransitInPelarian(string tgl1, string tgl2, int tgltype, string partNo)
        {
            string strSQL;
            //if (tgltype == 1)
            //{
            //strSQL = "SELECT   A.CreatedTime as tglProduksi,  A.TglJemur as tglserah, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, " +
            //    "L2.Lokasi AS Lokasi2, A.QtyIn AS qty, A.CreatedBy AS [user] " +
            //    "FROM FC_Items AS I2 RIGHT OUTER JOIN T1_JemurLg AS A ON I2.ID = A.ItemID LEFT OUTER JOIN FC_Lokasi AS L2 ON A.LokID = L2.ID LEFT OUTER JOIN " +
            //    "FC_Lokasi AS L1 ON A.LokID0 = L1.ID LEFT OUTER JOIN FC_Items AS I1 ON A.ItemID0 = I1.ID where  A.status>-1 and CONVERT(char(8), A.CreatedTime, 112)>='" +
            //    tgl1 + "' AND CONVERT(char(8), A.CreatedTime, 112)<='" + tgl2 + "' " + partno + " ORDER BY A.CreatedTime";
            strSQL = " Select P1.ID,P1.DestID,P1.TglProduksi,P1.TglJemur,P1.PartNo as PartNo1,P1.Lokasi as Lokasi1,P1.NoPAlet,BC.PartNo AS PartNo2,CC.Lokasi as Lokasi2,P1.QtyIn as QTY,P1.CreatedBy as [User] " +
                      " from (select B.TglProduksi,A.DestID,A.ItemID0,A.TglJemur,A.LokID0,BB.PartNo,C.Lokasi,P.NoPAlet, A.QtyIn,A.CreatedBy,A.CreatedTime,A.ID " +
                      " from T1_JemurLg as A, BM_Destacking as B,FC_Lokasi as C, FC_Items as BB,BM_Palet as P " +
                      " where B.LokasiID=C.ID and  A.DestID=B.ID and B.PaletID=P.ID and B.ItemID = BB.ID and A.status>-1 " + tgl1 + tgl2 + " ) as P1 " +
                      " INNER JOIN FC_ItemsAS BC ON P1.ItemID0 = BC.ID " +
                      " INNER JOIN FC_Lokasi AS CC ON P1.LokID0 = CC.ID " +
                      " " + partNo + " order by P1.TglJemur ";

            //}
            //else
            //{
            //strSQL = "SELECT   A.CreatedTime as tglProduksi,  A.TglJemur as tglserah, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, " +
            //    "L2.Lokasi AS Lokasi2, A.QtyIn AS qty, A.CreatedBy AS [user] " +
            //    "FROM FC_Items AS I2 RIGHT OUTER JOIN T1_JemurLg AS A ON I2.ID = A.ItemID LEFT OUTER JOIN FC_Lokasi AS L2 ON A.LokID = L2.ID LEFT OUTER JOIN " +
            //    "FC_Lokasi AS L1 ON A.LokID0 = L1.ID LEFT OUTER JOIN FC_Items AS I1 ON A.ItemID0 = I1.ID where  A.status>-1 and  CONVERT(char(8), A.TglJemur, 112)>='" +
            //    tgl1 + "' AND CONVERT(char(8), A.TglJemur, 112)<='" + tgl2 + "' " + partno + " ORDER BY A.TglJemur";
            //    strSQL = " Select P1.ID,P1.DestID,P1.TglProduksi,P1.TglJemur,P1.PartNo as PartNo1,P1.Lokasi as Lokasi1,P1.NoPAlet,BC.PartNo AS PartNo2,CC.Lokasi as Lokasi2,P1.QtyIn as QTY,P1.CreatedBy as [User] " +
            //                  " from (select AA.TglProduksi,A.DestID,A.ItemID0,A.TglJemur,A.LokID0,BB.PartNo,C.Lokasi,P.NoPAlet,A.QtyIn,A.CreatedBy,A.CreatedTime,A.ID " +
            //                  " from T1_JemurLg as A, BM_Destacking as AA,FC_Lokasi as C, FC_Items as BB,BM_Palet as P where A.status>-1 and AA.LokasiID=C.ID and  A.DestID=AA.ID and AA.PaletID=P.ID and AA.ItemID = BB.ID and  CONVERT(char(8), AA.TglProduksi, 112)>='20130901' AND CONVERT(char(8), AA.TglProduksi, 112)<='20130930') as P1 " +
            //                  " INNER JOIN FC_ItemsAS BC ON P1.ItemID0 = BC.ID " +
            //                  " INNER JOIN FC_Lokasi AS CC ON P1.LokID0 = CC.ID " +
            //                  " order by P1.TglJemur ";
            //}
            return strSQL;
        }

        public string ViewLPenyerahan(string tgl, string periode)
        {
            string strSQL;
            if (periode == "harian")
                strSQL = "SELECT A.TglProduksi,  SUBSTRING(B.partno,1,3) Jenis,B.Tebal, B.Lebar, B.Panjang, SUM(A.Qty) as qty, C.Lokasi " +
                "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS B ON A.ItemID = B.ID LEFT OUTER JOIN " +
                "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN BM_Formula AS D ON A.FormulaID = D.ID where qty>0 and convert(varchar,A.TglProduksi,112)='" + tgl + "' " +
                "and C.Lokasi not like'%adj%' and  A.rowstatus=0 GROUP BY A.TglProduksi,  B.partno,B.Tebal, B.Lebar, B.Panjang, C.Lokasi " +
                "order by A.TglProduksi  ";
            else
                strSQL = "SELECT A.TglProduksi,  SUBSTRING(B.partno,1,3) Jenis, B.Tebal, B.Lebar, B.Panjang, SUM(A.Qty) as qty, C.Lokasi " +
               "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS B ON A.ItemID = B.ID LEFT OUTER JOIN " +
               "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN BM_Formula AS D ON A.FormulaID = D.ID where qty>0 and left(convert(varchar,A.TglProduksi,112),6)='" + tgl.Substring(0, 6) + "' " +
               " and C.Lokasi not like'%adj%' and  A.rowstatus=0 GROUP BY A.TglProduksi, B.partno, B.Tebal, B.Lebar, B.Panjang, C.Lokasi " +
               "order by A.TglProduksi  ";
            return strSQL;
        }

        public string ViewLSimetris(string tgl1, string tgl2, int tgltype)
        {
            string strSQL;
            if (tgltype == 0)
                strSQL = "SELECT B.ID,B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm," +
                    " B.QtyOut as QtyOutSm,B.CreatedBy, D.Groups,I1.Volume V1,I2.Volume V2,ISNULL(B.MCutter,'-')MCutter FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN " +
                    "FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN " +
                    "FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                    "where I1.partno<>I2.partno and B.rowstatus>-1 and convert(varchar,B.tgltrans,112)>='" + tgl1 + "' and convert(varchar,B.tgltrans,112)<='" + tgl2 + "' order by I1.PartNo";
            else
                strSQL = "SELECT B.ID,B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm," +
                    " B.QtyOut as QtyOutSm,B.CreatedBy, D.Groups,I1.Volume V1,I2.Volume V2,ISNULL(B.MCutter,'-')MCutter FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN " +
                    "FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN " +
                    "FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                    "where I1.partno<>I2.partno and B.rowstatus>-1 and convert(varchar,B.createdtime,112)>='" + tgl1 + "' and convert(varchar,B.createdtime,112)<='" + tgl2 + "' order by I1.PartNo";
            return strSQL;
        }

        public string ViewLSimetrisAutoBS(string tgl1, string tgl2, int tgltype)
        {
            string strSQL;
            if (tgltype == 0)
                strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsimetris]') AND type in (N'U')) DROP TABLE [dbo].[tempsimetris] " +
                        "select IDENTITY(int, 1,1) AS IDn,ID as CutID,tglinput,Tanggal,PartnoSer,LokasiSer,QtyInSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then PartnoSm else '' end PartnoSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then LokasiSm else '' end LokasiSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then QtyOutSm else 0 end QtyOutSm, " +
                        "case when PartnoSm like '%-p-%' then PartnoSm else '' end PartnoBP, " +
                        "case when PartnoSm like '%-p-%' then LokasiSm else '' end LokasiBP, " +
                        "case when PartnoSm like '%-p-%' then QtyOutSm else 0 end QtyOutBP, " +
                        "case when PartnoSm like '%-S-%' then PartnoSm else '' end PartnoBS, " +
                        "case when PartnoSm like '%-S-%' then LokasiSm else '' end LokasiBS, " +
                        "case when PartnoSm like '%-S-%' then QtyOutSm else 0 end QtyOutBS, " +
                        "CreatedBy into tempsimetris from ( " +
                        "SELECT B.ID,R.ID as RID, B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm,  " +
                        "I3.PartNo AS PartnoSm, L3.Lokasi AS LokasiSm, R.QtyiN as QtyOutSm,B.CreatedBy, D.Groups  " +
                        "FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID  " +
                        "INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2  " +
                        "ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID left join T3_Rekap R on B.ID=R.CutID and R.QtyIn>0 " +
                        "inner join FC_Items I3 on R.ItemID=I3.ID and R.QtyIn>0 inner join FC_Lokasi L3 on R.LokID =L3.ID and R.QtyIn>0   " +
                        "where I1.partno<>I2.partno and B.rowstatus>-1 and R.[status]>-1 and convert(varchar,B.tgltrans,112)>='" + tgl1 + "' and  " +
                        "convert(varchar,B.tgltrans,112)<='" + tgl2 + "')  as S order by CutID,RID  " +
                        "select CutID,case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else tglinput end tglinput, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else Tanggal end Tanggal, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else PartnoSer end PartnoSer, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else LokasiSer end LokasiSer, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else QtyInSm end QtyInSm, " +
                        "PartnoSm,LokasiSm,QtyOutSm,PartnoBP,LokasiBP,QtyOutBP,PartnoBS,LokasiBS,QtyOutBS,CreatedBy " +
                        " from tempsimetris A order by cutID,QtyInSm desc,QtyOutSm desc,QtyOutBP desc,QtyOutBS desc ";
            else
                strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsimetris]') AND type in (N'U')) DROP TABLE [dbo].[tempsimetris] " +
                        "select IDENTITY(int, 1,1) AS IDn,ID as CutID,tglinput,Tanggal,PartnoSer,LokasiSer,QtyInSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then PartnoSm else '' end PartnoSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then LokasiSm else '' end LokasiSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then QtyOutSm else 0 end QtyOutSm, " +
                        "case when PartnoSm like '%-p-%' then PartnoSm else '' end PartnoBP, " +
                        "case when PartnoSm like '%-p-%' then LokasiSm else '' end LokasiBP, " +
                        "case when PartnoSm like '%-p-%' then QtyOutSm else 0 end QtyOutBP, " +
                        "case when PartnoSm like '%-S-%' then PartnoSm else '' end PartnoBS, " +
                        "case when PartnoSm like '%-S-%' then LokasiSm else '' end LokasiBS, " +
                        "case when PartnoSm like '%-S-%' then QtyOutSm else 0 end QtyOutBS, " +
                        "CreatedBy into tempsimetris from ( " +
                        "SELECT B.ID,R.ID as RID, B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm,  " +
                        "I3.PartNo AS PartnoSm, L3.Lokasi AS LokasiSm, R.QtyiN as QtyOutSm,B.CreatedBy, D.Groups  " +
                        "FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID  " +
                        "INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2  " +
                        "ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID left join T3_Rekap R on B.ID=R.CutID and R.QtyIn>0 " +
                        "inner join FC_Items I3 on R.ItemID=I3.ID and R.QtyIn>0 inner join FC_Lokasi L3 on R.LokID =L3.ID and R.QtyIn>0   " +
                        "where I1.partno<>I2.partno and B.rowstatus>-1 and R.[status]>-1 and convert(varchar,B.createdtime,112)>='" + tgl1 + "' and  " +
                        "convert(varchar,B.createdtime,112)<='" + tgl2 + "')  as S order by CutID,RID  " +
                        "select CutID,case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else tglinput end tglinput, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else Tanggal end Tanggal, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else PartnoSer end PartnoSer, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else LokasiSer end LokasiSer, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else QtyInSm end QtyInSm, " +
                        "PartnoSm,LokasiSm,QtyOutSm,PartnoBP,LokasiBP,QtyOutBP,PartnoBS,LokasiBS,QtyOutBS,CreatedBy " +
                        " from tempsimetris A order by cutID,QtyInSm desc,QtyOutSm desc,QtyOutBP desc,QtyOutBS desc ";
            return strSQL;
        }

        public string ViewLMutasiLokasi(string tgl1, string tgl2, int tgltype)
        {
            string strSQL;
            if (tgltype == 0)
                strSQL = "SELECT B.CreatedTime as tglinput,B.TglTrans AS Tanggal, I.PartNo AS PartnoSer, L1.Lokasi AS LokasiSer, L2.Lokasi AS LokasiSm, B.Qty AS QtyOutSm, B.CreatedBy " +
                    "FROM T3_MutasiLok AS B INNER JOIN FC_Items AS I ON B.ItemID = I.ID INNER JOIN FC_Lokasi AS L2 ON B.LokID2 = L2.ID  " +
                    "INNER JOIN FC_Lokasi AS L1 ON B.LokID1 = L1.ID " +
                    "where  B.rowstatus>-1 and  convert(varchar,B.tgltrans,112)>='" + tgl1 + "' and convert(varchar,B.tgltrans,112)<='" + tgl2 + "' order by I.PartNo";
            else
                strSQL = "SELECT B.CreatedTime as tglinput,B.TglTrans AS Tanggal, I.PartNo AS PartnoSer, L1.Lokasi AS LokasiSer, L2.Lokasi AS LokasiSm, B.Qty AS QtyOutSm, B.CreatedBy " +
                    "FROM T3_MutasiLok AS B INNER JOIN FC_Items AS I ON B.ItemID = I.ID INNER JOIN FC_Lokasi AS L2 ON B.LokID2 = L2.ID  " +
                    "INNER JOIN FC_Lokasi AS L1 ON B.LokID1 = L1.ID " +
                    "where  B.rowstatus>-1 and  convert(varchar,B.createdtime,112)>='" + tgl1 + "' and convert(varchar,B.createdtime,112)<='" + tgl2 + "' order by I.PartNo";
            return strSQL;
        }

        public string ViewLASimetris(string tgl1, string tgl2, int tgltype)
        {
            string strSQL;
            if (tgltype == 0)
                strSQL = "SELECT A.CreatedTime as tglinput,rtrim(A.DocNo)+rtrim(cast(A.serahid as varchar)) + rtrim(cast(A.QtyIn as varchar)) as docno, A.TglTrans, I.PartNo AS PartNoIn, L.Lokasi AS LokasiIn, A.QtyIn, I1.PartNo, A.QtyOut, L1.Lokasi, A.CreatedBy " +
                    "FROM T3_Asimetris AS A left JOIN T3_Serah AS B ON A.SerahID = B.ID INNER JOIN FC_Lokasi AS L ON B.LokID = L.ID INNER JOIN " +
                    "FC_Items AS I ON B.ItemID = I.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID " +
                    "where  A.rowstatus>-1 and convert(varchar,A.tgltrans,112)>='" + tgl1 + "' and convert(varchar,A.tgltrans,112)<='" + tgl2 + "' order by I.PartNo";
            else
                strSQL = "SELECT A.CreatedTime as tglinput,rtrim(A.DocNo)+rtrim(cast(A.serahid as varchar)) + rtrim(cast(A.QtyIn as varchar)) as docno, A.TglTrans, I.PartNo AS PartNoIn, L.Lokasi AS LokasiIn, A.QtyIn, I1.PartNo, A.QtyOut, L1.Lokasi, A.CreatedBy " +
                    "FROM T3_Asimetris AS A left JOIN T3_Serah AS B ON A.SerahID = B.ID INNER JOIN FC_Lokasi AS L ON B.LokID = L.ID INNER JOIN " +
                    "FC_Items AS I ON B.ItemID = I.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID " +
                    "where  A.rowstatus>-1 and convert(varchar,A.createdtime,112)>='" + tgl1 + "' and convert(varchar,A.createdtime,112)<='" + tgl2 + "' order by I.PartNo";
            return strSQL;
        }

        public string ViewLSaldoLokasi(string kriteria)
        {
            string strSQL;
            //strSQL = "select partno,ukuran,lokasi,sum(qty) as Qty from (SELECT C.PartNo,  cast(CAST(C.tebal as decimal(18,1)) as varchar) + ' X ' + cast(CAST(C.Lebar as int) as varchar)+ " +
            //        "' X ' + cast(CAST(C.Panjang as int) as varchar) as ukuran, B.Lokasi, A.Qty " +
            //        "FROM  T3_Serah AS A INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID INNER JOIN FC_Items AS C ON A.ItemID = C.ID " +
            //        "WHERE  A.rowstatus>-1 and  A.qty <> 0 " + kriteria + " ) as A  group by partno,ukuran,lokasi";

            strSQL =
         " select PartNo,ukuran,Lokasi,Qty,ROUND((Qty*Volume),3)M3  " +
         " from (select partno,ukuran,lokasi,sum(qty) as Qty,Volume " +
         " from (SELECT C.PartNo,  cast(CAST(C.tebal as decimal(18,1)) as varchar) + ' X ' + cast(CAST(C.Lebar as int) as varchar)+ ' X ' + cast(CAST(C.Panjang as int) as varchar) as ukuran, B.Lokasi, A.Qty,((C.Tebal*C.Panjang*C.Lebar)/1000000000)Volume " +
         " FROM  T3_Serah AS A " +
         " INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID " +
         " INNER JOIN FC_Items AS C ON A.ItemID = C.ID " +
         " WHERE  A.rowstatus>-1 and  A.qty <> 0  " + kriteria + " " +
         " ) as A  group by partno,ukuran,lokasi,Volume ) as A1 ";

            return strSQL;
        }

        public string ViewLTransitIn(string tgl1, string tgl2, int tgltype, string Partno)
        {
            //transit in harian
            string strSQL;
            //if (tgltype == 1)
            //{
            //    strSQL = "SELECT B.TglProduksi, A.TglSerah, I2.PartNo as PartNo1, L2.Lokasi as Lokasi1, I1.PartNo AS PartNo2, L1.Lokasi AS Lokasi2, " +
            //        "A.QtyIn as qty,A.createdby as [user] FROM FC_Items AS I2 INNER JOIN T1_Serah AS A ON I2.ID = A.itemID0 INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID " +
            //        "INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID LEFT OUTER JOIN BM_Destacking AS B INNER JOIN FC_Lokasi AS L2 ON " +
            //        "B.LokasiID = L2.ID ON A.DestID = B.ID where CONVERT(char(8), B.TglProduksi, 112)>='" +
            //        tgl1 + "' AND CONVERT(char(8), B.TglProduksi, 112)<='" + tgl2 + "' " + Partno + " ORDER BY B.TglProduksi";
            //}
            //else
            //{
            //strSQL = "SELECT A.ID, A.createdtime as tglinput,B.TglProduksi, case when A.sfrom='lari' then (select tgljemur from T1_JemurLg where ID=A.JemurID) else TglSerah end TglSerah, I1.PartNo AS PartNo1, L2.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, L1.Lokasi AS Lokasi2, A.QtyIn AS qty,  " +
            //        "A.CreatedBy AS [user], P.NoPAlet AS Palet "+
            //        "FROM FC_Lokasi AS L1 RIGHT OUTER JOIN T1_Serah AS A ON L1.ID = A.LokID LEFT OUTER JOIN FC_Items AS I2 ON A.itemID = I2.ID LEFT OUTER JOIN "+
            //        "FC_Lokasi AS L2 RIGHT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS B INNER JOIN FC_Items AS I1 ON B.ItemID = I1.ID ON P.ID = B.PaletID ON L2.ID = B.LokasiID ON A.DestID = B.ID where A.status>-1 " + tgl1 + tgl2 + Partno + " ORDER BY  A.tglserah";
            strSQL = "SELECT A.ID, B.TglProduksi,  A.TglSerah, I1.PartNo AS PartNo1, L2.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, " +
                "L1.Lokasi AS Lokasi2, A.QtyIn AS qty,A.CreatedBy AS [user], P.NoPAlet AS Palet,case when left(convert(char,A.tglserah,112),6)<'201904' then J.Oven else A.oven end Oven " +
                "FROM FC_Lokasi AS L1 RIGHT OUTER JOIN T1_Serah AS A ON L1.ID = A.LokID LEFT OUTER JOIN FC_Items AS I2 ON A.itemID = I2.ID LEFT OUTER JOIN " +
                "FC_Lokasi AS L2 RIGHT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS B INNER JOIN FC_Items AS I1 ON B.ItemID = " +
                "I1.ID ON P.ID = B.PaletID ON L2.ID = B.LokasiID ON A.DestID = B.ID left join T1_Jemur J on J.DestID=A.DestID and J.ID=A.JemurID where  A.SFrom<>'lari' and  A.status>-1 " + tgl1 + tgl2 + Partno +
                " ORDER BY  A.tglserah";

            //}
            return strSQL;
        }

        public string ViewLTransitInPel(string kriteria)
        {
            //transit in pelarian
            string strSQL;
            #region mark
            //if (tgltype == 1)
            //{
            //    strSQL = "SELECT C.ItemID , A.TglProduksi, B.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, P.NoPAlet,I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, " +
            //        "case when C.ID >0 and (select COUNT(itemID0) from T1_Serah C1 where C1.DestID =C.DestID and C1.JemurID=B.ID and C.itemID0=C1.itemID0 and C1.ID<C.ID)=0  " +
            //        "then B.Qtyin when isnull(C.ID,0)=0 then B.QTYin else 0 end Qty1, C.TglSerah, I3.PartNo AS PartNo3, L3.Lokasi AS Lokasi3, C.QtyIn AS QTY2 " +
            //        "FROM FC_Items AS I2 INNER JOIN T1_JemurLg AS B ON I2.ID = B.ItemID0 INNER JOIN FC_Lokasi AS L2 ON L2.ID = B.LokID0 INNER JOIN " +
            //        "FC_Items AS I1 INNER JOIN BM_Destacking AS A ON I1.ID = A.ItemID INNER JOIN FC_Lokasi AS L1 ON A.LokasiID = L1.ID INNER JOIN " +
            //        "BM_Palet AS P ON A.PaletID = P.ID ON B.DestID = A.ID LEFT OUTER JOIN FC_Lokasi AS L3 INNER JOIN FC_Items AS I3 INNER JOIN " +
            //        "T1_Serah AS C ON I3.ID = C.ItemID ON L3.ID = C.LokID ON B.ID = C.JemurID AND B.DestID = C.DestID " +
            //        "WHERE (CONVERT(char(8), B.createdtime, 112) >= '" + tgl1 + "') AND (CONVERT(char(8), B.createdtime, 112) <= '" + tgl2 + "') " +
            //        "ORDER BY B.DestID,C.ID";
            //}
            //else
            //{
            //strSQL = "select *,(qty1-isnull(qty2,0)) as sisa from (SELECT B.destid,C.ID,C.ItemID , A.TglProduksi, B.TglJemur, I1.PartNo AS PartNo1, "+
            //    "L1.Lokasi AS Lokasi1, P.NoPAlet,I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, " +
            //    "case when C.ID >0 and (select COUNT(itemID0) from T1_Serah C1 where C1.DestID =C.DestID and C1.JemurID=B.ID and C.itemID0=C1.itemID0 and C1.ID<C.ID)=0  " +
            //    "then B.Qtyin when isnull(C.ID,0)=0 then B.QTYin else 0 end Qty1, C.TglSerah, I3.PartNo AS PartNo3, L3.Lokasi AS Lokasi3, C.QtyIn AS QTY2,B.createdBy as [user],B.createdtime as tglinput " +
            //    "FROM FC_Items AS I2 INNER JOIN T1_JemurLg AS B ON I2.ID = B.ItemID0 INNER JOIN FC_Lokasi AS L2 ON L2.ID = B.LokID0 INNER JOIN " +
            //    "FC_Items AS I1 INNER JOIN BM_Destacking AS A ON I1.ID = A.ItemID INNER JOIN FC_Lokasi AS L1 ON A.LokasiID = L1.ID INNER JOIN " +
            //    "BM_Palet AS P ON A.PaletID = P.ID ON B.DestID = A.ID LEFT OUTER JOIN FC_Lokasi AS L3 INNER JOIN FC_Items AS I3 INNER JOIN " +
            //    "T1_Serah AS C ON I3.ID = C.ItemID ON L3.ID = C.LokID ON B.ID = C.JemurID AND B.DestID = C.DestID " +
            //    "WHERE  B.status>-1 and C.status>-1 " +
            //    ") as lari where destid>0  " + kriteria +
            //    "            union all " +
            //    "select *,(qty1-isnull(qty2,0)) as sisa from ( " +
            //    "SELECT     B.DestID,0 AS ID,0 as itemid, A.TglProduksi, B.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, P.NoPAlet, I2.PartNo AS PartNo2,  " +
            //    "L2.Lokasi AS Lokasi2,  B.QTYin  AS Qty1, B.TglJemur as tglserah,null as PartNo3,null as Lokasi3, ISNULL(B.QtyOut, 0) AS QTY2, B.CreatedBy AS [user], B.CreatedTime AS tglinput " +
            //    "FROM FC_Lokasi AS L1 RIGHT OUTER JOIN " +
            //    "                      FC_Lokasi AS L2 RIGHT OUTER JOIN " +
            //    "                      T1_JemurLg AS B ON L2.ID = B.LokID0 LEFT OUTER JOIN " +
            //    "                      FC_Items AS I2 ON B.ItemID0 = I2.ID LEFT OUTER JOIN " +
            //    "                      BM_Destacking AS A ON B.DestID = A.ID LEFT OUTER JOIN " +
            //    "                      FC_Items AS I1 ON A.ItemID = I1.ID LEFT OUTER JOIN " +
            //    "                      BM_Palet AS P ON A.PaletID = P.ID ON L1.ID = A.LokasiID " +
            //    "WHERE  B.qtyout- B.qtyout>0 and  (B.Status > - 1) " +
            //    " ) as lari where destid>0  " + kriteria + " ORDER BY DestID,ID";

            //}
            #endregion mark

            #region Lama
            //strSQL = "select ID,(select [GROUp] from BM_PlantGroup where ID in (select plantgroupID from bm_destacking where ID=lagi.destid)) //GP,DestID,TglProduksi,TglJemur,createdtime,PartNo1,Lokasi1,NoPAlet,PartNo2,Lokasi2,Qty1,[user],tglinput,tglinputserah,serahID,jemurID,TglSerah, //" +
            //"case when PartNo3=PartNo2 and Lokasi3=Lokasi2 then '' else  PartNo3 end PartNo3,case when PartNo3=PartNo2 and Lokasi3=Lokasi2 then '' else
            //"+
            //"(select top 1 lokasi from ( " +
            //"select Lokasi from FC_Lokasi where ID in (select top 1 lokid from T3_Rekap where T1SerahID=lagi.serahID )union all " +
            //"select Lokasi from FC_Lokasi where  ID in (select top 1 lokid from T1_serah where ID=lagi.serahID ))L where lokasi<>'c99' order by Lokasi //desc) end Lokasi3,QTY2,(qty1-isnull(qty2,0)) as sisa from ( " +
            //"select lari.ID,lari.DestID,TglProduksi,TglJemur,createdtime,PartNo1,Lokasi1,NoPAlet,PartNo2,Lokasi2,case when "+
            //"(select COUNT(destid) from T1_Serah where JemurID=serah.JemurID and  ID<serah.serahID and [status]>-1 and itemid0=serah.itemID0 )>0 then 0 //else Qty1 end Qty1," +
            //"[user],tglinput,tglinputserah,serahID,jemurID,TglSerah,PartNo3,Lokasi3,case when Lokasi2=Lokasi3 then 0 else QTY2 end  QTY2  " +
            //"from ( " +
            //"SELECT B.ID,B.DestID, A.TglProduksi,B.createdtime, B.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, P.NoPAlet, I2.PartNo AS
            //PartNo2, L2.Lokasi AS Lokasi2,  " +
            // "  B.Qtyin  AS Qty1, B.CreatedBy AS [user], B.CreatedTime AS tglinput  " +
            //"FROM FC_Lokasi AS L2 RIGHT OUTER JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 LEFT OUTER JOIN " +
            //"FC_Items AS I2 ON B.ItemID0 = I2.ID LEFT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS A ON P.ID = A.PaletID LEFT OUTER JOIN
            //" +
            //"FC_Items AS I1 ON A.ItemID = I1.ID LEFT OUTER JOIN FC_Lokasi AS L1 ON A.LokasiID = L1.ID ON B.DestID = A.ID " +
            //"WHERE (B.Status > - 1) ) as lari " +
            //"left join  " +
            //"(SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID, I3.PartNo AS PartNo3, L3.Lokasi
            //AS Lokasi3, C.QtyIn AS QTY2 " +
            //  "FROM FC_Items AS I3 RIGHT OUTER JOIN FC_Lokasi AS L3 RIGHT OUTER JOIN T1_Serah AS C ON L3.ID = C.LokID ON I3.ID = C.ItemID " +
            // "WHERE C.Status > - 1) as serah on lari.DestID=serah.DestID and lari.ID=serah.JemurID " +
            //") as lagi  where destid>0  " + kriteria + " ORDER BY DestID,ID,Qty1 desc";
            //return strSQL;
            #endregion Lama

            #region Baru 08 Oktober 2018 By Beny
            strSQL = "select ID,(select [GROUp] from BM_PlantGroup where ID in (select plantgroupID from bm_destacking where ID=lagi.destid)) GP,DestID,TglProduksi,TglJemur,createdtime,PartNo1,Lokasi1,NoPAlet,PartNo2,Lokasi2,Qty1,[user],tglinput,tglinputserah,serahID,jemurID,TglSerah, " +
                            "case when PartNo3=PartNo2 and Lokasi3=Lokasi2 then '' else  PartNo3 end PartNo3,case when PartNo3=PartNo2 and Lokasi3=Lokasi2 then '' else " +
                            "(select top 1 lokasi from ( " +
                            "select Lokasi from FC_Lokasi where ID in (select top 1 lokid from T3_Rekap where T1SerahID=lagi.serahID )union all " +
                            "select Lokasi from FC_Lokasi where  ID in (select top 1 lokid from T1_serah where ID=lagi.serahID ))L where lokasi<>'c99' order by Lokasi desc) end Lokasi3,QTY2,(qty1-isnull(qty2,0)) as sisa, " +
                            "(select top 1 oven from t1_serah where destid=lagi.DestID and oven>0 )Oven from ( " +
                            "select lari.ID,lari.DestID,TglProduksi,TglJemur,createdtime,PartNo1,Lokasi1,NoPAlet,PartNo2,Lokasi2,case when " +
                            "(select COUNT(destid) from T1_Serah where JemurID=serah.JemurID and  ID<serah.serahID and [status]>-1 and itemid0=serah.itemID0 and DestID=serah.DestID)>0 then 0 else Qty1 end Qty1," +
                            "[user],tglinput,tglinputserah,serahID,jemurID,TglSerah,PartNo3,Lokasi3,case when Lokasi2=Lokasi3 then 0 else QTY2 end  QTY2  " +
                            "from ( " +
                            "SELECT B.ID,B.DestID, A.TglProduksi,B.createdtime, B.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, P.NoPAlet, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2,  " +
                            "  B.Qtyin  AS Qty1, B.CreatedBy AS [user], B.CreatedTime AS tglinput  " +
                            "FROM FC_Lokasi AS L2 RIGHT OUTER JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 LEFT OUTER JOIN " +
                            "FC_Items AS I2 ON B.ItemID0 = I2.ID LEFT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS A ON P.ID = A.PaletID LEFT OUTER JOIN " +
                            "FC_Items AS I1 ON A.ItemID = I1.ID LEFT OUTER JOIN FC_Lokasi AS L1 ON A.LokasiID = L1.ID ON B.DestID = A.ID " +
                            "WHERE (B.Status > - 1) ) as lari " +
                            "left join  " +
                            "(SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID, I3.PartNo AS PartNo3, L3.Lokasi AS Lokasi3, C.QtyIn AS QTY2 " +
                            "FROM FC_Items AS I3 RIGHT OUTER JOIN FC_Lokasi AS L3 RIGHT OUTER JOIN T1_Serah AS C ON L3.ID = C.LokID ON I3.ID = C.ItemID " +
                            "WHERE C.Status > - 1) as serah on lari.DestID=serah.DestID and lari.ID=serah.JemurID " +
                            ") as lagi  where destid>0  " + kriteria + " ORDER BY DestID,ID,Qty1 desc";
            return strSQL;
            #endregion Baru 08 Oktober 2018 By Beny
        }

        public string ViewLSaldoItemB(string blnQty, int tahun, string jenis)
        {
            //saldo bulanan
            string Q1 = string.Empty;
            string Q2 = string.Empty;

            if (jenis == "W")
            {
                Q1 = " (B.PartNo like'%-M-%' or B.PartNo like'%-W-%') ";
            }
            else
            {
                Q1 = " B.PartNo like'%-" + jenis + "-%' ";
            }

            string strSQL;
            strSQL = "SELECT distinct B.PartNo, B.ItemDesc, A." + blnQty + " as qty, A.YearPeriod FROM SaldoInventoryBJ AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID " +
                    "where A." + blnQty + ">0 and A.YearPeriod=" + tahun + " and  " + Q1 + " ";
            return strSQL;
        }

        public string ViewLSaldoTransit_T1()
        {
            string strSQL;
            strSQL = "select left(convert(char,tglserah,112),6) as periode0,right(rtrim(convert(char,tglserah,106)),8) as periode,I.PartNo,cast(CAST(I.tebal as decimal(18,1)) as varchar) + ' X ' + cast(CAST(I.Lebar as int) as varchar)+ ' X ' + " +
                 "cast(CAST(I.Panjang as int) as varchar) as ukuran, L.Lokasi,sum(A.QtyIn-A.QtyOut) as Qty from t1_serah A left join  " +
                 "FC_Items I ON A.ItemID=I.ID left join FC_Lokasi L on A.LokID =L.ID  where  A.Status >-1 and  A.QtyIn>A.QtyOut and  " +
                 "A.DestID in(select ID from BM_Destacking where TglProduksi >='12/1/2013')  and Lokasi not like 'adj%' " +
                 "group by left(convert(char,tglserah,112),6),right(rtrim(convert(char,tglserah,106)),8),I.PartNo,L.Lokasi,I.tebal,I.Lebar,I.Panjang " +
                 "union all " +
                "select left(convert(char,TglJemur,112),6) as periode0,right(rtrim(convert(char,TglJemur,106)),8) as periode,I.PartNo, " +
                "cast(CAST(I.tebal as decimal(18,1)) as varchar) + ' X ' + cast(CAST(I.Lebar as int) as varchar)+ ' X ' +  " +
                "cast(CAST(I.Panjang as int) as varchar) as ukuran, L.Lokasi,sum(A.QtyIn-A.QtyOut) as Qty  " +
                "from T1_JemurLg  A left join  FC_Items I ON A.ItemID0=I.ID left join FC_Lokasi L on A.LokID0 =L.ID   " +
                "where  A.Status >-1 and  A.QtyIn>A.QtyOut and A.DestID in(select ID from BM_Destacking where TglProduksi >='12/1/2013')  and Lokasi not like 'adj%'  " +
                "group by left(convert(char,TglJemur,112),6),right(rtrim(convert(char,TglJemur,106)),8),I.PartNo,L.Lokasi,I.tebal,I.Lebar,I.Panjang  " +
                "order by L.Lokasi,I.PartNo";
            return strSQL;
        }

        public string ViewPemantauanT1(string tgl1, string tgl2, int tgltype)
        {
            string strSQL;
            if (tgltype == 0)
            {
                strSQL = "SELECT A.id_dstk,  A.TglProduksi, IA.PartNo AS PartNo1,LA.Lokasi AS Lokasi1, A.Qty AS Qty1,IA.Volume AS V1, B.TglSerah,IB.PartNo AS PartNo2, " +
                    "SUM(B.QtyIn) AS Qty2, LB.Lokasi AS Lokasi2,  sum(IB.Volume*B.QtyIn) AS V2 " +
                    "FROM FC_Items AS IB INNER JOIN T1_Serah AS B ON IB.ID = B.ItemID INNER JOIN FC_Lokasi AS LB ON B.LokID = LB.ID LEFT OUTER JOIN " +
                    "FC_Lokasi AS LA INNER JOIN BM_Destacking AS A ON LA.ID = A.LokasiID INNER JOIN FC_Items AS IA ON A.ItemID = IA.ID ON B.DestID = A.ID " +
                    "WHERE  B.status>-1 and convert(varchar,A.TglProduksi,112)>='" + tgl1 + "' and convert(varchar,A.TglProduksi,112)<='" + tgl2 + "' " +
                    "GROUP BY A.Qty, B.TglSerah, LA.Lokasi, IA.PartNo, IB.PartNo, LB.Lokasi, A.id_dstk, IA.Volume, IB.Volume, A.TglProduksi " +
                    "ORDER BY A.id_dstk";
            }
            else
            {
                strSQL = "SELECT A.id_dstk,  A.TglProduksi, IA.PartNo AS PartNo1,LA.Lokasi AS Lokasi1, A.Qty AS Qty1,IA.Volume AS V1, B.TglSerah,IB.PartNo AS PartNo2, " +
                    "SUM(B.QtyIn) AS Qty2, LB.Lokasi AS Lokasi2,  sum(IB.Volume*B.QtyIn) AS V2 " +
                    "FROM FC_Items AS IB INNER JOIN T1_Serah AS B ON IB.ID = B.ItemID INNER JOIN FC_Lokasi AS LB ON B.LokID = LB.ID LEFT OUTER JOIN " +
                    "FC_Lokasi AS LA INNER JOIN BM_Destacking AS A ON LA.ID = A.LokasiID INNER JOIN FC_Items AS IA ON A.ItemID = IA.ID ON B.DestID = A.ID " +
                    "WHERE  B.status>-1 and LEFT(convert(varchar,A.TglProduksi,112),6) ='" + tgl1 + "' " +
                    "GROUP BY A.Qty, B.TglSerah, LA.Lokasi, IA.PartNo, IB.PartNo, LB.Lokasi, A.id_dstk, IA.Volume, IB.Volume, A.TglProduksi " +
                    "ORDER BY A.id_dstk";
            }
            return strSQL;
        }

        public string ViewPemetaanT1(string tgl1, string tgl2, int tgltype, string kriteria)
        {
            string strSQL;
            if (tgltype == 0)
            {
                strSQL = "select ID,id_dstk,FormulaCode as Jenis, TglProduksi,Group, PartNo, Lokasi, NoPAlet, JML_DEST, TglJemur, Rak, " +
                    "case when JML_DEST=0 and isnull(JML_JEMUR,0)>0 then 0 else  isnull(JML_JEMUR,0) end JML_JEMUR, TglSerah, " +
                    "isnull(JML_SERAH,0) as JML_SERAH ,JML_DEST-isnull(JML_SERAH,0) as Sisa from( " +
                    "SELECT   A.ID, A.id_dstk, A.TglProduksi, IA.PartNo, LA.Lokasi, P.NoPAlet, A.Qty AS JML_DEST, NULL AS TglJemur,null as Rak, NULL AS JML_JEMUR,  " +
                    "NULL AS TglSerah, NULL AS JML_SERAH, J.FormulaCode, G.[Group] AS Group " +
                    "FROM BM_PlantGroup AS G RIGHT OUTER JOIN FC_Lokasi AS LA RIGHT OUTER JOIN BM_Destacking AS A ON LA.ID = A.LokasiID ON G.ID = A.PlantGroupID LEFT OUTER JOIN " +
                    "BM_Formula AS J ON A.FormulaID = J.ID LEFT OUTER JOIN BM_Palet AS P ON A.PaletID = P.ID LEFT OUTER JOIN FC_Items AS IA ON A.ItemID = IA.ID " +
                    "WHERE    A.status=0 and  (A.RowStatus > - 1) AND  " +
                    "(CONVERT(varchar, A.TglProduksi, 112) >= '" + tgl1 + "') AND (CONVERT(varchar, A.TglProduksi, 112) <= '" + tgl2 + "') " + kriteria +
                    " union all " +
                    "SELECT A.ID, A.id_dstk, A.TglProduksi, IA.PartNo, LA.Lokasi, P.NoPAlet, A.Qty AS JML_DEST, B.TglJemur, FC_Rak.Rak, B.QtyIn  AS JML_JEMUR, NULL  " +
                    "AS TglSerah, NULL AS JML_SERAH, J.FormulaCode, G.[Group] AS Group " +
                    "FROM BM_PlantGroup AS G RIGHT OUTER JOIN T1_Jemur AS B INNER JOIN FC_Rak ON B.RakID = FC_Rak.ID RIGHT OUTER JOIN " +
                    "BM_Destacking AS A ON B.DestID = A.ID ON G.ID = A.PlantGroupID LEFT OUTER JOIN BM_Formula AS J ON A.FormulaID = J.ID RIGHT OUTER JOIN " +
                    "FC_Items AS IA ON A.ItemID = IA.ID LEFT OUTER JOIN BM_Palet AS P ON A.PaletID = P.ID RIGHT OUTER JOIN FC_Lokasi AS LA ON A.LokasiID = LA.ID " +
                    "WHERE  A.Status=1 and  (A.RowStatus >-1) and B.RowStatus>-1 AND  " +
                    "(CONVERT(varchar, A.TglProduksi, 112) >= '" + tgl1 + "') AND (CONVERT(varchar, A.TglProduksi, 112) <= '" + tgl2 + "') " + kriteria +
                    " union all " +
                    "SELECT C.ID,A.id_dstk, A.TglProduksi, IA.PartNo, LA.Lokasi, P.NoPAlet, " +
                    "case when C.ID >0 and (select COUNT(ItemID) from T1_Serah C1 where C1.DestID =C.DestID and C1.ID<C.ID and C1.status>-1)=0 then A.Qty else 0 end JML_DEST,  " +
                    "B.TglJemur, FC_Rak.Rak, B.QtyIn AS JML_JEMUR, C.TglSerah, C.QtyIn AS JML_SERAH, J.FormulaCode, G.[Group] as Group " +
                    "FROM BM_Palet AS P RIGHT OUTER JOIN BM_PlantGroup AS G RIGHT OUTER JOIN BM_Destacking AS A ON G.ID = A.PlantGroupID RIGHT OUTER JOIN " +
                    "FC_Items AS IA ON A.ItemID = IA.ID ON P.ID = A.PaletID RIGHT OUTER JOIN FC_Lokasi AS LA ON A.LokasiID = LA.ID LEFT OUTER JOIN " +
                    "BM_Formula AS J ON A.FormulaID = J.ID LEFT OUTER JOIN T1_Jemur AS B LEFT OUTER JOIN T1_Serah AS C ON B.DestID = C.DestID LEFT OUTER JOIN " +
                    "FC_Rak ON B.RakID = FC_Rak.ID ON A.ID = B.DestID WHERE A.Status=2 and C.status>-1 and (A.RowStatus >= 0) AND (B.RowStatus >= 0) AND (CONVERT(varchar, A.TglProduksi, 112) >= '" + tgl1 +
                    "') AND (CONVERT(varchar, A.TglProduksi, 112) <= '" + tgl2 + "')" + kriteria + ")  " +
                    "as peta where ISNULL(ID,0)<>0  ORDER BY peta.id_dstk,peta.ID";
            }
            else
            {
                strSQL = "SELECT A.id_dstk, A.TglProduksi, IA.PartNo, LA.Lokasi, P.NoPAlet,AVG(A.Qty)AS JML_DEST, B.TglJemur, FC_Rak.Rak, " +
                    "AVG(B.QtyIn) AS JML_JEMUR, C.TglSerah, SUM(C.QtyIn) AS JML_SERAH " +
                    "FROM BM_Palet AS P INNER JOIN FC_Lokasi AS LA INNER JOIN BM_Destacking AS A ON LA.ID = A.LokasiID INNER JOIN FC_Items AS IA ON A.ItemID = IA.ID ON " +
                    "P.ID = A.PaletID LEFT OUTER JOIN T1_Jemur AS B LEFT JOIN T1_Serah AS C ON B.DestID = C.DestID INNER JOIN FC_Rak ON B.RakID = FC_Rak.ID ON A.ID = B.DestID " +
                    "WHERE A.rowstatus>=0 and B.rowstatus>=0 and C.status>-1 and  LEFT(convert(varchar,A.TglProduksi,112),6) ='" + tgl1 + "' " + kriteria +
                    " GROUP BY C.TglSerah, LA.Lokasi, IA.PartNo, A.id_dstk, A.TglProduksi, P.NoPAlet, B.TglJemur, FC_Rak.Rak " +
                    "ORDER BY A.id_dstk";
            }
            return strSQL;
        }

        public string ViewT1SaldoPerLokasiPeta(string tgl1, string tgl2, int tgltype, string kriteria)
        {
            string strSQL = string.Empty;
            if (tgltype == 0)
            {
                strSQL = "select partno,lokasi,isnull(awal,0) as awal ,0 as AdjustIn,0 as AdjustOut,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran, " +
                     "isnull(awal,0)+sum(penerimaan)-sum(pengeluaran) as saldo from  ( " +
                     "select  thnbln,case when ItemID>0 then (select partno from FC_Items  where id=saldo2.ItemID )end Partno, " +
                     "case when ItemID>0 then (select Lokasi from FC_Lokasi where ID =saldo2.LokasiID )end Lokasi ,0 as awal, penerimaan,pengeluaran from ( " +
                     "select  thnbln, itemid, lokasiid, sum(penerimaan) as penerimaan,sum(pengeluaran) as pengeluaran from ( " +
                     "SELECT tanggal as thnbln, itemid, lokasiid, totqty AS penerimaan,0 as pengeluaran FROM ( " +
                     "SELECT TglProduksi AS tanggal, itemid, lokasiid, sum(qty) AS totqty FROM BM_Destacking where BM_Destacking.rowstatus>-1 GROUP BY TglProduksi, itemid, lokasiid) AS destacking " +
                     "UNION all " +
                     "SELECT tanggal as thnbln, itemid, lokasiid, 0 as penerimaan,sum(qty) as pengeluaran  FROM ( " +
                     "SELECT TglProduksi   AS tanggal, itemid, lokasiid, qty FROM (SELECT BM_Destacking.TglProduksi , T1_serah.DestID,  " +
                     "BM_Destacking.ItemID,BM_Destacking.lokasiID, (T1_serah.QtyIn ) AS qty  FROM T1_serah  ,BM_Destacking " +
                     "WHERE BM_Destacking.id=T1_serah.DestID and  BM_Destacking.rowstatus>-1 and  T1_serah.status>-1 ) AS jemur) AS jemur1 GROUP BY tanggal, itemid, lokasiid) as saldo1 " +
                     "group by thnbln, itemid, lokasiid) as saldo2 )as saldo3 where ISNULL(partno,'-')<>'-' and convert(varchar,thnbln,112)>='" +
                     tgl1 + "' and convert(varchar,thnbln,112)<='" + tgl2 + "' " + kriteria + "  group by partno,lokasi,AWAL order by lokasi,Partno";
            }
            return strSQL;
        }

        public string ViewT1SaldoPerLokasi(string tgl1, string tgl2, int tgltype, string kriteria)
        {
            string strSQL = string.Empty;
            if (tgltype == 0)
            {
                //strSQL = "select partno,lokasi,isnull(awal,0) as awal ,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran, " +
                //     "isnull(awal,0)+sum(penerimaan)-sum(pengeluaran) as saldo from  ( " +
                //     "select  thnbln,case when ItemID>0 then (select partno from FC_Items  where id=saldo2.ItemID )end Partno, " +
                //     "case when ItemID>0 then (select Lokasi from FC_Lokasi where ID =saldo2.LokasiID )end Lokasi ,0 as awal, penerimaan,pengeluaran from ( " +
                //     "select  thnbln, itemid, lokasiid, sum(penerimaan) as penerimaan,sum(pengeluaran) as pengeluaran from ( " +
                //     "SELECT tanggal as thnbln, itemid, lokasiid, totqty AS penerimaan,0 as pengeluaran FROM ( " +
                //     "SELECT TglProduksi AS tanggal, itemid, lokasiid, sum(qty) AS totqty FROM BM_Destacking where BM_Destacking.rowstatus>-1 GROUP BY TglProduksi, itemid, lokasiid) AS destacking " +
                //     "UNION all " +
                //     "SELECT tanggal as thnbln, itemid, lokasiid, 0 as penerimaan,sum(qty) as pengeluaran  FROM ( " +
                //     "SELECT TglProduksi   AS tanggal, itemid, lokasiid, qty FROM (SELECT BM_Destacking.TglProduksi , T1_Jemur.DestID,  " +
                //     "BM_Destacking.ItemID,BM_Destacking.lokasiID, (T1_Jemur.QtyOut ) AS qty  FROM T1_Jemur  ,BM_Destacking " +
                //     "WHERE BM_Destacking.id=T1_Jemur.DestID and  BM_Destacking.rowstatus>-1 and  T1_Jemur.status>-1 ) AS jemur) AS jemur1 GROUP BY tanggal, itemid, lokasiid) as saldo1 " +
                //     "group by thnbln, itemid, lokasiid) as saldo2 )as saldo3 where ISNULL(partno,'-')<>'-' and convert(varchar,thnbln,112)>='" +
                //     "20130601'  group by partno,lokasi,AWAL order by lokasi,Partno";
                ////tgl1 + "' and convert(varchar,thnbln,112)<='" + tgl2 + "' " + kriteria + "  group by partno,lokasi,AWAL order by lokasi,Partno";
                //strSQL = "select partno,lokasi,SUM(Qty) as penerimaan,SUM(QtySerah) as pengeluaran,SUM(Qty)-SUM(QtySerah) as saldo, SUM(QtySisaP99) as P99,SUM(QtySerah)+SUM(QtySisaP99)  from( " +
                //    "select B.PartNo,A.TglProduksi,C.Lokasi,A.Qty,A.ID,  " +
                //    "isnull((select SUM(QtyIn) from T1_Serah as a1 where a1.Status>-1 and a1.DestID=A.ID group by DestID),0) as QtySerah, " +
                //    "isnull((select SUM(QtyIn)-SUM(QtyOut) from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID),0) as QtySisaP99, " +
                //    "isnull((select SUM(QtyIn) from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID),0) as QtyP99 " +
                //    "from BM_Destacking as A, FC_Items as B, FC_Lokasi as C  " +
                //    "where A.ItemID=B.ID and A.LokasiID=C.ID and Convert(varchar,A.TglProduksi,112) >= '20130601' and  " +
                //    "Convert(varchar,A.TglProduksi,112) <= GETDATE() and A.Status>-1 and A.RowStatus>-1 ) test1 group by partno,lokasi";
                //strSQL = "select PartNo,Lokasi,Saldo,P99,QtyTest from ( " +
                //    "select PartNo,Lokasi,SUM(qty) as Saldo, SUM(QtySisaDariP99) as P99, SUM(QtyTest) as QtyTest " +
                //    "from (select PartNo,Lokasi,ID,case when QtySisaP99=0 then (Qty-QtySerah) else 0 end Qty," +
                //    "case when QtySisaP99>0 then (Qty-QtySerah) else 0 end QtySisaDariP99,Qty as QtyTest " +
                //    "from (select B.PartNo,A.TglProduksi,C.Lokasi,A.Qty,A.ID, " +
                //    "isnull((select SUM(QtyIn) from T1_Serah as a1 where a1.Status>-1 and a1.DestID=A.ID group by DestID),0) as QtySerah," +
                //    "isnull((select SUM(QtyIn)-SUM(QtyOut) from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID),0) as QtySisaP99," +
                //    "isnull((select SUM(QtyIn) from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID),0) as QtyP99 " +
                //    "from BM_Destacking as A, FC_Items as B, FC_Lokasi as C " +
                //    "where A.ItemID=B.ID and A.LokasiID=C.ID and Convert(varchar,A.TglProduksi,112) >= '20130601' and Convert(varchar,A.TglProduksi,112) <= '" + tgl2 + "' " +
                //    "and A.Status>-1 and A.RowStatus>-1 " +
                //    ") as b1 ) as c1 group by PartNo,Lokasi) as Test where (Saldo+P99)!=0";
                strSQL = "select PartNo,Lokasi,Saldo,P99,QtyTest from ( " +
                   "select PartNo,Lokasi,SUM(qty) as Saldo, SUM(QtySisaDariP99) as P99, SUM(QtyTest) as QtyTest " +
                   //"from (select PartNo,Lokasi,ID,case when Qty-QtySerah>0 then (Qty-QtySerah-QtySisaP99) else 0 end Qty," +
                   "from (select PartNo,Lokasi,ID,(Qty-QtySerah-QtySisaP99) as Qty," +
                   "case when QtySisaP99>0 then QtySisaP99 else 0 end QtySisaDariP99,Qty as QtyTest " +
                   "from (select B.PartNo,A.TglProduksi,C.Lokasi,A.Qty,A.ID, " +
                   "isnull((select SUM(QtyIn) from T1_Serah as a1 where a1.Status>-1 and a1.DestID=A.ID group by DestID),0) as QtySerah," +
                   "isnull((select sum(qty) from (select SUM(QtyIn-QtyOut) as qty from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID " +
   " union all select SUM(QtyIn-QtyOut) as qty  from T1_Serah as a3 where a3.Status>-1 and a3.DestID =A.ID " +
    "and a3.SFrom='lari' group by  a3.DestID) as lari),0) as QtySisaP99," +
                   "isnull((select SUM(QtyIn) from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID),0) as QtyP99 " +
                   "from BM_Destacking as A, FC_Items as B, FC_Lokasi as C " +
                   "where A.ItemID=B.ID and A.LokasiID=C.ID and Convert(varchar,A.TglProduksi,112) >= '20130601' and Convert(varchar,A.TglProduksi,112) <= '" + tgl2 + "' " +
                   "and A.Status>-1 and A.RowStatus>-1 " +
                   ") as b1 ) as c1 group by PartNo,Lokasi) as Test where (Saldo+P99)!=0";
            }
            return strSQL;
        }

        public string ViewT1StockPerLokasiDet(string thnbln1, string thnbln2, string process)
        {
            string strSQL = string.Empty;
            if (process == "curing")
                strSQL = "select  null as tgljemurlg,*,(select SUM(qtyin-qtyout) from T1_Jemur where DestID=A.destid)as sisajemur from (select destid, partno,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
                    "where convert(varchar,tglproduksi,112)>='20130601' and  convert(varchar,tglproduksi,112)<='" + thnbln2 + "' " +
                    "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid having sum(Qty)>0 ) as A  where destid not in (select destid from t1_serah_err) and isnull(tgljemur,'1/1/1900')='1/1/1900'";
            else
                strSQL = "select  null as tgljemurlg,*,(select SUM(qtyin-qtyout) from T1_Jemur where DestID=A.destid)as sisajemur from (select destid, partno,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
               "where convert(varchar,tglproduksi,112)>='20130601' and  convert(varchar,tglproduksi,112)<='" + thnbln2 + "' " +
               "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid having sum(Qty)>0 ) as A  where destid not in (select destid from t1_serah_err) and isnull(tgljemur,'1/1/1900')<>'1/1/1900'";
            return strSQL;
        }

        public string ViewT1StockPerLokasiTgl(string tanggal1, string tanggal2, string thnbln)
        {
            string strSQL = string.Empty;
            string thnblntrans = tanggal1.Substring(0, 6);
            //if (tgltype != 0)
            //{
            strSQL = "select (select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID=E.itemid)as volume, itemid,partno,lokasi,sum(awal) as awal,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from(  " +
                "select thnbln, itemid,lokid,(select partno from fc_items where id=D.itemid ) as partno,(select lokasi from fc_lokasi where id=D.lokid ) as lokasi,  " +
                "awal,penerimaan,pengeluaran,AdjustIn,AdjustOut from ( " +
                "select thnbln, itemid, lokid,isnull(SUM(awal),0) as awal, isnull(sum(penerimaan),0) as penerimaan,isnull(sum(pengeluaran),0)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut  from (  " +
                "select thnbln, itemid,lokid,Saldo + (select isnull(SUM(qty),0) from vw_KartustockWIP where itemid0=T1S.ItemID and lokid=T1S.lokid and " +
                "LEFT(convert(varchar,tanggal,112),6)='" + thnblntrans + "' and convert(varchar,tanggal,112)<'" + tanggal1 +
                "')as awal,0 as penerimaan,0 as pengeluaran,0 as AdjustIn,0 as AdjustOut from T1_SaldoPerLokasi T1S where rowstatus>-1 and thnbln='" + thnbln + "' " +
                "union   All " +
                "select thnbln, itemid, lokid,0 as awal, sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut  from (  " +
                "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,isnull(qty,0) as penerimaan,0 as pengeluaran, " +
                "0 as AdjustIn,0 as AdjustOut from vw_KartustockWIP where idrec not like '%in' and qty >0 and convert(varchar,tanggal,112)>='" + tanggal1 + "'and convert(varchar,tanggal,112)<='" + tanggal2 + "') as A  " +
                "group by thnbln, itemid, lokid  " +
                "union  All " +
                "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid, 0 as penerimaan,0 as pengeluaran, " +
                "isnull(qty,0) as AdjustIn,0 as AdjustOut from vw_KartustockWIP where idrec like '%in' and qty >0 and convert(varchar,tanggal,112)>='" + tanggal1 + "'and convert(varchar,tanggal,112)<='" + tanggal2 + "') as A  " +
                "group by thnbln, itemid, lokid  " +
                "union  All " +
                "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,0 as pengeluaran,0 as AdjustIn,qty * -1 as AdjustOut  from vw_KartustockWIP   " +
                "where  idrec  like '%ou' and qty <0 and convert(varchar,tanggal,112)>='" + tanggal1 + "'and convert(varchar,tanggal,112)<='" + tanggal2 + "') as A group by thnbln, itemid, lokid " +
                "union All " +
                "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,qty * -1 as pengeluaran,0 as AdjustIn,0 as AdjustOut  from vw_KartustockWIP   " +
                "where  idrec not like '%ou' and qty <0 and convert(varchar,tanggal,112)>='" + tanggal1 + "'and convert(varchar,tanggal,112)<='" + tanggal2 + "') as A group by thnbln, itemid, lokid " +
                ") as B group by thnbln, itemid, lokid  " +
                ") as C group by thnbln, itemid, lokid) as D)as E  " +
                "where awal+penerimaan+pengeluaran+AdjustIn +AdjustOut <>0 and lokasi <>'p99' and itemid in " +
                "(select distinct itemid from bm_destacking where tglproduksi>='6/1/2013')group by itemid,partno,lokasi order by partno,lokasi";
            //}
            return strSQL;
        }

        public string ViewT1StockPerLokasi(string thnbln1, string thnbln2, int tgltype)
        {
            string strSQL = string.Empty;
            if (tgltype != 0)
            {
                int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
                string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
                if (Convert.ToInt32(thnbln2) <= Convert.ToInt32(periode))
                    strSQL = "select (select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID=E.itemid)as volume, itemid,partno,lokasi,sum(awal) as awal,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,0 as inlari, 0 as outlari,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from(  " +
                        "select thnbln, itemid,lokid,(select partno from fc_items where id=D.itemid ) as partno,(select lokasi from fc_lokasi where id=D.lokid ) as lokasi,  " +
                        "awal,penerimaan,pengeluaran,AdjustIn,AdjustOut from ( " +
                        "select thnbln, itemid, lokid,isnull(SUM(awal),0) as awal, isnull(sum(penerimaan),0) as penerimaan,isnull(sum(pengeluaran),0)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut  from (  " +
                        "select thnbln, itemid,lokid,Saldo as awal,0 as penerimaan,0 as pengeluaran,0 as AdjustIn,0 as AdjustOut from T1_SaldoPerLokasi where rowstatus>-1 and thnbln='" + thnbln1 + "' " +
                        "union   All " +
                        "select thnbln, itemid, lokid,0 as awal, sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut  from (  " +
                        "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,isnull(qty,0) as penerimaan,0 as pengeluaran, " +
                        "0 as AdjustIn,0 as AdjustOut from vw_KartustockWIPOld where idrec not like '%in' and qty >0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A  " +
                        "group by thnbln, itemid, lokid  " +
                        "union  All " +
                        "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid, 0 as penerimaan,0 as pengeluaran, " +
                        "isnull(qty,0) as AdjustIn,0 as AdjustOut from vw_KartustockWIPOld where idrec like '%in' and qty >0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A  " +
                        "group by thnbln, itemid, lokid  " +
                        "union  All " +
                        //"select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        //"select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid, " +
                        //"isnull(qty,0) as penerimaan,0 as pengeluaran,0 as AdjustIn,0 as AdjustOut from vw_KartustockWIP2   " +
                        //"where  idrec not like '%in' and qty >0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid  " +
                        //"union  All " +
                        //"select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        //"select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid,  lokid, " +
                        //"0 as penerimaan,0 as pengeluaran,isnull(qty,0) as AdjustIn,0 as AdjustOut from vw_KartustockWIP2   " +
                        //"where  idrec like '%in' and qty >0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid  " +
                        //"union  All " +
                        //"select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        //"select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid, " +
                        //"0 as penerimaan,isnull(qty*-1,0) as pengeluaran,0 as AdjustIn,0 as AdjustOut  from vw_KartustockWIP2   " +
                        //"where  idrec not like '%out' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid  " +
                        //"union  All " +
                        //"select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        //"select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid, " +
                        //"0 as penerimaan,0 as pengeluaran,0 as AdjustIn,isnull(qty*-1,0) as AdjustOut  from vw_KartustockWIP2   " +
                        //"where  idrec like '%out' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid  " +
                        //"union  All " +
                        "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,0 as pengeluaran,0 as AdjustIn,qty * -1 as AdjustOut  from vw_KartustockWIPOld   " +
                        "where  idrec  like '%ou%' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid " +
                        "union All " +
                        "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (  " +
                        "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,qty * -1 as pengeluaran,0 as AdjustIn,0 as AdjustOut  from vw_KartustockWIPOld   " +
                        "where  idrec not like '%ou%' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid " +
                        ") as B group by thnbln, itemid, lokid  " +
                        ") as C group by thnbln, itemid, lokid) as D)as E  " +
                        "where awal+penerimaan+pengeluaran+AdjustIn +AdjustOut <>0 " +
                        "and lokasi <>'p99' " +
                        "and itemid in " +
                        "(select distinct itemid from bm_destacking where tglproduksi>='6/1/2013')group by itemid,partno,lokasi order by partno,lokasi";
                else
                    strSQL = "select (select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID=E.itemid)as volume, itemid,partno,lokasi,sum(awal) as awal, " +
                    "sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from(   " +
                    "select thnbln, itemid,lokid,(select partno from fc_items where id=D.itemid ) as partno,(select lokasi from fc_lokasi where id=D.lokid ) as lokasi,   " +
                    "awal,penerimaan,pengeluaran,inlari,outlari,AdjustIn,AdjustOut from (  " +
                    "select thnbln, itemid, lokid,isnull(SUM(awal),0) as awal, isnull(sum(penerimaan),0) as penerimaan,isnull(sum(pengeluaran),0)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut  from (   " +
                    "select thnbln, itemid,lokid,Saldo as awal,0 as penerimaan,0 as pengeluaran,0 as inlari,0 as outlari,0 as AdjustIn,0 as AdjustOut from T1_SaldoPerLokasi where rowstatus>-1 and thnbln='" + thnbln1 + "'  " +
                    "union   All  " +
                    "select thnbln, itemid, lokid,0 as awal, sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut  from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,isnull(qty,0) as penerimaan,0 as pengeluaran, 0 as inlari,0 as outlari, " +
                    "0 as AdjustIn,0 as AdjustOut from vw_KartustockWIP where idrec not like '%adj%' and qty >0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A   " +
                    "group by thnbln, itemid, lokid   " +
                    "union  All  " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid, 0 as penerimaan,0 as pengeluaran,0 as inlari,0 as outlari,  " +
                    "isnull(qty,0) as AdjustIn,0 as AdjustOut from vw_KartustockWIP where idrec like '%in%' and qty >0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A   " +
                    "group by thnbln, itemid, lokid   " +
                    "union  All  " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,0 as pengeluaran,0 as inlari,0 as outlari,0 as AdjustIn,qty * -1 as AdjustOut  from vw_KartustockWIP    " +
                    "where destid not in(select destid from T1_AdjustDetail where lokid in(select ID from FC_Lokasi where Lokasi='p99')) and idrec  like '%ou%' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid  " +
                    "union All  " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,qty * -1 as pengeluaran,0 as inlari,0 as outlari,0 as AdjustIn,0 as AdjustOut  from vw_KartustockWIP    " +
                    "where  idrec not like '%adj%' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "' and process<>'lari') as A group by thnbln, itemid, lokid  " +
                    "union All  " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,0 as pengeluaran,0 as inlari,qty * -1 as outlari,0 as AdjustIn,0 as AdjustOut  from vw_KartustockWIP    " +
                    "where  idrec not like '%adj%' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "' and process='lari') as A group by thnbln, itemid, lokid  " +
                    //"--stock pelarian "+
                    "union  All  " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid, 0 as penerimaan,0 as pengeluaran,isnull(qty,0) as inlari,0 as outlari,  " +
                    "0 as AdjustIn,0 as AdjustOut from vw_KartustockWIP2 where qty >0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A   " +
                    "group by thnbln, itemid, lokid   " +
                    "union  All  " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,qty * -1  as pengeluaran,0 as inlari,0 as outlari,0 as AdjustIn,0 as AdjustOut  from vw_KartustockWIP2    " +
                    "where idrec not like '%adj%' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid  " +
                    "union  All  " +
                    "select thnbln, itemid, lokid,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran,sum(inlari) as inlari, sum(outlari) as outlari, sum(AdjustIn) as AdjustIn,sum(AdjustOut) as AdjustOut from (   " +
                    "select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, lokid,0 as penerimaan,0  as pengeluaran,0 as inlari,0 as outlari,0 as AdjustIn,qty * -1 as AdjustOut  from vw_KartustockWIP2    " +
                    "where  destid in(select destid from T1_AdjustDetail where lokid in(select ID from FC_Lokasi where Lokasi='p99')) and idrec  like '%ou%' and qty <0 and LEFT(convert(varchar,tanggal,112),6)='" + thnbln2 + "') as A group by thnbln, itemid, lokid  " +

                    ") as B group by thnbln, itemid, lokid  " +
                    ") as C group by thnbln, itemid, lokid) as D)as E   " +
                    "where awal+penerimaan+pengeluaran+AdjustIn +inlari+outlari+AdjustOut <>0  " +
                    //"--and lokasi <>'p99'  "+
                    "and itemid in (select distinct ItemID from (select distinct itemid from bm_destacking where tglproduksi>='6/1/2013'  " +
                    "union all select distinct itemid0 as ItemID from T1_JemurLg where tgljemur>='6/1/2013') as I ) " +
                    "group by itemid,partno,lokasi order by lokasi,partno ";
            }
            return strSQL;
        }

        public string ViewSaldoPerUkuran(string Periode, string tglawal, string tglakhir, string awal)
        {
            string strSQL = string.Empty;
            int tahun = 0;
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                strSQL = "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                        "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                        "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%'  OR B.PartNo  like '%-M-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                        "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                        "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                        "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID ,JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                        "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                        ") as saldo1 where QTY>0 union all " +
                        "select 'BP' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                        "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *,  Awal*volume as AwalKubikasi,qty*volume as Kubikasi from ( " +
                        "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                        "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                        "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                        "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                        "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-P-%' ) group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                        ") as saldo1 where QTY>0 ";
            else
                strSQL = "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                    ") as saldo1 where QTY>0 union all " +
                    "select 'BP' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *,  Awal*volume as AwalKubikasi,qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-P-%' ) group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                    ") as saldo1 where QTY>0 ";
            return strSQL;
        }

        public string ViewTotalSaldoPerUkuranBP(string Periode, string tglawal, string tglakhir, string awal)
        {
            string strSQL = string.Empty;
            int tahun = 0;
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                strSQL = "select sum(awal) as saldoawalbp,sum(awalkubikasi) as saldoawalbpkubik,0 as saldoawalok,0 as saldoawalokkubik,sum(qty)  as saldobp,sum(kubikasi)  as saldobpkubik,0 as saldook,sum(kubikasi) as saldookkubik from (" +
                        "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                        "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                        "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                        "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                        "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                        "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                        "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                        ") as saldo1 where QTY>0 union all " +
                        "select 'BP' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                        "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *,  Awal*volume as AwalKubikasi,qty*volume as Kubikasi from ( " +
                        "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                        "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                        "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                        "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                        "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-P-%' ) group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                        ") as saldo1 where QTY>0) as total where kwalitas='bp'";
            else
                strSQL = "select sum(awal) as saldoawalbp,sum(awalkubikasi) as saldoawalbpkubik,0 as saldoawalok,0 as saldoawalokkubik,sum(qty)  as saldobp,sum(kubikasi)  as saldobpkubik,0 as saldook,sum(kubikasi) as saldookkubik from (" +
                    "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                    ") as saldo1 where QTY>0 union all " +
                    "select 'BP' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *,  Awal*volume as AwalKubikasi,qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-P-%' ) group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                    ") as saldo1 where QTY>0) as total where kwalitas='bp'";
            return strSQL;
        }

        public string ViewTotalSaldoPerUkuranOK(string Periode, string tglawal, string tglakhir, string awal)
        {
            string strSQL = string.Empty;
            int tahun = 0;
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                strSQL = "select 0 as saldoawalbp,0 as saldoawalbpkubik,sum(awal) as saldoawalok,sum(awalkubikasi) as saldoawalokkubik,0 as saldobp,0 as saldobpkubik,sum(qty) as saldook,sum(kubikasi) as saldookkubik from (" +
                        "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                        "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                        "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                        "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                        "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                        "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                        "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                        ") as saldo1 where QTY>0 union all " +
                        "select 'BP' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                        "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *,  Awal*volume as AwalKubikasi,qty*volume as Kubikasi from ( " +
                        "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                        "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                        "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                        "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                        "union all " +
                        "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                        "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                        "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-P-%' ) group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                        ") as saldo1 where QTY>0) as total where kwalitas='OK'";
            else
                strSQL = "select 0 as saldoawalbp,0 as saldoawalbpkubik,sum(awal) as saldoawalok,sum(awalkubikasi) as saldoawalokkubik,0 as saldobp,0 as saldobpkubik,sum(qty) as saldook,sum(kubikasi) as saldookkubik from (" +
                    "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%' OR B.PartNo  like '%-M-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                    ") as saldo1 where QTY>0 union all " +
                    "select 'BP' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *,  Awal*volume as AwalKubikasi,qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJNew A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-P-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID , JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-P-%' ) group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                    ") as saldo1 where QTY>0) as total where kwalitas='OK'";
            return strSQL;
        }

        public string ViewKartuStockPerUkuranOK(string Periode0, string Periode1, string ukuran, string items)
        {
            string strSQL = string.Empty;
            string kriteria = string.Empty;
            if (items.ToUpper() == "ALL ITEMS")
                kriteria = " Where ukuran='" + ukuran + "' ";
            else
                kriteria = " Where ukuran='" + ukuran + "' and items='" + items + "' ";
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode0) <= Convert.ToInt32(periode))
                strSQL = "create table #adminksOKUkuran(id varchar(100),Tanggal datetime,items varchar(25),qty int) " +
                    "declare @id varchar(100) " +
                    "declare @Tanggal datetime " +
                    "declare @items varchar(100) " +
                    "declare @qty int " +
                    "declare kursor cursor for " +
                    "select id,Tanggal,items,qty from vw_T3KartustockOKByUkuran " +
                    "where LEFT(CONVERT(varchar, Tanggal , 112), 6) = '" + Periode1 + "' " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @ID,@Tanggal,@items,@qty " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                "    insert into #adminksOKUkuran(ID,Tanggal,items,qty)values(@ID,@Tanggal,@items,@qty) " +
                "    FETCH NEXT FROM kursor " +
                "    INTO @ID,@Tanggal,@items,@qty " +
                    "END " +
                    "CLOSE kursor " +
                    "DEALLOCATE kursor " +
                    "select ID, tanggal, ukuran, items,Keterangan,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo from ( " +
                    "select A.Periode as ID, CAST('201402'+'01' as datetime) as tanggal, ukuran, items,'Saldo Awal' as Keterangan,qty as awal, " +
                    "0 as Penerimaan,0 as pengeluaran from vw_T3SaldoAwalOK A  where A.Qty>0 and A.Periode= '" + Periode0 + "' " +
                    "union " +
                    "select ID, tanggal, ukuran, items,Keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "    select qty  from vw_T3SaldoAwalOK A  where A.Items=ain1.Items and A.Periode= '" + Periode0 + "' " +
                "    union " +
                "    SELECT isnull(SUM(qty),0) as qty from #adminksOKUkuran where items=ain1.items and ID<ain1.ID)as ain0) end  awal,  " +
                    "Penerimaan,pengeluaran from ( " +
                    "SELECT ID, tanggal,  ukuran, items,Keterangan,Qty as Penerimaan,0 as Pengeluaran FROM vw_T3KartustockOKByUkuran " +
                    "where Qty >0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "' " +
                    "union  " +
                    "SELECT ID, tanggal, ukuran, items,Keterangan,0 as Penerimaan,Qty *-1 as Pengeluaran FROM vw_T3KartustockOKByUkuran " +
                    "where Qty <0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "') as ain1 ) as atALL " + kriteria + " order by ukuran, items,ID " +
                    "drop table #adminksOKUkuran";
            else
                strSQL = "create table #adminksOKUkuran(id varchar(100),Tanggal datetime,items varchar(25),qty int) " +
                "declare @id varchar(100) " +
                "declare @Tanggal datetime " +
                "declare @items varchar(100) " +
                "declare @qty int " +
                "declare kursor cursor for " +
                "select id,Tanggal,items,qty from vw_T3KartustockOKByUkuranNew " +
                "where LEFT(CONVERT(varchar, Tanggal , 112), 6) = '" + Periode1 + "' " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @ID,@Tanggal,@items,@qty " +
                "WHILE @@FETCH_STATUS = 0 " +
                "begin " +
                "    insert into #adminksOKUkuran(ID,Tanggal,items,qty)values(@ID,@Tanggal,@items,@qty) " +
                "    FETCH NEXT FROM kursor " +
                "    INTO @ID,@Tanggal,@items,@qty " +
                "END " +
                "CLOSE kursor " +
                "DEALLOCATE kursor " +
                "select ID, tanggal, ukuran, items,Keterangan,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo from ( " +
                "select A.Periode as ID, CAST('201402'+'01' as datetime) as tanggal, ukuran, items,'Saldo Awal' as Keterangan,qty as awal, " +
                "0 as Penerimaan,0 as pengeluaran from vw_T3SaldoAwalOK A  where A.Qty>0 and A.Periode= '" + Periode0 + "' " +
                "union " +
                "select ID, tanggal, ukuran, items,Keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                "    select qty  from vw_T3SaldoAwalOK A  where A.Items=ain1.Items and A.Periode= '" + Periode0 + "' " +
                "    union " +
                "    SELECT isnull(SUM(qty),0) as qty from #adminksOKUkuran where items=ain1.items and ID<ain1.ID)as ain0) end  awal,  " +
                "Penerimaan,pengeluaran from ( " +
                "SELECT ID, tanggal,  ukuran, items,Keterangan,Qty as Penerimaan,0 as Pengeluaran FROM vw_T3KartustockOKByUkuranNew " +
                "where Qty >0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "' " +
                "union  " +
                "SELECT ID, tanggal, ukuran, items,Keterangan,0 as Penerimaan,Qty *-1 as Pengeluaran FROM vw_T3KartustockOKByUkuranNew " +
                "where Qty <0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "') as ain1 ) as atALL " + kriteria + " order by ukuran, items,ID " +
                "drop table #adminksOKUkuran";
            return strSQL;
        }

        public string ViewKartuStockPerUkuranKW(string Periode0, string Periode1, string ukuran, string items)
        {
            string strSQL = string.Empty;
            string kriteria = string.Empty;
            if (items.ToUpper() == "ALL ITEMS")
                kriteria = " Where ukuran='" + ukuran + "' ";
            else
                kriteria = " Where ukuran='" + ukuran + "' and items='" + items + "' ";
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode0) <= Convert.ToInt32(periode))
                strSQL = "create table #adminksKWUkuran(id varchar(100),Tanggal datetime,items varchar(25),qty int) " +
                    "declare @id varchar(100) " +
                    "declare @Tanggal datetime " +
                    "declare @items varchar(100) " +
                    "declare @qty int " +
                    "declare kursor cursor for " +
                    "select id,Tanggal,items,qty from vw_T3KartustockKWByUkuran " +
                    "where LEFT(CONVERT(varchar, Tanggal , 112), 6) = '" + Periode1 + "' " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @ID,@Tanggal,@items,@qty " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                    "    insert into #adminksKWUkuran(ID,Tanggal,items,qty)values(@ID,@Tanggal,@items,@qty) " +
                    "    FETCH NEXT FROM kursor " +
                    "    INTO @ID,@Tanggal,@items,@qty " +
                    "END " +
                    "CLOSE kursor " +
                    "DEALLOCATE kursor " +
                    "select ID, tanggal, ukuran, items,Keterangan,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo from ( " +
                    "select A.Periode as ID, CAST('201402'+'01' as datetime) as tanggal, ukuran, items,'Saldo Awal' as Keterangan,qty as awal, " +
                    "0 as Penerimaan,0 as pengeluaran from vw_T3SaldoAwalKW A  where A.Qty>0 and A.Periode= '" + Periode0 + "' " +
                    "union " +
                    "select ID, tanggal, ukuran, items,Keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "    select qty  from vw_T3SaldoAwalKW A  where A.Items=ain1.Items and A.Periode= '" + Periode0 + "' " +
                    "    union " +
                    "    SELECT isnull(SUM(qty),0) as qty from #adminksKWUkuran where items=ain1.items and ID<ain1.ID)as ain0) end  awal,  " +
                    "Penerimaan,pengeluaran from ( " +
                    "SELECT ID, tanggal,  ukuran, items,Keterangan,Qty as Penerimaan,0 as Pengeluaran FROM vw_T3KartustockKWByUkuran " +
                    "where Qty >0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "' " +
                    "union  " +
                    "SELECT ID, tanggal, ukuran, items,Keterangan,0 as Penerimaan,Qty *-1 as Pengeluaran FROM vw_T3KartustockKWByUkuran " +
                    "where Qty <0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "') as ain1 ) as atALL " + kriteria + " order by ukuran, items,ID " +
                    "drop table #adminksKWUkuran";
            else
                strSQL = "create table #adminksKWUkuran(id varchar(100),Tanggal datetime,items varchar(25),qty int) " +
                "declare @id varchar(100) " +
                "declare @Tanggal datetime " +
                "declare @items varchar(100) " +
                "declare @qty int " +
                "declare kursor cursor for " +
                "select id,Tanggal,items,qty from vw_T3KartustockKWByUkuranNew " +
                "where LEFT(CONVERT(varchar, Tanggal , 112), 6) = '" + Periode1 + "' " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @ID,@Tanggal,@items,@qty " +
                "WHILE @@FETCH_STATUS = 0 " +
                "begin " +
                "    insert into #adminksKWUkuran(ID,Tanggal,items,qty)values(@ID,@Tanggal,@items,@qty) " +
                "    FETCH NEXT FROM kursor " +
                "    INTO @ID,@Tanggal,@items,@qty " +
                "END " +
                "CLOSE kursor " +
                "DEALLOCATE kursor " +
                "select ID, tanggal, ukuran, items,Keterangan,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo from ( " +
                "select A.Periode as ID, CAST('201402'+'01' as datetime) as tanggal, ukuran, items,'Saldo Awal' as Keterangan,qty as awal, " +
                "0 as Penerimaan,0 as pengeluaran from vw_T3SaldoAwalKW A  where A.Qty>0 and A.Periode= '" + Periode0 + "' " +
                "union " +
                "select ID, tanggal, ukuran, items,Keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                "    select qty  from vw_T3SaldoAwalKW A  where A.Items=ain1.Items and A.Periode= '" + Periode0 + "' " +
                "    union " +
                "    SELECT isnull(SUM(qty),0) as qty from #adminksKWUkuran where items=ain1.items and ID<ain1.ID)as ain0) end  awal,  " +
                "Penerimaan,pengeluaran from ( " +
                "SELECT ID, tanggal,  ukuran, items,Keterangan,Qty as Penerimaan,0 as Pengeluaran FROM vw_T3KartustockKWByUkuranNew " +
                "where Qty >0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "' " +
                "union  " +
                "SELECT ID, tanggal, ukuran, items,Keterangan,0 as Penerimaan,Qty *-1 as Pengeluaran FROM vw_T3KartustockKWByUkuranNew " +
                "where Qty <0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "') as ain1 ) as atALL " + kriteria + " order by ukuran, items,ID " +
                "drop table #adminksKWUkuran";
            return strSQL;
        }

        public string ViewKartuStockPerUkuranBP(string Periode0, string Periode1, string ukuran, string items)
        {
            string strSQL = string.Empty;
            string kriteria = string.Empty;
            if (items.ToUpper() == "ALL ITEMS")
                kriteria = " Where ukuran='" + ukuran + "' ";
            else
                kriteria = " Where ukuran='" + ukuran + "' and items='" + items + "' ";
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode0) <= Convert.ToInt32(periode))
                strSQL = "create table #adminksBPUkuran(id varchar(100),Tanggal datetime,items varchar(25),qty int) " +
                    "declare @id varchar(100) " +
                    "declare @Tanggal datetime " +
                    "declare @items varchar(100) " +
                    "declare @qty int " +
                    "declare kursor cursor for " +
                    "select id,Tanggal,items,qty from vw_T3KartustockBPByUkuran " +
                    "where LEFT(CONVERT(varchar, Tanggal , 112), 6) = '" + Periode1 + "' " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @ID,@Tanggal,@items,@qty " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                    "    insert into #adminksBPUkuran(ID,Tanggal,items,qty)values(@ID,@Tanggal,@items,@qty) " +
                    "    FETCH NEXT FROM kursor " +
                    "    INTO @ID,@Tanggal,@items,@qty " +
                    "END " +
                    "CLOSE kursor " +
                    "DEALLOCATE kursor " +
                    "select ID, tanggal, ukuran, items,Keterangan,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo from ( " +
                    "select A.Periode as ID, CAST('201402'+'01' as datetime) as tanggal, ukuran, items,'Saldo Awal' as Keterangan,qty as awal, " +
                    "0 as Penerimaan,0 as pengeluaran from vw_T3SaldoAwalBP A  where A.Qty>0 and A.Periode= '" + Periode0 + "' " +
                    "union " +
                    "select ID, tanggal, ukuran, items,Keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "    select qty  from vw_T3SaldoAwalBP A  where A.Items=ain1.Items and A.Periode= '" + Periode0 + "' " +
                    "    union " +
                    "    SELECT isnull(SUM(qty),0) as qty from #adminksBPUkuran where items=ain1.items and ID<ain1.ID)as ain0) end  awal,  " +
                    "Penerimaan,pengeluaran from ( " +
                    "SELECT ID, tanggal,  ukuran, items,Keterangan,Qty as Penerimaan,0 as Pengeluaran FROM vw_T3KartustockBPByUkuran " +
                    "where Qty >0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "' " +
                    "union  " +
                    "SELECT ID, tanggal, ukuran, items,Keterangan,0 as Penerimaan,Qty *-1 as Pengeluaran FROM vw_T3KartustockBPByUkuran " +
                    "where Qty <0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "') as ain1 ) as atALL " + kriteria + " order by ukuran, items,ID " +
                    "drop table #adminksBPUkuran";
            else
                strSQL = "create table #adminksBPUkuran(id varchar(100),Tanggal datetime,items varchar(25),qty int) " +
                "declare @id varchar(100) " +
                "declare @Tanggal datetime " +
                "declare @items varchar(100) " +
                "declare @qty int " +
                "declare kursor cursor for " +
                "select id,Tanggal,items,qty from vw_T3KartustockBPByUkuranNew " +
                "where LEFT(CONVERT(varchar, Tanggal , 112), 6) = '" + Periode1 + "' " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @ID,@Tanggal,@items,@qty " +
                "WHILE @@FETCH_STATUS = 0 " +
                "begin " +
                "    insert into #adminksBPUkuran(ID,Tanggal,items,qty)values(@ID,@Tanggal,@items,@qty) " +
                "    FETCH NEXT FROM kursor " +
                "    INTO @ID,@Tanggal,@items,@qty " +
                "END " +
                "CLOSE kursor " +
                "DEALLOCATE kursor " +
                "select ID, tanggal, ukuran, items,Keterangan,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo from ( " +
                "select A.Periode as ID, CAST('201402'+'01' as datetime) as tanggal, ukuran, items,'Saldo Awal' as Keterangan,qty as awal, " +
                "0 as Penerimaan,0 as pengeluaran from vw_T3SaldoAwalBP A  where A.Qty>0 and A.Periode= '" + Periode0 + "' " +
                "union " +
                "select ID, tanggal, ukuran, items,Keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                "    select qty  from vw_T3SaldoAwalBP A  where A.Items=ain1.Items and A.Periode= '" + Periode0 + "' " +
                "    union " +
                "    SELECT isnull(SUM(qty),0) as qty from #adminksBPUkuran where items=ain1.items and ID<ain1.ID)as ain0) end  awal,  " +
                "Penerimaan,pengeluaran from ( " +
                "SELECT ID, tanggal,  ukuran, items,Keterangan,Qty as Penerimaan,0 as Pengeluaran FROM vw_T3KartustockBPByUkuranNew " +
                "where Qty >0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "' " +
                "union  " +
                "SELECT ID, tanggal, ukuran, items,Keterangan,0 as Penerimaan,Qty *-1 as Pengeluaran FROM vw_T3KartustockBPByUkuranNew " +
                "where Qty <0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "') as ain1 ) as atALL " + kriteria + " order by ukuran, items,ID " +
                "drop table #adminksBPUkuran";
            return strSQL;
        }

        public string ViewLKirim(string tgl1, string tgl2, int tgltype, string partno, string tglmundur, string tglmaju)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            if (tgltype == 0)
                strTgl = " WHERE convert(varchar,AA.CreatedTime,112)>='" + tgl1 + "' and convert(varchar,AA.CreatedTime,112)<='" + tgl2 + "' ";
            else
                strTgl = " WHERE convert(varchar,AA.TglKirim,112)>='" + tgl1 + "' and convert(varchar,AA.TglKirim,112)<='" + tgl2 + "' ";
            //strSQL = " SELECT A.CreatedTime, A.TglKirim, A.Customer, A.SJNo, C.PartNo, B.Qty FROM T3_Kirim AS A INNER JOIN " +
            //           "T3_KirimDetail AS B ON A.ID = B.KirimID INNER JOIN FC_Items AS C ON B.ItemID = C.ID " + strTgl + partno ;

            //strSQL = "SELECT AA.KirimID,AA.CreatedTime ,AA.TglKirim,null  tglSJ,AA.Customer, AA.SJNo, C.PartNo, sum(AA.Qty) as Qty, ((AA.tebal/1000)*sum(AA.Qty) ) as Meter,D.Groups, AA.Cust " +
            //         "FROM (select (select tebal from fc_items where ID=b.itemid) as tebal,a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo,a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty, " +
            //         "case when SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' then 'CPD' else 'EKS' end Cust " +
            //         "from T3_Kirim as a,T3_KirimDetail as b where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 and b.RowStatus>-1 and b.Status>-1 ) as AA  " +
            //         "INNER JOIN FC_Items AS C ON AA.ItemID = C.ID " +
            //         "left JOIN T3_GroupM as D ON D.ID = C.GroupID " + strTgl + partno + " group by AA.KirimID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo,D.Groups, AA.Cust,AA.tebal ";

            strSQL =
            " WITH DataAwal AS ( SELECT AA.KirimID,AA.CreatedTime ,AA.TglKirim,null  tglSJ,AA.Customer, AA.SJNo, C.PartNo, sum(AA.Qty) as Qty,  " +
            " ((AA.tebal/1000)*sum(AA.Qty) ) as Meter,D.Groups, AA.Cust  FROM (select (select tebal from fc_items where ID=b.itemid) as tebal," +
            " a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo, a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty,  case when " +
            " SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' then 'CPD' else 'EKS' end Cust  from T3_Kirim as a," +
            " T3_KirimDetail as b where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 and b.RowStatus>-1 and b.Status>-1 ) as AA    " +
            " INNER JOIN FC_Items AS C ON AA.ItemID = C.ID  left JOIN T3_GroupM as D ON D.ID = C.GroupID  " +
            " " + strTgl + partno + "" +
            " group by AA.KirimID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo,D.Groups, AA.Cust,AA.tebal), " +

            " Data2 AS (select suratjalanno,keterangan,BB.Alamatlain from [sql1.grcboard.com].GRCboard.dbo.suratjalan AA LEFT JOIN " +
            " [sql1.grcboard.com].GRCboard.dbo.OP BB ON AA.OPID=BB.ID where convert(varchar,PostingReceiveDate,112)>='" + tglmundur + "' and " +
            " convert(varchar,PostingReceiveDate,112)<='" + tglmaju + "' and AA.status>-1 and BB.status>-1 " +

            " union all " +

            " select suratjalanno,''keterangan,DD.ToDepoName Alamatlain from [sql1.grcboard.com].GRCboard.dbo.suratjalanTO CC " +
            " LEFT JOIN [sql1.grcboard.com].GRCboard.dbo.TransferOrder DD ON CC.TransferOrderID=DD.ID " +
            " where convert(varchar,CC.CreatedTime,112)>='" + tglmundur + "' and convert(varchar,CC.CreatedTime,112)<='" + tglmaju + "' " +
            " and CC.status>-1 and DD.status>-1 and DD.rowstatus>-1 ) " +

            " select A.*,ISNULL(B.keterangan,'')Keterangan,ISNULL(B.AlamatLain,'')Alamat from DataAwal A LEFT JOIN Data2 B ON A.SJNo=B.suratjalanno ";

            return strSQL;
        }

        public string ViewMutasiBJDet(string PartNO, string Periode, string awal)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total, " +
                "case when ItemID>0 then (select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "' and convert(varchar,tanggal  ,112)<B.HMY  " +
                "and ItemID=B.ItemID )+(select isnull(sum(" + awal + "),0) from SaldoInventoryBJ where rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + " ) end Awal from (select *, " +
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process='direct' and process not like 'ex%') end TWIP, " +
                "case when ItemID >0 then (select isnull(avg(HPP),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process='direct' and process not like 'ex%') end HTWIP, " +
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and SUBSTRING(Keterangan,5,1)!='3' and SUBSTRING(Keterangan ,5,1)!='W') end TBP, " +
                "case when ItemID >0 then (select isnull(AVG(HPP),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and SUBSTRING(Keterangan,5,1)!='3' and SUBSTRING(Keterangan ,5,1)!='W')  end HTBP, " +
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' OR SUBSTRING(Keterangan ,5,1)='W'))  end TBJ, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' OR SUBSTRING(Keterangan ,5,1)='W'))  end HTBJ, " +
                "case when ItemID >0 then (select isnull(SUM(Qty),0) from T3_Retur where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end TRETUR, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Retur where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end HTRETUR, " +
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end TAdjust, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end HTAdjust, " +
                "case when ItemID >0 then (select isnull(SUM(Qty),0) from T3_KirimDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and KirimID>0) end KKirim, " +
                "case when ItemID >0 then (select isnull(SUM(hpp),0) from T3_KirimDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and KirimID>0) end HKKirim, " +
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='P' or SUBSTRING(Keterangan,6,1)='P')  " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end KBP, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='P' or  SUBSTRING(Keterangan,6,1)='P')  " + //
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end HKBP, " +
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' Or SUBSTRING(Keterangan,5,1)='W' or SUBSTRING(Keterangan,6,1)='3' Or SUBSTRING(Keterangan,6,1)='W') " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end KBJ, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' Or SUBSTRING(Keterangan,5,1)='W' or SUBSTRING(Keterangan,6,1)='3' Or SUBSTRING(Keterangan,6,1)='W') " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end HKBJ, " +
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' " +
                "and LokID In (select ID from FC_Lokasi where lokasi='q99')) end KSample, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%'  " +
                "and LokID In (select ID from FC_Lokasi where lokasi='q99')) end HKSample, " +
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='-s-' Or SUBSTRING(Keterangan,6,1)='-s-') " +//
                " ) end KBS, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='s' Or SUBSTRING(Keterangan,6,1)='s') " +//
                " ) end HKBS, " +
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end KAdjust, " +
                "case when ItemID >0 then (select isnull(Avg(HPP),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end HKAdjust from ( " +
                "select distinct convert(varchar,tanggal ,112) as HMY,(select CONVERT(varchar,tanggal,103)) as tanggal, ItemID from vw_KartuStockBJ ) as A) as B  " +
                "where LEFT(convert(varchar,HMY,112),6)='" + Periode + "' and ItemID in  " +
                "(select ID from FC_Items where partno='" + PartNO + "' and (SUBSTRING(partno,5,1)='3' or SUBSTRING(partno,5,1)='W'))order by tanggal";
            return strSQL;
        }

        public string ViewMutasiBJT1DetRekapHarian(string Periode1, string Periode2, string awal, string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode2.Substring(0, 4));
            if (int.Parse(Periode2.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode2.Substring(0, 4));
            else
                tahun = int.Parse(Periode2.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode1) <= Convert.ToInt32(periode))
                #region query lama
                strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") +" +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where  ItemID=B.ItemID " +
                    "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and  convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  '" + Periode1 + "' as HMY1,'" + Periode2 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            #endregion query lama
            else
            {
                strSQL = "declare @periode1 varchar(10) " +
                    "declare @periode2 varchar(10) " +
                    "select @periode1='" + Periode2.Substring(0, 6) + "' + '01' " +
                    "select @periode2='" + Periode2 + "' " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or partno like '%-m-%' OR PartNo  like '%-M-%') " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJNew where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") +" +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  ItemID=B.ItemID " +
                    "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%' or Keterangan like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%' or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  '" + Periode2.Substring(0, 6) + "' + '01' as HMY1,'" + Periode2 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
        }

        public string ViewMutasiBJT1DetRekap(string Periode, string awal, string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode.Substring(0, 4));
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                # region Query lama
                strSQL = "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                    "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID ) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            #endregion
            else
            {
                strSQL =
                    "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode  " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID ) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
        }

        public string ViewMutasiBJT1DetRekapKonversi(string Periode, string awal, string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode.Substring(0, 4));
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                #region query lama
                strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) *(Luas/LuasUtuh) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) *(Luas/LuasUtuh) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) *(Luas/LuasUtuh) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  '" + Periode + "' as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            #endregion
            else
            {
                strSQL =
                    "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemid in (select ID from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%'))" +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJNew where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) *(Luas/LuasUtuh) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) *(Luas/LuasUtuh) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%'  or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99'))  *(Luas/LuasUtuh) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99'))*(Luas/LuasUtuh) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY )*(Luas/LuasUtuh) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  @periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
        }

        public string ViewMutasiBJDetRekapHarian(string Periode1, string Periode2, string awal, string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode2.Substring(0, 4));
            if (int.Parse(Periode2.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode2.Substring(0, 4));
            else
                tahun = int.Parse(Periode2.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode1) <= Convert.ToInt32(periode))
                #region query lama
                strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") +" +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where  ItemID=B.ItemID " +
                    "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and  convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  '" + Periode1 + "' as HMY1,'" + Periode2 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            #endregion
            else
            {
                strSQL = "declare @periode1 varchar(10) " +
                    "declare @periode2 varchar(10) " +
                    "select @periode1='" + Periode2.Substring(0, 6) + "' + '01' " +
                    "select @periode2='" + Periode2 + "' " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or PartNo like '%-M-%') " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJNew where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") +" +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  ItemID=B.ItemID " +
                    "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) + (select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-1-%'))as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  '" + Periode2.Substring(0, 6) + "' + '01' as HMY1,'" + Periode2 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            if (Convert.ToInt32(Periode1) >= 201906)
            {
                strSQL = "declare @periode1 varchar(10) " +
                    "declare @periode2 varchar(10) " +
                    "select @periode1='" + Periode2.Substring(0, 6) + "' + '01' " +
                    "select @periode2='" + Periode2 + "' " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or PartNo like '%-M-%') " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") +" +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  ItemID=B.ItemID " +
                    "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                    "or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping') and ItemID=A.ItemID and " +
                    "convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 " +
                    "and convert(varchar,tanggal ,112)<=A.HMY2  and (process  like '%simetris%' ) and (Keterangan like '%-1-%'))as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ( " +
                    "isnull(sfrom,'-') like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') and ItemID=A.ItemID and " +
                    "convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  '" + Periode2.Substring(0, 6) + "' + '01' as HMY1,'" + Periode2 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
        }

        public string ViewMutasiBJDetRekap(string Periode, string awal, string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode.Substring(0, 4));
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                # region Query lama
                strSQL = "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                    "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID ) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            #endregion
            else
            {
                strSQL =
                    "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode  " +
                    "select *,TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID )+ " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-1-%')) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items " +
                    "where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            if (Convert.ToInt32(Periode) >= 201906)
            {
                strSQL =
                    "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode  " +
                    "select *,TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                    "or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping')and ItemID=A.ItemID )+ " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-1-%')) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ( " +
                    "isnull(sfrom,'-') like 'bevel%' or isnull(sfrom,'-') like 'strapingr%')and ItemID=A.ItemID )+ " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items " +
                    "where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
        }

        public string ViewMutasiBJDetRekapKonversi(string Periode, string awal, string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode.Substring(0, 4));
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                #region query lama
                strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) *(Luas/LuasUtuh) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) *(Luas/LuasUtuh) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) *(Luas/LuasUtuh) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  '" + Periode + "' as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            #endregion
            else
            {
                strSQL =
                    "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemid in (select ID from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%'))" +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJNew where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                    "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) *(Luas/LuasUtuh) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) *(Luas/LuasUtuh) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                    "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99'))  *(Luas/LuasUtuh) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                    "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99'))*(Luas/LuasUtuh) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY )*(Luas/LuasUtuh) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select  @periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas," +
                    "(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
        }

        public string ViewMutasiBSDetRekap(string Periode, string awal, string blnqty, string bs)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode.Substring(0, 4));
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                strSQL = "declare @periode varchar(10) " +
                        "select @periode='" + Periode + "'" +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                        "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                        "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                        "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                        "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID) as TWIP,  " +
                        "0 as HTWIP,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                        "0 as HTBP,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID " +
                        " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                        "0 as HTBJ,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                        "0 as HTRETUR,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                        "0 as HTAdjust,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                        "0 as HKKirim,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                        "0 as HKBP,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                        "0 as HKBJ,  " +
                        "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                        "0 as HKSample,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                        "0 as HKBS,  " +
                        "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                        "0 as HKAdjust from (  " +
                        "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                        ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            else
            {
                if (Convert.ToInt32(Periode) < Convert.ToInt32("201708"))
                    strSQL =
                        "declare @periode varchar(10) " +
                        "select @periode='" + Periode + "'" +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                        "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                        "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                        "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                        "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID) as TWIP,  " +
                        "0 as HTWIP,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                        "0 as HTBP,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%')) as TBS,  " +
                        "0 as HTBS,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID " +
                        " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                        "0 as HTBJ,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                        "0 as HTRETUR,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                        "0 as HTAdjust,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                        "0 as HKKirim,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                        "0 as HKBP,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                        "0 as HKBJ,  " +
                        "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                        "0 as HKSample,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                        "0 as HKBS,  " +
                        "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                        "0 as HKAdjust from (  " +
                        "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                        ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                else
                    if (bs == "BSAUTO")
                    strSQL =
                    "declare @periode varchar(10)  " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID in  (select itemid from tempitembsauto) " +
                    "select *,TWIP+TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                    "from (select *,  " +
                    "isnull((select distinct  isnull(sum( " + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal  " +
                    "from (select *,   " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, 0 as HTWIP,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  0 as HTBP,   " +
                     "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%' " +
                    ")  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS,   " +

                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%')  " +
                    "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                    " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                    "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KSample,  0 as HKSample,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                    "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                    "from (   " +
                    "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                    "where ID  in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                    " " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                else
                    strSQL =
                    "declare @periode varchar(10)  " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID not in  (select itemid from tempitembsauto) " +
                    "select *,TWIP+TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                    "from (select *,  " +
                    "isnull((select distinct  isnull(sum( " + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal  " +
                    "from (select *,   " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, 0 as HTWIP,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  0 as HTBP, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%'  " +
                    ")  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' )  " +
                    "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                    " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                    "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  0 as HKSample,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%' )  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                    "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                    "from (   " +
                    "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                    "where partno like '%-s-%' and ID  not in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                    " " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
            }
            if (Convert.ToInt32(Periode) >= 201906)
            {
                if (bs == "BSAUTO")
                    strSQL =
                    "declare @periode varchar(10)  " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID in  (select itemid from tempitembsauto) " +
                    "select *,TWIP+TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                    "from (select *,  " +
                    "isnull((select distinct  isnull(sum( " + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal  " +
                    "from (select *,   " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, 0 as HTWIP,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  0 as HTBP,   " +
                     "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%' " +
                    ")  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS,   " +

                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%')  " +
                    "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                    " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                    "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KSample,  0 as HKSample,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                    "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                    "from (   " +
                    "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                    "where ID  in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                    " " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                else
                    strSQL =
                    "declare @periode varchar(10)  " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID not in  (select itemid from tempitembsauto) " +
                    "select *,TWIP+TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                    "from (select *,  " +
                    "isnull((select distinct  isnull(sum( " + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal  " +
                    "from (select *,   " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, 0 as HTWIP,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  0 as HTBP, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%'  " +
                    ")  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS, " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' )  " +
                    "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                    " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                    "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  0 as HKSample,   " +
                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%' )  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                    "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                    "from (   " +
                    "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                    "where partno like '%-s-%' and ID  not in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                    " " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
            }
            return strSQL;
        }

        public string ViewMutasiBSDetRekapHarian(string Periode1, string Periode2, string awal, string blnqty, string bs)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode1.Substring(0, 4));
            if (int.Parse(Periode2.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode2.Substring(0, 4));
            else
                tahun = int.Parse(Periode2.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode1) <= Convert.ToInt32(periode))
                #region Query Lama
                strSQL = "declare @periode varchar(10) " +
                        "select @periode='" + Periode1 + "'" +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                        "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                        "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                        "isnull((select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + "),0) as Awal from (select *,  " +
                        "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID) as TWIP,  " +
                        "0 as HTWIP,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                        "0 as HTBP,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID " +
                        " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                        "0 as HTBJ,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                        "0 as HTRETUR,  " +
                        "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                        "0 as HTAdjust,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                        "0 as HKKirim,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                        "0 as HKBP,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                        "0 as HKBJ,  " +
                        "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                        "0 as HKSample,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                        "0 as HKBS,  " +
                        "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                        "0 as HKAdjust from (  " +
                        "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                        ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            #endregion
            else
                if (bs == "BSAUTO")
                strSQL =
                "declare @periode1 varchar(10) " +
                "declare @periode2 varchar(10) " +
                "select @periode1='" + Periode2.Substring(0, 6) + "' + '01' " +
                "select @periode2='" + Periode2 + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and itemID in  (select itemid from tempitembsauto) " +
                "select *,awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                "(select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") + " +
                "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                "and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, 0 as HTWIP,   " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                "and ItemID=A.ItemID and lokid  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                "substring(Keterangan,1,21) like '%-1-%')  and lokid  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  0 as HTBP, " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%' )) as TBS,  " +
                "0 as HTBS,  " +

                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' ) " +
                "and LokID  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                "and LokID  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and LokID  in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-S-%') and (LokID  in (select ID from FC_Lokasi where Lokasi='BSAUTO'))) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select @periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where ID  in  (select itemid from tempitembsauto)) as B0) as A  ) as B  " +
                ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0" +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
            else
                strSQL =
                "declare @periode1 varchar(10) " +
                "declare @periode2 varchar(10) " +
                "select @periode1='" + Periode2.Substring(0, 6) + "' + '01' " +
                "select @periode2='" + Periode2 + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and itemID not in  (select itemid from tempitembsauto) " +
                "select *,awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                "(select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") + " +
                "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, 0 as HTWIP,   " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                "substring(Keterangan,1,21) like '%-1-%')  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  0 as HTBP, " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%' )) as TBS,  " +
                "0 as HTBS,  " +

                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' ) " +
                "and LokID not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                "and LokID not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and LokID  in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-S-%') and (LokID not in (select ID from FC_Lokasi where Lokasi='BSAUTO'))) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select @periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where partno like '%-s-%' and ID not in  (select itemid from tempitembsauto)) as B0) as A  ) as B  " +
                ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";

            return strSQL;
        }

        public string ViewMutasiBSDetRekapKonversi(string Periode, string awal, string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode.Substring(0, 4));
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                strSQL = "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                    "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                "(select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ")*(Luas/LuasUtuh) as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID )*(Luas/LuasUtuh) as TWIP,  " +
                "0 as HTWIP,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%'))*(Luas/LuasUtuh) as TBP,  " +
                "0 as HTBP,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%'))*(Luas/LuasUtuh) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID  and process='retur')*(Luas/LuasUtuh) as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID  and process='Adjust-in')*(Luas/LuasUtuh) as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID  and process='kirim')*(Luas/LuasUtuh) as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID   " +
                "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99')))*(Luas/LuasUtuh) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  )*(Luas/LuasUtuh) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select  @Periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            else
                strSQL = "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                "(select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ")*(Luas/LuasUtuh) as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID )*(Luas/LuasUtuh) as TWIP,  " +
                "0 as HTWIP,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%'))*(Luas/LuasUtuh) as TBP,  " +
                "0 as HTBP,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%' )) as TBS,  " +
                    "0 as HTBS,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%'))*(Luas/LuasUtuh) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID  and process='retur')*(Luas/LuasUtuh) as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in')*(Luas/LuasUtuh) as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID  and process='kirim')*(Luas/LuasUtuh) as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99')))*(Luas/LuasUtuh) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  )*(Luas/LuasUtuh) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select  @Periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            return strSQL;
        }

        public string ViewMutasiBPDet(string PartNO, string Periode, string awal)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total, " +
                "(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode +
                "' and convert(varchar,tanggal  ,112)<B.HMY and ItemID=B.ItemID )+(select isnull(sum( " + awal +
                "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + " ) as Awal from (select *, " +
                "(select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process='direct' and process not like 'ex%') as TWIP, " +
                "(select isnull(avg(HPP),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process='direct' and process not like 'ex%') as HTWIP, " +
                "(select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and  SUBSTRING(Keterangan,5,1)!='3'  and SUBSTRING(Keterangan,5,1)!='W') as TBP, " +
                "(select isnull(AVG(HPP),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and  SUBSTRING(Keterangan,5,1)!='3'  and SUBSTRING(Keterangan,5,1)!='W') as HTBP, " +
                "(select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and  (SUBSTRING(Keterangan,5,1)='3' OR SUBSTRING(Keterangan ,5,1)='W')) as TBJ, " +
                "(select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and  (SUBSTRING(Keterangan,5,1)='3' OR SUBSTRING(Keterangan ,5,1)='W')) as HTBJ, " +
                "(select isnull(SUM(Qty),0) from T3_Retur where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) as TRETUR, " +
                " (select isnull(avg(hpp),0) from T3_Retur where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) as HTRETUR, " +
                " (select isnull(SUM(QtyIn),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) as TAdjust, " +
                " (select isnull(avg(hpp),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) as HTAdjust, " +
                " (select isnull(SUM(Qty),0) from T3_KirimDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and KirimID>0) as KKirim, " +
                " (select isnull(SUM(hpp),0) from T3_KirimDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and KirimID>0) as HKKirim, " +
                " (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='P' or SUBSTRING(Keterangan,6,1)='P')  " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99')) as KBP, " +
                " (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='P' or SUBSTRING(Keterangan,6,1)='P')  " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99')) as HKBP, " +
                " (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' Or SUBSTRING(Keterangan,5,1)='W' or SUBSTRING(Keterangan,6,1)='3' Or SUBSTRING(Keterangan,6,1)='W') " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99')) as KBJ, " +
                " (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' Or SUBSTRING(Keterangan,5,1)='W' or SUBSTRING(Keterangan,6,1)='3' Or SUBSTRING(Keterangan,6,1)='W') " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99')) as HKBJ, " +
                " (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%'  " +
                "and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample, " +
                " (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%'   " +
                "and LokID In (select ID from FC_Lokasi where lokasi='q99')) as HKSample, " +
                " (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='S' Or SUBSTRING(Keterangan,6,1)='S') " +//
                ") as KBS, " +
                " (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='S' Or SUBSTRING(Keterangan,6,1)='S') " +//
                ") as HKBS, " +
                " (select isnull(SUM(QtyOut),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) as KAdjust, " +
                " (select isnull(Avg(HPP),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) as HKAdjust from ( " +
                "select distinct convert(varchar,tanggal ,112) as HMY,(select CONVERT(varchar,tanggal,103)) as tanggal, ItemID from vw_KartuStockBJ ) as A) as B  " +
                "where LEFT(convert(varchar,HMY,112),6)='" + Periode + "' and ItemID in  " +
                "(select ID from FC_Items where partno='" + PartNO + "' and SUBSTRING(partno,5,1)='P' )order by tanggal";
            return strSQL;
        }

        public string ViewMutasiBPDetRekap(string Periode, string blnqty0, string blnqty, int dept)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            string strdept = string.Empty;
            string PlusStockdept = string.Empty;
            string filterDept = "where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            int tahun = 0;
            string periode0 = string.Empty;
            if (dept == 1)
                filterDept = "where TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            if (int.Parse(Periode.Substring(4, 2)) <= 1)
            {
                tahun = int.Parse(Periode.Substring(0, 4)) - 1;
            }
            else
            {
                tahun = int.Parse(Periode.Substring(0, 4));
            }
            string test = Periode.Substring(4, 2);
            if (int.Parse(Periode.Substring(4, 2)) == 1)
            {
                periode0 = (int.Parse(Periode.Substring(0, 4)) - 1).ToString() + "12";
            }
            else
            {
                periode0 = Periode.Substring(0, 4) + (int.Parse(Periode.Substring(4, 2)) - 1).ToString().PadLeft(2, '0');
            }
            if (dept == 0)
            {
                strdept = string.Empty;
            }
            if (dept == 1)
                strdept = " and dept='FINISHING' ";
            if (dept == 2)
                strdept = " and isnull(dept,'-')<>'FINISHING' ";
            if (dept == 2)
                PlusStockdept = " and dept='FINISHING' ";
            if (dept == 1)
                PlusStockdept = " and isnull(dept,'-')<>'FINISHING' ";
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                #region query lama
                strSQL =
                        "declare @periode varchar(10) " +
                        "select @periode='" + Periode + "'" +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                        "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                        "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                        "(select distinct  isnull(sum(" + blnqty + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                        "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where process='direct' and ItemID=A.ItemID   ) as TWIP,  " +
                        "0 as HTWIP,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                        "0 as HTBP,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                        " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                        "0 as HTBJ,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                        "0 as HTRETUR,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                        "0 as HTAdjust,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                        "0 as HKKirim,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                        "0 as HKBP,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                        "0 as HKBJ,  " +
                        "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                        "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                        "0 as HKSample,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID   " +
                        "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                        "0 as HKBS,  " +
                        "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                        "0 as HKAdjust from (  " +
                        "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                        ") as C where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            #endregion
            else
                strSQL = "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                    "select *,TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "(select distinct  isnull(sum(" + blnqty + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and ItemID=A.ItemID ) + " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%'))as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' or process  like '%Supply%') and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%Supply%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            if (Convert.ToInt32(Periode) >= 201906)
            {
                strSQL = "declare @periode varchar(10) " +
                    "select @periode='" + Periode + "'" +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                    "select *,TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "(select distinct  isnull(sum(" + blnqty + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                    "or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping') and ItemID=A.ItemID ) + " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%'))as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and " +
                    "(isnull(sfrom,'-') like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') and ItemID=A.ItemID ) + " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID and (process  like '%simetris%' ) and  " +
                    "(substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ, " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' or process  like '%Supply%') and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%Supply%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
        }

        public string ViewMutasiBPDetRekapHarian(string Periode1, string Periode2, string awal, string blnqty, int dept)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            string strdept = string.Empty;
            string PlusStockdept = string.Empty;
            string filterDept = "where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            int tahun = 0;
            string periode0 = string.Empty;
            if (dept == 1)
                filterDept = "where TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            if (int.Parse(Periode2.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode2.Substring(0, 4));
            else
                tahun = int.Parse(Periode2.Substring(0, 4)) - 1;
            string test = Periode2.Substring(4, 2);
            if (int.Parse(Periode2.Substring(4, 2)) == 1)
            {
                periode0 = (int.Parse(Periode2.Substring(0, 4)) - 1).ToString() + "12";
            }
            else
            {
                periode0 = Periode2.Substring(0, 4) + (int.Parse(Periode2.Substring(4, 2)) - 1).ToString().PadLeft(2, '0');
            }
            if (dept == 0)
            {
                strdept = string.Empty;
            }
            if (dept == 1)
                strdept = " and dept='FINISHING' ";
            if (dept == 2)
                strdept = " and isnull(dept,'-')<>'FINISHING' ";
            if (dept == 2)
                PlusStockdept = " and dept='FINISHING' ";
            if (dept == 1)
                PlusStockdept = " and isnull(dept,'-')<>'FINISHING' ";
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode1) <= Convert.ToInt32(periode))
                #region query lama
                strSQL =
                        "declare @periode varchar(10) " +
                        "select @periode='" + Periode1 + "'" +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                        "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                        "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                        "(select distinct  isnull(sum(" + blnqty + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                        "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where process='direct' and ItemID=A.ItemID   ) as TWIP,  " +
                        "0 as HTWIP,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                        "0 as HTBP,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                        " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                        "0 as HTBJ,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                        "0 as HTRETUR,  " +
                        "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                        "0 as HTAdjust,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                        "0 as HKKirim,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                        "0 as HKBP,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                        "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                        "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                        "0 as HKBJ,  " +
                        "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                        "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                        "0 as HKSample,  " +
                        "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID   " +
                        "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                        "0 as HKBS,  " +
                        "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                        "0 as HKAdjust from (  " +
                        "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                        ") as C where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            #endregion
            else
                strSQL = "declare @periode1 varchar(10) " +
                    "declare @periode2 varchar(10) " +
                    "select @periode1='" + Periode2.Substring(0, 6) + "' + '01' " +
                    "select @periode2='" + Periode2 + "' " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-P-%' ) " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "(select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") + " +
                    "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                    "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and ItemID=A.ItemID )+(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%' )) as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' )) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";

            if (Convert.ToInt32(Periode1) >= 201906)
            {
                strSQL = "declare @periode1 varchar(10) " +
                    "declare @periode2 varchar(10) " +
                    "select @periode1='" + Periode2.Substring(0, 6) + "' + '01' " +
                    "select @periode2='" + Periode2 + "' " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-P-%' ) " +
                    "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    "(select distinct  isnull(sum(" + awal + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + tahun + ") + " +
                    "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                    "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                    "or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping') and ItemID=A.ItemID ) + " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%'))as TWIP,  " +
                    "0 as HTWIP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and " +
                    "(isnull(sfrom,'-') like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') and ItemID=A.ItemID ) + " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%')) as TBP,  " +
                    "0 as HTBP,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                    "0 as HTBJ,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                    " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                    "0 as HTBS,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                    "0 as HTRETUR,  " +
                    "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                    "0 as HTAdjust,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                    "0 as HKKirim,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                    "0 as HKBP,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                    "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                    "0 as HKBJ,  " +
                    "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                    "0 as HKSample,  " +
                    "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                    "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                    "0 as HKBS,  " +
                    "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                    "0 as HKAdjust from (  " +
                    "select @Periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                    ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
            }
            return strSQL;
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
            if (partno.Trim().Substring(3, 3) != "-1-")//|| partno.Trim().Substring(4, 3) != "-1-" || partno.Trim().Substring(5, 3) != "-1-")
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
                    "insert into #adminkswip(idrec,tanggal,itemid0,qty,lokid)values(@idrec,@tanggal,@itemid0,@qty,@lokid) " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
                    "END " +
                    "CLOSE kursor " +
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
                    "and A.YearPeriod=" + thawal + " " +
                    "UNION ALL " +
                    "    SELECT SUM(qty) as qty from #adminkswip where itemid0=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + Thn + s + bln + "' " +
                    "     )as ain0) end awal, Penerimaan,pengeluaran,hpp   " +
                    "from (  " +
                    "    select '0'as ID ,ItemID ,'" + tglA + "' as tanggal, '-' as partno,'saldo awal' as keterangan, " +
                    "0  as penerimaan,0 as pengeluaran,0 as hpp  from SaldoInventoryT1 A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "and A.YearPeriod=" + (Thn + s + bln).Substring(0, 4) + " " +
                    "UNION ALL " +
                    "    SELECT CONVERT(varchar, TglProduksi, 112) + '0' + RTRIM(CAST(A.ItemID AS varchar(10))) + RTRIM(CAST(A.ItemID AS varchar(10))) AS ID,A.itemID as itemid,A.TglProduksi as tanggal" +
                    "    ,B.PartNo, RTRIM(B.PartNo) AS keterangan, sum(A.Qty) AS Penerimaan, 0 AS Pengeluaran, avg(A.HPP) AS hpp FROM FC_Items AS B INNER JOIN  " +
                    "    BM_Destacking AS A ON B.ID = A.ItemID  WHERE (LEFT(CONVERT(varchar, A.TglProduksi, 112), 6) = '" + Thn + s + bln + "') " +
                    "    and A.rowstatus>-1 AND (B.PartNo = '" + partno + "') group by A.TglProduksi,A.ItemID,B.PartNo  " +
                    "UNION ALL " +
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
                    "GROUP BY  A.TglSerah,B.itemid,A.itemid, C1.PartNo, C.PartNo " +
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
                   "insert into #adminkswip(idrec,tanggal,itemid0,qty,lokid)values(@idrec,@tanggal,@itemid0,@qty,@lokid) " +
                   "FETCH NEXT FROM kursor " +
                   "INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
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
                   "and A.YearPeriod=" + thawal + " " +
                   "UNION ALL " +
                   "    SELECT SUM(qty) as qty from #adminkswip where itemid0=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + Thn + s + bln + "' " +
                   "     )as ain0) end awal, Penerimaan,pengeluaran,hpp   " +
                   "from (  " +
                   "    select '0'as ID ,ItemID ,'" + tglA + "' as tanggal, '-' as partno,'saldo awal' as keterangan, " +
                   "0  as penerimaan,0 as pengeluaran,0 as hpp  from SaldoInventoryT1 A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                   "and A.YearPeriod=" + (Thn + s + bln).Substring(0, 4) + " " +
                   "UNION ALL " +
                   "SELECT CONVERT(varchar, TglJemur, 112) + '0'+  RTRIM(CAST(A.ItemID0 AS varchar(10)))  + CONVERT(varchar, C.TglProduksi, 112) +RTRIM(CAST(A.ItemID AS varchar(10))) " +
                   " +rtrim((select nopalet from bm_palet where id in(select paletID from bm_destacking where id = A.Destid))) AS ID, A.itemID0 as itemid, " +
                   "A.TglJemur as tanggal,B.PartNo, 'dr produksi ' + RTRIM(D.PartNo) + ' -' +rtrim(convert(varchar,C.TglProduksi,110)) AS keterangan,   " +
                   "sum(A.QtyIn) AS Penerimaan, 0 AS Pengeluaran, avg(A.HPP) AS hpp FROM FC_Items AS B INNER JOIN T1_JemurLg AS A ON B.ID = A.ItemID0   " +
                   "inner join BM_Destacking C on C.ID=A.DestID inner join FC_Items D on C.ItemID=D.ID WHERE (LEFT(CONVERT(varchar, A.TglJemur, 112), 6) = '" + Thn + s + bln + "')  " +
                   " and A.status>-1 AND (B.PartNo = '" + partno + "')  group by C.TglProduksi,A.TglJemur,A.itemID0,A.ItemID,D.PartNo,B.PartNo,A.DestID          " +
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
            /**
             * Perubahan query pembacaan data dari createdtime ke tgltrans
             * Perubahan ini berlaku untuk report bulan nov 2014 dan seterusnya
             * sedangkan untuk report bulan okt 2014 dan sebelumnya masih membaca createdtime
             * pada table T1_SaldoPerlokasi ditambahkan data secara manual untuk mengcover
             * data yng tidak tergenerate oleh query
             * data ini akan hilang jika dilakukan posting di bulan oktober 2014 (jangan lakukan posting ulang u/ bln 10/2014)
             * Data update manual ada di file App_Data/UpdateSaldoAwalNovember2014.xls
             */
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
            #endregion

            string bln = fldBln.ToString();
            string s = new string('0', (2 - bln.Length));
            int lastDay = DateTime.DaysInMonth((Convert.ToInt16(periodeTahun)), fldBln);
            string d = new string('0', (2 - lastDay.ToString().Length));
            string lastperiode = string.Empty;
            string Periode = Thn + s + bln;
            if (Convert.ToInt32(bln) == 1)
                lastperiode = (Convert.ToInt32(Thn) - 1).ToString() + 12;
            else
                lastperiode = Thn + (Convert.ToInt32(bln) - 1).ToString().PadLeft(2, '0');
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
            {
                //Query dengan pembacaan data by createdtime
                strSQL = "declare @HMY varchar(6) " +
                "declare @thnbln varchar(6) " +
                "select @HMY='" + Thn + s + bln + "' " +
                "select @thnbln='" + lastperiode + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP1] " +
                "select * into tempWIP1 from vw_KartustockWIPOld where left(CONVERT(char, tanggal ,112),6)=@HMY " +
                   "select itemID,HMY,NoDocument,AwalQty, " +
                   "InProdQty,InAdjustQty,outAdjustQty,H99,B99,C99,Q99,InP99,OutP99,(select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= M2.itemid)as volume from (  " +
                   "select itemID,HMY,partno as NoDocument, " +
                   "(select isnull(SUM(qty),0) from BM_Destacking where qty>0 and LEFT(CONVERT(varchar,TglProduksi, 112), 6) = M1.HMY " +
                   "and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%adj%')) as InProdQty," +
                   "(select isnull(SUM(qty),0) from tempWIP1 where qty>0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%in%') as InAdjustQty, " +
                   "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and (lokasi='H99' or lokasi like '%in%')) as H99, " +
                   "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='B99') as B99, " +
                   "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='C99') as C99, " +
                   "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='Q99') as Q99, " +
                   "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%ou%') as outAdjustQty,0 as InP99, 0 as OutP99, " +
                   "(select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln and itemid =M1.ItemID and lokid not in (select ID from fc_lokasi where lokasi like '%p99%'))as AwalQty " +
                   "from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, @HMY as HMY from (  " +
                   "select distinct itemid  from bm_destacking where tglproduksi>='6/1/2013' " +
                   ") as A " +
                   ") as M1 " +
                   ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or B99<>0 or C99<>0 or Q99<>0 order by M2.NoDocument ";
            }
            else
            {
                strSQL = "declare @HMY varchar(6) " +
                "declare @thnbln varchar(6) " +
                "select @HMY='" + Thn + s + bln + "' " +
                "select @thnbln='" + lastperiode + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP1] " +
                "select * into tempWIP1 from vw_KartustockWIP where left(CONVERT(char, tanggal ,112),6)=@HMY " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP2]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP2] " +
                "select * into tempWIP2 from vw_KartustockWIP2 where left(CONVERT(char, tanggal ,112),6)=@HMY " +
                "select itemID,HMY,NoDocument,sum(AwalQty) as AwalQty,sum(InProdQty) as InProdQty,sum(InP99) as InP99,sum(InAdjustQty) as InAdjustQty,sum(InI99) as InI99,  " +
                "sum(outAdjustQty) as outAdjustQty,sum(H99) as H99,sum(OutI99) as I99,sum(B99) as B99,sum(C99) as C99,sum(Q99) as Q99,sum(OutP99) as OutP99,  " +
                "(select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= atALL.itemid)as volume,(select Tebal from fc_items where ID= atALL.itemid) Tebal from(  " +
                "    select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (    " +
                "        select itemID,HMY,'A' + partno as NoDocument,   " +
                "        (select isnull(SUM(qty),0) from BM_Destacking where qty>0 and LEFT(CONVERT(varchar,TglProduksi, 112), 6) = M1.HMY   " +
                "        and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as InProdQty,0 as InP99,  " +
                "        (select isnull(SUM(qty),0) from tempWIP1 where qty>0 and ItemID0=M1.ItemID  and idrec like '%in%') as InAdjustQty,  0 as InI99,  " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and (lokasi='H99' and process<>'lari' and process<>'listplank')) as H99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID   and (lokasi='i99' and process<>'lari' and process<>'listplank')) as OutI99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='B99' and process<>'lari' and process<>'listplank' ) as B99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='C99' and process<>'lari' and process<>'listplank') as C99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='Q99' and process<>'lari' and process<>'listplank') as Q99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and process='lari') as OutP99,  " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and idrec like '%ou%') as outAdjustQty,   " +
                "        (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln and itemid =M1.ItemID and   " +
                "        LokID not in (select ID from FC_Lokasi where Lokasi='p99') )as AwalQty   " +
                "        from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, @HMY as HMY from (  " +
                "        select distinct itemid  from bm_destacking where tglproduksi>='6/1/2013'  " +
                "        ) as A   " +
                "    ) as M1   " +
                ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0  or B99<>0 or C99<>0 or Q99<>0   " +
                "union all   " +
                "select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (    " +
                "    select itemID,HMY,'PP:' + partno as NoDocument,0 as InProdQty,  " +
                "    (select isnull(SUM(qty),0) from tempWIP2 where qty>0  and idrec not like '%IN%' and ItemID0=M1.ItemID and   " +
                "    left(CONVERT(char, tanggal ,112),6)=M1.HMY and process='lari') as InP99,  " +
                "    (select isnull(SUM(qty),0) from tempWIP2 where qty>0 and ItemID0=M1.ItemID and " +
                "    left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%in%'  and process='lari') as InAdjustQty, 0 as InI99,  " +
                "    (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno like '%-3-%' or I.PartNo like '%-W-%' or I.PartNo like '%-m-%')   " +
                "    where qty<0  and idrec not like '%ou%'  and ItemID0=M1.ItemID   and process='lari') as H99," +
                "    (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno like '%-p-%' or I.partno  like '%-1-%' )   " +
                "    where qty<0  and idrec not like '%ou%'  and ItemID0=M1.ItemID   and process='lari'and lokid1  in (select ID from fc_lokasi where lokasi='I99')) as OutI99,  " +
                "    (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno  like '%-p-%'  )   " +
                "    where qty<0  and idrec not like '%ou%'  and ItemID0=M1.ItemID   and process='lari'and lokid1 not in (select ID from fc_lokasi where lokasi='I99')) as B99," +
                "    (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno  like '%-1-%'  )   " +
                "    where qty<0  and idrec not like '%ou%'  and ItemID0=M1.ItemID   and process='lari'and lokid1 not in (select ID from fc_lokasi where lokasi='I99') and V.LastModifiedBy <>'adjust' ) as C99, 0 as Q99, 0 as OutP99,   " +
                "    (select isnull(SUM(qty)*-1,0) from tempWIP2 where qty<0 and ItemID0=M1.ItemID and idrec like '%ou%') as outAdjustQty,   " +
                "    (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln   " +
                "    and itemid =M1.ItemID and LokID in (select ID from FC_Lokasi where Lokasi='p99'))as AwalQty   " +
                "    from(select distinct A.itemid0 as ItemID,(select partno from fc_items where ID=A.itemid0) as partno, @HMY as HMY from (  " +
                "        select distinct itemid0  from vw_KartuStockWIP2 where tanggal >='6/1/2013'  " +
                "        ) as A   " +
                "    ) as M1   " +
                ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0 or B99<>0 or C99<>0 or Q99<>0 or InP99<>0 or OutP99<>0   " +

                ") as atALL group by itemID,HMY,NoDocument order by NoDocument";
            }
            return strSQL;
        }

        public string ViewMutasiWIPRekapHarian(string periode1, string periode2)
        {
            #region prepared data
            string strSQL = string.Empty;
            //string prd = PartNo.Substring(0, 3);
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;

            #endregion

            string lastperiode = string.Empty;
            string Periode = periode2.Substring(0, 6);
            string bln = periode2.Substring(4, 2);
            if (Convert.ToInt32(bln) == 1)
                lastperiode = (Convert.ToInt32(periode2.Substring(0, 4)) - 1).ToString() + 12;
            else
                lastperiode = periode2.Substring(0, 4) + (Convert.ToInt32(bln) - 1).ToString().PadLeft(2, '0');
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
            {
                ////Query dengan pembacaan data by createdtime
                strSQL = "declare @HMY varchar(6) ";
            }
            else
            {
                strSQL = "declare @HMY1 varchar(10)  " +
                "declare @HMY2 varchar(10) " +
                "declare @thnbln varchar(6)  " +
                "select @HMY1='" + periode2.Substring(0, 6) + "' + '01' " +
                "select @HMY2='" + periode2 + "'  " +
                "select @thnbln='" + lastperiode + "'  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP1] " +
                "select * into tempWIP1 from vw_KartustockWIP where CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP2]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP2] " +
                "select * into tempWIP2 from vw_KartustockWIP2 where CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 " +
                "select itemID,HMY,NoDocument,sum(AwalQty) as AwalQty,sum(InProdQty) as InProdQty,sum(InP99) as InP99,sum(InAdjustQty) as InAdjustQty,sum(InI99) as InI99,  " +
                "sum(outAdjustQty) as outAdjustQty,sum(H99) as H99,sum(OutI99) as I99,sum(B99) as B99,sum(C99) as C99,sum(Q99) as Q99,sum(OutP99) as OutP99,  " +
                "(select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= atALL.itemid)as volume,(select Tebal from fc_items where ID= atALL.itemid) Tebal from(  " +
                "    select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (    " +
                "        select itemID,HMY,'A' + partno as NoDocument,   " +
                "        (select isnull(SUM(qty),0) from BM_Destacking where qty>0 and CONVERT(char, tglproduksi ,112)>=@HMY1 and CONVERT(char, tglproduksi ,112)<=@HMY2   " +
                "        and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as InProdQty,0 as InP99,  " +
                "        (select isnull(SUM(qty),0) from tempWIP1 where qty>0 and ItemID0=M1.ItemID  and idrec like '%in%') as InAdjustQty,  0 as InI99,  " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and (lokasi='H99' and process<>'lari' and process<>'listplank')) as H99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID   and (lokasi='i99' and process<>'lari' and process<>'listplank')) as OutI99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='B99' and process<>'lari' and process<>'listplank' ) as B99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='C99' and process<>'lari' and process<>'listplank') as C99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='Q99' and process<>'lari' and process<>'listplank') as Q99,   " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and process='lari') as OutP99,  " +
                "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and idrec like '%ou%') as outAdjustQty,   " +
                "        (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln and itemid =M1.ItemID and   " +
                "        LokID not in (select ID from FC_Lokasi where Lokasi='p99'))+isnull((select SUM(qty) from vw_KartustockWIP where ItemID0=M1.ItemID and process<>'lari' and process<>'listplank' and left(CONVERT(varchar,tanggal, 112),6) = left(@HMY1,6) and CONVERT(varchar,tanggal, 112) <@HMY1),0) as AwalQty   " +
                "        from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, @HMY1 as HMY from (  " +
                "        select distinct itemid  from bm_destacking where tglproduksi>='6/1/2013'  " +
                "        ) as A   " +
                "    ) as M1   " +
                ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0  or B99<>0 or C99<>0 or Q99<>0   " +
                "union all   " +
                "select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (    " +
                "    select itemID,HMY,'PP:' + partno as NoDocument,0 as InProdQty,  " +
                "    (select isnull(SUM(qty),0) from tempWIP2 where qty>0  and idrec not like '%IN%' and ItemID0=M1.ItemID and   " +
                "    process='lari') as InP99,  " +
                "    (select isnull(SUM(qty),0) from tempWIP2 where qty>0 and ItemID0=M1.ItemID and   " +
                "    idrec like '%in%'  and process='lari') as InAdjustQty,  0 as InI99,  " +
                "    (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno like '%-3-%' or I.PartNo like '%-W-%' or I.PartNo like '%-m-%')   " +
                "    where qty<0  and idrec not like '%ou%' and ItemID0=M1.ItemID   and process='lari') as H99,0 as OutI99,  " +
                "   (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno  like '%-p-%' or I.partno  like '%-1-%' )   " +
                "    where qty<0  and idrec not like '%ou%'  and ItemID0=M1.ItemID   and process='lari') as B99,0 as C99, 0 as Q99, 0 as OutP99,   " +
                "    (select isnull(SUM(qty)*-1,0) from tempWIP2 where qty<0 and ItemID0=M1.ItemID    " +
                "    and idrec like '%ou%') as outAdjustQty, (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln   " +
                "    and itemid =M1.ItemID and LokID in (select ID from FC_Lokasi where Lokasi='p99'))+isnull((select SUM(qty) from vw_KartustockWIP2 where ItemID0=M1.ItemID and process='lari' and left(CONVERT(varchar,tanggal, 112),6) = left(@HMY1,6) and CONVERT(varchar,tanggal, 112) <@HMY1),0)as AwalQty   " +
                "    from(select distinct A.itemid0 as ItemID,(select partno from fc_items where ID=A.itemid0) as partno, @HMY1 as HMY from (  " +
                "        select distinct itemid0  from vw_KartuStockWIP2 where tanggal >='6/1/2013'  " +
                "        ) as A   " +
                "    ) as M1   " +
                ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0 or B99<>0 or C99<>0 or Q99<>0 or InP99<>0 or OutP99<>0   " +
                ") as atALL group by itemID,HMY,NoDocument order by NoDocument";
            }
            return strSQL;
        }

        public string ViewJurnalT1(string Bln0, string Bln)
        {
            string strSQL = string.Empty;
            strSQL = "declare @thnbln varchar(6) " +
                "declare @thnbln0 varchar(6) " +
                "set @thnbln0='" + Bln0 + "' " +
                "set @thnbln='" + Bln + "' " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, awal,awalH,terima,terimaH,keluar,keluarH,Saldo,SaldoH from (  " +
            "    select *,case when Saldo>0 then CAST(SaldoH/saldo as int) else 0 end avgprice from ( " +
            "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
            "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,case when (awal+terima)>0 then (awalH+terimaH)/(awal+terima)*keluar else 0 end keluarH,saldo,saldoH, " +
            "    case when saldo1.Saldo>0 then CAST(SaldoH/saldo as int) else 0 end avgprice from (  " +
        "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar ,SUM(keluarH)keluarH , " +
        "        SUM(awal)+SUM(terima)- SUM(keluar) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
    "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang, 0 awal,0 awalH,SUM(qty) terima, SUM(qty*HPP) terimaH,0 keluar,0 keluarH  " +
    "            from vw_KartuStockWIP A, FC_Items B  where A.ItemID0 =B.ID  and  A.qty>0  and  " +
    "            left(convert(varchar,tanggal,112),6)=@thnbln group by B.ID ,B.Tebal,B.Lebar,B.Panjang " +
    "            union all  " +
    "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,0 as terima,0 as terimaH,SUM(qty)*-1 as keluar,(SUM(qty*HPP)*-1)as keluarH  " +
    "            from vw_KartuStockWIP A, FC_Items B  where A.ItemID0 =B.ID  and A.qty<0 and  " +
    "            left(convert(varchar,tanggal,112),6)=@thnbln group by B.ID,B.Tebal,B.Lebar,B.Panjang  " +
    "            union all  " +
    "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,SUM(saldo ) as awal,SUM(saldo * avgprice) as awalH, 0 as terima, 0 as terimaH,0 as keluar,0 as keluarH   " +
    "            FROM T1_Saldoperlokasi A, FC_Items B  where  A.itemid=B.ID  and A.Rowstatus>-1 and A.thnbln =@thnbln0 and A.LokID not in  " +
    "            (select ID from FC_Lokasi where Lokasi='p99') group by B.ID,B.Tebal,B.Lebar,B.Panjang " +
    "            union all " +
    "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,SUM(qty)  as terima, SUM(qty*HPP)  as terimaH,0 as keluar,0 as keluarH  " +
    "            from vw_KartuStockWIP2 A, FC_Items B  where A.ItemID0 =B.ID  and  A.qty>0 and  " +
    "            left(convert(varchar,tanggal,112),6)=@thnbln group by B.ID ,B.Tebal,B.Lebar,B.Panjang " +
    "            union all  " +
    "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,0 as terima,0 as terimaH,SUM(qty)*-1 as keluar,(SUM(qty*HPP)*-1)as keluarH  " +
    "            from vw_KartuStockWIP2 A, FC_Items B  where A.ItemID0 =B.ID  and A.qty<0  and  " +
    "            left(convert(varchar,tanggal,112),6)=@thnbln group by B.ID ,B.Tebal,B.Lebar,B.Panjang " +
    "            union all  " +
    "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,SUM(saldo) as awal,SUM(saldo*avgprice) as awalH, 0 as terima, 0 as terimaH,0 as keluar,0 as keluarH   " +
    "            FROM T1_Saldoperlokasi A, FC_Items B  where  A.itemid=B.ID  and A.Rowstatus>-1 and A.thnbln =@thnbln0 and A.LokID  in  " +
    "            (select ID from FC_Lokasi where Lokasi='p99') " +
    "            group by B.ID,B.Tebal,B.Lebar,B.Panjang " +
        "        ) as saldo group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3 " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 order by tebal,lebar,panjang, ukuran ";
            return strSQL;
        }

        public string ViewJurnalT3(string Bln0, string Bln, string PARTNO)
        {
            string strSQL = string.Empty;
            strSQL = "declare @sqlWIP nvarchar(max) " +
                "declare @thnbln varchar(6) " +
                "declare @thnbln0 varchar(6) " +
                "set @thnbln0='" + Bln0 + "' " +
                "set @thnbln='" + Bln + "' " +
                "declare @thnT3 varchar(4) " +
                "declare @blnAwalT3 varchar(2) " +
                "declare @tglawalT3 varchar(max) " +
                "declare @AwalT3 varchar(6) " +
                "declare @AwalT3Price varchar(max) " +
                "declare @tahunAwalT3 varchar(6) " +
                "select @thnT3=LEFT(@thnbln,4) " +
                "select @blnAwalT3=RIGHT(@thnbln0,2) " +
                "if right(@blnAwalT3,2)='01' begin set @AwalT3='janqty' set @AwalT3Price='janAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='02' begin set @AwalT3='febqty' set @AwalT3Price='febAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='03' begin set @AwalT3='marqty' set @AwalT3Price='marAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='04' begin set @AwalT3='aprqty' set @AwalT3Price='aprAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='05' begin set @AwalT3='meiqty' set @AwalT3Price='meiAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='06' begin set @AwalT3='junqty' set @AwalT3Price='junAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='07' begin set @AwalT3='julqty' set @AwalT3Price='julAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='08' begin set @AwalT3='aguqty' set @AwalT3Price='aguAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='09' begin set @AwalT3='sepqty' set @AwalT3Price='sepAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='10' begin set @AwalT3='oktqty' set @AwalT3Price='oktAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='11' begin set @AwalT3='novqty' set @AwalT3Price='novAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "if right(@blnAwalT3,2)='12' begin set @AwalT3='desqty' set @AwalT3Price='desAvgprice' set @tahunAwalT3=left(@thnbln0,4) end " +
                "declare @strSQL as varchar(max) set @sqlWIP ='select SL1.ukuran,SL1.awal,SL1.awalH,SL1.terima,SL1.terimaH,SL1.keluar,SL1.keluarH,SL1.Saldo, " +
                "isnull(SL1.SaldoH,0)SaldoH,isnull(SL2.WIP,0)WIP,ISNULL(SL2.WIPH,0)WIPH,isnull(SL3.BJ,0)BJ,isnull(SL3.BJH,0)BJH,isnull(SL4.BP,0)BP, " +
                "isnull(SL4.BPH,0)BPH,isnull(SL6.Adjust,0)Adjust,isnull(SL6.AdjustH,0)AdjustH,isnull(SL2.WIP,0)+isnull(SL3.BJ,0)+isnull(SL4.BP,0)+isnull(SL6.Adjust,0)total, " +
                "isnull(SL2.WIPH,0)+isnull(SL3.BJH,0)+isnull(SL4.BPH,0)+isnull(SL6.AdjustH,0) totalH,isnull(SL7.JualToko,0)JualToko,isnull(SL7.JualTokoH,0)JualTokoH, " +
                "isnull(SL8.JualDepo,0)JualDepo,isnull(SL8.JualDepoH,0)JualDepoH,isnull(SL9.JualMemo,0)JualMemo,isnull(SL9.JualMemoH,0)JualMemoH,isnull(SL5.Jual,0)Jual,isnull(SL5.JualH,0)JualH from ( " +
                "select Tebal,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,case when Saldo>0 then SaldoH/saldo  else 0 end avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,case when (awal+terima)>0 then (awalH+terimaH)/(awal+terima)*keluar else 0 end keluarH,saldo,saldoH, " +
                "    case when saldo1.Saldo>0 then CAST(SaldoH/saldo as int) else 0 end  avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar ,0 keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang, 0 awal,0 awalH,SUM(qty) terima, SUM(qty*HPP) terimaH,0 keluar,0 keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and  A.qty>0  and (" + PARTNO + " ) and  " +
                "           left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  group by B.ID ,B.Tebal,B.Lebar,B.Panjang,B.ID " +
                "            union all  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,0 as terima,0 as terimaH,SUM(qty)*-1 as keluar,0 as keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and A.qty<0 and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  group by B.ID,B.Tebal,B.Lebar,B.Panjang ,B.ID " +
                "            union all  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,SUM('+ @AwalT3 +' ) as awal,SUM(' + @AwalT3 + ' * '+ @AwalT3Price +') as awalH, 0 as terima,  " +
                "            0 as terimaH,0 as keluar,0 as keluarH   " +
                "            FROM SaldoInventoryBJ A, FC_Items B  where  A.itemid=B.ID and A.Rowstatus>-1 and (" + PARTNO + " ) and   " +
                "            A.YearPeriod ='+ @tahunAwalT3 +' group by B.ID,B.Tebal,B.Lebar,B.Panjang,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SL1  " +
                "left join  " +
                "(select ukuran, isnull(terima,0) WIP,isnull(terimaH,0) WIPH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,case when Saldo>0 then SaldoH/saldo  else 0 end avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,case when (awal+terima)>0 then (awalH+terimaH)/(awal+terima)*keluar else 0 end keluarH,saldo,saldoH, " +
                "    case when saldo1.Saldo>0 then CAST(SaldoH/saldo as int) else 0 end  avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar ,0 keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang, 0 awal,0 awalH,SUM(qty) terima, SUM(qty*HPP) terimaH,0 keluar,0 keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and  A.qty>0  and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and A.Process=''direct'' group by B.ID ,B.Tebal,B.Lebar,B.Panjang,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2 ) SL2 on SL1.ukuran=SL2.ukuran  " +
                "left join " +
                "(select ukuran, isnull(terima,0) BJ,isnull(terimaH,0) BJH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,case when Saldo>0 then SaldoH/saldo  else 0 end avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,case when (awal+terima)>0 then (awalH+terimaH)/(awal+terima)*keluar else 0 end keluarH,saldo,saldoH, " +
                "    case when saldo1.Saldo>0 then CAST(SaldoH/saldo as int) else 0 end  avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar ,0 keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang, 0 awal,0 awalH,SUM(qty) terima, SUM(qty*HPP) terimaH,0 keluar,0 keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and  A.qty>0  and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and (A.Keterangan like ''%-3-%'' or A.Keterangan like ''%-W-%'' or A.Keterangan like ''%-m-%'') group by B.ID ,B.Tebal,B.Lebar,B.Panjang,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2 ) SL3 on SL1.ukuran=SL3.ukuran  " +
                "left join " +
                "(select ukuran, isnull(terima,0) BP,isnull(terimaH,0) BPH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,case when Saldo>0 then SaldoH/saldo  else 0 end avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,case when (awal+terima)>0 then (awalH+terimaH)/(awal+terima)*keluar else 0 end keluarH,saldo,saldoH, " +
                "    case when saldo1.Saldo>0 then CAST(SaldoH/saldo as int) else 0 end  avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar ,0 keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang, 0 awal,0 awalH,SUM(qty) terima, SUM(qty*HPP) terimaH,0 keluar,0 keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and  A.qty>0  and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and (A.Keterangan like ''%-P-%'' ) group by B.ID ,B.Tebal,B.Lebar,B.Panjang,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2 ) SL4 on SL1.ukuran=SL4.ukuran  " +
                "left join  " +
                "(select ukuran, isnull(terima,0) Adjust,isnull(terimaH,0) AdjustH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,case when Saldo>0 then SaldoH/saldo  else 0 end avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,case when (awal+terima)>0 then (awalH+terimaH)/(awal+terima)*keluar else 0 end keluarH,saldo,saldoH, " +
                "    case when saldo1.Saldo>0 then CAST(SaldoH/saldo as int) else 0 end  avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar ,0 keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang, 0 awal,0 awalH,SUM(qty) terima, SUM(qty*HPP) terimaH,0 keluar,0 keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and  A.qty>0  and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and (A.Process=''adjust'' or A.Process=''retur'') group by B.ID ,B.Tebal,B.Lebar,B.Panjang,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2 ) SL6 on SL1.ukuran=SL6.ukuran  " +
                "left join " +
                "(select ukuran, isnull(keluar ,0) Jual,isnull(keluarH,0) JualH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,0 avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,saldoH, " +
                "    0 avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar , SUM(keluarH)keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,0 as terima,0 as terimaH,SUM(qty)*-1 as keluar,SUM(HPP*qty)*-1 as keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and A.qty<0 and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and A.Process=''kirim'' group by B.ID,B.Tebal,B.Lebar,B.Panjang ,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2 ) SL5 on SL1.ukuran=SL5.ukuran  " +
                "left join  " +
                "(select ukuran, isnull(keluar ,0) JualToko,isnull(keluarH,0) JualTokoH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,0 avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,saldoH, " +
                "    0 avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar , SUM(keluarH)keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,0 as terima,0 as terimaH,SUM(qty)*-1 as keluar,SUM(HPP*qty)*-1 as keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and A.qty<0 and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and A.Process=''kirim'' and A.Keterangan like ''%/sj/%'' group by B.ID,B.Tebal,B.Lebar,B.Panjang ,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2)SL7 on SL1.ukuran=SL7.ukuran  " +
                "left join " +
                "(select ukuran, isnull(keluar ,0) JualDepo,isnull(keluarH,0) JualDepoH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,0 avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,saldoH, " +
                "    0 avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar , SUM(keluarH)keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,0 as terima,0 as terimaH,SUM(qty)*-1 as keluar,SUM(HPP*qty)*-1 as keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and A.qty<0 and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and A.Process=''kirim'' and A.Keterangan like ''%/to/%'' group by B.ID,B.Tebal,B.Lebar,B.Panjang ,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2 )SL8  on SL1.ukuran=SL8.ukuran  " +
                "left join " +
                "(select ukuran, isnull(keluar ,0) Jualmemo,isnull(keluarH,0) JualmemoH from ( " +
                "select rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, sum(awal)awal,sum(awalH)awalH,sum(terima)terima,sum(terimaH)terimaH, " +
                "sum(keluar)keluar,sum(keluarH)keluarH,sum(Saldo)Saldo,sum(SaldoH)SaldoH from (  " +
                "    select *,0 avgprice from ( " +
                "    select PartNo,Itemid,tebal,lebar,panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,awalH+terimaH-keluarH saldoH from (  " +
                "    select I.PartNo,Itemid,I.tebal,I.lebar,I.panjang,awal,awalH,terima,terimaH,keluar,keluarH,saldo,saldoH, " +
                "    0 avgprice from (  " +
                "        select itemid, tebal,lebar,panjang,SUM(awal)awal,SUM(awalH)awalH,SUM(terima)terima,SUM(terimaH)terimaH, SUM(keluar)keluar , SUM(keluarH)keluarH , " +
                "        isnull(SUM(awal),0)+isnull(SUM(terima),0)- isnull(SUM(keluar),0) Saldo,SUM(awalH)+SUM(terimaH)- SUM(keluarH)SaldoH from (  " +
                "            select B.ID Itemid,B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awalH,0 as terima,0 as terimaH,SUM(qty)*-1 as keluar,SUM(HPP*qty)*-1 as keluarH  " +
                "            from vw_KartuStockBJnew A, FC_Items B  where A.ItemID =B.ID  and A.qty<0 and (" + PARTNO + " ) and  " +
                "            left(convert(varchar,tanggal,112),6)= '+ @thnbln +'  and A.Process=''kirim'' and A.Keterangan like ''%/sjmo/%'' group by B.ID,B.Tebal,B.Lebar,B.Panjang ,B.ID " +
                "        ) as saldo where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang,Itemid  " +
                "    ) as saldo1 inner join FC_Items I on saldo1.Itemid=I.ID where awal<>0 or terima<>0 or keluar<>0 ) as S2 )S3  " +
                ") as saldo1 where awal<>0 or terima<>0 or keluar<>0 group by tebal,lebar,panjang ) as SLA2)SL9 on SL1.ukuran=SL9.ukuran  " +
                "order by  Tebal ' exec sp_executesql @sqlWIP, N''";
            return strSQL;
        }

        public string ViewStockKubik(string Bln, string Thn)
        {
            string strSQL = string.Empty;
            //string prd = PartNo.Substring(0, 3);
            string SaldoLaluQty = string.Empty;
            string SaldoLaluPrice = string.Empty;
            string periodeTahun = string.Empty;
            int fldBln = 0;
            int fldBln2 = 0;
            int fldBln3 = 0;

            fldBln = Convert.ToInt16(Bln);
            if (Bln == "11")
            {
                fldBln2 = 12;
                fldBln3 = 1;
            }
            else
                if (Bln == "12")
            {
                fldBln2 = 1;
                fldBln3 = 2;
            }
            else
            {
                fldBln2 = Convert.ToInt16(Bln) + 1;
                fldBln3 = Convert.ToInt16(Bln) + 2;
            }
            #region prepared data
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
            #endregion
            string bln = fldBln.ToString();
            string bln2 = fldBln2.ToString();
            string bln3 = fldBln3.ToString();
            string s = new string('0', (2 - bln.Length));
            string s2 = new string('0', (2 - bln2.Length));
            string s3 = new string('0', (2 - bln3.Length));
            int lastDay = DateTime.DaysInMonth((Convert.ToInt16(periodeTahun)), fldBln);
            string d = new string('0', (2 - lastDay.ToString().Length));
            string lastperiode = string.Empty;

            string Periode = string.Empty;
            string Periode2 = string.Empty;
            string Periode3 = string.Empty;

            Periode = Thn + s + bln;
            if (bln == "11")
            {
                Periode2 = Thn + s2 + bln2;
                Periode3 = (Convert.ToInt32(Thn) + 1).ToString() + s3 + bln3;
            }
            if (bln == "12")
            {
                Periode2 = (Convert.ToInt32(Thn) + 1).ToString() + s2 + bln2;
                Periode3 = (Convert.ToInt32(Thn) + 1).ToString() + s3 + bln3;
            }
            if (bln != "11" && bln2 != "12")
            {
                Periode2 = Thn + s2 + bln2;
                Periode3 = Thn + s3 + bln3;
            }

            if (Convert.ToInt32(bln) == 1)
                lastperiode = (Convert.ToInt32(Thn) - 1).ToString() + 12;
            else
                lastperiode = Thn + (Convert.ToInt32(bln) - 1).ToString().PadLeft(2, '0');
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            //string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            strSQL =
                "declare @Bulan1 varchar(6) " +
                "declare @Bulan2 varchar(6) " +
                "declare @Bulan3 varchar(6) " +
                "declare @tahunAwalT3 varchar(6) " +
                "declare @AwalT3 varchar(6) " +
                "declare @AwalT1 varchar(6) " +
                "declare @Bulan1WIP varchar(6) " +
                "declare @Bulan2WIP varchar(6) " +
                "declare @Bulan3WIP varchar(6) " +
                "select @tahunAwalT3='" + periodeTahun + "' " +
                "select @AwalT3 ='" + SaldoLaluQty + "' " +
                "select @AwalT1 ='" + lastperiode + "' " +
                "select @Bulan1 ='" + Periode + "' " +
                "select @Bulan2 ='" + Periode2 + "' " +
                "select @Bulan3 ='" + Periode3 + "' " +
                "declare @sourceWIP1 varchar(max) " +
                "declare @sourceWIP2 varchar(max) " +
                "declare @sourceWIP3 varchar(max) " +
                "declare @AwalT12 varchar(6) " +
                "declare @AwalT13 varchar(6) " +
                "declare @thnT1 varchar(4) " +
                "declare @blnT1 int " +
                "declare @tglawalT1 varchar(max) " +
                "select @thnT1=LEFT(@awalt1,4) " +
                "select @blnT1=RIGHT(@AwalT1,2) " +
                "select @tglawalT1=@thnT1 + '-' +REPLACE(STR(@blnT1, 2), SPACE(1), '0') + '-01' " +
                "select @AwalT12=LEFT(convert(char, DATEADD(month,1,@tglawalT1),112),6) " +
                "select @AwalT13=LEFT(convert(char, DATEADD(month,2,@tglawalT1),112),6) " +
                "declare @AwalT32 varchar(6) " +
                "declare @AwalT33 varchar(6) " +
                "declare @tahunAwalT32 varchar(6) " +
                "declare @tahunAwalT33 varchar(6) " +
                "declare @thnT3 varchar(4) " +
                "declare @blnT3 int " +
                "declare @tglawalT3 varchar(max) " +
                "select @thnT3=LEFT(@Bulan1,4) " +
                "select @blnT3=RIGHT(@Bulan1,2) " +
                "if right(@Bulan1,2)='01' begin set @AwalT32='janqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='02' begin set @AwalT32='febqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='03' begin set @AwalT32='marqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='04' begin set @AwalT32='aprqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='05' begin set @AwalT32='meiqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='06' begin set @AwalT32='junqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='07' begin set @AwalT32='julqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='08' begin set @AwalT32='aguqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='09' begin set @AwalT32='sepqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='10' begin set @AwalT32='oktqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='11' begin set @AwalT32='novqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan1,2)='12' begin set @AwalT32='desqty' set @tahunAwalT32=left(@Bulan1,4) end " +
                "if right(@Bulan2,2)='01' begin set @AwalT33='janqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='02' begin set @AwalT33='febqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='03' begin set @AwalT33='marqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='04' begin set @AwalT33='aprqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='05' begin set @AwalT33='meiqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='06' begin set @AwalT33='junqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='07' begin set @AwalT33='julqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='08' begin set @AwalT33='aguqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='09' begin set @AwalT33='sepqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='10' begin set @AwalT33='oktqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='11' begin set @AwalT33='novqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "if right(@Bulan2,2)='12' begin set @AwalT33='desqty' set @tahunAwalT33=left(@Bulan2,4) end " +
                "declare @sqlWIP nvarchar(max) " +
                "if @Bulan1<'201411' begin select @sourceWIP1='vw_kartustockWIPOld' select @Bulan1WIP='190001' end else begin select @sourceWIP1='vw_KartuStockWIP' select @Bulan1WIP=@Bulan1 end " +
                "if @Bulan2<'201411' begin select @sourceWIP2='vw_kartustockWIPOld' select @Bulan2WIP='190001' end else begin select @sourceWIP2='vw_KartuStockWIP' select @Bulan2WIP=@Bulan2 end " +
                "if @Bulan3<'201411' begin select @sourceWIP3='vw_kartustockWIPOld' select @Bulan3WIP='190001' end else begin select @sourceWIP3='vw_KartuStockWIP' select @Bulan3WIP=@Bulan3 end " +
                "/*OutPut Produksi*/ " +
                "set @sqlWIP=' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempOutPutProduksi]'') AND type in (N''U'')) DROP TABLE [dbo].[tempOutPutProduksi] " +
                "select * into tempOutPutProduksi from ( " +
                "select ''OutPut Produksi'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from ' + @sourceWIP3 + ' A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan3 +'  and Qty>0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from ' + @sourceWIP2 + ' A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan2 +'   and Qty>0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @sourceWIP1 + ' A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan1 +'  and Qty>0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,SUM(saldo) as awal, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  T1_SaldoPerLokasi as  A, FC_Items B   " +
                "where  A.rowstatus>-1 and A.thnbln='+@AwalT1 +' and itemid =B.ID and A.ItemID =B.ID group by B.Tebal,B.Lebar,B.Panjang  " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from vw_KartustockWIP2 A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+@Bulan3WIP +'  AND qty>0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from vw_KartustockWIP2 A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+@Bulan2WIP +'  AND qty>0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from vw_KartustockWIP2 A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+@Bulan1WIP +' AND qty>0 group by B.Tebal,B.Lebar,B.Panjang   " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1  " +
                ") as SA ' " +
                "exec sp_executesql @sqlWIP, N'' " +
                "set @sqlWIP=' " +
                "/*Penyerahan Tahap1*/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempPenyerahanProduksi]'') AND type in (N''U'')) DROP TABLE [dbo].[tempPenyerahanProduksi] " +
                "select * into tempPenyerahanProduksi from ( " +
                "select ''Penyerahan ke Tahap 3'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, 0 as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty)*-1 as tQTY3 from ' + @sourceWIP3 + ' A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan3 +'   AND qty<0 and process=''trans_in'' and A.Lokasi<>''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty)*-1 as tQTY2,0 as tQTY3 from ' + @sourceWIP2 + ' A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan2 +'   AND qty<0 and process=''trans_in'' and A.Lokasi<>''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)*-1 as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @sourceWIP1 + ' A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan1 +'  AND qty<0 and process=''trans_in'' and A.Lokasi<>''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty)*-1 as tQTY3 from vw_KartustockWIP2 A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan3WIP +' AND qty<0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty)*-1 as tQTY2,0 as tQTY3 from vw_KartustockWIP2 A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan2WIP +' AND qty<0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)*-1 as tQTY1, 0 as tQTY2, 0 as tQTY3 from vw_KartustockWIP2 A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan1WIP +'  AND qty<0 group by B.Tebal,B.Lebar,B.Panjang   " +
                ") as saldo group by Tebal,Lebar,Panjang  " +
                ") as saldo1  " +
                ") as SA ' " +
                "exec sp_executesql @sqlWIP, N'' " +
                "set @sqlWIP=' " +
                "/*Waste Penyerahan Tahap1*/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempWasteProduksi]'') AND type in (N''U'')) DROP TABLE [dbo].[tempWasteProduksi] " +
                "select * into tempWasteProduksi from ( " +
                "select ''Penyerahan ke Tahap 3'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, 0 as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,((Tebal/1000)*(Lebar/1000)*(Panjang/1000))-((Tebal1/1000)*(Lebar1/1000)*(Panjang1/1000))as volume,SUM(awal) as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar as Lebar,B.Panjang as Panjang,C.Tebal as Tebal1,C.Lebar as Lebar1,C.Panjang as Panjang1 ,0 as awal,0 as tQTY1, " +
                "0 as tQTY2,SUM(qty)*-1 as tQTY3 from ' + @sourceWIP3 + ' A inner join FC_Items B  on A.itemid0=B.ID inner join FC_items C on A.itemid=C.ID  " +
                "where left(convert(varchar,tanggal,112),6)='+ @Bulan3 +'   AND qty<0 and process=''trans_in'' and A.Lokasi<>''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang,C.tebal,C.Lebar,C.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar as Lebar,B.Panjang as Panjang,C.Tebal as Tebal1,C.Lebar as Lebar1,C.Panjang as Panjang1 ,0 as awal,0 as tQTY1, " +
                "SUM(qty)*-1 as tQTY2,0 as tQTY3 from ' + @sourceWIP2 + ' A inner join FC_Items B  on A.itemid0=B.ID inner join FC_items C on A.itemid=C.ID   " +
                "where left(convert(varchar,tanggal,112),6)='+ @Bulan2 +'   AND qty<0 and process=''trans_in'' and A.Lokasi<>''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang,C.tebal,C.Lebar,C.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar as Lebar,B.Panjang as Panjang,C.Tebal as Tebal1,C.Lebar as Lebar1,C.Panjang as Panjang1 ,0 as awal,SUM(qty)*-1 as tQTY1,  " +
                "0 as tQTY2, 0 as tQTY3 from ' + @sourceWIP1 + ' A inner join FC_Items B  on A.itemid0=B.ID inner join FC_items C on A.itemid=C.ID   " +
                "where left(convert(varchar,tanggal,112),6)='+ @Bulan1 +'  AND qty<0 and process=''trans_in'' and A.Lokasi<>''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang,C.tebal,C.Lebar,C.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar as Lebar,B.Panjang as Panjang,C.Tebal as Tebal1,C.Lebar as Lebar1,C.Panjang as Panjang1 ,0 as awal,0 as tQTY1, " +
                "0 as tQTY2,SUM(qty)*-1 as tQTY3 from vw_KartustockWIP2 A inner join FC_Items B  on A.itemid0=B.ID inner join FC_items C on A.itemid=C.ID   " +
                "where left(convert(varchar,tanggal,112),6)='+ @Bulan3WIP +' AND qty<0 group by B.Tebal,B.Lebar,B.Panjang,C.tebal,C.Lebar,C.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar as Lebar,B.Panjang as Panjang,C.Tebal as Tebal1,C.Lebar as Lebar1,C.Panjang as Panjang1 ,0 as awal,0 as tQTY1, " +
                "SUM(qty)*-1 as tQTY2,0 as tQTY3 from vw_KartustockWIP2 A inner join FC_Items B  on A.itemid0=B.ID inner join FC_items C on A.itemid=C.ID   " +
                "where left(convert(varchar,tanggal,112),6)='+ @Bulan2WIP +' AND qty<0 group by B.Tebal,B.Lebar,B.Panjang,C.tebal,C.Lebar,C.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar as Lebar,B.Panjang as Panjang,C.Tebal as Tebal1,C.Lebar as Lebar1,C.Panjang as Panjang1 ,0 as awal,SUM(qty)*-1 as tQTY1,  " +
                "0 as tQTY2, 0 as tQTY3 from vw_KartustockWIP2 A inner join FC_Items B  on A.itemid0=B.ID inner join FC_items C on A.itemid=C.ID   " +
                "where left(convert(varchar,tanggal,112),6)='+ @Bulan1WIP +' AND qty<0 group by B.Tebal,B.Lebar,B.Panjang,C.tebal,C.Lebar,C.Panjang   " +
                ") as saldo group by Tebal,Lebar,Panjang,Tebal1,Lebar1,Panjang1  " +
                ") as saldo1  " +
                ") as SA ' " +
                "exec sp_executesql @sqlWIP, N'' " +
                "set @sqlWIP=' " +
                "/*Barang Sample*/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempSample]'') AND type in (N''U'')) DROP TABLE [dbo].[tempSample] " +
                "select * into tempSample from ( " +
                "select ''Barang Sample'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty)*-1 as tQTY3 from ' + @sourceWIP3 + ' A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan3 +'   AND qty<0 and process=''trans_in'' and A.Lokasi=''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty)*-1 as tQTY2,0 as tQTY3 from ' + @sourceWIP2 + ' A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan2 +'   AND qty<0 and process=''trans_in'' and A.Lokasi=''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)*-1 as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @sourceWIP1 + ' A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan1 +'  AND qty<0 and process=''trans_in'' and A.Lokasi=''q99'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty)*-1 as tQTY3 from vw_KartustockWIP A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan3WIP +'   AND qty<0 and process=''lari'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty)*-1 as tQTY2,0 as tQTY3 from vw_KartuStockWIP A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan2WIP +'   AND qty<0 and process=''lari'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)*-1 as tQTY1, 0 as tQTY2, 0 as tQTY3 from vw_KartuStockWIP A, FC_Items B   " +
                "where A.ItemID0 =B.ID and left(convert(varchar,tanggal,112),6)='+ @Bulan1WIP +'  AND qty<0 and process=''lari'' " +
                "group by B.Tebal,B.Lebar,B.Panjang  " +
                ") as saldo group by Tebal,Lebar,Panjang  " +
                ") as saldo1  " +
                ") as SA'  " +
                "exec sp_executesql @sqlWIP, N'' " +
                "set @sqlWIP=' " +
                "/*Stock WIP Tahap 1*/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempStockProduksi]'') AND type in (N''U'')) DROP TABLE [dbo].[tempStockProduksi] " +
                "select * into tempStockProduksi from ( " +
                "select ''Info Stock WIP / Tahap 1'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, SUM(awal)+sum(tQTY1) as tQTY1 " +
                ",SUM(awal2)+sum(tQTY2) as tQTY2,SUM(awal3)+sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awal2,0 as awal3,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from ' + @sourceWIP3 + ' A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan3 +'   group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awal2,0 as awal3,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from ' + @sourceWIP2 + ' A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan2 +'   group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awal2,0 as awal3,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @sourceWIP1 + ' A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan1 +'  group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awal2,0 as awal3,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from vw_KartustockWIP2 A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan3WIP +'   group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awal2,0 as awal3,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from vw_KartustockWIP2 A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan2WIP +'   group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awal2,0 as awal3,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from vw_KartustockWIP2 A, FC_Items B  where A.ItemID0 =B.ID and  " +
                "left(convert(varchar,tanggal,112),6)='+ @Bulan1WIP +'  group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,SUM(saldo) as awal,0 as awal2,0 as awal3,0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  T1_SaldoPerLokasi as  A, FC_Items B   " +
                "where  A.rowstatus>-1 and A.thnbln='+@AwalT1 +' and itemid =B.ID and A.ItemID =B.ID group by B.Tebal,B.Lebar,B.Panjang  " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(saldo) as awal2,0 as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  T1_SaldoPerLokasi as  A, FC_Items B   " +
                "where  A.rowstatus>-1 and A.thnbln='+@AwalT12 +' and itemid =B.ID and A.ItemID =B.ID group by B.Tebal,B.Lebar,B.Panjang  " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as awal2,SUM(saldo) as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  T1_SaldoPerLokasi as  A, FC_Items B   " +
                "where  A.rowstatus>-1 and A.thnbln='+@AwalT13 +' and itemid =B.ID and A.ItemID =B.ID group by B.Tebal,B.Lebar,B.Panjang  " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1  " +
                ") as SA  " +
                "/*Proses Rework*/' " +
                "exec sp_executesql @sqlWIP, N'' " +
                "declare @source1 varchar(max) " +
                "declare @source2 varchar(max) " +
                "declare @source3 varchar(max) " +
                "declare @sql nvarchar(max) " +
                "if @Bulan1<'201411' begin select @source1='vw_kartustockBJ' end else begin select @source1='vw_KartuStockBJNew' end " +
                "if @Bulan2<'201411' begin select @source2='vw_kartustockBJ' end else begin select @source2='vw_kartustockBJnew' end " +
                "if @Bulan3<'201411' begin select @source3='vw_kartustockBJ' end else begin select @source3='vw_kartustockBJnew' end " +
                "set @sql =' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempRework]'') AND type in (N''U'')) DROP TABLE [dbo].[tempRework] " +
                "select * into tempRework from ( " +
                "select ''Proses Rework'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, 0 as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from ' + @source3 +' A, FC_Items B   " +
                "where A.ItemID =B.ID and left(convert(varchar,tanggal,112),6)='+@Bulan3 +' AND qty>0 and Process<>''direct'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from ' + @source2 +' A, FC_Items B   " +
                "where A.ItemID =B.ID and left(convert(varchar,tanggal,112),6)='+@Bulan2 +' AND qty>0 and Process<>''direct'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @source1 +' A, FC_Items B   " +
                "where A.ItemID =B.ID and left(convert(varchar,tanggal,112),6)='+@Bulan1 +' AND qty>0 and Process<>''direct'' " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                " " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1 " +
                ") as SA  " +
                "/*Stock Transit Tahap1*/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempStockTransit]'') AND type in (N''U'')) DROP TABLE [dbo].[tempStockTransit] " +
                "select * into tempStockTransit from ( " +
                "select ''Stock Transit Tahap 1'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, 0 as awal, sum(tQTY1) as tQTY1 ,sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3   " +
                "from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(A.QtyIn-A.QtyOut) as tQTY3 from t1_serah A left join  FC_Items B ON A.ItemID=B.ID  " +
                "left join FC_Lokasi L on A.LokID =L.ID where  A.Status >-1 and  A.QtyIn>A.QtyOut and A.DestID in(select ID from BM_Destacking  " +
                "where TglProduksi >=''12/1/2013'') and Lokasi  in(''b99'',''c99'')  and left(convert(varchar,A.TglSerah,112),6)='+@Bulan3 +'  group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(A.QtyIn-A.QtyOut) as tQTY2,0 as tQTY3 from t1_serah A left join  FC_Items B ON A.ItemID=B.ID  " +
                "left join FC_Lokasi L on A.LokID =L.ID where  A.Status >-1 and  A.QtyIn>A.QtyOut and A.DestID in(select ID from BM_Destacking  " +
                "where TglProduksi >=''12/1/2013'') and Lokasi in(''b99'',''c99'')  and left(convert(varchar,A.TglSerah,112),6)='+@Bulan2 +'  group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(A.QtyIn-A.QtyOut) as tQTY1, 0 as tQTY2, 0 as tQTY3 from t1_serah A left join  FC_Items B ON A.ItemID=B.ID  " +
                "left join FC_Lokasi L on A.LokID =L.ID where  A.Status >-1 and  A.QtyIn>A.QtyOut and A.DestID in(select ID from BM_Destacking  " +
                "where TglProduksi >=''12/1/2013'') and Lokasi in(''b99'',''c99'') and left(convert(varchar,A.TglSerah,112),6)='+@Bulan1 +'  group by B.Tebal,B.Lebar,B.Panjang   " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1  " +
                ") as SA  " +
                "/*Pengeluaran stok (Tahap 3)*/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempPengeluaran]'') AND type in (N''U'')) DROP TABLE [dbo].[tempPengeluaran] " +
                "select * into tempPengeluaran from ( " +
                "select ''Pengeluaran OK'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty)*-1 as tQTY3 from ' + @source3 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and left(convert(varchar,tanggal,112),6)='+@Bulan3 +'  AND qty<0  " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty)*-1 as tQTY2,0 as tQTY3 from ' + @source2 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and left(convert(varchar,tanggal,112),6)='+@Bulan2 +'  AND qty<0  " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)*-1 as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @source1 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and left(convert(varchar,tanggal,112),6)='+@Bulan1 +'  AND qty<0  " +
                "group by B.Tebal,B.Lebar,B.Panjang   " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1 " +
                "union all " +
                "select ''Pengeluaran BP'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik,  tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3  from ( " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty)*-1 as tQTY3 from ' + @source3 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-P-%'' ) and left(convert(varchar,tanggal,112),6)='+@Bulan3 +'  AND qty<0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty)*-1 as tQTY2,0 as tQTY3 from ' + @source2 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-P-%'' ) and left(convert(varchar,tanggal,112),6)='+@Bulan2 +'  AND qty<0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all   " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)*-1 as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @source1 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-P-%'' ) and left(convert(varchar,tanggal,112),6)='+@Bulan1 +'  AND qty<0 group by B.Tebal,B.Lebar,B.Panjang  " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1 " +
                "union all " +
                "select ''Pengeluaran BS'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubik,  tQTY1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, sum(tQTY1) as tQTY1 " +
                ",sum(tQTY2) as tQTY2,sum(tQTY3) as tQTY3 from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,0 as tQTY2,SUM(qty)*-1 as tQTY3 from ' + @source3 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-S-%'' ) and left(convert(varchar,tanggal,112),6)='+@Bulan3 +'  AND qty<0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as tQTY1,SUM(qty)*-1 as tQTY2,0 as tQTY3 from ' + @source2 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-S-%'' ) and left(convert(varchar,tanggal,112),6)='+@Bulan2 +'  AND qty<0 group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)*-1 as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @source1 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-S-%'' ) and left(convert(varchar,tanggal,112),6)='+@Bulan1 +'  AND qty<0 group by B.Tebal,B.Lebar,B.Panjang  " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1  " +
                ") as SA ' " +
                "exec sp_executesql @sql, N'' " +
                "set @sql =' " +
                "/*Sisa stok akhir*/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[tempSaldoAkhir]'') AND type in (N''U'')) DROP TABLE [dbo].[tempSaldoAkhir]  " +
                "select * into tempSaldoAkhir from ( " +
                "select ''Stock Akhir OK'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, awal*volume  as AwalKubik, tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, SUM(awal)+sum(tQTY1) as tQTY1 " +
                ",SUM(awal2)+sum(tQTY2) as tQTY2,SUM(awal3)+sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from ' + @source3 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and left(convert(varchar,tanggal,112),6)='+ @Bulan3 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from ' + @source2 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and left(convert(varchar,tanggal,112),6)='+ @Bulan2 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @source1 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and left(convert(varchar,tanggal,112),6)='+ @Bulan1 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,SUM('+ @AwalT3 +') as awal, 0 as awal2, 0 as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt3 +' and A.ItemID =B.ID and (B.PartNo  like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') group by B.Tebal,B.Lebar,B.Panjang  " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, SUM('+ @AwalT32 +') as awal2, 0 as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt32 +' and A.ItemID =B.ID and (B.PartNo  like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') group by B.Tebal,B.Lebar,B.Panjang " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, SUM('+ @AwalT33 +') as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt33 +' and A.ItemID =B.ID and (B.PartNo  like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') group by B.Tebal,B.Lebar,B.Panjang " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1  " +
                "union all " +
                "select ''Stock Akhir BP'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, awal*volume  as AwalKubik,  tqty1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, SUM(awal)+sum(tQTY1) as tQTY1 " +
                ",SUM(awal2)+sum(tQTY2) as tQTY2,SUM(awal3)+sum(tQTY3) as tQTY3  from ( " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from ' + @source3 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-P-%'' ) and left(convert(varchar,tanggal,112),6)='+ @Bulan3 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from ' + @source2 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-P-%'' ) and left(convert(varchar,tanggal,112),6)='+@Bulan2 +'group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all   " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @source1 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-P-%'' ) and left(convert(varchar,tanggal,112),6)='+ @Bulan1 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,SUM('+ @AwalT3 +') as awal, 0 as awal2, 0 as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt3 +' and A.ItemID =B.ID and (B.PartNo  like ''%-P-%'') group by B.Tebal,B.Lebar,B.Panjang  " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, SUM('+ @AwalT32 +') as awal2, 0 as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt32 +' and A.ItemID =B.ID and (B.PartNo  like ''%-P-%'') group by B.Tebal,B.Lebar,B.Panjang " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, SUM('+ @AwalT33 +') as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt33 +' and A.ItemID =B.ID and (B.PartNo  like ''%-P-%'') group by B.Tebal,B.Lebar,B.Panjang  " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1  " +
                "union all " +
                "select ''Stock Akhir BS'' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' X '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +  " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, awal*volume  as AwalKubik,  tQTY1*volume as Kubik1, tqty2*volume as Kubik2 " +
                ", tqty3*volume as Kubik3 from (  " +
                "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal, SUM(awal)+sum(tQTY1) as tQTY1 " +
                ",SUM(awal2)+sum(tQTY2) as tQTY2,SUM(awal3)+sum(tQTY3) as tQTY3  from (  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,0 as tQTY1,0 as tQTY2,SUM(qty) as tQTY3 from ' + @source3 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-S-%'' ) and left(convert(varchar,tanggal,112),6)='+ @Bulan3 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,0 as tQTY1,SUM(qty) as tQTY2,0 as tQTY3 from ' + @source2 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-S-%'' ) and left(convert(varchar,tanggal,112),6)='+ @Bulan2 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, 0 as awal3,SUM(qty) as tQTY1, 0 as tQTY2, 0 as tQTY3 from ' + @source1 +' A, FC_Items B  where A.ItemID =B.ID and  " +
                "(B.PartNo like ''%-S-%'' ) and left(convert(varchar,tanggal,112),6)='+ @Bulan1 +' group by B.Tebal,B.Lebar,B.Panjang   " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,SUM('+ @AwalT3 +') as awal, 0 as awal2, 0 as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt3 +' and A.ItemID =B.ID and (B.PartNo  like ''%-S-%'') group by B.Tebal,B.Lebar,B.Panjang  " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, SUM('+ @AwalT32 +') as awal2, 0 as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt32 +' and A.ItemID =B.ID and (B.PartNo  like ''%-S-%'') group by B.Tebal,B.Lebar,B.Panjang " +
                "union all  " +
                "select B.Tebal,B.Lebar,B.Panjang,0 as awal, 0 as awal2, SUM('+ @AwalT33 +') as awal3, 0 as tQTY1, 0 as tQTY2, 0 as tQTY3 from  ( SELECT DISTINCT ItemID, YearPeriod,  " +
                "ItemTypeID, JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty,NovQty, DesQty  FROM SaldoInventoryBJ)as  A,  " +
                "FC_Items B  where  A.YearPeriod='+ @TahunAwalt33 +' and A.ItemID =B.ID and (B.PartNo  like ''%-S-%'') group by B.Tebal,B.Lebar,B.Panjang " +
                ") as saldo group by Tebal,Lebar,Panjang   " +
                ") as saldo1  " +
                ") as SA ' " +
                "exec sp_executesql @sql, N'' " +
                "/*Retrieve OutPut Produksi GRC di bawah 4mm*/ " +
                "select '1. OutPut Produksi' as GN1,'GRC di bawah 4mm' as GN2, ' ' as GN3,'0' as G1,'1' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempOutPutProduksi where tebal<4  " +
                "union all " +
                "/*Retrieve OutPut Produksi GRC 4mm*/ " +
                "select '1. OutPut Produksi' as GN1,'GRC 4mm' as GN2, ' ' as GN3,'0' as G1,'2' as G2,'A' as G3, isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempOutPutProduksi where tebal=4  " +
                "union all " +
                "/*Retrieve Stock Produksi di atas GRC 4mm*/ " +
                "select '1. OutPut Produksi' as GN1,'GRC di atas 4mm' as GN2, ' ' as GN3,'0' as G1,'3' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempOutPutProduksi  " +
                "where tebal>4 and tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 )   " +
                "union all " +
                "/*Retrieve OutPut Produksi ListplankGRC 4mm*/ " +
                "select '1. OutPut Produksi' as GN1,'Listplank' as GN2, ' ' as GN3,'0' as G1,'4' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempOutPutProduksi where (tebal>=8 and tebal<=9 )   " +
                "union all " +
                "/*Retrieve Penyerahan Produksi GRC di bawah 4mm*/ " +
                "select '2. Penyerahan ke Tahap 3' as GN1,'GRC di bawah 4mm' as GN2, ' ' as GN3,'1' as G1,'1' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPenyerahanProduksi where tebal<4  " +
                "union all " +
                "/*Retrieve Penyerahan Produksi GRC 4mm*/ " +
                "select '2. Penyerahan ke Tahap 3' as GN1,'GRC 4mm' as GN2, ' ' as GN3,'1' as G1,'2' as G2,'A' as G3, isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPenyerahanProduksi where tebal=4  " +
                "union all " +
                "/*Retrieve Penyerahan Produksi di atas GRC 4mm*/ " +
                "select '2. Penyerahan ke Tahap 3' as GN1,'GRC di atas 4mm' as GN2, ' ' as GN3,'1' as G1,'3' as G2,'A' as G3, isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPenyerahanProduksi  " +
                "where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 )   " +
                "union all " +
                "/*Retrieve Penyerahan Produksi ListplankGRC 4mm*/ " +
                "select '2. Penyerahan ke Tahap 3' as GN1,'Listplank' as GN2, ' ' as GN3,'1' as G1,'4' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPenyerahanProduksi where (tebal>=8 and tebal<=9 )   " +
                "/*Retrieve Stock Transit*/ " +
                "union all " +
                "select ' ' as GN1,'Penyerahan Sample dan Pelarian' as GN2, ' ' as GN3,'2' as G1,'0' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSample " +
                "/*Retrieve Waste Penyerahan Produksi*/ " +
                "union all " +
                "select ' ' as GN1,'Waste Penyerahan ke Tahap 3' as GN2, ' ' as GN3,'2' as G1,'1' as G2,'A' as G3,0 as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,0 as LQty1,isnull(SUM(Kubik1),0) as Kubik1,0 as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "0 as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempWasteProduksi " +
                "/*Retrieve Stock Produksi GRC di bawah 4mm*/ " +
                "union all " +
                "select '3. Info Stock WIP / Tahap 1' as GN1,'GRC di bawah 4mm' as GN2, ' ' as GN3,'3' as G1,'1' as G2,'A' as G3,0 as LAwal, " +
                "0 as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempStockProduksi where tebal<4  " +
                "union all " +
                "/*Retrieve Stock Produksi GRC 4mm*/ " +
                "select '3. Info Stock WIP / Tahap 1' as GN1,'GRC 4mm' as GN2, ' ' as GN3,'3' as G1,'3' as G2,'A' as G3,0 as LAwal, " +
                "0 as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempStockProduksi where tebal=4  " +
                "union all " +
                "/*Retrieve Stock Produksi di atas GRC 4mm*/ " +
                "select '3. Info Stock WIP / Tahap 1' as GN1,'GRC di atas 4mm' as GN2, ' ' as GN3,'3' as G1,'3' as G2,'A' as G3,0 as LAwal, " +
                "0 as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempStockProduksi  " +
                "where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 )   " +
                "union all " +
                "/*Retrieve Stock Produksi ListplankGRC 4mm*/ " +
                "select '3. Info Stock WIP / Tahap 1' as GN1,'Listplank' as GN2, ' ' as GN3,'3' as G1,'4' as G2,'A' as G3,0 as LAwal, " +
                "0 as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempStockProduksi where (tebal>=8 and tebal<=9 )   " +
                "/*Retrieve Proses Rework Tahap3*/ " +
                "union all " +
                "select '' as GN1,'Proses Rework' as GN2, ' ' as GN3,'4' as G1,'0' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempRework " +
                "/*Retrieve Stock Transit*/ " +
                "union all " +
                "select ' ' as GN1,'Stock Transit Tahap 1' as GN2, ' ' as GN3,'4' as G1,'1' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempStockTransit " +
                "/*Pengeluaran stok (Tahap 3)*/ " +
                "/*Retrieve Pengeluaran GRC di bawah 4mm*/ " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC di bawah 4mm' as GN2, 'OK' as GN3,'5' as G1,'1' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal<4 and kwalitas like '%ok%'  " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC di bawah 4mm' as GN2, 'BP' as GN3,'5' as G1,'1' as G2,'B' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal<4 and kwalitas like '%BP%'  " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC di bawah 4mm' as GN2, 'BS' as GN3,'5' as G1,'1' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal<4 and kwalitas like '%BS%'  " +
                "/*Retrieve Pengeluaran GRC 4mm*/ " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC 4mm' as GN2, 'OK' as GN3,'5' as G1,'2' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal=4 and kwalitas like '%ok%' " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC 4mm' as GN2, 'BP' as GN3,'5' as G1,'2' as G2,'B' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal=4 and kwalitas like '%BP%' " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC 4mm' as GN2, 'BS' as GN3,'5' as G1,'2' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal=4 and kwalitas like '%BS%'  " +
                "/*Retrieve Pengeluaran di atas GRC 4mm*/ " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC di atas 4mm' as GN2, 'OK' as GN3,'5' as G1,'3' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 )  and kwalitas like '%ok%' " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC di atas 4mm' as GN2, 'BP' as GN3,'5' as G1,'3' as G2,'B' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 ) and kwalitas like '%BP%' " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'GRC di atas 4mm' as GN2, 'BS' as GN3,'5' as G1,'3' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 ) and kwalitas like '%BS%' " +
                "/*Retrieve Pengeluaran ListplankGRC 4mm*/ " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'Listplank' as GN2, 'OK' as GN3,'5' as G1,'4' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where (tebal>=8 and tebal<=9 )  and kwalitas like '%ok%' " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'Listplank' as GN2, 'BP' as GN3,'5' as G1,'4' as G2,'B' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where (tebal>=8 and tebal<=9 ) and kwalitas like '%BP%' " +
                "union all " +
                "select '5. Pengeluaran' as GN1,'Listplank' as GN2, 'BS' as GN3,'5' as G1,'4' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempPengeluaran where (tebal>=8 and tebal<=9 ) and kwalitas like '%BS%' " +
                "/*Sisa stok akhir*/ " +
                "/*Retrieve Saldo Akhir GRC di bawah 4mm*/ " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC di bawah 4mm' as GN2, 'OK' as GN3,'6' as G1,'1' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal<4 and kwalitas like '%ok%'  " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC di bawah 4mm' as GN2, 'BP' as GN3,'6' as G1,'1' as G2,'B' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal<4 and kwalitas like '%BP%'  " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC di bawah 4mm' as GN2, 'BS' as GN3,'6' as G1,'1' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal<4 and kwalitas like '%BS%'  " +
                "/*Retrieve Saldo Akhir GRC 4mm*/ " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC 4mm' as GN2, 'OK' as GN3,'6' as G1,'2' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal=4 and kwalitas like '%ok%' " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC 4mm' as GN2, 'BP' as GN3,'6' as G1,'2' as G2,'B' as G3, isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal=4 and kwalitas like '%BP%' " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC 4mm' as GN2, 'BS' as GN3,'6' as G1,'2' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal=4 and kwalitas like '%BS%'  " +
                "/*Retrieve Saldo Akhir di atas GRC 4mm*/ " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC di atas 4mm' as GN2, 'OK' as GN3,'6' as G1,'3' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 )  and kwalitas like '%ok%' " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC di atas 4mm' as GN2, 'BP' as GN3,'6' as G1,'3' as G2,'B' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 ) and kwalitas like '%BP%' " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'GRC di atas 4mm' as GN2, 'BS' as GN3,'6' as G1,'3' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where tebal>4 and  tebal not  in (select tebal from fc_items where tebal>=8 AND tebal<=9 ) and kwalitas like '%BS%' " +
                "/*Retrieve Saldo Akhir ListplankGRC 4mm*/ " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'Listplank' as GN2, 'OK' as GN3,'6' as G1,'4' as G2,'A' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where (tebal>=8 and tebal<=9 )  and kwalitas like '%ok%' " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'Listplank' as GN2, 'BP' as GN3,'6' as G1,'4' as G2,'B' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where (tebal>=8 and tebal<=9 ) and kwalitas like '%BP%' " +
                "union all " +
                "select '6. Saldo Akhir' as GN1,'Listplank' as GN2, 'BS' as GN3,'6' as G1,'4' as G2,'C' as G3,isnull(sum(awal),0) as LAwal, " +
                "isnull(SUM(awalKubik),0) as awal,isnull(sum(TQty1),0) as LQty1,isnull(SUM(Kubik1),0) as Kubik1,isnull(sum(TQty2),0) as LQty2,isnull(SUM(Kubik2),0) as Kubik2, " +
                "isnull(sum(TQty3),0) as LQty3,isnull(SUM(Kubik3),0) as Kubik3 from tempSaldoAkhir where (tebal>=8 and tebal<=9 ) and kwalitas like '%BS%' " +
                "/*drop table tempSaldoAkhir " +
                "drop table tempPengeluaran " +
                "drop table tempStockProduksi " +
                "drop table tempPenyerahanProduksi*/";
            return strSQL;
        }

        public ArrayList ViewRekapReturPrev(string tgl1, string tgl2, string plant)
        {
            string strQuery = this.ViewRekapRetur(tgl1, tgl2, plant);
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sda = dta.RetrieveDataByString(strQuery);
            arrReport = new ArrayList();
            decimal sTotal = 0;
            if (dta.Error == string.Empty && sda.HasRows)
            {
                while (sda.Read())
                {
                    sTotal += Convert.ToDecimal(sda["Qty"].ToString());
                    arrReport.Add(new ReturnBJ
                    {
                        Tanggal = Convert.ToDateTime(sda["tgltrans"].ToString()),
                        SJNo = sda["SJNo"].ToString(),
                        OPNo = sda["OPNo"].ToString(),
                        Customer = sda["Customer"].ToString(),
                        PartNo = sda["PartNo"].ToString(),
                        Qty = Convert.ToDecimal(sda["Qty"].ToString()),
                        typeR = sda["typeR"].ToString(),
                        Total = sTotal
                    });
                }
            }
            return arrReport;
        }

        private string SortBy()
        {
            string strQuery = (HttpContext.Current.Session["SortBy"] != null) ? HttpContext.Current.Session["SortBy"].ToString() : "order by T3_Retur.Customer";
            return strQuery;
        }

        public string ViewRekapRetur(string tgl1, string tgl2, string plant)
        {
            #region depreciated query
            //string strsql = "select CONVERT(varchar,T3_Retur.TglTrans,103) as tgltrans," +
            //                "T3_Retur.Customer,T3_Retur.SJNo,T3_Retur.Qty,FC_Items.PartNo,T3_Retur.OPNo " +
            //               "from T3_Simetris " +
            //               "left join T3_Serah on T3_Simetris.SerahID=T3_Serah.ID " +
            //               "left join t3_retur on T3_Serah.LokID=T3_Retur.LokID and T3_Serah.ItemID=T3_Retur.ItemID and T3_Retur.TglTrans=T3_Simetris.TglTrans " +
            //               "left join FC_Items on T3_Retur.ItemID=FC_Items.ID " +
            //               "where T3_Serah.LokID in (select ID from FC_Lokasi where Lokasi='R99') " +
            //               "and convert(varchar,t3_retur.tgltrans,112)>='" + tgl1 + "' and  convert(varchar,t3_retur.tgltrans,112)<='" +
            //                   tgl2 + "' " + this.SortBy();

            //string strsql = "select CONVERT(varchar,T3_Retur.TglTrans,103) as tgltrans," +
            //    "T3_Retur.Customer,T3_Retur.SJNo,T3_Retur.Qty,FC_Items.PartNo,T3_Retur.OPNo " +
            //    "from T3_Retur " +
            //    "left join FC_Items on T3_Retur.ItemID=FC_Items.ID " +
            //    "where T3_Retur.RowStatus > -1 and convert(varchar,t3_retur.tgltrans,112)>='" + tgl1 + "' and  convert(varchar,t3_retur.tgltrans,112)<='" +
            //     tgl2 + "' " + this.SortBy();
            #endregion
            string strsql = "declare @plant1 varchar(20) " +
                "declare @plant2 varchar(20)  " +
                "set @plant1='" + plant + "' " +
                "if @plant1='Karawang' " +
                "begin " +
                "    set @plant1='Krwg' " +
                "    set @plant2='Ctrp' " +
                "end " +
                "else " +
                "begin " +
                "   set @plant1='Ctrp' " +
                "   set @plant2='Krwg' " +
                "end " +
                "select CONVERT(varchar,A.TglTrans,103) as tgltrans,A.Customer,A.SJNo,A.Qty,FC_Items.PartNo,A.OPNo, " +
                "case when isnull(A.TypeR,1) =1 then @plant1 when isnull(A.TypeR,1)=2 then @plant2 + ' di ' + @plant1 end typeR " +
                "from T3_Retur A left join FC_Items on A.ItemID=FC_Items.ID  " +
                "where A.RowStatus > -1 and convert(varchar,A.tgltrans,112)>='" + tgl1 + "' and  convert(varchar,A.tgltrans,112)<='" + tgl2 + "'  " +
                "union all " +
                "select CONVERT(varchar,A.TglTrans,103) as tgltrans,A.Customer,A.SJNo,A.Qty,FC_Items.PartNo,A.OPNo , " +
                "@plant1 + ' di ' + @plant2 as typeR from T3_ReturO A left join FC_Items on A.ItemID=FC_Items.ID  " +
                "where A.RowStatus > -1 and convert(varchar,A.tgltrans,112)>='" + tgl1 + "' and convert(varchar,A.tgltrans,112)<='" + tgl2 + "' " + this.SortBy();
            return strsql;
        }

        public string ViewPemantauanRetur(string tgl1, string tgl2)
        {
            string strsql = "select tanggal,PartNo,volume ,sum(isnull(QtyC1,0))QtyC1,sum(isnull(QtyP1,0)) QtyP1,sum(isnull(QtyS1,0)) QtyS1,sum(isnull(Tot1,0)) Tot1,sum(isnull(TotK1,0)) TotK1, " +
                "sum(isnull(QtyC2,0)) QtyC2,sum(isnull(QtyP2,0)) QtyP2,sum(isnull(QtyS2,0)) QtyS2,sum(isnull(Tot2,0)) Tot2, " +
                "sum(isnull(TotK2,0)) TotK2,sum(isnull(QtyC3,0)) QtyC3,sum(isnull(QtyP3,0)) QtyP3,sum(isnull(QtyS3,0)) QtyS3,sum(isnull(Tot3,0)) Tot3,sum(isnull(TotK3,0)) TotK3 from ( " +
                "select tanggal ,PartNo,volume,case when typeR=1 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC1," +
                "case when typeR=1 and (KW='P'and lokasi <> 'cl') then Qty end QtyP1, " +
                "case when typeR=1 and (KW='S'and lokasi <> 'cl') then Qty end QtyS1, " +
                "case when typeR=1 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty end Tot1, " +
                "case when typeR=1 and (KW='P' or KW='S') then QtyK end TotK1, " +
                "case when typeR=2 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC2, " +
                "case when typeR=2 and (KW='P' and lokasi <> 'cl') then Qty end QtyP2, " +
                "case when typeR=2 and (KW='S' and lokasi <> 'cl') then Qty end QtyS2, " +
                "case when typeR=2 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty end Tot2, " +
                "case when typeR=2 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK2, " +
                "case when typeR=3 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC3, " +
                "case when typeR=3 and (KW='P' and lokasi <> 'cl') then Qty end QtyP3, " +
                "case when typeR=3 and (KW='S' and lokasi <> 'cl') then Qty end QtyS3, " +
                "case when typeR=3 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty   end Tot3, " +
                "case when typeR=3 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK3 " +
                "from ( " +
                "    select R.TglTrans tanggal,I.PartNo,I.volume,SUBSTRING(I.partno,5,1) as KW,sum(R.Qty)Qty, " +
                "    sum(((I.Panjang*I.Lebar)/(1220*2440) ) * R.Qty) QtyK,isnull(R.TypeR,1)  TypeR,I.ID,L.Lokasi " +
                "    from T3_Retur R inner join FC_Items I on R.ItemID =I.ID  inner join FC_lokasi L on R.LokID=L.ID  where R.RowStatus>-1 " +
                "    group by R.TglTrans,I.PartNo,I.volume,R.TypeR,I.ID,L.Lokasi " +
                "union all " +
                "select R.TglTrans tanggal,I.PartNo,I.volume,SUBSTRING(I.partno,5,1) as KW,sum(R.Qty)Qty," +
                "    sum(((I.Panjang*I.Lebar)/(1220*2440) ) * R.Qty) QtyK,3  TypeR,I.ID,L.Lokasi " +
                "    from T3_ReturO R inner join FC_Items I on R.ItemID =I.ID  inner join FC_lokasi L on R.LokID=L.ID  where R.RowStatus>-1 " +
                "    group by R.TglTrans,I.PartNo,I.volume,I.ID,L.Lokasi " +
                ") as A ) as B where convert(char,tanggal,112)>='" + tgl1 + "' and convert(char,tanggal,112)<='" + tgl2 + "'group by tanggal,PartNo,volume order by PartNo";
            return strsql;
        }

        public string ViewProsesListPlank(string thnbln)
        {
            string strsql = "declare @thbln char(6)  " +
                "set @thbln='" + thnbln + "' " +
                "declare @thnbln0 varchar(6)  " +
                "declare @tgl datetime  " +
                "declare @users varchar(25)  " +
                "set @tgl=CAST( (@thbln+ '01') as datetime)  " +
                "set @tgl= DATEADD(month,-1,@tgl)  " +
                "set @thnbln0=LEFT(convert(char,@tgl,112),6) " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL    " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL1    " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL2  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL3 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL5   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL4   " +
                " " +
                "select ROW_NUMBER() OVER (ORDER BY PartnoAsal, " +
                "i99_SA desc,qtyin desc,qtyout desc) AS Row,    " +
                "PartNoAsal, part0,isnull(PartNo,'')PartNo, isnull(i99_SA,0)i99_SA,isnull(qtyin,0)qtyin, isnull(qtyout,0)qtyout, isnull(qi,0)qi, isnull(qo,0)qo " +
                ",isnull(PartNoAsal1,'')PartNoAsal1, part1,isnull(PartNo1,'')PartNo1, isnull(runingsaw_SA,0)runingsaw_SA, isnull(qtyin1,0)qtyin1, isnull(qtyout1,0)qtyout1, isnull(qi1,0)qi1, isnull(qo1,0)qo1   " +
                ",isnull(PartNoAsal2,'')PartNoAsal2, part2,isnull(PartNo2,'')PartNo2, isnull(Bevel_SA,0)Bevel_SA, isnull(qtyin2,0)qtyin2, isnull(qtyout2,0)qtyout2, isnull(qi2,0)qi2, isnull(qo2,0)qo2   " +
                ",'' PartNoAsal3,'' part3,''PartNo3, 0 Print_SA,0 qtyin3, 0  qtyout3,0 qi3,0 qo3    " +
                ",isnull(PartNoAsal4,'')PartNoAsal4, part4,isnull(PartNo4,'')PartNo4, isnull(straping_SA,0)straping_SA,isnull(qtyin4,0)qtyin4, isnull(qtyout4,0)qtyout4, isnull(qi4,0)qi4, isnull(qo4,0)qo4    " +
                " into tempListPlankL    " +
                "from ( select * from (     " +
                "select distinct * from (  " +
                "select distinct I0.PartNo PartNoAsal from T1_SaldoListPlank A inner join fc_items I0 on A.ItemID0=I0.ID   " +
                "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  " +
                "union all  " +
                "select distinct partnoasal from vw_KartuStockListplank where LEFT(convert(char,tanggal,112),6)=@thbln) as P   " +
                "right join     " +
                "(     " +
                "select PartNoAsal PartNoAsal0,part0,PartNo,sum(i99_SA) i99_SA,sum(qtyin)qtyin,sum(qtyout)qtyout,sum(isnull(Qi,0))Qi,sum(isnull(Qo,0))Qo from (     " +
                "select I0.PartNo PartNoAsal,I.PartNo part0,I.PartNo PartNo,Saldo i99_SA,0 qtyin,0 qtyout,0 Qi,0 Qo from T1_SaldoListPlank A   " +
                "inner join fc_items I0 on A.ItemID0=I0.ID     " +
                "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='i99'     " +
                "union all    " +
                "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,0 Qi,0 Qo from vw_KartuStockListplank A    " +
                "where qty>0 and process Like '%i99%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo  " +
                "from vw_KartuStockListplank A where qty<0 and process Like '%i99%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln " +
                "union All " +
                "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplank A    " +
                "where qty>0 and process Like '%adjini99%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,0 Qi,qty*-1 Qo  " +
                "from vw_KartuStockListplank A where qty<0 and process Like '%adjouti99%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln) as i99 group by PartNoAsal,part0,PartNo  " +
                ") as i99A on i99A.PartNoAsal0=P.PartnoAsal    " +
                "left join     " +
                "(     " +
                "select PartNoAsal PartNoAsal1,Part1,PartNo PartNo1,sum(runingsaw_SA) runingsaw_SA,sum(qtyin)qtyin1,sum(qtyout)qtyout1, " +
                "sum(isnull(Qi,0))Qi1,sum(isnull(Qo,0))Qo1 from (     " +
                "select I0.PartNo PartNoAsal,I.PartNo Part1,I.PartNo PartNo,Saldo runingsaw_SA,0 qtyin,0 qtyout,0 Qi,0 Qo  " +
                "from T1_SaldoListPlank A inner join fc_items I0 on A.ItemID0=I0.ID     " +
                "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='runingsaw'     " +
                "union all    " +
                "select PartNoAsal,PartNoAsal1 Part1,PartNo,0 runingsaw_SA,qty qtyin,0 qtyout,0 Qi,0 Qo  " +
                "from vw_KartuStockListplank A  where qty>0 and process Like '%runingsaw%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 Part1,PartNo,0 runingsaw_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo  " +
                "from vw_KartuStockListplank A  where qty<0 and process Like '%runingsaw%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln " +
                "union All " +
                "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplank A    " +
                "where qty>0 and process Like '%adjinruningsaw%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,0 Qi,qty*-1 Qo  " +
                "from vw_KartuStockListplank A where qty<0 and process Like '%adjoutruningsaw%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln " +
                ") as runingsaw     " +
                "group by PartNoAsal,Part1,PartNo) as runingsawA      " +
                "on P.PartNoAsal=runingsawA.PartNoAsal1     " +
                "left join      " +
                "(     " +
                "select isnull(PartNoAsal,'') PartNoAsal2,Part2,isnull(PartNo,'') PartNo2,isnull(sum(Bevel_SA),0) Bevel_SA, " +
                "isnull(sum(qtyin),0)qtyin2,isnull(sum(qtyout),0)qtyout2,sum(isnull(Qi,0))Qi2,sum(isnull(Qo,0))Qo2 from (     " +
                "select I0.PartNo PartNoAsal,I.PartNo Part2,I.PartNo PartNo,Saldo Bevel_SA,0 qtyin,0 qtyout,0 Qi,0 Qo  " +
                "from T1_SaldoListPlank A inner join fc_items I0 on A.ItemID0=I0.ID     " +
                "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='Bevel'     " +
                "union all    " +
                "select PartNoAsal,PartNoAsal1 Part2,PartNo,0 Bevel_SA,qty qtyin,0 qtyout,0 Qi,0 Qo  " +
                "from vw_KartuStockListplank A  where qty>0 and process Like '%Bevel%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 Part2,PartNo,0 Bevel_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo  " +
                "from vw_KartuStockListplank A  where qty<0 and process Like '%Bevel%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln " +
                "union All " +
                "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplank A    " +
                "where qty>0 and process Like '%adjinBevel%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,0 Qi,qty*-1 Qo  " +
                "from vw_KartuStockListplank A where qty<0 and process Like '%adjoutBevel%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln) as Bevel     " +
                "group by PartNoAsal,Part2,PartNo) as BevelA      " +
                "on P.PartNoAsal=BevelA.PartNoAsal2 and SUBSTRING(BevelA.PartNo2,7,11)=SUBSTRING(runingsawA.PartNo1,7,11)     " +
                "left join      " +
                "(     " +
                "select PartNoAsal PartNoAsal4,Part4,PartNo PartNo4,sum(straping_SA) straping_SA,sum(qtyin)qtyin4, " +
                "sum(qtyout)qtyout4,sum(isnull(Qi,0))Qi4,sum(isnull(Qo,0))Qo4 from (     " +
                "select I0.PartNo PartNoAsal,I.PartNo  Part4,I.PartNo PartNo,Saldo straping_SA,0 qtyin,0 qtyout,0 Qi,0 Qo  " +
                "from T1_SaldoListPlank A inner join fc_items I0 on A.ItemID0=I0.ID     " +
                "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='straping'     " +
                "union all    " +
                "select PartNoAsal,PartNoAsal1 Part4,PartNo,0 straping_SA,qty qtyin,0 qtyout,0 Qi,0 Qo  " +
                "from vw_KartuStockListplank A  where qty>0 and process Like '%straping%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 Part4,PartNo,0 straping_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo  " +
                "from vw_KartuStockListplank A  where qty<0 and process Like '%straping%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln " +
                "union All " +
                "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplank A    " +
                "where qty>0 and process Like '%adjinstraping%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
                "union all     " +
                "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,0 qtyout,0 Qi,qty*-1 Qo  " +
                "from vw_KartuStockListplank A where qty<0 and process Like '%adjoutstraping%' and      " +
                "left(CONVERT(char,tanggal,112),6)=@thbln) as straping     " +
                "group by PartNoAsal,Part4,PartNo) as strapingA      " +
                "on P.PartNoAsal =strapingA.PartNoAsal4 and SUBSTRING(BevelA.Part2,6,11)=SUBSTRING(strapingA.Part4 ,6,11)  " +
                ") as d where I99_SA<>0 or qtyin<>0 or qtyout<>0  " +
                "or RuningSaw_SA<>0 or qtyin1<>0 or qtyout1<>0 " +
                "or Bevel_SA<>0 or qtyin2<>0 or qtyout2<>0  " +
                "or Straping_SA<>0  or qtyin4<>0 or qtyout4<>0   " +
                ") as d0  " +
                "    " +
                "select  PartnoAsal I99_Partno,sum(I99_SA) I99_SA,isnull(sum(qtyin),0) I99_In,isnull(sum(qtyout),0) I99_Out,sum(isnull(Qi,0))Qi,sum(isnull(Qo,0))Qo, Partnoasal1,part1,   " +
                "isnull(Partno1,0) RuningSaw_Partno,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0) RuningSaw_In,isnull(sum(qtyout1),0) RuningSaw_Out,sum(isnull(Qi1,0))Qi1,sum(isnull(Qo1,0))Qo1,Partnoasal2,part2,    " +
                "isnull(Partno2,0) Bevel_Partno, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0) Bevel_In,isnull(sum(qtyout2),0) Bevel_Out,sum(isnull(Qi2,0))Qi2,sum(isnull(Qo2,0))Qo2,Partnoasal3,part3,     " +
                "isnull(Partno3,0) Print_Partno, sum(Print_SA) Print_SA,isnull(sum(qtyin3),0) Print_In,isnull(sum(qtyout3),0) Print_Out,sum(isnull(Qi3,0))Qi3,sum(isnull(Qo3,0))Qo3,Partnoasal4,part4,isnull(Partno4,0)     " +
                "Straping_Partno, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0) Straping_In,isnull(sum(qtyout4),0) Straping_out,sum(isnull(Qi4,0))Qi4,sum(isnull(Qo4,0))Qo4 into tempListPlankL1  from (     " +
                "select PartnoAsal,sum(I99_SA) I99_SA,isnull(sum(qtyin),0)qtyin,isnull(sum(qtyout),0)qtyout,sum(isnull(Qi,0))Qi,sum(isnull(Qo,0))Qo,Partnoasal1,part1,    " +
                "Partno1,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0)qtyin1,isnull(sum(qtyout1),0)qtyout1,sum(isnull(Qi1,0))Qi1,sum(isnull(Qo1,0))Qo1,Partnoasal2,part2,    " +
                "Partno2, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0)qtyin2,isnull(sum(qtyout2),0)qtyout2,sum(isnull(Qi2,0))Qi2,sum(isnull(Qo2,0))Qo2,Partnoasal3,part3,    " +
                "Partno3,sum(Print_SA) Print_SA,isnull(sum(qtyin3),0)qtyin3,isnull(sum(qtyout3),0)qtyout3,sum(isnull(Qi3,0))Qi3,sum(isnull(Qo3,0))Qo3,Partnoasal4,part4,   " +
                "Partno4, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0)qtyin4,isnull(sum(qtyout4),0)qtyout4,sum(isnull(Qi4,0))Qi4,sum(isnull(Qo4,0))Qo4 from (     " +
                "select Row,PartnoAsal,     " +
                "case when (select COUNT(Partno) from tempListPlankL where partno=L.partno and ROW<L.row)=0 then isnull(I99_SA,0) else 0 end I99_SA,     " +
                "case when (select COUNT(Partno) from tempListPlankL where partno=L.partno and ROW<L.row)=0 then isnull(qtyin,0) else 0 end qtyin,     " +
                "case when (select COUNT(Partno) from tempListPlankL where partno=L.partno and ROW<L.row)=0 then isnull(qtyout,0) else 0 end qtyout,    " +
                "case when (select COUNT(Partno) from tempListPlankL where partno=L.partno and ROW<L.row and isnull(Qi,0)>0)=0 then isnull(Qi,0) else 0 end Qi,   " +
                "case when (select COUNT(Partno) from tempListPlankL where partno=L.partno and ROW<L.row and isnull(Qo,0)>0)=0 then isnull(Qo,0) else 0 end Qo,  Partnoasal1,part1, Partno1,     " +
                "case when (select COUNT(Partno1) from tempListPlankL where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(RuningSaw_SA,0) else 0 end RuningSaw_SA,     " +
                "case when (select COUNT(Partno1) from tempListPlankL where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyin1,0) else 0 end qtyin1,     " +
                "case when (select COUNT(Partno1) from tempListPlankL where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyout1,0) else 0 end qtyout1,    " +
                "case when (select COUNT(Partno1) from tempListPlankL where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(Qi1,0) else 0 end Qi1,   " +
                "case when (select COUNT(Partno1) from tempListPlankL where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(Qo1,0) else 0 end Qo1, " +
                "Partnoasal2,part2,Partno2,    " +
                "case when (select COUNT(Partno2) from tempListPlankL where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(Bevel_SA,0) else 0 end Bevel_SA,    " +
                "case when (select COUNT(Partno2) from tempListPlankL where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyin2,0) else 0 end qtyin2,     " +
                "case when (select COUNT(Partno2) from tempListPlankL where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyout2,0) else 0 end qtyout2,   " +
                "case when (select COUNT(Partno2) from tempListPlankL where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(Qi2,0) else 0 end Qi2,   " +
                "case when (select COUNT(Partno2) from tempListPlankL where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(Qo2,0) else 0 end Qo2, " +
                "Partnoasal3,part3,Partno3,      " +
                "0 Print_SA,     " +
                "0 qtyin3,      " +
                "0 qtyout3,   " +
                "0 qi3,      " +
                "0 qo3,Partnoasal4,part4,Partno4,     " +
                "case when (select COUNT(Partno4) from tempListPlankL where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(Straping_SA,0) else 0 end Straping_SA,    " +
                "case when (select COUNT(Partno4) from tempListPlankL where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyin4,0) else 0 end qtyin4,     " +
                "case when (select COUNT(Partno4) from tempListPlankL where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyout4,0) else 0 end qtyout4,     " +
                "case when (select COUNT(Partno4) from tempListPlankL where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(Qi4,0) else 0 end Qi4,   " +
                "case when (select COUNT(Partno4) from tempListPlankL where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0then isnull(Qo4,0) else 0 end Qo4     " +
                "from tempListPlankL L) as L1  group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal3,part3,Partno3,Partnoasal4,part4,Partno4 ) as L2      " +
                "where I99_SA<>0 or qtyin<>0 or qtyout<>0  " +
                "or RuningSaw_SA<>0 or qtyin1<>0 or qtyout1<>0 " +
                "or Bevel_SA<>0 or qtyin2<>0 or qtyout2<>0  " +
                "or Straping_SA<>0  or qtyin4<>0 or qtyout4<>0 group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal3,part3,Partno3,Partnoasal4,part4,Partno4       " +
                "order by PartnoAsal,Partno1 desc,Partno2 desc,Partno3 desc,Partno4 desc  " +
                "  " +
                "select ROW_NUMBER() OVER (ORDER BY i99_partno,runingsaw_partno,bevel_partno,straping_partno,i99_SA desc,i99_in desc,i99_out desc,runingsaw_in desc,runingsaw_out desc,bevel_in desc,bevel_out desc) AS Row,* into tempListPlankL2 from tempListPlankL1  " +
                "where I99_SA+RuningSaw_SA+Bevel_SA+Straping_SA+i99_in+i99_out+Runingsaw_in+Runingsaw_out+Bevel_in+Bevel_out+straping_in+straping_out>0   " +
                "     " +
                "select i99_partno,i99_SA,i99_in,i99_out,i99_SA+i99_Saldo i99_Saldo,isnull(Qi,0)Qi,isnull(Qo,0)Qo, " +
                "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_SA+runingsaw_Saldo runingsaw_Saldo,isnull(Qi1,0)Qi1,isnull(Qo1,0)Qo1,  " +
                "Partnoasal2,case when rtrim(Part2) <> SUBSTRING(part4,1,3)+'-1-'+ SUBSTRING(part4,7,11) then SUBSTRING(part4,1,3)+'-1-'+ SUBSTRING(part4,7,11) else Part2 end part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_SA+Bevel_Saldo Bevel_Saldo,isnull(Qi2,0)Qi2,isnull(Qo2,0)Qo2,  " +
                "Partnoasal4,part4,Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_SA+Straping_Saldo Straping_Saldo,isnull(Qi4,0)Qi4,isnull(Qo4,0)Qo4  " +
                "into tempListPlankL3 from (     " +
                "select i99_partno,i99_SA, i99_in,i99_out,(i99_in-i99_out)i99_Saldo,Qi,Qo,Partnoasal1,part1,     " +
                "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,(runingsaw_in-runingsaw_out)runingsaw_Saldo,Qi1,Qo1,Partnoasal2,part2,     " +
                "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,(Bevel_in-Bevel_out) Bevel_Saldo,Qi2,Qo2,Partnoasal3,part3,     " +
                "Print_partno,Print_SA,Print_in,Print_out,(Print_in-Print_out)Print_Saldo,Qi3,Qo3,Partnoasal4,part4,     " +
                "Straping_partno,Straping_SA,Straping_in,Straping_out,(Straping_in-Straping_out)Straping_Saldo,Qi4,Qo4 from (     " +
                "select  i99_partno,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else Qi end Qi,    " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else Qo end Qo,  " +
                "Partnoasal1,part1,runingsaw_partno,   " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and    " +
                "(select count(runingsaw_partno) from tempListPlankL2 where i99_partno=A.i99_partno and    " +
                "runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL2 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL2 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "row < A.row )>0 then '0' else runingsaw_out end runingsaw_out,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL2 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "row < A.row )>0 then '0' else Qi1 end Qi1,   " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL2 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "row < A.row )>0 then '0' else Qo1 end Qo1,A.Partnoasal2,A.part2,Bevel_partno,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "row < A.row )>0 then '0' else Bevel_in end Bevel_in,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "row < A.row )>0 then '0' else Bevel_out end Bevel_out,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "row < A.row )>0 then '0' else Qi2 end Qi2,   " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "row < A.row )>0 then '0' else Qo2 end Qo2,A.Partnoasal3,A.part3,Print_partno,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Print_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Print_partno=A.Print_partno and    " +
                "row < A.row )>0 then '0' else Print_SA end Print_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Print_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Print_partno=A.Print_partno and    " +
                "row < A.row )>0 then '0' else Print_in end Print_in,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Print_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Print_partno=A.Print_partno and    " +
                "row < A.row )>0 then '0' else Print_out end Print_out,  0 qi3,  0 qo3, A.Partnoasal4,A.part4,Straping_partno,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "row < A.row )>0 then '0' else Straping_SA end Straping_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "row < A.row )>0 then '0' else Straping_in end Straping_in,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "row < A.row )>0 then '0' else Straping_out end Straping_out,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "row < A.row )>0 then '0' else Qi4 end Qi4,     " +
                "case when (select count(i99_partno) from tempListPlankL2 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL2 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "row < A.row )>0 then '0' else Qo4 end Qo4     " +
                "from tempListPlankL2 A) B) C where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or    " +
                "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0 or Print_SA<>0 or Print_in<>0 or    " +
                "Print_out<>0 or Print_Saldo<>0 or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0    " +
                "  " +
                "select ROW_NUMBER() OVER (ORDER BY i99_partno,Runingsaw_partno,Partnoasal2,part2,Bevel_Partno,Partnoasal4,part4,Straping_Partno) AS Row,*  " +
                "into tempListPlankL4  " +
                "from  tempListPlankL3 TL left join (  " +
                "select partnoasal5,part5,partno5,SUM(qtyin) Qty from (  " +
                "select case when ISNULL(sfrom,'')='straping' then (select partno from FC_Items where ID In (select ItemID0 from T1_Straping where ID=A.T1SerahID ))  " +
                "when ISNULL(sfrom,'')='bevel' then (select SUBSTRING(PartNo, 1, 3) + '-1-' + SUBSTRING(PartNo, 7, 11) from FC_Items where ID In (select ItemID0 from T1_bevel where ID=A.T1SerahID )) end PartnoAsal5,  " +
                "case when ISNULL(sfrom,'')='straping' then (  " +
                "select partno from FC_Items where ID In (select ItemID from T1_Bevel where ID= (select L1ID from T1_Straping where ID= A.T1SerahID) ))  " +
                "when ISNULL(sfrom,'')='bevel' then (select SUBSTRING(PartNo, 1, 3) + '-1-' + SUBSTRING(PartNo, 7, 11) from FC_Items where ID In (select ItemID from T1_bevel where ID=A.T1SerahID )) end Part5,  " +
                "I.PartNo PartNo5,  " +
                "qtyin from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID where A.rowstatus>-1 and (ISNULL(sfrom,'')='bevel' or   " +
                "ISNULL(sfrom,'')='straping') and LEFT(convert(char,tgltrans,112),6)= @thbln )T group by partnoasal5,part5,partno5)T3  " +
                "on (TL.part2=T3.Part5 and TL.partnoasal2=T3.PartnoAsal5  ) " +
                "order by i99_partno,Partnoasal1,part1,RUningsaw_Partno,Partnoasal2,part2,Bevel_Partno,Partnoasal4,part4,Straping_Partno  " +
                " " +
                "select ROW_NUMBER() OVER (ORDER BY i99_partno)as row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,Qi,Qo,Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out, " +
                "runingsaw_Saldo,Qi1,Qo1,Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,Qi2,Qo2,Partnoasal4,part4, " +
                "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,Qi4,Qo4, " +
                "case when partnoasal5=partnoasal4 or partnoasal5=partnoasal2 then partnoasal5 else '-'end partnoasal5, " +
                "case when part5=part4 or part5=part2 then part5 else '-'end part5,case when part5=part4 or part5=part2 then partno5 else '-'end partno5, " +
                "case when part5=part4 or part5=part2 then Qty else 0 end Qty " +
                "into tempListPlankL5  " +
                "from tempListPlankL4 " +
                "  " +
                "select row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,isnull(Qi,0)Qi,isnull(Qo,0)Qo,  " +
                "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,isnull(Qi1,0)Qi1,isnull(Qo1,0)Qo1,    " +
                "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,isnull(Qi2,0)Qi2,isnull(Qo2,0)Qo2,   " +
                "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,isnull(Qi4,0)Qi4,isnull(Qo4,0)Qo4  " +
                ",T3_partno,T3_Qty  " +
                "from (     " +
                "select row,i99_partno,i99_SA, i99_in,i99_out,i99_Saldo,Qi,Qo,  " +
                "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,Qi1,Qo1,  " +
                "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,Qi2,Qo2,  " +
                "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,Qi4,Qo4 " +
                ",T3_partno,T3_Qty   " +
                "from (     " +
                "select row,case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 then '-' else i99_partno end i99_partno,   " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out,    " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_saldo end i99_saldo,   " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else Qi end Qi,    " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else Qo end Qo,  " +
                "  " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL5 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '-' else runingsaw_partno end runingsaw_partno,   " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and    " +
                "(select count(runingsaw_partno) from tempListPlankL5 where i99_partno=A.i99_partno and    " +
                "part1=A.part1 and runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL5 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL5 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_out end runingsaw_out,   " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL5 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_saldo end runingsaw_saldo,   " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL5 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '0' else Qi1 end Qi1,   " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(runingsaw_partno) from tempListPlankL5 where i99_partno=A.i99_partno and runingsaw_partno=A.runingsaw_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '0' else Qo1 end Qo1,A.Partnoasal2,A.part2,   " +
                " " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL5 where Bevel_partno=A.Bevel_partno and part1=A.part1 and   " +
                "row < A.row )>0 then '-' else Bevel_partno end Bevel_partno,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_in end Bevel_in,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_out end Bevel_out,  " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_saldo end Bevel_saldo,    " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Qi2 end Qi2,   " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Bevel_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Bevel_partno=A.Bevel_partno and    " +
                "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Qo2 end Qo2,  " +
                " " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '-' else Straping_partno end Straping_partno,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "part1=A.part1 and row < A.row )>0 then '0' else Straping_SA end Straping_SA,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_in end Straping_in,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_out end Straping_out,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_saldo end Straping_saldo, " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Qi4 end Qi4,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
                "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Qo4 end Qo4,  " +
                "  " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(partno5) from tempListPlankL5 where i99_partno=A.i99_partno and partno5=A.partno5 and    " +
                "partnoasal5=A.partnoasal5 and part5=A.part5 and row < A.row )>0 then '-' else partno5 end T3_partno,     " +
                "case when (select count(i99_partno) from tempListPlankL5 where i99_partno=A.i99_partno and row < A.row )>0 and     " +
                "(select count(Straping_partno) from tempListPlankL5 where i99_partno=A.i99_partno and partno5=A.partno5 and    " +
                "partnoasal5=A.partnoasal5 and part5=A.part5 and row < A.row )>0 then '0' else Qty end T3_Qty     " +
                "    " +
                "from tempListPlankL5 A) B) C   " +
                "where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or    " +
                "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0  " +
                "or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0   " +
                "or T3_Qty<>0  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL    " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL1    " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL2    " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL3    " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL4 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankL5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankL5 ";
            return strsql;
        }

        public string ViewProsesListPlankR1(string thnbln)
        {
            string strsql = "declare @thbln char(6)  " +
                "set @thbln='" + thnbln + "' " +
                            "declare @thnbln0 varchar(6)   " +
            "declare @tgl datetime   " +
            "declare @users varchar(25)   " +
            "set @tgl=CAST( (@thbln+ '01') as datetime)   " +
            "set @tgl= DATEADD(month,-1,@tgl)   " +
            "set @thnbln0=LEFT(convert(char,@tgl,112),6)   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L1   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L2   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L3   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L4    " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L5  " +
            "    " +
            "select ROW_NUMBER() OVER (ORDER BY PartnoAsal0,partno1,partno2,partno4,I99_SA desc,qtyin desc,qtyout desc) AS Row,  " +
            " case when isnull(PartnoAsal,'')<>'' then  PartnoAsal  else case when isnull(PartnoAsal1,'')<>'' then PartnoAsal1 else   " +
            "case when isnull(PartnoAsal2,'')<>'' then PartnoAsal2 else case when isnull(PartnoAsal4,'')<>'' then PartnoAsal4 else '' end end end end PartNoAsal,  part0,isnull(PartNo,'')PartNo,   " +
            "isnull(i99_SA,0)i99_SA, isnull(qtyin,0)qtyin, isnull(qtyout,0)qtyout,isnull(Qi,0)Qi,isnull(Qo,0)Qo, isnull(PartNoAsal1,'')PartNoAsal1,part1,   " +
            "isnull(PartNo1,'')PartNo1, isnull(runingsaw_SA,0)runingsaw_SA,isnull(qtyin1,0)qtyin1, isnull(qtyout1,0)qtyout1, isnull(qi1,0)qi1, isnull(qo1,0)qo1,   " +
            "isnull(PartNoAsal2,'')PartNoAsal2,part2, isnull(PartNo2,'')PartNo2, isnull(Bevel_SA,0)Bevel_SA,isnull(qtyin2,0)qtyin2, isnull(qtyout2,0)qtyout2,  " +
            "isnull(qi2,0)qi2, isnull(qo2,0)qo2,isnull(PartNoAsal4,'')PartNoAsal4,part4, isnull(PartNo4,'')PartNo4, isnull(straping_SA,0)straping_SA,   " +
            "isnull(qtyin4,0)qtyin4, isnull(qtyout4,0)qtyout4 , isnull(qi4,0)qi4, isnull(qo4,0)qo4    " +
            " into tempListPlankR1L     " +
            "from (      " +
            "select distinct * from (    " +
            "select distinct I0.PartNo PartNoAsal from T1_SaldoListPlankR1 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  " +
            "union all  " +
            "select distinct partnoasal from vw_KartuStockListPlankR1 where LEFT(convert(char,tanggal,112),6)=@thbln  " +
            ") as P left join      " +
            "(      " +
            "select PartNoAsal PartNoAsal0,PartNoAsal part0,PartNo,sum(i99_SA) i99_SA,sum(qtyin)qtyin,sum(qtyout)qtyout,sum(isnull(Qi,0))Qi,sum(isnull(Qo,0))Qo from (      " +
            "select I0.PartNo PartNoAsal,I.PartNo part0,I.PartNo PartNo,Saldo i99_SA,0 qtyin,0 qtyout,0 Qi,0 Qo from T1_SaldoListPlankR1 A inner join fc_items I0 on A.ItemID0=I0.ID      " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='i99'      " +
            "union all     " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR1  where qty>0 and process='i99' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all      " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR1  where qty<0 and process='i99' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln  " +
            "union All  " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplankR1 A     " +
            "where qty>0 and process Like '%adjini99R1%' and  left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all      " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1  qtyout,0 Qi,qty*-1 Qo   " +
            "from vw_KartuStockListplankR1 A where qty<0 and process Like '%adjouti99R1%' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln  " +
            ") as i99 group by PartNoAsal,part0,PartNo having sum(i99_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0) as i99A on P.PartNoAsal=i99A.PartnoAsal0  " +
            "left join      " +
            "(      " +
            "select PartNoAsal PartNoAsal1,part1,PartNo PartNo1,sum(runingsaw_SA) runingsaw_SA,sum(qtyin)qtyin1,sum(qtyout)qtyout1,sum(isnull(Qi,0))Qi1,sum(isnull(Qo,0))Qo1  from (      " +
            "select I0.PartNo PartNoAsal,I.PartNo part1,I.PartNo PartNo,Saldo runingsaw_SA,0 qtyin,0 qtyout,0 Qi,0 Qo from T1_SaldoListPlankR1 A inner join fc_items I0 on A.ItemID0=I0.ID      " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='runingsaw'      " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part1,PartNo,0 runingsaw_SA,qty qtyin,0 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR1  where qty>0 and process='runingsaw' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all      " +
            "select PartNoAsal,PartNoAsal1 part1,PartNo,0 runingsaw_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR1  where qty<0 and process='runingsaw' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln  " +
            "union all      " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplankR1 A     " +
            "where qty>0 and process Like '%adjinruningsawR1%' and  left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all      " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo   " +
            "from vw_KartuStockListplankR1 A where qty<0 and process Like '%adjoutruningsawR1%' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as runingsaw      " +
            "group by PartNoAsal,part1,PartNo having sum(runingsaw_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0 ) as runingsawA   " +
            "on P.PartNoAsal=runingsawA.PartNoAsal1   " +
            "  " +
            "left join       " +
            "(      " +
            "select PartNoAsal PartNoAsal2,part2,PartNo PartNo2,sum(Bevel_SA)Bevel_SA,sum(qtyin)qtyin2,sum(qtyout)qtyout2,sum(isnull(Qi,0))Qi2,sum(isnull(Qo,0))Qo2 from (    " +
            "select I0.PartNo PartNoAsal,I.PartNo part2,I.PartNo PartNo,Saldo Bevel_SA,0 qtyin,0 qtyout,0 Qi,0 Qo,ItemID0,itemid  from T1_SaldoListPlankR1 A inner join fc_items I0 on A.ItemID0=I0.ID      " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='Bevel'      " +
            "union all    " +
            "select PartNoAsal,PartNoAsal1 part2,PartNo,0 Bevel_SA,qty qtyin,0 qtyout,0 Qi,0 Qo,ItemID0,itemid  from vw_KartuStockListplankR1  where qty>0 and process='Bevel' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all    " +
            "select PartNoAsal,PartNoAsal1 part2,PartNo,0 Bevel_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo,ItemID0,itemid  from vw_KartuStockListplankR1  where qty<0 and process='Bevel' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln  " +
            "union All  " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo,ItemID0,itemid  from vw_KartuStockListplankR1 A     " +
            "where qty>0 and process Like '%adjinBevelR1%' and  left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all      " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo ,ItemID0,itemid   " +
            "from vw_KartuStockListplankR1 A where qty<0 and process Like '%adjoutBevelR1%' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as bevel   " +
            "group by PartNoAsal,part2,PartNo   having sum(Bevel_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0 ) as BevelA       " +
            "on P.PartNoAsal=BevelA.PartNoAsal2   " +
            "left join       " +
            "(      " +
            "select PartNoAsal PartNoAsal4,part4,PartNo PartNo4,sum(straping_SA)straping_SA,sum(qtyin)qtyin4,sum(qtyout)qtyout4,sum(isnull(Qi,0))Qi4,sum(isnull(Qo,0))Qo4 from (     " +
            "select I0.PartNo PartNoAsal,I.PartNo part4,I.PartNo PartNo,Saldo Straping_SA,0 qtyin,0 qtyout,0 Qi,0 Qo  from T1_SaldoListPlankR1 A inner join fc_items I0 on A.ItemID0=I0.ID      " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='strapingR1'      " +
            "union all   " +
            "select PartNoAsal,PartNoAsal1 part4,PartNo,0 straping_SA,qty qtyin,0 qtyout,0 Qi,0 Qo  from vw_KartuStockListplankR1  where qty>0 and process='strapingR1' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all    " +
            "select PartNoAsal,PartNoAsal1 part4,PartNo,0 straping_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo  from vw_KartuStockListplankR1  where qty<0 and process='strapingR1' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln  " +
            "union All  " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplankR1 A     " +
            "where qty>0 and process Like '%adjinstrapingR1%' and  left(CONVERT(char,tanggal,112),6)=@thbln      " +
            "union all      " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo   " +
            "from vw_KartuStockListplankR1 A where qty<0 and process Like '%adjoutstrapingR1%' and       " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as straping   " +
            "group by PartNoAsal,part4,PartNo  having sum(straping_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0  ) as strapingA       " +
            "on P.PartNoAsal=strapingA.PartNoAsal4  " +
            ") as d   " +
            "  " +
            "select   case when isnull(PartnoAsal,'')<>'' then  PartnoAsal  else case when isnull(PartnoAsal1,'')<>'' then PartnoAsal1 else   " +
            "case when isnull(PartnoAsal2,'')<>'' then PartnoAsal2 else case when isnull(PartnoAsal4,'')<>'' then PartnoAsal4 else '' end end end end  " +
            " I99_Partno,sum(I99_SA) I99_SA,isnull(sum(qtyin),0) I99_In,isnull(sum(qtyout),0) I99_Out,isnull(sum(qi),0)qi,isnull(sum(qo),0)qo,  " +
            "Partnoasal1,part1,isnull(Partno1,0) RuningSaw_Partno,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0) RuningSaw_In,isnull(sum(qtyout1),0) RuningSaw_Out,isnull(sum(qi1),0)qi1,isnull(sum(qo1),0)qo1,  " +
            "Partnoasal2,part2,isnull(Partno2,0) Bevel_Partno, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0) Bevel_In,isnull(sum(qtyout2),0) Bevel_Out,isnull(sum(qi2),0)qi2,isnull(sum(qo2),0)qo2,  " +
            "Partnoasal4,part4,isnull(Partno4,0) Straping_Partno, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0) Straping_In,isnull(sum(qtyout4),0) Straping_out,isnull(sum(qi4),0)qi4,isnull(sum(qo4),0)qo4   " +
            "into tempListPlankR1L1  from (      " +
            "select PartnoAsal,sum(I99_SA) I99_SA,isnull(sum(qtyin),0)qtyin,isnull(sum(qtyout),0)qtyout,isnull(sum(qi),0)qi,isnull(sum(qo),0)qo,  " +
            "Partnoasal1,part1,Partno1,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0)qtyin1,isnull(sum(qtyout1),0)qtyout1,isnull(sum(qi1),0)qi1,isnull(sum(qo1),0)qo1,  " +
            "Partnoasal2,part2,Partno2, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0)qtyin2,isnull(sum(qtyout2),0)qtyout2,isnull(sum(qi2),0)qi2,isnull(sum(qo2),0)qo2,  " +
            "Partnoasal4,part4,Partno4, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0)qtyin4,isnull(sum(qtyout4),0)qtyout4,isnull(sum(qi4),0)qi4,isnull(sum(qo4),0)qo4 from (      " +
            "select Row,   " +
            "case when (select COUNT(Partno) from tempListPlankR1L where PartnoAsal=L.PartnoAsal and ROW<L.row)=0 then PartnoAsal else '' end PartnoAsal,      " +
            "case when (select COUNT(Partno) from tempListPlankR1L where PartnoAsal=L.PartnoAsal and ROW<L.row)=0 then isnull(I99_SA,0) else 0 end I99_SA,      " +
            "case when (select COUNT(Partno) from tempListPlankR1L where PartnoAsal=L.PartnoAsal and ROW<L.row)=0 then isnull(qtyin,0) else 0 end qtyin,      " +
            "case when (select COUNT(Partno) from tempListPlankR1L where PartnoAsal=L.PartnoAsal and ROW<L.row)=0 then isnull(qtyout,0) else 0 end qtyout,  " +
            "case when (select COUNT(Partno) from tempListPlankR1L where PartnoAsal=L.PartnoAsal and ROW<L.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi,  " +
            "case when (select COUNT(Partno) from tempListPlankR1L where PartnoAsal=L.PartnoAsal and ROW<L.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo,     " +
            "  " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row)=0 then Partnoasal1 else '' end Partnoasal1,   " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row)=0 then part1 else '' end part1,   " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row)=0 then Partno1 else '' end Partno1,   " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row)=0 then isnull(RuningSaw_SA,0) else 0 end RuningSaw_SA,      " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyin1,0) else 0 end qtyin1,      " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyout1,0) else 0 end qtyout1,  " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row and isnull(Qi1,0)>0)=0 then isnull(qi1,0) else 0 end qi1,  " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno1 and ROW<L.row and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1,     " +
            "  " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno2 and ROW<L.row)=0 then Partnoasal2 else '' end Partnoasal2,   " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno2 and ROW<L.row)=0 then part2 else '' end part2,   " +
            "case when (select COUNT(Partno1) from tempListPlankR1L where PartnoAsal1=L.PartnoAsal1 and partno1=L.partno2 and ROW<L.row)=0 then Partno2 else '' end Partno2,      " +
            "case when (select COUNT(Partno2) from tempListPlankR1L where PartnoAsal2=L.PartnoAsal2 and partno2=L.partno2 and ROW<L.row)=0 then isnull(Bevel_SA,0) else 0 end Bevel_SA,     " +
            "case when (select COUNT(Partno2) from tempListPlankR1L where PartnoAsal2=L.PartnoAsal2 and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyin2,0) else 0 end qtyin2,      " +
            "case when (select COUNT(Partno2) from tempListPlankR1L where PartnoAsal2=L.PartnoAsal2 and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyout2,0) else 0 end qtyout2,  " +
            "case when (select COUNT(Partno2) from tempListPlankR1L where PartnoAsal2=L.PartnoAsal2 and partno2=L.partno2 and ROW<L.row and isnull(Qi2,0)>0)=0 then isnull(qi2,0) else 0 end qi2,  " +
            "case when (select COUNT(Partno2) from tempListPlankR1L where PartnoAsal2=L.PartnoAsal2 and partno2=L.partno2 and ROW<L.row and isnull(Qo2,0)>0)=0 then isnull(qo2,0) else 0 end qo2,    " +
            "  " +
            "Partnoasal4,   " +
            "part4,   " +
            "Partno4,      " +
            "case when (select COUNT(Partno4) from tempListPlankR1L where PartnoAsal4=L.PartnoAsal4 and partno4=L.partno4 and ROW<L.row)=0 then isnull(Straping_SA,0) else 0 end Straping_SA,     " +
            "case when (select COUNT(Partno4) from tempListPlankR1L where PartnoAsal4=L.PartnoAsal4 and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyin4,0) else 0 end qtyin4,      " +
            "case when (select COUNT(Partno4) from tempListPlankR1L where PartnoAsal4=L.PartnoAsal4 and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyout4,0) else 0 end qtyout4,   " +
            "case when (select COUNT(Partno4) from tempListPlankR1L where PartnoAsal4=L.PartnoAsal4 and partno4=L.partno4 and ROW<L.row and isnull(qi4,0)>0)=0 then isnull(qi4,0) else 0 end qi4,   " +
            "case when (select COUNT(Partno4) from tempListPlankR1L where PartnoAsal4=L.PartnoAsal4 and partno4=L.partno4 and ROW<L.row and isnull(qo4,0)>0)=0 then isnull(qo4,0) else 0 end qo4      " +
            "from tempListPlankR1L L) as L1  group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal4,part4,Partno4 ) as L2       " +
            "where I99_SA<>0 or qtyin<>0 or qtyout<>0   " +
            "or RuningSaw_SA<>0 or qtyin1<>0 or qtyout1<>0  " +
            "or Bevel_SA<>0 or qtyin2<>0 or qtyout2<>0   " +
            "or Straping_SA<>0  or qtyin4<>0 or qtyout4<>0   " +
            "group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal4,part4,Partno4        " +
            "order by PartnoAsal,Partno1 desc,Partno2 desc,Partno4 desc  " +
            "   " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc,straping_sa desc,Straping_in desc,Straping_out desc) AS Row,* into tempListPlankR1L2 from tempListPlankR1L1   " +
            "where I99_SA+RuningSaw_SA+Bevel_SA+Straping_SA+i99_in+i99_out+Runingsaw_in+Runingsaw_out+Bevel_in+Bevel_out+straping_in+straping_out>0    " +
            "  " +
            "select i99_partno,i99_SA,i99_in,i99_out,i99_SA+i99_Saldo i99_Saldo,qi,qo,  " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_SA+runingsaw_Saldo runingsaw_Saldo,qi1,qo1,  " +
            "Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_SA+Bevel_Saldo Bevel_Saldo,qi2,qo2,  " +
            "Partnoasal4,part4,Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_SA+Straping_Saldo Straping_Saldo,qi4,qo4  " +
            "into tempListPlankR1L3 from (      " +
            "select i99_partno,i99_SA, i99_in,i99_out,(i99_in-i99_out)i99_Saldo,qi,qo,  " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,(runingsaw_in-runingsaw_out)runingsaw_Saldo,qi1,qo1,  " +
            "Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,(Bevel_in-Bevel_out) Bevel_Saldo,qi2,qo2,  " +
            "Partnoasal4,part4,Straping_partno,Straping_SA,Straping_in,Straping_out,(Straping_in-Straping_out)Straping_Saldo,qi4,qo4 from (      " +
            "select  i99_partno,      " +
            "case when (select count(i99_partno) from tempListPlankR1L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,      " +
            "case when (select count(i99_partno) from tempListPlankR1L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,      " +
            "case when (select count(i99_partno) from tempListPlankR1L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out,  " +
            "case when (select count(i99_partno) from tempListPlankR1L2 where i99_partno=A.i99_partno and row < A.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi,  " +
            "case when (select count(i99_partno) from tempListPlankR1L2 where i99_partno=A.i99_partno and row < A.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo,  " +
            "Partnoasal1,part1,runingsaw_partno,    " +
            "case when  (select count(runingsaw_partno) from tempListPlankR1L2 where Partnoasal1=A.Partnoasal1 and     " +
            "runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,      " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L2 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,      " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L2 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "row < A.row )>0 then '0' else runingsaw_out end runingsaw_out,  " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L2 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "row < A.row and isnull(Qi1,0)>0)=0 then isnull(qi1,0) else 0 end qi1,  " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L2 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "row < A.row and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1,  " +
            "   " +
            "A.Partnoasal2,A.part2,Bevel_partno,      " +
            "case when (select count(Bevel_partno) from tempListPlankR1L2 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and     " +
            "row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,      " +
            "case when (select count(Bevel_partno) from tempListPlankR1L2 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and     " +
            "row < A.row )>0 then '0' else Bevel_in end Bevel_in,      " +
            "case when (select count(Bevel_partno) from tempListPlankR1L2 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and     " +
            "row < A.row )>0 then '0' else Bevel_out end Bevel_out,  " +
            "case when (select count(Bevel_partno) from tempListPlankR1L2 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and     " +
            "row < A.row and isnull(Qi2,0)>0)=0 then isnull(qi2,0) else 0 end qi2,  " +
            "case when (select count(Bevel_partno) from tempListPlankR1L2 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and     " +
            "row < A.row and isnull(Qo2,0)>0)=0 then isnull(qo2,0) else 0 end qo2,    " +
            "  " +
            " A.Partnoasal4,A.part4,Straping_partno,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L2 where Partnoasal4=A.Partnoasal4 and part4=A.part4 and Straping_partno=A.Straping_partno and     " +
            "row < A.row )>0 then '0' else Straping_SA end Straping_SA,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L2 where Partnoasal4=A.Partnoasal4 and part4=A.part4 and Straping_partno=A.Straping_partno and     " +
            "row < A.row )>0 then '0' else Straping_in end Straping_in,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L2 where Partnoasal4=A.Partnoasal4 and part4=A.part4 and Straping_partno=A.Straping_partno and     " +
            "row < A.row )>0 then '0' else Straping_out end Straping_out,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L2 where Partnoasal4=A.Partnoasal4 and part4=A.part4 and Straping_partno=A.Straping_partno and     " +
            "row < A.row and isnull(Qi4,0)>0)=0 then isnull(qi4,0) else 0 end qi4,    " +
            "case when (select count(Straping_partno) from tempListPlankR1L2 where Partnoasal4=A.Partnoasal4 and part4=A.part4 and Straping_partno=A.Straping_partno and     " +
            "row < A.row and isnull(Qo4,0)>0)=0 then isnull(qo4,0) else 0 end qo4    " +
            "  " +
            "from tempListPlankR1L2 A) B) C where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or     " +
            "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0 or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0   " +
            "  " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc) AS Row,*   " +
            "into tempListPlankR1L4   " +
            "from  tempListPlankR1L3 TL left join (   " +
            "select partnoasal5,part5,partno5,SUM(qtyin) Qty,sfrom from (   " +
            "select   " +
            "case when ISNULL(sfrom,'')='strapingR1' then (select partno from FC_Items where ID In (select ItemID0 from T1_LR1_Straping where ID=A.T1SerahID ))   " +
            "when ISNULL(sfrom,'')='bevelR1' then (select PartNo from FC_Items where ID In (select ItemID0 from T1_LR1_bevel where ID=A.T1SerahID )) end PartnoAsal5,   " +
            "case when ISNULL(sfrom,'')='strapingR1' then (select partno from FC_Items where ID In (select ItemID  from T1_LR1_Straping where ID= A.T1SerahID))   " +
            "when ISNULL(sfrom,'')='bevelR1' then (select  PartNo  from FC_Items where ID In (select ItemID from T1_LR1_bevel where ID=A.T1SerahID )) end Part5,   " +
            "I.PartNo PartNo5,   " +
            "qtyin,sfrom from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID where A.rowstatus>-1 and (ISNULL(sfrom,'')='bevelR1' or    " +
            "ISNULL(sfrom,'')='strapingR1') and LEFT(convert(char,tgltrans,112),6)= @thbln )T group by partnoasal5,part5,partno5,sfrom  " +
            ")T3   " +
            "on  T3.Part5 = case when sfrom ='bevelR1' then  TL.Part2 when rtrim(sfrom) ='StrapingR1' then TL.part4 end and   " +
            "T3.PartnoAsal5=case when sfrom ='bevelR1' then  TL.Partnoasal2 when sfrom ='StrapingR1' then TL.partnoasal4 end   " +
            "order by i99_partno,Partnoasal1,part1,RUningsaw_Partno,Partnoasal2,part2,Bevel_Partno,Partnoasal4,part4,Straping_Partno   " +
            "    " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc,Straping_SA desc,Straping_in desc,Straping_out desc)as row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,qi,qo,  " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,  " +
            "runingsaw_Saldo,qi1,qo1,Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2,Partnoasal4,part4,  " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4,  " +
            "case when partnoasal5=partnoasal4 or partnoasal5=partnoasal2 then partnoasal5 else '-'end partnoasal5,  " +
            "part5,  " +
            "partno5,  " +
            "Qty  " +
            "into tempListPlankR1L5   " +
            "from tempListPlankR1L4  " +
            "   " +
            "select row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,qi,qo,  " +
            "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,qi1,qo1,  " +
            "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2,  " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4,  " +
            "T3_partno,T3_Qty   " +
            "from (      " +
            "select row,i99_partno,i99_SA, i99_in,i99_out,i99_Saldo,qi,qo,  " +
            "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,qi1,qo1,  " +
            "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2,  " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4,  " +
            "T3_partno,case when isnull(t3_partno,'-')='-' then 0 else T3_Qty  end  T3_Qty    " +
            "from (      " +
            "select row,case when (select count(i99_partno) from tempListPlankR1L5 where i99_partno=A.i99_partno and row < A.row )>0 then '-' else i99_partno end i99_partno,    " +
            "case when (select count(i99_partno) from tempListPlankR1L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,      " +
            "case when (select count(i99_partno) from tempListPlankR1L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,      " +
            "case when (select count(i99_partno) from tempListPlankR1L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out,     " +
            "case when (select count(i99_partno) from tempListPlankR1L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_saldo end i99_saldo,    " +
            "case when (select count(i99_partno) from tempListPlankR1L5 where i99_partno=A.i99_partno and row < A.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi,  " +
            "case when (select count(i99_partno) from tempListPlankR1L5 where i99_partno=A.i99_partno and row < A.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo,  " +
            "   " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "part1=A.part1 and row < A.row )>0 then '-' else runingsaw_partno end runingsaw_partno,    " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L5 where Partnoasal1=A.Partnoasal1 and     " +
            "part1=A.part1 and runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,      " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,      " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_out end runingsaw_out,    " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_saldo end runingsaw_saldo,    " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "part1=A.part1 and row < A.row and isnull(Qi1,0)>0)=0 then isnull(Qi1,0) else 0 end Qi1,  " +
            "case when (select count(runingsaw_partno) from tempListPlankR1L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and     " +
            "part1=A.part1 and row < A.row  and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1,  " +
            "  " +
            "A.Partnoasal2,A.part2,    " +
            "case when (select count(Bevel_partno) from tempListPlankR1L5 where Bevel_partno=A.Bevel_partno and     " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '-' else Bevel_partno end Bevel_partno,      " +
            "case when (select count(Bevel_partno) from tempListPlankR1L5 where Bevel_partno=A.Bevel_partno and     " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,      " +
            "case when (select count(Bevel_partno) from tempListPlankR1L5 where Bevel_partno=A.Bevel_partno and     " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_in end Bevel_in,      " +
            "case when (select count(Bevel_partno) from tempListPlankR1L5 where Bevel_partno=A.Bevel_partno and     " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_out end Bevel_out,   " +
            "case when (select count(Bevel_partno) from tempListPlankR1L5 where Bevel_partno=A.Bevel_partno and     " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_saldo end Bevel_saldo,     " +
            "case when (select count(Bevel_partno) from tempListPlankR1L5 where Bevel_partno=A.Bevel_partno and     " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row and isnull(Qi2,0)>0)=0 then isnull(Qi2,0) else 0 end Qi2,  " +
            "case when (select count(Bevel_partno) from tempListPlankR1L5 where Bevel_partno=A.Bevel_partno and     " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row and isnull(Qo2,0)>0)=0 then isnull(Qo2,0) else 0 end Qo2,  " +
            "  " +
            "case when (select count(Straping_partno) from tempListPlankR1L5 where Straping_partno=A.Straping_partno and     " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '-' else Straping_partno end Straping_partno,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L5 where Straping_partno=A.Straping_partno and     " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_SA end Straping_SA,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L5 where Straping_partno=A.Straping_partno and     " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_in end Straping_in,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L5 where Straping_partno=A.Straping_partno and     " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_out end Straping_out,      " +
            "case when (select count(Straping_partno) from tempListPlankR1L5 where Straping_partno=A.Straping_partno and     " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_saldo end Straping_saldo,  " +
            "case when (select count(Straping_partno) from tempListPlankR1L5 where Straping_partno=A.Straping_partno and     " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row and isnull(Qi4,0)>0)=0 then isnull(Qi4,0) else 0 end Qi4,  " +
            "case when (select count(Straping_partno) from tempListPlankR1L5 where Straping_partno=A.Straping_partno and     " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row and isnull(Qo4,0)>0)=0 then isnull(Qo4,0) else 0 end Qo4,  " +
            "   " +
            "case when (select count(partno5) from tempListPlankR1L5 where i99_partno=A.i99_partno and partno5=A.partno5 and     " +
            "partnoasal5=A.partnoasal5 and part5=A.part5 and row < A.row )>0 then '-' else partno5 end T3_partno,    " +
            "    " +
            "qty  T3_Qty      " +
            "from tempListPlankR1L5 A) B) C    " +
            "where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or     " +
            "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0   " +
            "or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0    " +
            "or T3_Qty<>0   " +
            "or T3_Qty<>0   " +

                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L1   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L2   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L3   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L4 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR1L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR1L5";

            return strsql;
        }

        public string ViewProsesListPlankR2(string thnbln)
        {
            string strsql = "declare @thbln char(6)  " +
            "set @thbln='" + thnbln + "'  " +
            "declare @thnbln0 varchar(6)  " +
            "declare @tgl datetime  " +
            "declare @users varchar(25)  " +
            "set @tgl=CAST( (@thbln+ '01') as datetime)  " +
            "set @tgl= DATEADD(month,-1,@tgl)  " +
            "set @thnbln0=LEFT(convert(char,@tgl,112),6)  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L1   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L2   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L3   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L4 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L5 " +
            "   " +
            "select ROW_NUMBER() OVER (ORDER BY PartnoAsal0,I99_SA desc,qtyin desc,qtyout desc) AS Row,isnull(PartNoAsal0,'')PartNoAsal,  part0,isnull(PartNo,'')PartNo,  " +
            "isnull(i99_SA,0)i99_SA, isnull(qtyin,0)qtyin, isnull(qtyout,0)qtyout,isnull(Qi,0)Qi,isnull(Qo,0)Qo, isnull(PartNoAsal1,'')PartNoAsal1,part1,  " +
            "isnull(PartNo1,'')PartNo1, isnull(runingsaw_SA,0)runingsaw_SA,isnull(qtyin1,0)qtyin1, isnull(qtyout1,0)qtyout1, isnull(qi1,0)qi1, isnull(qo1,0)qo1,  " +
            "isnull(PartNoAsal2,'')PartNoAsal2,part2, isnull(PartNo2,'')PartNo2, isnull(Bevel_SA,0)Bevel_SA,isnull(qtyin2,0)qtyin2, isnull(qtyout2,0)qtyout2, " +
            "isnull(qi2,0)qi2, isnull(qo2,0)qo2,isnull(PartNoAsal4,'')PartNoAsal4,part4, isnull(PartNo4,'')PartNo4, isnull(straping_SA,0)straping_SA,  " +
            "isnull(qtyin4,0)qtyin4, isnull(qtyout4,0)qtyout4 , isnull(qi4,0)qi4, isnull(qo4,0)qo4   " +
            " into tempListPlankR2L    " +
            "from (     " +
            "select distinct * from (   " +
            "select distinct I0.PartNo PartNoAsal from T1_SaldoListPlankR2 A inner join fc_items I0 on A.ItemID0=I0.ID    " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0 " +
            "union all " +
            "select distinct partnoasal from vw_KartuStockListPlankR2 where LEFT(convert(char,tanggal,112),6)=@thbln " +
            ") as P  " +
            "left join     " +
            "(     " +
            "select PartNoAsal PartNoAsal0,PartNoAsal part0,PartNo,sum(i99_SA) i99_SA,sum(qtyin)qtyin,sum(qtyout)qtyout,sum(isnull(Qi,0))Qi,sum(isnull(Qo,0))Qo from (     " +
            "select I0.PartNo PartNoAsal,I.PartNo part0,I.PartNo PartNo,Saldo i99_SA,0 qtyin,0 qtyout,0 Qi,0 Qo  " +
            "from T1_SaldoListPlankR2 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='i99'     " +
            "union all    " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR2  where qty>0 and process='i99' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR2  where qty<0 and process='i99' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union All " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplankR2 A    " +
            "where qty>0 and process Like '%adjini99R2%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1  qtyout,0 Qi,qty*-1 Qo  " +
            "from vw_KartuStockListplankR2 A where qty<0 and process Like '%adjouti99R2%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as i99     " +
            "group by PartNoAsal,part0,PartNo having sum(i99_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0) as i99A on i99A.PartNoAsal0=P.PartnoAsal " +
            "left join     " +
            "(     " +
            "select PartNoAsal PartNoAsal1,part1,PartNo PartNo1,sum(runingsaw_SA) runingsaw_SA,sum(qtyin)qtyin1,sum(qtyout)qtyout1,sum(isnull(Qi,0))Qi1,sum(isnull(Qo,0))Qo1 from (     " +
            "select I0.PartNo PartNoAsal,I.PartNo part1,I.PartNo PartNo,Saldo runingsaw_SA,0 qtyin,0 qtyout,0 Qi,0 Qo from T1_SaldoListPlankR2 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='runingsaw'     " +
            "union all    " +
            "select PartNoAsal,PartNoAsal1 part1,PartNo,0 runingsaw_SA,qty qtyin,0 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR2  where qty>0 and process='runingsaw' and     " +
            "left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part1,PartNo,0 runingsaw_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR2  where qty<0 and process='runingsaw' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union all     " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplankR2 A    " +
            "where qty>0 and process Like '%adjinruningsawR2%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo  " +
            "from vw_KartuStockListplankR2 A where qty<0 and process Like '%adjoutruningsawR2%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as runingsaw     " +
            "group by PartNoAsal,part1,PartNo having sum(runingsaw_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0 ) as runingsawA  " +
            "on  P.PartNoAsal=runingsawA.PartNoAsal1 " +
            "left join      " +
            "(     " +
            "select PartNoAsal PartNoAsal2,part2,PartNo PartNo2,SUM(Bevel_SA) Bevel_SA,sum(qtyin)qtyin2,sum(qtyout)qtyout2,sum(isnull(Qi,0))Qi2,sum(isnull(Qo,0))Qo2  from (   " +
            "select I0.PartNo PartNoAsal,I.PartNo part2,I.PartNo PartNo,Saldo Bevel_SA,0 qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid from T1_SaldoListPlankR2 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='Bevel'     " +
            "union all    " +
            "select PartNoAsal,PartNoAsal1 part2,PartNo,0 Bevel_SA,qty qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR2  where qty>0 and process='Bevel' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln    " +
            "union all   " +
            "select PartNoAsal,PartNoAsal1 part2,PartNo,0 Bevel_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR2  where qty<0 and process='Bevel' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union All " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo,ItemID0,itemid  from vw_KartuStockListplankR2 A    " +
            "where qty>0 and process Like '%adjinBevelR2%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo ,ItemID0,itemid  " +
            "from vw_KartuStockListplankR2 A where qty<0 and process Like '%adjoutBevelR2%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as bevel  " +
            "group by PartNoAsal,part2,PartNo  having sum(Bevel_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0 ) as BevelA      " +
            "on P.PartNoAsal=BevelA.PartNoAsal2  " +
            "left join      " +
            "(     " +
            "select PartNoAsal PartNoAsal4,part4,PartNo PartNo4,sum(straping_SA)straping_SA, " +
            "sum(qtyin)qtyin4,sum(qtyout)qtyout4,sum(isnull(Qi,0))Qi4,sum(isnull(Qo,0))Qo4 from (     " +
            "select I0.PartNo PartNoAsal,I.PartNo part4,I.PartNo PartNo,Saldo straping_SA,0 qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid from T1_SaldoListPlankR2 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='strapingR2'     " +
            "union all " +
            "select PartNoAsal,PartNoAsal1 part4,PartNo,0 straping_SA,qty qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR2  where qty>0 and process='strapingR2' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all   " +
            "select PartNoAsal,PartNoAsal1 part4,PartNo,0 straping_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR2  where qty<0 and process='strapingR2' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union All " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo,ItemID0,itemid  from vw_KartuStockListplankR2 A    " +
            "where qty>0 and process Like '%adjinstrapingR2%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo ,ItemID0,itemid  " +
            "from vw_KartuStockListplankR2 A where qty<0 and process Like '%adjoutstrapingR2%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as straping  " +
            "group by PartNoAsal,part4,PartNo   having sum(straping_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0  ) as strapingA      " +
            "on P.PartNoAsal=strapingA.PartNoAsal4 " +
            ") as d " +
            " " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L1  " +
            " " +
            "select  case when isnull(PartnoAsal,'')<>'' then  PartnoAsal  else case when isnull(PartnoAsal1,'')<>'' then PartnoAsal1 else " +
            "case when isnull(PartnoAsal2,'')<>'' then PartnoAsal2 else case when isnull(PartnoAsal4,'')<>'' then PartnoAsal4 else '' end end end end I99_Partno,sum(I99_SA) I99_SA,isnull(sum(qtyin),0) I99_In,isnull(sum(qtyout),0) I99_Out,isnull(sum(qi),0)qi,isnull(sum(qo),0)qo, " +
            "Partnoasal1,part1,isnull(Partno1,0) RuningSaw_Partno,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0) RuningSaw_In,isnull(sum(qtyout1),0) RuningSaw_Out,isnull(sum(qi1),0)qi1,isnull(sum(qo1),0)qo1, " +
            "Partnoasal2,part2,isnull(Partno2,0) Bevel_Partno, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0) Bevel_In,isnull(sum(qtyout2),0) Bevel_Out,isnull(sum(qi2),0)qi2,isnull(sum(qo2),0)qo2, " +
            "Partnoasal4,part4,isnull(Partno4,0) Straping_Partno, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0) Straping_In,isnull(sum(qtyout4),0) Straping_out,isnull(sum(qi4),0)qi4,isnull(sum(qo4),0)qo4  " +
            "into tempListPlankR2L1  from (     " +
            "select PartnoAsal,sum(I99_SA) I99_SA,isnull(sum(qtyin),0)qtyin,isnull(sum(qtyout),0)qtyout,isnull(sum(qi),0)qi,isnull(sum(qo),0)qo, " +
            "Partnoasal1,part1,Partno1,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0)qtyin1,isnull(sum(qtyout1),0)qtyout1,isnull(sum(qi1),0)qi1,isnull(sum(qo1),0)qo1, " +
            "Partnoasal2,part2,Partno2, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0)qtyin2,isnull(sum(qtyout2),0)qtyout2,isnull(sum(qi2),0)qi2,isnull(sum(qo2),0)qo2, " +
            "Partnoasal4,part4,Partno4, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0)qtyin4,isnull(sum(qtyout4),0)qtyout4,isnull(sum(qi4),0)qi4,isnull(sum(qo1),0)qo4 from (     " +
            "select Row,PartnoAsal,     " +
            "case when (select COUNT(Partno) from tempListPlankR2L where partno=L.partno and ROW<L.row)=0 then isnull(I99_SA,0) else 0 end I99_SA,     " +
            "case when (select COUNT(Partno) from tempListPlankR2L where partno=L.partno and ROW<L.row)=0 then isnull(qtyin,0) else 0 end qtyin,     " +
            "case when (select COUNT(Partno) from tempListPlankR2L where partno=L.partno and ROW<L.row)=0 then isnull(qtyout,0) else 0 end qtyout, " +
            "case when (select COUNT(Partno) from tempListPlankR2L where partno=L.partno and ROW<L.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi, " +
            "case when (select COUNT(Partno) from tempListPlankR2L where partno=L.partno and ROW<L.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo,    " +
            "Partnoasal1,part1, Partno1,     " +
            "case when (select COUNT(Partno1) from tempListPlankR2L where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(RuningSaw_SA,0) else 0 end RuningSaw_SA,     " +
            "case when (select COUNT(Partno1) from tempListPlankR2L where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyin1,0) else 0 end qtyin1,     " +
            "case when (select COUNT(Partno1) from tempListPlankR2L where partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyout1,0) else 0 end qtyout1, " +
            "case when (select COUNT(Partno1) from tempListPlankR2L where partno=L.partno and partno1=L.partno1 and ROW<L.row and isnull(Qi1,0)>0)=0 then isnull(qi1,0) else 0 end qi1, " +
            "case when (select COUNT(Partno1) from tempListPlankR2L where partno=L.partno and partno1=L.partno1 and ROW<L.row and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1,    " +
            "Partnoasal2,part2,Partno2,    " +
            "case when (select COUNT(Partno2) from tempListPlankR2L where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(Bevel_SA,0) else 0 end Bevel_SA,    " +
            "case when (select COUNT(Partno2) from tempListPlankR2L where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyin2,0) else 0 end qtyin2,     " +
            "case when (select COUNT(Partno2) from tempListPlankR2L where partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyout2,0) else 0 end qtyout2, " +
            "case when (select COUNT(Partno2) from tempListPlankR2L where partno=L.partno and partno2=L.partno2 and ROW<L.row and isnull(Qi2,0)>0)=0 then isnull(qi2,0) else 0 end qi2, " +
            "case when (select COUNT(Partno2) from tempListPlankR2L where partno=L.partno and partno2=L.partno2 and ROW<L.row and isnull(Qo2,0)>0)=0 then isnull(qo2,0) else 0 end qo2,   " +
            "Partnoasal4,part4,Partno4,     " +
            "case when (select COUNT(Partno4) from tempListPlankR2L where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(Straping_SA,0) else 0 end Straping_SA,    " +
            "case when (select COUNT(Partno4) from tempListPlankR2L where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyin4,0) else 0 end qtyin4,     " +
            "case when (select COUNT(Partno4) from tempListPlankR2L where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyout4,0) else 0 end qtyout4,  " +
            "case when (select COUNT(Partno4) from tempListPlankR2L where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qi4,0) else 0 end qi4,  " +
            "case when (select COUNT(Partno4) from tempListPlankR2L where partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qo4,0) else 0 end qo4     " +
            "from tempListPlankR2L L) as L1  group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal4,part4,Partno4 ) as L2      " +
            "where I99_SA<>0 or qtyin<>0 or qtyout<>0  " +
            "or RuningSaw_SA<>0 or qtyin1<>0 or qtyout1<>0 " +
            "or Bevel_SA<>0 or qtyin2<>0 or qtyout2<>0  " +
            "or Straping_SA<>0  or qtyin4<>0 or qtyout4<>0  " +
            "group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal4,part4,Partno4 " +
            "order by PartnoAsal,Partno1 desc,Partno2 desc,Partno4 desc  " +
            " " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L2  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L3  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L4   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L5 " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc,straping_sa desc,Straping_in desc,Straping_out desc) AS Row,* into tempListPlankR2L2 from tempListPlankR2L1  " +
            "where I99_SA+RuningSaw_SA+Bevel_SA+Straping_SA+i99_in+i99_out+Runingsaw_in+Runingsaw_out+Bevel_in+Bevel_out+straping_in+straping_out>0   " +
            " " +
            "select i99_partno,i99_SA,i99_in,i99_out,i99_SA+i99_Saldo i99_Saldo,qi,qo, " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_SA+runingsaw_Saldo runingsaw_Saldo,qi1,qo1, " +
            "Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_SA+Bevel_Saldo Bevel_Saldo,qi2,qo2, " +
            "Partnoasal4,part4,Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_SA+Straping_Saldo Straping_Saldo,qi4,qo4 " +
            "into tempListPlankR2L3 from ( " +
            "select i99_partno,i99_SA, i99_in,i99_out,(i99_in-i99_out)i99_Saldo,qi,qo, " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,(runingsaw_in-runingsaw_out)runingsaw_Saldo,qi1,qo1, " +
            "Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,(Bevel_in-Bevel_out) Bevel_Saldo,qi2,qo2, " +
            "Partnoasal4,part4,Straping_partno,Straping_SA,Straping_in,Straping_out,(Straping_in-Straping_out)Straping_Saldo,qi4,qo4 from ( " +
            "select  i99_partno, " +
            "case when (select count(i99_partno) from tempListPlankR2L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,     " +
            "case when (select count(i99_partno) from tempListPlankR2L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,     " +
            "case when (select count(i99_partno) from tempListPlankR2L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out, " +
            "case when (select count(i99_partno) from tempListPlankR2L2 where i99_partno=A.i99_partno and row < A.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi, " +
            "case when (select count(i99_partno) from tempListPlankR2L2 where i99_partno=A.i99_partno and row < A.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo, " +
            "Partnoasal1,part1,runingsaw_partno,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L2 where i99_partno=A.i99_partno and    " +
            "runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,     " +
            "case when(select count(runingsaw_partno) from tempListPlankR2L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,     " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row )>0 then '0' else runingsaw_out end runingsaw_out, " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row and isnull(Qi1,0)>0)=0 then isnull(qi1,0) else 0 end qi1, " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1, " +
            "  " +
            "A.Partnoasal2,A.part2,Bevel_partno,     " +
            "case when (select count(Bevel_partno) from tempListPlankR2L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,     " +
            "case when (select count(Bevel_partno) from tempListPlankR2L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row )>0 then '0' else Bevel_in end Bevel_in,     " +
            "case when (select count(Bevel_partno) from tempListPlankR2L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row )>0 then '0' else Bevel_out end Bevel_out, " +
            "case when (select count(Bevel_partno) from tempListPlankR2L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row and isnull(Qi2,0)>0)=0 then isnull(qi2,0) else 0 end qi2, " +
            "case when (select count(Bevel_partno) from tempListPlankR2L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row and isnull(Qo2,0)>0)=0 then isnull(qo2,0) else 0 end qo2,   " +
            " " +
            " A.Partnoasal4,A.part4,Straping_partno,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row )>0 then '0' else Straping_SA end Straping_SA,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row )>0 then '0' else Straping_in end Straping_in,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row )>0 then '0' else Straping_out end Straping_out,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row)=0 then isnull(qi4,0) else 0 end qi4,   " +
            "case when (select count(Straping_partno) from tempListPlankR2L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row)=0 then isnull(qo4,0) else 0 end qo4   " +
            " " +
            "from tempListPlankR2L2 A) B) C where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or    " +
            "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0 or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0  " +
            " " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc,straping_sa desc,Straping_in desc,Straping_out desc) AS Row,*  " +
            "into tempListPlankR2L4  " +
            "from  tempListPlankR2L3 TL left join (  " +
            "select partnoasal5,part5,partno5,SUM(qtyin) Qty,sfrom from (  " +
            "select  " +
            "case when ISNULL(sfrom,'')='strapingR2' then (select partno from FC_Items where ID In (select ItemID0 from T1_LR2_Straping where ID=A.T1SerahID ))  " +
            "when ISNULL(sfrom,'')='bevelR2' then (select PartNo from FC_Items where ID In (select ItemID0 from T1_LR2_bevel where ID=A.T1SerahID )) end PartnoAsal5,  " +
            "case when ISNULL(sfrom,'')='strapingR2' then (select partno from FC_Items where ID In (select ItemID  from T1_LR2_Straping where ID= A.T1SerahID))  " +
            "when ISNULL(sfrom,'')='bevelR2' then (select  PartNo  from FC_Items where ID In (select ItemID from T1_LR2_bevel where ID=A.T1SerahID )) end Part5,  " +
            "I.PartNo PartNo5,  " +
            "qtyin,sfrom from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID where A.rowstatus>-1 and (ISNULL(sfrom,'')='bevelR2' or   " +
            "ISNULL(sfrom,'')='strapingR2') and LEFT(convert(char,tgltrans,112),6)= @thbln )T group by partnoasal5,part5,partno5,sfrom " +
            ")T3  " +
            "on  T3.Part5 = case when sfrom ='bevelR2' then  TL.Part2 when rtrim(sfrom) ='StrapingR2' then TL.part4 end and  " +
            "T3.PartnoAsal5=case when sfrom ='bevelR2' then  TL.Partnoasal2 when sfrom ='StrapingR2' then TL.partnoasal4 end  " +
            "order by i99_partno,Partnoasal1,part1,RUningsaw_Partno,Partnoasal2,part2,Bevel_Partno,Partnoasal4,part4,Straping_Partno  " +
            "   " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc,straping_sa desc,Straping_in desc,Straping_out desc)as row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,qi,qo, " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out, " +
            "runingsaw_Saldo,qi1,qo1,Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2,Partnoasal4,part4, " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4, " +
            "case when partnoasal5=partnoasal4 or partnoasal5=partnoasal2 then partnoasal5 else '-'end partnoasal5,part5,partno5,Qty " +
            "into tempListPlankR2L5  " +
            "from tempListPlankR2L4 " +
            "  " +
            "select row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,qi,qo, " +
            "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,qi1,qo1, " +
            "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2, " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4, " +
            "T3_partno,T3_Qty  " +
            "from (     " +
            "select row,i99_partno,i99_SA, i99_in,i99_out,i99_Saldo,qi,qo, " +
            "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,qi1,qo1, " +
            "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2, " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4, " +
            "T3_partno,case when isnull(t3_partno,'-')='-' then 0 else T3_Qty  end  T3_Qty   " +
            "from (     " +
            "select row,case when (select count(i99_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and row < A.row )>0 then '-' else i99_partno end i99_partno,   " +
            "case when (select count(i99_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,     " +
            "case when (select count(i99_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,     " +
            "case when (select count(i99_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out,    " +
            "case when (select count(i99_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_saldo end i99_saldo,   " +
            "case when (select count(i99_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and row < A.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi, " +
            "case when (select count(i99_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and row < A.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo, " +
            "  " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L5 where Partnoasal1 =A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '-' else runingsaw_partno end runingsaw_partno,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L5 where Partnoasal1=A.Partnoasal1 and    " +
            "part1=A.part1 and runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,     " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,     " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_out end runingsaw_out,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_saldo end runingsaw_saldo,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row and isnull(Qi1,0)>0)=0 then isnull(Qi1,0) else 0 end Qi1, " +
            "case when (select count(runingsaw_partno) from tempListPlankR2L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row  and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1, " +
            " " +
            "A.Partnoasal2,A.part2,   " +
            "case when (select count(Bevel_partno) from tempListPlankR2L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '-' else Bevel_partno end Bevel_partno,     " +
            "case when (select count(Bevel_partno) from tempListPlankR2L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,     " +
            "case when (select count(Bevel_partno) from tempListPlankR2L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_in end Bevel_in,     " +
            "case when (select count(Bevel_partno) from tempListPlankR2L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_out end Bevel_out,  " +
            "case when  (select count(Bevel_partno) from tempListPlankR2L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_saldo end Bevel_saldo,    " +
            "case when (select count(Bevel_partno) from tempListPlankR2L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row and isnull(Qi2,0)>0)=0 then isnull(Qi2,0) else 0 end Qi2, " +
            "case when (select count(Bevel_partno) from tempListPlankR2L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row and isnull(Qo2,0)>0)=0 then isnull(Qo2,0) else 0 end Qo2, " +
            " " +
            "case when (select count(Straping_partno) from tempListPlankR2L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '-' else Straping_partno end Straping_partno,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_SA end Straping_SA,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_in end Straping_in,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_out end Straping_out,     " +
            "case when (select count(Straping_partno) from tempListPlankR2L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_saldo end Straping_saldo, " +
            "case when (select count(Straping_partno) from tempListPlankR2L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row and isnull(Qi4,0)>0)=0 then isnull(Qi4,0) else 0 end Qi4, " +
            "case when (select count(Straping_partno) from tempListPlankR2L5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row and isnull(Qo4,0)>0)=0 then isnull(Qo4,0) else 0 end Qo4, " +
            "  " +
            "case when (select count(partno5) from tempListPlankR2L5 where i99_partno=A.i99_partno and partno5=A.partno5 and    " +
            "partnoasal5=A.partnoasal5 and part5=A.part5 and row < A.row )>0 then '-' else partno5 end T3_partno,   " +
            "   " +
            "qty  T3_Qty     " +
            "from tempListPlankR2L5 A) B) C   " +
            "where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or    " +
            "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0  " +
            "or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0   " +
            "or T3_Qty<>0  " +
            "or T3_Qty<>0  " +

            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L1   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L2   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L3   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L4 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR2L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR2L5 ";
            return strsql;
        }

        public string ViewProsesListPlankR3(string thnbln)
        {
            string strsql = "declare @thbln char(6)  " +
            "set @thbln='" + thnbln + "'  " +
            "declare @thnbln0 varchar(6)  " +
            "declare @tgl datetime  " +
            "declare @users varchar(25)  " +
            "set @tgl=CAST( (@thbln+ '01') as datetime)  " +
            "set @tgl= DATEADD(month,-1,@tgl)  " +
            "set @thnbln0=LEFT(convert(char,@tgl,112),6)  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L1   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L2   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L3   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L4 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L5 " +
            "   " +
            "select ROW_NUMBER() OVER (ORDER BY PartnoAsal0,I99_SA desc,qtyin desc,qtyout desc) AS Row,isnull(PartNoAsal0,'')PartNoAsal,  part0,isnull(PartNo,'')PartNo,  " +
            "isnull(i99_SA,0)i99_SA, isnull(qtyin,0)qtyin, isnull(qtyout,0)qtyout,isnull(Qi,0)Qi,isnull(Qo,0)Qo, isnull(PartNoAsal1,'')PartNoAsal1,part1,  " +
            "isnull(PartNo1,'')PartNo1, isnull(runingsaw_SA,0)runingsaw_SA,isnull(qtyin1,0)qtyin1, isnull(qtyout1,0)qtyout1, isnull(qi1,0)qi1, isnull(qo1,0)qo1,  " +
            "isnull(PartNoAsal2,'')PartNoAsal2,part2, isnull(PartNo2,'')PartNo2, isnull(Bevel_SA,0)Bevel_SA,isnull(qtyin2,0)qtyin2, isnull(qtyout2,0)qtyout2, " +
            "isnull(qi2,0)qi2, isnull(qo2,0)qo2,isnull(PartNoAsal4,'')PartNoAsal4,part4, isnull(PartNo4,'')PartNo4, isnull(straping_SA,0)straping_SA,  " +
            "isnull(qtyin4,0)qtyin4, isnull(qtyout4,0)qtyout4 , isnull(qi4,0)qi4, isnull(qo4,0)qo4   " +
            " into tempListPlankR3L    " +
            "from (     " +
            "select distinct * from (   " +
            "select distinct I0.PartNo PartNoAsal from T1_SaldoListPlankR3 A inner join fc_items I0 on A.ItemID0=I0.ID    " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0 " +
            "union all " +
            "select distinct partnoasal from vw_KartuStockListPlankR3 where LEFT(convert(char,tanggal,112),6)=@thbln " +
            ") as P  " +
            "left join     " +
            "(     " +
            "select PartNoAsal PartNoAsal0,PartNoAsal part0,PartNo,sum(i99_SA) i99_SA,sum(qtyin)qtyin,sum(qtyout)qtyout,sum(isnull(Qi,0))Qi,sum(isnull(Qo,0))Qo from (     " +
            "select I0.PartNo PartNoAsal,I.PartNo part0,I.PartNo PartNo,Saldo i99_SA,0 qtyin,0 qtyout,0 Qi,0 Qo  " +
            "from T1_SaldoListPlankR3 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='i99'     " +
            "union all    " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR3  where qty>0 and process='i99' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR3  where qty<0 and process='i99' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union All " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplankR3 A    " +
            "where qty>0 and process Like '%adjini99R3%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1  qtyout,0 Qi,qty*-1 Qo  " +
            "from vw_KartuStockListplankR3 A where qty<0 and process Like '%adjouti99R3%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as i99     " +
            "group by PartNoAsal,part0,PartNo having sum(i99_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0) as i99A on i99A.PartNoAsal0=P.PartnoAsal " +
            "left join     " +
            "(     " +
            "select PartNoAsal PartNoAsal1,part1,PartNo PartNo1,sum(runingsaw_SA) runingsaw_SA,sum(qtyin)qtyin1,sum(qtyout)qtyout1,sum(isnull(Qi,0))Qi1,sum(isnull(Qo,0))Qo1 from (     " +
            "select I0.PartNo PartNoAsal,I.PartNo part1,I.PartNo PartNo,Saldo runingsaw_SA,0 qtyin,0 qtyout,0 Qi,0 Qo from T1_SaldoListPlankR3 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='runingsaw'     " +
            "union all    " +
            "select PartNoAsal,PartNoAsal1 part1,PartNo,0 runingsaw_SA,qty qtyin,0 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR3  where qty>0 and process='runingsaw' and     " +
            "left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part1,PartNo,0 runingsaw_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo from vw_KartuStockListplankR3  where qty<0 and process='runingsaw' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union all     " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo from vw_KartuStockListplankR3 A    " +
            "where qty>0 and process Like '%adjinruningsawR3%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo  " +
            "from vw_KartuStockListplankR3 A where qty<0 and process Like '%adjoutruningsawR3%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as runingsaw     " +
            "group by PartNoAsal,part1,PartNo having sum(runingsaw_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0 ) as runingsawA  " +
            "on  P.PartNoAsal=runingsawA.PartNoAsal1 " +
            "left join      " +
            "(     " +
            "select PartNoAsal PartNoAsal2,part2,PartNo PartNo2,SUM(Bevel_SA) Bevel_SA,sum(qtyin)qtyin2,sum(qtyout)qtyout2,sum(isnull(Qi,0))Qi2,sum(isnull(Qo,0))Qo2  from (   " +
            "select I0.PartNo PartNoAsal,I.PartNo part2,I.PartNo PartNo,Saldo Bevel_SA,0 qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid from T1_SaldoListPlankR3 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='Bevel'     " +
            "union all    " +
            "select PartNoAsal,PartNoAsal1 part2,PartNo,0 Bevel_SA,qty qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR3  where qty>0 and process='Bevel' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln    " +
            "union all   " +
            "select PartNoAsal,PartNoAsal1 part2,PartNo,0 Bevel_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR3  where qty<0 and process='Bevel' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union All " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo,ItemID0,itemid  from vw_KartuStockListplankR3 A    " +
            "where qty>0 and process Like '%adjinBevelR3%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo ,ItemID0,itemid  " +
            "from vw_KartuStockListplankR3 A where qty<0 and process Like '%adjoutBevelR3%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as bevel  " +
            "group by PartNoAsal,part2,PartNo  having sum(Bevel_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0 ) as BevelA      " +
            "on P.PartNoAsal=BevelA.PartNoAsal2  " +
            "left join      " +
            "(     " +
            "select PartNoAsal PartNoAsal4,part4,PartNo PartNo4,sum(straping_SA)straping_SA, " +
            "sum(qtyin)qtyin4,sum(qtyout)qtyout4,sum(isnull(Qi,0))Qi4,sum(isnull(Qo,0))Qo4 from (     " +
            "select I0.PartNo PartNoAsal,I.PartNo part4,I.PartNo PartNo,Saldo straping_SA,0 qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid from T1_SaldoListPlankR3 A inner join fc_items I0 on A.ItemID0=I0.ID     " +
            "inner join FC_Items I on A.ItemID=I.ID where A.thnbln=@thnbln0  and A.Process='strapingR3'     " +
            "union all " +
            "select PartNoAsal,PartNoAsal1 part4,PartNo,0 straping_SA,qty qtyin,0 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR3  where qty>0 and process='strapingR3' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all   " +
            "select PartNoAsal,PartNoAsal1 part4,PartNo,0 straping_SA,0 qtyin,qty*-1 qtyout,0 Qi,0 Qo ,ItemID0,itemid  from vw_KartuStockListplankR3  where qty<0 and process='strapingR3' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln " +
            "union All " +
            "select PartNoAsal ,PartNoAsal1 part0,PartNo,0 i99_SA,qty qtyin,0 qtyout,qty Qi,0 Qo,ItemID0,itemid  from vw_KartuStockListplankR3 A    " +
            "where qty>0 and process Like '%adjinstrapingR3%' and  left(CONVERT(char,tanggal,112),6)=@thbln     " +
            "union all     " +
            "select PartNoAsal,PartNoAsal1 part0,PartNo,0 i99_SA,0 qtyin,qty*-1 qtyout,0 Qi,qty*-1 Qo ,ItemID0,itemid  " +
            "from vw_KartuStockListplankR3 A where qty<0 and process Like '%adjoutstrapingR3%' and      " +
            "left(CONVERT(char,tanggal,112),6)=@thbln) as straping  " +
            "group by PartNoAsal,part4,PartNo   having sum(straping_SA)<>0 or sum(qtyin)<>0 or sum(qtyout)<>0  ) as strapingA      " +
            "on P.PartNoAsal=strapingA.PartNoAsal4 " +
            ") as d " +
            " " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L1  " +
            " " +
            "select  case when isnull(PartnoAsal,'')<>'' then  PartnoAsal  else case when isnull(PartnoAsal1,'')<>'' then PartnoAsal1 else  " +
            "case when isnull(PartnoAsal2,'')<>'' then PartnoAsal2 else case when isnull(PartnoAsal4,'')<>'' then PartnoAsal4 else '' end end end end  " +
            "I99_Partno,sum(I99_SA) I99_SA,isnull(sum(qtyin),0) I99_In,isnull(sum(qtyout),0) I99_Out,isnull(sum(qi),0)qi,isnull(sum(qo),0)qo, " +
            "Partnoasal1,part1,isnull(Partno1,0) RuningSaw_Partno,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0) RuningSaw_In,isnull(sum(qtyout1),0) RuningSaw_Out,isnull(sum(qi1),0)qi1,isnull(sum(qo1),0)qo1, " +
            "Partnoasal2,part2,isnull(Partno2,0) Bevel_Partno, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0) Bevel_In,isnull(sum(qtyout2),0) Bevel_Out,isnull(sum(qi2),0)qi2,isnull(sum(qo2),0)qo2, " +
            "Partnoasal4,part4,isnull(Partno4,0) Straping_Partno, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0) Straping_In,isnull(sum(qtyout4),0) Straping_out,isnull(sum(qi4),0)qi4,isnull(sum(qo4),0)qo4  " +
            "into tempListPlankR3L1  from (     " +
            "select PartnoAsal,sum(I99_SA) I99_SA,isnull(sum(qtyin),0)qtyin,isnull(sum(qtyout),0)qtyout,isnull(sum(qi),0)qi,isnull(sum(qo),0)qo, " +
            "Partnoasal1,part1,Partno1,sum(RuningSaw_SA) RuningSaw_SA,isnull(sum(qtyin1),0)qtyin1,isnull(sum(qtyout1),0)qtyout1,isnull(sum(qi1),0)qi1,isnull(sum(qo1),0)qo1, " +
            "Partnoasal2,part2,Partno2, sum(Bevel_SA) Bevel_SA,isnull(sum(qtyin2),0)qtyin2,isnull(sum(qtyout2),0)qtyout2,isnull(sum(qi2),0)qi2,isnull(sum(qo2),0)qo2, " +
            "Partnoasal4,part4,Partno4, sum(Straping_SA) Straping_SA,isnull(sum(qtyin4),0)qtyin4,isnull(sum(qtyout4),0)qtyout4,isnull(sum(qi4),0)qi4,isnull(sum(qo4),0)qo4 from (     " +
            "select Row,PartnoAsal,     " +
            "case when (select COUNT(Partno) from tempListPlankR3L where partno=L.partno and ROW<L.row)=0 then isnull(I99_SA,0) else 0 end I99_SA,     " +
            "case when (select COUNT(Partno) from tempListPlankR3L where partno=L.partno and ROW<L.row)=0 then isnull(qtyin,0) else 0 end qtyin,     " +
            "case when (select COUNT(Partno) from tempListPlankR3L where partno=L.partno and ROW<L.row)=0 then isnull(qtyout,0) else 0 end qtyout, " +
            "case when (select COUNT(Partno) from tempListPlankR3L where partno=L.partno and ROW<L.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi, " +
            "case when (select COUNT(Partno) from tempListPlankR3L where partno=L.partno and ROW<L.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo,    " +
            "Partnoasal1,part1, Partno1,     " +
            "case when (select COUNT(Partno1) from tempListPlankR3L where partnoasal1=L.partnoasal1 and partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(RuningSaw_SA,0) else 0 end RuningSaw_SA,     " +
            "case when (select COUNT(Partno1) from tempListPlankR3L where partnoasal1=L.partnoasal1 and partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyin1,0) else 0 end qtyin1,     " +
            "case when (select COUNT(Partno1) from tempListPlankR3L where partnoasal1=L.partnoasal1 and partno=L.partno and partno1=L.partno1 and ROW<L.row)=0 then isnull(qtyout1,0) else 0 end qtyout1, " +
            "case when (select COUNT(Partno1) from tempListPlankR3L where partnoasal1=L.partnoasal1 and partno=L.partno and partno1=L.partno1 and ROW<L.row and isnull(Qi1,0)>0)=0 then isnull(qi1,0) else 0 end qi1, " +
            "case when (select COUNT(Partno1) from tempListPlankR3L where partnoasal1=L.partnoasal1 and partno=L.partno and partno1=L.partno1 and ROW<L.row and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1,    " +
            "Partnoasal2,part2,Partno2,    " +
            "case when (select COUNT(Partno2) from tempListPlankR3L where partnoasal2=L.partnoasal2 and partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(Bevel_SA,0) else 0 end Bevel_SA,    " +
            "case when (select COUNT(Partno2) from tempListPlankR3L where partnoasal2=L.partnoasal2 and partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyin2,0) else 0 end qtyin2,     " +
            "case when (select COUNT(Partno2) from tempListPlankR3L where partnoasal2=L.partnoasal2 and partno=L.partno and partno2=L.partno2 and ROW<L.row)=0 then isnull(qtyout2,0) else 0 end qtyout2, " +
            "case when (select COUNT(Partno2) from tempListPlankR3L where partnoasal2=L.partnoasal2 and partno=L.partno and partno2=L.partno2 and ROW<L.row and isnull(Qi2,0)>0)=0 then isnull(qi2,0) else 0 end qi2,  " +
            "case when (select COUNT(Partno2) from tempListPlankR3L where partnoasal2=L.partnoasal2 and partno=L.partno and partno2=L.partno2 and ROW<L.row and isnull(Qo2,0)>0)=0 then isnull(qo2,0) else 0 end qo2,   " +
            "Partnoasal4,part4,Partno4,     " +
            "case when (select COUNT(Partno4) from tempListPlankR3L where partnoasal4=L.partnoasal4 and partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(Straping_SA,0) else 0 end Straping_SA,    " +
            "case when (select COUNT(Partno4) from tempListPlankR3L where partnoasal4=L.partnoasal4 and partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyin4,0) else 0 end qtyin4,     " +
            "case when (select COUNT(Partno4) from tempListPlankR3L where partnoasal4=L.partnoasal4 and partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qtyout4,0) else 0 end qtyout4,  " +
            "case when (select COUNT(Partno4) from tempListPlankR3L where partnoasal4=L.partnoasal4 and partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qi4,0) else 0 end qi4,  " +
            "case when (select COUNT(Partno4) from tempListPlankR3L where partnoasal4=L.partnoasal4 and partno=L.partno and partno4=L.partno4 and ROW<L.row)=0 then isnull(qo4,0) else 0 end qo4  " +
            "from tempListPlankR3L L) as L1  group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal4,part4,Partno4 ) as L2      " +
            "where I99_SA<>0 or qtyin<>0 or qtyout<>0  " +
            "or RuningSaw_SA<>0 or qtyin1<>0 or qtyout1<>0 " +
            "or Bevel_SA<>0 or qtyin2<>0 or qtyout2<>0  " +
            "or Straping_SA<>0  or qtyin4<>0 or qtyout4<>0  " +
            "group by PartnoAsal,Partnoasal1,part1,Partno1,Partnoasal2,part2,Partno2,Partnoasal4,part4,Partno4       " +
            "order by PartnoAsal,Partno1 desc,Partno2 desc,Partno4 desc  " +
            " " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L2  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L3  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L4   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L5 " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc) AS Row,* into tempListPlankR3L2 from tempListPlankR3L1  " +
            "where I99_SA+RuningSaw_SA+Bevel_SA+Straping_SA+i99_in+i99_out+Runingsaw_in+Runingsaw_out+Bevel_in+Bevel_out+straping_in+straping_out>0   " +
            " " +
            "select i99_partno,i99_SA,i99_in,i99_out,i99_SA+i99_Saldo i99_Saldo,qi,qo, " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_SA+runingsaw_Saldo runingsaw_Saldo,qi1,qo1, " +
            "Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_SA+Bevel_Saldo Bevel_Saldo,qi2,qo2, " +
            "Partnoasal4,part4,Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_SA+Straping_Saldo Straping_Saldo,qi4,qo4 " +
            "into tempListPlankR3L3 from (     " +
            "select i99_partno,i99_SA, i99_in,i99_out,(i99_in-i99_out)i99_Saldo,qi,qo, " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,(runingsaw_in-runingsaw_out+qi1-qo1)runingsaw_Saldo,qi1,qo1, " +
            "Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,(Bevel_in-Bevel_out) Bevel_Saldo,qi2,qo2, " +
            "Partnoasal4,part4,Straping_partno,Straping_SA,Straping_in,Straping_out,(Straping_in-Straping_out)Straping_Saldo,qi4,qo4 from (     " +
            "select  i99_partno,     " +
            "case when (select count(i99_partno) from tempListPlankR3L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,     " +
            "case when (select count(i99_partno) from tempListPlankR3L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,     " +
            "case when (select count(i99_partno) from tempListPlankR3L2 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out, " +
            "case when (select count(i99_partno) from tempListPlankR3L2 where i99_partno=A.i99_partno and row < A.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi, " +
            "case when (select count(i99_partno) from tempListPlankR3L2 where i99_partno=A.i99_partno and row < A.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo, " +
            "Partnoasal1,part1,runingsaw_partno,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L2 where i99_partno=A.i99_partno and    " +
            "runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,     " +
            "case when(select count(runingsaw_partno) from tempListPlankR3L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,     " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row )>0 then '0' else runingsaw_out end runingsaw_out, " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row and isnull(Qi1,0)>0)=0 then isnull(qi1,0) else 0 end qi1, " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L2 where partnoasal1=A.partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "row < A.row and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1, " +
            "  " +
            "A.Partnoasal2,A.part2,Bevel_partno,     " +
            "case when (select count(Bevel_partno) from tempListPlankR3L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,     " +
            "case when (select count(Bevel_partno) from tempListPlankR3L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row )>0 then '0' else Bevel_in end Bevel_in,     " +
            "case when (select count(Bevel_partno) from tempListPlankR3L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row )>0 then '0' else Bevel_out end Bevel_out, " +
            "case when (select count(Bevel_partno) from tempListPlankR3L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row and isnull(Qi2,0)>0)=0 then isnull(qi2,0) else 0 end qi2, " +
            "case when (select count(Bevel_partno) from tempListPlankR3L2 where partnoasal2=A.partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "row < A.row and isnull(Qo2,0)>0)=0 then isnull(qo2,0) else 0 end qo2,   " +
            " " +
            " A.Partnoasal4,A.part4,Straping_partno,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row )>0 then '0' else Straping_SA end Straping_SA,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row )>0 then '0' else Straping_in end Straping_in,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row )>0 then '0' else Straping_out end Straping_out,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row)=0 then isnull(qi4,0) else 0 end qi4,   " +
            "case when (select count(Straping_partno) from tempListPlankR3L2 where partnoasal4=A.partnoasal4 and Straping_partno=A.Straping_partno and    " +
            "row < A.row)=0 then isnull(qo4,0) else 0 end qo4   " +
            " " +
            "from tempListPlankR3L2 A) B) C where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or    " +
            "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0 or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0  " +
            " " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc) AS Row,*  " +
            "into tempListPlankR3L4  " +
            "from  tempListPlankR3L3 TL left join (  " +
            "select partnoasal5,part5,partno5,SUM(qtyin) Qty,sfrom from (  " +
            "select  " +
            "case when ISNULL(sfrom,'')='strapingR3' then (select partno from FC_Items where ID In (select ItemID0 from T1_LR3_Straping where ID=A.T1SerahID ))  " +
            "when ISNULL(sfrom,'')='bevelR3' then (select PartNo from FC_Items where ID In (select ItemID0 from T1_LR3_bevel where ID=A.T1SerahID )) end PartnoAsal5,  " +
            "case when ISNULL(sfrom,'')='strapingR3' then (select partno from FC_Items where ID In (select ItemID  from T1_LR3_Straping where ID= A.T1SerahID))  " +
            "when ISNULL(sfrom,'')='bevelR3' then (select  PartNo  from FC_Items where ID In (select ItemID from T1_LR3_bevel where ID=A.T1SerahID )) end Part5,  " +
            "I.PartNo PartNo5,  " +
            "qtyin,sfrom from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID where A.rowstatus>-1 and (ISNULL(sfrom,'')='bevelR3' or   " +
            "ISNULL(sfrom,'')='strapingR3') and LEFT(convert(char,tgltrans,112),6)= @thbln )T group by partnoasal5,part5,partno5,sfrom " +
            ")T3  " +
            "on  T3.Part5 = case when sfrom ='bevelR3' then  TL.Part2 when rtrim(sfrom) ='StrapingR3' then TL.part4 end and  " +
            "T3.PartnoAsal5=case when sfrom ='bevelR3' then  TL.Partnoasal2 when sfrom ='StrapingR3' then TL.partnoasal4 end  " +
            "order by i99_partno,Partnoasal1,part1,RUningsaw_Partno,Partnoasal2,part2,Bevel_Partno,Partnoasal4,part4,Straping_Partno  " +
            "   " +
            "select ROW_NUMBER() OVER (ORDER BY i99_partno,i99_SA desc,i99_in desc,i99_out desc)as row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,qi,qo, " +
            "Partnoasal1,part1,runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out, " +
            "runingsaw_Saldo,qi1,qo1,Partnoasal2,part2,Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2,Partnoasal4,part4, " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4, " +
            "case when partnoasal5=partnoasal4 or partnoasal5=partnoasal2 then partnoasal5 else '-'end partnoasal5,part5,partno5,Qty " +
            "into tempListPlankR3L5  " +
            "from tempListPlankR3L4 " +
            "  " +
            "select row,i99_partno,i99_SA,i99_in,i99_out,i99_Saldo,qi,qo, " +
            "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,qi1,qo1, " +
            "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2, " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4, " +
            "T3_partno,T3_Qty  " +
            "from (     " +
            "select row,i99_partno,i99_SA, i99_in,i99_out,i99_Saldo,qi,qo, " +
            "runingsaw_partno,runingsaw_SA,runingsaw_in,runingsaw_out,runingsaw_Saldo,qi1,qo1, " +
            "Bevel_partno,Bevel_SA,Bevel_in,Bevel_out,Bevel_Saldo,qi2,qo2, " +
            "Straping_partno,Straping_SA,Straping_in,Straping_out,Straping_Saldo,qi4,qo4, " +
            "T3_partno,case when isnull(t3_partno,'-')='-' then 0 else T3_Qty  end  T3_Qty   " +
            "from (     " +
            "select row,case when (select count(i99_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and row < A.row )>0 then '-' else i99_partno end i99_partno,   " +
            "case when (select count(i99_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_SA end i99_SA,     " +
            "case when (select count(i99_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_in end i99_in,     " +
            "case when (select count(i99_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_out end i99_out,    " +
            "case when (select count(i99_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and row < A.row )>0 then 0 else i99_saldo end i99_saldo,   " +
            "case when (select count(i99_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and row < A.row and isnull(Qi,0)>0)=0 then isnull(qi,0) else 0 end qi, " +
            "case when (select count(i99_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and row < A.row and isnull(Qo,0)>0)=0 then isnull(qo,0) else 0 end qo, " +
            "  " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L5 where Partnoasal1 =A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '-' else runingsaw_partno end runingsaw_partno,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L5 where Partnoasal1=A.Partnoasal1 and    " +
            "part1=A.part1 and runingsaw_partno=A.runingsaw_partno and row < A.row )>0 then '0' else runingsaw_SA end runingsaw_SA,     " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_in end runingsaw_in,     " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_out end runingsaw_out,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '0' else runingsaw_saldo end runingsaw_saldo,   " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row and isnull(Qi1,0)>0)=0 then isnull(Qi1,0) else 0 end Qi1, " +
            "case when (select count(runingsaw_partno) from tempListPlankR3L5 where Partnoasal1=A.Partnoasal1 and runingsaw_partno=A.runingsaw_partno and    " +
            "part1=A.part1 and row < A.row  and isnull(Qo1,0)>0)=0 then isnull(qo1,0) else 0 end qo1, " +
            " " +
            "A.Partnoasal2,A.part2,   " +
            "case when (select count(Bevel_partno) from tempListPlankR3L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and   " +
            "row < A.row )>0 then '-' else Bevel_partno end Bevel_partno,     " +
            "case when (select count(Bevel_partno) from tempListPlankR3L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "part1=A.part1 and row < A.row )>0 then '0' else Bevel_SA end Bevel_SA,     " +
            "case when (select count(Bevel_partno) from tempListPlankR3L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_in end Bevel_in,     " +
            "case when (select count(Bevel_partno) from tempListPlankR3L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_out end Bevel_out,  " +
            "case when  (select count(Bevel_partno) from tempListPlankR3L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row )>0 then '0' else Bevel_saldo end Bevel_saldo,    " +
            "case when (select count(Bevel_partno) from tempListPlankR3L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row and isnull(Qi2,0)>0)=0 then isnull(Qi2,0) else 0 end Qi2, " +
            "case when (select count(Bevel_partno) from tempListPlankR3L5 where Partnoasal2=A.Partnoasal2 and Bevel_partno=A.Bevel_partno and    " +
            "partnoasal2=A.partnoasal2 and part2=A.part2 and row < A.row and isnull(Qo2,0)>0)=0 then isnull(Qo2,0) else 0 end Qo2, " +
            " " +
            "case when (select count(Straping_partno) from tempListPlankR3L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '-' else Straping_partno end Straping_partno,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_SA end Straping_SA,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_in end Straping_in,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_out end Straping_out,     " +
            "case when (select count(Straping_partno) from tempListPlankR3L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row )>0 then '0' else Straping_saldo end Straping_saldo, " +
            "case when (select count(Straping_partno) from tempListPlankR3L5 where Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row and isnull(Qi4,0)>0)=0 then isnull(Qi4,0) else 0 end Qi4, " +
            "case when (select count(Straping_partno) from tempListPlankR3L5 where i99_partno=A.i99_partno and Straping_partno=A.Straping_partno and    " +
            "partnoasal4=A.partnoasal4 and part4=A.part4 and row < A.row and isnull(Qo4,0)>0)=0 then isnull(Qo4,0) else 0 end Qo4, " +
            "  " +
            "case when (select count(partno5) from tempListPlankR3L5 where i99_partno=A.i99_partno and partno5=A.partno5 and    " +
            "partnoasal5=A.partnoasal5 and part5=A.part5 and row < A.row )>0 then '-' else partno5 end T3_partno,   " +
            "   " +
            "qty  T3_Qty     " +
            "from tempListPlankR3L5 A) B) C   " +
            "where i99_SA<>0 or i99_in<>0 or i99_out<>0 or i99_Saldo<>0 or runingsaw_SA<>0 or runingsaw_in<>0 or    " +
            "runingsaw_out<>0 or runingsaw_Saldo<>0 or  Bevel_SA<>0 or Bevel_in<>0 or Bevel_out<>0 or Bevel_Saldo<>0  " +
            "or Straping_SA<>0 or Straping_in<>0 or Straping_out<>0 or Straping_Saldo<>0   " +
            "or T3_Qty<>0  " +
            "or T3_Qty<>0  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L1]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L1   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L2]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L2   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L3]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L3   " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L4]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L4 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListPlankR3L5]') AND type in (N'U')) DROP TABLE [dbo].tempListPlankR3L5 ";
            return strsql;
        }

        public string ViewNCHandling(string thnbln)
        {
            string strsql = "declare @thnbln varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "SELECT B.tgltrans as Tanggal, I1.PartNo as PartnoAwal, sum(B.QtyIn) as LembarAwal,sum(I1.Volume*B.QtyIn) KubikAwal,  " +
                "I2.PartNo AS PartnoAkhir, sum(B.QtyOut) as LembarAkhir,sum(I2.Volume*B.QtyOut) KubikAkhir  FROM T3_Serah AS A  " +
                "INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  " +
                "FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID  " +
                "where B.NCH>0 and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln  " +
                "group by B.tgltrans,I1.PartNo,I2.PartNo order by B.tgltrans,I1.PartNo";
            return strsql;
        }

        public string ViewNCSortir(string thnbln)
        {
            string strsql = "declare @thnbln varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "select Tanggal,PartnoAwal,sum(QtyAwal)QtyAwal,sum(M3Awal)M3Awal,PartnoAkhir,sum(QtyAkhirSS)QtyAkhirSS,sum(M3AkhirSS)M3AkhirSS, " +
                "sum(QtyAkhirSE)QtyAkhirSE,sum(M3AkhirSE)M3AkhirSE from ( " +
                "SELECT B.tgltrans as Tanggal, I1.PartNo as PartnoAwal, sum(B.QtyIn) as QtyAwal,sum(I1.Volume*B.QtyIn) M3Awal,  " +
                "I2.PartNo AS PartnoAkhir, case when B.NCSS>0 then sum(B.QtyOut) else 0 end QtyAkhirSS, " +
                "case when B.NCSS>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSS,case when B.NCSE>0 then sum(B.QtyOut) else 0 end QtyAkhirSE, " +
                "case when B.NCSE>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSE,B.NCSS,B.NCSE  FROM T3_Serah AS A  " +
                "INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  " +
                "FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID  " +
                "where (B.NCSS>0 or B.NCSE>0) and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln  " +
                "group by B.tgltrans,I1.PartNo,I2.PartNo,B.NCSS,B.NCSE) S  group by Tanggal,PartnoAwal,PartnoAkhir " +
                "order by Tanggal,PartnoAwal";
            return strsql;
        }

        public string ViewNCHandlingBln(string thnbln)
        {
            string strsql = "declare @thnbln varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "SELECT I2.PartNo AS PartnoAkhir, sum(B.QtyOut) as LembarAkhir,sum(I2.Volume*B.QtyOut) KubikAkhir  FROM " +
                "T3_Simetris AS B inner join FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                "where B.NCH>0 and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln " +
                "group by I2.PartNo order by I2.PartNo";
            return strsql;
        }

        public string ViewNCSortirBln(string thnbln)
        {
            string strsql = "declare @thnbln varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "select PartnoAkhir,sum(QtyAkhirSS)QtyAkhirSS,sum(M3AkhirSS)M3AkhirSS, " +
                "sum(QtyAkhirSE)QtyAkhirSE,sum(M3AkhirSE)M3AkhirSE from ( " +
                "SELECT B.tgltrans as Tanggal, I1.PartNo as PartnoAwal, sum(B.QtyIn) as QtyAwal,sum(I1.Volume*B.QtyIn) M3Awal,  " +
                "I2.PartNo AS PartnoAkhir, case when B.NCSS>0 then sum(B.QtyOut) else 0 end QtyAkhirSS, " +
                "case when B.NCSS>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSS,case when B.NCSE>0 then sum(B.QtyOut) else 0 end QtyAkhirSE, " +
                "case when B.NCSE>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSE,B.NCSS,B.NCSE  FROM T3_Serah AS A  " +
                "INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  " +
                "FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID  " +
                "where (B.NCSS>0 or B.NCSE>0) and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln  " +
                "group by B.tgltrans,I1.PartNo,I2.PartNo,B.NCSS,B.NCSE) S  group by PartnoAkhir " +
                "order by PartnoAkhir";
            return strsql;
        }

        public string ViewPareto(string thnbln)
        {
            string strsql = "declare @tgl1 varchar(max) " +
                "declare @tgl2 varchar(max) " +
                "set @tgl1 ='" + thnbln + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefectPrtL]') AND type in (N'U')) DROP TABLE [dbo].[tempdefectPrtL] " +
                "select * into tempdefectPrtL from (  " +
                "select case when D.DeptID=0 then case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end else D.DeptID end deptID1,  " +
                "A.CreatedTime as tglInput,A.Tgl as TglPeriksa,E.TglProduksi as TglProd,  P.NoPAlet, D.DefCode,BP.PlantName as Line, PG.[Group],BF.FormulaCode as Jenis,   " +
                "D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.DestID,B.Qty QtyIn,B.Qty*I.Volume as Kubik,A.tPotong as TotPotong  " +
                "from Def_DefectDetail B inner join Def_Defect A on A.Id=B.DefectID  " +
                "left join Def_MasterDefect D on B.MasterID=D.ID left join BM_Destacking E on E.ID=A.DestID left join BM_Palet P on E.PaletID=P.ID  " +
                "left join BM_PlantGroup PG on A.GroupProdID=PG.ID left join BM_Formula BF on BF.ID=E.FormulaID  left join BM_Plant BP on BP.ID=E.PlantID  " +
                "left join FC_Items I on I.ID=E.ItemID  where B.RowStatus>-1 ) as Def  " +
                "where left(CONVERT(varchar, TglPeriksa,112),6)=@tgl1 " +
                "select '1' line, D.DefName,C.kubik,C.Persen  from Def_MasterDefect D  " +
                "left join (select *,(kubik/jumlah)*100 Persen from ( " +
                "select *,(select SUM(kubik) from tempdefectPrtL where line= A.line and deptid1=2)jumlah from ( " +
                "select Line,DefName,sum(kubik)kubik from tempdefectPrtL where deptid1=2 and line like '%1%' group by Line,defname )A)B)C on D.DefName=C.defname   " +
                "where D.DefName in (select DefName from tempdefectPrtL where deptid1=2)   " +
                "union all " +
                "select '2' line,D.DefName,C.kubik,C.Persen  from Def_MasterDefect D  " +
                "left join (select *,(kubik/jumlah)*100 Persen from ( " +
                "select *,(select SUM(kubik) from tempdefectPrtL where line= A.line and deptid1=2)jumlah from ( " +
                "select Line,DefName,sum(kubik)kubik from tempdefectPrtL where deptid1=2 and line like '%2%' group by Line,defname )A)B)C on D.DefName=C.defname   " +
                "where D.DefName in (select DefName from tempdefectPrtL where deptid1=2) " +
                "union all " +
                "select '3' line,D.DefName,C.kubik,C.Persen  from Def_MasterDefect D  " +
                "left join (select *,(kubik/jumlah)*100 Persen from ( " +
                "select *,(select SUM(kubik) from tempdefectPrtL where line= A.line and deptid1=2)jumlah from ( " +
                "select Line,DefName,sum(kubik)kubik from tempdefectPrtL where deptid1=2 and line like '%3%' group by Line,defname )A)B)C on D.DefName=C.defname   " +
                "where D.DefName in (select DefName from tempdefectPrtL where deptid1=2) " +
                "union all " +
                "select '4' line,D.DefName,C.kubik,C.Persen  from Def_MasterDefect D  " +
                "left join (select *,(kubik/jumlah)*100 Persen from ( " +
                "select *,(select SUM(kubik) from tempdefectPrtL where line= A.line and deptid1=2)jumlah from ( " +
                "select Line,DefName,sum(kubik)kubik from tempdefectPrtL where deptid1=2 and line like '%4%' group by Line,defname )A)B)C on D.DefName=C.defname   " +
                "where D.DefName in (select DefName from tempdefectPrtL where deptid1=2) " +
                "union all " +
                "select '5' line,D.DefName,C.kubik,C.Persen  from Def_MasterDefect D  " +
                "left join (select *,(kubik/jumlah)*100 Persen from ( " +
                "select *,(select SUM(kubik) from tempdefectPrtL where line= A.line and deptid1=2)jumlah from ( " +
                "select Line,DefName,sum(kubik)kubik from tempdefectPrtL where deptid1=2 and line like '%5%' group by Line,defname )A)B)C on D.DefName=C.defname   " +
                "where D.DefName in (select DefName from tempdefectPrtL where deptid1=2)  " +
                "union all " +
                "select '6' line,D.DefName,C.kubik,C.Persen  from Def_MasterDefect D  " +
                "left join (select *,(kubik/jumlah)*100 Persen from ( " +
                "select *,(select SUM(kubik) from tempdefectPrtL where line= A.line and deptid1=2)jumlah from ( " +
                "select Line,DefName,sum(kubik)kubik from tempdefectPrtL where deptid1=2 and line like '%6%' group by Line,defname )A)B)C on D.DefName=C.defname   " +
                "where D.DefName in (select DefName from tempdefectPrtL where deptid1=2) order by Line,kubik desc,defname";
            return strsql;
        }

        //public string ViewLReadyStock(string Periode1, string Periode2,int plantID)
        //{
        //    string unitkerja = string.Empty ;
        //    if (plantID == 7)
        //        unitkerja = "[sqlctrp.grcboard.com]";
        //    else
        //        unitkerja = "[sqlkrwg.grcboard.com]";
        //    string strsql = "declare @tgl1 char(8)  " +
        //         "declare @tgl2 char(8)   " +
        //         "declare @thbln char(6)  " +
        //         "set @tgl1='" + Periode1 + "'   " +
        //         "set @tgl2 ='" + Periode2 + "'  " +
        //         "set @tgl1=left(@tgl2,6)+'01'  " +
        //         "set @thbln=left(@tgl1,6) " +
        //         "declare  @thnbln0   varchar(6)  " +
        //         "declare @tgl datetime  " +
        //         "set @tgl=CAST( (@thbln+ '01') as datetime) " +
        //         "set @tgl= DATEADD(month,-1,@tgl)   " +
        //         "set  @thnbln0  =LEFT(convert(char,@tgl,112),6)   " +
        //         "declare @thnAwal  varchar(4)  " +
        //         "declare @blnAwal varchar(2)  " +
        //         "declare @AwalQty varchar(7)  " +
        //         "declare @AwalAvgPrice varchar(11)  " +
        //         "declare @tglawal varchar(10)  " +
        //         "set @tglawal='01-' + right(@thbln,2)+'-'+ LEFT(@thbln,4)   " +
        //         "set  @thnAwal =left( @thnbln0  ,4)  " +
        //         "set @blnAwal=RIGHT( @thnbln0  ,2)  " +
        //         "if right(@blnAwal,2)='01' begin set @AwalQty='janqty' set @AwalAvgPrice='janAvgprice'  end  " +
        //         "if right(@blnAwal,2)='02' begin set @AwalQty='febqty' set @AwalAvgPrice='febAvgprice'  end  " +
        //         "if right(@blnAwal,2)='03' begin set @AwalQty='marqty' set @AwalAvgPrice='marAvgprice'  end  " +
        //         "if right(@blnAwal,2)='04' begin set @AwalQty='aprqty' set @AwalAvgPrice='aprAvgprice'  end  " +
        //         "if right(@blnAwal,2)='05' begin set @AwalQty='meiqty' set @AwalAvgPrice='meiAvgprice'  end  " +
        //         "if right(@blnAwal,2)='06' begin set @AwalQty='junqty' set @AwalAvgPrice='junAvgprice'  end  " +
        //         "if right(@blnAwal,2)='07' begin set @AwalQty='julqty' set @AwalAvgPrice='julAvgprice'  end  " +
        //         "if right(@blnAwal,2)='08' begin set @AwalQty='aguqty' set @AwalAvgPrice='aguAvgprice'  end  " +
        //         "if right(@blnAwal,2)='09' begin set @AwalQty='sepqty' set @AwalAvgPrice='sepAvgprice'  end  " +
        //         "if right(@blnAwal,2)='10' begin set @AwalQty='oktqty' set @AwalAvgPrice='oktAvgprice'  end  " +
        //         "if right(@blnAwal,2)='11' begin set @AwalQty='novqty' set @AwalAvgPrice='novAvgprice'  end  " +
        //         "if right(@blnAwal,2)='12' begin set @AwalQty='desqty' set @AwalAvgPrice='desAvgprice'  end  " +
        //         "declare @sqlP nvarchar(max)  " +
        //         "declare @sqlP1 nvarchar(max)  " +
        //         "declare @sqlP2 nvarchar(max) " +
        //         " set @sqlP1='  " +
        //         " SELECT ukuran,grup,KW,Tebal,lebar ,Panjang,sum(Qty) qty1,sum(qty1 )qty2,sum(qtywip )qty3,sum(qtywip1 )qty4, " +
        //         "sum(Qty*((Tebal*lebar*Panjang)/1000000000)) qty1M3,sum(qty1*((Tebal*lebar*Panjang)/1000000000))qty2M3,sum(qtywip*((Tebal*lebar*Panjang)/1000000000) )qty3M3, " +
        //         "sum(qtywip1 *((Tebal*lebar*Panjang)/1000000000))qty4M3, " +
        //         "sum(QtyK )qty1K,sum(qty1K )qty2K,sum(qtywipK )qty3K,sum(qtywip1K )qty4K FROM (  " +
        //         "select isnull((select kelompok from t3_readystockstd where rowstatus>-1 and partno=saldo1.partno),''X'') grup,  " +
        //         "rtrim(cast(CAST(Tebal as decimal(18,0)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   " +
        //         "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi as ukuran, *,((panjang * lebar)/(1220*2440))*Qty QtyK,  " +
        //         "   ((panjang * lebar)/(1220*2440))*Qty1 Qty1K,((panjang * lebar)/(1220*2440))*QtyWIP QtyWIPK,  " +
        //         "   ((panjang * lebar)/(1220*2440))*QtyWIP1 QtyWIP1K, qty*volume as Kubikasi from (  " +
        //      "   select Tebal,lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume,KW,Jenis,sisi,Partno,   " +
        //      "   SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY,   " +
        //      "   SUM(awal1) as awal1,SUM(terima1)  as terima1,SUM(keluar1)  as keluar1 , SUM(awal1)+SUM(terima1)- SUM(keluar1) as QTY1,   " +
        //      "   SUM(awalWIP) as awalWIP,SUM(terimaWIP)  as terimaWIP, SUM(keluarWIP)  as keluarWIP ,SUM(awalWIP)+SUM(terimaWIP)- SUM(keluarWIP) as QTYWIP,   " +
        //      "   SUM(awalWIP1) as awalWIP1,SUM(terimaWIP1)  as terimaWIP1, SUM(keluarWIP1)  as keluarWIP1 ,SUM(awalWIP1)+SUM(terimaWIP1)- SUM(keluarWIP1) as QTYWIP1 from (  " +
        //      "   select Tebal,case when Lebar>1220 then 1220 when Lebar=1220 and Panjang=2420 then 1200 else lebar end lebar,  " +
        //      "   case when Panjang=2460 then 2440 when Lebar=1220 and Panjang=2420 then 2400 else Panjang end Panjang,  " +
        //      "   (Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume,KW,CASE when Jenis=''TBP'' then ''TBP'' else ''INT'' end jenis,case when Sisi=''B1'' then ''BV'' else sisi end sisi,Partno,   " +
        //      "   SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY,   " +
        //      "   SUM(awal1) as awal1,SUM(terima1)  as terima1,SUM(keluar1)  as keluar1 , SUM(awal1)+SUM(terima1)- SUM(keluar1) as QTY1,   " +
        //      "   SUM(awalWIP) as awalWIP,SUM(terimaWIP)  as terimaWIP, SUM(keluarWIP)  as keluarWIP ,SUM(awalWIP)+SUM(terimaWIP)- SUM(keluarWIP) as QTYWIP,  " +
        //      "   SUM(awalWIP1) as awalWIP1,SUM(terimaWIP1)  as terimaWIP1, SUM(keluarWIP1)  as keluarWIP1 ,SUM(awalWIP1)+SUM(terimaWIP1)- SUM(keluarWIP1) as QTYWIP1 from (  " +
        //         "  select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW, " +
        //         "  case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else SUBSTRING(B.partno,18,2) end sisi,B.Partno,0 as awal,qty  as terima, 0 as keluar ' " +
        //         "  set @sqlP2 =' " +
        //  "       ,0 as awal1,0  as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
        //  "       from vw_KartuStockBJnew A, FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'')   " +
        //  "        and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'   " +
        //  "       union all  " +
        //  "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
        //  "       SUBSTRING(B.partno,18,2) end sisi,B.Partno,0 as awal,0 as terima,qty*-1 as keluar  " +
        //  "       ,0 as awal1,0  as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1   " +
        //  "       from vw_KartuStockBJnew A, FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'') and A.qty<0 and   " +
        //  "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
        //  "       union all  " +
        //  "       /*hitung saldo awal*/  " +
        //  "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
        //  "       SUBSTRING(B.partno,18,2) end sisi,B.Partno,'+ @AwalQty +'  as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1 from  (  " +
        //  "       SELECT DISTINCT ItemID, YearPeriod, ItemTypeID,  JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty,   " +
        //         "       AguQty,SepQty, OktQty, NovQty,DesQty  FROM [SaldoInventoryBJ])as  A, FC_Items B   " +
        //  "       where A.YearPeriod='+@thnAwal+' and A.ItemID =B.ID and (B.PartNo  like ''%-3-%'' OR B.PartNo like ''%-W-%'')  " +
        //  "       union all  " +
        //  "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE''   " +
        //  "       else SUBSTRING(B.partno,18,2) end sisi,B.Partno,0 as awal,0  as terima, 0 as keluar ,0 as awal1,qty  as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
        //        //"       from vw_KartuStockBJnew_other A, vw_FC_Items_Other B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'') and  " +
        //  "       from "+ unitkerja + ".bpasctrp.dbo.vw_KartuStockBJnew A, "+ unitkerja + ".bpasctrp.dbo.FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'') and  " +
        //  "       A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'   " +
        //  "       union all  " +
        //         "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
        //  "       SUBSTRING(B.partno,18,2) end sisi,B.Partno,0 as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, qty*-1 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
        //        //"       from vw_KartuStockBJnew_other A, vw_FC_Items_Other B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'') and A.qty<0 and   " +
        //  "       from "+ unitkerja + ".bpasctrp.dbo.vw_KartuStockBJnew A, "+ unitkerja + ".bpasctrp.dbo.FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'') and A.qty<0 and   " +
        //  "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
        //  "       union all  " +
        //  "       /*hitung saldo awal*/  " +
        //  "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
        //  "       SUBSTRING(B.partno,18,2) end sisi ,B.Partno,0 as awal,0 as terima,0 as keluar ,'+ @AwalQty +'  as awal1,0  as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1 from  (  " +
        //  "       SELECT DISTINCT ItemID, YearPeriod, ItemTypeID,JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty,   " +
        //        //"       aguqty , SepQty, OktQty, NovQty,DesQty  FROM [vw_SaldoInventoryBJ_Other])as  A, vw_FC_Items_Other B   " +
        //  "       aguqty , SepQty, OktQty, NovQty,DesQty  FROM "+ unitkerja + ".bpasctrp.dbo.SaldoInventoryBJ)as  A, "+ unitkerja + ".bpasctrp.dbo.FC_Items B   " +
        //  "       where A.YearPeriod='+@thnAwal +' and A.ItemID =B.ID and (B.PartNo  like ''%-3-%'' OR B.PartNo like ''%-W-%'')  " +
        //  "       union all   " +
        //         "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,0 as awal,0  as terima, 0 as keluar , " +
        //  "       0 as awal1,0  as terima1, 0 as keluar1,0 as awalWIP,qty as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
        //  "       from vw_KartustockWIP A, FC_Items B where A.itemid0 =B.ID and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and   " +
        //  "       convert(varchar,tanggal,112)<='+@tgl2 +'   " +
        //  "       union all  " +
        //         "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,0 as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,qty*-1 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1   " +
        //  "       from vw_KartustockWIP A, FC_Items B where A.ItemID0 =B.ID and A.qty<0 and   " +
        //  "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
        //  "       union all  " +
        //  "       /*hitung saldo awal*/ " +
        //         "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
        //  "       0 as awal,0 as terima,0 as keluar,0 as awal1,0 as terima1, 0 as keluar1,  " +
        //  "       saldo as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1   " +
        //  "       from  T1_SaldoPerLokasi A, FC_Items B   " +
        //  "       where A.thnbln ='+ @thnbln0 +'  and A.ItemID =B.ID and A.LokID not in (select ID from FC_Lokasi where (Lokasi=''p99'' or Lokasi=''i99''))  " +
        //  "       union All  " +
        //         "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3'' KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
        //  "       0 as awal,0  as terima, 0 as keluar ,0 as awal1,0  as terima1, 0 as keluar1,0 as awalWIP, " +
        //  "       0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,qty as terimaWIP1,0 as keluarWIP1  " +
        //        //"       from vw_KartustockWIP_Other A, vw_FC_Items_Other B where A.itemid0 =B.ID and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and   " +
        //  "       from "+ unitkerja + ".bpasctrp.dbo.vw_KartustockWIP A, "+ unitkerja + ".bpasctrp.dbo.FC_Items B where A.itemid0 =B.ID and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and   " +
        //  "       convert(varchar,tanggal,112)<='+@tgl2 +'   " +
        //  "       union all  " +
        //         "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''K,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
        //  "       0 as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,qty*-1 as keluarWIP1   " +
        //        //"       from vw_KartustockWIP_Other A, vw_FC_Items_Other B where A.ItemID0 =B.ID and A.qty<0 and   " +
        //  "       from "+ unitkerja + ".bpasctrp.dbo.vw_KartustockWIP A, "+ unitkerja + ".bpasctrp.dbo.FC_Items B where A.ItemID0 =B.ID and A.qty<0 and   " +
        //  "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
        //  "       union all  " +
        //  "       /*hitung saldo awal*/  " +
        //         "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3'' KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
        //  "       0 as awal,0 as terima,0 as keluar,0 as awal1,0 as terima1, 0 as keluar1,  " +
        //  "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,saldo as awalWIP1,0 as terimaWIP1,0 as keluarWIP1    " +
        //        //"       from  T1_SaldoPerLokasi_Other A, vw_FC_Items_Other B   " +
        //        "       from  "+ unitkerja + ".bpasctrp.dbo.T1_SaldoPerLokasi A, "+ unitkerja + ".bpasctrp.dbo.FC_Items B   " +
        //        //"       where A.thnbln ='+ @thnbln0 +'  and A.ItemID =B.ID and A.LokID not in (select ID from vw_FC_Lokasi_Other where (Lokasi=''p99'' or Lokasi=''i99''))  " +
        //  "       where A.thnbln ='+ @thnbln0 +'  and A.ItemID =B.ID and A.LokID not in (select ID from "+ unitkerja + ".bpasctrp.dbo.FC_Lokasi where (Lokasi=''p99'' or Lokasi=''i99''))  " +
        //      "   ) as saldo  group by KW,sisi,Partno,Jenis ,Tebal,Lebar,Panjang  " +
        //      "   ) as saldoc  group by KW,sisi,Partno,Jenis ,Tebal,Lebar,Panjang  " +
        //         ") as saldo1 where QTY<>0 or QTY1<>0 or  QTYWIP<>0 or  QTYWIP1>0  " +
        //         ")allq group by grup,ukuran,KW,Tebal,lebar ,Panjang order by grup,ukuran,KW,Tebal,lebar ,Panjang ' " +
        //         "set @sqlP=@sqlP1+@sqlP2 " +
        //        "exec sp_executesql @sqlP, N''";
        //    return strsql;
        //}
        public string ViewLReadyStock(string Periode1, string Periode2, int plantID)
        {
            string unitkerja = string.Empty;
            if (plantID == 1)
                //unitkerja = ""+ unitkerja + "";
                unitkerja = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            else
                unitkerja = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
            string strsql = "declare @tgl1 char(8)  " +
                 "declare @tgl2 char(8)   " +
                 "declare @thbln char(6)  " +
                 "set @tgl1='" + Periode1 + "'   " +
                 "set @tgl2 ='" + Periode2 + "'  " +
                 "set @tgl1=left(@tgl2,6)+'01'  " +
                 "set @thbln=left(@tgl1,6) " +
                 "declare  @thnbln0   varchar(6)  " +
                 "declare @tgl datetime  " +
                 "set @tgl=CAST( (@thbln+ '01') as datetime) " +
                 "set @tgl= DATEADD(month,-1,@tgl)   " +
                 "set  @thnbln0  =LEFT(convert(char,@tgl,112),6)   " +
                 "declare @thnAwal  varchar(4)  " +
                 "declare @blnAwal varchar(2)  " +
                 "declare @AwalQty varchar(7)  " +
                 "declare @AwalAvgPrice varchar(11)  " +
                 "declare @tglawal varchar(10)  " +
                 "set @tglawal='01-' + right(@thbln,2)+'-'+ LEFT(@thbln,4)   " +
                 "set  @thnAwal =left( @thnbln0  ,4)  " +
                 "set @blnAwal=RIGHT( @thnbln0  ,2)  " +
                 "if right(@blnAwal,2)='01' begin set @AwalQty='janqty' set @AwalAvgPrice='janAvgprice'  end  " +
                 "if right(@blnAwal,2)='02' begin set @AwalQty='febqty' set @AwalAvgPrice='febAvgprice'  end  " +
                 "if right(@blnAwal,2)='03' begin set @AwalQty='marqty' set @AwalAvgPrice='marAvgprice'  end  " +
                 "if right(@blnAwal,2)='04' begin set @AwalQty='aprqty' set @AwalAvgPrice='aprAvgprice'  end  " +
                 "if right(@blnAwal,2)='05' begin set @AwalQty='meiqty' set @AwalAvgPrice='meiAvgprice'  end  " +
                 "if right(@blnAwal,2)='06' begin set @AwalQty='junqty' set @AwalAvgPrice='junAvgprice'  end  " +
                 "if right(@blnAwal,2)='07' begin set @AwalQty='julqty' set @AwalAvgPrice='julAvgprice'  end  " +
                 "if right(@blnAwal,2)='08' begin set @AwalQty='aguqty' set @AwalAvgPrice='aguAvgprice'  end  " +
                 "if right(@blnAwal,2)='09' begin set @AwalQty='sepqty' set @AwalAvgPrice='sepAvgprice'  end  " +
                 "if right(@blnAwal,2)='10' begin set @AwalQty='oktqty' set @AwalAvgPrice='oktAvgprice'  end  " +
                 "if right(@blnAwal,2)='11' begin set @AwalQty='novqty' set @AwalAvgPrice='novAvgprice'  end  " +
                 "if right(@blnAwal,2)='12' begin set @AwalQty='desqty' set @AwalAvgPrice='desAvgprice'  end  " +
                 "declare @sqlP nvarchar(max)  " +
                 "declare @sqlP1 nvarchar(max)  " +
                 "declare @sqlP2 nvarchar(max) " +
                 " set @sqlP1='  " +
                 " SELECT ukuran,grup,KW,Tebal,lebar ,Panjang,sum(Qty) qty1,sum(qty1 )qty2,sum(qtywip )qty3,sum(qtywip1 )qty4, " +
                 "sum(Qty*((Tebal*lebar*Panjang)/1000000000)) qty1M3,sum(qty1*((Tebal*lebar*Panjang)/1000000000))qty2M3,sum(qtywip*((Tebal*lebar*Panjang)/1000000000) )qty3M3, " +
                 "sum(qtywip1 *((Tebal*lebar*Panjang)/1000000000))qty4M3, " +
                 "sum(QtyK )qty1K,sum(qty1K )qty2K,sum(qtywipK )qty3K,sum(qtywip1K )qty4K FROM (  " +
                 "select isnull((select kelompok from t3_readystockstd where rowstatus>-1 and partno=saldo1.partno),''X'') grup,  " +

                 "rtrim(cast(CAST(Tebal as decimal(18,0)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   " +
                "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi as ukuran, " +

                 //" case " +
                 //" when tebal>3 and tebal<4 then rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi " +
                 //" when tebal>4 and tebal<5 then rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi " +
                 //" when tebal>5 and tebal<6 then rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi " +
                 //" when tebal>6 and tebal<7 then rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi " +
                 //" when tebal>7 and tebal<8 then rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi " +
                 //" else rtrim(cast(CAST(Tebal as decimal(18,0)) as CHAR)) + '' mm '' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + '' X '' +   rtrim(cast(CAST(Panjang as decimal(18)) as CHAR))+'' '' +sisi end ukuran, " +

                 "*,((panjang * lebar)/(1220*2440))*Qty QtyK,  " +
                 "   ((panjang * lebar)/(1220*2440))*Qty1 Qty1K,((panjang * lebar)/(1220*2440))*QtyWIP QtyWIPK,  " +
                 "   ((panjang * lebar)/(1220*2440))*QtyWIP1 QtyWIP1K, qty*volume as Kubikasi from (  " +
              "   select Tebal,lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume,KW,Jenis,sisi,Partno,   " +
              "   SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY,   " +
              "   SUM(awal1) as awal1,SUM(terima1)  as terima1,SUM(keluar1)  as keluar1 , SUM(awal1)+SUM(terima1)- SUM(keluar1) as QTY1,   " +
              "   SUM(awalWIP) as awalWIP,SUM(terimaWIP)  as terimaWIP, SUM(keluarWIP)  as keluarWIP ,SUM(awalWIP)+SUM(terimaWIP)- SUM(keluarWIP) as QTYWIP,   " +
              "   SUM(awalWIP1) as awalWIP1,SUM(terimaWIP1)  as terimaWIP1, SUM(keluarWIP1)  as keluarWIP1 ,SUM(awalWIP1)+SUM(terimaWIP1)- SUM(keluarWIP1) as QTYWIP1 from (  " +
              "   select Tebal,case when Lebar>1220 then 1220 when Lebar=1220 and Panjang=2420 then 1200 else lebar end lebar,  " +
              "   case when Panjang=2460 then 2440 when Lebar=1220 and Panjang=2420 then 2400 else Panjang end Panjang,  " +
              "   (Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume,KW,CASE when Jenis=''TBP'' then ''TBP'' else ''INT'' end jenis,case when Sisi=''B1'' then ''BV'' else sisi end sisi,Partno,   " +
              "   SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY,   " +
              "   SUM(awal1) as awal1,SUM(terima1)  as terima1,SUM(keluar1)  as keluar1 , SUM(awal1)+SUM(terima1)- SUM(keluar1) as QTY1,   " +
              "   SUM(awalWIP) as awalWIP,SUM(terimaWIP)  as terimaWIP, SUM(keluarWIP)  as keluarWIP ,SUM(awalWIP)+SUM(terimaWIP)- SUM(keluarWIP) as QTYWIP,  " +
              "   SUM(awalWIP1) as awalWIP1,SUM(terimaWIP1)  as terimaWIP1, SUM(keluarWIP1)  as keluarWIP1 ,SUM(awalWIP1)+SUM(terimaWIP1)- SUM(keluarWIP1) as QTYWIP1 from (  " +
                 "  select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW, " +
                 "  case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else SUBSTRING(B.partno,18,6) end sisi,B.Partno,0 as awal,qty  as terima, 0 as keluar ' " +
                 "  set @sqlP2 =' " +
          "       ,0 as awal1,0  as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
          "       from vw_KartuStockBJnew A, FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'')   " +
          "        and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'   " +
          "       union all  " +
          "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
          "       SUBSTRING(B.partno,18,6) end sisi,B.Partno,0 as awal,0 as terima,qty*-1 as keluar  " +
          "       ,0 as awal1,0  as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1   " +
          "       from vw_KartuStockBJnew A, FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and A.qty<0 and   " +
          "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
          "       union all  " +
          "       /*hitung saldo awal*/  " +
          "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
          "       SUBSTRING(B.partno,18,6) end sisi,B.Partno,'+ @AwalQty +'  as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1 from  (  " +
          "       SELECT DISTINCT ItemID, YearPeriod, ItemTypeID,  JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty,   " +
                 "       AguQty,SepQty, OktQty, NovQty,DesQty  FROM [SaldoInventoryBJ])as  A, FC_Items B   " +
          "       where A.YearPeriod='+@thnAwal+' and A.ItemID =B.ID and (B.PartNo  like ''%-3-%'' OR B.PartNo like ''%-W-%'' OR B.PartNo  like ''%-m-%'')  " +
          "       union all  " +
          "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE''   " +
          "       else SUBSTRING(B.partno,18,6) end sisi,B.Partno,0 as awal,0  as terima, 0 as keluar ,0 as awal1,qty  as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
          //"       from vw_KartuStockBJnew_other A, vw_FC_Items_Other B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'') and  " +
          "       from " + unitkerja + "vw_KartuStockBJnew A, " + unitkerja + "FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and  " +
          "       A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'   " +
          "       union all  " +
                 "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
          "       SUBSTRING(B.partno,18,6) end sisi,B.Partno,0 as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, qty*-1 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
          //"       from vw_KartuStockBJnew_other A, vw_FC_Items_Other B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'') and A.qty<0 and   " +
          "       from " + unitkerja + "vw_KartuStockBJnew A, " + unitkerja + "FC_Items B where A.ItemID =B.ID and (B.PartNo like ''%-3-%'' OR B.PartNo  like ''%-W-%'' OR B.PartNo  like ''%-m-%'') and A.qty<0 and   " +
          "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
          "       union all  " +
          "       /*hitung saldo awal*/  " +
          "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,SUBSTRING(B.partno,5,1)KW,case when SUBSTRING(B.partno,18,2)=''s'' then ''SE'' else   " +
          "       SUBSTRING(B.partno,18,6) end sisi ,B.Partno,0 as awal,0 as terima,0 as keluar ,'+ @AwalQty +'  as awal1,0  as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1 from  (  " +
          "       SELECT DISTINCT ItemID, YearPeriod, ItemTypeID,JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty,   " +
          //"       aguqty , SepQty, OktQty, NovQty,DesQty  FROM [vw_SaldoInventoryBJ_Other])as  A, vw_FC_Items_Other B   " +
          "       aguqty , SepQty, OktQty, NovQty,DesQty  FROM " + unitkerja + "SaldoInventoryBJ)as  A, " + unitkerja + "FC_Items B   " +
          "       where A.YearPeriod='+@thnAwal +' and A.ItemID =B.ID and (B.PartNo  like ''%-3-%'' OR B.PartNo like ''%-W-%'' OR B.PartNo  like ''%-m-%'')  " +
          "       union all   " +
                 "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,0 as awal,0  as terima, 0 as keluar , " +
          "       0 as awal1,0  as terima1, 0 as keluar1,0 as awalWIP,qty as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1  " +
          "       from vw_KartustockWIP A, FC_Items B where A.itemid0 =B.ID and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and   " +
          "       convert(varchar,tanggal,112)<='+@tgl2 +'   " +
          "       union all  " +
                 "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,0 as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,qty*-1 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1   " +
          "       from vw_KartustockWIP A, FC_Items B where A.ItemID0 =B.ID and A.qty<0 and   " +
          "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
          "       union all  " +
          "       /*hitung saldo awal*/ " +
                 "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
          "       0 as awal,0 as terima,0 as keluar,0 as awal1,0 as terima1, 0 as keluar1,  " +
          "       saldo as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,0 as keluarWIP1   " +
          "       from  T1_SaldoPerLokasi A, FC_Items B   " +
          "       where A.thnbln ='+ @thnbln0 +'  and A.ItemID =B.ID and A.LokID not in (select ID from FC_Lokasi where (Lokasi=''p99'' or Lokasi=''i99''))  " +
          "       union All  " +
                 "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3'' KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
          "       0 as awal,0  as terima, 0 as keluar ,0 as awal1,0  as terima1, 0 as keluar1,0 as awalWIP, " +
          "       0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,qty as terimaWIP1,0 as keluarWIP1  " +
          //"       from vw_KartustockWIP_Other A, vw_FC_Items_Other B where A.itemid0 =B.ID and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and   " +
          "       from " + unitkerja + "vw_KartustockWIP A, " + unitkerja + "FC_Items B where A.itemid0 =B.ID and A.qty>0 and convert(varchar,tanggal,112)>='+@tgl1 +' and   " +
          "       convert(varchar,tanggal,112)<='+@tgl2 +'   " +
          "       union all  " +
                 "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3''K,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
          "       0 as awal,0 as terima,0 as keluar,0 as awal1,0  as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,0 as awalWIP1,0 as terimaWIP1,qty*-1 as keluarWIP1   " +
          //"       from vw_KartustockWIP_Other A, vw_FC_Items_Other B where A.ItemID0 =B.ID and A.qty<0 and   " +
          "       from " + unitkerja + "vw_KartustockWIP A, " + unitkerja + "FC_Items B where A.ItemID0 =B.ID and A.qty<0 and   " +
          "       convert(varchar,tanggal,112)>='+@tgl1 +' and convert(varchar,tanggal,112)<='+@tgl2 +'  " +
          "       union all  " +
          "       /*hitung saldo awal*/  " +
          "       select SUBSTRING(B.partno,1,3)jenis,B.Tebal,B.Lebar,B.Panjang,''3'' KW,''SE''  sisi,''INT-3-'' + substring(B.Partno,7,11) + ''SE'' Partno,  " +
          "       0 as awal,0 as terima,0 as keluar,0 as awal1,0 as terima1, 0 as keluar1,  " +
          "       0 as awalWIP,0 as terimaWIP,0 as keluarWIP,saldo as awalWIP1,0 as terimaWIP1,0 as keluarWIP1    " +
          //"       from  T1_SaldoPerLokasi_Other A, vw_FC_Items_Other B   " +
          "       from  " + unitkerja + "T1_SaldoPerLokasi A, " + unitkerja + "FC_Items B   " +
          //"       where A.thnbln ='+ @thnbln0 +'  and A.ItemID =B.ID and A.LokID not in (select ID from vw_FC_Lokasi_Other where (Lokasi=''p99'' or Lokasi=''i99''))  " +
          "       where A.thnbln ='+ @thnbln0 +'  and A.ItemID =B.ID and A.LokID not in (select ID from " + unitkerja + "FC_Lokasi where (Lokasi=''p99'' or Lokasi=''i99''))  " +
          "   ) as saldo  group by KW,sisi,Partno,Jenis ,Tebal,Lebar,Panjang  " +
          "      ) as saldoc  group by KW,sisi,Partno,Jenis ,Tebal,Lebar,Panjang  " +
                 ") as saldo1 where QTY<>0 or QTY1<>0 or  QTYWIP<>0 or  QTYWIP1>0  " +
                 ")allq group by grup,ukuran,KW,Tebal,lebar ,Panjang order by grup,ukuran,KW,Tebal,lebar ,Panjang ' " +
                 "set @sqlP=@sqlP1+@sqlP2 " +
                "exec sp_executesql @sqlP, N''";
            return strsql;
        }

        public string ViewLPemantauan4mm_Lama(string thnbln)
        {
            string strsql = "declare @HMY varchar(6)  " +
                "declare @thnbln varchar(6)  " +
                "select @HMY='" + thnbln + "'  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP4mm]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP4mm]  " +
                "select * into tempWIP4mm from (select * from vw_KartustockWIP where left(CONVERT(char, tanggal ,112),6)=@HMY union all select * from vw_KartustockWIP2 where left(CONVERT(char, tanggal ,112),6)=@HMY )A  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBJ4mm]') AND type in (N'U')) DROP TABLE [dbo].[tempBJ4mm]  " +
                "select * into tempBJ4mm from vw_KartuStockBJNew  where process like '%simetris%' and left(CONVERT(char, tanggal ,112),6)=@HMY  " +
                "and itemid in (select ID from FC_Items where Tebal=4) and keterangan like '%-P-%' " +
                "select tanggal, " +
                "(select isnull(SUM(qty),0) from BM_Destacking where qty>0 and CONVERT(varchar,TglProduksi, 112) = M1.HMY and  " +
                "ItemID in (select ID from FC_Items where Tebal=4)   " +
                " and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as ProdQty, " +
                " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and process<>'listplank' and  " +
                " ItemID0 in (select ID from FC_Items where Tebal=4) " +
                " and CONVERT(varchar,tanggal, 112) = M1.HMY and itemid not in (select ID from fc_items where partno like '%-1-%')) as TPot, " +
                " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and (lokasi='H99' or lokasi='P99' or lokasi='I99')  and process<>'listplank' " +
                " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where Tebal=4)  " +
                " and ItemID in (select ID from FC_Items where PartNo like '%-3-%')) as OKT1, " +
                " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and lokasi='C99'  and process<>'listplank' " +
                " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where  Tebal=4) ) as BPUT1, " +
                " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and (lokasi='B99'  or lokasi='P99' )  and process<>'listplank' " +
                " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where  Tebal=4 ) and ItemID in (select ID from FC_Items where  partno like '%-p-%'))  as BPFT1, " +
                " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and ((lokasi='H99' or lokasi='P99' ) and process<>'listplank') " +
                " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where Tebal=4)  " +
                " and ItemID in (select ID from FC_Items where PartNo like '%-W-%' or PartNo like '%-m-%')) as KWT1, " +
                "(select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                "ItemID in (select ID from FC_Items where PartNo like '%3%' and Lebar=1200)) as OKT3_1200, " +
                "(select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                "ItemID in (select ID from FC_Items where (PartNo like '%W%'  or PartNo like '%-m-%') and Lebar=1200)) as KWT3_1200, " +
                "(select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                "ItemID in (select ID from FC_Items where (PartNo like '%3%' or PartNo like '%W%'  or PartNo like '%-m-%') and Lebar=1000 and Panjang=1000)) as Mawar, " +
                "(select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                "ItemID in (select ID from FC_Items where PartNo like '%P%' and Lebar=1000 and Panjang=1000)) as ExMawar, " +
                "(select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                "ItemID in (select ID from FC_Items where (PartNo like '%3%' or PartNo like '%W%'  or PartNo like '%-m-%') and Lebar=600 and Panjang=1200)) as OK600x1200, " +
                "(select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                "ItemID in (select ID from FC_Items where PartNo like '%P%' and Lebar=600 and Panjang=1200)) as BP600x1200 " +
                " from ( " +
                "select distinct * from ( " +
                "select distinct tanggal, CONVERT(varchar,tanggal,112)HMY from tempBJ4mm  " +
                "union all select distinct tanggal, CONVERT(varchar,tanggal,112)HMY from tempWIP4mm  " +
                "union all select distinct tglproduksi tanggal,CONVERT(varchar,tglproduksi,112)HMY from BM_Destacking   " +
                "where LEFT(CONVERT(varchar,TglProduksi, 112), 6) =@HMY)A)M1 " +
                "order by tanggal " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP4mm]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP4mm]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBJ4mm]') AND type in (N'U')) DROP TABLE [dbo].[tempBJ4mm] ";
            return strsql;
        }

        public string ViewLPemantauan4mm(string thnbln, int UnitKerja)
        {
            string MawarOK = string.Empty;
            string MawarKW = string.Empty;
            string exMawar = string.Empty;
            string KWT3_1200 = string.Empty;
            string OKT3_1200 = string.Empty;
            string KWT1_1200 = string.Empty;
            string KWT1_4x8 = string.Empty;
            string LokasiOK = string.Empty;
            string TotPot = string.Empty;
            string BPUT1 = string.Empty;
            string BPFT1_4X8 = string.Empty;
            string BPFT1_1200 = string.Empty;
            string PBNNO_4x8 = string.Empty;

            if (UnitKerja != 0)
            {
                if (UnitKerja == 7)
                {
                    exMawar = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                              " ItemID in (select ID from FC_Items where PartNo like '%MWR-P-040%' and Lebar=1000 and Panjang=1000)) as ExMawar, ";

                    MawarKW = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                              " ItemID in (select ID from FC_Items where (PartNo like '%MWR-W-040%' or PartNo like '%MWR-M-040%') and Lebar=1000 and Panjang=1000)) as Mawar_KW,";

                    MawarOK = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  " +
                              " ItemID in (select ID from FC_Items where (PartNo like '%MWR-3-040%') and Lebar=1000 and Panjang=1000)) as Mawar,";

                    KWT3_1200 = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  ItemID in " +
                                " (select ID from FC_Items where (PartNo like '%-W-%' or PartNo like '%-M-%') and Lebar=1200)) as KWT3_1200, ";

                    OKT3_1200 = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  ItemID in " +
                                " (select ID from FC_Items where PartNo like '%3%' and Lebar=1200)) as OKT3_1200, ";

                    KWT1_1200 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and ((lokasi='H99' or lokasi='P99' ) and process<>'listplank') " +
                                " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where Tebal=4)  " +
                                " and ItemID in (select ID from FC_Items where (PartNo like '%-W-%' or PartNo like '%-m-%') and lebar='1200' and panjang='2400')) as KWT1_1200, ";

                    KWT1_4x8 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and ((lokasi='H99' or lokasi='P99' ) and process<>'listplank') " +
                                " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where Tebal=4)  " +
                                " and ItemID in (select ID from FC_Items where (PartNo like '%-W-%' or PartNo like '%-m-%') and lebar='1220' and panjang='2440')) as KWT1_4x8, ";

                    TotPot = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and process<>'listplank' and  " +
                             " ItemID0 in (select ID from FC_Items where Tebal=4) " +
                             " and CONVERT(varchar,tanggal, 112) = M1.HMY and itemid not in (select ID from fc_items where partno like '%-1-%')) as TPot, ";

                    BPUT1 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and lokasi='C99'  and process<>'listplank' " +
                             " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where  Tebal=4) ) as BPUT1, ";
                    BPUT1 = " 0 as BPUT1, ";

                    BPFT1_4X8 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and (lokasi='B99'  or lokasi='P99' )  and process<>'listplank' " +
                                " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where  Tebal=4 ) and ItemID in " +
                                " (select ID from FC_Items where  partno like '%-p-%' and lebar='1220' and panjang='2440'))  as BPFT1_4X8, ";

                    BPFT1_1200 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and (lokasi='B99'  or lokasi='P99' )  and process<>'listplank' " +
                                 " and CONVERT(varchar,tanggal, 112) = M1.HMY and ItemID0 in (select ID from FC_Items where  Tebal=4 ) and ItemID in " +
                                 " (select ID from FC_Items where  partno like '%-p-%' and lebar='1200' and panjang='2400'))  as BPFT1_1200, ";

                    PBNNO_4x8 = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  serahid in " +
                                " (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%INT-P-%' and Lebar=1220 " +
                                " and Panjang=2440) and RowStatus > -1) and  ItemID in  (select ID from FC_Items where (PartNo like '%PNK-W-%' " +
                                " or PartNo like'%NNO-W-%' or PartNo like '%PNK-M-%' or PartNo like '%NNO-M-%') and Lebar=1220 and Panjang=2440)) as PBNNO_4x8, ";
                }
                else
                {
                    exMawar = " (select isnull(SUM(qty),0) from [tempMutasiLok4mm] where qty>0 and CONVERT(varchar,tgltrans, 112) = M1.HMY and  ItemID in " +
                                " (select ID from FC_Items where (PartNo like '%P%') and  Tebal=4 and Lebar=1000 and Panjang=1000) and LokID1 in " +
                                " (select ID from FC_Lokasi where Lokasi='ACQA') and LokID2 in (select ID from FC_Lokasi where Lokasi='H99') ) as ExMawar, ";

                    MawarKW = " (select isnull(SUM(qty),0) from [tempMutasiLok4mm] where qty>0 and CONVERT(varchar,tgltrans, 112) = M1.HMY and  ItemID in " +
                                " (select ID from FC_Items where (PartNo like '%-W-%' or PartNo like '%-M-%') and Lebar=1000 and Panjang=1000))  as Mawar_KW,  ";

                    MawarOK = " (select isnull(SUM(qty),0) from [tempMutasiLok4mm] where qty>0 and CONVERT(varchar,tgltrans, 112) = M1.HMY and  " +
                                " ItemID in (select ID from FC_Items where (PartNo like '%3%') and Lebar=1000 and Panjang=1000))  as Mawar, ";

                    KWT3_1200 = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  ItemID in " +
                                " (select ID from FC_Items where PartNo like '%W%' and Lebar=1200)) as KWT3_1200, ";

                    OKT3_1200 = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  ItemID in " +
                                " (select ID from FC_Items where PartNo like '%3%' and Lebar=1200)) as OKT3_1200, ";

                    KWT1_1200 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and ((lokasi='H99' or lokasi='P99' or process='lari' ) " +
                                " and process<>'listplank')  and CONVERT(varchar,tanggal, 112) = M1.HMY and destid in (select ID from BM_Destacking where " +
                                " ItemID in (select ID from FC_Items where Tebal=4))   and ItemID in (select ID from FC_Items where " +
                                " (PartNo NOT like '%PNK-W-%' and PartNo NOT like '%NNO-W-%' and PartNo NOT like '%PNK-M-%' and PartNo NOT like '%NNO-M-%') " +
                                "and Lebar=1200 and Panjang=2400 and (PartNo like'%-W-%' or PartNo like'%-M-%'))) as KWT1_1200, ";

                    KWT1_4x8 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and ((lokasi='H99' or lokasi='P99' or process='lari' ) " +
                                " and process<>'listplank')  and CONVERT(varchar,tanggal, 112) = M1.HMY and destid in (select ID from BM_Destacking where " +
                                " ItemID in (select ID from FC_Items where Tebal=4))   and ItemID in (select ID from FC_Items where (PartNo NOT like '%PNK-W-%' " +
                                " and PartNo NOT like '%NNO-W-%' and PartNo NOT like '%PNK-M-%' and PartNo NOT like '%NNO-M-%') " +
                                "and Lebar=1220 and Panjang=2440 and (PartNo like'%-W-%' or PartNo like'%-M-%'))) as KWT1_4x8, ";

                    //TotPot =    " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and lokasi<>'P99'  and (process<>'listplank') and   " +
                    //            " destid in (select ID from BM_Destacking where ItemID in (select ID from FC_Items where Tebal=4)) and " +
                    //            " CONVERT(varchar,tanggal, 112) = M1.HMY and itemid in (select ID from fc_items where Tebal=4)) as TPot, ";

                    TotPot = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and lokasi<>'P99'  and process<>'listplank' and  " +
                                //" ItemID0 in (select ID from FC_Items where Tebal=4) " +
                                " destid in (select ID from BM_Destacking where ItemID in (select ID from FC_Items where Tebal=4)) and " +
                                " CONVERT(varchar,tanggal, 112) = M1.HMY and itemid not in (select ID from fc_items where partno like '%-1-%') and itemid in (select ID from fc_items where Tebal=4)) as TPot, ";

                    BPUT1 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and (lokasi='C99' or process='lari')  and process<>'listplank'  " +
                                " and CONVERT(varchar,tanggal, 112) = M1.HMY and destid in (select ID from BM_Destacking where ItemID in (select ID from FC_Items " +
                                " where Tebal=4))  and ItemID in (select ID from FC_Items where  partno like '%-p-%' and Lebar=1240 and Tebal=4)  ) as BPUT1, ";

                    BPFT1_4X8 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and (lokasi='B99'  or lokasi='P99' or process='lari' )  " +
                                " and process<>'listplank'  and CONVERT(varchar,tanggal, 112) = M1.HMY and  destid in (select ID from BM_Destacking where " +
                                " ItemID in (select ID from FC_Items where Tebal=4)) and ItemID in (select ID from FC_Items where  partno like '%-p-%' and " +
                                " (Lebar=1220 and panjang=2440 and Tebal=4)))  as BPFT1_4X8, ";

                    BPFT1_1200 = " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and (lokasi='B99'  or lokasi='P99' or process='lari' )  " +
                                 " and process<>'listplank'  and CONVERT(varchar,tanggal, 112) = M1.HMY and  destid in (select ID from BM_Destacking where " +
                                 " ItemID in (select ID from FC_Items where Tebal=4)) and ItemID in (select ID from FC_Items where  partno like '%-p-%' and " +
                                 " (Lebar=1200 and panjang=2400 and Tebal=4)))  as BPFT1_1200, ";

                    PBNNO_4x8 = " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  serahid in " +
                                " (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like'%INT-P-%' and Lebar=1220 " +
                                " and Panjang=2440) and RowStatus > -1) and  ItemID in  (select ID from FC_Items where (PartNo like '%PNK-W-%' " +
                                " or PartNo like'%NNO-W-%' or PartNo like'%PNK-M-%'or PartNo like'%NNO-M-%') and Lebar=1220 and Panjang=2440)) as PBNNO_4x8, ";
                }
            }

            string strsql =
            " declare @HMY varchar(6) " +
            " declare @thnbln varchar(6) " +
            " select @HMY='" + thnbln + "'" +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP4mm]') AND type in (N'U')) " +
            " DROP TABLE [dbo].[tempWIP4mm] " +

            " select * into tempWIP4mm from (select * from vw_KartustockWIP where left(CONVERT(char, tanggal ,112),6)=@HMY " +
            " union all " +
            " select * from vw_KartustockWIP2 where left(CONVERT(char, tanggal ,112),6)=@HMY )A  " +

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBJ4mm]') AND type in (N'U')) " +
            " DROP TABLE [dbo].[tempBJ4mm] " +

            " select * into tempBJ4mm from vw_KartuStockBJNew  where process like '%simetris%' and left(CONVERT(char, tanggal ,112),6)=@HMY  and itemid in (select ID from FC_Items where Tebal=4) and keterangan like '%-P-%' " +

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempMutasiLok4mm]') AND type in (N'U')) " +
            " DROP TABLE [dbo].[tempMutasiLok4mm] " +

            " select * into tempMutasiLok4mm from T3_MutasiLok where left(CONVERT(char, TglTrans ,112),6)=@HMY and itemid in (select ID from FC_Items where Tebal=4 and Lebar=1000 and Panjang=1000 and RowStatus > -1 and LokID1 in (select ID from FC_Lokasi where Lokasi='ACQA' and LokID2 in (select ID from FC_Lokasi where Lokasi='H99'))) " +
            " select distinct cast(convert(char,tanggal,112) as datetime) tanggal,(select isnull(SUM(qty),0) from BM_Destacking where qty>0 and CONVERT(varchar,TglProduksi, 112) = M1.HMY and  ItemID in (select ID from FC_Items where Tebal=4) and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as ProdQty, " +

            //Total Potong
            "" + TotPot + "" +
            //" (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0 and lokasi<>'P99'  and (process<>'listplank') and   "+
            //" destid in (select ID from BM_Destacking where ItemID in (select ID from FC_Items where Tebal=4)) and "+
            //" CONVERT(varchar,tanggal, 112) = M1.HMY and itemid in (select ID from fc_items where Tebal=4)) as TPot, " +

            //OK Tahap 1
            " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and (lokasi='H99' or lokasi='P99' or lokasi='I99' or process='lari') " +
            " and process<>'listplank'  and CONVERT(varchar,tanggal, 112) = M1.HMY and destid in (select ID from BM_Destacking where ItemID in " +
            " (select ID from FC_Items where Tebal=4))  and ItemID in (select ID from FC_Items where PartNo like '%-3-%' and Tebal=4)) as 'OKT1', " +

            //OK Tahap 1 | Partno 3 : Lebar 1220 Panjang 2440
            " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and (lokasi='H99' or lokasi='P99' or lokasi='I99' or process='lari') " +
            " and process<>'listplank'  and CONVERT(varchar,tanggal, 112) = M1.HMY and destid in (select ID from BM_Destacking where ItemID in " +
            " (select ID from FC_Items where Tebal=4))  and ItemID in (select ID from FC_Items where PartNo like '%-3-%' and Tebal=4 and Lebar=1220)) " +
            " as 'OKT1_4x8', " +

            //OK Tahap 1 | Partno 3 : Lebar 1200 Panjang 2400
            " (select isnull(SUM(qty)*-1,0) from tempWIP4mm where qty<0  and (lokasi='H99' or lokasi='P99' or lokasi='I99' or process='lari') " +
            " and process<>'listplank'  and CONVERT(varchar,tanggal, 112) = M1.HMY and destid in (select ID from BM_Destacking where ItemID in " +
            " (select ID from FC_Items where Tebal=4))  and ItemID in (select ID from FC_Items where PartNo like '%-3-%' and Tebal=4 and Lebar=1200)) " +
            " as 'OKT1_1200', " +

            //BP Unfinish Tahap 1 | Partno P : Lebar 1240 Panjang 2460
            " " + BPUT1 + " " +

            //BP Finish Tahap 1 | Partno P : Lebar 1220 Panjang 2440
            " " + BPFT1_4X8 + " " +

            //BP Finish Tahap 1 | Partno P : Lebar 1200 Panjang 2400
            " " + BPFT1_1200 + " " +

            //KW Tahap 1 | Partno W : Lebar 1220 Panjang 2440
            "" + KWT1_4x8 + "" +

            //KW Tahap 1 | Partno W : Lebar 1200 Panjang 2400
            "" + KWT1_1200 + "" +

            //PinkBoard Nano Tahap 3 | Partno W : Lebar 1220 Panjang 2440
            "" + PBNNO_4x8 + "" +

            //OK Tahap 3 | Partno 3 : Lebar 1200 Panjang 2400
            "" + OKT3_1200 + "" +

            //KW Tahap 3 | Partno W : Lebar 1200 Panjang 2400
            "" + KWT3_1200 + "" +

            //Mawar OK | Partno 3 : Lebar 1200 Panjang 2400 : VS KRWG Match
            "" + MawarKW + "" +

            //Mawar KW | Partno W : Lebar 1000 Panjang 1000
            "" + MawarOK + "" +

            //exMawar | Partno P : Lebar 1000 Panjang 1000 : VS KRWG Match
            "" + exMawar + "" +

            //Tidak dipakai
            " (select isnull(SUM(qty),0) from [tempMutasiLok4mm] where qty>0 and CONVERT(varchar,tgltrans, 112) = M1.HMY and  " +
            " ItemID in (select ID from FC_Items where Tebal=4 and Lebar=1000 and Panjang=1000) and LokID1 in (select ID from FC_Lokasi " +
            " where Lokasi='ACQA') and LokID2 in (select ID from FC_Lokasi where Lokasi='H99') ) as ExMawar2, " +

            //Tidak dipakai
            " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  ItemID in " +
            " (select ID from FC_Items where (PartNo like '%3%' or PartNo like '%W%') and Lebar=600 and Panjang=1200)) as OK600x1200, " +

            //Tidak dipakai
            " (select isnull(SUM(qty),0) from tempBJ4mm where qty>0 and CONVERT(varchar,tanggal, 112) = M1.HMY and  ItemID in " +
            " (select ID from FC_Items where PartNo like '%P%' and Lebar=600 and Panjang=1200)) as BP600x1200 " +

            " from ( select distinct * from ( select distinct tanggal, CONVERT(varchar,tanggal,112)HMY from tempBJ4mm " +
            " union all " +
            " select distinct tanggal, CONVERT(varchar,tanggal,112)HMY from tempWIP4mm " +
            " union all " +

            " select distinct tglproduksi tanggal,CONVERT(varchar,tglproduksi,112)HMY from BM_Destacking  " +
            " where LEFT(CONVERT(varchar,TglProduksi, 112), 6) =@HMY)A)M1 order by tanggal " +

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP4mm]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP4mm] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBJ4mm]') AND type in (N'U')) DROP TABLE [dbo].[tempBJ4mm] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempMutasiLok4mm]') AND type in (N'U')) DROP TABLE [dbo].[tempMutasiLok4mm] ";
            return strsql;
        }

        public string ViewLRejectH(string thnbln)
        {
            string strsql = "declare @tgl1 varchar(6) " +
                "set @tgl1='" + thnbln + "' " +
                "select * from (  " +
                "select A.TglTrans,I.PartNo,sum(A.QtyOut) qtyL,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.QtyOut) qtyM3L,(I.Lebar*I.Panjang)/(1220*2440) * sum(A.QtyOut) QtyKonvL,  " +
                "0 qtyF,0 qtyM3F,0 QtyKonvF,0 qtySA,0 qtyM3SA,0 QtyKonvSA,0 qtyA,0 qtyM3A,0 QtyKonvA from T3_Simetris A inner join FC_Items I on A.ItemID=I.ID   " +
                "where A.SerahID not in(select ID from T3_Serah where ItemID In (select ID from fc_items where PartNo like '%S-%')) and A.rowstatus>-1 and left(CONVERT(char,tgltrans,112),6)=@tgl1  and ISNULL(bs,'')='log' and A.SerahID not in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like '%-s-%')) 5group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang  " +
                "UNION ALL  " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,sum(A.Qty) qtyF,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qty) qtyM3F,  " +
                "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.Qty) QtyKonvF,0 qtySA,0 qtyM3SA,0 QtyKonvSA,0 qtyA,0 qtyM3A,0 QtyKonvA from T3_retur A inner join FC_Items I on A.ItemID=I.ID   " +
                "where A.rowstatus>-1 and I.partno like '%-s-%' and left(CONVERT(char,tgltrans,112),6)=@tgl1  group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang  " +
                "UNION ALL  " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,0 qtyF,0 qtyM3F,  " +
                "0 QtyKonvF,sum(A.Qtyin) qtySA,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) qtyM3SA,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) QtyKonvSA,0 qtyA,0 qtyM3A,0 QtyKonvA  " +
                "from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID   " +
                "where  A.rowstatus>-1 and I.partno like '%-s-04004001000%' and A.Qtyin>0 and left(CONVERT(char,tgltrans,112),6)=@tgl1  group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang  " +
                "UNION ALL  " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,0 qtyF,0 qtyM3F,0 QtyKonvF,0 qtySA,0 qtyM3SA,0 QtyKonvSA,sum(A.Qtyin) qtyA,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) qtyM3A,  " +
                "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.Qtyin) QtyKonvA  " +
                "from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID where A.rowstatus>-1 and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') and  " +
                "left(CONVERT(char,tgltrans,112),6)=@tgl1  group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang " +
                ") A";
            return strsql;
        }

        public string ViewLRejectB(string thnbln)
        {
            string strsql = "declare @tgl1 varchar(6) " +
                "set @tgl1='" + thnbln + "' " +
                "select PartNo,SUM(qtyL)qtyL,SUM(qtyM3L)qtyM3L,SUM(QtyKonvL)QtyKonvL,SUM(qtyF)qtyF,SUM(qtyM3F)qtyM3F,SUM(QtyKonvF)QtyKonvF,SUM(qtySA)qtySA,SUM(qtyM3SA)qtyM3SA,SUM(QtyKonvSA)QtyKonvSA ,SUM(qtyA)qtyA,SUM(qtyM3A)qtyM3A,SUM(QtyKonvA)QtyKonvA from ( " +
                "select A.TglTrans,I.PartNo,sum(A.QtyOut) qtyL,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.QtyOut) qtyM3L,(I.Lebar*I.Panjang)/(1220*2440) * sum(A.QtyOut) QtyKonvL,  " +
                "0 qtyF,0 qtyM3F,0 QtyKonvF,0 qtySA,0 qtyM3SA,0 QtyKonvSA,0 qtyA,0 qtyM3A,0 QtyKonvA from T3_Simetris A inner join FC_Items I on A.ItemID=I.ID   " +
                "where A.SerahID not in(select ID from T3_Serah where ItemID In (select ID from fc_items where PartNo like '%-S-%')) and  A.rowstatus>-1 and left(CONVERT(char,tgltrans,112),6)=@tgl1  and ISNULL(bs,'')='log' and A.SerahID not in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like '%-s-%')) group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang  " +
                "UNION ALL  " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,sum(A.Qty) qtyF,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qty) qtyM3F,  " +
                "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.Qty) QtyKonvF,0 qtySA,0 qtyM3SA,0 QtyKonvSA,0 qtyA,0 qtyM3A,0 QtyKonvA from T3_retur A inner join FC_Items I on A.ItemID=I.ID   " +
                "where A.rowstatus>-1 and I.partno like '%-s-%' and left(CONVERT(char,tgltrans,112),6)=@tgl1  group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang  " +
                "UNION ALL  " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,0 qtyF,0 qtyM3F,  " +
                "0 QtyKonvF,sum(A.Qtyin) qtySA,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) qtyM3SA,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) QtyKonvSA,0 qtyA,0 qtyM3A,0 QtyKonvA  " +
                "from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID   " +
                "where A.rowstatus>-1 and I.partno like '%-s-04004001000%' and A.Qtyin>0 and left(CONVERT(char,tgltrans,112),6)=@tgl1  group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang  " +
                "UNION ALL  " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,0 qtyF,0 qtyM3F,0 QtyKonvF,0 qtySA,0 qtyM3SA,0 QtyKonvSA,sum(A.Qtyin) qtyA,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) qtyM3A,  " +
                "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.Qtyin) QtyKonvA  " +
                "from T3_Rekap A inner join FC_Items I on A.ItemID=I.ID where A.rowstatus>-1 and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') and  " +
                "left(CONVERT(char,tgltrans,112),6)=@tgl1 and (process ='simetris' or process ='direct') and A.keterangan not like '%-s-%' group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang) A GROUP BY PartNo " +
                "order by partno";
            return strsql;
        }

        public string ViewLRejectFH(string thnbln)
        {
            string strsql = "declare @tgl1 varchar(6) " +
                "set @tgl1='" + thnbln + "' " +
                "select * from ( " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,sum(A.QtyOut) qtyF,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.QtyOut) qtyM3F, " +
                "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.QtyOut) QtyKonvF,0 qtyA,0 qtyM3A,0 QtyKonvA from T3_Simetris A inner join FC_Items I on A.ItemID=I.ID  " +
                "where A.rowstatus>-1 and left(CONVERT(char,tgltrans,112),6)=@tgl1  and ISNULL(bs,'')='fin' and A.SerahID not in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like '%-s-%')) group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang " +
                "union all " +
            "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,sum(A.Qtyin) qtyF,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) qtyM3F,  " +
            "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.Qtyin) QtyKonvF,0 qtyA,0 qtyM3A,0 QtyKonvA " +
            "from T3_Rekap  A inner join FC_Items I on A.ItemID=I.ID " +
                "where A.rowstatus>-1 and left(CONVERT(char,tgltrans,112),6)=@tgl1  and ISNULL(sfrom,'-')<>'-' and I.partno like '%-s-%' group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang " +
                ") A order by tgltrans,partno";
            return strsql;
        }

        public string ViewLRejectFB(string thnbln)
        {
            string strsql = "declare @tgl1 varchar(6) " +
                "set @tgl1='" + thnbln + "' " +
                "select PartNo,SUM(qtyL)qtyL,SUM(qtyM3L)qtyM3L,SUM(QtyKonvL)QtyKonvL,SUM(qtyF)qtyF,SUM(qtyM3F)qtyM3F,SUM(QtyKonvF)QtyKonvF,SUM(qtyA)qtyA,SUM(qtyM3A)qtyM3A,SUM(QtyKonvA)QtyKonvA from ( " +
                "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,sum(A.QtyOut) qtyF,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.QtyOut) qtyM3F, " +
                "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.QtyOut) QtyKonvF,0 qtyA,0 qtyM3A,0 QtyKonvA from T3_Simetris A inner join FC_Items I on A.ItemID=I.ID  " +
                "where A.rowstatus>-1 and left(CONVERT(char,tgltrans,112),6)=@tgl1  and ISNULL(bs,'')='fin' and A.SerahID not in (select ID from T3_Serah where ItemID in (select ID from FC_Items where PartNo like '%-s-%')) group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang " +
                "union all " +
            "select A.TglTrans,I.PartNo,0 qtyL,0 qtyM3L,0 QtyKonvL,sum(A.Qtyin) qtyF,(I.Tebal*I.Lebar*I.Panjang)/1000000000 * sum(A.Qtyin) qtyM3F,  " +
            "(I.Lebar*I.Panjang)/(1220*2440) * sum(A.Qtyin) QtyKonvF,0 qtyA,0 qtyM3A,0 QtyKonvA  " +
            "from T3_Rekap  A inner join FC_Items I on A.ItemID=I.ID   " +
                "where A.rowstatus>-1 and left(CONVERT(char,tgltrans,112),6)=@tgl1  and ISNULL(sfrom,'-')<>'-' and I.partno like '%-s-%' group by A.TglTrans,I.PartNo,I.Tebal,I.Lebar,I.Panjang " +
                ") A GROUP BY PartNo " +
                "order by partno";
            return strsql;
        }

        public string ViewPenurunanReject(string thnbln)
        {
            string strsql =
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBSReject]') AND type in (N'U')) DROP TABLE [dbo].[tempBSReject]  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQty]') AND type in (N'U')) DROP TABLE [dbo].tempQty " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQty1]') AND type in (N'U')) DROP TABLE [dbo].tempQty1 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempresult]') AND type in (N'U')) DROP TABLE [dbo].tempresult " +
            "declare @periode char(6) " +
            "declare @AwalT3 char(6) " +
            "declare @tahunAwalT3 char(6) " +
            "declare @sqlWIP nvarchar(max) " +
            "set @periode='" + thnbln + "' " +
            "declare @periode0 varchar(6) " +
            "declare @tgl datetime " +
            "set @tgl=CAST( (@periode+ '01') as datetime) " +
            "set @tgl= DATEADD(month,-1,@tgl) " +
            "set @periode0=LEFT(convert(char,@tgl,112),6) " +
            "if right(@periode0,2)='01' begin set @AwalT3='janqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='02' begin set @AwalT3='febqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='03' begin set @AwalT3='marqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='04' begin set @AwalT3='aprqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='05' begin set @AwalT3='meiqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='06' begin set @AwalT3='junqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='07' begin set @AwalT3='julqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='08' begin set @AwalT3='aguqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='09' begin set @AwalT3='sepqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='10' begin set @AwalT3='oktqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='11' begin set @AwalT3='novqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "if right(@periode0,2)='12' begin set @AwalT3='desqty' set @tahunAwalT3=left(@periode0,4) end  " +
            "set @sqlWIP=' select sum(qty) qty into tempQty from (select  isnull(sum(' + @AwalT3 + ' *((I.Tebal*I.Panjang*I.Lebar)/1000000000)),0) qty  " +
            "from SaldoInventoryBJ A  inner join fc_items I on A.itemid=I.id where  A.rowstatus>-1 and A.ItemID=I.ID and I.partno like ''%-1-%'' and YearPeriod='+ @tahunAwalT3 + " +
            "'union all " +
            "select  isnull(sum(qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)),0) qty from vw_kartustockbjnew A  inner join fc_items I on A.itemid=I.id  " +
            "where A.ItemID=I.ID and I.partno like ''%-1-%'' and left(convert(char,tanggal,112),6)=' +@periode + ')A' " +
            "exec sp_executesql @sqlWIP, N'' " +
            "set @sqlWIP=' select sum(qty) qty into tempQty1 from (select  isnull(sum(' + @AwalT3 + ' *((I.Tebal*I.Panjang*I.Lebar)/1000000000)),0) qty  " +
            "from SaldoInventoryBJ A  inner join fc_items I on A.itemid=I.id where  A.rowstatus>-1 and A.ItemID=I.ID and I.partno like ''%-P-%'' and dept=''finishing'' and YearPeriod='+ @tahunAwalT3 + " +
            "'union all " +
            "select  isnull(sum(qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)),0) qty from vw_kartustockbjnew A  inner join fc_items I on A.itemid=I.id  " +
            "where A.ItemID=I.ID and I.partno like ''%-P-%'' and dept=''finishing'' and left(convert(char,tanggal,112),6)=' +@periode + ')A' " +
            "exec sp_executesql @sqlWIP, N'' " +
            " " +
            "select distinct ItemID into tempitembsauto from vw_KartuStockBJNew where ItemID in  " +
            "(select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto')  " +
            " " +
            "select * into tempBSReject from vw_kartustockbjnew where left(convert(char,tanggal,112),6)=@periode " +
            "select * into tempresult from (select     " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where (Process='direct' and isnull(sfrom,'') not like'%R1%' and isnull(sfrom,'') not like'%R2%' and isnull(sfrom,'') not like'%R3%' and PartNo like '%-s-%' and     " +
            "SUBSTRING(partno,18,2) not in('sl','sr','dl','dr','cl','br','cr') and LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)) ),0) OT1N,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where (Process like '%simetris%' and (Keterangan like '%-p-%' or Keterangan like '%-1-%') and PartNo like '%-s-%' and     " +
            "SUBSTRING(partno,18,2) not in('sl','sr','dl','dr','cl','br','cr') and LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)) or (  " +
            "left(convert(char,tanggal,112),6)=@periode and Process='direct' and (isnull(sfrom,'') not like'%R1%' or isnull(sfrom,'') not like'%R2%' or isnull(sfrom,'') not like'%R3%') and PartNo like '%-s-%' and     " +
            "SUBSTRING(partno,18,2) not in('sl','sr','dl','dr','cl','br','cr') and LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto))),0) OBPN,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where qty>0 and Process like '%simetris%' and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') and PartNo like '%-s-%' and     " +
            "SUBSTRING(partno,18,2) not in('sl','sr','dl','dr','cl','br','cr') and LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)),0) OBJN,    " +
            "    " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process='direct' and PartNo like '%-s-%' and     " +
            "SUBSTRING(partno,18,2) not in('sl','sr','dl','dr','cl','br','cr') and LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) OT1A,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process like '%simetris%' and (Keterangan like '%-p-%' or Keterangan like '%-1-%') and PartNo like '%-s-%' and     " +
            "SUBSTRING(partno,18,2) not in('sl','sr','dl','dr','cl','br','cr') and LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) OBPA,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process like '%simetris%' and  " +
            "(Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') and PartNo like '%-s-%' and     " +
            "SUBSTRING(partno,18,2) not in('sl','sr','dl','dr','cl','br','cr') and LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) OBJA,    " +
            "    " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process='direct'  and isnull(sfrom,'') not like'%R1%' and isnull(sfrom,'') not like'%R2%' and isnull(sfrom,'') not like'%R3%' and PartNo like '%-s-%' and     " +
            "LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)),0) KT1N,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where (Process like '%simetris%' and (Keterangan like '%-p-%' or Keterangan like '%-1-%') and PartNo like '%-s-%'  and rtrim(PartNo) not like '%r' and     " +
            "LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)) or (left(convert(char,tanggal,112),6)=@periode and Process='direct'    " +
            "and (isnull(sfrom,'') not like'%R1%' or isnull(sfrom,'') not like'%R2%' or isnull(sfrom,'') not like'%R3%') and PartNo like '%-s-%'and     " +
            "LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto))),0) KBPN,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where qty>0 and Process like '%simetris%' and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') and PartNo like '%-s-%' and     " +
            "LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)),0) KBJN,    " +
            "    " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process='direct' and isnull(sfrom,'') not like'%R1%' and isnull(sfrom,'') not like'%R2%' and isnull(sfrom,'') not like'%R3%' and PartNo like '%-s-%' and     " +
            "LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) KT1A,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where  Process like '%simetris%' and (Keterangan like '%-p-%' or Keterangan like '%-1-%') and PartNo like '%-s-%' and rtrim(PartNo) not like '%r' and     " +
            "LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) KBPA,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where  Process like '%simetris%' and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') and PartNo like '%-s-%'     " +
            "and  LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) KBJA,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process like '%simetris%'  and PartNo like '%-s-%' and rtrim(PartNo) like '%r'  " +
            "and LokID not in (select ID from fc_lokasi where lokasi='bsauto')),0) KBRetur,    " +
            " " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where  Process like '%simetris%'  and PartNo like '%-s-%' and rtrim(PartNo) like '%r'  " +
            "and LokID in (select ID from fc_lokasi where lokasi='bsauto')),0) KBRA,   " +
            " " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where (Process like '%simetris%' and (Keterangan like '%-p-%' or Keterangan like '%-1-%') and PartNo like '%-s-%' and     " +
            "rtrim(partno)  like'%l' and LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)) or ( Process='direct' and (sfrom not like'%R1%' or sfrom not like'%R2%' or sfrom not like'%R3%') and PartNo like '%-s-%' and     " +
            "rtrim(partno)  like'%l'  and LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto))),0) SLBPN,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where qty>0 and Process like '%simetris%' and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') and PartNo like '%-s-%' and     " +
            "rtrim(partno)  like'%l'  and LokID not in (select ID from fc_lokasi where lokasi='bsauto')   " +
            "and ItemID not in (select ItemID from tempitembsauto)),0) SLBJN,    " +
            " " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process like '%simetris%' and (Keterangan like '%-p-%' or Keterangan like '%-1-%') and PartNo like '%-s-%' and     " +
            "rtrim(partno)  like'%l'  and LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) SLBPA,    " +
            "  " +
            "isnull((select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1)) from tempBSReject     " +
            "where Process like '%simetris%' and  " +
            "(Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') and PartNo like '%-s-%' and     " +
            "rtrim(partno)  like'%l' and LokID  in (select ID from fc_lokasi where lokasi='bsauto')),0) SLBJA,  " +
            " " +
            "isnull((select sum(qty)*-1 from (    " +
            "select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1))qty  from vw_KartustockWIP A    " +
            "inner join fc_items B on A.itemid0=B.ID    " +
            "where left(convert(char,tanggal,112),6)=@periode and qty<0 and process<>'lari' and process<>'listplank' and lokasi in ('h99','i99','b99','c99','q99')    " +
            "union all    " +
            "select CAST( sum(qty*((tebal*panjang*lebar)/1000000000)) as decimal(18,1))qty  from vw_KartustockWIP2 A    " +
            "inner join fc_items B on A.itemid0=B.ID    " +
            "where left(convert(char,tanggal,112),6)=@periode and qty<0 and process='lari' )V    " +
            "),0) T1Serah, " +
            "isnull((select sum(qty) qty from ( " +
            "select sum(A.saldo *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from T1_SaldoListPlank A inner join fc_items I on A.itemid=I.id  " +
            "where A.rowstatus>-1 and process='i99' and thnbln=@periode0 " +
            "union all " +
            "select sum(A.qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from vw_KartuStockListplank A inner join fc_items I on A.itemid=I.id  " +
            "where process='i99' and left(convert(char,tanggal,112),6)=@periode " +
            "union all " +
            "select sum(A.saldo *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from T1_SaldoPerLokasi A inner join fc_items I on A.itemid=I.id   " +
            "where  A.rowstatus>-1 and thnbln=@periode0  " +
            "union all " +
            "select sum(A.qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from vw_KartuStockWIP A inner join fc_items I on A.itemid0=I.id  " +
            "where left(convert(char,tanggal,112),6)=@periode " +
            "union all " +
            "select sum(A.qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from vw_KartuStockWIP2 A inner join fc_items I on A.itemid0=I.id  " +
            "where left(convert(char,tanggal,112),6)=@periode " +
            "union all " +
            "select qty from tempQty)A " +
            "),0) SPT1, " +
            "isnull((select sum(qty) qty from ( " +
            "select sum(A.saldo *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from T1_SaldoListPlankR1 A inner join fc_items I on A.itemid=I.id  " +
            "where A.rowstatus>-1 and process='i99' and thnbln=@periode0 " +
            "union all " +
            "select sum(A.qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from vw_KartuStockListplankR1 A inner join fc_items I on A.itemid=I.id  " +
            "where process='i99' and left(convert(char,tanggal,112),6)=@periode " +
            "union all " +
            "select sum(A.saldo *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from T1_SaldoListPlankR2 A inner join fc_items I on A.itemid=I.id  " +
            "where A.rowstatus>-1 and process='i99' and thnbln=@periode0 " +
            "union all " +
            "select sum(A.qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from vw_KartuStockListplankr2 A inner join fc_items I on A.itemid=I.id  " +
            "where process='i99' and left(convert(char,tanggal,112),6)=@periode " +
            "union all " +
            "select sum(A.saldo *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from T1_SaldoListPlankR3 A inner join fc_items I on A.itemid=I.id  " +
            "where A.rowstatus>-1 and process='i99' and thnbln=@periode0 " +
            "union all " +
            "select sum(A.qty *((I.Tebal*I.Panjang*I.Lebar)/1000000000)) qty from vw_KartuStockListplankR3 A inner join fc_items I on A.itemid=I.id  " +
            "where process='i99' and left(convert(char,tanggal,112),6)=@periode " +
            "union all " +
            "select qty from tempQty1)A " +
            "),0) SPBPF)A " +
            "update SPD_TransPrs set actual=(select  cast ((KT1N+KBPN+KBJN+KBRetur+KT1A+KBPA+KBJA)/T1Serah * 100 as decimal(18,1)) from tempresult) where Approval=0 and tahun=SUBSTRING(@periode,1,4) and bulan=SUBSTRING(@periode,5,2) and SarmutPID in (  " +
            "select ID from SPD_Perusahaan where deptid=(select ID from spd_dept where dept like '%finishing%') and rowstatus>-1 and SarMutPerusahaan='Penurunan Reject Produk') " +
            "select * from tempresult " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempresult]') AND type in (N'U')) DROP TABLE [dbo].tempresult  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBSReject]') AND type in (N'U')) DROP TABLE [dbo].[tempBSReject]  " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQty]') AND type in (N'U')) DROP TABLE [dbo].tempQty " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQty1]') AND type in (N'U')) DROP TABLE [dbo].tempQty1 ";
            return strsql;
        }

        private static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static List<ReportT1T3> GetRekapPemetaanLokasi(int thnblnhari)
        {
            List<ReportT1T3> alldata = new List<ReportT1T3>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "select PartNo,Lokasi,Saldo,P99,QtyTest from ( " +
                       "select PartNo,Lokasi,SUM(qty) as Saldo, SUM(QtySisaDariP99) as P99, SUM(QtyTest) as QtyTest " +
                       "from (select PartNo,Lokasi,ID,(Qty-QtySerah-QtySisaP99) as Qty," +
                       "case when QtySisaP99>0 then QtySisaP99 else 0 end QtySisaDariP99,Qty as QtyTest " +
                       "from (select B.PartNo,A.TglProduksi,C.Lokasi,A.Qty,A.ID, " +
                       "isnull((select SUM(QtyIn) from T1_Serah as a1 where a1.Status>-1 and a1.DestID=A.ID group by DestID),0) as QtySerah," +
                       "isnull((select sum(qty) from (select SUM(QtyIn-QtyOut) as qty from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID " +
                       " union all select SUM(QtyIn-QtyOut) as qty  from T1_Serah as a3 where a3.Status>-1 and a3.DestID =A.ID " +
                       "and a3.SFrom='lari' group by  a3.DestID) as lari),0) as QtySisaP99," +
                       "isnull((select SUM(QtyIn) from T1_JemurLg as a2 where a2.Status>-1 and a2.DestID=A.ID group by DestID),0) as QtyP99 " +
                       "from BM_Destacking as A, FC_Items as B, FC_Lokasi as C " +
                       "where A.ItemID=B.ID and A.LokasiID=C.ID and Convert(varchar,A.TglProduksi,112) >= '20130601' and Convert(varchar,A.TglProduksi,112) <= '" + thnblnhari + "' " +
                       "and A.Status>-1 and A.RowStatus>-1 " +
                       ") as b1 ) as c1 group by PartNo,Lokasi) as Test where (Saldo+P99)!=0";
                    alldata = connection.Query<ReportT1T3>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3> GetRekapCuringJemur(int thnblnhari, string proses)
        {
            List<ReportT1T3> alldata = new List<ReportT1T3>();
            string strSQL;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (proses == "curing")
                    {
                        strSQL = "select  null as tgljemurlg,*,(select SUM(qtyin-qtyout) from T1_Jemur where DestID=A.destid)as sisajemur from (select destid, partno,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
                            "where convert(varchar,tglproduksi,112)>='20130601' and  convert(varchar,tglproduksi,112)<='" + thnblnhari + "' " +
                            "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid having sum(Qty)>0 ) as A  where destid not in (select destid from t1_serah_err) and isnull(tgljemur,'1/1/1900')='1/1/1900'";
                    }
                    else
                    {
                        strSQL = "select  null as tgljemurlg,*,(select SUM(qtyin-qtyout) from T1_Jemur where DestID=A.destid)as sisajemur from (select destid, partno,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
                            "where convert(varchar,tglproduksi,112)>='20130601' and  convert(varchar,tglproduksi,112)<='" + thnblnhari + "' " +
                            "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid having sum(Qty)>0 ) as A  where destid not in (select destid from t1_serah_err) and isnull(tgljemur,'1/1/1900')<>'1/1/1900'";
                    }

                    alldata = connection.Query<ReportT1T3>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3> GetRekapProduksiHarian(int thnblnhariawal, int thnblnhari, string kriteria)
        {
            List<ReportT1T3> alldata = new List<ReportT1T3>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "select partno,lokasi,isnull(awal,0) as awal ,0 as AdjustIn,0 as AdjustOut,sum(penerimaan) as penerimaan,sum(pengeluaran)as pengeluaran, " +
                        "isnull(awal,0)+sum(penerimaan)-sum(pengeluaran) as saldo from  ( " +
                        "select  thnbln,case when ItemID>0 then (select partno from FC_Items  where id=saldo2.ItemID )end Partno, " +
                        "case when ItemID>0 then (select Lokasi from FC_Lokasi where ID =saldo2.LokasiID )end Lokasi ,0 as awal, penerimaan,pengeluaran from ( " +
                        "select  thnbln, itemid, lokasiid, sum(penerimaan) as penerimaan,sum(pengeluaran) as pengeluaran from ( " +
                        "SELECT tanggal as thnbln, itemid, lokasiid, totqty AS penerimaan,0 as pengeluaran FROM ( " +
                        "SELECT TglProduksi AS tanggal, itemid, lokasiid, sum(qty) AS totqty FROM BM_Destacking where BM_Destacking.rowstatus>-1 GROUP BY TglProduksi, itemid, lokasiid) AS destacking " +
                        "UNION all " +
                        "SELECT tanggal as thnbln, itemid, lokasiid, 0 as penerimaan,sum(qty) as pengeluaran  FROM ( " +
                        "SELECT TglProduksi   AS tanggal, itemid, lokasiid, qty FROM (SELECT BM_Destacking.TglProduksi , T1_serah.DestID,  " +
                        "BM_Destacking.ItemID,BM_Destacking.lokasiID, (T1_serah.QtyIn ) AS qty  FROM T1_serah  ,BM_Destacking " +
                        "WHERE BM_Destacking.id=T1_serah.DestID and  BM_Destacking.rowstatus>-1 and  T1_serah.status>-1 ) AS jemur) AS jemur1 GROUP BY tanggal, itemid, lokasiid) as saldo1 " +
                        "group by thnbln, itemid, lokasiid) as saldo2 )as saldo3 where ISNULL(partno,'-')<>'-' and convert(varchar,thnbln,112)>='" + thnblnhariawal + "' and convert(varchar,thnbln,112)<='" + thnblnhari + "' " + kriteria + "  group by partno,lokasi,AWAL order by lokasi,Partno";

                    alldata = connection.Query<ReportT1T3>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3> GetLapLokasiTransitT1()
        {
            List<ReportT1T3> alldata = new List<ReportT1T3>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "select left(convert(char,tglserah,112),6) as periode0,right(rtrim(convert(char,tglserah,106)),8) as periode,I.PartNo,cast(CAST(I.tebal as decimal(18,1)) as varchar) + ' X ' + cast(CAST(I.Lebar as int) as varchar)+ ' X ' + " +
                        "cast(CAST(I.Panjang as int) as varchar) as ukuran, L.Lokasi,sum(A.QtyIn-A.QtyOut) as Qty from t1_serah A left join  " +
                        "FC_Items I ON A.ItemID=I.ID left join FC_Lokasi L on A.LokID =L.ID  where  A.Status >-1 and  A.QtyIn>A.QtyOut and  " +
                        "A.DestID in(select ID from BM_Destacking where TglProduksi >='12/1/2013')  and Lokasi not like 'adj%' " +
                        "group by left(convert(char,tglserah,112),6),right(rtrim(convert(char,tglserah,106)),8),I.PartNo,L.Lokasi,I.tebal,I.Lebar,I.Panjang " +
                        "union all " +
                        "select left(convert(char,TglJemur,112),6) as periode0,right(rtrim(convert(char,TglJemur,106)),8) as periode,I.PartNo, " +
                        "cast(CAST(I.tebal as decimal(18,1)) as varchar) + ' X ' + cast(CAST(I.Lebar as int) as varchar)+ ' X ' +  " +
                        "cast(CAST(I.Panjang as int) as varchar) as ukuran, L.Lokasi,sum(A.QtyIn-A.QtyOut) as Qty  " +
                        "from T1_JemurLg  A left join  FC_Items I ON A.ItemID0=I.ID left join FC_Lokasi L on A.LokID0 =L.ID   " +
                        "where  A.Status >-1 and  A.QtyIn>A.QtyOut and A.DestID in(select ID from BM_Destacking where TglProduksi >='12/1/2013')  and Lokasi not like 'adj%'  " +
                        "group by left(convert(char,TglJemur,112),6),right(rtrim(convert(char,TglJemur,106)),8),I.PartNo,L.Lokasi,I.tebal,I.Lebar,I.Panjang  " +
                        "order by L.Lokasi,I.PartNo";

                    alldata = connection.Query<ReportT1T3>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3> GetLapTransitFinishing(string tglProduksi, string tglSerah, string partno)
        {
            List<ReportT1T3> alldata = new List<ReportT1T3>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "SELECT A.ID, B.TglProduksi Tglproduksi,  A.TglSerah Tglserah, A.CreatedTime, I1.PartNo AS PartNo, L2.Lokasi AS Lokasi, I2.PartNo AS PartNo2, " +
                        "L1.Lokasi AS Lokasi2, A.QtyIn AS qty,A.CreatedBy AS [user], P.NoPAlet AS Palet,case when left(convert(char,A.tglserah,112),6)<'201904' then J.Oven else A.oven end Oven " +
                        "FROM FC_Lokasi AS L1 RIGHT OUTER JOIN T1_Serah AS A ON L1.ID = A.LokID LEFT OUTER JOIN FC_Items AS I2 ON A.itemID = I2.ID LEFT OUTER JOIN " +
                        "FC_Lokasi AS L2 RIGHT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS B INNER JOIN FC_Items AS I1 ON B.ItemID = " +
                        "I1.ID ON P.ID = B.PaletID ON L2.ID = B.LokasiID ON A.DestID = B.ID left join T1_Jemur J on J.DestID=A.DestID and J.ID=A.JemurID where  A.SFrom<>'lari' and  A.status>-1 " + tglProduksi + tglSerah + partno +
                        " ORDER BY  A.tglserah";
                    alldata = connection.Query<ReportT1T3>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3> GetLapTransitFin(int thnbln)
        {
            List<ReportT1T3> alldata = new List<ReportT1T3>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "SELECT A.ID, B.TglProduksi Tglproduksi,  A.TglSerah Tglserah, A.CreatedTime, I1.PartNo AS PartNo, L2.Lokasi AS Lokasi, I2.PartNo AS PartNo2, " +
                        "L1.Lokasi AS Lokasi2, A.QtyIn AS qty,A.CreatedBy AS [user], P.NoPAlet ,case when left(convert(char,A.tglserah,112),6)<'201904' then J.Oven else A.oven end Oven, " +
                        "(I1.Tebal * I1.Lebar * I1.Panjang)/1000000000 * A.QtyIn M3,Ln.PlantName Line,GP.[Group] " +
                        "FROM FC_Lokasi AS L1 RIGHT OUTER JOIN T1_Serah AS A ON L1.ID = A.LokID LEFT OUTER JOIN FC_Items AS I2 ON A.itemID = I2.ID LEFT OUTER JOIN  " +
                        "FC_Lokasi AS L2 RIGHT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS B INNER JOIN FC_Items AS I1 ON B.ItemID =  " +
                        "I1.ID ON P.ID = B.PaletID ON L2.ID = B.LokasiID ON A.DestID = B.ID left join T1_Jemur J on J.DestID=A.DestID and J.ID=A.JemurID  inner join BM_Plant Ln on B.PlantID =Ln.ID  " +
                        "inner join BM_PlantGroup GP on B.PlantGroupID=GP.ID " +
                        "where  A.SFrom<>'lari' and  A.status>-1 and left(convert(char,A.tglserah,112),6)='" + thnbln + "' order by Line,GP.[Group],I1.PartNo,TglSerah ";
                    alldata = connection.Query<ReportT1T3>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3> GetLapTransitFinishingPelarian(string kriteria)
        {
            List<ReportT1T3> alldata = new List<ReportT1T3>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "select ID,(select [GROUp] from BM_PlantGroup where ID in (select plantgroupID from bm_destacking where ID=lagi.destid)) [Group],DestID,Tglproduksi,Tgljemur,createdtime,PartNo,Lokasi,NoPAlet,PartNo2,Lokasi2,Qty,[user],tglinput,tglinputserah,serahID,jemurID,Tglserah, " +
                        "case when PartNo3=PartNo2 and Lokasi3=Lokasi2 then '' else  PartNo3 end PartNo3,case when PartNo3=PartNo2 and Lokasi3=Lokasi2 then '' else " +
                        "(select top 1 lokasi from ( " +
                        "select Lokasi from FC_Lokasi where ID in (select top 1 lokid from T3_Rekap where T1SerahID=lagi.serahID )union all " +
                        "select Lokasi from FC_Lokasi where  ID in (select top 1 lokid from T1_serah where ID=lagi.serahID ))L where lokasi<>'c99' order by Lokasi desc) end Lokasi3,QTY2,(qty-isnull(qty2,0)) as Sisa, " +
                        "(select top 1 oven from t1_serah where destid=lagi.DestID and oven>0 )Oven " +
                        /** Tambahan **/
                        " ,Volume*Qty M3_2,Volume*Qty2 M3_3 " +
                        /** End Tambahan **/
                        "from ( " +
                        "select lari.ID,lari.DestID,TglProduksi,TglJemur,createdtime,PartNo,Lokasi,NoPAlet,PartNo2,Lokasi2,case when " +
                        "(select COUNT(destid) from T1_Serah where JemurID=serah.JemurID and  ID<serah.serahID and [status]>-1 and itemid0=serah.itemID0 and DestID=serah.DestID)>0 then 0 else Qty end Qty," +
                        "[user],tglinput,tglinputserah,serahID,jemurID,TglSerah,PartNo3,Lokasi3,case when Lokasi2=Lokasi3 then 0 else QTY2 end  QTY2  " +
                        /** Tambahan **/
                        " ,Volume " +
                        /** End Tambahan **/
                        "from ( " +
                        "SELECT B.ID,B.DestID, A.TglProduksi,B.createdtime, B.TglJemur, I1.PartNo AS PartNo, L1.Lokasi AS Lokasi, P.NoPAlet, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2,  " +
                        "  B.Qtyin  AS Qty, B.CreatedBy AS [user], B.CreatedTime AS tglinput  " +
                        /** Tambahan **/
                        ",((I2.Tebal*I2.Lebar*I2.Panjang)/1000000000)Volume  " +
                        /** End Tambahan **/
                        "FROM FC_Lokasi AS L2 RIGHT OUTER JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 LEFT OUTER JOIN " +
                        "FC_Items AS I2 ON B.ItemID0 = I2.ID LEFT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS A ON P.ID = A.PaletID LEFT OUTER JOIN " +
                        "FC_Items AS I1 ON A.ItemID = I1.ID LEFT OUTER JOIN FC_Lokasi AS L1 ON A.LokasiID = L1.ID ON B.DestID = A.ID " +
                        "WHERE (B.Status > - 1) ) as lari " +
                        "left join  " +
                        "(SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID, I3.PartNo AS PartNo3, L3.Lokasi AS Lokasi3, C.QtyIn AS QTY2 " +
                        "FROM FC_Items AS I3 RIGHT OUTER JOIN FC_Lokasi AS L3 RIGHT OUTER JOIN T1_Serah AS C ON L3.ID = C.LokID ON I3.ID = C.ItemID " +
                        "WHERE C.Status > - 1) as serah on lari.DestID=serah.DestID and lari.ID=serah.JemurID " +
                        ") as lagi  where destid>0  " + kriteria + " ORDER BY DestID,ID,Qty desc";
                    alldata = connection.Query<ReportT1T3>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3Simetris> GetLapHarianSimetris(int thnblnhariawal, int thnblnhariakhir, int param)
        {
            string strSQL = "";
            List<ReportT1T3Simetris> alldata = new List<ReportT1T3Simetris>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (param == 1)
                    {
                        strSQL = "SELECT B.ID,B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm," +
                            " B.QtyOut as QtyOutSm,B.CreatedBy, D.Groups,I1.Volume V1,I2.Volume V2,ISNULL(B.MCutter,'-')MCutter FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN " +
                            "FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN " +
                            "FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                            "where I1.partno<>I2.partno and B.rowstatus>-1 and convert(varchar,B.createdtime,112)>='" + thnblnhariawal + "' and convert(varchar,B.createdtime,112)<='" + thnblnhariakhir + "' order by I1.PartNo";
                    }
                    else
                    {
                        strSQL = "SELECT B.ID,B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm," +
                            " B.QtyOut as QtyOutSm,B.CreatedBy, D.Groups,I1.Volume V1,I2.Volume V2,ISNULL(B.MCutter,'-')MCutter FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN " +
                            "FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN " +
                            "FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                            "where I1.partno<>I2.partno and B.rowstatus>-1 and convert(varchar,B.tgltrans,112)>='" + thnblnhariawal + "' and convert(varchar,B.tgltrans,112)<='" + thnblnhariakhir + "' order by I1.PartNo";
                    }
                    alldata = connection.Query<ReportT1T3Simetris>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3Simetris> GetLapHarianSimetrisAutoBS(int thnblnhariawal, int thnblnhariakhir, int param)
        {
            string strSQL = "";
            List<ReportT1T3Simetris> alldata = new List<ReportT1T3Simetris>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (param == 1)
                    {
                        strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsimetris]') AND type in (N'U')) DROP TABLE [dbo].[tempsimetris] " +
                            "select IDENTITY(int, 1,1) AS IDn,ID as CutID,tglinput,Tanggal,PartnoSer,LokasiSer,QtyInSm, " +
                            "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then PartnoSm else '' end PartnoSm, " +
                            "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then LokasiSm else '' end LokasiSm, " +
                            "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then QtyOutSm else 0 end QtyOutSm, " +
                            "case when PartnoSm like '%-p-%' then PartnoSm else '' end PartnoBP, " +
                            "case when PartnoSm like '%-p-%' then LokasiSm else '' end LokasiBP, " +
                            "case when PartnoSm like '%-p-%' then QtyOutSm else 0 end QtyOutBP, " +
                            "case when PartnoSm like '%-S-%' then PartnoSm else '' end PartnoBS, " +
                            "case when PartnoSm like '%-S-%' then LokasiSm else '' end LokasiBS, " +
                            "case when PartnoSm like '%-S-%' then QtyOutSm else 0 end QtyOutBS, " +
                            "CreatedBy into tempsimetris from ( " +
                            "SELECT B.ID,R.ID as RID, B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm,  " +
                            "I3.PartNo AS PartnoSm, L3.Lokasi AS LokasiSm, R.QtyiN as QtyOutSm,B.CreatedBy, D.Groups  " +
                            "FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID  " +
                            "INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2  " +
                            "ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID left join T3_Rekap R on B.ID=R.CutID and R.QtyIn>0 " +
                            "inner join FC_Items I3 on R.ItemID=I3.ID and R.QtyIn>0 inner join FC_Lokasi L3 on R.LokID =L3.ID and R.QtyIn>0   " +
                            "where I1.partno<>I2.partno and B.rowstatus>-1 and R.[status]>-1 and convert(varchar,B.createdtime,112)>='" + thnblnhariawal + "' and  " +
                            "convert(varchar,B.createdtime,112)<='" + thnblnhariakhir + "')  as S order by CutID,RID  " +
                            "select CutID,case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else tglinput end tglinput, " +
                            "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else Tanggal end Tanggal, " +
                            "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else PartnoSer end PartnoSer, " +
                            "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else LokasiSer end LokasiSer, " +
                            "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else QtyInSm end QtyInSm, " +
                            "PartnoSm,LokasiSm,QtyOutSm,PartnoBP,LokasiBP,QtyOutBP,PartnoBS,LokasiBS,QtyOutBS,CreatedBy " +
                            " from tempsimetris A order by cutID,QtyInSm desc,QtyOutSm desc,QtyOutBP desc,QtyOutBS desc ";
                    }
                    else
                    {
                        strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsimetris]') AND type in (N'U')) DROP TABLE [dbo].[tempsimetris] " +
                        "select IDENTITY(int, 1,1) AS IDn,ID as CutID,tglinput,Tanggal,PartnoSer,LokasiSer,QtyInSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then PartnoSm else '' end PartnoSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then LokasiSm else '' end LokasiSm, " +
                        "case when PartnoSm like '%-3-%' or PartnoSm like '%-W-%' or PartnoSm like '%-M-%' then QtyOutSm else 0 end QtyOutSm, " +
                        "case when PartnoSm like '%-p-%' then PartnoSm else '' end PartnoBP, " +
                        "case when PartnoSm like '%-p-%' then LokasiSm else '' end LokasiBP, " +
                        "case when PartnoSm like '%-p-%' then QtyOutSm else 0 end QtyOutBP, " +
                        "case when PartnoSm like '%-S-%' then PartnoSm else '' end PartnoBS, " +
                        "case when PartnoSm like '%-S-%' then LokasiSm else '' end LokasiBS, " +
                        "case when PartnoSm like '%-S-%' then QtyOutSm else 0 end QtyOutBS, " +
                        "CreatedBy into tempsimetris from ( " +
                        "SELECT B.ID,R.ID as RID, B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm,  " +
                        "I3.PartNo AS PartnoSm, L3.Lokasi AS LokasiSm, R.QtyiN as QtyOutSm,B.CreatedBy, D.Groups  " +
                        "FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID  " +
                        "INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2  " +
                        "ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID left join T3_Rekap R on B.ID=R.CutID and R.QtyIn>0 " +
                        "inner join FC_Items I3 on R.ItemID=I3.ID and R.QtyIn>0 inner join FC_Lokasi L3 on R.LokID =L3.ID and R.QtyIn>0   " +
                        "where I1.partno<>I2.partno and B.rowstatus>-1 and R.[status]>-1 and convert(varchar,B.tgltrans,112)>='" + thnblnhariawal + "' and  " +
                        "convert(varchar,B.tgltrans,112)<='" + thnblnhariakhir + "')  as S order by CutID,RID  " +
                        "select CutID,case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else tglinput end tglinput, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else Tanggal end Tanggal, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else PartnoSer end PartnoSer, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else LokasiSer end LokasiSer, " +
                        "case when (select COUNT(cutid) from tempsimetris where cutid = A.cutid and idn <A.idn)>0 then null else QtyInSm end QtyInSm, " +
                        "PartnoSm,LokasiSm,QtyOutSm,PartnoBP,LokasiBP,QtyOutBP,PartnoBS,LokasiBS,QtyOutBS,CreatedBy " +
                        " from tempsimetris A order by cutID,QtyInSm desc,QtyOutSm desc,QtyOutBP desc,QtyOutBS desc ";
                    }
                    alldata = connection.Query<ReportT1T3Simetris>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3Simetris> GetLapMutasiLokasi(int thnblnhariawal, int thnblnhariakhir, int param)
        {
            List<ReportT1T3Simetris> alldata = new List<ReportT1T3Simetris>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "SELECT B.CreatedTime as tglinput,B.TglTrans AS Tanggal, I.PartNo AS PartnoSer, L1.Lokasi AS LokasiSer, L2.Lokasi AS LokasiSm, B.Qty AS QtyOutSm, B.CreatedBy " +
                        "FROM T3_MutasiLok AS B INNER JOIN FC_Items AS I ON B.ItemID = I.ID INNER JOIN FC_Lokasi AS L2 ON B.LokID2 = L2.ID  " +
                        "INNER JOIN FC_Lokasi AS L1 ON B.LokID1 = L1.ID " +
                        "where  B.rowstatus>-1 and  convert(varchar,B.tgltrans,112)>='" + thnblnhariawal + "' and convert(varchar,B.tgltrans,112)<='" + thnblnhariakhir + "' order by I.PartNo";
                    alldata = connection.Query<ReportT1T3Simetris>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3Pengiriman> GetLapPengirimanSJ(int thnblnhariawal, int thnblnhariakhir, string partno, int param)
        {
            string strTgl = "";
            string jam = DateTime.Now.ToString("yyMMddhhmmss");
            if (param == 0)
                strTgl = " WHERE convert(varchar,AA.CreatedTime,112)>='" + thnblnhariawal + "' and convert(varchar,AA.CreatedTime,112)<='" + thnblnhariakhir + "' ";
            else
                strTgl = " WHERE convert(varchar,AA.TglKirim,112)>='" + thnblnhariawal + "' and convert(varchar,AA.TglKirim,112)<='" + thnblnhariakhir + "' ";
            List<ReportT1T3Pengiriman> alldata = new List<ReportT1T3Pengiriman>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwal" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].DataAwal" + jam + " " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Data2" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].Data2" + jam + " " +
                        " SELECT AA.KirimID,AA.CreatedTime ,AA.TglKirim,null  tglSJ,AA.Customer, AA.SJNo, C.PartNo, c.tebal, c.panjang, c.lebar, C.volume, (sum(AA.Qty) * c.volume) m3, (((c.volume * 1000000000) / (4*1220*2440)) * sum(AA.Qty)) konversi, sum(AA.Qty) as Qty,  " +
                        " ((AA.tebal/1000)*sum(AA.Qty) ) as Meter,D.Groups, AA.Cust,AA.pengiriman,AA.JenisPalet,AA.JmlPalet into DataAwal" + jam + "  FROM (select (select tebal from fc_items where ID=b.itemid) as tebal," +
                        " a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo, a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty,  case when " +
                        " SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' then 'CPD' else 'EKS' end Cust,b.pengiriman,b.jenispalet,b.jmlpalet  from T3_Kirim as a," +
                        " T3_KirimDetail as b where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 and b.RowStatus>-1 and b.Status>-1 ) as AA    " +
                        " INNER JOIN FC_Items AS C ON AA.ItemID = C.ID  left JOIN T3_GroupM as D ON D.ID = C.GroupID  " +
                        " " + strTgl + partno + "" +
                        " group by AA.KirimID,AA.KirimDetailID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo, c.tebal, c.panjang, c.lebar, C.volume, D.Groups, AA.Cust,AA.tebal,AA.pengiriman,AA.JenisPalet,AA.JmlPalet " +
                        " select A.*,'' Keterangan,'' from DataAwal" + jam + " A  " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwal" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].DataAwal" + jam + " " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Data2" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].Data2" + jam + " ";
                    alldata = connection.Query<ReportT1T3Pengiriman>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<ReportT1T3Pengiriman> GetLapPengiriman(int thnblnhariawal, int thnblnhariakhir, string PeriodeAwalMundurSF, string PeriodeAkhirMajuSF, string partno, int param)
        {
            string strTgl = "";
            string jam = DateTime.Now.ToString("yyMMddhhmmss");
            if (param == 0)
                strTgl = " WHERE convert(varchar,AA.CreatedTime,112)>='" + thnblnhariawal + "' and convert(varchar,AA.CreatedTime,112)<='" + thnblnhariakhir + "' ";
            else
                strTgl = " WHERE convert(varchar,AA.TglKirim,112)>='" + thnblnhariawal + "' and convert(varchar,AA.TglKirim,112)<='" + thnblnhariakhir + "' ";
            List<ReportT1T3Pengiriman> alldata = new List<ReportT1T3Pengiriman>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwal" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].DataAwal" + jam + " " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Data2" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].Data2" + jam + " " +
                        " SELECT AA.KirimID,AA.CreatedTime ,AA.TglKirim,null  tglSJ,AA.Customer, AA.SJNo, C.PartNo, c.tebal, c.panjang, c.lebar, C.volume, (sum(AA.Qty) * c.volume) m3, (((c.volume * 1000000000) / (4*1220*2440)) * sum(AA.Qty)) konversi, sum(AA.Qty) as Qty,  " +
                        " ((AA.tebal/1000)*sum(AA.Qty) ) as Meter,D.Groups, AA.Cust,AA.pengiriman,AA.JenisPalet,AA.JmlPalet into DataAwal" + jam + "  FROM (select (select tebal from fc_items where ID=b.itemid) as tebal," +
                        " a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo, a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty,  case when " +
                        " SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' then 'CPD' else 'EKS' end Cust,b.pengiriman,b.jenispalet,b.jmlpalet  from T3_Kirim as a," +
                        " T3_KirimDetail as b where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 and b.RowStatus>-1 and b.Status>-1 ) as AA    " +
                        " INNER JOIN FC_Items AS C ON AA.ItemID = C.ID  left JOIN T3_GroupM as D ON D.ID = C.GroupID  " +
                        " " + strTgl + partno + "" +
                        " group by AA.KirimID,AA.KirimDetailID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo, c.tebal, c.panjang, c.lebar, c.volume, D.Groups, AA.Cust,AA.tebal,AA.pengiriman,AA.JenisPalet,AA.JmlPalet " +
                        " select *  into Data2" + jam + " from (select suratjalanno,Sc.Scheduleno,AA.keterangan,BB.Alamatlain from [sql1.grcboard.com].GRCboard.dbo.suratjalan AA LEFT JOIN " +
                        " [sql1.grcboard.com].GRCboard.dbo.OP BB ON AA.OPID=BB.ID  inner join [sql1.grcboard.com].GRCboard.dbo.schedule Sc on Sc.ID=AA.scheduleid " +
                        " where convert(varchar,PostingReceiveDate,112)>='" + PeriodeAwalMundurSF + "' and " +
                        " convert(varchar,PostingReceiveDate,112)<='" + PeriodeAkhirMajuSF + "' and AA.status>-1 and BB.status>-1 " +
                        " union all " +
                        " select suratjalanno,Sc.Scheduleno,''keterangan,DD.ToDepoName Alamatlain from [sql1.grcboard.com].GRCboard.dbo.suratjalanTO CC " +
                        " LEFT JOIN [sql1.grcboard.com].GRCboard.dbo.TransferOrder DD ON CC.TransferOrderID=DD.ID inner join [sql1.grcboard.com].GRCboard.dbo.schedule Sc on Sc.ID=CC.scheduleid " +
                        " where convert(varchar,CC.CreatedTime,112)>='" + PeriodeAwalMundurSF + "' and convert(varchar,CC.CreatedTime,112)<='" + PeriodeAkhirMajuSF + "' " +
                        " and CC.status>-1 and DD.status>-1 and DD.rowstatus>-1 ) A " +
                        " select A.*,B.ScheduleNo,ISNULL(B.keterangan,'')Keterangan,ISNULL(B.AlamatLain,'')Alamat from DataAwal" + jam + " A LEFT JOIN Data2" + jam + " B ON A.SJNo=B.suratjalanno " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwal" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].DataAwal" + jam + " " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Data2" + jam + "]') AND type in (N'U')) DROP TABLE [dbo].Data2" + jam + " ";
                    alldata = connection.Query<ReportT1T3Pengiriman>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public static decimal GetTargetCustomerComplaint(int thnblnhariawal, int thnblnhariakhir)
        {
            decimal target;
            string jam = DateTime.Now.ToString("yyMMddhhmmss");
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwalR" + jam + "]') AND type in (N'U')) DROP TABLE[dbo].DataAwalR" + jam + " " +
                        " SELECT (((AA.tebal*AA.Lebar*AA.Panjang)*(sum(AA.Qty)))/1000000000)as M3,AA.tebal,AA.Lebar,AA.Panjang,AA.KirimID,AA.CreatedTime ,AA.TglKirim,null  tglSJ,AA.Customer, AA.SJNo, C.PartNo, sum(AA.Qty) as Qty, " +
                        " ((AA.tebal/1000)*sum(AA.Qty) ) as Meter,D.Groups, AA.Cust,AA.pengiriman,AA.JenisPalet,AA.JmlPalet into DataAwalR" + jam + "  FROM " +
                        " (select (select tebal from fc_items where ID=b.itemid) as tebal,(select Lebar from FC_Items where ID=b.ItemID) as Lebar,(select Panjang from FC_Items where ID=b.ItemID) as Panjang, " +
                        " a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo, a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty,  case when  SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' " +
                        " then 'CPD' else 'EKS' end Cust,b.pengiriman,b.jenispalet,b.jmlpalet  from T3_Kirim as a, T3_KirimDetail as b where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 and b.RowStatus>-1 and b.Status>-1 ) as AA  " +
                        " INNER JOIN FC_Items AS C ON AA.ItemID = C.ID  left JOIN T3_GroupM as D ON D.ID = C.GroupID    WHERE convert(varchar,AA.TglKirim,112)>='" + thnblnhariawal + "' and convert(varchar,AA.TglKirim,112)<='" + thnblnhariakhir + "'" +
                        " group by AA.KirimID,AA.KirimDetailID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo,D.Groups, AA.Cust,AA.tebal,AA.Lebar,AA.Panjang,AA.pengiriman,AA.JenisPalet,AA.JmlPalet,Qty " +
                        " select isnull([target],0) [target] from ( " +
                        " select M3, " +
                        " case when cast(M3 as int)>=0 and cast(M3 as int)<=4000 then '1'  " +
                            " when cast(M3 as int)>=4001 and cast(M3 as int)<=8000 then '2'  " +
                            " when cast(M3 as int)>=8001 and cast(M3 as int)<=12000 then '3'  " +
                            " when cast(M3 as int)>=12001 and cast(M3 as int)<=16000 then '4'  " +
                        " end [Target]  " +
                        " from (  " +
                        " select sum(M3)M3 From DataAwalR" + jam + " " +
                        " ) as Data1 " +
                        " ) as Data2 " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwalR" + jam + "]') AND type in (N'U')) DROP TABLE[dbo].DataAwalR" + jam + " ";
                    target = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    target = 0;
                }
            }
            return target;
        }

        public static int GetSPDDept(string dept)
        {
            int deptid;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select id from spd_dept where dept like '%" + dept + "%'";
                    deptid = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    deptid = 0;
                }
            }
            return deptid;
        }

        public static int UpdateSarmutCustomerComplaint(decimal target, int tahun, int bulan, int dept, string sarmutPrs)
        {
            int r;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + tahun +
                        " set @bulan=" + bulan + " " +
                        "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                        "update SPD_TransPrs set Target=" + target.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan " +
                        " and SarmutPID in ( " +
                        "select ID from SPD_Perusahaan where deptid=" + dept + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
                    connection.Execute(query);
                    r = 1;
                }
                catch (Exception e)
                {
                    r = 0;
                }
                try
                {
                    string query = "update SPD_TransPrs set Target= " + target.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + tahun +
                        " and Bulan=" + bulan +
                        " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
                    connection.Execute(query);
                    r = 1;
                }
                catch (Exception e)
                {
                    r = 0;
                }
            }
            return r;
        }
    }

    public class ReturnBJ : GRCBaseDomain
    {
        public virtual DateTime Tanggal { get; set; }
        public virtual string SJNo { get; set; }
        public virtual string OPNo { get; set; }
        public virtual string Customer { get; set; }
        public virtual string PartNo { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string typeR { get; set; }
    }
}