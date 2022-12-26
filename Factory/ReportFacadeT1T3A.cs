using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Domain;
using Cogs;

namespace Factory
{
    public class ReportFacadeT1T3
    {
        public string ViewPemantauanPelarian(string tgl1, string tgl2, int tgltype, string partno)
        {
            string strSQL;
            if (tgltype == 1)
            {
                strSQL = "SELECT ID,DestID, TglJemur, TglSerah, PartNo1, Lokasi1, kubik1, PartNo2, Lokasi2, qty, kubik2, kubik1 - kubik2 AS sisa "+
                        "FROM (SELECT ID,DestID, TglJemur, PartNo1, Lokasi1, PartNo2, Lokasi2, qty, [user],  "+
                        "(select top 1 tglserah from T1_Serah where DestID=P.DestID and JemurID=P.ID) as TglSerah, Tebal, Lebar, Panjang, Tebal2, Lebar2, Panjang2,  "+
                        "Tebal * Panjang * Lebar / 1000000000 * qty AS kubik1, Tebal2 * Panjang2 * Lebar2 / 1000000000 * qty AS kubik2 "+
                        "FROM (SELECT DISTINCT A.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, A.QtyIn AS qty, A.CreatedBy AS [user], I1.Tebal, I1.Lebar,  "+
                        "I1.Panjang, I2.Tebal AS Tebal2, I2.Lebar AS Lebar2, I2.Panjang AS Panjang2, A.DestID, A.ID "+
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
                   tgl1+ tgl2 + partno + "";
            //}
            return strSQL;
        }

        public string ViewLTransitHPel(string tgl1, string tgl2, int tgltype,string Partno)
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
                      " INNER JOIN FC_Items	AS BC ON P1.ItemID0 = BC.ID " +
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
            //                  " INNER JOIN FC_Items	AS BC ON P1.ItemID0 = BC.ID " +
            //                  " INNER JOIN FC_Lokasi AS CC ON P1.LokID0 = CC.ID " +
            //                  " order by P1.TglJemur ";
            //}
            return strSQL;
        }

        public string ViewLPenyerahan(string tgl,string periode)
        {
            string strSQL;
            if (periode=="harian")
            strSQL = "SELECT A.TglProduksi, A.FormulaID ,D.FormulaCode, B.Tebal, B.Lebar, B.Panjang, SUM(A.Qty) as qty, C.Lokasi " +
                "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS B ON A.ItemID = B.ID LEFT OUTER JOIN " +
                "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN BM_Formula AS D ON A.FormulaID = D.ID where qty>0 and convert(varchar,A.TglProduksi,112)='" + tgl + "' " +
                " and  A.rowstatus=0 GROUP BY A.TglProduksi, A.FormulaID, D.FormulaCode, B.Tebal, B.Lebar, B.Panjang, C.Lokasi " +
                "order by A.TglProduksi,D.formulacode  ";
            else
                strSQL = "SELECT A.TglProduksi, A.FormulaID ,D.FormulaCode, B.Tebal, B.Lebar, B.Panjang, SUM(A.Qty) as qty, C.Lokasi " +
               "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS B ON A.ItemID = B.ID LEFT OUTER JOIN " +
               "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN BM_Formula AS D ON A.FormulaID = D.ID where qty>0 and left(convert(varchar,A.TglProduksi,112),6)='" + tgl.Substring(0, 6) + "' " +
               " and  A.rowstatus=0 GROUP BY A.TglProduksi, A.FormulaID, D.FormulaCode, B.Tebal, B.Lebar, B.Panjang, C.Lokasi " +
               "order by A.TglProduksi,D.formulacode  ";
            return strSQL;
        }

        public string ViewLSimetris(string tgl1, string tgl2,int tgltype)
        {
            string strSQL;
            if (tgltype == 0) 
            strSQL = "SELECT B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm," +
                " B.QtyOut as QtyOutSm,B.CreatedBy, D.Groups FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN " +
                "FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN " +
                "FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                "where I1.partno<>I2.partno and B.rowstatus>-1 and convert(varchar,B.tgltrans,112)>='" + tgl1 + "' and convert(varchar,B.tgltrans,112)<='" + tgl2 + "' order by I1.PartNo";
            else 
            strSQL = "SELECT B.CreatedTime as tglinput,B.tgltrans as Tanggal, I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm," +
                " B.QtyOut as QtyOutSm,B.CreatedBy, D.Groups FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN " +
                "FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN " +
                "FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                "where I1.partno<>I2.partno and B.rowstatus>-1 and convert(varchar,B.createdtime,112)>='" + tgl1 + "' and convert(varchar,B.createdtime,112)<='" + tgl2 + "' order by I1.PartNo";
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
            strSQL = "SELECT C.PartNo,  cast(CAST(C.tebal as decimal(18,1)) as varchar) + ' X ' + cast(CAST(C.Lebar as int) as varchar)+ " +
                    "' X ' + cast(CAST(C.Panjang as int) as varchar) as ukuran, B.Lokasi, A.Qty " +
                    "FROM  T3_Serah AS A INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID INNER JOIN FC_Items AS C ON A.ItemID = C.ID " +
                    "WHERE  A.rowstatus>-1 and  A.qty <> 0 " + kriteria;
            return strSQL;
        }
        //transit in harian
        public string ViewLTransitIn(string tgl1, string tgl2, int tgltype, string Partno)
        {
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

            strSQL = "SELECT A.ID, B.TglProduksi,  A.TglSerah, I1.PartNo AS PartNo1, L2.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, L1.Lokasi AS Lokasi2, A.QtyIn AS qty,  " +
                   "A.CreatedBy AS [user], P.NoPAlet AS Palet " +
                   "FROM FC_Lokasi AS L1 RIGHT OUTER JOIN T1_Serah AS A ON L1.ID = A.LokID LEFT OUTER JOIN FC_Items AS I2 ON A.itemID = I2.ID LEFT OUTER JOIN " +
                   "FC_Lokasi AS L2 RIGHT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS B INNER JOIN FC_Items AS I1 ON B.ItemID = I1.ID ON P.ID = B.PaletID ON L2.ID = B.LokasiID ON A.DestID = B.ID where L1.lokasi <>'p99' and A.status>-1 " + tgl1 + tgl2 + Partno + " ORDER BY  A.tglserah";
            //}
            return strSQL;
        }
        //transit in pelarian
        public string ViewLTransitInPel(string kriteria )
        {
            string strSQL;
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
            strSQL = "select *,(qty1-isnull(qty2,0)) as sisa  from ( " +
                "select lari.ID,lari.DestID,TglProduksi,TglJemur,createdtime,PartNo1,Lokasi1,NoPAlet,PartNo2,Lokasi2,case when "+
                "(select COUNT(destid) from T1_Serah where JemurID=serah.JemurID and  ID<serah.serahID )>0 then 0 else Qty1 end Qty1," +
                "[user],tglinput,tglinputserah,serahID,jemurID,TglSerah,PartNo3,Lokasi3,QTY2  " +
                "from ( " +
                "SELECT B.ID,B.DestID, A.TglProduksi,B.createdtime, B.TglJemur, I1.PartNo AS PartNo1, L1.Lokasi AS Lokasi1, P.NoPAlet, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2,  " +
                "  B.Qtyin  AS Qty1, B.CreatedBy AS [user], B.CreatedTime AS tglinput  " +
                "FROM FC_Lokasi AS L2 RIGHT OUTER JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 LEFT OUTER JOIN " +
                "FC_Items AS I2 ON B.ItemID0 = I2.ID LEFT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS A ON P.ID = A.PaletID LEFT OUTER JOIN " +
                "FC_Items AS I1 ON A.ItemID = I1.ID LEFT OUTER JOIN FC_Lokasi AS L1 ON A.LokasiID = L1.ID ON B.DestID = A.ID " +
                "WHERE (B.Status > - 1) ) as lari " +
                "left join  " +
                "(SELECT C.ID, C.DestID, C.jemurID,  C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID, I3.PartNo AS PartNo3, L3.Lokasi AS Lokasi3, C.QtyIn AS QTY2 " +
                "FROM FC_Items AS I3 RIGHT OUTER JOIN FC_Lokasi AS L3 RIGHT OUTER JOIN T1_Serah AS C ON L3.ID = C.LokID ON I3.ID = C.ItemID " +
                "WHERE C.Status > - 1) as serah on lari.DestID=serah.DestID and lari.ID=serah.JemurID " +
                ") as lagi  where destid>0  " + kriteria + " ORDER BY DestID,ID";
            return strSQL;
        }
        //saldo bulanan
        public string ViewLSaldoItemB(string blnQty, int tahun, string jenis)
        {
            string strSQL;
            strSQL = "SELECT distinct B.PartNo, B.ItemDesc, A." + blnQty + " as qty, A.YearPeriod FROM SaldoInventoryBJ AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID " +
                    "where A." + blnQty + ">0 and A.YearPeriod=" + tahun + " and  B.PartNo like'%-" + jenis + "-%'";
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
                strSQL = "select ID,id_dstk,FormulaCode as Jenis, TglProduksi,GP, PartNo, Lokasi, NoPAlet, JML_DEST, TglJemur, Rak, "+
                    "case when JML_DEST=0 and isnull(JML_JEMUR,0)>0 then 0 else  isnull(JML_JEMUR,0) end JML_JEMUR, TglSerah, "+
                    "isnull(JML_SERAH,0) as JML_SERAH ,JML_DEST-isnull(JML_SERAH,0) as Sisa from( "+
                    "SELECT   A.ID, A.id_dstk, A.TglProduksi, IA.PartNo, LA.Lokasi, P.NoPAlet, A.Qty AS JML_DEST, NULL AS TglJemur,null as Rak, NULL AS JML_JEMUR,  " +
                    "NULL AS TglSerah, NULL AS JML_SERAH, J.FormulaCode, G.[Group] AS GP " +
                    "FROM BM_PlantGroup AS G RIGHT OUTER JOIN FC_Lokasi AS LA RIGHT OUTER JOIN BM_Destacking AS A ON LA.ID = A.LokasiID ON G.ID = A.PlantGroupID LEFT OUTER JOIN " +
                    "BM_Formula AS J ON A.FormulaID = J.ID LEFT OUTER JOIN BM_Palet AS P ON A.PaletID = P.ID LEFT OUTER JOIN FC_Items AS IA ON A.ItemID = IA.ID " +
                    "WHERE    A.status=0 and  (A.RowStatus > - 1) AND  " +
                    "(CONVERT(varchar, A.TglProduksi, 112) >= '" + tgl1 + "') AND (CONVERT(varchar, A.TglProduksi, 112) <= '" + tgl2 + "') " + kriteria +
                    " union all " +
                    "SELECT A.ID, A.id_dstk, A.TglProduksi, IA.PartNo, LA.Lokasi, P.NoPAlet, A.Qty AS JML_DEST, B.TglJemur, FC_Rak.Rak, B.QtyIn  AS JML_JEMUR, NULL  "+
                    "AS TglSerah, NULL AS JML_SERAH, J.FormulaCode, G.[Group] AS GP "+
                    "FROM BM_PlantGroup AS G RIGHT OUTER JOIN T1_Jemur AS B INNER JOIN FC_Rak ON B.RakID = FC_Rak.ID RIGHT OUTER JOIN "+
                    "BM_Destacking AS A ON B.DestID = A.ID ON G.ID = A.PlantGroupID LEFT OUTER JOIN BM_Formula AS J ON A.FormulaID = J.ID RIGHT OUTER JOIN "+
                    "FC_Items AS IA ON A.ItemID = IA.ID LEFT OUTER JOIN BM_Palet AS P ON A.PaletID = P.ID RIGHT OUTER JOIN FC_Lokasi AS LA ON A.LokasiID = LA.ID " +
                    "WHERE  A.Status=1 and  (A.RowStatus >-1) and B.RowStatus>-1 AND  "+
                    "(CONVERT(varchar, A.TglProduksi, 112) >= '" + tgl1 + "') AND (CONVERT(varchar, A.TglProduksi, 112) <= '" + tgl2 + "') " + kriteria +
                    " union all "+
                    "SELECT C.ID,A.id_dstk, A.TglProduksi, IA.PartNo, LA.Lokasi, P.NoPAlet, "+
                    "case when C.ID >0 and (select COUNT(ItemID) from T1_Serah C1 where C1.DestID =C.DestID and C1.ID<C.ID)=0 then A.Qty else 0 end JML_DEST,  "+
                    "B.TglJemur, FC_Rak.Rak, B.QtyIn AS JML_JEMUR, C.TglSerah, C.QtyIn AS JML_SERAH, J.FormulaCode, G.[Group] as GP "+
                    "FROM BM_Palet AS P RIGHT OUTER JOIN BM_PlantGroup AS G RIGHT OUTER JOIN BM_Destacking AS A ON G.ID = A.PlantGroupID RIGHT OUTER JOIN "+
                    "FC_Items AS IA ON A.ItemID = IA.ID ON P.ID = A.PaletID RIGHT OUTER JOIN FC_Lokasi AS LA ON A.LokasiID = LA.ID LEFT OUTER JOIN "+
                    "BM_Formula AS J ON A.FormulaID = J.ID LEFT OUTER JOIN T1_Jemur AS B LEFT OUTER JOIN T1_Serah AS C ON B.DestID = C.DestID LEFT OUTER JOIN " +
                    "FC_Rak ON B.RakID = FC_Rak.ID ON A.ID = B.DestID WHERE A.Status=2 and C.status>-1 and (A.RowStatus >= 0) AND (B.RowStatus >= 0) AND (CONVERT(varchar, A.TglProduksi, 112) >= '" + tgl1 + 
                    "') AND (CONVERT(varchar, A.TglProduksi, 112) <= '" + tgl2 + "')"+ kriteria +")  " +
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
            string strSQL=string.Empty ;
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
            string strSQL=string.Empty ;
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
				    "	and a3.SFrom='lari' group by  a3.DestID) as lari),0) as QtySisaP99," +
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
            if (process=="curing")
            strSQL = "select  null as tgljemurlg,*,(select SUM(qtyin-qtyout) from T1_Jemur where DestID=A.destid)as sisajemur from (select destid, partno,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
                "where convert(varchar,tglproduksi,112)>='20130601' and  convert(varchar,tglproduksi,112)<='" + thnbln2 + "' " +
                "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid having sum(Qty)<>0 ) as A  where isnull(tgljemur,'1/1/1900')='1/1/1900'";
            else
                strSQL = "select  null as tgljemurlg,*,(select SUM(qtyin-qtyout) from T1_Jemur where DestID=A.destid)as sisajemur from (select destid, partno,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
               "where convert(varchar,tglproduksi,112)>='20130601' and  convert(varchar,tglproduksi,112)<='" + thnbln2 + "' " +
               "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid having sum(Qty)<>0 ) as A  where isnull(tgljemur,'1/1/1900')<>'1/1/1900'";
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
                    "select thnbln, itemid,lokid,Saldo + (select isnull(SUM(qty),0) from vw_KartustockWIP where itemid0=T1S.ItemID and lokid=T1S.lokid and "+
                    "LEFT(convert(varchar,tanggal,112),6)='" + thnblntrans + "' and convert(varchar,tanggal,112)<'"+tanggal1+
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
            strSQL = "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM("+awal+" ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID AS JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod="+tahun+" and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
                    ") as saldo1 where QTY>0 union all "+
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
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID AS JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
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
            strSQL = "select sum(awal) as saldoawalbp,sum(awalkubikasi) as saldoawalbpkubik,0 as saldoawalok,0 as saldoawalokkubik,sum(qty)  as saldobp,sum(kubikasi)  as saldobpkubik,0 as saldook,sum(kubikasi) as saldookkubik from (" +
                    "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID AS JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
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
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID AS JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
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
            strSQL = "select 0 as saldoawalbp,0 as saldoawalbpkubik,sum(awal) as saldoawalok,sum(awalkubikasi) as saldoawalokkubik,0 as saldobp,0 as saldobpkubik,sum(qty) as saldook,sum(kubikasi) as saldookkubik from (" +
                    "select 'OK' as kwalitas,rtrim(cast(CAST(Tebal as decimal(18,1)) as CHAR)) + ' X ' + rtrim(cast(CAST(Lebar as decimal(18)) as CHAR)) + ' X ' + " +
                    "rtrim(cast(CAST(Panjang as decimal(18)) as CHAR)) as ukuran, *, Awal*volume as AwalKubikasi,  qty*volume as Kubikasi from ( " +
                    "select Tebal,Lebar,Panjang,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume, SUM(awal) as awal,SUM(terima)  as terima, SUM(keluar)  as keluar , SUM(awal)+SUM(terima)- SUM(keluar) as QTY from ( " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,SUM(qty)  as terima, 0 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%') and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and  " +
                    "A.qty>0 and convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "'  group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,0 as awal,0 as terima,SUM(qty)*-1 as keluar from vw_KartuStockBJ A, FC_Items B  " +
                    "where A.ItemID =B.ID and (B.PartNo like '%-3-%' OR B.PartNo  like '%-W-%') and A.qty<0 and lokid not in(select ID from FC_Lokasi where lokasi like 'S%' ) and " +
                    "convert(varchar,tanggal,112)>='" + tglawal + "'  and convert(varchar,tanggal,112)<='" + tglakhir + "' group by B.Tebal,B.Lebar,B.Panjang  " +
                    "union all " +
                    "select B.Tebal,B.Lebar,B.Panjang,SUM(" + awal + " ) as awal, 0 as terima,0 as keluar from  ( " +
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID AS JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
                    "where  A.YearPeriod=" + tahun + " and A.ItemID =B.ID and (B.PartNo  like '%-3-%' OR B.PartNo  like '%-W-%') group by B.Tebal,B.Lebar,B.Panjang ) as saldo group by Tebal,Lebar,Panjang  " +
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
                    "SELECT DISTINCT ItemID, YearPeriod, ItemTypeID, GroupID AS JanQty, FebQty, MarQty, AprQty, MeiQty, JunQty, JunAvgPrice, JulQty, AguQty, SepQty, OktQty, NovQty, DesQty  FROM SaldoInventoryBJ)as  A, FC_Items B  " +
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
                kriteria = " Where ukuran='" + ukuran + "' and items='" +items + "' ";
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode0) <= Convert.ToInt32(periode))
            strSQL="create table #adminksOKUkuran(id varchar(100),Tanggal datetime,items varchar(25),qty int) "+
                "declare @id varchar(100) "+
                "declare @Tanggal datetime "+
                "declare @items varchar(100) "+ 
                "declare @qty int "+
                "declare kursor cursor for "+
                "select id,Tanggal,items,qty from vw_T3KartustockOKByUkuran "+
                "where LEFT(CONVERT(varchar, Tanggal , 112), 6) = '" + Periode1 + "' " +
                "open kursor "+
                "FETCH NEXT FROM kursor "+
                "INTO @ID,@Tanggal,@items,@qty "+
                "WHILE @@FETCH_STATUS = 0 "+
                "begin "+
	            "    insert into #adminksOKUkuran(ID,Tanggal,items,qty)values(@ID,@Tanggal,@items,@qty) "+
	            "    FETCH NEXT FROM kursor "+
	            "    INTO @ID,@Tanggal,@items,@qty "+
                "END "+
                "CLOSE kursor "+
                "DEALLOCATE kursor "+
                "select ID, tanggal, ukuran, items,Keterangan,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo from ( "+
                "select A.Periode as ID, CAST('201402'+'01' as datetime) as tanggal, ukuran, items,'Saldo Awal' as Keterangan,qty as awal, "+
                "0 as Penerimaan,0 as pengeluaran from vw_T3SaldoAwalOK A  where A.Qty>0 and A.Periode= '" + Periode0 + "' " +
                "union "+
                "select ID, tanggal, ukuran, items,Keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( "+
                "    select qty  from vw_T3SaldoAwalOK A  where A.Items=ain1.Items and A.Periode= '" + Periode0 + "' " +
	            "    union "+
	            "    SELECT isnull(SUM(qty),0) as qty from #adminksOKUkuran where items=ain1.items and ID<ain1.ID)as ain0) end  awal,  "+
                "Penerimaan,pengeluaran from ( "+
                "SELECT ID, tanggal,  ukuran, items,Keterangan,Qty as Penerimaan,0 as Pengeluaran FROM vw_T3KartustockOKByUkuran "+
                "where Qty >0 and LEFT(convert(varchar,tanggal,112),6) ='" + Periode1 + "' " +
                "union  "+
                "SELECT ID, tanggal, ukuran, items,Keterangan,0 as Penerimaan,Qty *-1 as Pengeluaran FROM vw_T3KartustockOKByUkuran "+
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
        public string ViewLKirim(string tgl1, string tgl2, int tgltype, string partno)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            if (tgltype == 0)
                strTgl = " WHERE convert(varchar,AA.CreatedTime,112)>='" + tgl1 + "' and convert(varchar,AA.CreatedTime,112)<='" + tgl2 + "' ";
            else
                strTgl = " WHERE convert(varchar,AA.TglKirim,112)>='" + tgl1 + "' and convert(varchar,AA.TglKirim,112)<='" + tgl2 + "' ";
            //strSQL = " SELECT A.CreatedTime, A.TglKirim, A.Customer, A.SJNo, C.PartNo, B.Qty FROM T3_Kirim AS A INNER JOIN " +
            //           "T3_KirimDetail AS B ON A.ID = B.KirimID INNER JOIN FC_Items AS C ON B.ItemID = C.ID " + strTgl + partno ;
            strSQL = " SELECT   AA.KirimID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo, sum(AA.Qty) as Qty, D.Groups, AA.Cust " +
                     " FROM (select a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo,a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty, " +
                     " case when SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' then 'CPD' else 'EKS' end Cust " +
                     " from T3_Kirim as a,T3_KirimDetail as b where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 and b.RowStatus>-1 and b.Status>-1 ) as AA  " +
                     " INNER JOIN FC_Items AS C ON AA.ItemID = C.ID " +
                     " left JOIN T3_GroupM as D ON D.ID = C.GroupID " + strTgl + partno +" group by AA.KirimID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo,D.Groups, AA.Cust";
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
            strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total, "+
                "case when ItemID>0 then (select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "' and convert(varchar,tanggal  ,112)<B.HMY  " +
                "and ItemID=B.ItemID )+(select distinct " + awal + " from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + " ) end Awal from (select *, " +
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process='direct' and process not like 'ex%') end TWIP, "+
                "case when ItemID >0 then (select isnull(avg(HPP),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process='direct' and process not like 'ex%') end HTWIP, "+
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and SUBSTRING(Keterangan,5,1)!='3' and SUBSTRING(Keterangan ,5,1)!='W') end TBP, " +
                "case when ItemID >0 then (select isnull(AVG(HPP),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and SUBSTRING(Keterangan,5,1)!='3' and SUBSTRING(Keterangan ,5,1)!='W')  end HTBP, " +
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' OR SUBSTRING(Keterangan ,5,1)='W'))  end TBJ, " +
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' OR SUBSTRING(Keterangan ,5,1)='W'))  end HTBJ, " +
                "case when ItemID >0 then (select isnull(SUM(Qty),0) from T3_Retur where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end TRETUR, "+
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Retur where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end HTRETUR, "+
                "case when ItemID >0 then (select isnull(SUM(QtyIn),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end TAdjust, "+
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end HTAdjust, "+
                "case when ItemID >0 then (select isnull(SUM(Qty),0) from T3_KirimDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and KirimID>0) end KKirim, " +
                "case when ItemID >0 then (select isnull(SUM(hpp),0) from T3_KirimDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and KirimID>0) end HKKirim, " +
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='P' or SUBSTRING(Keterangan,6,1)='P')  " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end KBP, "+
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='P' or  SUBSTRING(Keterangan,6,1)='P')  " + //
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end HKBP, "+
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' Or SUBSTRING(Keterangan,5,1)='W' or SUBSTRING(Keterangan,6,1)='3' Or SUBSTRING(Keterangan,6,1)='W') " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end KBJ, "+
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='3' Or SUBSTRING(Keterangan,5,1)='W' or SUBSTRING(Keterangan,6,1)='3' Or SUBSTRING(Keterangan,6,1)='W') " +//
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' )) end HKBJ, "+
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' "+
                "and LokID In (select ID from FC_Lokasi where lokasi='q99')) end KSample, "+
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%'  "+
                "and LokID In (select ID from FC_Lokasi where lokasi='q99')) end HKSample, "+
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='s' Or SUBSTRING(Keterangan,6,1)='s') " +//
                " ) end KBS, "+
                "case when ItemID >0 then (select isnull(avg(hpp),0) from T3_Rekap where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 and process != 'direct'and process not like 'ex%' and (SUBSTRING(Keterangan,5,1)='s' Or SUBSTRING(Keterangan,6,1)='s') " +//
                " ) end HKBS, "+
                "case when ItemID >0 then (select isnull(SUM(QtyOut),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end KAdjust, " +
                "case when ItemID >0 then (select isnull(Avg(HPP),0) from T3_AdjustDetail where ItemID=A.ItemID and convert(varchar,CreatedTime ,112)=A.HMY " +
                "and RowStatus>-1 ) end HKAdjust from ( " +
                "select distinct convert(varchar,tanggal ,112) as HMY,(select CONVERT(varchar,tanggal,103)) as tanggal, ItemID from vw_KartuStockBJ ) as A) as B  " +
                "where LEFT(convert(varchar,HMY,112),6)='" + Periode + "' and ItemID in  " +
                "(select ID from FC_Items where partno='" + PartNO + "' and (SUBSTRING(partno,5,1)='3' or SUBSTRING(partno,5,1)='W'))order by tanggal";
            return strSQL;
        }

        public string ViewMutasiBJDetRekap(string Periode, string awal,string blnqty)
        {
            string strSQL = string.Empty;
            string strTgl = string.Empty;
            int tahun = 0;
            int tahun1 = int.Parse(Periode.Substring(0, 4));
            if (int.Parse(Periode.Substring(4, 2)) > 1)
                tahun = int.Parse(Periode.Substring(0, 4));
            else
                tahun = int.Parse(Periode.Substring(0, 4))-1;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
            
            strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                //"(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                "(select distinct  isnull(" + awal + ",0) from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) as TWIP,  " +
                "0 as HTWIP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                "0 as HTBP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select  '" + Periode + "' as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%')) as B0) as A  ) as B  " +
                ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            else 
            {
            strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                //"(select isnull(SUM(qty),0) from vw_KartuStockBJNew where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                "(select distinct  isnull(" + awal + ",0) from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) as TWIP,  " +
                "0 as HTWIP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                "0 as HTBP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJNew where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select  '" + Periode + "' as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%')) as B0) as A  ) as B  " +
                ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
        } 
            return strSQL;
        }

        public string ViewMutasiBSDetRekap(string Periode, string awal, string blnqty)
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
            strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                //"(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                "(select distinct  isnull(" + awal + ",0) from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) as TWIP,  " +
                "0 as HTWIP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                "0 as HTBP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0)* -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select  '" + Periode + "' as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            else
                strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJNew where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                "(select distinct  isnull(" + awal + ",0) from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) as TWIP,  " +
                "0 as HTWIP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                "0 as HTBP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0)* -1 from vw_KartuStockBJNew where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select  '" + Periode + "' as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
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
                "' and convert(varchar,tanggal  ,112)<B.HMY and ItemID=B.ItemID )+(select distinct " + awal +
                " from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + " ) as Awal from (select *, " +
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
            if (dept==1)
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
            if (dept==1)
                strdept= " and dept='FINISHING' ";
            if (dept == 2)
                strdept = " and isnull(dept,'-')<>'FINISHING' ";
            if (dept == 2)
                PlusStockdept = " and dept='FINISHING' ";
            if (dept == 1)
                PlusStockdept = " and isnull(dept,'-')<>'FINISHING' ";
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
            strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, "+
                //"(select isnull(SUM(qty),0) from vw_KartuStockBJ where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                "(select distinct  isnull(" + blnqty + ",0) from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, "+
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID   and left(convert(varchar,tanggal ,112),6)=A.HMY) as TWIP,  " +
                "0 as HTWIP,  "+
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                "0 as HTBP,  "+
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                "0 as HTBJ,  "+
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID   and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  "+
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  "+
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') as KKirim,  " +
                "0 as HKKirim,  "+
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') "+  
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  "+
                "0 as HKBP,  "+
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') "+  
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  "+
                "0 as HKBJ,  "+
                "(select isnull(SUM(Qty),0)* -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  "+
                "0 as HKSample,  "+
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                "0 as HKBS,  "+
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) as KAdjust,  " +
                "0 as HKAdjust from (  "+
                "select  '" + Periode + "' as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                ") as C where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
            else
                strSQL = "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                    //"(select isnull(SUM(qty),0) from vw_KartuStockBJNew where LEFT(convert(varchar,tanggal,112),6)='" + Periode + "'  " +
                    //"and convert(varchar,tanggal  ,112)<B.HMY  and ItemID=B.ItemID)+ "+
                "(select distinct  isnull(" + blnqty + ",0) from SaldoInventoryBJ where ItemID=B.ItemID and YearPeriod=" + tahun + ") as Awal from (select *,  " +
                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where process='direct' and ItemID=A.ItemID   and left(convert(varchar,tanggal ,112),6)=A.HMY) as TWIP,  " +
                "0 as HTWIP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                "0 as HTBP,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                "0 as HTBJ,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where ItemID=A.ItemID   and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') as TRETUR,  " +
                "0 as HTRETUR,  " +
                "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') as TAdjust,  " +
                "0 as HTAdjust,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') as KKirim,  " +
                "0 as HKKirim,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                "0 as HKBP,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                "0 as HKBJ,  " +
                "(select isnull(SUM(Qty),0)* -1 from vw_KartuStockBJNew where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                "0 as HKSample,  " +
                "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                "0 as HKBS,  " +
                "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) as KAdjust,  " +
                "0 as HKAdjust from (  " +
                "select  '" + Periode + "' as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                ") as C where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
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
                   "SELECT CONVERT(varchar, TglJemur, 112) + '0'+  RTRIM(CAST(A.ItemID0 AS varchar(10)))  + CONVERT(varchar, C.TglProduksi, 112) +RTRIM(CAST(A.ItemID AS varchar(10))) " +
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
                lastperiode = (Convert.ToInt32(Thn) - 1).ToString()+12 ;
            else
                lastperiode = Thn + (Convert.ToInt32(bln) - 1).ToString().PadLeft(2,'0');
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(Periode) <= Convert.ToInt32(periode))
                 strSQL =
                    "select itemID,HMY,NoDocument,AwalQty, " +
                    "InProdQty,InAdjustQty,outAdjustQty,H99,I99,B99,C99,Q99,InP99,OutP99,(select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= M2.itemid)as volume from (  " +
                   "select itemID,HMY,partno as NoDocument, " +
                   "(select isnull(SUM(qty),0) from BM_Destacking where qty>0 and LEFT(CONVERT(varchar,TglProduksi, 112), 6) = M1.HMY " +
                   "and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%adj%')) as InProdQty," +
                   "(select isnull(SUM(qty),0) from vw_KartustockWIPOld where qty>0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%in%') as InAdjustQty, " +
                   "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIPOld where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and (lokasi='H99' or lokasi like '%in%')) as H99, " +
                   "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIPOld where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='B99') as B99, " +
                   "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIPOld where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='C99') as C99, " +
                   "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIPOld where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='Q99') as Q99, " +
                   "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIPOld where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%ou%') as outAdjustQty,0 as InP99, 0 as OutP99, " +
                   "(select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln='" + lastperiode + "' and itemid =M1.ItemID and lokid not in (select ID from fc_lokasi where lokasi like '%p99%'))as AwalQty " +
                   "from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, '" + Thn + s + bln + "' as HMY from (  " +
                   "select distinct itemid  from bm_destacking where tglproduksi>='6/1/2013' " +
                   ") as A " +
                   ") as M1 " +
                   ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or B99<>0 or C99<>0 or Q99<>0 order by M2.NoDocument ";
            else
            strSQL = "select itemID,HMY,NoDocument,sum(AwalQty) as AwalQty,sum(InProdQty) as InProdQty,sum(InP99) as InP99,sum(InAdjustQty) as InAdjustQty," +
                "sum(outAdjustQty) as outAdjustQty,sum(H99) as H99,sum(I99) as I99,sum(B99) as B99,sum(C99) as C99,sum(Q99) as Q99,sum(OutP99) as OutP99," +
                "(select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= atALL.itemid)as volume from(" +
                "select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,outAdjustQty,H99,I99,B99,C99,Q99,OutP99 from (  " +
                "select itemID,HMY,partno as NoDocument, " +
                "(select isnull(SUM(qty),0) from BM_Destacking where qty>0 and LEFT(CONVERT(varchar,TglProduksi, 112), 6) = M1.HMY " +
                "and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as InProdQty,0 as InP99," +
                "(select isnull(SUM(qty),0) from vw_KartustockWIP where qty>0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%in%') as InAdjustQty, " +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and (lokasi='H99' and process<>'lari')) as H99, " +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and (lokasi='I99' and process<>'lari')) as I99, " +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='B99' and process<>'lari') as B99, " +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='C99' and process<>'lari') as C99, " +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='Q99' and process<>'lari') as Q99, " +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and process='lari') as OutP99," +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%ou%') as outAdjustQty, " +
                "(select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln='" + lastperiode + "' and itemid =M1.ItemID and " +
                "LokID not in (select ID from FC_Lokasi where Lokasi='p99') )as AwalQty " +
                "from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, '" + Thn + s + bln + "' as HMY from (" +
                "select distinct itemid  from bm_destacking where tglproduksi>='6/1/2013'" +
                ") as A " +
                ") as M1 " +
                ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or I99<>0  or B99<>0 or C99<>0 or Q99<>0 " +
                "union all " +
                "select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,outAdjustQty,H99,I99,B99,C99,Q99,OutP99 from (  " +
                "select itemID,HMY,'P:' + partno as NoDocument,0 as InProdQty," +
                "(select isnull(SUM(qty),0) from vw_KartustockWIP2 where qty>0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY) as InP99," +
                "(select isnull(SUM(qty),0) from vw_KartustockWIP2 where qty>0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%in%') as InAdjustQty, 0 as H99,0 as I99, 0 as B99, " +
                "(select isnull(SUM(qty)*-1,0) from vw_KartustockWIP2 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY) as C99, " +
                "0 as Q99, 0 as OutP99, (select isnull(SUM(qty)*-1,0) from vw_KartustockWIP2 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%ou%') as outAdjustQty, " +
                "(select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln='" + lastperiode + "' and itemid =M1.ItemID and " +
                "LokID in (select ID from FC_Lokasi where Lokasi='p99'))as AwalQty " +
                "from(select distinct A.itemid0 as ItemID,(select partno from fc_items where ID=A.itemid0) as partno, '" + Thn + s + bln + "' as HMY from (" +
                "select distinct itemid0  from vw_KartustockWIP2 where tanggal >='6/1/2013'" +
                ") as A " +
                ") as M1 " +
                ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or I99<>0 or B99<>0 or C99<>0 " +
                "or Q99<>0 or InP99<>0 or OutP99<>0 " +
                ") as atALL group by itemID,HMY,NoDocument order by NoDocument";

            return strSQL;

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
